using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThuVien
{
    internal class User
    {
        //Attributes
        private string userName;
        private string password;

        public User() { }

        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }
        public string UserName { get => userName; set => userName = value; }

        /// <summary>
        /// Passwoord được hash
        /// </summary>
        public string Password { get => password; set => password = value; }

        //Methods
        public string PrintFile()
        {
            return $"{userName}#{password}";
        }


        /*
         * Chức Năng: Lưu tài khoản.
         */
    }
}
