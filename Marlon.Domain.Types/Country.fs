namespace Marlon.Domain.Types



type Country = Country of string

module Country = 
    let value (Country country) = country





type WorldCupHost = 
| Host of Country
| CoHosted of Country * Country

module WorldCupHost =
    let create (strHost : string) =
        match strHost.Contains "&" with
        | true ->
            let strList = strHost.Split '&'
            CoHosted (Country strList.[0] , Country strList.[1])
        | false ->
            Host (Country strHost)


    let value wcHost =
        match wcHost with 
        | Host country -> country |> Country.value
        | CoHosted (country, cohostCountry) -> 
            sprintf "%s & %s" (country |> Country.value) (cohostCountry |> Country.value)

