﻿namespace Marlon.Domain.Types

type WorldCupFsharp = {
    Id : WorldCupId
    Year : Year
    HostCountry : Country
    Winner : Country
}


type WorldCupFsharpWithIsEuropeanWinner = {
    Id : WorldCupId
    Year : Year
    HostCountry : Country
    Winner : Country
    IsEuropeanWinner : bool option
}
