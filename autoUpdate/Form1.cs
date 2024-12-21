using SharpCompress.Readers;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

/* 简易自动更新程序设计思路：
 * 主要流程：上传程序更新的压缩包 >> 客户端执行自动更新程序下载 > 解压 > 替换 
 * 1，上传功能集合在一起，通过参数进行功能判断；
 * 2，同一程序保留2条数据，用于防止更新新版出错后可以回滚到之前版本；
 * 思路调整：
 * 1，客户端不管什么情况下都请求本程序；
 * 2，更新程序中切换所需要的版本；
 */

namespace autoUpdate
{
    public partial class Form1 : Form
    {
        string exepath = Environment.CurrentDirectory;
        string folderPath = Environment.CurrentDirectory + "\\updatefile";
        static readonly string connStr = "Data Source=.;Initial Catalog=.;Persist Security Info=True;User ID=.;Password=.";
        string[] comArgs = null;
        DataTable dt = new DataTable();
        string verfile = Path.Combine(Environment.CurrentDirectory, "version.txt");

        string ename = "";
        string cname = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(verfile))
            {
                lb_locVer.Text = File.ReadAllText(verfile).Trim('\r', '\n');
                
            }

            comArgs = Environment.GetCommandLineArgs();
            if (comArgs.Length == 2 && comArgs[1] == "admin")
            {
                panel1.Visible = true;
                panel2.Visible = true;
            }
            else if (comArgs.Length == 3)
            {
                panel1.Visible = false;
                panel2.Visible = false;

                CheckVersion(comArgs[1], comArgs[2]);
                DateTime Ver0 = DateTime.MinValue;
                DateTime Ver1 = DateTime.MinValue;
                DateTime Ver2 = DateTime.MinValue;
                if (DateTime.TryParseExact(lb_locVer.Text.Substring(2, lb_locVer.Text.Length - 2), "yyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out Ver0)) { }
                if (DateTime.TryParseExact(listBox1.Items[0].ToString().Substring(2, listBox1.Items[0].ToString().Length - 2), "yyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out Ver1)) { }
                if (DateTime.TryParseExact(listBox1.Items[1].ToString().Substring(2, listBox1.Items[1].ToString().Length - 2), "yyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out Ver2)) { }

                if (lb_locVer.Text.Substring(0, 1) == "0" && Ver0 <= Ver1)
                {
                    this.Close();
                    return;
                }
                else if(lb_locVer.Text.Substring(0, 1) == "1" && Ver0 <= Ver2)
                {
                    this.Close();
                    return;
                }
                else
                {
                    DownFile(comArgs[1], "1");
                }
                this.Close();
            }
            else
            {
                panel1.Visible = true;
                panel2.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        void CheckVersion(string arg1, string arg2)
        {
            if (arg1.Length < 2) return;

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string[] filePaths = Directory.GetFiles(folderPath);
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand("SELECT ROW_NUMBER() over(order by uptime asc) -1 rownum,fileversion,filedata, filename FROM autoupdata where ename=@ename and cname=@cname order by uptime asc", connection);
                connection.Open();
                command.Parameters.AddWithValue("@ename", arg1);
                command.Parameters.AddWithValue("@cname", arg2);
                SqlDataReader dr = command.ExecuteReader();
                dt.Clear();
                dt.Load(dr);
                dr.Close();
            }

            string oldVer = "0";
            string newVer = "0";

            if (dt.Rows.Count > 1)
            {
                oldVer = dt.Rows[0]["fileversion"].ToString();
                newVer = dt.Rows[1]["fileversion"].ToString();
            }
            else if (dt.Rows.Count == 1)
            {
                DataRow row = dt.NewRow();
                row.ItemArray = dt.Rows[0].ItemArray;
                row[0] = "1";
                dt.Rows.Add(row);
                oldVer = dt.Rows[0]["fileversion"].ToString();
                newVer = oldVer;
            }

            listBox1.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                listBox1.Items.Add(dt.Rows[0]["rownum"].ToString() + "_" + oldVer);
                listBox1.Items.Add(dt.Rows[1]["rownum"].ToString() + "_" + newVer);

                if (lb_locVer.Text == listBox1.Items[1].ToString())
                {
                    label1.ForeColor = Color.Green;
                    lb_locVer.ForeColor = Color.Green;
                    label1.Text += "new version";
                }
                else
                {
                    label1.ForeColor = Color.Black;
                    lb_locVer.ForeColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// 1 auto
        /// </summary>
        /// <param name="programName"></param>
        /// <param name="type"></param>
        void DownFile(string programName,string type)
        {
            string filename = "";
            string filter = "";
            if (type == "1") 
            {
                filter = $"fileversion = '{listBox1.Items[1].ToString().Substring(2, listBox1.Items[1].ToString().Length - 2)}'";
            }
            else if(type == "0")
            {
                filter = $"fileversion = '{listBox1.SelectedItem.ToString().Substring(2, listBox1.SelectedItem.ToString().Length - 2)}'";
            }
            DataRow[] selectRows = dt.Select(filter);
            foreach (DataRow dr in selectRows)
            {
                byte[] binaryData = (byte[])dr["filedata"];

                filename = dr["filename"].ToString();
                string filePath1 = Path.Combine(folderPath, filename);
                File.WriteAllBytes(filePath1, binaryData);
            }

            using (var stream = File.OpenRead(folderPath + "\\" + filename))
            {
                using (var reader = ReaderFactory.Open(stream))
                {
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            string filePath = Path.Combine(folderPath, reader.Entry.Key);
                            string directoryPath = Path.GetDirectoryName(filePath);

                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                            reader.WriteEntryToFile(filePath);
                        }
                    }
                }
            }

            Upprogram(programName);
        }

        void Upprogram(string programName)
        {
            foreach (var process in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(programName)))
            {
                process.Kill();
                process.WaitForExit();
            }

            MoveDirectory(folderPath, exepath);

            using (StreamWriter writer = new StreamWriter(exepath + "\\version.txt"))
            {
                if (listBox1.SelectedIndex == 0)
                {
                    writer.WriteLine("0_" + dt.Rows[0]["fileversion"].ToString());
                }
                else
                {
                    writer.WriteLine("1_" + dt.Rows[1]["fileversion"].ToString());
                }
            }
            //Process.Start(programName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="targetDirectory"></param>
        static void MoveDirectory(string sourceDirectory, string targetDirectory)
        {
            //Directory.CreateDirectory(targetDirectory);
            foreach (string dirPath in Directory.GetDirectories(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourceDirectory, targetDirectory));
            }

            foreach (string newPath in Directory.GetFiles(sourceDirectory, "*.*", SearchOption.AllDirectories))
            {
                string extension = Path.GetExtension(newPath);
                if (extension != ".rar" && extension != ".zip")
                {
                    File.Copy(newPath, newPath.Replace(sourceDirectory, targetDirectory), true);
                }
            }

            Directory.Delete(sourceDirectory, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "rar Files (*.rar)|*.rar|zip Files (*.zip)|*.zip";

            if (ename == "" && cname=="")
            {
                MessageBox.Show("change version");
                return;
            }

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                int cnt = 0;
                string dtVer = DateTime.Now.ToString("yyMMddHHmmss");

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();
                    StringBuilder str = new StringBuilder();
                    str.Append("WITH CTE AS (SELECT top(1) fileversion FROM autoupdata ");
                    str.Append("WHERE ename =@ename AND cname =@cname ORDER BY fileversion ASC) ");
                    str.Append("DELETE FROM autoupdata ");
                    str.Append("WHERE fileversion IN (SELECT fileversion FROM CTE) ");
                    str.Append("AND (SELECT COUNT(*) FROM autoupdata WHERE ename =@ename AND cname =@cname) > 1");

                    using (SqlCommand command = new SqlCommand(str.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ename", ename);
                        command.Parameters.AddWithValue("@cname", cname);
                        command.ExecuteNonQuery();
                    }

                    foreach (string filePath in openFile.FileNames)
                    {
                        byte[] binaryData = File.ReadAllBytes(filePath);
                        string fileName = Path.GetFileName(filePath);

                        string insertQuery = "INSERT INTO autoupdata (ename,cname,fileversion,filedata,filename,uptime) VALUES (@ename,@cname,@fileVer,@BinaryData,@fileName,@uptime)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@BinaryData", binaryData);
                            command.Parameters.AddWithValue("@fileName", fileName);
                            command.Parameters.AddWithValue("@fileVer", dtVer);
                            command.Parameters.AddWithValue("@ename", ename);
                            command.Parameters.AddWithValue("@cname", cname);
                            command.Parameters.AddWithValue("@uptime", DateTime.Now);
                            cnt = command.ExecuteNonQuery();
                        }
                    }

                }
                if (cnt == 1)
                {
                    MessageBox.Show("up finish");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((lb_locVer.Text.Substring(0,1)=="0" && lb_locVer.Text == listBox1.SelectedItem.ToString()) || (lb_locVer.Text.Substring(0, 1) == "1" && listBox1.SelectedItem.ToString() == lb_locVer.Text))
            {
                MessageBox.Show("no new version");
            }
            else
            {
                DownFile(ename,"0");
            }
        }

        private void rb1_MouseDown(object sender, MouseEventArgs e)
        {
            ename = "111";
            cname = "ProgramA";
            CheckVersion(ename, cname);
        }

        private void rb2_MouseDown(object sender, MouseEventArgs e)
        {
            ename = "222";
            cname = "ProgramA";
            CheckVersion(ename, cname);
        }
    }
}
