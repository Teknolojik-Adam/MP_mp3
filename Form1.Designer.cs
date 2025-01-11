
namespace Mehmet_mp3
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.onceki_sarki = new System.Windows.Forms.Button();
            this.sonraki_sarki = new System.Windows.Forms.Button();
            this.oynat = new System.Windows.Forms.Button();
            this.duraklat = new System.Windows.Forms.Button();
            this.durdur = new System.Windows.Forms.Button();
            this.Lgoritma = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.delete = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.file_nameedit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // onceki_sarki
            // 
            this.onceki_sarki.Location = new System.Drawing.Point(12, 335);
            this.onceki_sarki.Name = "onceki_sarki";
            this.onceki_sarki.Size = new System.Drawing.Size(78, 23);
            this.onceki_sarki.TabIndex = 0;
            this.onceki_sarki.Text = "Önceki Şarki";
            this.onceki_sarki.UseVisualStyleBackColor = true;
            this.onceki_sarki.Click += new System.EventHandler(this.onceki_sarki_Click);
            // 
            // sonraki_sarki
            // 
            this.sonraki_sarki.Location = new System.Drawing.Point(93, 334);
            this.sonraki_sarki.Name = "sonraki_sarki";
            this.sonraki_sarki.Size = new System.Drawing.Size(79, 23);
            this.sonraki_sarki.TabIndex = 1;
            this.sonraki_sarki.Text = "Sonraki Şarki";
            this.sonraki_sarki.UseVisualStyleBackColor = true;
            this.sonraki_sarki.Click += new System.EventHandler(this.sonraki_sarki_Click);
            // 
            // oynat
            // 
            this.oynat.Location = new System.Drawing.Point(174, 334);
            this.oynat.Name = "oynat";
            this.oynat.Size = new System.Drawing.Size(75, 23);
            this.oynat.TabIndex = 2;
            this.oynat.Text = "Oynat";
            this.oynat.UseVisualStyleBackColor = true;
            this.oynat.Click += new System.EventHandler(this.oynat_Click);
            // 
            // duraklat
            // 
            this.duraklat.Location = new System.Drawing.Point(255, 334);
            this.duraklat.Name = "duraklat";
            this.duraklat.Size = new System.Drawing.Size(75, 23);
            this.duraklat.TabIndex = 3;
            this.duraklat.Text = "Duraklat";
            this.duraklat.UseVisualStyleBackColor = true;
            this.duraklat.Click += new System.EventHandler(this.duraklat_Click);
            // 
            // durdur
            // 
            this.durdur.Location = new System.Drawing.Point(336, 334);
            this.durdur.Name = "durdur";
            this.durdur.Size = new System.Drawing.Size(75, 23);
            this.durdur.TabIndex = 4;
            this.durdur.Text = "Durdur";
            this.durdur.UseVisualStyleBackColor = true;
            this.durdur.Click += new System.EventHandler(this.durdur_Click);
            // 
            // Lgoritma
            // 
            this.Lgoritma.Location = new System.Drawing.Point(498, 170);
            this.Lgoritma.Name = "Lgoritma";
            this.Lgoritma.Size = new System.Drawing.Size(162, 23);
            this.Lgoritma.TabIndex = 5;
            this.Lgoritma.Text = "AI Müzik önerisi";
            this.Lgoritma.UseVisualStyleBackColor = true;
            this.Lgoritma.Click += new System.EventHandler(this.Lgoritma_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(459, 51);
            this.trackBar1.Maximum = 150;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 276);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Value = 20;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 50);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(441, 277);
            this.listBox1.TabIndex = 7;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(-2, 0);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(662, 45);
            this.trackBar2.TabIndex = 8;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(417, 335);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(74, 23);
            this.delete.TabIndex = 9;
            this.delete.Text = "Listeden Sil";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 120;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(498, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(162, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(498, 143);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(162, 21);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // file_nameedit
            // 
            this.file_nameedit.Location = new System.Drawing.Point(498, 199);
            this.file_nameedit.Name = "file_nameedit";
            this.file_nameedit.Size = new System.Drawing.Size(162, 23);
            this.file_nameedit.TabIndex = 12;
            this.file_nameedit.Text = "Müziğin adini değiştir";
            this.file_nameedit.UseVisualStyleBackColor = true;
            this.file_nameedit.Click += new System.EventHandler(this.file_nameedit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 373);
            this.Controls.Add(this.file_nameedit);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.Lgoritma);
            this.Controls.Add(this.durdur);
            this.Controls.Add(this.duraklat);
            this.Controls.Add(this.oynat);
            this.Controls.Add(this.sonraki_sarki);
            this.Controls.Add(this.onceki_sarki);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button onceki_sarki;
        private System.Windows.Forms.Button sonraki_sarki;
        private System.Windows.Forms.Button oynat;
        private System.Windows.Forms.Button duraklat;
        private System.Windows.Forms.Button durdur;
        private System.Windows.Forms.Button Lgoritma;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button file_nameedit;
    }
}

