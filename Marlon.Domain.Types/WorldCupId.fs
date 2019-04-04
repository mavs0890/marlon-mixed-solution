namespace Marlon.Domain.Types

open System

type WorldCupId = WorldCupId of Guid

module WorldCupId =
    let value (WorldCupId worldCupId) = worldCupId