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
    public partial class AffichageCartesDevoilees : Form
    {
        public static List<Carte> listCartesDevoilees = new List<Carte>();

        public AffichageCartesDevoilees()
        {
            InitializeComponent();
        }

        private void AffichageCartesDevoilees_Load(object sender, EventArgs e)
        {
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
            listPB.Add(pictureBox11);
            listPB.Add(pictureBox12);
            listPB.Add(pictureBox13);
            listPB.Add(pictureBox14);
            listPB.Add(pictureBox15);
            listPB.Add(pictureBox16);

            int i = 0;
            foreach(Carte carte in listCartesDevoilees)
            {
                listPB[i].ImageLocation = carte.Image;
                listPB[i].Enabled = true;
                listPB[i].Visible = true;
                i++;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
