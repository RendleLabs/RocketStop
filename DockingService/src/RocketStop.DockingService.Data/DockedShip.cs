using System;

namespace RocketStop.DockingService.Data
{
    public class DockedShip
    {
        public int DockedShipId { get; set; }
        public int ShipId { get; set; }
        public Ship Ship { get; set; }
        public int BayId { get; set; }
        public Bay Bay { get; set; }
        public DateTimeOffset DockedAt { get; set; }
        public int ExpectedHours { get; set; }
    }
}
