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
        public List<Carte> Deck;
        public List<Carte> Main = new List<Carte>();
        //public List<Carte> EnJeu = new List<Carte>();
        public List<Carte> Defausse = new List<Carte>();
        public int ActionDispo = 1;
        public int AchatDispo = 1;
        public int MonnaieDispo = 0;
        public int JetonVictoireDispo = 0;

        public Joueur(string pNom)
        {
            this.Nom = pNom;
        }

        public void MelangerLeDeck()
        {
            if (this.Deck.Count == 0)
            {
                this.Deck = new List<Carte>(this.Defausse);
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
                    if (this.Deck.Count == 0)
                    { PartieForm.deckPB.ImageLocation = ""; }
                    PartieForm.deckTB.Text = "Deck : " + this.Deck.Count.ToString();
                }
            }
        }

        public void Devoiler(int nombre)
        {

        }

        public void Defausser(Carte cible)
        {
            //Si la carte est en jeu, on doit d'abord passer le "En Jeu" à false
            if (cible.EnJeu)
            { cible.EnJeu = false; }
            //On ajoute la carte à la List de défausse
            this.Defausse.Add(cible);
            //Puis on supprime de la main
            this.Main.RemoveAt(this.Main.FindIndex(x => (x.Nom == cible.Nom) && (x.EnJeu == false)));

            //Si le joueur qui défausse a la main, on doit supprimer l'image de sa main et l'ajouter dans la défausse
            if (this == PartieForm.JoueurActuel)
            {
                this.MAJMain();
                PartieForm.defaussePB.ImageLocation = cible.Image;
                PartieForm.defausseTB.Text = "Défausse : " + this.Defausse.Count.ToString();
            }
        }

        public void Ecarter(Carte cible)
        {
            //On retire la carte de la main
            this.Main.RemoveAt(this.Main.FindIndex(x => (x.Nom == cible.Nom) && (x.EnJeu == false)));

            //Si le joueur qui défausse a la main, on doit supprimer l'image de sa main
            if (this == PartieForm.JoueurActuel)
            { this.MAJMain(); }
            //DEBUG
            Console.WriteLine("_________\nFonction d'écartement");
            Console.WriteLine(this.Nom + " écarte " + cible.Nom+"\n______________");
            //DEBUG
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
                if (listPictureBoxMain[i].ImageLocation != carte.Image)
                {
                    listPictureBoxMain[i].ImageLocation = carte.Image;
                    listPictureBoxMain[i].Visible = true;
                    listPictureBoxMain[i].Enabled = true;
                    listPictureBoxMain[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    if (carte.EnJeu)
                    { listPictureBoxMain[i].Anchor = AnchorStyles.Bottom; }
                    else
                    { listPictureBoxMain[i].Anchor = AnchorStyles.Top; }
                }
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

        public Carte ChoisirUneCarte(string type, List<Carte> choix, bool obligation)
        {
            //On assigne les variables nécessaires 
            PartieForm.tempJoueur = this;
            PartieForm.typeChoix = type;
            PartieForm.listeChoix = choix;
            PartieForm.obligation = obligation;
            PartieForm.nbCarte = 1;

            ChoixForm choixForm = new ChoixForm();
            choixForm.ShowDialog();

            return ChoixForm.listeCartesChoisies[0];
        }

        public List<Carte> ChoisirDesCartes(string type, List<Carte> choix, int nbCarte, bool obligation)
        {
            //On assigne les variables nécessaires 
            PartieForm.tempJoueur = this;
            PartieForm.typeChoix = type;
            PartieForm.listeChoix = choix;
            PartieForm.obligation = obligation;
            PartieForm.nbCarte = nbCarte;

            ChoixForm choixForm = new ChoixForm();
            choixForm.ShowDialog();

            return ChoixForm.listeCartesChoisies;
        }




    }
}

