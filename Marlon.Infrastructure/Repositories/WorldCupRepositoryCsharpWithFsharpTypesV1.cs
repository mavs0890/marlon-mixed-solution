using System;
using System.Collections.Generic;
using System.Linq;
using Marlon.Domain.Types;
using Marlon.Infrastructure.Domain;

namespace Marlon.Infrastructure.Repositories
{
    public interface IWorldCupRepositoryCSharpWithFsharpTypesV1
    {
        void Save(WorldCupFsharp worldCup);
        WorldCupFsharp FindByYear(int year);
    }

    public class WorldCupRepositoryCSharpWithFsharpTypesV1 : IWorldCupRepositoryCSharpWithFsharpTypesV1
    {
        private readonly IPostgresConnection _postgresConnection;

        public WorldCupRepositoryCSharpWithFsharpTypesV1(IPostgresConnection postgresConnection)
        {
            _postgresConnection = postgresConnection;
        }

        public void Save(WorldCupFsharp worldCup)
        {
            var saveStatement =
                @"
                insert into world_cup
                    (id, year, host_country, winner)
                values
                    (@id, @year, @hostCountry, @winner);";

            _postgresConnection.writeData(saveStatement,
                new
                {
                    WorldCupId = WorldCupIdModule.value(worldCup.Id),
                    Year = YearModule.value(worldCup.Year),
                    HostCountry = CountryModule.value(worldCup.HostCountry),
                    Winner = CountryModule.value(worldCup.Winner)
                });

        }

        public class WorldCupFsharpDto
        {
            public Guid Id { get; set; }
            public int Year { get; set; }
            public string HostCountry { get; set; }
            public string Winner { get; set; }
        }

        public WorldCupFsharp FindByYear(int year)
        {
            try
            {
                var readStatement =
                    @"
                select id, year, host_country as hostCountry, winner
                from world_cup
                where year = @year";
                var result =  _postgresConnection.readData<WorldCupFsharpDto>(readStatement, new
                {
                    year
                }).Single();

                return new WorldCupFsharp(
                    WorldCupId.NewWorldCupId(result.Id),
                    YearModule.create(result.Year),
                    Country.NewCountry(result.HostCountry),
                    Country.NewCountry(result.Winner)
                    );
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
