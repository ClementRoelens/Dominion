using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Dominion
{
    public class Carte : ICloneable
    {
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
            (string pNom, string pImage, int pCout, string pType, string pEffetText, int pMonnaieDonnee
            , int pCarteDonnee, int pActionDonnee, int pAchatDonne, int pJetonPointDonne, int pPointDonne)
        {
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
            Joueur JoueurActuel = PartieForm.JoueurActuel;

            switch (this.Nom)
            {
                case "Mascarade":

                    {
                        //Chaque joueur doit passer une carte de sa main au joueur suivant
                        Carte carteEcartee = new Carte();
                        List<Joueur> listeJoueurs = LancementForm.listeJoueurs;
                        //On fait donc une boucle de tous les joueurs, et on va choisir la carte, l'écarter de la main puis l'ajouter dans celle du joueur suivant
                        for (int i = 0, c = listeJoueurs.Count; i < c; i++)
                        {
                            //On utilise la méthode pour choisir une carte dans la main
                            carteEcartee = listeJoueurs[i].ChoisirUneCarte("Ecarter", listeJoueurs[i].Main, true);
                            //Puis on écarte
                            listeJoueurs[i].Ecarter(carteEcartee);
                            //Et on passe soit au joueur suivant, soit au premier si ce joueur est le dernier
                            if (listeJoueurs[i] == listeJoueurs[listeJoueurs.Count - 1])
                            { listeJoueurs[0].Main.Add(carteEcartee); }
                            else
                            { listeJoueurs[i + 1].Main.Add(carteEcartee); }
                        }
                        //Puis le deuxième effet : le joueur qui joue peut écarter une carte de sa main
                        carteEcartee = JoueurActuel.ChoisirUneCarte("Ecarter", JoueurActuel.Main, true);
                        JoueurActuel.Ecarter(carteEcartee);
                        //Après ça, il faut mettre à jour la main
                        JoueurActuel.MAJMain();
                    }
                    break;

                case "Evèque":
                    {
                        //Ici on ajoute 1 pièce et 1 jeton, puis on fait écarter une carte et on gagne d'autres jetons selon son coût, et les autres peuvent écarter également
                        Carte carteEcartee = new Carte();
                        List<Joueur> listeJoueurs = LancementForm.listeJoueurs;
                        //On fait choisir la carte puis on l'écarte
                        carteEcartee = JoueurActuel.ChoisirUneCarte("Ecarter", JoueurActuel.Main, true);
                        JoueurActuel.Ecarter(carteEcartee);
                        //Puis on ajoute le bon nombre de jetons de victoire
                        double tempCout = carteEcartee.Cout / 2;
                        JoueurActuel.JetonVictoireDispo += (int)Math.Floor(tempCout);

                        //Puis on boucle sur les autres joueurs pour qu'ils écartent
                        for (int i = 0; i < listeJoueurs.Count; i++)
                        {
                            if (listeJoueurs[i] != JoueurActuel)
                            {
                                //Gérer le cas d'annulation
                                carteEcartee = listeJoueurs[i].ChoisirUneCarte("Ecarter", listeJoueurs[i].Main, false);
                                listeJoueurs[i].Ecarter(carteEcartee);
                            }
                        }
                    }
                    break;

                case "Bibliothèque":
                    {
                        //Le joueur va piocher des cartes jusqu'à en avoir 7 et peut défausser les cartes action
                        List<Carte> deCoté = new List<Carte>();
                        while (JoueurActuel.Main.Count < 7)
                        {
                            JoueurActuel.Piocher(1);
                            Carte carte = JoueurActuel.Main[JoueurActuel.Main.Count - 1];
                            if (carte.Type.Contains("Action"))
                            {
                                //On affiche la question.
                                DialogResult dr = MessageBox.Show("Voulez-vous mettre " + carte.Nom + " de côté?", "Choix", MessageBoxButtons.YesNo);
                                //Si l'utilisateur répond oui, on ajoute la carte à la liste des cartes allant être défaussées
                                if (dr == DialogResult.Yes)
                                { deCoté.Add(carte); }
                            }
                        }
                        //Puis on défausse les cartes ayant été mises de côté
                        foreach (Carte carte in deCoté)
                        { JoueurActuel.Defausser(carte); }
                    }
                    break;

                case "Aventurier":
                    {
                        //On va dévoiler des cartes jusqu'à trouver deux trésors. On défausse les autres
                        List<Carte> cartesDevoilees = new List<Carte>();
                        int i = 0;
                        while (i < 2)
                        {
                            if (JoueurActuel.Deck[0].Type != "Trésor")
                            { JoueurActuel.Defausse.Add(JoueurActuel.Deck[0]); }
                            else
                            {
                                JoueurActuel.Main.Add(JoueurActuel.Deck[0]);
                                i++;
                            }
                            cartesDevoilees.Add(JoueurActuel.Deck[0]);
                            JoueurActuel.Deck.RemoveAt(0);
                        }
                        AffichageCartesDevoilees.listCartesDevoilees = cartesDevoilees;
                        AffichageCartesDevoilees form = new AffichageCartesDevoilees();
                        form.ShowDialog();
                    }
                    break;

                case "Agrandissement":
                    {
                        //On écarte une carte de la main et reçoit une carte coûtant jusqu'à 3 de plus
                        JoueurActuel.ChoisirUneCarte("Ecarter", JoueurActuel.Main, true);
                        JoueurActuel.Ecarter(ChoixForm.carteChoisie);
                        MessageBox.Show("Choisissez une carte coûtant jusqu'à 3 de plus que la carte écartée");
                        List<Carte> piles = new List<Carte>();
                        foreach (Pile pile in PartieForm.mapListe)
                        {
                            if ((pile.nombre > 0) && (pile.carte.Cout <= ChoixForm.carteChoisie.Cout + 3))
                            { piles.Add(pile.carte); }
                        }
                        JoueurActuel.ChoisirUneCarte("Recevoir", piles, true);
                        JoueurActuel.Defausse.Add(ChoixForm.carteChoisie);
                        //On lance la vérification pour savoir si on achève la partie
                        bool flag = false;
                        int i = 0;
                        while (!flag)
                        {
                            if (PartieForm.mapListe[i].carte == ChoixForm.carteChoisie)
                            {
                                PartieForm.mapListe[i].nombre--;
                                flag = true;
                                if (PartieForm.mapListe[i].nombre == 0)
                                {
                                    PartieForm.mapListe[i].pictureBox.ImageLocation = "";
                                    if ((PartieForm.mapListe[i].carte.Nom == "Province") || (PartieForm.mapListe[i].carte.Nom == "Colonie") || (PartieForm.nbPileVide == 3))
                                    {
                                        FinDePartie finDePartie = new FinDePartie();
                                        finDePartie.ShowDialog();
                                    }
                                }
                            }
                            i++;
                        }
                    }
                    break;
            }
        }

        public object Clone()
        {
            return new Carte
                (this.Nom, this.Image, this.Cout, this.Type, this.EffetText, this.MonnaieDonnee, this.CarteDonnee, this.ActionDonnee,
                this.AchatDonne, this.JetonPointDonne, this.PointDonne); ;
        }
    }
}
