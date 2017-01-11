using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RealTimeLevelEditor;
using static System.Console;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using System.IO;

namespace ConsoleTestApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WriteLine("Enter the filepath to save the random tiles to:");
			string path = ReadLine();

			var tiles = GetTestTiles(
				new Rectangle(-2000, -2000, 2000, 2000),
				new Size(40, 40));
			string json = JsonConvert.SerializeObject(tiles, Formatting.Indented);
			try
			{
				File.WriteAllText(path, json);
			}
			catch (Exception ex)
			{
				WriteLine(ex.Message);
			}

			ReadLine();
		}

		private static IEnumerable<Tile<T>> GetTestTiles<T>(
			Rectangle region, 
			Size density,
			Func<TileIndex, T> dataFactory)
		{
			for (long x = region.Left; x < region.Right; x += density.X)
			{
				for (long y = region.Top; y < region.Bottom; y += density.Y)
				{
					var index = new TileIndex(x, y);
					yield return new Tile<T>(
						index,
						dataFactory(index));
				}
			}
		}

		private static IEnumerable<Tile<Guid>> GetTestTiles(
			Rectangle region,
			Size density)
		{
			return GetTestTiles(
				region, density,
				x => Guid.NewGuid());
		}

		private static void JsonSerializationTests()
		{
			var lookup = new Dictionary<TileIndex, string>
			{
				{ new TileIndex(76, -29), "Caterpillers" },
				{ new TileIndex(11, 5), "Dogs" },
				{ new TileIndex(-8, -2), "Cats" },
				{ new TileIndex(519, 87), "Fishies" }
			};
			//string json = JsonConvert.SerializeObject(lookup, Formatting.Indented);
			//var deserialized = JsonConvert.DeserializeObject<Dictionary<TileIndex, string>>(json);
			var casted = (ICollection<KeyValuePair<TileIndex, string>>)lookup;
			var json = JsonConvert.SerializeObject(casted, Formatting.Indented);
			var rawDeserialized = JsonConvert.DeserializeObject<ICollection<KeyValuePair<TileIndex, string>>>(json);
			var deserialized = new Dictionary<TileIndex, string>();
			foreach (var pair in rawDeserialized)
			{
				deserialized.Add(pair.Key, pair.Value);
			}

			WriteLine("Json:");
			WriteLine(json);
			WriteLine();
			WriteLine("Original:");
			WriteLine(PrintDictionary(lookup));
			WriteLine();
			WriteLine("After Deserialization:");
			WriteLine(PrintDictionary(deserialized));
			WriteLine();
		}

		private static string PrintDictionary<K, V>(IDictionary<K, V> lookup)
		{
			var sb = new StringBuilder();
			foreach (KeyValuePair<K, V> pair in lookup)
			{
				string line = $"{pair.Key}: {pair.Value}";
				sb.AppendLine(line);
			}

			return sb.ToString();
		}

		private static void TileIndexListTest()
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
		}
	}
}
