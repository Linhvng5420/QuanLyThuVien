using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThuVien
{
    internal class BanDoc
    {
        // Attributes
        private string maBD;
        private string name;
        private DateTime ngayDK;

        private BanDoc() { }

        public BanDoc(string maBD, string name)
        {
            this.maBD = maBD;
            this.name = name;
            this.ngayDK = DateTime.Now;
        }

        public BanDoc(string maBD, string name, string ngayDK)
        {
            this.maBD = maBD;
            this.name = name;
            string[] s = ngayDK.Split('/');
            this.ngayDK = new DateTime(int.Parse(s[2]), int.Parse(s[0]), int.Parse(s[1]));
        }

        public string MaBD { get => maBD; set => maBD = value; }
        public string Name { get => name; set => name = value; }
        public DateTime NgayDK { get => ngayDK; set => ngayDK = value; }

        // Methods
        public string Print()
        {
            return $"|{maBD,-10}|{name,-20}|{ngayDK.ToString("MM/dd/yyyy"),-20}|";
        }
        public string PrintFile()
        {
            return $"{maBD}#{name}#{ngayDK.ToString("MM/dd/yyyy")}";
        }
    }
}

