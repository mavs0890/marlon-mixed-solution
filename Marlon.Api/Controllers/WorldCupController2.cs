using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Marlon.Domain.Types;
using Marlon.Domain.Types.Extensions;
using Marlon.Infrastructure;
using Marlon.Infrastructure.Domain;
using Marlon.Infrastructure.Repositories;
using Marlon.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using Newtonsoft.Json;

namespace Marlon.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WorldCupController2 : Controller
    {

        // 2 - $$ - F# Module

        private readonly IPostgresConnection _postgresConnection;

        public WorldCupController2(IPostgresConnection postgresConnection)
        {
            _postgresConnection = postgresConnection;
        }

        [HttpGet("{year}")]
        public IActionResult Index(int year)
        {
            Func<string, IDictionary<string, object>, FSharpList<WorldCupFsharpRepositoryModule.WorldCupDto>>
                readDataFunc = (s, p) =>
                {
                    var list = _postgresConnection.readData<WorldCupFsharpRepositoryModule.WorldCupDto>(s,
                        p);
                    return ListModule.OfSeq(list);
                };

            var readData = FuncConvert.FromFunc(readDataFunc);

            var worldCupFsharpOption = WorldCupFsharpRepositoryModule.findByYear(readData, YearModule.create(year));

            if (worldCupFsharpOption.HasValue())
            {
                var worldCupFsharp = worldCupFsharpOption.Value;

                var worldCupVm = new WorldCupVm
                {
                    Year = YearModule.value(worldCupFsharp.Year),
                    Host = WorldCupHostModule.value(worldCupFsharp.Host),
                    Winner = CountryModule.value(worldCupFsharp.Winner)
                };

                return Content(JsonConvert.SerializeObject(worldCupVm), "application/json");
            }

            return NotFound();

        }

        [HttpPost]
        public void Post([FromBody] WorldCupVm wc)
        {
            var worldCupFsharpToSave = new WorldCupFsharp(
                WorldCupId.NewWorldCupId(Guid.NewGuid()),
                YearModule.create(wc.Year),
                WorldCupHostModule.create(wc.Host),
                Country.NewCountry(wc.Winner)
                );

            Action<string, IDictionary<string, object>> writeData = _postgresConnection.writeData;
            var fSharpWriteData = FuncConvert.FromAction(writeData);


            WorldCupFsharpRepositoryModule.save(fSharpWriteData, worldCupFsharpToSave);
        }

        // end - 2 - $$
    }
}
