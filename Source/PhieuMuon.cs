using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThuVien
{
    internal class PhieuMuon
    {
        // Attributes
        private string maPhieuMuon;
        private string maBD;
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
            this.maBD = maBD;
            this.maSach = maSach;
            MaPhieuMuon = "";
            tinhTrang = 0;
            ngayMuon = DateTime.Now;
            ngayTra = ngayMuon.AddDays(7);
        }
        public PhieuMuon(string maPhieuMuon, string maBD, string maSach, string ngayMuon, string ngayTra, int tinhTrang)
        {
            this.maPhieuMuon = maPhieuMuon;
            this.maBD = maBD;
            this.maSach = maSach;
            string[] s = ngayMuon.Split('/');
            this.ngayMuon = new DateTime(int.Parse(s[2]), int.Parse(s[0]), int.Parse(s[1]));
            s = ngayTra.Split('/');
            this.ngayTra = new DateTime(int.Parse(s[2]), int.Parse(s[0]), int.Parse(s[1]));
            this.tinhTrang = tinhTrang;
        }

        public string MaPhieuMuon { get => maPhieuMuon; set => maPhieuMuon = value; }
        public string MaBD { get => maBD; set => maBD = value; }
        public string MaSach { get => maSach; set => maSach = value; }
        public DateTime NgayMuon { get => ngayMuon; set => ngayMuon = value; }
        public DateTime NgayTra { get => ngayTra; set => ngayTra = value; }
        public int TinhTrang { get => tinhTrang; set => tinhTrang = value; }

        public string GetTinhTrang()
        {
            if (tinhTrang == 0)
            {
                return "Chua tra";
            }
            else { return "Da tra"; }
        }

        // Methods
        override
        public string ToString()
        {
            return $"\t{MaPhieuMuon,-20}|{maBD,-10}|{maSach,-10}|{ngayMuon.ToString("MM/dd/yyyy"),-12}|{ngayTra.ToString("MM/dd/yyyy"),-12}|{GetTinhTrang(),-10}";

        }
        public string PrintFile()
        {
            return $"{maPhieuMuon}#{maBD}#{MaSach}#{ngayMuon.ToString("MM/dd/yyyy")}#{ngayTra.ToString("MM/dd/yyyy")}#{tinhTrang}";
        }
    }
}
