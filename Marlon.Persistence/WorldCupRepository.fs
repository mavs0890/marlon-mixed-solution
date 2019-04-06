namespace Marlon.Persistence

open System
open Marlon.Domain.Types
open Marlon.Domain.Types.Extensions
open Database
open System.Collections
open Marlon.Infrastructure


//2 - $$ F# module 

module WorldCupFsharpRepositoryModule =
    //talk about CLI mutable
    //needed for JSON serialization and dapper
    //mapping a nullable to an option type? - important because of working with library like dapper that was build for C#
    [<CLIMutable>]
    type WorldCupDto = {
        Id : Guid
        Year : int
        HostCountry : string
        Winner : string
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

    
        
    let findByYear readData (Year year) =
        let query = """
            select id, year, host_country as hostCountry, winner
            from world_cup
            where year = @year
        """
        readData
            query
            ([ 
                p "year" year
            ] |> dict)
        |> List.ofSeq
        |> List.singleOrNone
        |> Option.map toDomain

// end - 2 - $$







// its usually better to write that bridge code in F# than it is in F#
// Classes are more friendly to C# than functions and modules
// 3 - $$ - Wrapper class

type IWorldCupRepository =
    abstract member Save : WorldCupFsharp -> unit
    abstract member FindByYear : Year -> WorldCupFsharp option

type WorldCupRepository (connection : IPostgresConnection) =
    let writeData = FuncConvert.FuncFromTupled connection.writeData
    let readData = FuncConvert.FuncFromTupled connection.readData<WorldCupFsharpRepositoryModule.WorldCupDto>
    interface IWorldCupRepository with
        member __.Save worldCup =
            WorldCupFsharpRepositoryModule.save
                writeData
                worldCup
        member __.FindByYear year = 
            WorldCupFsharpRepositoryModule.findByYear
                readData
                year

// end - 3 - $$