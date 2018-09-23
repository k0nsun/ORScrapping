using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EasyOR.DataAccess.SqlServer
{
    public class QuestAction
    {

        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDapper"].ConnectionString);
        public List<QuestAction> GetAllQuest()
        {
           return _db.Query<QuestAction>(@"Select q.* FROM Quest q").AsList();
        }  
    }
}
