namespace Marlon.Persistence

module Database =
    let p (key : string) (value : 'a) = (key, box value)


