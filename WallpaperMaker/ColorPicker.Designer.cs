
namespace WallpaperMaker.WinForm
{
    partial class ColorPicker
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
            this.lb_PalletsAvaliable = new System.Windows.Forms.ListBox();
            this.lbl_ColorsInPallet = new System.Windows.Forms.Label();
            this.lbl_PalletsAvaliable = new System.Windows.Forms.Label();
            this.bt_newPallet = new System.Windows.Forms.Button();
            this.bt_addColor = new System.Windows.Forms.Button();
            this.bt_DeleteColor = new System.Windows.Forms.Button();
            this.bt_DeletePallet = new System.Windows.Forms.Button();
            this.pb_ColorPreview = new System.Windows.Forms.PictureBox();
            this.bt_Done = new System.Windows.Forms.Button();
            this.lv_ColorsInPallet = new System.Windows.Forms.ListView();
            this.tb_newPalletName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ColorPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_PalletsAvaliable
            // 
            this.lb_PalletsAvaliable.FormattingEnabled = true;
            this.lb_PalletsAvaliable.ItemHeight = 15;
            this.lb_PalletsAvaliable.Location = new System.Drawing.Point(341, 31);
            this.lb_PalletsAvaliable.Name = "lb_PalletsAvaliable";
            this.lb_PalletsAvaliable.Size = new System.Drawing.Size(115, 274);
            this.lb_PalletsAvaliable.TabIndex = 0;
            this.lb_PalletsAvaliable.SelectedIndexChanged += new System.EventHandler(this.lb_PalletsAvaliable_SelectedIndexChanged);
            // 
            // lbl_ColorsInPallet
            // 
            this.lbl_ColorsInPallet.AutoSize = true;
            this.lbl_ColorsInPallet.Location = new System.Drawing.Point(220, 13);
            this.lbl_ColorsInPallet.Name = "lbl_ColorsInPallet";
            this.lbl_ColorsInPallet.Size = new System.Drawing.Size(92, 15);
            this.lbl_ColorsInPallet.TabIndex = 2;
            this.lbl_ColorsInPallet.Text = "Colors in Pallet: ";
            // 
            // lbl_PalletsAvaliable
            // 
            this.lbl_PalletsAvaliable.AutoSize = true;
            this.lbl_PalletsAvaliable.Location = new System.Drawing.Point(341, 13);
            this.lbl_PalletsAvaliable.Name = "lbl_PalletsAvaliable";
            this.lbl_PalletsAvaliable.Size = new System.Drawing.Size(92, 15);
            this.lbl_PalletsAvaliable.TabIndex = 3;
            this.lbl_PalletsAvaliable.Text = "Pallets Avaliable";
            // 
            // bt_newPallet
            // 
            this.bt_newPallet.Location = new System.Drawing.Point(12, 9);
            this.bt_newPallet.Name = "bt_newPallet";
            this.bt_newPallet.Size = new System.Drawing.Size(75, 23);
            this.bt_newPallet.TabIndex = 4;
            this.bt_newPallet.Text = "New Pallet";
            this.bt_newPallet.UseVisualStyleBackColor = true;
            this.bt_newPallet.Click += new System.EventHandler(this.bt_newPallet_Click);
            // 
            // bt_addColor
            // 
            this.bt_addColor.Location = new System.Drawing.Point(12, 38);
            this.bt_addColor.Name = "bt_addColor";
            this.bt_addColor.Size = new System.Drawing.Size(75, 23);
            this.bt_addColor.TabIndex = 5;
            this.bt_addColor.Text = "Add Color";
            this.bt_addColor.UseVisualStyleBackColor = true;
            this.bt_addColor.Click += new System.EventHandler(this.bt_addColor_Click);
            // 
            // bt_DeleteColor
            // 
            this.bt_DeleteColor.Location = new System.Drawing.Point(12, 67);
            this.bt_DeleteColor.Name = "bt_DeleteColor";
            this.bt_DeleteColor.Size = new System.Drawing.Size(75, 44);
            this.bt_DeleteColor.TabIndex = 6;
            this.bt_DeleteColor.Text = "Delete Color";
            this.bt_DeleteColor.UseVisualStyleBackColor = true;
            this.bt_DeleteColor.Click += new System.EventHandler(this.bt_DeleteColor_Click);
            // 
            // bt_DeletePallet
            // 
            this.bt_DeletePallet.Location = new System.Drawing.Point(12, 117);
            this.bt_DeletePallet.Name = "bt_DeletePallet";
            this.bt_DeletePallet.Size = new System.Drawing.Size(75, 44);
            this.bt_DeletePallet.TabIndex = 7;
            this.bt_DeletePallet.Text = "Delete Pallet";
            this.bt_DeletePallet.UseVisualStyleBackColor = true;
            this.bt_DeletePallet.Click += new System.EventHandler(this.bt_DeletePallet_Click);
            // 
            // pb_ColorPreview
            // 
            this.pb_ColorPreview.Location = new System.Drawing.Point(93, 39);
            this.pb_ColorPreview.Name = "pb_ColorPreview";
            this.pb_ColorPreview.Size = new System.Drawing.Size(121, 122);
            this.pb_ColorPreview.TabIndex = 8;
            this.pb_ColorPreview.TabStop = false;
            // 
            // bt_Done
            // 
            this.bt_Done.Location = new System.Drawing.Point(12, 280);
            this.bt_Done.Name = "bt_Done";
            this.bt_Done.Size = new System.Drawing.Size(75, 23);
            this.bt_Done.TabIndex = 9;
            this.bt_Done.Text = "Done";
            this.bt_Done.UseVisualStyleBackColor = true;
            this.bt_Done.Click += new System.EventHandler(this.bt_Done_Click);
            // 
            // lv_ColorsInPallet
            // 
            this.lv_ColorsInPallet.HideSelection = false;
            this.lv_ColorsInPallet.Location = new System.Drawing.Point(220, 31);
            this.lv_ColorsInPallet.Name = "lv_ColorsInPallet";
            this.lv_ColorsInPallet.Size = new System.Drawing.Size(115, 274);
            this.lv_ColorsInPallet.TabIndex = 10;
            this.lv_ColorsInPallet.UseCompatibleStateImageBehavior = false;
            this.lv_ColorsInPallet.SelectedIndexChanged += new System.EventHandler(this.lv_ColorsInPallet_SelectedIndexChanged);
            // 
            // tb_newPalletName
            // 
            this.tb_newPalletName.Location = new System.Drawing.Point(93, 10);
            this.tb_newPalletName.Name = "tb_newPalletName";
            this.tb_newPalletName.Size = new System.Drawing.Size(121, 23);
            this.tb_newPalletName.TabIndex = 11;
            this.tb_newPalletName.Text = "NewPalletName";
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 315);
            this.Controls.Add(this.tb_newPalletName);
            this.Controls.Add(this.lv_ColorsInPallet);
            this.Controls.Add(this.bt_Done);
            this.Controls.Add(this.pb_ColorPreview);
            this.Controls.Add(this.bt_DeletePallet);
            this.Controls.Add(this.bt_DeleteColor);
            this.Controls.Add(this.bt_addColor);
            this.Controls.Add(this.bt_newPallet);
            this.Controls.Add(this.lbl_PalletsAvaliable);
            this.Controls.Add(this.lbl_ColorsInPallet);
            this.Controls.Add(this.lb_PalletsAvaliable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorPicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ColorPicker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColorPicker_FormClosing);
            this.Load += new System.EventHandler(this.ColorPicker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_ColorPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_PalletsAvaliable;
        private System.Windows.Forms.Label lbl_ColorsInPallet;
        private System.Windows.Forms.Label lbl_PalletsAvaliable;
        private System.Windows.Forms.Button bt_newPallet;
        private System.Windows.Forms.Button bt_addColor;
        private System.Windows.Forms.Button bt_DeleteColor;
        private System.Windows.Forms.Button bt_DeletePallet;
        private System.Windows.Forms.PictureBox pb_ColorPreview;
        private System.Windows.Forms.Button bt_Done;
        private System.Windows.Forms.ListView lv_ColorsInPallet;
        private System.Windows.Forms.TextBox tb_newPalletName;
    }
}