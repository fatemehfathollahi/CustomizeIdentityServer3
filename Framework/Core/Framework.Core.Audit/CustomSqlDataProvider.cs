using Audit.Core;
using Framework.Core.Common.Extensions;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Framework.Core.Audit
{
	public class CustomSqlDataProvider : AuditDataProvider
	{
		#region Fields

		private readonly string _schema = "dbo";
		private readonly string _tableName = "Events";
		private readonly string _idColumnName = "EventId";
		private readonly string _jsonColumnName = "Data";
		private readonly string _yearColumnName = "Year";
		private readonly string _insertedDateColumnName = "InsertedDate";
		private readonly string _lastUpdatedDateColumnName = "LastUpdatedDate";
		private readonly string _connectionString = null;
		private readonly DbContext _context = null;

		#endregion Fields

		#region Ctors

		public CustomSqlDataProvider(
			DbContext context = null,
			string connectionString = null,
			string schema = null,
			string tableName = null,
			string idColumnName = null,
			string jsonColumnName = null,
			string lastUpdatedDateColumnName = null,
			string insertedDateColumnName = null)
		{
			if (context == null && string.IsNullOrEmpty(connectionString))
			{
				throw new NullReferenceException("Context Or ConnectionString is Null or Invalid");
			}

			if (!string.IsNullOrEmpty(connectionString))
			{
				_connectionString = connectionString;
				_context = new DbContext(_connectionString);
			}

			if (context != null)
			{
				_context = context;
				_connectionString = context.Database.Connection.ConnectionString;
			}

			if (!string.IsNullOrEmpty(schema))
			{
				_schema = schema;
			}

			if (!string.IsNullOrEmpty(tableName))
			{
				_tableName = tableName;
			}

			if (!string.IsNullOrEmpty(idColumnName))
			{
				_idColumnName = idColumnName;
			}

			if (!string.IsNullOrEmpty(jsonColumnName))
			{
				_jsonColumnName = jsonColumnName;
			}

			if (!string.IsNullOrEmpty(lastUpdatedDateColumnName))
			{
				_lastUpdatedDateColumnName = lastUpdatedDateColumnName;
			}

			if (!string.IsNullOrEmpty(insertedDateColumnName))
			{
				_insertedDateColumnName = insertedDateColumnName;
			}
		}

		#endregion Ctors

		#region Private Members

		private string FullTableName => _schema != null
					? string.Format("[{0}].[{1}]", _schema, _tableName)
					: string.Format("[{0}]", _tableName);

		#endregion Private Members

		#region AuditDataProvider Override Members

		public override object InsertEvent(AuditEvent auditEvent)
		{
			SqlParameter json = new SqlParameter("json", auditEvent.ToJson());

			using (DbContext ctx = new DbContext(_connectionString))
			{
				string cmdText = string.Format(
					"INSERT INTO {0} ([{1}],[{2}],[{3}]) OUTPUT CONVERT(NVARCHAR(MAX), INSERTED.[{4}]) AS [Id] VALUES (@json,{5},{6})"
					, FullTableName
					, _jsonColumnName
					, _insertedDateColumnName
					, _yearColumnName
					, _idColumnName
					, "GETUTCDATE()"
					, DateTime.UtcNow.ToStringPersianWithFormat("yyyy")
				);

				System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = ctx.Database.SqlQuery<string>(cmdText, json);
				return result.FirstOrDefault();
			}
		}

		public override void ReplaceEvent(object eventId, AuditEvent auditEvent)
		{
			string json = auditEvent.ToJson();
			using (DbContext ctx = new DbContext(_connectionString))
			{
				string ludScript = _lastUpdatedDateColumnName != null ? string.Format(", [{0}] = GETUTCDATE()", _lastUpdatedDateColumnName) : string.Empty;
				string cmdText = string.Format("UPDATE {0} SET [{1}] = @json{2} WHERE [{3}] = @eventId", FullTableName, _jsonColumnName, ludScript, _idColumnName);

				ctx.Database.ExecuteSqlCommand(cmdText, new SqlParameter("@json", json), new SqlParameter("@eventId", eventId));
			}
		}

		#endregion AuditDataProvider Override Members
	}
}