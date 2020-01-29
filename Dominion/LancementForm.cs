using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dominion
{
    public partial class LancementForm : Form
    {
        //Création de la liste des cartes
        public static List<Carte> listeCartes = new List<Carte>();
        //Création de la liste de joueurs
        public static List<Joueur> listeJoueurs = new List<Joueur>();

        public LancementForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Configuration et connexion
            string connectPath = @".\SQLEXPRESS01";
            SqlConnection conn = new SqlConnection(@"Data Source=" + connectPath + @"; integrated security = true; Initial catalog = DominionBD");
            SqlCommand cmd;
            string sSQL = "SELECT* FROM [dbo].[Cartes]";
            cmd = new SqlCommand(sSQL, conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dataRead;
            conn.Open();
            dataRead = cmd.ExecuteReader();
            //On initialise une carte temporaire
            Carte tempCarte;
            //Elle va recevoir toutes les caractéristiques enregistrées dans la BD
            //Puis elle sera ajoutée à la liste
            //Puis on passe à la carte suivante
            while (dataRead.Read())
            {
                tempCarte = new Carte(int.Parse(dataRead["id"].ToString()), dataRead["Nom"].ToString(), dataRead["Image"].ToString(), int.Parse(dataRead["Cout"].ToString()), dataRead["Type"].ToString()
                    , dataRead["Effet"].ToString(), int.Parse(dataRead["MonnaieDonne"].ToString()), int.Parse(dataRead["CarteDonnee"].ToString()), int.Parse(dataRead["ActionDonnee"].ToString()),
                    int.Parse(dataRead["AchatDonne"].ToString()), int.Parse(dataRead["JetonPointDonne"].ToString()), int.Parse(dataRead["PointDonne"].ToString()));
                listeCartes.Add(tempCarte);
            }
            dataRead.Close();
        }


        private void CheckBoxJoueur3_CheckedChanged(object sender, EventArgs e)
        {
            //Les champs Joueurs 3 et 4 sont désactivés par défaut
            //On peut activer un champ en cochant sa checkbox... Mais on active donc la CB du Joueur 4 seulement si le Joueur 3 est actif!
            if (checkBoxJoueur3.Checked)
            {
                nomJoueur3.Enabled = true;
                checkBoxJoueur4.Enabled = true;
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
            listeJoueurs.Add(tempJoueur);
            tempJoueur = new Joueur(nomJoueur2.Text);
            listeJoueurs.Add(tempJoueur);
            if (checkBoxJoueur3.Checked)
            {
                tempJoueur = new Joueur(nomJoueur3.Text);
                listeJoueurs.Add(tempJoueur);

                if (checkBoxJoueur4.Checked)
                {
                    tempJoueur = new Joueur(nomJoueur4.Text);
                    listeJoueurs.Add(tempJoueur);
                }
            }

            //On constitue également le deck de base
            List<Carte> DeckGenerique = new List<Carte>();

            //On crée une carte pour stocker le résultat de nos requêtes
            Carte tempCarte = new Carte();
            //On crée une requête pour le cuivre
            var cuivreQuery =
                    from carte in listeCartes
                    where carte.Nom == "Cuivre"
                    select carte;
            //Puis on stocke son résultat dans notre tempCarte
            foreach (Carte carte in cuivreQuery)
            { tempCarte = carte; }
            //Et on l'ajoute 7 fois au deck
            for (int i = 0; i < 7; i++)
            {
                Carte carteAjoutee = (Carte)tempCarte.Clone();
                DeckGenerique.Add(carteAjoutee);
            }
            //Ensuite on fait de même avec le Domaine, mais 3 fois
            var domaineQuery = 
                from carte in listeCartes
                where carte.Nom == "Domaine"
                select carte;
            foreach (Carte carte in domaineQuery)
            { tempCarte = carte; }
            for (int i = 0; i < 3; i++)
            {
                Carte carteAjoutee = (Carte)tempCarte.Clone();
                DeckGenerique.Add(tempCarte);
            }

            //Et on donne ce même deck à chaque joueur
            foreach (Joueur joueur in listeJoueurs)
            { joueur.Deck = new List<Carte>(DeckGenerique); }
            
            //Et on lance la Form de la partie
            PartieForm partie = new PartieForm();
            partie.Show();


        }
    }
}
