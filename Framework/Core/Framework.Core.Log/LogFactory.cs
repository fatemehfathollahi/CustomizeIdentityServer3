using Framework.Core.Contracts.Log;
using Framework.Core.Log.LogStorages;
using System;
using System.IO;

namespace Framework.Core.Log
{
	public class LogFactory : ILogFactory
	{
		#region Fields

		private DateTime _currentDay;
		private readonly string _logPath;

		#endregion Fields

		#region Ctors

		public LogFactory()
		{
			_currentDay = DateTime.Now;
			_logPath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
		}

		#endregion Ctors

		#region ILogFactory Members

		public ILogStorage CreateStorage()
		{
			if (!Directory.Exists(_logPath))
			{
				Directory.CreateDirectory(_logPath);
			}

			return new LogStorage(_logPath + _currentDay.ToString("yyyy-MM-dd") + ".logdb");
		}

		#endregion ILogFactory Members

		#region IDisposable Members

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion IDisposable Members
	}
}