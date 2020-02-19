namespace Dominion
{
    partial class LancementForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LancementForm));
            this.nomJoueur1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nomJoueur2 = new System.Windows.Forms.TextBox();
            this.nomJoueur3 = new System.Windows.Forms.TextBox();
            this.nomJoueur4 = new System.Windows.Forms.TextBox();
            this.checkBoxJoueur3 = new System.Windows.Forms.CheckBox();
            this.checkBoxJoueur4 = new System.Windows.Forms.CheckBox();
            this.goButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nomJoueur1
            // 
            this.nomJoueur1.Location = new System.Drawing.Point(276, 94);
            this.nomJoueur1.MaxLength = 10;
            this.nomJoueur1.Name = "nomJoueur1";
            this.nomJoueur1.Size = new System.Drawing.Size(100, 20);
            this.nomJoueur1.TabIndex = 1;
            this.nomJoueur1.Text = "Joueur 1";
            this.nomJoueur1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nomJoueur1.Click += new System.EventHandler(this.Focus);
            this.nomJoueur1.Enter += new System.EventHandler(this.Focus);
            this.nomJoueur1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Lancer);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(225, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entrez vos noms";
            // 
            // nomJoueur2
            // 
            this.nomJoueur2.Location = new System.Drawing.Point(276, 144);
            this.nomJoueur2.MaxLength = 10;
            this.nomJoueur2.Name = "nomJoueur2";
            this.nomJoueur2.Size = new System.Drawing.Size(100, 20);
            this.nomJoueur2.TabIndex = 2;
            this.nomJoueur2.Text = "Joueur 2";
            this.nomJoueur2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nomJoueur2.Click += new System.EventHandler(this.Focus);
            this.nomJoueur2.Enter += new System.EventHandler(this.Focus);
            this.nomJoueur2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Lancer);
            // 
            // nomJoueur3
            // 
            this.nomJoueur3.Enabled = false;
            this.nomJoueur3.Location = new System.Drawing.Point(276, 197);
            this.nomJoueur3.MaxLength = 10;
            this.nomJoueur3.Name = "nomJoueur3";
            this.nomJoueur3.Size = new System.Drawing.Size(100, 20);
            this.nomJoueur3.TabIndex = 3;
            this.nomJoueur3.Text = "Joueur 3";
            this.nomJoueur3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nomJoueur3.Click += new System.EventHandler(this.Focus);
            this.nomJoueur3.Enter += new System.EventHandler(this.Focus);
            this.nomJoueur3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Lancer);
            // 
            // nomJoueur4
            // 
            this.nomJoueur4.Enabled = false;
            this.nomJoueur4.Location = new System.Drawing.Point(276, 254);
            this.nomJoueur4.MaxLength = 10;
            this.nomJoueur4.Name = "nomJoueur4";
            this.nomJoueur4.Size = new System.Drawing.Size(100, 20);
            this.nomJoueur4.TabIndex = 4;
            this.nomJoueur4.Text = "Joueur 4";
            this.nomJoueur4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nomJoueur4.Click += new System.EventHandler(this.Focus);
            this.nomJoueur4.Enter += new System.EventHandler(this.Focus);
            this.nomJoueur4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Lancer);
            // 
            // checkBoxJoueur3
            // 
            this.checkBoxJoueur3.AutoSize = true;
            this.checkBoxJoueur3.Location = new System.Drawing.Point(396, 200);
            this.checkBoxJoueur3.Name = "checkBoxJoueur3";
            this.checkBoxJoueur3.Size = new System.Drawing.Size(15, 14);
            this.checkBoxJoueur3.TabIndex = 5;
            this.checkBoxJoueur3.UseVisualStyleBackColor = true;
            this.checkBoxJoueur3.CheckedChanged += new System.EventHandler(this.CheckBoxJoueur3_CheckedChanged);
            // 
            // checkBoxJoueur4
            // 
            this.checkBoxJoueur4.AutoSize = true;
            this.checkBoxJoueur4.Enabled = false;
            this.checkBoxJoueur4.Location = new System.Drawing.Point(396, 257);
            this.checkBoxJoueur4.Name = "checkBoxJoueur4";
            this.checkBoxJoueur4.Size = new System.Drawing.Size(15, 14);
            this.checkBoxJoueur4.TabIndex = 6;
            this.checkBoxJoueur4.UseVisualStyleBackColor = true;
            this.checkBoxJoueur4.CheckedChanged += new System.EventHandler(this.CheckBoxJoueur4_CheckedChanged);
            // 
            // goButton
            // 
            this.goButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goButton.Location = new System.Drawing.Point(261, 307);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(131, 48);
            this.goButton.TabIndex = 7;
            this.goButton.Text = "Lancer la partie";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.GoButton_Click);
            this.goButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Lancer);
            // 
            // LancementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 450);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.checkBoxJoueur4);
            this.Controls.Add(this.checkBoxJoueur3);
            this.Controls.Add(this.nomJoueur4);
            this.Controls.Add(this.nomJoueur3);
            this.Controls.Add(this.nomJoueur2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nomJoueur1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LancementForm";
            this.Text = "Choisissez les joueurs";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nomJoueur1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nomJoueur2;
        private System.Windows.Forms.TextBox nomJoueur3;
        private System.Windows.Forms.TextBox nomJoueur4;
        private System.Windows.Forms.CheckBox checkBoxJoueur3;
        private System.Windows.Forms.CheckBox checkBoxJoueur4;
        private System.Windows.Forms.Button goButton;
    }
}

