using Dapper;
using EasyOR.DTO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
        public int UpdatePlayer(Player player)
        {
            string sql = "UPDATE [dbo].[PLAYER] SET [Name] = @Name, [IsAFK] = @IsAFK, [IsVacation] = @IsVacation, [IsQuestPlayer] = @IsQuestPlayer WHERE PlayerId = @PlayerId";
            return _db.Execute(sql, player);
        }
        public Player GetPlayerById(int playerId)
        {
            string sql = @"SELECT * FROM PLAYER p WHERE p.PlayerId = @PlayerId";
            return _db.Query<Player>(sql, new { PlayerId = playerId }).FirstOrDefault();
        }

        public IEnumerable<Player> GetPlayerWithoutName()
        {
            var lookup = new Dictionary<int, Player>();
            string sql = @"Select pl.PlayerId, pl.Name,pl.IsAFK,pl.IsVacation,pl.InternalIdOR,pl.IsQuestPlayer," +
                        @"p.PlanetId, p.Name,p.Galaxy,p.System,p.Position,p.PlayerId " +
                        @"from PLAYER pl INNER JOIN PLANET p on p.PlayerId = pl.PlayerId WHERE pl.NAME = '' OR pl.NAME is NULL";
            _db.Query<Player, Planet, Player>(sql, (pl, p) =>
           {
               if (!lookup.TryGetValue(pl.PlayerId, out Player player))
               {
                   lookup.Add(pl.PlayerId, player = pl);
               }
               if (player.Planets == null)
               {
                   player.Planets = new List<Planet>();
               }
               player.Planets.Add(p);
               return player;
           }, null, null, true, "PlanetId");
            return lookup.Values;
        }


        public IEnumerable<Player> GetPlayerQuestWithoutName()
        {
            var lookup = new Dictionary<int, Player>();
            string sql = @"Select pl.PlayerId, pl.Name,pl.IsAFK,pl.IsVacation,pl.InternalIdOR,pl.IsQuestPlayer," +
                        @"p.PlanetId, p.Name,p.Galaxy,p.System,p.Position,p.PlayerId " +
                        @"from PLAYER pl INNER JOIN PLANET p on p.PlayerId = pl.PlayerId WHERE pl.NAME is NULL AND pl.IsQuestPlayer = 1 order by pl.PlayerId;";
            _db.Query<Player, Planet, Player>(sql, (pl, p) =>
            {
                if (!lookup.TryGetValue(pl.PlayerId, out Player player))
                {
                    lookup.Add(pl.PlayerId, player = pl);
                }
                if (player.Planets == null)
                {
                    player.Planets = new List<Planet>();
                }
                player.Planets.Add(p);
                return player;
            }, null, null, true, "PlanetId");
            return lookup.Values;
        }
    }
}
