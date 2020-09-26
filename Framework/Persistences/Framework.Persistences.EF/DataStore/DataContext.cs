using Framework.Persistences.Contracts.DataStore;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Framework.Persistences.EF.DataStore
{
	public class DataContext : DbContext, IDataStore
	{
		#region Ctor

		protected DataContext(DbCompiledModel model)
			: base(model)
		{
		}

		public DataContext(string nameOrConnectionString)
			: base(nameOrConnectionString)
		{
		}

		public DataContext(string nameOrConnectionString, DbCompiledModel model)
			: base(nameOrConnectionString, model)
		{
		}

		public DataContext(DbConnection existingConnection, bool contextOwnsConnection)
			: base(existingConnection, contextOwnsConnection)
		{
		}

		public DataContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
			: base(objectContext, dbContextOwnsObjectContext)
		{
		}

		public DataContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
			: base(existingConnection, model, contextOwnsConnection)
		{
		}

		#endregion Ctor
	}
}