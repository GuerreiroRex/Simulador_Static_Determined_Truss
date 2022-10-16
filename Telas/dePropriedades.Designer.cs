namespace CalculoTre
{
    partial class dePropriedades
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
            this.deProjecao = new System.Windows.Forms.Panel();
            this.deTitulo = new System.Windows.Forms.Label();
            this.deFechar = new System.Windows.Forms.Button();
            this.deMenu = new System.Windows.Forms.Panel();
            this.deValorX = new System.Windows.Forms.NumericUpDown();
            this.deValorY = new System.Windows.Forms.NumericUpDown();
            this.deMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deValorX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deValorY)).BeginInit();
            this.SuspendLayout();
            // 
            // deProjecao
            // 
            this.deProjecao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deProjecao.BackColor = System.Drawing.Color.White;
            this.deProjecao.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deProjecao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.deProjecao.Location = new System.Drawing.Point(108, 12);
            this.deProjecao.Name = "deProjecao";
            this.deProjecao.Size = new System.Drawing.Size(458, 311);
            this.deProjecao.TabIndex = 2;
            // 
            // deTitulo
            // 
            this.deTitulo.AutoSize = true;
            this.deTitulo.Location = new System.Drawing.Point(3, 0);
            this.deTitulo.Name = "deTitulo";
            this.deTitulo.Size = new System.Drawing.Size(35, 13);
            this.deTitulo.TabIndex = 0;
            this.deTitulo.Text = "Nome";
            // 
            // deFechar
            // 
            this.deFechar.Location = new System.Drawing.Point(12, 280);
            this.deFechar.Name = "deFechar";
            this.deFechar.Size = new System.Drawing.Size(90, 43);
            this.deFechar.TabIndex = 0;
            this.deFechar.Text = "Fechar";
            this.deFechar.UseVisualStyleBackColor = true;
            this.deFechar.Click += new System.EventHandler(this.deFechar_Click);
            // 
            // deMenu
            // 
            this.deMenu.Controls.Add(this.deTitulo);
            this.deMenu.Location = new System.Drawing.Point(12, 12);
            this.deMenu.Name = "deMenu";
            this.deMenu.Size = new System.Drawing.Size(90, 213);
            this.deMenu.TabIndex = 0;
            // 
            // deValorX
            // 
            this.deValorX.Location = new System.Drawing.Point(12, 231);
            this.deValorX.Name = "deValorX";
            this.deValorX.Size = new System.Drawing.Size(90, 20);
            this.deValorX.TabIndex = 0;
            // 
            // deValorY
            // 
            this.deValorY.Location = new System.Drawing.Point(12, 254);
            this.deValorY.Name = "deValorY";
            this.deValorY.Size = new System.Drawing.Size(90, 20);
            this.deValorY.TabIndex = 3;
            // 
            // dePropriedades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 335);
            this.Controls.Add(this.deValorX);
            this.Controls.Add(this.deValorY);
            this.Controls.Add(this.deMenu);
            this.Controls.Add(this.deFechar);
            this.Controls.Add(this.deProjecao);
            this.Name = "dePropriedades";
            this.Text = "Propriedades da Barra";
            this.deMenu.ResumeLayout(false);
            this.deMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deValorX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deValorY)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel deProjecao;
        private System.Windows.Forms.Label deTitulo;
        private System.Windows.Forms.Button deFechar;
        private System.Windows.Forms.Panel deMenu;
        private System.Windows.Forms.NumericUpDown deValorX;
        private System.Windows.Forms.NumericUpDown deValorY;
    }
}