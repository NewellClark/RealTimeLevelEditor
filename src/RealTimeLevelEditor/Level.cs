using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	public class Level<T> : TileCollection<T>
	{
		public Level(
			IChunkRepository<T> chunkRepository,
			Size chunkSize)
		{
			_chunkSize = chunkSize;
			_repo = chunkRepository;
		}

		public override Tile<T> this[TileIndex index]
		{
			get
			{
				var chunkIndex = index.ToChunkIndex(_chunkSize);
				if (!_repo.Contains(chunkIndex))
					throw new KeyNotFoundException(
						$"There is no chunk with index {chunkIndex} in the repository, " +
						$"which is where tile {index} would be located.");
				var chunk = _repo.Load(chunkIndex);
				if (!chunk.Data.Tiles.Contains(index))
					throw new KeyNotFoundException(
						$"There is no tile with index of {index} in the repository.");
				return chunk.Data.Tiles[index];
			}
		}

		public override void AddOrUpdate(Tile<T> tile)
		{
			var chunkIndex = tile.Index.ToChunkIndex(_chunkSize);
			var chunkTile = LoadOrCreate(chunkIndex);
			chunkTile.Data.Tiles.AddOrUpdate(tile);
			_repo.Save(chunkTile);
		}

		public override void AddOrUpdate(IEnumerable<Tile<T>> tiles)
		{
			var groups = from tile in tiles
						 group tile by tile.Index.ToChunkIndex(_chunkSize);

			foreach (var grouping in groups)
			{
				var chunk = LoadOrCreate(grouping.Key);
				chunk.Data.Tiles.AddOrUpdate(grouping);
				_repo.Save(chunk);
			}
		}

		public override bool Contains(TileIndex index)
		{
			var chunkIndex = index.ToChunkIndex(_chunkSize);
			if (!_repo.Contains(chunkIndex))
				return false;
			var chunk = _repo.Load(chunkIndex);
			return chunk.Data.Tiles.Contains(index);
		}

		public override void Delete(TileIndex index)
		{
			var chunkIndex = index.ToChunkIndex(_chunkSize);
			if (!_repo.Contains(chunkIndex))
				return;
			var chunk = _repo.Load(chunkIndex);
			chunk.Data.Tiles.Delete(index);
			_repo.Save(chunk);
		}

		public override void Delete(IEnumerable<TileIndex> indeces)
		{
			var groups = from index in indeces
						 group index by index.ToChunkIndex(_chunkSize);

			foreach (var grouping in groups)
			{
				if (!_repo.Contains(grouping.Key))
					continue;
				var chunk = _repo.Load(grouping.Key);
				chunk.Data.Tiles.Delete(grouping);
				_repo.Save(chunk);
			}
		}

		public void Delete(Rectangle region)
		{
			Delete(region.EnclosedTiles);
		}

		public Size ChunkSize => _chunkSize;

		/// <summary>
		/// Gets all the chunks that at least partially overlap the specified region.
		/// The region is specified in tile-coordinates, not chunk coordinates.
		/// </summary>
		/// <param name="region">The region to load chunks from. Must be specified in
		/// Tile coordinates, not chunk coordinates.</param>
		/// <returns>Every chunk that overlaps the specified region.</returns>
		public IEnumerable<Tile<LevelChunk<T>>> GetChunksInRegion(Rectangle region)
		{
			Rectangle chunkRegion = region.ToChunkCoordinates(ChunkSize);
			var results = _repo.Indeces
				.Where(x => chunkRegion.Contains(x))
				.Select(x => _repo.Load(x));

			return results;
		}

		/// <summary>
		/// Gets all the tiles who's indeces are included in the specified group.
		/// </summary>
		/// <param name="tileIndeces"></param>
		/// <returns></returns>
		public IEnumerable<Tile<T>> GetExistingTiles(IEnumerable<TileIndex> tileIndeces)
		{
			var tileGroups = tileIndeces
				.GroupBy(x => x.ToChunkIndex(ChunkSize))
				.Where(x => _repo.Contains(x.Key))
				.Select(x =>
				{
					var chunk = _repo.Load(x.Key);
					return x.Where(y => chunk.Data.Tiles.Contains(y))
					.Select(y => chunk.Data.Tiles[y]);
				});

			foreach (var tileGroup in tileGroups)
			{
				foreach (var tile in tileGroup)
				{
					yield return tile;
				}
			}
		}

		public IEnumerable<Tile<T>> GetTilesInRegion(Rectangle region)
		{
			var groupByChunks = GetChunksInRegion(region)
				.Select(x => x.Data.Tiles);
			
			foreach (var chunkGroup in groupByChunks)
			{
				var inRegion = chunkGroup
					.Where(x => chunkGroup.Region.Contains(x.Index));
				foreach (var tile in inRegion)
					yield return tile;
			}
		}

		public override IEnumerator<Tile<T>> GetEnumerator()
		{
			var chunks = _repo.Indeces
				.Select(x => _repo.Load(x));

			foreach (var chunk in chunks)
			{
				foreach (var tile in chunk.Data.Tiles)
				{
					yield return tile;
				}
			}
		}

		public override int Count
		{
			get
			{
				int count = 0;
				foreach (var index in _repo.Indeces)
				{
					var chunk = _repo.Load(index);
					count += chunk.Data.Tiles.Count;
				}

				return count;
			}
		}

		private Tile<LevelChunk<T>> CreateEmpty(TileIndex chunkIndex)
		{
			var topLeft = new TileIndex(
				chunkIndex.X * _chunkSize.X,
				chunkIndex.Y * _chunkSize.Y);
			var region = new Rectangle(topLeft.X, topLeft.Y,
				_chunkSize.X, _chunkSize.Y);
			var chunk = new LevelChunk<T>(region);
			var result = new Tile<LevelChunk<T>>(chunkIndex, chunk);

			return result;
		}

		private Tile<LevelChunk<T>> LoadOrCreate(TileIndex chunkIndex)
		{
			if (_repo.Contains(chunkIndex))
				return _repo.Load(chunkIndex);

			return CreateEmpty(chunkIndex);
		}

		private Size _chunkSize;
		private IChunkRepository<T> _repo;
	}
}
