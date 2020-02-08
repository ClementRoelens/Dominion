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
        public static List<Carte> listeCartesChoisies = new List<Carte>();
        List<PictureBox> listPB = new List<PictureBox>();
        public static bool estValide;
        List<Carte> listeChoix = new List<Carte>();

        public ChoixForm()
        {
            InitializeComponent();
        }

        private void ChoixForm_Load(object sender, EventArgs e)
        {
            //Avant-tout, on réinitialise notre List de cartes choisis
            listeCartesChoisies.Clear();
            //On indique qui va choisir et ce qu'il doit choisir
            joueurLabel.Text = PartieForm.tempJoueur.Nom.ToString() + " : choisissez";
            if (PartieForm.nbCarte == 1)
            { joueurLabel.Text += " la carte "; }
            else
            { joueurLabel.Text += " les cartes "; }
            switch (PartieForm.typeChoix)
            {
                case "Achat":
                    joueurLabel.Text = PartieForm.tempJoueur.Nom.ToString() + " : choisissez les trésors à utiliser pour payer.";
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
            //Et on affiche la main (ou autre liste de cartes) comme d'hab aussi , et sans afficher les cartes en jeu
            listeChoix = PartieForm.listeChoix.FindAll(x => x.EnJeu == false);
            //Si c'est un achat, on n'affiche également que les cartes Trésor
            if (PartieForm.typeChoix == "Achat")
            { listeChoix = listeChoix.FindAll(x => x.Type == "Trésor"); }
            int i = 0;
            foreach (Carte carte in listeChoix)
            {
                listPB[i].ImageLocation = carte.Image;
                listPB[i].Enabled = true;
                i++;
            }
            //On désactive le bouton d'annulation si le choix est obligatoire
            if (PartieForm.obligation)
            { buttonCancel.Enabled = false; }
            //De même avec le bouton de validation si le choix n'est que d'une carte
            if (PartieForm.nbCarte == 1)
            { validerButton.Enabled = false; }
            //Et par défaut on remet la valeur de validation à false
            estValide = false;

            #region Présélection des trésors

            //Mais en cas d'achat, on va également présélectionner les trésors pour le joueur
            if (PartieForm.typeChoix == "Achat")
            {
                int monnaiePreSelectionnee = 0;
                bool flag = false;
                i = 0;
                while ((!flag) && (i < listeChoix.Count))
                {
                    if (monnaiePreSelectionnee < PartieForm.carteAacheter.Cout)
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
            if (PartieForm.nbCarte == 1)
            {
                listeCartesChoisies[0] = listeChoix[i];
                estValide = true;
                //DEBUG
                Console.WriteLine("_____________\nChoix fait");
                Console.Write(PartieForm.tempJoueur.Nom + " choisit ");
                if (!(listeCartesChoisies is null))
                { Console.WriteLine(listeCartesChoisies[0].Nom); }
                //DEBUG
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
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            estValide = false;
            listeCartesChoisies = null;
            //DEBUG
            Console.WriteLine("______________\nAnnulation du choix");
            Console.WriteLine(PartieForm.tempJoueur.Nom + " ne choisit rien.");
            if (!(listeCartesChoisies[0] is null))
            { Console.WriteLine("Problème : la carte choisie est " + listeCartesChoisies[0].Nom+"\n______________"); }
            else { Console.WriteLine("_____________"); }
            //DEBUG
            this.Close();
        }
    }
}
