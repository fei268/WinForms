using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Login : Form
    {
        LoginInfo lo = new LoginInfo();
        LoginInfo lg = new LoginInfo();
        public Login()
        {
            InitializeComponent();
        }

        public Login(ref LoginInfo login)
        {
            InitializeComponent();
            this.lo = login;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lo.name = "admin";
            lo.pwd = "admin";

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            lg.name = userName.Text.Trim();
            lg.pwd = userPwd.Text.Trim();

            if (lo.name==lg.name && lo.pwd == lg.pwd)
            {
                this.DialogResult = DialogResult.OK;
                MessageBox.Show("Login success");
            }
            else
            {
                this.DialogResult = DialogResult.None;
                MessageBox.Show("Login fail");
            }
            
            this.Close();
        }

        private void userPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginBtn_Click(sender, e);
            }
        }
    }
}
