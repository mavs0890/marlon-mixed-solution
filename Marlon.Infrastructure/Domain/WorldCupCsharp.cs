using System;

namespace Marlon.Infrastructure.Domain
{
    public class WorldCupCsharp
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string HostCountry { get; set; }
        public string Winner { get; set; }
    }
}
