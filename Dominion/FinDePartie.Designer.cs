namespace Dominion
{
    partial class FinDePartie
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelVainqueur = new System.Windows.Forms.TextBox();
            this.joueur1Points = new System.Windows.Forms.TextBox();
            this.joueur2Points = new System.Windows.Forms.TextBox();
            this.joueur3Points = new System.Windows.Forms.TextBox();
            this.joueur4Points = new System.Windows.Forms.TextBox();
            this.restartButton = new System.Windows.Forms.Button();
            this.labelRestart = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelVainqueur
            // 
            this.labelVainqueur.Cursor = System.Windows.Forms.Cursors.No;
            this.labelVainqueur.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVainqueur.Location = new System.Drawing.Point(216, 41);
            this.labelVainqueur.Name = "labelVainqueur";
            this.labelVainqueur.ReadOnly = true;
            this.labelVainqueur.Size = new System.Drawing.Size(381, 41);
            this.labelVainqueur.TabIndex = 0;
            this.labelVainqueur.Text = "Joueur gagne la partie!";
            this.labelVainqueur.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // joueur1Points
            // 
            this.joueur1Points.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.joueur1Points.Cursor = System.Windows.Forms.Cursors.No;
            this.joueur1Points.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joueur1Points.Location = new System.Drawing.Point(197, 132);
            this.joueur1Points.Name = "joueur1Points";
            this.joueur1Points.ReadOnly = true;
            this.joueur1Points.Size = new System.Drawing.Size(111, 22);
            this.joueur1Points.TabIndex = 1;
            this.joueur1Points.Text = "Joueur 1 : 50";
            this.joueur1Points.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // joueur2Points
            // 
            this.joueur2Points.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.joueur2Points.Cursor = System.Windows.Forms.Cursors.No;
            this.joueur2Points.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joueur2Points.Location = new System.Drawing.Point(467, 132);
            this.joueur2Points.Name = "joueur2Points";
            this.joueur2Points.ReadOnly = true;
            this.joueur2Points.Size = new System.Drawing.Size(111, 22);
            this.joueur2Points.TabIndex = 2;
            this.joueur2Points.Text = "Joueur 1 : 50";
            this.joueur2Points.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // joueur3Points
            // 
            this.joueur3Points.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.joueur3Points.Cursor = System.Windows.Forms.Cursors.No;
            this.joueur3Points.Enabled = false;
            this.joueur3Points.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joueur3Points.Location = new System.Drawing.Point(197, 195);
            this.joueur3Points.Name = "joueur3Points";
            this.joueur3Points.ReadOnly = true;
            this.joueur3Points.Size = new System.Drawing.Size(111, 22);
            this.joueur3Points.TabIndex = 3;
            this.joueur3Points.Text = "Joueur 1 : 50";
            this.joueur3Points.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.joueur3Points.Visible = false;
            // 
            // joueur4Points
            // 
            this.joueur4Points.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.joueur4Points.Cursor = System.Windows.Forms.Cursors.No;
            this.joueur4Points.Enabled = false;
            this.joueur4Points.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.joueur4Points.Location = new System.Drawing.Point(467, 195);
            this.joueur4Points.Name = "joueur4Points";
            this.joueur4Points.ReadOnly = true;
            this.joueur4Points.Size = new System.Drawing.Size(111, 22);
            this.joueur4Points.TabIndex = 4;
            this.joueur4Points.Text = "Joueur 1 : 50";
            this.joueur4Points.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.joueur4Points.Visible = false;
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(352, 319);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(75, 42);
            this.restartButton.TabIndex = 5;
            this.restartButton.Text = "Oui";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.RestartButton_Click);
            // 
            // labelRestart
            // 
            this.labelRestart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelRestart.Cursor = System.Windows.Forms.Cursors.No;
            this.labelRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRestart.Location = new System.Drawing.Point(330, 273);
            this.labelRestart.Multiline = true;
            this.labelRestart.Name = "labelRestart";
            this.labelRestart.ReadOnly = true;
            this.labelRestart.Size = new System.Drawing.Size(127, 20);
            this.labelRestart.TabIndex = 6;
            this.labelRestart.Text = "Voulez-vous rejouer?";
            // 
            // FinDePartie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelRestart);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.joueur4Points);
            this.Controls.Add(this.joueur3Points);
            this.Controls.Add(this.joueur2Points);
            this.Controls.Add(this.joueur1Points);
            this.Controls.Add(this.labelVainqueur);
            this.Name = "FinDePartie";
            this.Text = "FinDePartie";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox labelVainqueur;
        private System.Windows.Forms.TextBox joueur1Points;
        private System.Windows.Forms.TextBox joueur2Points;
        private System.Windows.Forms.TextBox joueur3Points;
        private System.Windows.Forms.TextBox joueur4Points;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.TextBox labelRestart;
    }
}