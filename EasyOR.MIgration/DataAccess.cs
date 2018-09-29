using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EasyOR.MIgration.DTOOld;

namespace EasyOR.MIgration
{
    public class DataAccess
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["ORDEV2"].ConnectionString);
        public IEnumerable<QueteDetails> GetAllQuestDetails()
        {
            string sql = @"SELECT p.* from QuetesDetails p";                     

            return _db.Query<QueteDetails>(sql);
        }

        public Quete GetQuestById(int queteId)
        {
            string sql = @"SELECT p.* from Quetes p where p.ID = @QuestId";

            return _db.Query<Quete>(sql, new { QuestId = queteId }).FirstOrDefault();
        }
    }
}
