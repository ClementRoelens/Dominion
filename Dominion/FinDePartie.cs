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
    public partial class FinDePartie : Form
    {
        public FinDePartie()
        {
            InitializeComponent();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void FinDePartie_Load(object sender, EventArgs e)
        {
            //On va compter les points de chaque joueur
            List<Joueur> listeJoueurs = LancementForm.ListeJoueurs;
            //Pour ça, pour chaque joueur, on va ajouter la défausse et la main au deck et compter les points donnés par chaque carte
            foreach (Joueur joueur in listeJoueurs)
            {
                joueur.Deck.AddRange(joueur.Defausse);
                joueur.Deck.AddRange(joueur.Main);
                foreach (Carte carte in joueur.Deck.FindAll(x=>x.Type.Contains("Victoire")))
                {
                    joueur.Points += carte.PointDonne;
                    //Et si la carte a un gain de point de victoire dynamique, on le compte également
                    if (carte.EffetText != "")
                    {
                        PartieForm.tempJoueur = joueur;
                        carte.CompterPointsDeVictoire();
                    }
                }
                //Et on ajoute les jetons
                joueur.Points += joueur.JetonVictoireDispo;
            }
            //Et on indique les points pour les joueurs

            //D'abord les deux premiers
            joueur1Points.Text = $"{listeJoueurs[0].Nom} : {listeJoueurs[0].Points}";
            joueur2Points.Text = $"{listeJoueurs[1].Nom} : {listeJoueurs[1].Points}";

            //On affiche et affecte les autres selon le nombre de joueurs
            switch (listeJoueurs.Count)
            {
                case 3:
                    joueur3Points.Enabled = true;
                    joueur3Points.Visible = true;
                    joueur3Points.Text = $"{listeJoueurs[2].Nom} : {listeJoueurs[2].Points}";
                    break;
                case 4:
                    joueur3Points.Enabled = true;
                    joueur3Points.Visible = true;
                    joueur3Points.Text = $"{listeJoueurs[2].Nom} : {listeJoueurs[2].Points}";
                    joueur4Points.Enabled = true;
                    joueur4Points.Visible = true;
                    joueur4Points.Text = $"{listeJoueurs[3].Nom} : {listeJoueurs[3].Points}";
                    break;
            }

            //On doit maintenant déterminer le vainqueur
            //Pour ça, on crée une List des scores des joueurs respectifs
            List<int> ListPoints = new List<int>();
            foreach (Joueur joueur in listeJoueurs)
            {
                ListPoints.Add(joueur.Points);
            }
            //Puis on cherche le ou les joueurs avec le meilleur score
            List<Joueur> Vainqueurs = (listeJoueurs.Where(x => x.Points == ListPoints.Max())).ToList();
            //Et on l'indique selon le cas
            switch (Vainqueurs.Count)
            {
                case 1:
                    labelVainqueur.Text = $"{Vainqueurs[0].Nom} remporte la partie!";
                    break;
                case 2:
                    labelVainqueur.Text = $"{Vainqueurs[0].Nom} et {Vainqueurs[1].Nom} ex-aequo!";
                    break;
                case 3:
                    labelVainqueur.Text = $"{Vainqueurs[0].Nom}, {Vainqueurs[1].Nom} et {Vainqueurs[2].Nom} ex-aequo!";
                    break;
                case 4:
                    labelVainqueur.Text = "Egalité totale, chapeau!";
                    break;
            }



        }
    }
}
