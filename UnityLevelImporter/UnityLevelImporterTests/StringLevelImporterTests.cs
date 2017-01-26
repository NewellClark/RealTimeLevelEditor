using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityLevelImporter;
using Newtonsoft.Json;

namespace UnityLevelImporterTests
{
	public class StringLevelImporterTests : LevelImporterTests
	{
		protected override LevelImporter GetLevelImporter(ImportedLevel levelChunks)
		{
			var serializer = new JsonSerializer();
			var source = new StringBuilder();
			using (var writer = new JsonTextWriter(new StringWriter(source)))
			{
				serializer.Serialize(writer, levelChunks);
			}

			return new StringLevelImporter(source);
		}
	}
}
