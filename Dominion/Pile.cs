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
        public PictureBox pictureBox;

        public Pile()
        { }

        public Pile(Carte pCarte)
        {
            this.carte = pCarte;

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

            }
        }
    }
}
