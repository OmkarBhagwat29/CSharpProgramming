namespace RevitTest
{
    partial class CreateBeamsCoulmnsBracesFrom
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
            this.ShowBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.DistanceLabel = new System.Windows.Forms.Label();
            this.unitLabel = new System.Windows.Forms.Label();
            this.floornumberLabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.floornumberTextBox = new System.Windows.Forms.TextBox();
            this.DistanceTextBox = new System.Windows.Forms.TextBox();
            this.YTextBox = new System.Windows.Forms.TextBox();
            this.XTextBox = new System.Windows.Forms.TextBox();
            this.braceLabel = new System.Windows.Forms.Label();
            this.beamLabel = new System.Windows.Forms.Label();
            this.columnLabel = new System.Windows.Forms.Label();
            this.braceComboBox = new System.Windows.Forms.ComboBox();
            this.beamComboBox = new System.Windows.Forms.ComboBox();
            this.columnComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ShowBtn
            // 
            this.ShowBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowBtn.Location = new System.Drawing.Point(340, 195);
            this.ShowBtn.Name = "ShowBtn";
            this.ShowBtn.Size = new System.Drawing.Size(76, 28);
            this.ShowBtn.TabIndex = 0;
            this.ShowBtn.Text = "Ok";
            this.ShowBtn.UseVisualStyleBackColor = true;
            this.ShowBtn.Click += new System.EventHandler(this.ShowBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBtn.Location = new System.Drawing.Point(445, 195);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(73, 28);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // DistanceLabel
            // 
            this.DistanceLabel.Location = new System.Drawing.Point(12, 9);
            this.DistanceLabel.Name = "DistanceLabel";
            this.DistanceLabel.Size = new System.Drawing.Size(152, 23);
            this.DistanceLabel.TabIndex = 15;
            this.DistanceLabel.Text = "Distance between Columns:";
            // 
            // unitLabel
            // 
            this.unitLabel.Location = new System.Drawing.Point(135, 37);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(32, 23);
            this.unitLabel.TabIndex = 26;
            this.unitLabel.Text = "feet";
            // 
            // floornumberLabel
            // 
            this.floornumberLabel.Location = new System.Drawing.Point(15, 179);
            this.floornumberLabel.Name = "floornumberLabel";
            this.floornumberLabel.Size = new System.Drawing.Size(144, 23);
            this.floornumberLabel.TabIndex = 25;
            this.floornumberLabel.Text = "Number of Floors:";
            // 
            // XLabel
            // 
            this.XLabel.Location = new System.Drawing.Point(15, 67);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(200, 23);
            this.XLabel.TabIndex = 24;
            this.XLabel.Text = "Number of Columns in the X Direction:";
            // 
            // YLabel
            // 
            this.YLabel.Location = new System.Drawing.Point(15, 123);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(200, 23);
            this.YLabel.TabIndex = 23;
            this.YLabel.Text = "Number of Columns in the Y Direction:";
            // 
            // floornumberTextBox
            // 
            this.floornumberTextBox.Location = new System.Drawing.Point(15, 203);
            this.floornumberTextBox.Name = "floornumberTextBox";
            this.floornumberTextBox.Size = new System.Drawing.Size(112, 20);
            this.floornumberTextBox.TabIndex = 22;
            // 
            // DistanceTextBox
            // 
            this.DistanceTextBox.Location = new System.Drawing.Point(15, 35);
            this.DistanceTextBox.Name = "DistanceTextBox";
            this.DistanceTextBox.Size = new System.Drawing.Size(112, 20);
            this.DistanceTextBox.TabIndex = 19;
            // 
            // YTextBox
            // 
            this.YTextBox.Location = new System.Drawing.Point(15, 147);
            this.YTextBox.Name = "YTextBox";
            this.YTextBox.Size = new System.Drawing.Size(136, 20);
            this.YTextBox.TabIndex = 21;
            // 
            // XTextBox
            // 
            this.XTextBox.Location = new System.Drawing.Point(15, 91);
            this.XTextBox.Name = "XTextBox";
            this.XTextBox.Size = new System.Drawing.Size(136, 20);
            this.XTextBox.TabIndex = 20;
            // 
            // braceLabel
            // 
            this.braceLabel.Location = new System.Drawing.Point(230, 122);
            this.braceLabel.Name = "braceLabel";
            this.braceLabel.Size = new System.Drawing.Size(120, 23);
            this.braceLabel.TabIndex = 32;
            this.braceLabel.Text = "Type of Braces:";
            // 
            // beamLabel
            // 
            this.beamLabel.Location = new System.Drawing.Point(230, 66);
            this.beamLabel.Name = "beamLabel";
            this.beamLabel.Size = new System.Drawing.Size(120, 23);
            this.beamLabel.TabIndex = 31;
            this.beamLabel.Text = "Type of Beams:";
            // 
            // columnLabel
            // 
            this.columnLabel.Location = new System.Drawing.Point(230, 10);
            this.columnLabel.Name = "columnLabel";
            this.columnLabel.Size = new System.Drawing.Size(120, 23);
            this.columnLabel.TabIndex = 30;
            this.columnLabel.Text = "Type of Columns:";
            // 
            // braceComboBox
            // 
            this.braceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.braceComboBox.Location = new System.Drawing.Point(230, 146);
            this.braceComboBox.Name = "braceComboBox";
            this.braceComboBox.Size = new System.Drawing.Size(288, 21);
            this.braceComboBox.TabIndex = 29;
            // 
            // beamComboBox
            // 
            this.beamComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.beamComboBox.Location = new System.Drawing.Point(230, 90);
            this.beamComboBox.Name = "beamComboBox";
            this.beamComboBox.Size = new System.Drawing.Size(288, 21);
            this.beamComboBox.TabIndex = 28;
            // 
            // columnComboBox
            // 
            this.columnComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.columnComboBox.Location = new System.Drawing.Point(230, 34);
            this.columnComboBox.Name = "columnComboBox";
            this.columnComboBox.Size = new System.Drawing.Size(288, 21);
            this.columnComboBox.TabIndex = 27;
            // 
            // CreateBeamsCoulmnsBracesFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 269);
            this.Controls.Add(this.braceLabel);
            this.Controls.Add(this.beamLabel);
            this.Controls.Add(this.columnLabel);
            this.Controls.Add(this.braceComboBox);
            this.Controls.Add(this.beamComboBox);
            this.Controls.Add(this.columnComboBox);
            this.Controls.Add(this.unitLabel);
            this.Controls.Add(this.floornumberLabel);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.floornumberTextBox);
            this.Controls.Add(this.DistanceTextBox);
            this.Controls.Add(this.YTextBox);
            this.Controls.Add(this.XTextBox);
            this.Controls.Add(this.DistanceLabel);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.ShowBtn);
            this.Name = "CreateBeamsCoulmnsBracesFrom";
            this.Text = "CreateBeamsCoulmnsBracesFrom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ShowBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label DistanceLabel;
        private System.Windows.Forms.Label unitLabel;
        private System.Windows.Forms.Label floornumberLabel;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.TextBox floornumberTextBox;
        private System.Windows.Forms.TextBox DistanceTextBox;
        private System.Windows.Forms.TextBox YTextBox;
        private System.Windows.Forms.TextBox XTextBox;
        private System.Windows.Forms.Label braceLabel;
        private System.Windows.Forms.Label beamLabel;
        private System.Windows.Forms.Label columnLabel;
        private System.Windows.Forms.ComboBox braceComboBox;
        private System.Windows.Forms.ComboBox beamComboBox;
        private System.Windows.Forms.ComboBox columnComboBox;
    }
}