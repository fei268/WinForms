using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Main : Form
    {
        private bool isLoggedIn = false;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.RemoveAt(0);

            ThreadPool.QueueUserWorkItem(_ =>
            {
                AutoUp();
            });
        }

        void AutoUp()
        {
            #region 自动更新
            string autoExePath = Path.Combine(Environment.CurrentDirectory, "autoUpdate.exe");
            if (File.Exists(autoExePath))
            {
                Process autoUpdateProcess = Process.Start(new ProcessStartInfo
                {
                    FileName = autoExePath,
                    Arguments = $"{Assembly.GetEntryAssembly().GetName().Name} 0 ProgramA" //program name
                });
                autoUpdateProcess?.WaitForExit();

                string folderPath = Environment.CurrentDirectory + "\\up";//
                string autoUpdateFilePath = Path.Combine(folderPath, "autoUpdate.exe");
                if (File.Exists(autoUpdateFilePath))
                {
                    Thread.Sleep(9000);
                    File.Copy(autoUpdateFilePath, autoExePath, true);
                    Directory.Delete(folderPath, true);
                }
            }
            #endregion
        }

        private void mainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem clickedItem = e.ClickedItem;
            if (clickedItem is ToolStripMenuItem menuItem && menuItem.DropDownItems.Count > 0)
            {
                menuItem.DropDownItemClicked += SubMenuItem_Click;
            }
            else
            {
                CreateFormMenuItem(clickedItem);
            }
        }

        private void SubMenuItem_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem clickedItem = e.ClickedItem;
            CreateFormMenuItem(clickedItem);
        }

        //TabPages are more visually appealing than MDI containers
        void CreateFormMenuItem(ToolStripItem clickedItem)
        {
            //After creating a new window, you need to change the Text property of the window to a value different from its Name. For example, in the Ma2.cs window, if the Name is Ma2, the title bar Text cannot also be Ma2.

            Form frm = null;

            switch (clickedItem.Text)
            {
                case "Login":
                    if (!isLoggedIn)
                    {
                        Login loginForm = new Login();
                        if (loginForm.ShowDialog() == DialogResult.OK)
                        {
                            isLoggedIn = true;
                        }
                    }
                    return;
            }

            switch (clickedItem.Text)
            {
                
                case "Ma1":
                    if (!isLoggedIn)
                    {
                        MessageBox.Show("No Login");
                        return;
                    }
                    frm = new Ma1();
                    break;
                case "Ma2": frm = new Ma2(); break;
                case "Mb1": frm = new Mb1(); break;
                case "Mb2": frm = new Mb2(); break;
                case "Mc1": frm = new Mc1(); break;
                case "MenuD": frm = new Md(); break;
                default: break;
            }


            if (frm != null && !string.IsNullOrEmpty(frm.Text))
            {
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    if (tabPage.Controls.Count > 0 && tabPage.Controls[0] is Form form && form.Name == frm.Name)
                    {
                        tabControl1.SelectedTab = tabPage;
                        return;
                    }
                }

                AddNewTabPage(clickedItem.Text,frm);
            }
        }

        void AddNewTabPage(string tabPageText, Form frm)
        {
            TabPage newTabPage = new TabPage();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            newTabPage.Controls.Add(frm);
            newTabPage.Text = tabPageText + "    X ";
            frm.Show();

            tabControl1.TabPages.Add(newTabPage);
            tabControl1.SelectedTab = newTabPage;
        }

        void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePos = tabControl1.PointToClient(Control.MousePosition);
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                Rectangle tabRect = tabControl1.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                GraphicsPath path = GetCloseButtonPath(tabRect);
                if (path.IsVisible(mousePos))
                {
                    DialogResult dr = MessageBox.Show("Close " + tabControl1.TabPages[i].Text.Substring(0, tabControl1.TabPages[i].Text.Length - 5) + " ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        tabControl1.TabPages.RemoveAt(i);
                    }
                }
            }
        }

        private GraphicsPath GetCloseButtonPath(Rectangle tabBounds)
        {
            const int closeBoxWidth = 16;
            const int closeBoxHeight = 16;

            GraphicsPath path = new GraphicsPath();
            path.AddLine(tabBounds.Right - closeBoxWidth, tabBounds.Top, tabBounds.Right, tabBounds.Top + closeBoxHeight);
            path.AddLine(tabBounds.Right, tabBounds.Top, tabBounds.Right - closeBoxWidth, tabBounds.Top + closeBoxHeight);
            return path;
        }
    }
}
