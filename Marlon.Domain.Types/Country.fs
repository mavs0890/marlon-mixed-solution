namespace Marlon.Domain.Types

type Country =  Country of string

module Country =
    let value (Country country) = country

