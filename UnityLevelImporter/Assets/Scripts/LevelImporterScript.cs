using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace UnityLevelImporter
{
	public abstract class LevelImporterScript : MonoBehaviour
	{
		[SerializeField]
		private List<PrefabTilePair> _tileTypeDefinitions;
		public IList<PrefabTilePair> TileTypeDefinitions
		{
			get { return _tileTypeDefinitions; }
		}

		public void LoadLevel()
		{
			using (TextReader textReader = GetLevelDataReader())
			using (JsonReader jsonReader = new JsonTextReader(textReader))
			{
				var serializer = new JsonSerializer();
				var chunks = (ImportedLevelChunk[])serializer.Deserialize(jsonReader);
				foreach (var chunk in chunks)
				{
					PopulateChunk(chunk);
				}
			}
		}

		/// <summary>
		/// Gets a <c>TextReader</c> that will be used to read the level from wherever it is stored.
		/// </summary>
		/// <returns></returns>
		protected abstract TextReader GetLevelDataReader();

		private void PopulateChunk(ImportedLevelChunk chunk)
		{
			HashSet<TileIndex> loadedIndeces = new HashSet<TileIndex>();
			foreach (var tile in chunk.Tiles)
			{

			}
		}
	}

	[System.Serializable]
	public class PrefabTilePair
	{
		public string Type { get; set; }
		public GameObject Prefab { get; set; }
	}
}
