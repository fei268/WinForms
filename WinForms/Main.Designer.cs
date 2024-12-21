
namespace WinForms
{
    partial class Main
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mA1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ma2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mb1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mb2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mc1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.menuAToolStripMenuItem,
            this.menuBToolStripMenuItem,
            this.menuCToolStripMenuItem,
            this.menuDToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(800, 25);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            this.mainMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mainMenu_ItemClicked);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
            this.loginToolStripMenuItem.Text = "Login";
            // 
            // menuAToolStripMenuItem
            // 
            this.menuAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mA1ToolStripMenuItem,
            this.ma2ToolStripMenuItem});
            this.menuAToolStripMenuItem.Name = "menuAToolStripMenuItem";
            this.menuAToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.menuAToolStripMenuItem.Text = "MenuA";
            // 
            // mA1ToolStripMenuItem
            // 
            this.mA1ToolStripMenuItem.Name = "mA1ToolStripMenuItem";
            this.mA1ToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.mA1ToolStripMenuItem.Text = "Ma1";
            // 
            // ma2ToolStripMenuItem
            // 
            this.ma2ToolStripMenuItem.Name = "ma2ToolStripMenuItem";
            this.ma2ToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.ma2ToolStripMenuItem.Text = "Ma2";
            // 
            // menuBToolStripMenuItem
            // 
            this.menuBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mb1ToolStripMenuItem,
            this.mb2ToolStripMenuItem});
            this.menuBToolStripMenuItem.Name = "menuBToolStripMenuItem";
            this.menuBToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.menuBToolStripMenuItem.Text = "MenuB";
            // 
            // mb1ToolStripMenuItem
            // 
            this.mb1ToolStripMenuItem.Name = "mb1ToolStripMenuItem";
            this.mb1ToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.mb1ToolStripMenuItem.Text = "Mb1";
            // 
            // mb2ToolStripMenuItem
            // 
            this.mb2ToolStripMenuItem.Name = "mb2ToolStripMenuItem";
            this.mb2ToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.mb2ToolStripMenuItem.Text = "Mb2";
            // 
            // menuCToolStripMenuItem
            // 
            this.menuCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mc1ToolStripMenuItem});
            this.menuCToolStripMenuItem.Name = "menuCToolStripMenuItem";
            this.menuCToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.menuCToolStripMenuItem.Text = "MenuC";
            // 
            // mc1ToolStripMenuItem
            // 
            this.mc1ToolStripMenuItem.Name = "mc1ToolStripMenuItem";
            this.mc1ToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.mc1ToolStripMenuItem.Text = "Mc1";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 396);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 422);
            this.tabControl1.TabIndex = 1;
            // 
            // menuDToolStripMenuItem
            // 
            this.menuDToolStripMenuItem.Name = "menuDToolStripMenuItem";
            this.menuDToolStripMenuItem.Size = new System.Drawing.Size(62, 21);
            this.menuDToolStripMenuItem.Text = "MenuD";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mA1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ma2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mb1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mb2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mc1ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem menuDToolStripMenuItem;
    }
}