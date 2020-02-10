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
        public PictureBox PictureBox;

        public Carte() { }

        public Carte
            (string Nom, string Image, int Cout, string Type, string EffetText, int MonnaieDonnee
            , int CarteDonnee, int ActionDonnee, int AchatDonne, int JetonPointDonne, int PointDonne)
        {
            this.Nom = Nom;
            this.Image = Image;
            this.Cout = Cout;
            this.Type = Type;
            this.EffetText = EffetText;
            this.MonnaieDonnee = MonnaieDonnee;
            this.CarteDonnee = CarteDonnee;
            this.ActionDonnee = ActionDonnee;
            this.AchatDonne = AchatDonne;
            this.JetonPointDonne = JetonPointDonne;
            this.PointDonne = PointDonne;
        }

        public void MAJpb()
        {
            this.PictureBox.ImageLocation = this.Image;
            this.PictureBox.Anchor = (this.EnJeu) ? AnchorStyles.Bottom : AnchorStyles.Top; ;
            this.PictureBox.Visible = true;
            this.PictureBox.Enabled = true;
        }

        public void Effet()
        {
            //On code les effets
            Joueur JoueurActuel = PartieForm.JoueurActuel;

            switch (this.Nom)
            {
                case "Mascarade":
                    {
                        //Chaque joueur doit passer une carte de sa main au joueur suivant, puis le joueur ayant joué la carte peut écarter une carte

                        List<Carte> carteEcartees = new List<Carte>();
                        List<Joueur> listeJoueurs = LancementForm.listeJoueurs;
                        //On fait donc une boucle de tous les joueurs, et on va choisir la carte, l'écarter de la main puis l'ajouter dans celle du joueur suivant
                        for (int i = 0, c = listeJoueurs.Count; i < c; i++)
                        {
                            //On utilise la méthode pour choisir une carte dans la main
                            carteEcartees.Add(listeJoueurs[i].ChoisirUneCarte("Ecarter", listeJoueurs[i].Main, true));
                            //Puis on écarte
                            listeJoueurs[i].Ecarter(carteEcartees[i]);
                        }
                        //Puis la carte est passée au joueur suivant (ou au premier quand le compteur atteint la limite du nombre de joueurs)
                        for (int i = 0, c = listeJoueurs.Count; i < c; i++)
                        {
                            if (listeJoueurs[i] == listeJoueurs[listeJoueurs.Count - 1])
                            { listeJoueurs[0].Main.Add(carteEcartees[i]); }
                            else
                            { listeJoueurs[i + 1].Main.Add(carteEcartees[i]); }
                        }
                        JoueurActuel.MAJMain();

                        //Puis le deuxième effet : le joueur qui joue peut écarter une carte de sa main
                        Carte carteEcartee = new Carte();
                        carteEcartee = JoueurActuel.ChoisirUneCarte("Ecarter", JoueurActuel.Main, false);
                        if (!(carteEcartee is null))
                        { JoueurActuel.Ecarter(carteEcartee); }
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
                            carteEcartee = null;
                            //DEBUG
                            Console.WriteLine("________\nEntrée dans l'écartement optionnel de l'évèque");
                            Console.Write("carteEcartee vaut normalement null.\nElle vaut réellement : ");
                            if (carteEcartee is null)
                            { Console.WriteLine("null."); }
                            else
                            { Console.WriteLine(carteEcartee.Nom); }
                            //DEBUG
                            //Gérer le cas d'annulation
                            carteEcartee = listeJoueurs[i].ChoisirUneCarte("Ecarter", listeJoueurs[i].Main, false);
                            if (!(carteEcartee is null))
                            { listeJoueurs[i].Ecarter(carteEcartee); }
                        }
                    }
                    break;

                case "Bibliothèque":
                    {
                        //Le joueur va piocher des cartes jusqu'à en avoir 7 et peut défausser les cartes action

                        //On crée une liste pour les cartes allant être défaussées
                        List<Carte> deCoté = new List<Carte>();
                        while (JoueurActuel.Main.Count < 7)
                        {
                            JoueurActuel.Piocher(1);
                            Carte carte = JoueurActuel.Main[JoueurActuel.Main.Count - 1];
                            if (carte.Type.Contains("Action"))
                            {
                                //On affiche la question.
                                DialogResult dr = MessageBox.Show($"Voulez-vous mettre {carte.Nom} de côté?", "Choix", MessageBoxButtons.YesNo);
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

                        //On crée une List de cartes dévoilées
                        List<Carte> cartesDevoilees = new List<Carte>();
                        int i = 0;
                        //Le compteur sera incrémenté par les trésors uniquement
                        while (i < 2)
                        {
                            //Si la première carte du deck n'est pas un trésor, elle est ajoutée à la défausse
                            if (JoueurActuel.Deck[0].Type != "Trésor")
                            { JoueurActuel.Defausse.Add(JoueurActuel.Deck[0]); }
                            else
                            {
                                //Sinon, elle est ajoutée à la main et le compteur est incrémenté
                                JoueurActuel.Main.Add(JoueurActuel.Deck[0]);
                                i++;
                            }
                            //Dans tous les cas, la carte est ajoutée à la List des cartes dévoilées, puis supprimée du Deck
                            cartesDevoilees.Add(JoueurActuel.Deck[0]);
                            JoueurActuel.Deck.RemoveAt(0);
                        }
                        //On affiche les cartes dévoilées
                        AffichageCartesDevoilees.listCartesDevoilees = cartesDevoilees;
                        AffichageCartesDevoilees form = new AffichageCartesDevoilees();
                        form.ShowDialog();
                        //Puis on met à jour les infos et la main
                        JoueurActuel.MAJInfos();
                        JoueurActuel.MAJMain();
                    }
                    break;

                case "Agrandissement":
                    {
                        //On écarte une carte de la main et reçoit une carte coûtant jusqu'à 3 de plus

                        //On fait d'abord choisir la carte à écarter dans la main
                        Carte carteEcartee = JoueurActuel.ChoisirUneCarte("Ecarter", JoueurActuel.Main, true);
                        JoueurActuel.Ecarter(carteEcartee);
                        MessageBox.Show("Choisissez une carte coûtant jusqu'à 3 de plus que la carte écartée");
                        //Puis on crée une liste de cartes coûtant au max 3 de plus que la carte écartée afin de la proposer en choix
                        List<Carte> piles = new List<Carte>();
                        //On crée une autre liste de Pile qu'on va inverser pour afficher en premier les cartes les plus intéressantes
                        List<Pile> mapListe = new List<Pile>(PartieForm.mapListe);
                        mapListe.Reverse();
                        foreach (Pile pile in mapListe)
                        {
                            //Les cartes proposées ne doivent pas coûter plus que 3 de plus que la carte écartée, mais elles doivent aussi encore être disponibles
                            if ((pile.nombre > 0) && (pile.carte.Cout <= carteEcartee.Cout + 3))
                            { piles.Add(pile.carte); }
                        }
                        //On choisit la carte et on la reçoit
                        Carte carteRecue = JoueurActuel.ChoisirUneCarte("Recevoir", piles, true);
                        JoueurActuel.Recevoir(carteRecue);
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
