using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UnityLevelImporter
{
	/// <summary>
	/// Reads a level from a <c>Stream</c>
	/// </summary>
	public abstract class StreamLevelImporter : LevelImporter
	{
		protected override TextReader GetTextReader()
		{
			return new StreamReader(GetStream());
		}

		protected abstract Stream GetStream();
	}
}
