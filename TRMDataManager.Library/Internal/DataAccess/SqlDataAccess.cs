using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.Library.Internal.DataAccess
{
	internal class SqlDataAccess : IDisposable
	{
		public string GetConnectionString(string name)
		{
			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
		}

		public List<T> LoadData<T, U>(string storedProcedure, U paramaters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				List<T> rows = connection.Query<T>(storedProcedure, paramaters, commandType: CommandType.StoredProcedure).ToList();

				return rows;
			}
		}

		public void SaveData<T>(string storedProcedure, T paramaters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Execute(storedProcedure, paramaters, commandType: CommandType.StoredProcedure);
			}
		}


		private IDbConnection _connection;
		private IDbTransaction _transaction;
		public void StartTransaction(string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			_connection = new SqlConnection(connectionString);
			_connection.Open();

			_transaction = _connection.BeginTransaction();
		}

		public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U paramaters)
		{
			List<T> rows = _connection.Query<T>(storedProcedure, paramaters, 
				commandType: CommandType.StoredProcedure,
				transaction: _transaction)
				.ToList();

			return rows;
		}

		public void SaveDataInTransaction<T>(string storedProcedure, T paramaters)
		{
			_connection.Execute(storedProcedure, paramaters, 
				commandType: CommandType.StoredProcedure,
				transaction: _transaction);
		}

		public void CommitTransaction()
		{
			_transaction?.Commit();
			_connection.Close();
		}

		public void RollbackTransaction()
		{
			_transaction?.Rollback();
			_connection.Close();
		}

		public void Dispose()
		{
			// BUG - connection will already be closed when it comes to this
			CommitTransaction();
		}
	}
}
