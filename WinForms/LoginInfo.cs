using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Runtime.Serialization;

namespace WinForms
{
    [DataContract]
    public class LoginInfo
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string pwd { get; set; }

        public void MapToDB(SqlDataReader sdr)
        {
            this.name = sdr["name"].ToString();
            this.pwd = sdr["pwd"].ToString();
        }

        public void Encrypt()
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(this.pwd), 0, this.pwd.Length);
            this.pwd = BitConverter.ToString(res).Replace("-", "");
        }
    }
}
