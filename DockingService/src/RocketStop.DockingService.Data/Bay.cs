using Microsoft.Extensions.Logging;

namespace RocketStop.DockingService.Data
{
    public class Bay
    {
        public int BayId { get; set; }
        public string Section { get; set; }
        public int Number { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
        public Docking Docking { get; set; }
    }
}
