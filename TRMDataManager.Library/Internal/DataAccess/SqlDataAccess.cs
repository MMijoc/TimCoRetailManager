using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.Library
{
	internal class SqlDataAccess
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
	}
}
