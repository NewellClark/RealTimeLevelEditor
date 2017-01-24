using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace UnityLevelImporter
{
	class ImportedLevel
	{

	}

	[Serializable]
	class Tile
	{
		public Tile()
		{

		}

		public Tile(TileIndex index, string data)
		{
			Index = index;
			Data = data;
		}

		[JsonProperty]
		public TileIndex Index { get; private set; }

		[JsonProperty]
		public string Data { get; private set; }
	}




}
