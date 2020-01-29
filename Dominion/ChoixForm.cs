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
        public static string carteChoisie;

        public ChoixForm()
        {
            InitializeComponent();
        }

    
        private void ChoixForm_Load(object sender, EventArgs e)
        {
            //On fait une List de PictureBox comme d'hab
            List<PictureBox> listPB = new List<PictureBox>();
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

            //Et on affiche la main comme d'hab aussi
            int i = 0;
            foreach (Carte carte in PartieForm.JoueurActuel.Main)
            {
                listPB[i].ImageLocation = carte.Image;
                i++;
            }

            //On dit aussi qui va choisir
            joueurLabel.Text = PartieForm.tempJoueur.Nom.ToString() + " : choisissez.";

            //Et on active le bouton d'annulation s'il le faut
            if (PartieForm.obligation)
            {
                buttonCancel.Visible = true;
                buttonCancel.Enabled = true;
            }
        }

        private void Choix(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            carteChoisie = pb.ImageLocation;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
