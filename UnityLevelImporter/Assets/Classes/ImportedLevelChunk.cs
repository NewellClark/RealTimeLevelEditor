using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UnityLevelImporter
{
	[Serializable]
	class ImportedLevelChunk
	{
		[JsonProperty]
		public Rectangle Region { get; private set; }

		[JsonProperty]
		public Tile[] Tiles { get; private set; }
	}

	[Serializable]
	class Tile
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
	}
}
