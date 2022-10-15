namespace CalculoTre.Telas
{
    partial class deQuantidadeGrade
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.deVertical = new System.Windows.Forms.NumericUpDown();
            this.deHorizontal = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.deConfirmar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.deVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHorizontal)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resolução";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Vertical";
            // 
            // deVertical
            // 
            this.deVertical.Location = new System.Drawing.Point(79, 25);
            this.deVertical.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.deVertical.Name = "deVertical";
            this.deVertical.Size = new System.Drawing.Size(39, 20);
            this.deVertical.TabIndex = 2;
            this.deVertical.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.deVertical.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // deHorizontal
            // 
            this.deHorizontal.Location = new System.Drawing.Point(136, 25);
            this.deHorizontal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.deHorizontal.Name = "deHorizontal";
            this.deHorizontal.Size = new System.Drawing.Size(39, 20);
            this.deHorizontal.TabIndex = 4;
            this.deHorizontal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.deHorizontal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Horizontal";
            // 
            // deConfirmar
            // 
            this.deConfirmar.Location = new System.Drawing.Point(151, 111);
            this.deConfirmar.Name = "deConfirmar";
            this.deConfirmar.Size = new System.Drawing.Size(80, 25);
            this.deConfirmar.TabIndex = 5;
            this.deConfirmar.Text = "Confirmar";
            this.deConfirmar.UseVisualStyleBackColor = true;
            this.deConfirmar.Click += new System.EventHandler(this.deConfirmar_Click);
            // 
            // deQuantidadeGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 148);
            this.Controls.Add(this.deConfirmar);
            this.Controls.Add(this.deHorizontal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.deVertical);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "deQuantidadeGrade";
            this.Text = "deQuantidadeGrade";
            ((System.ComponentModel.ISupportInitialize)(this.deVertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHorizontal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown deVertical;
        private System.Windows.Forms.NumericUpDown deHorizontal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button deConfirmar;
    }
}