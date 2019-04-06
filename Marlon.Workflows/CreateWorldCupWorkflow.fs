namespace Marlon.Workflows

open Marlon.Domain.Types
open System

type CreateWorldCupError =
| AlreadyExists
| Other

module CreateWorldCup =
    let create 
        (generateId : unit -> WorldCupId)
        (findWorldCup : Year -> WorldCupFsharp option)
        (saveWorldCup : WorldCupFsharp -> unit)
        year
        host
        winner
        : Result<unit, CreateWorldCupError> =
        Ok ()


type ICreateWorldCupWorkflow =
    abstract member Execute : year : Year -> host : Country -> winner : Country -> Result<unit, CreateWorldCupError>

type CreateWorldCupWorkflow
    (
        generateId : Func<_>,
        findWorldCup : Func<_, _>,
        saveWorldCup : Action<_>
    ) =
    interface ICreateWorldCupWorkflow with
        member __.Execute year host winner =
            CreateWorldCup.create
                (FuncConvert.FromFunc generateId)
                (FuncConvert.FromFunc findWorldCup)
                (FuncConvert.FromAction saveWorldCup)
                year
                host
                winner