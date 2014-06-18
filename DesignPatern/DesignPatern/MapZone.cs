using System.Windows.Controls;

namespace MaraudersAdventure
{
    public class MapZone : Image
    {
        public ZoneAbstraite zone;

        public MapZone(ZoneAbstraite zone)
        {
            this.zone = zone;
            this.ToolTip =zone.point.X +" - "+ zone.point.Y;
        }
    }
}
