using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marlon.Domain.Types;
using Marlon.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FSharp.Core;
using Newtonsoft.Json;

namespace Marlon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldCupController3 : Controller
    {
        // 3 - $$ - Wrapper Class
        private readonly IWorldCupRepository _worldCupRepository;


        public WorldCupController3(IWorldCupRepository worldCupRepository)
        {
            _worldCupRepository = worldCupRepository;
        }

        [HttpGet("{year}")]
        public IActionResult Index(int year)
        {
            var worldCupFsharpOption = _worldCupRepository.FindByYear(YearModule.create(year));

            if (OptionModule.IsSome(worldCupFsharpOption)) //you'll get access to Option, List, etc module options as well
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

            _worldCupRepository.Save(worldCupFsharpToSave);
        }

        // end - 3 - $$
    }
}
