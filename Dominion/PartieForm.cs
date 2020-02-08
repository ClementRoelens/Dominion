using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Dominion
{
    public partial class PartieForm : Form
    {
        //La "map" du jeu est composée d'une List de Pile de cartes. 
        public static List<Pile> mapListe = new List<Pile>();
        Pile focusPile = new Pile();
        public static int nbPileVide = 0;
        List<TextBox> focusDetailsList = new List<TextBox>();
        List<PictureBox> focusIcones = new List<PictureBox>();
        List<Joueur> listeJoueurs = LancementForm.listeJoueurs;
        public static Joueur JoueurActuel;
        public static List<PictureBox> listPictureBoxMain = new List<PictureBox>();
        public static PictureBox defaussePB;
        public static TextBox defausseTB;
        public static PictureBox deckPB;
        public static TextBox deckTB;
        public static TextBox infoActionTB;

        //Pour les choix de cartes
        public static Joueur tempJoueur;
        public static bool obligation;
        public static string typeChoix = "Défaut";
        public static int nbCarte = int.MaxValue;
        public static List<Carte> listeChoix = new List<Carte>();
        public static Carte carteAacheter;

        public PartieForm()
        {
            InitializeComponent();
        }

        private void PartieForm_Load(object sender, EventArgs e)
        {
            //On commence par constituer la map du jeu en faisant une sélection de 10 cartes Action et en chargeant toutes les cartes

            #region Constitution de la map

            //Une pile est composée d'une carte, du nombre de cartes composant la pile, et de la PictureBox comportant l'image de la Carte
            //Cela est nécessaire car l'utilisateur n'interagira qu'avec les images, et donc les PictureBox
            //On commence par faire une List des PictureBox pour ensuite pouvoir les ajouter à nos Pile
            List<PictureBox> listPicturebox = new List<PictureBox>();
            listPicturebox.Add(pictureBox1);
            listPicturebox.Add(pictureBox2);
            listPicturebox.Add(pictureBox3);
            listPicturebox.Add(pictureBox4);
            listPicturebox.Add(pictureBox5);
            listPicturebox.Add(pictureBox6);
            listPicturebox.Add(pictureBox7);
            listPicturebox.Add(pictureBox8);
            listPicturebox.Add(pictureBox9);
            listPicturebox.Add(pictureBox10);
            listPicturebox.Add(pictureBox11);
            listPicturebox.Add(pictureBox12);
            listPicturebox.Add(pictureBox13);
            listPicturebox.Add(pictureBox14);
            listPicturebox.Add(pictureBox15);
            listPicturebox.Add(pictureBox16);
            listPicturebox.Add(pictureBox17);
            listPicturebox.Add(pictureBox18);
            //On va ensuite ajouter les Carte à chaque Pile
            //On mettra toutes les cartes de type Action à part, pour en tirer ensuite 10 au hasard
            List<Carte> cartesAction = new List<Carte>();
            //On commence donc par ajouter les cartes de type Trésor et Victoire à notre map, et les cartes Action à notre List à trier
            //On utilise un compteur implémenté à chaque fois qu'une carte est ajoutée à la map
            //Ainsi on sait quel PictureBox ajouter dans notre Pile
            int i = 0;
            foreach (Carte carte in LancementForm.listeCartes)
            {
                if ((carte.Type == "Trésor") || (carte.Type == "Victoire"))
                {
                    Pile pile = new Pile(carte);
                    pile.pictureBox = listPicturebox[i];
                    i++;
                    mapListe.Add(pile);
                }
                else
                { cartesAction.Add(carte); }
            }

            //Maintenant que nous avons ajouté toutes les cartes Trésor et Victoire, on va mélanger la List des Actions et ajouter les 10 premières.
            //Pour ce faire, on va rajouter aléatoirement une carte de notre première List à une List temporaire de cartes Action (pour ensuite les trier)
            Random rand = new Random();
            List<Carte> tempActions = new List<Carte>();
            for (int j = 0; j < 10; j++)
            {
                int index = rand.Next(0, cartesAction.Count);
                tempActions.Add(cartesAction[index]);
                cartesAction.RemoveAt(index);
            }
            //Une fois nos 10 cartes sélectionnées, on trie par Coût les cartes Action sélectionnées
            tempActions.Sort(delegate (Carte a1, Carte a2) { return a1.Cout - a2.Cout; });
            //Puis on les ajoute à la mapListe
            for (int j = 0; j < 10; j++)
            {
                Pile pile = new Pile(tempActions[j]);
                pile.pictureBox = listPicturebox[i];
                i++;
                mapListe.Add(pile);
            }

            //On remplit les PictureBox
            foreach (Pile pile in mapListe)
            {
                pile.pictureBox.ImageLocation = pile.carte.Image;
                pile.pictureBox.Dock = DockStyle.Fill;
                pile.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            #endregion

            //On constitue les decks, les mélange et pioche la première main pour chaque joueur

            #region Initialisation de la partie

            //On commence par faire une List des PictureBox qui seront utilisées pour afficher les cartes de la main
            listPictureBoxMain.Add(carteMain1);
            listPictureBoxMain.Add(carteMain2);
            listPictureBoxMain.Add(carteMain3);
            listPictureBoxMain.Add(carteMain4);
            listPictureBoxMain.Add(carteMain5);
            listPictureBoxMain.Add(carteMain6);
            listPictureBoxMain.Add(carteMain7);
            listPictureBoxMain.Add(carteMain8);
            listPictureBoxMain.Add(carteMain9);
            listPictureBoxMain.Add(carteMain10);
            listPictureBoxMain.Add(carteMain11);
            listPictureBoxMain.Add(carteMain12);
            listPictureBoxMain.Add(carteMain13);
            listPictureBoxMain.Add(carteMain14);
            listPictureBoxMain.Add(carteMain15);
            listPictureBoxMain.Add(carteMain16);
            listPictureBoxMain.Add(carteMain17);
            listPictureBoxMain.Add(carteMain18);
            listPictureBoxMain.Add(carteMain19);
            listPictureBoxMain.Add(carteMain20);
            listPictureBoxMain.Add(carteMain21);
            listPictureBoxMain.Add(carteMain22);
            listPictureBoxMain.Add(carteMain23);
            listPictureBoxMain.Add(carteMain24);
            listPictureBoxMain.Add(carteMain25);
            listPictureBoxMain.Add(carteMain26);
            listPictureBoxMain.Add(carteMain27);
            listPictureBoxMain.Add(carteMain28);
            listPictureBoxMain.Add(carteMain29);
            listPictureBoxMain.Add(carteMain30);
            listPictureBoxMain.Add(carteMain31);
            listPictureBoxMain.Add(carteMain32);

            //Puis on lance les méthodes correspondantes
            foreach (Joueur joueur in listeJoueurs)
            {
                joueur.MelangerLeDeck();
                joueur.Piocher(5);
            }

            //Ensuite, donne aléatoirement la main
            int main = rand.Next(0, listeJoueurs.Count);
            JoueurActuel = listeJoueurs[main];

            //Enfin, avant de commencer, on affecte nos variables globales
            deckPB = deckImage;
            deckTB = deckLabel;
            defaussePB = defausseImage;
            defausseTB = defausseLabel;
            infoActionTB = infoActionTextBox;

            //Les tours vont se succéder jusqu'à ce qu'un événement déclenche la fin de la partie
            NouveauTour();

            #endregion

        }

        private void NouveauTour()
        {
            //On commence par remettre l'Anchor à zéro de toutes les cartes, vu qu'aucune carte n'est en jeu au début du tour
            foreach (PictureBox box in listPictureBoxMain)
            {
                box.Anchor = AnchorStyles.Top;
            }
            //On remet les valeurs à 0
            JoueurActuel.AchatDispo = 1;
            JoueurActuel.ActionDispo = 1;
            JoueurActuel.MonnaieDispo = 0;
            //On affiche les infos et la main du joueur
            MAJInfos();
            JoueurActuel.MAJMain();

            //Et on met également à jour les affichages du deck et de la défausse
            if (JoueurActuel.Deck.Count > 0)
            { deckImage.ImageLocation = default; }
            else
            { deckImage.ImageLocation = ""; }
            deckLabel.Text = "Deck : " + JoueurActuel.Deck.Count.ToString();
            if (JoueurActuel.Defausse.Count > 0)
            { defausseImage.ImageLocation = JoueurActuel.Defausse[JoueurActuel.Defausse.Count - 1].Image; }
            else
            { defausseImage.ImageLocation = ""; }

            defausseLabel.Text = "Défausse : " + JoueurActuel.Defausse.Count.ToString();



        }

        private void Hover(object sender, EventArgs e)
        {
            //Ici, on va sélectionner la carte que l'utilisateur pointe et l'afficher dans le TableLayout de focus à droite

            //On commence par sélectionner la PictureBox pointée
            PictureBox selectedPictureBox = (PictureBox)sender;
            string imageCarte = selectedPictureBox.ImageLocation;
            //Puis on va boucler sur toute la liste de Pile jusqu'à trouver la PictureBox pointée
            //Une fois trouvée, notre variable globale focusPile prendra la valeur de cette Pile
            bool flag = false;
            int i = 0;
            while (!flag)
            {
                if (mapListe[i].carte.Image == imageCarte)
                {
                    flag = true;
                    focusPile = mapListe[i];
                }
                else
                { i++; }
            }
            //Infos communes à toutes les cartes
            focusNom.Text = focusPile.carte.Nom.ToUpper();
            focusPictureBox.ImageLocation = focusPile.carte.Image;
            focusPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            //Concernant le nombre de cartes restantes, cela n'a de sens que si l'utilisateur pointe uen carte dans la pile et pas dans la main
            if (selectedPictureBox.Parent == mapLayout)
            { focusNbPile.Text = focusPile.nombre.ToString() + " restantes"; }
            focusCout.ImageLocation = @"C:\Users\ohne6\Desktop\Dominion\Icones\" + focusPile.carte.Cout.ToString() + ".png";
            focusType.Text = focusPile.carte.Type.ToUpper();
            //Infos dépendant de la carte
            //On va devoir garder en mémoire les lignes utilisées
            int ligne = 1;

            //On fait un test pour chaque info possible, et si oui, on l'ajoute à notre aperçu de la carte
            if (focusPile.carte.CarteDonnee > 0)
            {
                //On crée le Textbox et on définit ses caractéristiques
                TextBox focusCarteDonnee = new TextBox();
                focusCarteDonnee.Text = "+ " + focusPile.carte.CarteDonnee.ToString() + " carte(s)";
                focusCarteDonnee.BorderStyle = BorderStyle.None;
                //Puis on l'ajoute au tableau, à la ligne actuelle
                layoutDetailFocus.Controls.Add(focusCarteDonnee, 0, ligne);
                layoutDetailFocus.SetColumnSpan(focusCarteDonnee, 2);
                //On l'ajoute aussi à notre List afin de pouvoir la supprimer au retrait de la souris
                focusDetailsList.Add(focusCarteDonnee);

                //On incrémente ensuite la ligne, vu que celle-ci est utilisée
                ligne++;
            }
            //Ainsi de suite pour chaque caractéristique de carte...
            if (focusPile.carte.ActionDonnee > 0)
            {
                TextBox focusActionDonnee = new TextBox();
                focusActionDonnee.Text = "+ " + focusPile.carte.ActionDonnee.ToString() + " action(s)";
                focusActionDonnee.BorderStyle = BorderStyle.None;
                layoutDetailFocus.Controls.Add(focusActionDonnee, 0, ligne);
                layoutDetailFocus.SetColumnSpan(focusActionDonnee, 2);
                focusDetailsList.Add(focusActionDonnee);

                ligne++;
            }

            if (focusPile.carte.AchatDonne > 0)
            {
                TextBox focusAchatDonne = new TextBox();
                focusAchatDonne.Text = "+ " + focusPile.carte.ActionDonnee.ToString() + " achat(s)";
                focusAchatDonne.BorderStyle = BorderStyle.None;
                layoutDetailFocus.Controls.Add(focusAchatDonne, 0, ligne);
                layoutDetailFocus.SetColumnSpan(focusAchatDonne, 2);
                focusDetailsList.Add(focusAchatDonne);

                ligne++;
            }

            if (focusPile.carte.MonnaieDonnee > 0)
            {
                TextBox focusMonnaieDonnee = new TextBox();
                focusMonnaieDonnee.Text = "+ " + focusPile.carte.MonnaieDonnee.ToString();
                focusMonnaieDonnee.BorderStyle = BorderStyle.None;
                focusMonnaieDonnee.Anchor = AnchorStyles.None;
                layoutDetailFocus.Controls.Add(focusMonnaieDonnee, 0, ligne);
                focusDetailsList.Add(focusMonnaieDonnee);

                //Concernant les jetons, on garde l'affichage des cartes et on doit donc afficher une image
                //Même logique que pour la TextBox
                PictureBox pictureMonnaie = new PictureBox();
                pictureMonnaie.ImageLocation = @"C:\Users\ohne6\Desktop\Dominion\Icones\monnaie.png";
                pictureMonnaie.Size = new Size(25, 25);
                pictureMonnaie.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureMonnaie.Anchor = AnchorStyles.Left;
                layoutDetailFocus.Controls.Add(pictureMonnaie, 1, ligne);
                focusIcones.Add(pictureMonnaie);

                ligne++;
            }

            if (focusPile.carte.JetonPointDonne > 0)
            {
                TextBox focusJetonPointDonne = new TextBox();
                focusJetonPointDonne.Text = "+ " + focusPile.carte.JetonPointDonne.ToString();
                focusJetonPointDonne.BorderStyle = BorderStyle.None;
                focusJetonPointDonne.Anchor = AnchorStyles.None;
                layoutDetailFocus.Controls.Add(focusJetonPointDonne, 0, ligne);
                focusDetailsList.Add(focusJetonPointDonne);

                PictureBox picturePoint = new PictureBox();
                picturePoint.ImageLocation = @"C:\Users\ohne6\Desktop\Dominion\Icones\tokenPoint.png";
                picturePoint.Size = new Size(25, 25);
                picturePoint.SizeMode = PictureBoxSizeMode.StretchImage;
                picturePoint.Anchor = AnchorStyles.Left;
                layoutDetailFocus.Controls.Add(picturePoint, 1, ligne);
                focusIcones.Add(picturePoint);

                ligne++;
            }

            if (focusPile.carte.PointDonne > 0)
            {
                TextBox focusPointDonne = new TextBox();
                focusPointDonne.Text = "+ " + focusPile.carte.PointDonne.ToString() + " point(s) de victoire";
                focusPointDonne.BorderStyle = BorderStyle.None;
                layoutDetailFocus.Controls.Add(focusPointDonne, 0, ligne);
                layoutDetailFocus.SetColumnSpan(focusPointDonne, 2);
                focusDetailsList.Add(focusPointDonne);

                ligne++;
            }

            if (focusPile.carte.EffetText != "")
            {
                TextBox focusEffet = new TextBox();
                focusEffet.Multiline = true;
                focusEffet.Text = focusPile.carte.EffetText.ToString();
                focusEffet.BorderStyle = BorderStyle.None;
                layoutDetailFocus.Controls.Add(focusEffet, 0, ligne);
                layoutDetailFocus.SetColumnSpan(focusEffet, 2);
                Size proposed = new Size(187, 400);
                Size size = TextRenderer.MeasureText(focusEffet.Text, new Font("Microsoft Sans Serif", 10F), proposed, TextFormatFlags.WordBreak);
                focusEffet.Width = size.Width;
                focusEffet.Height = size.Height + 5;
                layoutDetailFocus.RowStyles[ligne].Height = focusEffet.Height + 20;

                focusDetailsList.Add(focusEffet);

                ligne++;
            }

            //On synchronise l'apparence du texte
            foreach (TextBox detail in focusDetailsList)
            {
                detail.Font = new Font("Microsoft Sans Serif", 10F);
                detail.ReadOnly = true;
                detail.Dock = DockStyle.Fill;
            }

            //Et on redimensionne toutes les lignes... Sauf la dernière, contenant l'effet pouvant être assez long!
            for (int j = 0; j < ligne - 1; j++)
            { layoutDetailFocus.RowStyles[j].SizeType = SizeType.AutoSize; }

        }

        private void Unhover(object sender, EventArgs e)
        {
            //On vide tous les contrôles au retrait de la souris

            focusPictureBox.Image = default;
            focusNom.Text = default;
            focusCout.ImageLocation = default;
            focusType.Text = default;
            focusNbPile.Text = default;

            foreach (TextBox detail in focusDetailsList)
            {
                detail.Dispose();
            }

            foreach (PictureBox detail in focusIcones)
            {
                detail.Dispose();
            }
        }

        private void ActionMain(object sender, EventArgs e)
        {
            //On crée d'abord une variable contenant le PictureBox sur lequel l'utilisateur a cliqué
            PictureBox selectedPB = (PictureBox)sender;
            //Avant de chercher la carte, on teste si la carte n'est pas DEJA en jeu
            //Cela se traduit par la PictureBox avec un Anchor à bottom
            if (selectedPB.Anchor != AnchorStyles.Bottom)
            {
                //On va ensuite boucler jusqu'à trouver la carte correspondante dans la main, en se basant donc sur l'image de la carte
                bool flag = false;
                int i = 0;
                while (!flag)
                {
                    //La carte correspond si elle a la même image mais aussi qu'elle n'a pas déjà été jouée
                    if ((selectedPB.ImageLocation == JoueurActuel.Main[i].Image) && (!JoueurActuel.Main[i].EnJeu))
                    {
                        //Une fois trouvé, on lève le flag pour sortir de la boucle
                        flag = true;
                        //On a donc trouvé notre carte.  On va tester si cette carte est une carte Action (Contains car il y a des types multiples)
                        if (JoueurActuel.Main[i].Type.Contains("Action"))
                        {
                            //On teste donc si une action est disponible. Sinon on le dit au joueur et on sort de la boucle
                            if (JoueurActuel.ActionDispo < 1)
                            { MessageBox.Show("Vous n'avez plus d'action disponible"); }
                            else
                            {
                                //Si oui, on désincrémente le nombre d'actions disponibles et on passe la carte en jeu
                                JoueurActuel.ActionDispo--;
                                JoueurActuel.Main[i].EnJeu = true;
                                //Et on le signale
                                actionDispoTextBox.Text = JoueurActuel.ActionDispo.ToString() + " action(s)";
                                //Et on le signale graphiquement en la décalant vers le bas
                                selectedPB.Anchor = AnchorStyles.Bottom;
                                //Puis on ajoute les possibilités données
                                JoueurActuel.MonnaieDispo += JoueurActuel.Main[i].MonnaieDonnee;
                                JoueurActuel.AchatDispo += JoueurActuel.Main[i].AchatDonne;
                                JoueurActuel.ActionDispo += JoueurActuel.Main[i].ActionDonnee;
                                JoueurActuel.JetonVictoireDispo += JoueurActuel.Main[i].JetonPointDonne;
                                JoueurActuel.Piocher(JoueurActuel.Main[i].CarteDonnee);
                                //Et on lance l'effet
                                JoueurActuel.Main[i].Effet();
                                //Et on met à jour les infos
                                MAJInfos();
                            }
                        }
                    }
                    else
                    { i++; }

                }

            }

        }

        private void Achat(object sender, EventArgs e)
        {
            //Comme précédemment, on commence par récupérer la carte qui a été cliquée, donc à partir de la PictureBox
            PictureBox selectedPB = (PictureBox)sender;
            bool flag = false;
            int i = 0;
            while (!flag)
            {
                if (selectedPB == mapListe[i].pictureBox)
                {
                    flag = true;
                }
                else
                { i++; }
            }

            //Ce booléen va déterminer si oui ou non on saute l'étape de sélection de la monnaie
            bool continuer = false;

            //On vérifie ensuite si le joueur a au moins un achat dispo
            if (JoueurActuel.AchatDispo < 1)
            { MessageBox.Show("Vous n'avez plus d'achat disponible"); }
            //Si oui, on lance la procédure d'achat avec les différentes vérifications
            //Si non, l'achat va pas être validé puisque continuer == false
            else
            {
                //On teste si le joueur n'a pas déjà assez de monnaie disponible. Si oui, continuer = true et on va pouvoir valider l'achat directement
                if (JoueurActuel.MonnaieDispo >= mapListe[i].carte.Cout)
                { continuer = true; }
                //Si non, notre booléen va rester à faux et on va donc devoir demander au joueur de sélectionner des cartes Trésor
                else
                {
                    //On va appeler une fonction ouvrant notre formulaire de choix, et on doit donc lui passer la carte devant être achetée, pour avoir son coût
                    carteAacheter = mapListe[i].carte;
                    //La fonction retourne une liste de carte, on crée donc une nouvelle List
                    List<Carte> tresorsSelectionnes = JoueurActuel.ChoisirDesCartes("Achat", JoueurActuel.Main, int.MaxValue, false);
                    //Ensuite, on continue l'action seulement si le formulaire a bien été validé (si non, continuer == false et donc l'achat ne sera pas finalisé)
                    if (ChoixForm.estValide)
                    {
                        //Vérification nécessaire pour la carte Grand marché
                        bool grandMarche = true;
                        if (carteAacheter.Nom == "Grand marché")
                        {
                            foreach (Carte carte in JoueurActuel.Main)
                            {
                                if (carte.EnJeu && (carte.Nom == "Cuivre"))
                                {
                                    //La carte ne peut être achetée si des cuivres sont en jeu.
                                    //Si on en détecte un, on sort de la fonction d'achat
                                    MessageBox.Show("Vous ne pouvez pas acheter cette carte avec des cuivres.");
                                    grandMarche = false;
                                    break;
                                }
                            }
                        }
                        if (grandMarche)
                        {
                            //On vérifie d'abord si le joueur a bien sélectionné assez de monnaie
                            int monnaieSelectionnee = 0;
                            foreach (Carte carte in tresorsSelectionnes)
                            { monnaieSelectionnee += carte.MonnaieDonnee; }

                            if (monnaieSelectionnee < mapListe[i].carte.Cout)
                            {
                                //Si ce n'est pas le cas, on le dit et on arrête l'action, sans valider l'achat, sans mettre en jeu, toujours car continuer == false
                                MessageBox.Show("Vous n'avez pas assez de monnaie");
                            }
                            else
                            {

                                //Si oui, on refait une boucle pour trouver les PictureBox correspondantes et les déplacer vers le bas, ainsi que pour activer les cartes
                                foreach (Carte carte in tresorsSelectionnes)
                                {
                                    carte.EnJeu = true;
                                    //On doit chercher une image correspondante dans la main
                                    for (int j = 0, c = JoueurActuel.Main.Count; j < c; j++)
                                    {
                                        //On cherche l'image correspondante oui, mais elle ne doit pas déjà avoir été activée
                                        if ((listPictureBoxMain[j].ImageLocation == carte.Image) && (listPictureBoxMain[j].Anchor != AnchorStyles.Bottom))
                                        {
                                            listPictureBoxMain[j].Anchor = AnchorStyles.Bottom;
                                            //On utilise un break pour sortir de la boucle dès qu'une carte a été sélectionnée, afin de ne bien décaler qu'une PictureBox par carte...
                                            break;
                                        }
                                    }
                                }
                                //On ajoute le total de monnaie des cartes activées à la monnaie dispo du joueur
                                JoueurActuel.MonnaieDispo += monnaieSelectionnee;
                                //Et on peut valider le booléen pour continuer l'achat
                                continuer = true;
                            }
                        }
                    }
                    if (continuer)
                    {
                        //On crée une nouvelle instance de la carte qu'on va ajouter à la défausse
                        Carte tempCarte = (Carte)mapListe[i].carte.Clone();
                        JoueurActuel.Defausse.Add(tempCarte);
                        //Bien entendu on met à jour l'affichage de la défausse
                        defausseImage.ImageLocation = selectedPB.ImageLocation;
                        defausseLabel.Text = "Défausse : " + JoueurActuel.Defausse.Count.ToString();
                        //Et on désincrémente le nombre de cartes dans la pile
                        mapListe[i].nombre--;
                        //Et on lance la vérification pour savoir si on achève la partie
                        if (mapListe[i].nombre == 0)
                        {
                            mapListe[i].pictureBox.ImageLocation = "";
                            if ((mapListe[i].carte.Nom == "Province") || (mapListe[i].carte.Nom == "Colonie") || (nbPileVide == 3))
                            {
                                FinDePartie finDePartie = new FinDePartie();
                                finDePartie.ShowDialog();
                            }
                        }
                        //Et bien sûr on désincrémente également le nombre d'achats et la monnaie disponibles et on met à jour l'affichage
                        JoueurActuel.AchatDispo--;
                        JoueurActuel.MonnaieDispo -= mapListe[i].carte.Cout;
                        MAJInfos();
                    }
                }
            }
        }

        public void MAJInfos()
        {
            tourLabel.Text = "Tour de " + JoueurActuel.Nom;
            achatDispoTextBox.Text = JoueurActuel.AchatDispo.ToString() + " achat(s)";
            actionDispoTextBox.Text = JoueurActuel.ActionDispo.ToString() + " action(s)";
            monnaieDispoTextBox.Text = "Monnaie dispo : " + JoueurActuel.MonnaieDispo.ToString();
            int monnaietotale = 0;
            foreach (Carte carte in JoueurActuel.Main)
            {
                if (carte.Type.Contains("Trésor"))
                { monnaietotale += carte.MonnaieDonnee; }
            }
            monnaieTotaleTextBox.Text = "Monnaie totale en main : " + monnaietotale.ToString();
            jetonsTextBox.Text = JoueurActuel.JetonVictoireDispo.ToString() + " jeton(s) victoire";
        }

        private void FinDeTour(object sender, EventArgs e)
        {
            //D'abord on détermine l'index du joueur ayant la main dans la List
            int main = listeJoueurs.FindIndex(x => x.Nom == JoueurActuel.Nom);
            //Si celui-ci est le dernier, alors la main est donnée au premier de la List
            if (main == listeJoueurs.Count - 1)
            { JoueurActuel = listeJoueurs[0]; }
            //Sinon, elle est donnée au joueur suivant
            else
            { JoueurActuel = listeJoueurs[main + 1]; }

            //Ensuite, le joueur qui vient de jouer défausse sa main  et repioche 5 nouvelles cartes
            //(on le fait après avoir passé la main pour ne pas relancer la fonction MAJMain inutilement)
            for (int i = 0, c = listeJoueurs[main].Main.Count; i < c; i++)
            { listeJoueurs[main].Defausser(listeJoueurs[main].Main[0]); }

            listeJoueurs[main].Piocher(5);

            //Et on lance le nouveu tour
            NouveauTour();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Carte test = new Carte("Evèque", "", 0, "Action", "gfreg", 0, 0, 0, 0, 0, 0);
            test.Effet();
        }
    }
}

