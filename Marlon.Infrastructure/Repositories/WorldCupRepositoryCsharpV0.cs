using System;
using System.Collections.Generic;
using System.Linq;
using Marlon.Domain.Types;
using Marlon.Infrastructure.Domain;

namespace Marlon.Infrastructure.Repositories
{
    public interface IWorldCupRepositoryCSharpV0
    {
        void Save(WorldCupCsharp worldCup);
        WorldCupCsharp FindByYear(int year);
    }

    public class WorldCupRepositoryCSharpV0 : IWorldCupRepositoryCSharpV0
    {
        private readonly IPostgresConnection _postgresConnection;

        public WorldCupRepositoryCSharpV0(IPostgresConnection postgresConnection)
        {
            _postgresConnection = postgresConnection;
        }

        public void Save(WorldCupCsharp worldCup)
        {
            var saveStatement =
                @"
                insert into world_cup
                    (id, year, host_country, winner)
                values
                    (@id, @year, @host, @winner);";

            _postgresConnection.writeData(saveStatement,
                new
                {
                    worldCup.Id,
                    worldCup.Year,
                    worldCup.Host,
                    worldCup.Winner
                });

        }

        public WorldCupCsharp FindByYear(int year)
        {
            try
            {
                var readStatement =
                    @"
                select id, year, host_country as host, winner
                from world_cup
                where year = @year";
                return _postgresConnection.readData<WorldCupCsharp>(readStatement, new
                {
                    year
                }).Single();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
