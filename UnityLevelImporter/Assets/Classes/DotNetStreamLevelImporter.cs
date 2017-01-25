using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UnityLevelImporter
{
	public class DotNetStreamLevelImporter : LevelImporter
	{
		public DotNetStreamLevelImporter(Func<Stream> streamFactory)
		{
			_streamFactory = streamFactory;
		}

		protected override TextReader GetTextReader()
		{
			return new StreamReader(_streamFactory());
		}

		private Func<Stream> _streamFactory;
	}
}
