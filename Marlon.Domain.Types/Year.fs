namespace Marlon.Domain.Types

type Year = private Year of int

module Year =
    let create unvalidatedYear =
        match unvalidatedYear with
        | uy when uy < 1930 || uy > 2018 -> failwith "Error"
        | _ -> Year unvalidatedYear

    let value (Year year) = year