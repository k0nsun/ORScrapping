using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EasyOR.DataAccess.SqlServer
{
    public class Quest
    {

        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDapper"].ConnectionString);
        public List<Quest> GetAllQuest()
        {
           return _db.Query<Quest>(@"Select q.* FROM Quest q").AsList();
        }

  
    }
}
