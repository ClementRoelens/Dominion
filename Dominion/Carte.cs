using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominion
{
    public class Carte
    {
        int id;
        public string Nom;
        public string Image;
        public int Cout;
        public string Type;
        public string EffetText;
        public int MonnaieDonnee;
        public int CarteDonnee;
        public int ActionDonnee;
        public int AchatDonne;
        public int JetonPointDonne;
        public int PointDonne;
        Effet Effet;

        public Carte() { }

        public Carte
            (int pId , string pNom, string pImage , int pCout , string pType, string pEffetText, int pMonnaieDonnee 
            , int pCarteDonnee, int pActionDonnee, int pAchatDonne, int pJetonPointDonne, int pPointDonne )
        {
            this.id = pId;
            this.Nom = pNom;
            this.Image = pImage;
            this.Cout = pCout;
            this.Type = pType;
            this.EffetText = pEffetText;
            this.MonnaieDonnee = pMonnaieDonnee;
            this.CarteDonnee = pCarteDonnee;
            this.ActionDonnee = pActionDonnee;
            this.AchatDonne = pAchatDonne;
            this.JetonPointDonne = pJetonPointDonne;
            this.PointDonne = pPointDonne;
        }

        

    }
}
