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
            this.deFechar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // deProjecao
            // 
            this.deProjecao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deProjecao.BackColor = System.Drawing.Color.White;
            this.deProjecao.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deProjecao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.deProjecao.Location = new System.Drawing.Point(110, 10);
            this.deProjecao.Name = "deProjecao";
            this.deProjecao.Size = new System.Drawing.Size(456, 313);
            this.deProjecao.TabIndex = 2;
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
            // dePropriedades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 335);
            this.Controls.Add(this.deFechar);
            this.Controls.Add(this.deProjecao);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dePropriedades";
            this.Text = "Propriedades";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel deProjecao;
        private System.Windows.Forms.Button deFechar;
    }
}