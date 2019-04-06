namespace Marlon.Domain.Types

type Year = Year of int

module Year =

    //use a result
    let create unvalidatedYear =
        match unvalidatedYear with
        | uy when uy < 1930 || uy > 2018 -> failwith "Error"
        | _ -> Year unvalidatedYear

    let value (Year year) = year