﻿using System;
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
    public class WorldCupController : Controller
    {
        //0 $$
        private readonly IWorldCupRepositoryCSharpV0 _worldCupRepositoryCSharp;

        public WorldCupController(IWorldCupRepositoryCSharpV0 worldCupRepositoryCSharp)
        {
            _worldCupRepositoryCSharp = worldCupRepositoryCSharp;
        }

        [HttpGet("{year}")]
        public IActionResult Index(int year)
        {
            var worldCupCSharp = _worldCupRepositoryCSharp.FindByYear(year);

            if (worldCupCSharp == null)
            {
                return NotFound();
            }

            var worldCupVm = new WorldCupVm
            {
                Year = worldCupCSharp.Year,
                HostCountry = worldCupCSharp.HostCountry,
                Winner = worldCupCSharp.Winner
            };
            return Content(JsonConvert.SerializeObject(worldCupVm), "application/json");
        }

        [HttpPost]
        public void Post([FromBody] WorldCupVm wc)
        {
            var worldCupCSharp = new WorldCupCsharp
            {
                Id = Guid.NewGuid(),
                Year = wc.Year,
                HostCountry = wc.HostCountry,
                Winner = wc.Winner
            };

            _worldCupRepositoryCSharp.Save(worldCupCSharp);
        }

        // end - 0 - $$
























        // 1 - $$ - F# Types

        //private readonly IWorldCupRepositoryCSharpWithFsharpTypesV1 _worldCupRepositoryCSharpWithFsharpTypesV1;

        //public WorldCupController(IWorldCupRepositoryCSharpWithFsharpTypesV1 worldCupRepositoryCSharpWithFsharpTypesV1)
        //{
        //    _worldCupRepositoryCSharpWithFsharpTypesV1 = worldCupRepositoryCSharpWithFsharpTypesV1;
        //}

        //[HttpGet("{year}")]
        //public IActionResult Index(int year)
        //{
        //    var worldCupCSharp = _worldCupRepositoryCSharpWithFsharpTypesV1.FindByYear(year);

        //    if (worldCupCSharp == null)
        //    {
        //        return NotFound();
        //    }

        //    var worldCupVm = new WorldCupVm
        //    {
        //        Year = YearModule.value(worldCupCSharp.Year),
        //        HostCountry = CountryModule.value(worldCupCSharp.HostCountry),
        //        Winner = CountryModule.value(worldCupCSharp.Winner)
        //    };
        //    return Content(JsonConvert.SerializeObject(worldCupVm), "application/json");
        //}

        //[HttpPost]
        //public void Post([FromBody] WorldCupVm wc)
        //{
        //    var worldCupCsharp = new WorldCupFsharp(
        //        WorldCupId.NewWorldCupId(Guid.NewGuid()),
        //        YearModule.create(wc.Year),
        //        Country.NewCountry(wc.HostCountry),
        //        Country.NewCountry(wc.Winner),
        //        );

        //    _worldCupRepositoryCSharpWithFsharpTypesV1.Save(worldCupCsharp);
        //}

        // end - 1 - $$

























        // 2 - $$ - F# Module

        //private readonly IPostgresConnection _postgresConnection;

        //public WorldCupController(IPostgresConnection postgresConnection)
        //{
        //    _postgresConnection = postgresConnection;
        //}

        //[HttpGet("{year}")]
        //public IActionResult Index(int year)
        //{
        //    Func<string, IDictionary<string, object>, FSharpList<WorldCupFsharpRepositoryModule.WorldCupDto>>
        //        readDataFunc =
        //            delegate(string statement, IDictionary<string, object> parameters)
        //            {
        //                var list = _postgresConnection.readData<WorldCupFsharpRepositoryModule.WorldCupDto>(statement,
        //                    parameters);
        //                return ListModule.OfSeq(list);
        //            };

        //    var readData = FuncConvert.FromFunc(readDataFunc);

        //    var worldCupFsharpOption = WorldCupFsharpRepositoryModule.findByYear(readData, YearModule.create(year));

        //    if (worldCupFsharpOption.HasValue())
        //    {
        //        var worldCupFsharp = worldCupFsharpOption.Value;

        //        var worldCupVm = new WorldCupVm
        //        {
        //            Year = YearModule.value(worldCupFsharp.Year),
        //            HostCountry = CountryModule.value(worldCupFsharp.HostCountry),
        //            Winner = CountryModule.value(worldCupFsharp.Winner)
        //        };

        //        return Content(JsonConvert.SerializeObject(worldCupVm), "application/json");
        //    }

        //    return NotFound();

        //}

        //[HttpPost]
        //public void Post([FromBody] WorldCupVm wc)
        //{
        //    var worldCupFsharpToSave = new WorldCupFsharp(
        //        WorldCupId.NewWorldCupId(Guid.NewGuid()),
        //        YearModule.create(wc.Year),
        //        Country.NewCountry(wc.HostCountry),
        //        Country.NewCountry(wc.Winner)
        //        );

        //    Action<string, IDictionary<string, object>> writeData = _postgresConnection.writeData;
        //    var fSharpWriteData = FuncConvert.FromAction(writeData);


        //    WorldCupFsharpRepositoryModule.save(fSharpWriteData, worldCupFsharpToSave);
        //}

        // end - 2 - $$












        // 3 - $$ - Wrapper Class
        //private readonly IWorldCupRepository _worldCupRepository;


        //public WorldCupController(IWorldCupRepository worldCupRepository)
        //{
        //    _worldCupRepository = worldCupRepository;
        //}

        //[HttpGet("{year}")]
        //public IActionResult Index(int year)
        //{
        //    var worldCupFsharpOption = _worldCupRepository.FindByYear(YearModule.create(year));

        //    if (worldCupFsharpOption.HasValue()) //defined by me
        //    {
        //        var worldCupFsharp = worldCupFsharpOption.Value;

        //        var worldCupVm = new WorldCupVm
        //        {
        //            Year = YearModule.value(worldCupFsharp.Year),
        //            HostCountry = CountryModule.value(worldCupFsharp.HostCountry),
        //            Winner = CountryModule.value(worldCupFsharp.Winner)
        //        };

        //        return Content(JsonConvert.SerializeObject(worldCupVm), "application/json");
        //    }

        //    return NotFound();

        //}

        //[HttpPost]
        //public void Post([FromBody] WorldCupVm wc)
        //{
        //    var worldCupFsharpToSave = new WorldCupFsharp(
        //        WorldCupId.NewWorldCupId(Guid.NewGuid()),
        //        YearModule.create(wc.Year),
        //        Country.NewCountry(wc.HostCountry),
        //        Country.NewCountry(wc.Winner)
        //        );

        //    _worldCupRepository.Save(worldCupFsharpToSave);
        //}

        // end - 3 - $$



        // 4 - $$ - Option extensions
        //private readonly IWorldCupRepository _worldCupRepository;

        //public WorldCupController(IWorldCupRepository worldCupRepository)
        //{
        //    _worldCupRepository = worldCupRepository;
        //}

        //[HttpGet("{year}")]
        //public IActionResult Index(int year)
        //{
        //    var worldCupFsharpOption = _worldCupRepository.FindByYear(YearModule.create(year));

        //    return worldCupFsharpOption.Case<WorldCupFsharp, IActionResult>(
        //        some: worldCup =>
        //        {
        //            var worldCupVm = new WorldCupVm
        //            {
        //                Year = YearModule.value(worldCup.Year),
        //                HostCountry = CountryModule.value(worldCup.HostCountry),
        //                Winner = CountryModule.value(worldCup.Winner)
        //            };
        //            return Content(JsonConvert.SerializeObject(worldCupVm), "application/json");
        //        },
        //        none: () => NotFound());

        //}

        //[HttpPost]
        //public void Post([FromBody] WorldCupVm wc)
        //{
        //    var worldCupFsharpToSave = new WorldCupFsharp(
        //        WorldCupId.NewWorldCupId(Guid.NewGuid()),
        //        YearModule.create(wc.Year),
        //        Country.NewCountry(wc.HostCountry),
        //        Country.NewCountry(wc.Winner)
        //    );

        //    _worldCupRepository.Save(worldCupFsharpToSave);
        //}

        // end - 4 - $$

    }

    public class WorldCupVm
    {
        public int Year { get; set; }
        public string HostCountry { get; set; }
        public string Winner { get; set; }
    }
}
