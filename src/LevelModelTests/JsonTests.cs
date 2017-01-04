using Newtonsoft.Json;
using RealTimeLevelEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Reflection;

namespace LevelModelTests
{
	public class JsonTests
	{
		[Fact]
		internal void Rectangle_Serialize()
		{
			var rect = new Rectangle(0987987, -3241324, 522338887, 234);
			string json = JsonConvert.SerializeObject(rect);
			var copy = JsonConvert.DeserializeObject<Rectangle>(json);

			Assert.True(rect == copy);
		}

		[Fact]
		internal void TileIndex_Serialize()
		{
			var tile1 = new TileIndex(-098742, -397087071);
			string json = JsonConvert.SerializeObject(tile1);
			var tile2 = JsonConvert.DeserializeObject<TileIndex>(json);
			Assert.True(tile1 == tile2);
		}

		[Fact]
		internal void Tile_Serialize()
		{
			var tile = new Tile<string>(new TileIndex(135, -332335), "Hello world!");
			string json = JsonConvert.SerializeObject(tile);
			var copy = JsonConvert.DeserializeObject<Tile<string>>(json);

			bool result = tile.Index == copy.Index &&
				tile.Data == copy.Data;

			Assert.True(result);
		}

		[Fact]
		internal void Tile_JsonCtorParamNamesMatchProperties()
		{
			Assert.True(AreJsonCtorParameterNamesValid<Tile<object>>());
		}

		[Fact]
		internal void LevelChunk_Serialize()
		{
			var tiles = new Tile<string>[]
			{
				Helpers.GetTile(50, 22, "Only One - YellowCard"),
				Helpers.GetTile(-27, 809, "Lady Gaga - Hair"),
				Helpers.GetTile(576, 649, "Angels and AirWaves - The War")
			};
			var chunk = new LevelChunk<string>(
				new Rectangle(-10000, -10000, 20000, 20000),
				tiles);
			var json = JsonConvert.SerializeObject(chunk);
			var copy = JsonConvert.DeserializeObject<LevelChunk<string>>(json);

			Assert.True(ChunksEqualByValue(chunk, copy));
		}

		[Fact]
		internal void LevelChunk_JsonCtorParamNamesMatchProperties()
		{
			Assert.True(AreJsonCtorParameterNamesValid<LevelChunk<object>>());
		}

		[Fact]
		internal void Size_Serialize()
		{
			long x = 29;
			long y = 18;
			Size size = new Size(x, 18);
			string json = JsonConvert.SerializeObject(size);
			Size copy = JsonConvert.DeserializeObject<Size>(json);

			Assert.True(size == copy);
			Assert.True(copy.X == x);
			Assert.True(copy.Y == y);
		}

		[Fact]
		internal void Size_JsonCtorParamNamesMatchProperties()
		{
			Assert.True(AreJsonCtorParameterNamesValid<Size>());
		}


		private ConstructorInfo GetJsonCtor(Type type)
		{
			var ctor = type
				.GetConstructors(
					BindingFlags.Public |
					BindingFlags.NonPublic |
					BindingFlags.Instance)
				.Select(x => new
				{
					Ctor = x,
					Attributes = x.GetCustomAttributes<JsonConstructorAttribute>()
				});
			var jsonCtor = ctor
				.Where(x => x.Attributes.Count() > 0)
				.Select(x => x.Ctor)
				.SingleOrDefault();

			return jsonCtor;
		}
		private ConstructorInfo GetJsonCtor<T>()
		{
			return GetJsonCtor(typeof(T));
		}

		private bool CanPropertyBeSerializedByJson(PropertyInfo property)
		{
			return property.GetMethod.IsPublic ||
				property.GetMethod
				.GetCustomAttributes<JsonPropertyAttribute>()
				.Count() > 0;
		}

		/// <summary>
		/// Determines whether the paramater names for a class's Json 
		/// constructor match the names of json-serializable properties
		/// within that class.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private bool AreJsonCtorParameterNamesValid(Type type)
		{
			var ctor = GetJsonCtor(type);

			if (ctor == null)
				return false;

			var args = ctor.GetParameters();
			var propertyNames = type.GetProperties()
				.Where(x => CanPropertyBeSerializedByJson(x))
				.Select(x => x.Name.ToLower());

			foreach (var arg in args)
			{
				bool result = propertyNames.Contains(arg.Name.ToLower());
				if (!result)
					return false;
			}

			return true;
		}
		private bool AreJsonCtorParameterNamesValid<T>()
		{
			return AreJsonCtorParameterNamesValid(typeof(T));
		}

		private bool TilesEqual<T>(Tile<T> lhs, Tile<T> rhs)
		{
			return lhs.Index == rhs.Index &&
				lhs.Data.Equals(rhs.Data);
		}

		private bool ChunksEqualByValue(
			LevelChunk<string> lhs, 
			LevelChunk<string> rhs)
		{
			//return lhs.Region == rhs.Region &&
			//	Helpers.SeriesHaveSameElementsAndSizes(
			//		lhs.Tiles, rhs.Tiles, (l, r) => l == r);

			bool regionsEqual = lhs.Region == rhs.Region;
			bool contentsEqual = true;
			foreach (var tile in lhs.Tiles)
			{
				if (!rhs.Tiles.Contains(tile.Index))
				{
					contentsEqual = false;
					break;
				}
			}

			return regionsEqual &&
				contentsEqual &&
				lhs.Tiles.Count() == rhs.Tiles.Count();
		}
	}
}
