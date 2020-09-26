using Framework.Core.Contracts.Log;
using LiteDB;

namespace Framework.Core.Log.LogStorages
{
	public class LogStorage : LiteDatabase, ILogStorage
	{
		public LogStorage(string connectionString) : base(connectionString)
		{
		}
	}
}