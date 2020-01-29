using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public bool EnJeu = false;

        public Carte() { }

        public Carte
            (int pId, string pNom, string pImage, int pCout, string pType, string pEffetText, int pMonnaieDonnee
            , int pCarteDonnee, int pActionDonnee, int pAchatDonne, int pJetonPointDonne, int pPointDonne)
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

        public void Effet()
        {
            //On code les effets

            switch (this.Nom)
            {
                case "Mascarade":

                    {
                        Carte carteEcartee = new Carte();
                        List<Joueur> listeJoueurs = LancementForm.listeJoueurs;
                        for (int i = 0, c = listeJoueurs.Count; i < c; i++)
                        {
                            //On utilise la méthode pour choisir une carte dans la main
                            carteEcartee = listeJoueurs[i].ChoisirUneCarte();
                            //Puis on écarte
                            listeJoueurs[i].Ecarter(carteEcartee);
                            //Et on passe soit au joueur suivant, soit au premier si ce joueur est le dernier
                            if (listeJoueurs[i] == listeJoueurs[listeJoueurs.Count - 1])
                            { listeJoueurs[0].Main.Add(carteEcartee); }
                            else
                            { listeJoueurs[i + 1].Main.Add(carteEcartee); }
                        }
                        //Puis le deuxième effet
                        carteEcartee = PartieForm.JoueurActuel.ChoisirUneCarte();
                        PartieForm.JoueurActuel.Ecarter(carteEcartee);
                    }
                    break;

                case "Evèque":
                    {
                        Carte carteEcartee = new Carte();
                        Joueur JoueurActuel = PartieForm.JoueurActuel;
                        List<Joueur> listeJoueurs = LancementForm.listeJoueurs;
                        //On fait choisir la carte puis on l'écarte
                        carteEcartee = JoueurActuel.ChoisirUneCarte();
                        JoueurActuel.Ecarter(carteEcartee);
                        //Puis on ajoute le bon nombre de jetons de victoire
                        double tempCout = carteEcartee.Cout / 2;
                        JoueurActuel.JetonVictoireDispo += (int)Math.Floor(tempCout);

                        //Puis on boucle sur les autres joueurs pour qu'ils écartent
                        PartieForm.obligation = true;
                        for (int i = 0; i < listeJoueurs.Count; i++)
                        {
                            if (listeJoueurs[i] != JoueurActuel)
                            {
                                //Gérer le cas d'annulation
                                carteEcartee = listeJoueurs[i].ChoisirUneCarte();
                                listeJoueurs[i].Ecarter(carteEcartee);
                            }
                        }
                    }
                    break;
            }
        }

    }
}
