using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dominion
{
    public partial class ChoixForm : Form
    {
        public static List<Carte> listeCartesChoisies;
        List<PictureBox> listPB = new List<PictureBox>();
        public static bool estValide;
        public static bool obligation;
        public static string typeChoix = "Défaut";
        public static int nbCarte = int.MaxValue;
        public static List<Carte> listeChoix = new List<Carte>();
        public static Carte carteAacheter;
        public static string bouton1;
        public static string bouton2;
        public static string possibiliteChoisie;

        public ChoixForm()
        {
            InitializeComponent();
        }

        private void ChoixForm_Load(object sender, EventArgs e)
        {
            //On gère à part le choix de possibilités, puisqu'elle n'implique aucune carte
            if (typeChoix != "Possibilité")
            {
                //Avant-tout, on réinitialise notre List de cartes choisis
                listeCartesChoisies = new List<Carte>();
                //On affiche la main (ou autre liste de cartes) comme d'hab aussi , et sans afficher les cartes en jeu
                listeChoix = listeChoix.FindAll(x => x.EnJeu == false);
                //On indique qui va choisir et ce qu'il doit choisir, et on restreint le choix selon le type de choix
                joueurLabel.Text = PartieForm.tempJoueur.Nom.ToString() + " : choisissez";
                if (nbCarte == 1)
                { joueurLabel.Text += " la carte "; }
                else
                { joueurLabel.Text += " les cartes "; }
                switch (typeChoix)
                {
                    case "Achat":
                        joueurLabel.Text = PartieForm.tempJoueur.Nom.ToString() + " : choisissez les trésors à utiliser pour payer.";
                        listeChoix = listeChoix.FindAll(x => x.Type == "Trésor");
                        break;
                    case "Ecarter":
                        joueurLabel.Text += "à écarter.";
                        break;
                    case "Défausser":
                        joueurLabel.Text += "à défausser.";
                        break;
                    case "Recevoir":
                        joueurLabel.Text += "que vous voulez.";
                        break;
                    case "Réaction":
                        joueurLabel.Text += " si vous voulez réagir.";
                        listeChoix = listeChoix.FindAll(x => x.Type.Contains("Réaction"));
                        break;
                }

                //Puis pour choisir on fait une List de PictureBox comme d'hab
                listPB.Add(pictureBox1);
                listPB.Add(pictureBox2);
                listPB.Add(pictureBox3);
                listPB.Add(pictureBox4);
                listPB.Add(pictureBox5);
                listPB.Add(pictureBox6);
                listPB.Add(pictureBox7);
                listPB.Add(pictureBox8);
                listPB.Add(pictureBox9);
                listPB.Add(pictureBox10);
                listPB.Add(pictureBox11);
                listPB.Add(pictureBox12);
                listPB.Add(pictureBox13);
                listPB.Add(pictureBox14);
                listPB.Add(pictureBox15);
                listPB.Add(pictureBox16);

                int i = 0;
                try
                {
                    foreach (Carte carte in listeChoix)
                    {
                        listPB[i].ImageLocation = carte.Image;
                        listPB[i].Enabled = true;
                        i++;
                    }
                }
                catch (ArgumentOutOfRangeException)
                { MessageBox.Show("Oups, il n'y a pas assez de places pour afficher toutes les cartes... Shame on the developer!"); }
                //On désactive le bouton d'annulation si le choix est obligatoire
                if (obligation)
                { buttonCancel.Enabled = false; }
                //De même avec le bouton de validation si le choix n'est que d'une carte
                if (nbCarte == 1)
                { validerButton.Enabled = false; }
                //Et par défaut on remet la valeur de validation à false
                estValide = false;

                #region Présélection des trésors

                //Mais en cas d'achat, on va également présélectionner les trésors pour le joueur
                if (typeChoix == "Achat")
                {
                    int monnaiePreSelectionnee = 0;
                    bool flag = false;
                    i = 0;
                    while ((!flag) && (i < listeChoix.Count))
                    {
                        if (monnaiePreSelectionnee < carteAacheter.Cout)
                        {
                            listPB[i].Anchor = AnchorStyles.Bottom;
                            listeCartesChoisies.Add(listeChoix[i]);
                            monnaiePreSelectionnee += listeChoix[i].MonnaieDonnee;
                            i++;
                        }
                        else
                        { flag = true; }
                    }
                }

                #endregion
            }
            //Si le joueur doit choisir une possibilité, on change juste le texte des boutons
            else
            {
                joueurLabel.Text = PartieForm.tempJoueur.Nom.ToString() + " : choisissez une possibilité.";
                buttonCancel.Text = bouton1;
                validerButton.Text = bouton2;
            }
        }

        private void Choix(object sender, EventArgs e)
        {
            //Comme d'habitude, une fois la PictureBox sélectionnée, on fait une boucle dans la List jusqu'à trouver celle-ci
            PictureBox pb = (PictureBox)sender;
            int i = 0;
            bool flag = false;
            while (!flag)
            {
                if (listPB[i] == pb)
                { flag = true; }
                else
                { i++; }
            }
            //Les images des cartes et les Cartes elles-mêmes sont dans le même ordre, donc on a trouvé l'index de la Carte correspondante

            //Si le joueur ne peut choisir qu'une seule carte, on transmet la carte et on ferme la Form
            if (nbCarte == 1)
            {
                listeCartesChoisies.Add(listeChoix[i]);
                estValide = true;
                this.Close();
            }
            //Sinon, on ajoute juste la carte à la List qui sera transmise quand le joueur appuie sur le bouton de validation
            //...ou on la retire si la carte a déjà été choisie!
            else
            {
                if (!listeCartesChoisies.Contains(listeChoix[i]))
                {
                    listeCartesChoisies.Add(listeChoix[i]);
                    //Si la carte est ajoutée dans la List de choix, on le signale en décalant la PB vers le bas (comme les cartes jouées dans la main)
                    pb.Anchor = AnchorStyles.Bottom;
                }
                else
                {
                    //Le compteur j permettra de naviguer dans la liste des cartes choisies
                    //Le compteur i doit garder sa valeur pour garder la carte sélectionnée
                    int j = 0;
                    flag = false;
                    while (!flag)
                    {
                        if (listeCartesChoisies[j] == listeChoix[i])
                        { flag = true; }
                        else { j++; }
                    }
                    listeCartesChoisies.RemoveAt(j);
                    //Et bien entendu, on remonte la PB
                    pb.Anchor = AnchorStyles.Top;
                }
            }
        }

        private void ValiderButton_Click(object sender, EventArgs e)
        {
            estValide = true;
            this.Close();
            if (typeChoix == "Possibilité")
            { possibiliteChoisie = bouton2; }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            estValide = false;
            listeCartesChoisies = null;
            this.Close();
            if (typeChoix == "Possibilité")
            { possibiliteChoisie = bouton1; }
        }
    }
}
