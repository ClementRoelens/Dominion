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
        public int nombre;

        public Pile()
        { }

        public Pile(Carte Carte)
        {
            this.carte = Carte;

            switch (this.carte.Type)
            {
                case "Action":
                    this.nombre = 10;
                    break;
                case "Victoire":
                    this.nombre = 15;
                    break;
                case "Trésor":
                    this.nombre = 50;
                    break;
                case "Malédiction":
                    this.nombre = 15;
                    break;
            }
        }
    }
}
