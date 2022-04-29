using System.Collections.Generic;

namespace TRMDataManager.Library.DataAccess
{
	public interface ISqlDataAccess
	{
		void CommitTransaction();
		void Dispose();
		string GetConnectionString(string name);
		List<T> LoadData<T, U>(string storedProcedure, U paramaters, string connectionStringName);
		List<T> LoadDataInTransaction<T, U>(string storedProcedure, U paramaters);
		void RollbackTransaction();
		void SaveData<T>(string storedProcedure, T paramaters, string connectionStringName);
		void SaveDataInTransaction<T>(string storedProcedure, T paramaters);
		void StartTransaction(string connectionStringName);
	}
}