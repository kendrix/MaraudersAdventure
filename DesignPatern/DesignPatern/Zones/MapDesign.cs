using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MaraudersAdventure.Zones.Etage
{
    public class MapDesign
    {
         Bitmap inconnu = Properties.Resources.saule_cogneur;
         Bitmap standard = Properties.Resources.sol;
         Bitmap obstacle = Properties.Resources.cailloux;
         Bitmap objet = Properties.Resources.bourse;
         Bitmap cible = Properties.Resources.cible;
        
        public MapDesign(System.Drawing.Bitmap _inconnu, System.Drawing.Bitmap  _herbe, System.Drawing.Bitmap  _arbre, System.Drawing.Bitmap  _objet)
        {
            inconnu = _inconnu;
            standard = _herbe;
            obstacle = _arbre;
            objet = _objet;
        }

        public MapZone GetCaseImage(ZoneAbstraite z, List<Personnage> liste, List<Quete>quete)
        {
            if (z != null)
            {
                MapZone map = new MapZone(z);

                BitmapSource bt = null;
                
                foreach (Personnage p in liste)
                {
                    if (z != null && p != null && p.Position != null && p.Position.X == z.point.X
                        && p.Position.Y == z.point.Y)
                    {
                        bt = ToWpfBitmap(p.Image);
                        map.ToolTip += " " + p.Nom;
                    }                    
                }
                if (bt == null)
                {

                    foreach (Quete q in quete)
                    {
                        if (q.Type == TypeQuete.TrouverCase)
                        {
                            QueteZone qz = (QueteZone)q;
                            if (qz != null && z != null
                                && qz.ZoneATrouver.X == z.point.X
                                && qz.ZoneATrouver.Y == z.point.Y)
                                bt = ToWpfBitmap(cible);
                        }
                    }

                    if (bt == null && (z.objets != null && z.objets.Count > 0))
                    {
                        bt = ToWpfBitmap(objet);

                        foreach(Objet o in z.objets)
                            if (o.monType == monTypeObjet.Portoloin)
                                bt = ToWpfBitmap(inconnu);

                    }
                    else if (bt == null && z.Walkable)
                        bt = ToWpfBitmap(standard);
                    else if (bt == null)
                        bt = ToWpfBitmap(obstacle);
                }
                map.Source = bt;
                return map;
            }
            return null;
        }

        private   BitmapSource ToWpfBitmap(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}
