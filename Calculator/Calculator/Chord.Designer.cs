
namespace Calculator
{
    partial class Chord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chord));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblVResult = new System.Windows.Forms.Label();
            this.lblAResult = new System.Windows.Forms.Label();
            this.lblRResult = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtV = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtA = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtR = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtH = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtC = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblHResult = new System.Windows.Forms.Label();
            this.lblCResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(206, 162);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // lblVResult
            // 
            this.lblVResult.AutoSize = true;
            this.lblVResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVResult.Location = new System.Drawing.Point(81, 492);
            this.lblVResult.Name = "lblVResult";
            this.lblVResult.Size = new System.Drawing.Size(31, 20);
            this.lblVResult.TabIndex = 25;
            this.lblVResult.Text = "θ°: ";
            // 
            // lblAResult
            // 
            this.lblAResult.AutoSize = true;
            this.lblAResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAResult.Location = new System.Drawing.Point(83, 408);
            this.lblAResult.Name = "lblAResult";
            this.lblAResult.Size = new System.Drawing.Size(28, 20);
            this.lblAResult.TabIndex = 24;
            this.lblAResult.Text = "A: ";
            // 
            // lblRResult
            // 
            this.lblRResult.AutoSize = true;
            this.lblRResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRResult.Location = new System.Drawing.Point(83, 382);
            this.lblRResult.Name = "lblRResult";
            this.lblRResult.Size = new System.Drawing.Size(29, 20);
            this.lblRResult.TabIndex = 23;
            this.lblRResult.Text = "R: ";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LimeGreen;
            this.button1.Location = new System.Drawing.Point(32, 327);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 32);
            this.button1.TabIndex = 22;
            this.button1.Text = "Get result";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(83, 362);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "Results:";
            // 
            // txtV
            // 
            this.txtV.Location = new System.Drawing.Point(54, 301);
            this.txtV.Name = "txtV";
            this.txtV.Size = new System.Drawing.Size(142, 20);
            this.txtV.TabIndex = 20;
            this.txtV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "θ°";
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(54, 223);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(142, 20);
            this.txtA.TabIndex = 18;
            this.txtA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "A";
            // 
            // txtR
            // 
            this.txtR.Location = new System.Drawing.Point(54, 197);
            this.txtR.Name = "txtR";
            this.txtR.Size = new System.Drawing.Size(142, 20);
            this.txtR.TabIndex = 16;
            this.txtR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "R";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Two fields are required";
            // 
            // txtH
            // 
            this.txtH.Location = new System.Drawing.Point(54, 275);
            this.txtH.Name = "txtH";
            this.txtH.Size = new System.Drawing.Size(142, 20);
            this.txtH.TabIndex = 30;
            this.txtH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(27, 275);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "H";
            // 
            // txtC
            // 
            this.txtC.Location = new System.Drawing.Point(54, 249);
            this.txtC.Name = "txtC";
            this.txtC.Size = new System.Drawing.Size(142, 20);
            this.txtC.TabIndex = 28;
            this.txtC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(27, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 20);
            this.label7.TabIndex = 27;
            this.label7.Text = "C";
            // 
            // lblHResult
            // 
            this.lblHResult.AutoSize = true;
            this.lblHResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHResult.Location = new System.Drawing.Point(81, 463);
            this.lblHResult.Name = "lblHResult";
            this.lblHResult.Size = new System.Drawing.Size(28, 20);
            this.lblHResult.TabIndex = 32;
            this.lblHResult.Text = "A: ";
            // 
            // lblCResult
            // 
            this.lblCResult.AutoSize = true;
            this.lblCResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCResult.Location = new System.Drawing.Point(81, 437);
            this.lblCResult.Name = "lblCResult";
            this.lblCResult.Size = new System.Drawing.Size(28, 20);
            this.lblCResult.TabIndex = 31;
            this.lblCResult.Text = "C: ";
            // 
            // Chord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 512);
            this.Controls.Add(this.lblHResult);
            this.Controls.Add(this.lblCResult);
            this.Controls.Add(this.txtH);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtC);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblVResult);
            this.Controls.Add(this.lblAResult);
            this.Controls.Add(this.lblRResult);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtV);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtR);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Chord";
            this.Text = "Chord";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblVResult;
        private System.Windows.Forms.Label lblAResult;
        private System.Windows.Forms.Label lblRResult;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtR;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblHResult;
        private System.Windows.Forms.Label lblCResult;
    }
}