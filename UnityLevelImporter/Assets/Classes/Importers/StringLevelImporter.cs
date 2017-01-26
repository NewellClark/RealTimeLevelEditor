using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UnityLevelImporter
{
	/// <summary>
	/// A <c>LevelImporter</c> that imports levels directly from a JSON <c>System.String</c>.
	/// </summary>
	public class StringLevelImporter : LevelImporter
	{
		public StringLevelImporter(StringBuilder source)
		{
			_source = source;
		}
		public StringLevelImporter(string source) 
			: this(new StringBuilder(source)) { }

		protected override TextReader GetTextReader()
		{
			return new StringReader(_source.ToString());
		}

		private StringBuilder _source;
	}
}
