
namespace MaraudersAdventure
{
    public enum monTypeObjet
    {
        Aliment,
        ObjetDeQuete,
        Portoloin
    }
    public abstract  class Objet
    {
        public Position point;
        public string Nom;
        public monTypeObjet monType;

        public Objet(monTypeObjet type)
        {
            monType = type;
        }
        public abstract bool Utilisation(Personnage p, Equipe e);

    }
}
