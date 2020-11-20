
namespace WallpaperMaker
{
    partial class SettingsPannel
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
            this.tb_AmoutRecs = new System.Windows.Forms.TrackBar();
            this.lbl_AmoutRecs = new System.Windows.Forms.Label();
            this.lbl_AmoutSquares = new System.Windows.Forms.Label();
            this.tb_AmoutSquares = new System.Windows.Forms.TrackBar();
            this.lbl_AmountEllies = new System.Windows.Forms.Label();
            this.tb_AmoutEllies = new System.Windows.Forms.TrackBar();
            this.lbl_AmountCircles = new System.Windows.Forms.Label();
            this.tb_AmoutCircles = new System.Windows.Forms.TrackBar();
            this.lbl_SizeRecs = new System.Windows.Forms.Label();
            this.tb_SizeRecs = new System.Windows.Forms.TrackBar();
            this.tb_SizeSquares = new System.Windows.Forms.TrackBar();
            this.tb_SizeEllies = new System.Windows.Forms.TrackBar();
            this.tb_SizeCircles = new System.Windows.Forms.TrackBar();
            this.cb_isEnabledRecs = new System.Windows.Forms.CheckBox();
            this.cb_isEnabledCircles = new System.Windows.Forms.CheckBox();
            this.cb_isEnabledEllis = new System.Windows.Forms.CheckBox();
            this.cb_isEnabledSquares = new System.Windows.Forms.CheckBox();
            this.lbl_SizeCircles = new System.Windows.Forms.Label();
            this.lbl_SizeEllies = new System.Windows.Forms.Label();
            this.lbl_SizeSquares = new System.Windows.Forms.Label();
            this.tb_ResultSeed = new System.Windows.Forms.TextBox();
            this.lbl_ResultSeed = new System.Windows.Forms.Label();
            this.bt_Done = new System.Windows.Forms.Button();
            this.bt_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AmoutRecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AmoutSquares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AmoutEllies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AmoutCircles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SizeRecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SizeSquares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SizeEllies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SizeCircles)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_AmoutRecs
            // 
            this.tb_AmoutRecs.Location = new System.Drawing.Point(107, 12);
            this.tb_AmoutRecs.Maximum = 9;
            this.tb_AmoutRecs.Minimum = 1;
            this.tb_AmoutRecs.Name = "tb_AmoutRecs";
            this.tb_AmoutRecs.Size = new System.Drawing.Size(104, 45);
            this.tb_AmoutRecs.TabIndex = 0;
            this.tb_AmoutRecs.Value = 1;
            this.tb_AmoutRecs.Scroll += new System.EventHandler(this.AnyBar_Scroll);
            // 
            // lbl_AmoutRecs
            // 
            this.lbl_AmoutRecs.AutoSize = true;
            this.lbl_AmoutRecs.Location = new System.Drawing.Point(13, 13);
            this.lbl_AmoutRecs.Name = "lbl_AmoutRecs";
            this.lbl_AmoutRecs.Size = new System.Drawing.Size(88, 15);
            this.lbl_AmoutRecs.TabIndex = 1;
            this.lbl_AmoutRecs.Text = "# of Rectangles";
            // 
            // lbl_AmoutSquares
            // 
            this.lbl_AmoutSquares.AutoSize = true;
            this.lbl_AmoutSquares.Location = new System.Drawing.Point(13, 64);
            this.lbl_AmoutSquares.Name = "lbl_AmoutSquares";
            this.lbl_AmoutSquares.Size = new System.Drawing.Size(72, 15);
            this.lbl_AmoutSquares.TabIndex = 3;
            this.lbl_AmoutSquares.Text = "# of Squares";
            // 
            // tb_AmoutSquares
            // 
            this.tb_AmoutSquares.Location = new System.Drawing.Point(107, 63);
            this.tb_AmoutSquares.Maximum = 9;
            this.tb_AmoutSquares.Minimum = 1;
            this.tb_AmoutSquares.Name = "tb_AmoutSquares";
            this.tb_AmoutSquares.Size = new System.Drawing.Size(104, 45);
            this.tb_AmoutSquares.TabIndex = 2;
            this.tb_AmoutSquares.Value = 1;
            this.tb_AmoutSquares.Scroll += new System.EventHandler(this.AnyBar_Scroll);
            // 
            // lbl_AmountEllies
            // 
            this.lbl_AmountEllies.AutoSize = true;
            this.lbl_AmountEllies.Location = new System.Drawing.Point(13, 115);
            this.lbl_AmountEllies.Name = "lbl_AmountEllies";
            this.lbl_AmountEllies.Size = new System.Drawing.Size(72, 15);
            this.lbl_AmountEllies.TabIndex = 5;
            this.lbl_AmountEllies.Text = "# of Ellipsies";
            // 
            // tb_AmoutEllies
            // 
            this.tb_AmoutEllies.Location = new System.Drawing.Point(107, 114);
            this.tb_AmoutEllies.Maximum = 9;
            this.tb_AmoutEllies.Minimum = 1;
            this.tb_AmoutEllies.Name = "tb_AmoutEllies";
            this.tb_AmoutEllies.Size = new System.Drawing.Size(104, 45);
            this.tb_AmoutEllies.TabIndex = 4;
            this.tb_AmoutEllies.Value = 1;
            this.tb_AmoutEllies.Scroll += new System.EventHandler(this.AnyBar_Scroll);
            // 
            // lbl_AmountCircles
            // 
            this.lbl_AmountCircles.AutoSize = true;
            this.lbl_AmountCircles.Location = new System.Drawing.Point(13, 166);
            this.lbl_AmountCircles.Name = "lbl_AmountCircles";
            this.lbl_AmountCircles.Size = new System.Drawing.Size(66, 15);
            this.lbl_AmountCircles.TabIndex = 7;
            this.lbl_AmountCircles.Text = "# of Circles";
            // 
            // tb_AmoutCircles
            // 
            this.tb_AmoutCircles.Location = new System.Drawing.Point(107, 165);
            this.tb_AmoutCircles.Maximum = 9;
            this.tb_AmoutCircles.Minimum = 1;
            this.tb_AmoutCircles.Name = "tb_AmoutCircles";
            this.tb_AmoutCircles.Size = new System.Drawing.Size(104, 45);
            this.tb_AmoutCircles.TabIndex = 6;
            this.tb_AmoutCircles.Value = 1;
            this.tb_AmoutCircles.Scroll += new System.EventHandler(this.AnyBar_Scroll);
            // 
            // lbl_SizeRecs
            // 
            this.lbl_SizeRecs.AutoSize = true;
            this.lbl_SizeRecs.Location = new System.Drawing.Point(217, 13);
            this.lbl_SizeRecs.Name = "lbl_SizeRecs";
            this.lbl_SizeRecs.Size = new System.Drawing.Size(27, 15);
            this.lbl_SizeRecs.TabIndex = 11;
            this.lbl_SizeRecs.Text = "Size";
            // 
            // tb_SizeRecs
            // 
            this.tb_SizeRecs.Location = new System.Drawing.Point(250, 12);
            this.tb_SizeRecs.Maximum = 9;
            this.tb_SizeRecs.Minimum = 1;
            this.tb_SizeRecs.Name = "tb_SizeRecs";
            this.tb_SizeRecs.Size = new System.Drawing.Size(104, 45);
            this.tb_SizeRecs.TabIndex = 10;
            this.tb_SizeRecs.Value = 1;
            this.tb_SizeRecs.Scroll += new System.EventHandler(this.AnyBar_Scroll);
            // 
            // tb_SizeSquares
            // 
            this.tb_SizeSquares.Location = new System.Drawing.Point(250, 63);
            this.tb_SizeSquares.Maximum = 9;
            this.tb_SizeSquares.Minimum = 1;
            this.tb_SizeSquares.Name = "tb_SizeSquares";
            this.tb_SizeSquares.Size = new System.Drawing.Size(104, 45);
            this.tb_SizeSquares.TabIndex = 12;
            this.tb_SizeSquares.Value = 1;
            this.tb_SizeSquares.Scroll += new System.EventHandler(this.AnyBar_Scroll);
            // 
            // tb_SizeEllies
            // 
            this.tb_SizeEllies.Location = new System.Drawing.Point(250, 114);
            this.tb_SizeEllies.Maximum = 9;
            this.tb_SizeEllies.Minimum = 1;
            this.tb_SizeEllies.Name = "tb_SizeEllies";
            this.tb_SizeEllies.Size = new System.Drawing.Size(104, 45);
            this.tb_SizeEllies.TabIndex = 14;
            this.tb_SizeEllies.Value = 1;
            this.tb_SizeEllies.Scroll += new System.EventHandler(this.AnyBar_Scroll);
            // 
            // tb_SizeCircles
            // 
            this.tb_SizeCircles.Location = new System.Drawing.Point(250, 165);
            this.tb_SizeCircles.Maximum = 9;
            this.tb_SizeCircles.Minimum = 1;
            this.tb_SizeCircles.Name = "tb_SizeCircles";
            this.tb_SizeCircles.Size = new System.Drawing.Size(104, 45);
            this.tb_SizeCircles.TabIndex = 16;
            this.tb_SizeCircles.Value = 1;
            this.tb_SizeCircles.Scroll += new System.EventHandler(this.AnyBar_Scroll);
            // 
            // cb_isEnabledRecs
            // 
            this.cb_isEnabledRecs.AutoSize = true;
            this.cb_isEnabledRecs.Checked = true;
            this.cb_isEnabledRecs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_isEnabledRecs.Location = new System.Drawing.Point(360, 12);
            this.cb_isEnabledRecs.Name = "cb_isEnabledRecs";
            this.cb_isEnabledRecs.Size = new System.Drawing.Size(73, 19);
            this.cb_isEnabledRecs.TabIndex = 20;
            this.cb_isEnabledRecs.Text = "Enabled?";
            this.cb_isEnabledRecs.UseVisualStyleBackColor = true;
            this.cb_isEnabledRecs.CheckedChanged += new System.EventHandler(this.cb_isEnabledRecs_CheckedChanged);
            // 
            // cb_isEnabledCircles
            // 
            this.cb_isEnabledCircles.AutoSize = true;
            this.cb_isEnabledCircles.Checked = true;
            this.cb_isEnabledCircles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_isEnabledCircles.Location = new System.Drawing.Point(360, 165);
            this.cb_isEnabledCircles.Name = "cb_isEnabledCircles";
            this.cb_isEnabledCircles.Size = new System.Drawing.Size(73, 19);
            this.cb_isEnabledCircles.TabIndex = 22;
            this.cb_isEnabledCircles.Text = "Enabled?";
            this.cb_isEnabledCircles.UseVisualStyleBackColor = true;
            this.cb_isEnabledCircles.CheckedChanged += new System.EventHandler(this.cb_isEnabledCircles_CheckedChanged);
            // 
            // cb_isEnabledEllis
            // 
            this.cb_isEnabledEllis.AutoSize = true;
            this.cb_isEnabledEllis.Checked = true;
            this.cb_isEnabledEllis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_isEnabledEllis.Location = new System.Drawing.Point(360, 114);
            this.cb_isEnabledEllis.Name = "cb_isEnabledEllis";
            this.cb_isEnabledEllis.Size = new System.Drawing.Size(73, 19);
            this.cb_isEnabledEllis.TabIndex = 23;
            this.cb_isEnabledEllis.Text = "Enabled?";
            this.cb_isEnabledEllis.UseVisualStyleBackColor = true;
            this.cb_isEnabledEllis.CheckedChanged += new System.EventHandler(this.cb_isEnabledEllis_CheckedChanged);
            // 
            // cb_isEnabledSquares
            // 
            this.cb_isEnabledSquares.AutoSize = true;
            this.cb_isEnabledSquares.Checked = true;
            this.cb_isEnabledSquares.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_isEnabledSquares.Location = new System.Drawing.Point(360, 63);
            this.cb_isEnabledSquares.Name = "cb_isEnabledSquares";
            this.cb_isEnabledSquares.Size = new System.Drawing.Size(73, 19);
            this.cb_isEnabledSquares.TabIndex = 24;
            this.cb_isEnabledSquares.Text = "Enabled?";
            this.cb_isEnabledSquares.UseVisualStyleBackColor = true;
            this.cb_isEnabledSquares.CheckedChanged += new System.EventHandler(this.cb_isEnabledSquares_CheckedChanged);
            // 
            // lbl_SizeCircles
            // 
            this.lbl_SizeCircles.AutoSize = true;
            this.lbl_SizeCircles.Location = new System.Drawing.Point(217, 166);
            this.lbl_SizeCircles.Name = "lbl_SizeCircles";
            this.lbl_SizeCircles.Size = new System.Drawing.Size(27, 15);
            this.lbl_SizeCircles.TabIndex = 25;
            this.lbl_SizeCircles.Text = "Size";
            // 
            // lbl_SizeEllies
            // 
            this.lbl_SizeEllies.AutoSize = true;
            this.lbl_SizeEllies.Location = new System.Drawing.Point(217, 115);
            this.lbl_SizeEllies.Name = "lbl_SizeEllies";
            this.lbl_SizeEllies.Size = new System.Drawing.Size(27, 15);
            this.lbl_SizeEllies.TabIndex = 26;
            this.lbl_SizeEllies.Text = "Size";
            // 
            // lbl_SizeSquares
            // 
            this.lbl_SizeSquares.AutoSize = true;
            this.lbl_SizeSquares.Location = new System.Drawing.Point(217, 64);
            this.lbl_SizeSquares.Name = "lbl_SizeSquares";
            this.lbl_SizeSquares.Size = new System.Drawing.Size(27, 15);
            this.lbl_SizeSquares.TabIndex = 27;
            this.lbl_SizeSquares.Text = "Size";
            // 
            // tb_ResultSeed
            // 
            this.tb_ResultSeed.Location = new System.Drawing.Point(111, 216);
            this.tb_ResultSeed.Name = "tb_ResultSeed";
            this.tb_ResultSeed.ReadOnly = true;
            this.tb_ResultSeed.Size = new System.Drawing.Size(166, 23);
            this.tb_ResultSeed.TabIndex = 28;
            this.tb_ResultSeed.TabStop = false;
            // 
            // lbl_ResultSeed
            // 
            this.lbl_ResultSeed.AutoSize = true;
            this.lbl_ResultSeed.Location = new System.Drawing.Point(11, 219);
            this.lbl_ResultSeed.Name = "lbl_ResultSeed";
            this.lbl_ResultSeed.Size = new System.Drawing.Size(90, 15);
            this.lbl_ResultSeed.TabIndex = 29;
            this.lbl_ResultSeed.Text = "Resulting Seed: ";
            // 
            // bt_Done
            // 
            this.bt_Done.Location = new System.Drawing.Point(364, 215);
            this.bt_Done.Name = "bt_Done";
            this.bt_Done.Size = new System.Drawing.Size(75, 23);
            this.bt_Done.TabIndex = 30;
            this.bt_Done.Text = "Done";
            this.bt_Done.UseVisualStyleBackColor = true;
            this.bt_Done.Click += new System.EventHandler(this.bt_Done_Click);
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.Location = new System.Drawing.Point(283, 215);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_Cancel.TabIndex = 31;
            this.bt_Cancel.Text = "Cancel";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
            // 
            // SettingsPannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 251);
            this.Controls.Add(this.bt_Cancel);
            this.Controls.Add(this.bt_Done);
            this.Controls.Add(this.lbl_ResultSeed);
            this.Controls.Add(this.tb_ResultSeed);
            this.Controls.Add(this.lbl_SizeSquares);
            this.Controls.Add(this.lbl_SizeEllies);
            this.Controls.Add(this.lbl_SizeCircles);
            this.Controls.Add(this.cb_isEnabledSquares);
            this.Controls.Add(this.cb_isEnabledEllis);
            this.Controls.Add(this.cb_isEnabledCircles);
            this.Controls.Add(this.cb_isEnabledRecs);
            this.Controls.Add(this.tb_SizeCircles);
            this.Controls.Add(this.tb_SizeEllies);
            this.Controls.Add(this.tb_SizeSquares);
            this.Controls.Add(this.lbl_SizeRecs);
            this.Controls.Add(this.tb_SizeRecs);
            this.Controls.Add(this.lbl_AmountCircles);
            this.Controls.Add(this.tb_AmoutCircles);
            this.Controls.Add(this.lbl_AmountEllies);
            this.Controls.Add(this.tb_AmoutEllies);
            this.Controls.Add(this.lbl_AmoutSquares);
            this.Controls.Add(this.tb_AmoutSquares);
            this.Controls.Add(this.lbl_AmoutRecs);
            this.Controls.Add(this.tb_AmoutRecs);
            this.Name = "SettingsPannel";
            this.Text = "SettingsPannel";
            this.Load += new System.EventHandler(this.SettingsPannel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tb_AmoutRecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AmoutSquares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AmoutEllies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_AmoutCircles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SizeRecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SizeSquares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SizeEllies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_SizeCircles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tb_AmoutRecs;
        private System.Windows.Forms.Label lbl_AmoutRecs;
        private System.Windows.Forms.Label lbl_AmoutSquares;
        private System.Windows.Forms.TrackBar tb_AmoutSquares;
        private System.Windows.Forms.Label lbl_AmountEllies;
        private System.Windows.Forms.TrackBar tb_AmoutEllies;
        private System.Windows.Forms.Label lbl_AmountCircles;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Label lbl_SizeRecs;
        private System.Windows.Forms.TrackBar tb_SizeRecs;
        private System.Windows.Forms.TrackBar tb_SizeSquares;
        private System.Windows.Forms.TrackBar tb_SizeEllies;
        private System.Windows.Forms.TrackBar tb_SizeCircles;
        private System.Windows.Forms.CheckBox cb_isEnabledRecs;
        private System.Windows.Forms.CheckBox cb_isEnabledCircles;
        private System.Windows.Forms.CheckBox cb_isEnabledEllis;
        private System.Windows.Forms.CheckBox cb_isEnabledSquares;
        private System.Windows.Forms.Label lbl_SizeCircles;
        private System.Windows.Forms.Label lbl_SizeEllies;
        private System.Windows.Forms.Label lbl_SizeSquares;
        private System.Windows.Forms.TextBox tb_ResultSeed;
        private System.Windows.Forms.Label lbl_ResultSeed;
        private System.Windows.Forms.TrackBar tb_AmoutCircles;
        private System.Windows.Forms.Button bt_Done;
        private System.Windows.Forms.Button bt_Cancel;
    }
}