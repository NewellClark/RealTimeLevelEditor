using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using UnityLevelImporter;
using System.IO;

namespace UnityLevelImporterTests
{
	public abstract class LevelImporterTests
	{
		/// <summary>
		/// Serializes the specified level and returns a text-reader to the serialized level.
		/// </summary>
		/// <param name="levelToSerializeAndRead"></param>
		/// <returns></returns>
		protected abstract TextReader GetTextReader(IEnumerable<ImportedLevelChunk> levelToSerializeAndRead);


	}
}
