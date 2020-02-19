using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dominion
{
    public partial class LancementForm : Form
    {
        //Création de la liste des cartes
        public static List<Carte> ListeCartesDeBase = new List<Carte>();
        public static List<Carte> ListeCartesAction = new List<Carte>();
        //Création de la liste de joueurs
        public static List<Joueur> ListeJoueurs = new List<Joueur>();

        public LancementForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //On va d'abord créer une instance de chaque carte

            //Configuration et connexion
            //string connectPath = @".\SQLEXPRESS01";
            string path = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\.."));
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\BD_cartes.mdf;Integrated Security=True");
            SqlCommand cmd;
            string sSQL = "SELECT* FROM [dbo].[Cartes_de_base]";
            cmd = new SqlCommand(sSQL, conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dataRead;
            conn.Open();
            dataRead = cmd.ExecuteReader();
            //On initialise une carte temporaire
            Carte tempCarte;
            //Elle va recevoir toutes les caractéristiques enregistrées dans la BD
            //Puis elle sera ajoutée à la et on passe à la carte suivante
            while (dataRead.Read())
            {
                tempCarte = new Carte
                    (dataRead["Nom"].ToString(),
                    path + @"\Images\" + dataRead["Image"].ToString(),
                    int.Parse(dataRead["Cout"].ToString()), dataRead["Type"].ToString(),
                    dataRead["Effet"].ToString(),
                    int.Parse(dataRead["MonnaieDonne"].ToString()),
                    int.Parse(dataRead["CarteDonnee"].ToString()),
                    int.Parse(dataRead["ActionDonnee"].ToString()),
                    int.Parse(dataRead["AchatDonne"].ToString()),
                    int.Parse(dataRead["JetonPointDonne"].ToString()),
                    int.Parse(dataRead["PointDonne"].ToString()));
                ListeCartesDeBase.Add(tempCarte);
            }
            dataRead.Close();

            //Et on fait la même chose pour les cartes Action
            sSQL = "SELECT* FROM [dbo].[Cartes_action]";
            cmd = new SqlCommand(sSQL, conn);
            dataRead = cmd.ExecuteReader();
            while (dataRead.Read())
            {
                tempCarte = new Carte
                    (dataRead["Nom"].ToString(),
                    path + @"\Images\" + dataRead["Image"].ToString(),
                    int.Parse(dataRead["Cout"].ToString()), dataRead["Type"].ToString(),
                    dataRead["Effet"].ToString(),
                    int.Parse(dataRead["MonnaieDonne"].ToString()),
                    int.Parse(dataRead["CarteDonnee"].ToString()),
                    int.Parse(dataRead["ActionDonnee"].ToString()),
                    int.Parse(dataRead["AchatDonne"].ToString()),
                    int.Parse(dataRead["JetonPointDonne"].ToString()),
                    int.Parse(dataRead["PointDonne"].ToString()));
                ListeCartesAction.Add(tempCarte);
            }
            //Et on ferme la connexion
            dataRead.Close();
            conn.Close();
        }

        private void CheckBoxJoueur3_CheckedChanged(object sender, EventArgs e)
        {
            //Les champs Joueurs 3 et 4 sont désactivés par défaut
            //On peut activer un champ en cochant sa checkbox... Mais on active donc la CheckBox du Joueur 4 seulement si le Joueur 3 est actif
            if (checkBoxJoueur3.Checked)
            {
                nomJoueur3.Enabled = true;
                checkBoxJoueur4.Enabled = true;
                //On va aussi directement focus la TextBox pour simplifier la vie de l'utilisateur
                nomJoueur3.Focus();
            }
            //Et si la CB3 est désactivé, on désactive le J4 et sa CB également
            else
            {
                nomJoueur3.Enabled = false;
                checkBoxJoueur4.Checked = false;
                checkBoxJoueur4.Enabled = false;
            }
            
        }

        //De même, on active la TextBox du J4 seulement si sa CB est cochée
        private void CheckBoxJoueur4_CheckedChanged(object sender, EventArgs e)
        {
            nomJoueur4.Enabled = checkBoxJoueur4.Checked ? true : false;
        }

        //Fonction permettant de sélectionner le texte afin d'effacer rapidement le nom par défaut 
        private void Focus(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            textbox.SelectAll();
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            //On constitue la liste des joueurs selon le nombre
            Joueur tempJoueur = new Joueur(nomJoueur1.Text);
            ListeJoueurs.Add(tempJoueur);
            tempJoueur = new Joueur(nomJoueur2.Text);
            ListeJoueurs.Add(tempJoueur);
            if (checkBoxJoueur3.Checked)
            {
                tempJoueur = new Joueur(nomJoueur3.Text);
                ListeJoueurs.Add(tempJoueur);

                if (checkBoxJoueur4.Checked)
                {
                    tempJoueur = new Joueur(nomJoueur4.Text);
                    ListeJoueurs.Add(tempJoueur);
                }
            }

            //On constitue également le deck de base
            List<Carte> DeckGenerique = new List<Carte>();

            //On crée une cartetemporaire
            Carte tempCarte = new Carte();
            //Elle va d'abord pointer vers la carte cuivre
            tempCarte = ListeCartesDeBase.Find(x => x.Nom == "Cuivre");
            //Et on l'ajoute 7 fois au deck
            for (int i = 0; i < 7; i++)
            {
                Carte carteAjoutee = (Carte)tempCarte.Clone();
                DeckGenerique.Add(carteAjoutee);
            }
            //Ensuite on fait de même avec le Domaine, mais 3 fois
            tempCarte = ListeCartesDeBase.Find(x => x.Nom == "Domaine");
            for (int i = 0; i < 3; i++)
            {
                Carte carteAjoutee = (Carte)tempCarte.Clone();
                DeckGenerique.Add(carteAjoutee);
            }

            //Et on donne ce même deck à chaque joueur
            foreach (Joueur joueur in ListeJoueurs)
            {
                //On clone chaque carte et on l'ajoute au deck du joueur
                foreach (Carte carte in DeckGenerique)
                {
                    Carte carteClone = (Carte)carte.Clone();
                    joueur.Deck.Add(carteClone);
                }
            }

            //Et on lance la Form de la partie
            PartieForm partie = new PartieForm();
            partie.Show();


        }

        private void Lancer(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            { goButton.PerformClick(); }
        }
    }
}
