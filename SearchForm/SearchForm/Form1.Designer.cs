namespace SearchForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.startButton = new System.Windows.Forms.Button();
            this.cell1 = new System.Windows.Forms.Label();
            this.cell2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 90);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(776, 348);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(713, 61);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // cell1
            // 
            this.cell1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cell1.Location = new System.Drawing.Point(20, 100);
            this.cell1.Name = "cell1";
            this.cell1.Size = new System.Drawing.Size(30, 20);
            this.cell1.TabIndex = 2;
            this.cell1.Text = "000";
            this.cell1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cell1.Visible = false;
            // 
            // cell2
            // 
            this.cell2.BackColor = System.Drawing.SystemColors.Control;
            this.cell2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cell2.Cursor = System.Windows.Forms.Cursors.Default;
            this.cell2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell2.Location = new System.Drawing.Point(390, 215);
            this.cell2.Name = "cell2";
            this.cell2.Size = new System.Drawing.Size(30, 20);
            this.cell2.TabIndex = 3;
            this.cell2.Text = "000";
            this.cell2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cell2.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cell2);
            this.Controls.Add(this.cell1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label cell1;
        private System.Windows.Forms.Label cell2;
    }
}

