using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
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
	}
}

