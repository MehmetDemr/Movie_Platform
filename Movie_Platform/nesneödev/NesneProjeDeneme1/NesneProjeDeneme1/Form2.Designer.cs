namespace NesneProjeDeneme1
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.dgvFilmOneriListe = new System.Windows.Forms.DataGridView();
            this.btnProfilimeDon = new System.Windows.Forms.Button();
            this.btnFilmListeGoruntule = new System.Windows.Forms.Button();
            this.btnDegerlendirmeListe = new System.Windows.Forms.Button();
            this.btnYonetmeneGoreListe = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCikisTarihi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilmOneriListe)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFilmOneriListe
            // 
            this.dgvFilmOneriListe.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvFilmOneriListe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilmOneriListe.Location = new System.Drawing.Point(476, 206);
            this.dgvFilmOneriListe.Margin = new System.Windows.Forms.Padding(2);
            this.dgvFilmOneriListe.Name = "dgvFilmOneriListe";
            this.dgvFilmOneriListe.RowHeadersWidth = 51;
            this.dgvFilmOneriListe.RowTemplate.Height = 24;
            this.dgvFilmOneriListe.Size = new System.Drawing.Size(469, 287);
            this.dgvFilmOneriListe.TabIndex = 0;
            // 
            // btnProfilimeDon
            // 
            this.btnProfilimeDon.Location = new System.Drawing.Point(1120, 28);
            this.btnProfilimeDon.Margin = new System.Windows.Forms.Padding(2);
            this.btnProfilimeDon.Name = "btnProfilimeDon";
            this.btnProfilimeDon.Size = new System.Drawing.Size(258, 53);
            this.btnProfilimeDon.TabIndex = 1;
            this.btnProfilimeDon.Text = "Profilim";
            this.btnProfilimeDon.UseVisualStyleBackColor = true;
            this.btnProfilimeDon.Click += new System.EventHandler(this.btnProfilimeDon_Click);
            // 
            // btnFilmListeGoruntule
            // 
            this.btnFilmListeGoruntule.Location = new System.Drawing.Point(349, 206);
            this.btnFilmListeGoruntule.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilmListeGoruntule.Name = "btnFilmListeGoruntule";
            this.btnFilmListeGoruntule.Size = new System.Drawing.Size(122, 287);
            this.btnFilmListeGoruntule.TabIndex = 2;
            this.btnFilmListeGoruntule.Text = "Görüntüle";
            this.btnFilmListeGoruntule.UseVisualStyleBackColor = true;
            this.btnFilmListeGoruntule.Click += new System.EventHandler(this.btnFilmListeGoruntule_Click);
            // 
            // btnDegerlendirmeListe
            // 
            this.btnDegerlendirmeListe.Location = new System.Drawing.Point(654, 652);
            this.btnDegerlendirmeListe.Margin = new System.Windows.Forms.Padding(2);
            this.btnDegerlendirmeListe.Name = "btnDegerlendirmeListe";
            this.btnDegerlendirmeListe.Size = new System.Drawing.Size(122, 84);
            this.btnDegerlendirmeListe.TabIndex = 3;
            this.btnDegerlendirmeListe.Text = "Değerlendirmeye göre";
            this.btnDegerlendirmeListe.UseVisualStyleBackColor = true;
            this.btnDegerlendirmeListe.Click += new System.EventHandler(this.btnDegerlendirmeyeGore_Click);
            // 
            // btnYonetmeneGoreListe
            // 
            this.btnYonetmeneGoreListe.Location = new System.Drawing.Point(823, 652);
            this.btnYonetmeneGoreListe.Margin = new System.Windows.Forms.Padding(2);
            this.btnYonetmeneGoreListe.Name = "btnYonetmeneGoreListe";
            this.btnYonetmeneGoreListe.Size = new System.Drawing.Size(122, 84);
            this.btnYonetmeneGoreListe.TabIndex = 4;
            this.btnYonetmeneGoreListe.Text = "Yönetmene göre";
            this.btnYonetmeneGoreListe.UseVisualStyleBackColor = true;
            this.btnYonetmeneGoreListe.Click += new System.EventHandler(this.btnYonetmeneGoreListe_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F);
            this.label1.Location = new System.Drawing.Point(463, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 73);
            this.label1.TabIndex = 5;
            this.label1.Text = "Filmleri keşfedin!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.label2.Location = new System.Drawing.Point(316, 507);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(793, 116);
            this.label2.TabIndex = 6;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // btnCikisTarihi
            // 
            this.btnCikisTarihi.Location = new System.Drawing.Point(476, 652);
            this.btnCikisTarihi.Margin = new System.Windows.Forms.Padding(2);
            this.btnCikisTarihi.Name = "btnCikisTarihi";
            this.btnCikisTarihi.Size = new System.Drawing.Size(122, 84);
            this.btnCikisTarihi.TabIndex = 7;
            this.btnCikisTarihi.Text = "Çıkış tarihi";
            this.btnCikisTarihi.UseVisualStyleBackColor = true;
            this.btnCikisTarihi.Click += new System.EventHandler(this.btnCikisTarihi_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1426, 839);
            this.Controls.Add(this.btnCikisTarihi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnYonetmeneGoreListe);
            this.Controls.Add(this.btnDegerlendirmeListe);
            this.Controls.Add(this.btnFilmListeGoruntule);
            this.Controls.Add(this.btnProfilimeDon);
            this.Controls.Add(this.dgvFilmOneriListe);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilmOneriListe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFilmOneriListe;
        private System.Windows.Forms.Button btnProfilimeDon;
        private System.Windows.Forms.Button btnFilmListeGoruntule;
        private System.Windows.Forms.Button btnDegerlendirmeListe;
        private System.Windows.Forms.Button btnYonetmeneGoreListe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCikisTarihi;
    }
}