using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dominion
{
    public class Joueur
    {
        public string Nom;
        public List<Carte> Deck = new List<Carte>();
        public List<Carte> Main = new List<Carte>();
        //public List<Carte> EnJeu = new List<Carte>();
        public List<Carte> Defausse = new List<Carte>();
        public int ActionDispo = 1;
        public int AchatDispo = 1;
        public int MonnaieDispo = 0;
        public int JetonVictoireDispo = 0;
        public int Points;

        public Joueur() { }

        public Joueur(string Nom)
        {
            this.Nom = Nom;
        }

        public void MelangerLeDeck()
        {
            if (this.Deck.Count == 0)
            {
                this.Deck.AddRange(this.Defausse);
                this.Defausse.Clear();
            }
            //S'il n'y a qu'une carte dans le deck, nul besoin de le mélanger...
            if (Deck.Count > 1)
            {
                List<Carte> deckMelange = new List<Carte>();
                //On copie la valeur de deck.Count puisque celle-ci va baisser à chaque itération, vu que nous allons supprimer progressivement les cartes
                for (int i = 0, c = this.Deck.Count; i < c; i++)
                {
                    //On prend un index au hasard parmi les index du deck, puis on ajoute la carte correspondante au deck mélangé
                    //Puis on la supprime du deck d'origine pour ne pas dédoubler les cartes
                    int index = PartieForm.rand.Next(0, this.Deck.Count);
                    deckMelange.Add(this.Deck[index]);
                    this.Deck.RemoveAt(index);
                }
                //Finalement, on copie cette nouvelle List mélangée dans le Deck
                this.Deck = new List<Carte>(deckMelange);
            }
        }

        public void Piocher(int nombre)
        {
            //Si on pioche 0 carte, pas la peine de perdre du temps
            if (nombre != 0)
            {
                //On ajoute à la main la première carte du deck, puis on supprime celle-ci du deck, vu qu'elle n'est plus dans le deck mais dans la main
                //On répète l'action pour le nombre de carte à piocher
                for (int i = 0; i < nombre; i++)
                {
                    bool continuer = true;
                    //Cependant, on doit gérer le cas où le deck est vide
                    try
                    { this.Main.Add(this.Deck[0]); }
                    //Si le deck est vide, il y a deux cas possibles
                    catch (ArgumentOutOfRangeException)
                    {
                        //Soit la défausse n'est pas vide, et on la mélange donc pour constituer un nouveau deck
                        if (this.Defausse.Count > 0)
                        {
                            this.MelangerLeDeck();
                            this.Main.Add(this.Deck[0]);
                        }
                        //Soit elle est vide, et donc l'action s'arrête car on ne peut plus piocher
                        else
                        {
                            MessageBox.Show("Il n'y a plus de carte à piocher, votre défausse et votre deck sont vides.");
                            continuer = false;
                            break;
                        }
                    }
                    //On supprime la première carte du deck si le deck n'est pas vide, donc si continuer est vrai
                    if (continuer)
                    { this.Deck.RemoveAt(0); }
                }
                //Si c'est au tour du joueur piochant, on met à jour la main et on actualise la PictureBox du deck si celui-ci est vide
                if (this == PartieForm.JoueurActuel)
                {
                    this.MAJMain();
                    this.MAJInfos();
                }
            }
        }

        public List<Carte> Devoiler(int nombre)
        {
            List<Carte> cartesDevoilees = new List<Carte>();
            for (int i = 0; i < nombre; i++)
            {//Au cas où le joueur n'aurait plus de cartes dans son deck...
                if (this.Deck.Count == 0)
                { this.MelangerLeDeck(); }
                //Ensuite on ajoute la carte dans la liste et on la supprime du deck
                cartesDevoilees.Add(this.Deck[0]);
                this.Deck.RemoveAt(0);
            }
            //Ensuite on passe la List à la variable globale et on lance l'autre fenêtre
            AffichageCartesDevoilees.listCartesDevoilees = cartesDevoilees;
            AffichageCartesDevoilees.joueurDevoilant = this;
            AffichageCartesDevoilees affichage = new AffichageCartesDevoilees();
            affichage.ShowDialog();

            return cartesDevoilees;
        }

        public void Defausser(Carte cible)
        {
            //On désactive la carte puis on l'ajoute à la List de défausse, et on la supprime de la main
            cible.EnJeu = false;
            this.Defausse.Add(cible);
            this.Main.Remove(this.Main.Find(x => x == cible));

            //Si le joueur qui défausse a la main, on doit supprimer l'image de sa main et l'ajouter dans la défausse
            if (this == PartieForm.JoueurActuel)
            {
                this.MAJMain();
                this.MAJInfos();
            }
        }

        public void Recevoir(Carte cible, string zone = "Défausse")
        {

            //On crée une nouvelle instance de la carte qu'on va ajouter à la défausse
            Carte tempCarte = (Carte)cible.Clone();

            //Si la cible est un campement nomade, elle est forcément reçue sur le deck
            if (cible.Nom == "Campement nomade")
            { zone = "Deck"; }

            //On ajoute la carte à la zone spécifiée et on met à jour les infos
            switch (zone)
            {
                case "En main":
                    this.Main.Add(tempCarte);
                    if (this == PartieForm.JoueurActuel)
                    {
                        this.MAJMain();
                        this.MAJInfos();
                    }
                    break;

                case "Défausse":
                    this.Defausse.Add(tempCarte);
                    //Bien entendu on met à jour l'affichage de la défausse si le joueur ayant reçu la carte est le joueur actuel
                    if (this == PartieForm.JoueurActuel)
                    {
                        PartieForm.defaussePB.ImageLocation = cible.Image;
                        PartieForm.defausseTB.Text = $"Défausse : {this.Defausse.Count.ToString()}";
                    }
                    break;

                case "Deck":
                    this.Deck.Insert(0, cible);
                    PartieForm.deckTB.Text = $"Deck : {this.Deck.Count.ToString()}";
                    break;
            }

            //Et on désincrémente le nombre de cartes dans la pile si la carte n'est pas une Malédiction (car elle n'est pas dans les piles)
            if (cible.Nom != "Malédiction")
            {
                //En cherchant la carte avant-tout...
                List<Pile> mapListe = PartieForm.mapListe;
                bool flag = false;
                int i = 0;
                while (!flag)
                {
                    if (mapListe[i].carte.Nom == cible.Nom)
                    { flag = true; }
                    else
                    { i++; }
                }
                mapListe[i].nombre--;

                //Et on lance la vérification pour savoir si on achève la partie
                //La partie s'achève quand : une des piles Colonie ou Province est vide, ou quand 3 piles au total sont vides
                if (mapListe[i].nombre == 0)
                {
                    //Tout d'abord, si la pile de la carte venant d'être reçue tombe à 0, alors on efface l'image, on désactive le PictureBox et on incrémente le compteur de piles vides
                    mapListe[i].carte.PictureBox.ImageLocation = @"C:\Users\ohne6\Desktop\Dominion\Images\dosDeCarte.jpg";
                    mapListe[i].carte.PictureBox.Enabled = false;
                    PartieForm.nbPileVide++;
                    //Et ensuite on teste si la partie s'arrête
                    if ((mapListe[i].carte.Nom == "Province") || (mapListe[i].carte.Nom == "Colonie") || (PartieForm.nbPileVide == 3))
                    {
                        FinDePartie finDePartie = new FinDePartie();
                        finDePartie.ShowDialog();
                    }
                }

                Console.WriteLine($"{this.Nom} reçoit {cible.Nom}"); 
            }
            //Certaines cartes déclenchent des effets spécifiques quand elles sont reçues
            switch (tempCarte.Nom)
            {

                case "Province":
                    {
                        //Si quelqu'un reçoit une Province, un autre joueur peut activer Or des fous

                        foreach (Joueur joueur in LancementForm.ListeJoueurs.FindAll(x => x != this))
                        {
                            List<Carte> orsDesFous = new List<Carte>();
                            orsDesFous.AddRange(joueur.Main.FindAll(x => x.Nom == "Or des fous"));
                            if (orsDesFous.Count > 0)
                            {
                                PartieForm.tempJoueur = LancementForm.ListeJoueurs.Find(x => x != this);
                                foreach (Carte carte in orsDesFous)
                                { carte.Reagir(); }
                            }
                        }
                    }
                    break;

                case "Ambassade":
                    {
                        //Quand un joueur reçoit une Ambassade, les autres reçoivent un argent

                        foreach (Joueur joueur in LancementForm.ListeJoueurs.FindAll(x => x != this))
                        {
                            joueur.Recevoir(PartieForm.mapListe.Find(x => x.carte.Nom == "Argent").carte);
                        }
                    }
                    break;

                case "Mandarin":
                    {
                        //Le joueur va placer tous ses trésors en jeu sur son deck, dans l'ordre qu'il souhaite

                        //On commence par créer la liste des trésors en jeu
                        List<Carte> aPlacer = this.Main.FindAll(x => (x.Type.Contains("Trésor") && (x.EnJeu)));
                        MessageBox.Show("Vous allez placer vos trésors en jeu sur votre deck, un par un.");
                        //Puis on demande de placer un trésor sur le deck jusqu'à qu'ils soient tous lacés
                        for (int i = 0, c = aPlacer.Count - 1; i < c; i++)
                        {
                            Carte carteAplacer = this.ChoisirUneCarte("Deck", aPlacer, true);
                            carteAplacer.EnJeu = false;
                            this.Deck.Insert(0, carteAplacer);
                            this.Main.Remove(carteAplacer);
                            aPlacer.Remove(carteAplacer);
                        }
                        //On place la dernière carte automatiquement, puisqu'il n'y a plus de choix à faire
                        aPlacer[0].EnJeu = false;
                        this.Deck.Insert(0, aPlacer[0]);
                        this.Main.Remove(aPlacer[0]);
                        this.MAJMain();
                    }
                    break;
            }
        }

        public void Ecarter(Carte cible)
        {
            //On retire la carte de la main
            this.Main.Remove(cible);

            //Si le joueur qui défausse a la main, on doit supprimer l'image de sa main
            if (this == PartieForm.JoueurActuel)
            { this.MAJMain(); }
        }

        public void MAJMain()
        {
            //On importe la List des PictureBox de la main
            List<PictureBox> listPictureBoxMain = PartieForm.listPictureBoxMain;
            //Cet index servira à avancer dans notre List
            int i = 0;
            //On commence par afficher les cartes de la main
            foreach (Carte carte in this.Main)
            {
                carte.PictureBox = listPictureBoxMain[i];
                carte.MAJpb();
                i++;
            }
            //Puis on supprime les images restantes s'il y en a
            bool flag = false;
            while ((i < listPictureBoxMain.Count) & (!flag))
            {
                if (listPictureBoxMain[i].ImageLocation == default)
                { flag = true; }
                else
                {
                    listPictureBoxMain[i].ImageLocation = default; ;
                    listPictureBoxMain[i].Visible = false;
                    listPictureBoxMain[i].Enabled = false;
                    i++;
                }
            }
        }

        public void MAJInfos()
        {
            PartieForm.tourTB.Text = "Tour de " + this.Nom;
            PartieForm.achatDispoTB.Text = this.AchatDispo.ToString() + " achat(s)";
            PartieForm.actionDispoTB.Text = this.ActionDispo.ToString() + " action(s)";
            PartieForm.monnaieDispoTB.Text = "Monnaie dispo : " + this.MonnaieDispo.ToString();
            int monnaietotale = 0;
            foreach (Carte carte in this.Main)
            {
                if ((carte.Type.Contains("Trésor")) && !(carte.EnJeu))
                { monnaietotale += carte.MonnaieDonnee; }
            }
            PartieForm.monnaieTotaleTB.Text = $"Monnaie totale en main : {monnaietotale.ToString()}";
            PartieForm.jetonsTB.Text = this.JetonVictoireDispo.ToString() + " jeton(s) victoire";

            PartieForm.deckPB.ImageLocation = (this.Deck.Count > 0) ? default : "";
            PartieForm.deckTB.Text = $"Deck : {this.Deck.Count}";
            PartieForm.defaussePB.ImageLocation = (this.Defausse.Count > 0) ? this.Defausse[this.Defausse.Count - 1].Image : "";
            PartieForm.defausseTB.Text = $"Défausse : {this.Defausse.Count}";
        }

        public string ChoisirUnePossibilite(string possibilite1, string possibilite2)
        {
            PartieForm.tempJoueur = this;
            ChoixForm.typeChoix = "Possibilité";
            ChoixForm.bouton1 = possibilite1;
            ChoixForm.bouton2 = possibilite2;

            ChoixForm choixForm = new ChoixForm();
            choixForm.ShowDialog();

            return ChoixForm.possibiliteChoisie;
        }

        public Carte ChoisirUneCarte(string type, List<Carte> choix, bool obligation)
        {
            //On assigne les variables nécessaires 
            PartieForm.tempJoueur = this;
            ChoixForm.typeChoix = type;
            ChoixForm.listeChoix = choix;
            ChoixForm.obligation = obligation;
            ChoixForm.nbCarte = 1;

            ChoixForm choixForm = new ChoixForm();
            choixForm.ShowDialog();
            try
            { return ChoixForm.listeCartesChoisies[0]; }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public List<Carte> ChoisirDesCartes(string type, List<Carte> choix, int nbCarte, bool obligation)
        {
            //On assigne les variables nécessaires 
            PartieForm.tempJoueur = this;
            ChoixForm.typeChoix = type;
            ChoixForm.listeChoix = choix;
            ChoixForm.obligation = obligation;
            ChoixForm.nbCarte = nbCarte;

            ChoixForm choixForm = new ChoixForm();
            choixForm.ShowDialog();

            return ChoixForm.listeCartesChoisies;
        }

    }
}

