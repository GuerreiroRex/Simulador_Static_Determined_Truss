namespace CalculoTre
{
    partial class Form1
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
            this.deLimpar = new System.Windows.Forms.Button();
            this.deTela = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // deLimpar
            // 
            this.deLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deLimpar.Location = new System.Drawing.Point(12, 12);
            this.deLimpar.Name = "deLimpar";
            this.deLimpar.Size = new System.Drawing.Size(112, 61);
            this.deLimpar.TabIndex = 0;
            this.deLimpar.Text = "Limpar";
            this.deLimpar.UseVisualStyleBackColor = true;
            this.deLimpar.Click += new System.EventHandler(this.BtLimp);
            // 
            // deTela
            // 
            this.deTela.BackColor = System.Drawing.Color.White;
            this.deTela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deTela.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.deTela.Location = new System.Drawing.Point(130, 12);
            this.deTela.Name = "deTela";
            this.deTela.Size = new System.Drawing.Size(658, 426);
            this.deTela.TabIndex = 1;
            this.deTela.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CliquePainel);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.deTela);
            this.Controls.Add(this.deLimpar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button deLimpar;
        private System.Windows.Forms.Panel deTela;
    }
}

