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
			if (streamFactory == null)
				throw new ArgumentNullException("streamFactory");

			_streamFactory = streamFactory;
		}

		protected override TextReader GetTextReader()
		{
			Stream stream = _streamFactory();
			return new StreamReader(stream);
		}

		private Func<Stream> _streamFactory;
	}
}
