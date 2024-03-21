using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace QLThuVien
{
    internal class PhieuMuon
    {
        // Attributes
        private int soPM;
        private string maDG;
        private string maSach;
        private DateTime ngayMuon;
        private DateTime ngayTra;
        //chưa trả là 0, đã trả là 1
        private int tinhTrang;


        public PhieuMuon()
        {

            tinhTrang = 0;
            ngayMuon = DateTime.Now;
            ngayTra = ngayMuon.AddDays(7);
        }
        public PhieuMuon(string maBD, string maSach)
        {
            this.maDG = maBD;
            this.maSach = maSach;
            tinhTrang = 0;
            ngayMuon = DateTime.Now;
            ngayTra = ngayMuon.AddDays(7);
        }
        public PhieuMuon(int soPM, string maBD, string maSach, string ngayMuon, string ngayTra, int tinhTrang)
        {
            this.soPM=soPM;
            this.maDG = maBD;
            this.maSach = maSach;
            string[] s = ngayMuon.Split('/');
            this.ngayMuon = new DateTime(int.Parse(s[2]), int.Parse(s[0]), int.Parse(s[1]));
            s = ngayTra.Split('/');
            this.ngayTra = new DateTime(int.Parse(s[2]), int.Parse(s[0]), int.Parse(s[1]));
            this.tinhTrang = tinhTrang;
        }

        public int SoPM { get => soPM; set => soPM = PhieuMuon.Count(); }
        public string MaBD { get => maDG; set => maDG = value; }
        public string MaSach { get => maSach; set => maSach = value; }
        public DateTime NgayMuon { get => ngayMuon; set => ngayMuon = value; }
        public DateTime NgayTra { get => ngayTra; set => ngayTra = value; }
        public int TinhTrang { get => tinhTrang; set => tinhTrang = value; }

        public string GetTinhTrang()
        {
            if (tinhTrang == 1)
            {
                return "Chua tra";
            }
            else { return "[Da tra]"; }
        }

        // Methods
        override
        public string ToString()
        {
            return $"\t{SoPM,-20}|{maDG,-10}|{maSach,-10}|{ngayMuon.ToString("MM/dd/yyyy"),-12}|{ngayTra.ToString("MM/dd/yyyy"),-12}|{GetTinhTrang(),-10}";

        }
        public string PrintFile()
        {
            return $"{soPM}#{maDG}#{MaSach}#{ngayMuon.ToString("MM/dd/yyyy")}#{ngayTra.ToString("MM/dd/yyyy")}#{tinhTrang}";
        }

        static int Count()
        {
            FileStream f = new FileStream("PhieuMuon.txt", FileMode.Open);
            StreamReader s = new StreamReader(f);
            int count = 1;

            while (s.ReadLine() != null)
            {
                count++;
            }
            s.Close();
            f.Close();

            return count;
        }
    }

}
