using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UnityLevelImporter
{
	/// <summary>
	/// Responsible for importing a level that's been serialized to JSON.
	/// </summary>
	public abstract class LevelImporter
	{
		protected abstract TextReader GetTextReader();

		public IEnumerable<ImportedLevelChunk> LoadLevel()
		{
			using (TextReader textReader = GetTextReader())
			using (var jsonReader = new JsonTextReader(textReader))
			{
				var serializer = new JsonSerializer();
				return (ImportedLevelChunk[])serializer.Deserialize(jsonReader, typeof(ImportedLevelChunk[]));
			}
		}
	}
}
