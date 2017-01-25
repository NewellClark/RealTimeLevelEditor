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
		/// <summary>
		/// When overridden in a derived class, gets a <c>TextReader</c> that will be used to deserialize the level
		/// from the source owned by the current <c>LevelImporter</c>.
		/// </summary>
		/// <returns></returns>
		protected abstract TextReader GetTextReader();

		public ImportedLevel LoadLevel()
		{
			using (TextReader textReader = GetTextReader())
			using (var jsonReader = new JsonTextReader(textReader))
			{
				var serializer = new JsonSerializer();
				return (ImportedLevel)serializer.Deserialize(jsonReader, typeof(ImportedLevel));
			}
		}


	}
}
