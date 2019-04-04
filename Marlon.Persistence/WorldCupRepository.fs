namespace Marlon.Persistence

open System
open Marlon.Domain.Types
open Marlon.Domain.Types.Extensions
open Database
open System.Collections
open Marlon.Infrastructure


//2 - $$ F# module 

module WorldCupFsharpRepositoryModule =
    [<CLIMutable>]
    type WorldCupDto = {
        Id : Guid
        Year : int
        HostCountry : string
        Mascot : string
        Winner : string
        SecondPlace : string
        ThirdPlace : string
        TopScorer : string
    }

    let toDomain (dto : WorldCupDto) : WorldCupFsharp =
        {
            Id = dto.Id |> WorldCupId
            Year = dto.Year |> Year.create
            HostCountry = dto.HostCountry |> Country
            Winner = dto.Winner |> Country
        }

    let save writeData (worldCup : WorldCupFsharp) : unit =
        let query = """
            insert into world_cup
            (id, year, host_country, winner)
            values
            (@id, @year, @hostCountry, @winner);
        """
        writeData
            query
            ([
                p "id" (worldCup.Id |> WorldCupId.value)
                p "year" (worldCup.Year |> Year.value)
                p "hostCountry" (worldCup.HostCountry |> Country.value)
                p "winner" (worldCup.Winner |> Country.value)
            ] |> dict)

    
        
    let findByYear readData year =
        let query = """
            select id, year, host_country as hostCountry, winner
            from world_cup
            where year = @year
        """
        readData
            query
            ([ 
                p "year" (year |> Year.value)
            ] |> dict)
        |> List.ofSeq
        |> List.singleOrNone
        |> Option.map toDomain

// end - 2 - $$








// 3 - $$ - Wrapper class
//type IWorldCupRepository =
//    abstract member Save : WorldCupFsharp -> unit
//    abstract member FindByYear : Year -> WorldCupFsharp option

//type WorldCupRepository (connection : IPostgresConnection) =
//    let writeData = FuncConvert.FuncFromTupled connection.writeData
//    let readData = FuncConvert.FuncFromTupled connection.readData<WorldCupFsharpRepositoryModule.WorldCupDto>
//    interface IWorldCupRepository with
//        member __.Save worldCup =
//            WorldCupFsharpRepositoryModule.save
//                writeData
//                worldCup
//        member __.FindByYear year = 
//            WorldCupFsharpRepositoryModule.findByYear
//                readData
//                year

// end - 3 - $$