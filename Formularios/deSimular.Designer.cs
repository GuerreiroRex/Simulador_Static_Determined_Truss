namespace CalculoTre
{
    partial class deSimular
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
            this.deTipo = new System.Windows.Forms.ComboBox();
            this.deObjeto = new System.Windows.Forms.ComboBox();
            this.deProp = new System.Windows.Forms.Button();
            this.deConfigurarTela = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deLimpar
            // 
            this.deLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deLimpar.Location = new System.Drawing.Point(18, 38);
            this.deLimpar.Name = "deLimpar";
            this.deLimpar.Size = new System.Drawing.Size(112, 61);
            this.deLimpar.TabIndex = 0;
            this.deLimpar.Text = "Limpar";
            this.deLimpar.UseVisualStyleBackColor = true;
            this.deLimpar.Click += new System.EventHandler(this.deLimpar_Click);
            // 
            // deTela
            // 
            this.deTela.BackColor = System.Drawing.Color.White;
            this.deTela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deTela.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.deTela.Location = new System.Drawing.Point(136, 38);
            this.deTela.Name = "deTela";
            this.deTela.Size = new System.Drawing.Size(753, 451);
            this.deTela.TabIndex = 1;
            this.deTela.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CliquePainel);
            // 
            // deTipo
            // 
            this.deTipo.FormattingEnabled = true;
            this.deTipo.Location = new System.Drawing.Point(18, 105);
            this.deTipo.Name = "deTipo";
            this.deTipo.Size = new System.Drawing.Size(112, 21);
            this.deTipo.TabIndex = 0;
            this.deTipo.SelectedIndexChanged += new System.EventHandler(this.AtualizarListaObjetos);
            // 
            // deObjeto
            // 
            this.deObjeto.FormattingEnabled = true;
            this.deObjeto.Location = new System.Drawing.Point(18, 132);
            this.deObjeto.Name = "deObjeto";
            this.deObjeto.Size = new System.Drawing.Size(112, 21);
            this.deObjeto.TabIndex = 2;
            this.deObjeto.Click += new System.EventHandler(this.AtualizarListaObjetos);
            // 
            // deProp
            // 
            this.deProp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deProp.Location = new System.Drawing.Point(18, 159);
            this.deProp.Name = "deProp";
            this.deProp.Size = new System.Drawing.Size(112, 61);
            this.deProp.TabIndex = 3;
            this.deProp.Text = "Propriedades";
            this.deProp.UseVisualStyleBackColor = true;
            this.deProp.Click += new System.EventHandler(this.deProp_Click);
            // 
            // deConfigurarTela
            // 
            this.deConfigurarTela.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deConfigurarTela.Location = new System.Drawing.Point(735, 3);
            this.deConfigurarTela.Name = "deConfigurarTela";
            this.deConfigurarTela.Size = new System.Drawing.Size(154, 29);
            this.deConfigurarTela.TabIndex = 4;
            this.deConfigurarTela.Text = "Configurações de Tela";
            this.deConfigurarTela.UseVisualStyleBackColor = true;
            this.deConfigurarTela.Click += new System.EventHandler(this.deConfigurarTela_Click);
            // 
            // deSimular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 501);
            this.Controls.Add(this.deConfigurarTela);
            this.Controls.Add(this.deProp);
            this.Controls.Add(this.deObjeto);
            this.Controls.Add(this.deTipo);
            this.Controls.Add(this.deTela);
            this.Controls.Add(this.deLimpar);
            this.Name = "deSimular";
            this.Text = "Simulação de Treliça";
            this.SizeChanged += new System.EventHandler(this.deSimular_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button deLimpar;
        private System.Windows.Forms.Panel deTela;
        private System.Windows.Forms.ComboBox deTipo;
        private System.Windows.Forms.ComboBox deObjeto;
        private System.Windows.Forms.Button deProp;
        private System.Windows.Forms.Button deConfigurarTela;
    }
}

