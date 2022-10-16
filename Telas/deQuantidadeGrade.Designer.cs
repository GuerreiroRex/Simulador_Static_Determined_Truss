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
            this.deValorHorizontal = new System.Windows.Forms.NumericUpDown();
            this.deValorVertical = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.deVertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deHorizontal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deValorHorizontal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deValorVertical)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resolução";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Vertical";
            // 
            // deVertical
            // 
            this.deVertical.Location = new System.Drawing.Point(88, 23);
            this.deVertical.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.deVertical.Name = "deVertical";
            this.deVertical.Size = new System.Drawing.Size(60, 20);
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
            this.deHorizontal.Location = new System.Drawing.Point(154, 23);
            this.deHorizontal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.deHorizontal.Name = "deHorizontal";
            this.deHorizontal.Size = new System.Drawing.Size(60, 20);
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
            this.label3.Location = new System.Drawing.Point(151, 7);
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
            // deValorHorizontal
            // 
            this.deValorHorizontal.Location = new System.Drawing.Point(154, 57);
            this.deValorHorizontal.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.deValorHorizontal.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.deValorHorizontal.Name = "deValorHorizontal";
            this.deValorHorizontal.Size = new System.Drawing.Size(60, 20);
            this.deValorHorizontal.TabIndex = 8;
            this.deValorHorizontal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.deValorHorizontal.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // deValorVertical
            // 
            this.deValorVertical.Location = new System.Drawing.Point(88, 57);
            this.deValorVertical.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.deValorVertical.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.deValorVertical.Name = "deValorVertical";
            this.deValorVertical.Size = new System.Drawing.Size(60, 20);
            this.deValorVertical.TabIndex = 7;
            this.deValorVertical.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.deValorVertical.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Valor Máximo";
            // 
            // deQuantidadeGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 148);
            this.Controls.Add(this.deValorHorizontal);
            this.Controls.Add(this.deValorVertical);
            this.Controls.Add(this.label4);
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
            ((System.ComponentModel.ISupportInitialize)(this.deValorHorizontal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deValorVertical)).EndInit();
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
        private System.Windows.Forms.NumericUpDown deValorHorizontal;
        private System.Windows.Forms.NumericUpDown deValorVertical;
        private System.Windows.Forms.Label label4;
    }
}