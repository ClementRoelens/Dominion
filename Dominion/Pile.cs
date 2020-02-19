using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dominion
{
    public class Pile
    {
        public Carte carte;
        public int nombre = 15;

        public Pile()
        { }

        public Pile(Carte Carte)
        {
            //Le constructeur de la pile sert à indiquer de quelle Cartes est composée la pile
            this.carte = Carte;
            //Mais également le nombre de ces cartes
            switch (this.carte.Nom)
            {
                case "Cuivre":
                    this.nombre = 50;
                    break;
                case "Argent":
                    this.nombre = 40;
                    break;
                case "Or":
                    this.nombre = 30;
                    break;
                case "Platine":
                    this.nombre = 12;
                    break;
                case "Domaine":
                case "Duché":
                case "Province":
                case "Colonie":
                    this.nombre = (LancementForm.ListeJoueurs.Count > 2) ? 12 : 8;
                    break;
                case "Malédiction":
                    switch (LancementForm.ListeJoueurs.Count)
                    {
                        case 2:
                            this.nombre = 10;
                            break;
                        case 3:
                            this.nombre = 20;
                            break;
                        case 4:
                            this.nombre = 30;
                            break;
                    }
                    break;
                default:
                    this.nombre = 10;
                    break;
            }
        }
    }
}
