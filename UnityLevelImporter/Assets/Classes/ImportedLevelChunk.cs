using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.ObjectModel;

namespace UnityLevelImporter
{
	[Serializable]
	public class LevelChunk
	{
		public LevelChunk()
		{

		}
		internal LevelChunk(Rectangle region, IEnumerable<Tile> tiles)
		{
			Region = region;
			Tiles = tiles.ToArray();
		}

		[JsonProperty]
		public Rectangle Region { get; private set; }

		[JsonProperty]
		public Tile[] Tiles { get; private set; }
	}

	[Serializable]
	public class ImportedLevel
	{
		[JsonProperty]
		public LevelChunk[] Chunks { get; set; }
	}

	public class LevelChunkBuilder
	{
		public LevelChunkBuilder()
		{
			_tileLookup = new Dictionary<TileIndex, Tile>();
		}

		public Rectangle Region { get; set; }

		public void AddTile(Tile tile)
		{
			if (tile == null)
				throw new ArgumentNullException("tile");

			_tileLookup[tile.Index] = tile;
		}
		public bool RemoveAt(TileIndex index)
		{
			return _tileLookup.Remove(index);
		}

		public LevelChunk ToLevelChunk()
		{
			return new LevelChunk(Region, _tileLookup.Values.ToArray());
		}

		private Dictionary<TileIndex, Tile> _tileLookup;
	}

	[Serializable]
	public class Tile
	{
		public Tile() { }

		public Tile(TileIndex index, string data)
		{
			Index = index;
			Data = data;
		}

		[JsonProperty]
		public TileIndex Index { get; private set; }

		[JsonProperty]
		public string Data { get; set; }

		public override string ToString()
		{
			return "{ " + Index + ", " + Data + " }";
		}
	}
}
