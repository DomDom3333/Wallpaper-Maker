
namespace WallpaperMaker
{
    partial class frm_Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_Exit = new System.Windows.Forms.Button();
            this.bt_SaveOutput = new System.Windows.Forms.Button();
            this.bt_Generate = new System.Windows.Forms.Button();
            this.lbl_Preview = new System.Windows.Forms.Label();
            this.pb_Preview = new System.Windows.Forms.PictureBox();
            this.tb_xRes = new System.Windows.Forms.TextBox();
            this.tb_yRes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_AutoDetect = new System.Windows.Forms.Button();
            this.bt_Resize = new System.Windows.Forms.Button();
            this.cb_MSLevel = new System.Windows.Forms.ComboBox();
            this.lbl_MultiSample = new System.Windows.Forms.Label();
            this.bt_Settings = new System.Windows.Forms.Button();
            this.bt_Colors = new System.Windows.Forms.Button();
            this.cb_CollorPalletUsed = new System.Windows.Forms.ComboBox();
            this.lbl_ColorPalletUsed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_Exit
            // 
            this.bt_Exit.Location = new System.Drawing.Point(797, 12);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(75, 23);
            this.bt_Exit.TabIndex = 0;
            this.bt_Exit.Text = "Exit";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // bt_SaveOutput
            // 
            this.bt_SaveOutput.Location = new System.Drawing.Point(174, 12);
            this.bt_SaveOutput.Name = "bt_SaveOutput";
            this.bt_SaveOutput.Size = new System.Drawing.Size(75, 23);
            this.bt_SaveOutput.TabIndex = 1;
            this.bt_SaveOutput.Text = "Save Image";
            this.bt_SaveOutput.UseVisualStyleBackColor = true;
            this.bt_SaveOutput.Click += new System.EventHandler(this.bt_SaveOutput_Click);
            // 
            // bt_Generate
            // 
            this.bt_Generate.Location = new System.Drawing.Point(12, 12);
            this.bt_Generate.Name = "bt_Generate";
            this.bt_Generate.Size = new System.Drawing.Size(75, 23);
            this.bt_Generate.TabIndex = 6;
            this.bt_Generate.Text = "Generate";
            this.bt_Generate.UseVisualStyleBackColor = true;
            this.bt_Generate.Click += new System.EventHandler(this.bt_Generate_Click);
            // 
            // lbl_Preview
            // 
            this.lbl_Preview.AutoSize = true;
            this.lbl_Preview.Location = new System.Drawing.Point(12, 51);
            this.lbl_Preview.Name = "lbl_Preview";
            this.lbl_Preview.Size = new System.Drawing.Size(54, 15);
            this.lbl_Preview.TabIndex = 7;
            this.lbl_Preview.Text = "Preview: ";
            // 
            // pb_Preview
            // 
            this.pb_Preview.Location = new System.Drawing.Point(12, 69);
            this.pb_Preview.Name = "pb_Preview";
            this.pb_Preview.Size = new System.Drawing.Size(860, 369);
            this.pb_Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Preview.TabIndex = 8;
            this.pb_Preview.TabStop = false;
            // 
            // tb_xRes
            // 
            this.tb_xRes.Location = new System.Drawing.Point(469, 13);
            this.tb_xRes.MaxLength = 5;
            this.tb_xRes.Name = "tb_xRes";
            this.tb_xRes.Size = new System.Drawing.Size(100, 23);
            this.tb_xRes.TabIndex = 9;
            this.tb_xRes.Leave += new System.EventHandler(this.tb_xRes_Leave);
            // 
            // tb_yRes
            // 
            this.tb_yRes.Location = new System.Drawing.Point(469, 40);
            this.tb_yRes.MaxLength = 5;
            this.tb_yRes.Name = "tb_yRes";
            this.tb_yRes.Size = new System.Drawing.Size(100, 23);
            this.tb_yRes.TabIndex = 10;
            this.tb_yRes.Leave += new System.EventHandler(this.tb_yRes_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(356, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Vertical resolution: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Horizontal Resolution: ";
            // 
            // bt_AutoDetect
            // 
            this.bt_AutoDetect.Location = new System.Drawing.Point(575, 14);
            this.bt_AutoDetect.Name = "bt_AutoDetect";
            this.bt_AutoDetect.Size = new System.Drawing.Size(75, 22);
            this.bt_AutoDetect.TabIndex = 13;
            this.bt_AutoDetect.Text = "Auto Detect";
            this.bt_AutoDetect.UseVisualStyleBackColor = true;
            this.bt_AutoDetect.Click += new System.EventHandler(this.bt_AutoDetect_Click);
            // 
            // bt_Resize
            // 
            this.bt_Resize.Location = new System.Drawing.Point(575, 41);
            this.bt_Resize.Name = "bt_Resize";
            this.bt_Resize.Size = new System.Drawing.Size(75, 22);
            this.bt_Resize.TabIndex = 14;
            this.bt_Resize.Text = "Resize Window";
            this.bt_Resize.UseVisualStyleBackColor = true;
            this.bt_Resize.Click += new System.EventHandler(this.bt_Resize_Click);
            // 
            // cb_MSLevel
            // 
            this.cb_MSLevel.FormattingEnabled = true;
            this.cb_MSLevel.Location = new System.Drawing.Point(656, 40);
            this.cb_MSLevel.Name = "cb_MSLevel";
            this.cb_MSLevel.Size = new System.Drawing.Size(53, 23);
            this.cb_MSLevel.TabIndex = 15;
            // 
            // lbl_MultiSample
            // 
            this.lbl_MultiSample.AutoSize = true;
            this.lbl_MultiSample.Location = new System.Drawing.Point(656, 16);
            this.lbl_MultiSample.Name = "lbl_MultiSample";
            this.lbl_MultiSample.Size = new System.Drawing.Size(126, 15);
            this.lbl_MultiSample.TabIndex = 16;
            this.lbl_MultiSample.Text = "Multi-Sampling Level: ";
            // 
            // bt_Settings
            // 
            this.bt_Settings.Location = new System.Drawing.Point(93, 12);
            this.bt_Settings.Name = "bt_Settings";
            this.bt_Settings.Size = new System.Drawing.Size(75, 23);
            this.bt_Settings.TabIndex = 17;
            this.bt_Settings.Text = "Settings";
            this.bt_Settings.UseVisualStyleBackColor = true;
            this.bt_Settings.Click += new System.EventHandler(this.bt_Settings_Click);
            // 
            // bt_Colors
            // 
            this.bt_Colors.Location = new System.Drawing.Point(255, 12);
            this.bt_Colors.Name = "bt_Colors";
            this.bt_Colors.Size = new System.Drawing.Size(75, 23);
            this.bt_Colors.TabIndex = 18;
            this.bt_Colors.Text = "Colors";
            this.bt_Colors.UseVisualStyleBackColor = true;
            this.bt_Colors.Click += new System.EventHandler(this.bt_Colors_Click);
            // 
            // cb_CollorPalletUsed
            // 
            this.cb_CollorPalletUsed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_CollorPalletUsed.FormattingEnabled = true;
            this.cb_CollorPalletUsed.Location = new System.Drawing.Point(209, 40);
            this.cb_CollorPalletUsed.Name = "cb_CollorPalletUsed";
            this.cb_CollorPalletUsed.Size = new System.Drawing.Size(121, 23);
            this.cb_CollorPalletUsed.TabIndex = 19;
            this.cb_CollorPalletUsed.TabStop = false;
            this.cb_CollorPalletUsed.SelectedIndexChanged += new System.EventHandler(this.cb_CollorPalletUsed_SelectedIndexChanged);
            // 
            // lbl_ColorPalletUsed
            // 
            this.lbl_ColorPalletUsed.AutoSize = true;
            this.lbl_ColorPalletUsed.Location = new System.Drawing.Point(100, 43);
            this.lbl_ColorPalletUsed.Name = "lbl_ColorPalletUsed";
            this.lbl_ColorPalletUsed.Size = new System.Drawing.Size(103, 15);
            this.lbl_ColorPalletUsed.TabIndex = 20;
            this.lbl_ColorPalletUsed.Text = "Color Pallet Used: ";
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(881, 450);
            this.Controls.Add(this.lbl_ColorPalletUsed);
            this.Controls.Add(this.cb_CollorPalletUsed);
            this.Controls.Add(this.bt_Colors);
            this.Controls.Add(this.bt_Settings);
            this.Controls.Add(this.lbl_MultiSample);
            this.Controls.Add(this.cb_MSLevel);
            this.Controls.Add(this.bt_Resize);
            this.Controls.Add(this.bt_AutoDetect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_yRes);
            this.Controls.Add(this.tb_xRes);
            this.Controls.Add(this.pb_Preview);
            this.Controls.Add(this.lbl_Preview);
            this.Controls.Add(this.bt_Generate);
            this.Controls.Add(this.bt_SaveOutput);
            this.Controls.Add(this.bt_Exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frm_Main";
            this.Text = "WallpaperMaker";
            this.Load += new System.EventHandler(this.frm_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.Button bt_SaveOutput;
        private System.Windows.Forms.Button bt_Generate;
        private System.Windows.Forms.Label lbl_Preview;
        private System.Windows.Forms.PictureBox pb_Preview;
        private System.Windows.Forms.TextBox tb_xRes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_AutoDetect;
        private System.Windows.Forms.TextBox tb_yRes;
        private System.Windows.Forms.Button bt_Resize;
        private System.Windows.Forms.ComboBox cb_MSLevel;
        private System.Windows.Forms.Label lbl_MultiSample;
        private System.Windows.Forms.Button bt_Settings;
        private System.Windows.Forms.Button bt_Colors;
        private System.Windows.Forms.ComboBox cb_CollorPalletUsed;
        private System.Windows.Forms.Label lbl_ColorPalletUsed;
    }
}

