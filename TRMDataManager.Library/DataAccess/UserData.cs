using System.Collections.Generic;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
	public class UserData : IUserData
	{
		private readonly ISqlDataAccess _sql;

		public UserData(ISqlDataAccess sql)
		{
			_sql = sql;
		}
		public List<UserModel> GetUserById(string id)
		{
			var output = _sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", new { Id = id }, "TRMData");

			return output;
		}

		public void CreateUser(UserModel user)
		{
			_sql.SaveData("dbo.spUser_Insert", new { user.Id, user.FirstName, user.LastName, user.EmailAddress }, "TRMData");
		}
	}
}

