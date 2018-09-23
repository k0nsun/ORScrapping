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
    public class PlanetAction
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["ORDEV"].ConnectionString);

        public Planet GetByCoordinatesAndUserInternalID(short galaxy, short system, short position, int internalIdOR)
        {
            string sql = @"SELECT p.* from PLANET p" +
                        @" inner join PLAYER pl on p.PlayerId = pl.PlayerId" +
                        @" WHERE p.Galaxy = @Galaxy AND p.System = @System AND p.Position = @Position AND pl.InternalIdOR = @InternalIdOR";

            return _db.Query<Planet>(sql, new { @Galaxy = galaxy, @System = system, @Position = position, @InternalIdOR = internalIdOR }).FirstOrDefault();
        }

        public int AddPlanet(Planet planet)
        {
            string sql = @"INSERT INTO [dbo].[PLANET]([Galaxy],[System],[Position],[PlayerId], [Name]) VALUES (@Galaxy, @System, @Position, @PlayerId, @Name)";
            return _db.Execute(sql, planet);
        }
    }
}
