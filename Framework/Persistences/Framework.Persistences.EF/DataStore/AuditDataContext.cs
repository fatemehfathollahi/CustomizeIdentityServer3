using Audit.EntityFramework;
using Framework.Core.Audit;
using Framework.Core.Contracts.Audit.Model;
using Framework.Persistences.Contracts.DataStore;
using Framework.Persistences.EF.Configurations;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Framework.Persistences.EF.DataStore
{
	public class AuditDataContext : AuditDbContext, IDataStore
	{
		#region Props

		public IDbSet<AuditEvent> Events { get; set; }

		#endregion Props

		#region Ctor

		protected AuditDataContext(DbCompiledModel model)
			: base(model)
		{
			InitailizeAudit();
		}

		public AuditDataContext(string nameOrConnectionString)
			: base(nameOrConnectionString)
		{
			InitailizeAudit();
		}

		public AuditDataContext(string nameOrConnectionString, DbCompiledModel model)
			: base(nameOrConnectionString, model)
		{
			InitailizeAudit();
		}

		public AuditDataContext(DbConnection existingConnection, bool contextOwnsConnection)
			: base(existingConnection, contextOwnsConnection)
		{
			InitailizeAudit();
		}

		public AuditDataContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
			: base(objectContext, dbContextOwnsObjectContext)
		{
			InitailizeAudit();
		}

		public AuditDataContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
			: base(existingConnection, model, contextOwnsConnection)
		{
			InitailizeAudit();
		}

		#endregion Ctor

		#region Override Members

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add<AuditEvent>(new AuditEventsConfigurations());
		}

		#endregion Override Members

		#region Private Members

		private void InitailizeAudit()
		{
			AuditEventType = "{database}_{context}";
			Mode = AuditOptionMode.OptOut;
			IncludeEntityObjects = false;

			AuditDataProvider = new CustomSqlDataProvider(this);
		}

		#endregion Private Members

		#region Protected Members

		protected virtual void AuditIgnorePropertie(Audit.Core.AuditScope auditScope, string columnName)
		{
			foreach (EventEntry entry in auditScope.Event.GetEntityFrameworkEvent().Entries)
			{
				if (entry.Changes != null && entry.Changes.Any(o => o.ColumnName == columnName))
				{
					EventEntryChange changeEntry = entry.Changes.Find(o => o.ColumnName == columnName);

					changeEntry.OriginalValue = (string.IsNullOrEmpty((string)changeEntry.OriginalValue) ? "No Object" : "Old Object");
					changeEntry.NewValue = (string.IsNullOrEmpty((string)changeEntry.NewValue) ? "No Object" : "Object Changed");
				}

				if (entry.ColumnValues.Any(o => o.Key == columnName))
				{
					entry.ColumnValues[columnName] = (string.IsNullOrEmpty((string)entry.ColumnValues[columnName]) ? "No Object" : "Object Added");
				}
			}
		}

		#endregion Protected Members
	}
}