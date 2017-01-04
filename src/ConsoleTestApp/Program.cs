using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RealTimeLevelEditor;
using static System.Console;

namespace ConsoleTestApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var indexList = new List<TileIndex>
			{
				new TileIndex(214, 774),
				new TileIndex(-177, 332),
				new TileIndex(-101103, 0987),
				new TileIndex(-1, -5),
				new TileIndex(9988656, -2322087)
			};

			string json = JsonConvert.SerializeObject(indexList, Formatting.Indented);
			WriteLine(json);
			var deserialized = JsonConvert.DeserializeObject<List<TileIndex>>(json);
			foreach (var index in deserialized)
			{
				WriteLine(index);
			}

			ReadLine();
		}
	}
}
