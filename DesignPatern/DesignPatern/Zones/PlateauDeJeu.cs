
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaraudersAdventure
{
    public class PlateauDeJeu
    {
        public List<ZoneAbstraite> zones;
        public List<AccesAbstrait> acces;

        public PlateauDeJeu()
        {
            zones = new List<ZoneAbstraite>();
            acces = new List<AccesAbstrait>();
        }

        public void AjoutAcces(AccesAbstrait a)
        {
            acces.Add(a);
        }

        public void AjoutZone(ZoneAbstraite z)
        {
            zones.Add(z);
        }

        public List<ZoneAbstraite> GetNeighbourZones(Position p)
        {
            List<ZoneAbstraite> liste = new List<ZoneAbstraite>();
            foreach (AccesAbstrait a in acces)
            {
                if(a.AreNeighbour(p))
                {
                    if (a.z1.point.X == p.X && a.z1.point.Y == p.Y)
                        liste.Add(a.z2);
                    if (a.z2.point.X == p.X && a.z2.point.Y == p.Y)
                        liste.Add(a.z1);
                }
            }
            return liste;
        }

        public ZoneAbstraite GetZone(Position p)
        {
            foreach (ZoneAbstraite z in zones)
            {
                if (z.point.X == p.X && z.point.Y == p.Y)
                    return z;       
            }
            return null;
        }
    }
}
