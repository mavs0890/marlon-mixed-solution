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
    public class WorldCupController1 : Controller
    {

        // 1 - $$ - F# Types

        private readonly IWorldCupRepositoryCSharpV1 _worldCupRepositoryCSharpV1;

        public WorldCupController1(IWorldCupRepositoryCSharpV1 worldCupRepositoryCSharpV1)
        {
            _worldCupRepositoryCSharpV1 = worldCupRepositoryCSharpV1;
        }

        [HttpGet("{year}")]
        public IActionResult Index(int year)
        {
            var worldCupFSharp = _worldCupRepositoryCSharpV1.FindByYear(year);

            if (worldCupFSharp == null)
            {
                return NotFound();
            }

            var worldCupVm = new WorldCupVm
            {
                Year = YearModule.value(worldCupFSharp.Year),
                Host = WorldCupHostModule.value(worldCupFSharp.Host),
                Winner = CountryModule.value(worldCupFSharp.Winner)
            };
            return Content(JsonConvert.SerializeObject(worldCupVm), "application/json");
        }

        [HttpPost]
        public void Post([FromBody] WorldCupVm wc)
        {
            var worldCupCsharp = new WorldCupFsharp(
                WorldCupId.NewWorldCupId(Guid.NewGuid()),
                YearModule.create(wc.Year),
                WorldCupHostModule.create(wc.Host),
                Country.NewCountry(wc.Winner)
                );

            _worldCupRepositoryCSharpV1.Save(worldCupCsharp);
        }

        // end - 1 - $$
    }
}
