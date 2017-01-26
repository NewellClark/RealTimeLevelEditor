using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace UnityLevelImporter
{
	public class LevelLoader : MonoBehaviour
	{
		[SerializeField]
		private List<PrefabTilePair> _tileTypes;
		public IList<PrefabTilePair> TileTypes
		{
			get { return _tileTypes; }
		}

		[SerializeField]
		private string _filePath;
		public string FilePath
		{
			get { return _filePath; }
			set { _filePath = value; }
		}

		private void Start()
		{
			PopulateLevel();
		}

		private void PopulateLevel()
		{
			var importer = new DotNetStreamLevelImporter(() => File.OpenRead(_filePath));
			ImportedLevel level = importer.LoadLevel();
			var prefabLookup = _tileTypes
				.ToDictionary(x => x.Type, x => x.Prefab);

			foreach (var tile in GetTilesInLevel(level))
			{
				GameObject prefab;
				if (!prefabLookup.TryGetValue(tile.Data, out prefab))
				{
					throw new KeyNotFoundException("No prefab defined for `" + tile.Data + "'");
				}

				Instantiate(prefabLookup[tile.Data],
					new Vector3(tile.Index.X, tile.Index.Y), Quaternion.identity);
			}
		}

		private Stream GetFileStream()
		{
			return File.OpenRead(_filePath);
		}

		private static IEnumerable<Tile> GetTilesInLevel(ImportedLevel level)
		{
			foreach (var chunk in level.Chunks)
			{
				foreach (var tile in chunk.Tiles)
				{
					yield return tile;
				}
			}
		}

		
	}

	[System.Serializable]
	public class PrefabTilePair
	{
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		}
		[SerializeField]
		private string _type;

		public GameObject Prefab
		{
			get { return _prefab; }
			set { _prefab = value; }
		}
		[SerializeField]
		private GameObject _prefab;
	}
}
