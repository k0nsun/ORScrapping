using Dapper;
using EasyOR.DTO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EasyOR.DataAccess.SqlServer
{
    public class QuestAction
    {

        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["ORDEV"].ConnectionString);
        public List<Quest> GetAllQuest()
        {
            return _db.Query<Quest>(@"Select q.* FROM Quest q").AsList();
        }

        public int AddQuest(Quest quest)
        {
            string sql = @"INSERT INTO [dbo].[QUEST] ([Name],[RewardTypeId],[ProfitId] ,[ProfitTypeId],[ValueProfit],[DurationProfil],[HasSoldier],[HasSpaceship],[HasExploration],[HasDefense],[Comment],[Visible],[ProfilOldDatabase],[PlayerId],[IsCheckName]) " +
                        @"VALUES (@Name,@RewardTypeId,@ProfitId,@ProfitTypeId,@ValueProfit,@DurationProfil,@HasSoldier,@HasSpaceship,@HasExploration,@HasDefense,@Comment,@Visible,@ProfilOldDatabase,@PlayerId,@IsCheckName)";
            return _db.Execute(sql, quest);
        }

        public IEnumerable<Quest> GetQuestByName(string name)
        {
            string sql = "SELECT * FROM [dbo].[QUEST] WHERE [Name] = @Name";
            return _db.Query<Quest>(sql, new { Name = name });
        }
    }
}
