namespace Marlon.Domain.Types.Extensions

open System.Runtime.CompilerServices
open System

[<assembly:Extension>]
do()

[<Extension>]
type FSharpOption =
    [<Extension>]
    static member HasValue o = Option.isSome o

    [<Extension>]
    static member Case (o, some: Func<_,_>, none: Func<_>) =
        match o with
        | Some x -> some.Invoke x
        | _ -> none.Invoke()

    [<Extension>]
    static member Do (o, some: Action<_>, none: Action) =
        match o with
        | Some v -> some.Invoke v
        | _ -> none.Invoke()