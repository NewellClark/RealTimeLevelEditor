using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityLevelImporter;

namespace UnityLevelImporterTests
{
	public class DotNetStreamLevelImporterTests : LevelImporterTests
	{
		/// <summary>
		/// Serializes the specified level, and then provides a <c>LevelImporter</c> to deserialize it.
		/// </summary>
		/// <param name="levelChunks"></param>
		/// <returns></returns>
		protected override LevelImporter GetLevelImporter(ImportedLevel levelChunks)
		{
			return new DotNetStreamLevelImporter(() => StreamFactory(levelChunks));
		}

		private Stream StreamFactory(ImportedLevel level)
		{
			//	Intentionally not putting this in a using. It will be used as a return-value.
			Stream stream = new MemoryStream();
			using (var streamWriter = new StreamWriter(stream, Encoding.UTF8, 1024, true))
			{
				var serializer = new JsonSerializer();
				serializer.Serialize(streamWriter, level);
				streamWriter.Flush();
				stream.Seek(0, SeekOrigin.Begin);

				return stream;
			}
		}
	}
}
