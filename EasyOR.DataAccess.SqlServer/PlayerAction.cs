using Dapper;
using EasyOR.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOR.DataAccess.SqlServer
{
    public class PlayerAction
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["ORDEV"].ConnectionString);

        public Player GetUserByInternalIdOR(int internalIdOR)
        {
            return _db.Query<Player>(@"Select p.* FROM PLAYER p WHERE InternalIdOR = @InternalIdOR", new { InternalIdOR = internalIdOR }).FirstOrDefault();
        }

        public int AddPlayer(Player player)
        {
            string sql = @"INSERT INTO [dbo].[PLAYER] ([Name],[IsAFK],[IsVacation] ,[InternalIdOR]) VALUES (@Name, @IsAFK, @IsVacation, @InternalIdOR)";
            return _db.Execute(sql, player);
        }

    }
}
