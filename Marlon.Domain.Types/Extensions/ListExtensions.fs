namespace Marlon.Domain.Types.Extensions

module List =
    let singleOrNone (list : 'a list) =
        match list.Length with
        | l when l = 0 -> None
        | l when l = 1 -> list |> List.exactlyOne |> Some
        | _ -> failwith "More than one item"

