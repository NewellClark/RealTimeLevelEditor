using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeLevelEditor
{
	/// <summary>
	/// Serializes <c>Level`T'</c> into a format that is compatible with various external tools.
	/// </summary>
	public class SerializableLevelExporter
	{
		private class ExportableLevel<T>
		{
			public ExportableLevel() { }
			public ExportableLevel(IEnumerable<LevelChunk<T>> chunks)
			{
				Chunks = chunks.ToArray();
			}

			[JsonProperty]
			public LevelChunk<T>[] Chunks { get; set; }
		}

		/// <summary>
		/// Exports the specified level to a JSON format that is compatible with various external tools.
		/// </summary>
		/// <typeparam name="T">Tile data type.</typeparam>
		/// <param name="textWriter">The <c>TextWriter</c> that the serialized form of the level will
		/// be written to.</param>
		/// <param name="level">The level to export.</param>
		public void Export<T>(TextWriter textWriter, Level<T> level)
		{
			var chunks = level.GetChunks()
				.Select(x => x.Data)
				.ToArray();
			var exportable = new ExportableLevel<T>(chunks);

			using (var jsonWriter = new JsonTextWriter(textWriter))
			{
				var serializer = new JsonSerializer();
				serializer.Formatting = Formatting.Indented;
				serializer.Serialize(jsonWriter, exportable);
			}
		}
	}
}
