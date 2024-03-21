using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThuVien
{
    internal class QuanLy
    {
        // Attributes
        private LinkedList<Sach> sachs;
        private LinkedList<BanDoc> banDocs;
        private LinkedList<PhieuMuon> phieuMuons;
        private LinkedList<User> users;

        private string userFile = "Admin.txt";
        private string bookFile = "Sach.txt";
        private string phieuMuonFile = "PhieuMuon.txt";
        private string banDocFile = "BanDoc.txt";

        public QuanLy()
        {
            SelectBanDoc();
            SelectPhieuMuon();
            SelectUser();
            SelectSach();
        }

        //////////////////////////

        /// <summary>
        /// Lấy danh sách user trong file Admin.txt
        /// Dữ liệu lưu vào Linkedlist users
        /// </summary>
        /// <returns>Lấy thành công return true</returns>
        public bool SelectUser()
        {
            users = new LinkedList<User>();

            try
            {
                using (StreamReader reader = new StreamReader(userFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] strings = line.Split('#');
                        User u = new User(strings[0], strings[1]);
                        users.AddLast(u);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("\tUser Error: Lỗi dữ liệu !");
                return false;
            }

        }

        /// <summary>
        /// Lấy danh sách sách trong file Sach.txt
        /// Dữ liệu lưu vào Linkedlist sachs
        /// </summary>
        /// <returns>Lấy thành công return true</returns>
        public bool SelectSach()
        {
            sachs = new LinkedList<Sach>();
            try
            {
                using (StreamReader reader = new StreamReader(bookFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] strings = line.Split('#');
                        Sach sach = new Sach(strings[0], strings[1], strings[2], strings[3], double.Parse(strings[4]), int.Parse(strings[5]), int.Parse(strings[6]), strings[7], int.Parse(strings[8]));
                        sachs.AddLast(sach);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("\tSach error: Loi du lieu!");

                return false;
            }
        }
        /// <summary>
        /// Lấy sanh sách phiếu mượn trong file PhieuMuon.txt
        /// Dữ liệu lưu vào Linkedlist phieuMuons
        /// </summary>
        /// <returns>Lấy thành công return true</returns>
        public bool SelectPhieuMuon()
        {
            phieuMuons = new LinkedList<PhieuMuon>();
            try
            {
                using (StreamReader reader = new StreamReader(phieuMuonFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] strings = line.Split('#');
                        PhieuMuon phieuMuon = new PhieuMuon(int.Parse(strings[0]), strings[1], strings[2], strings[3], strings[4], int.Parse(strings[5]));
                        phieuMuons.AddLast(phieuMuon);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("\tPhieu muon error: loi du lieu!");
                return false;
            }
        }
        /// <summary>
        /// Lấy danh sách bạn đọc trong file Bandoc.txt
        /// Dữ liệu lưu vào Linkedlist banDocs
        /// </summary>
        /// <returns>Lấy thành công return true</returns>
        public bool SelectBanDoc()
        {
            banDocs = new LinkedList<BanDoc>();
            try
            {
                using (StreamReader reader = new StreamReader(banDocFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] s = line.Split('#');
                        BanDoc banDoc = new BanDoc(s[0], s[1], s[2]);
                        banDocs.AddLast(banDoc);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("\tBan doc error: Loi du lieu");
                return false;
            }
        }

        ///////////////////////

        /// <summary>
        /// Lưu danh sách sách vào file Sach.txt.
        /// Ghi đè dữ liệu cũ trong file.
        /// </summary>
        /// <returns>Khi update thành công return true</returns>
        public bool UpdateSach()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(bookFile))
                {
                    LinkedListNode<Sach> sachNode = sachs.First;
                    while (sachNode != null)
                    {
                        writer.WriteLine(sachNode.Value.SachPrintFile());
                        sachNode = sachNode.Next;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("\tSach error: Loi ghi file!");
                return false;
            }

        }
        /// <summary>
        /// Lưu danh sách vào file PhieuMuon.txt.
        /// Ghi đè dữ liệu cũ trong file.
        /// </summary>
        /// <returns>Khi update thành công return true</returns>
        public bool UpdatePhieuMuon()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(phieuMuonFile))
                {
                    LinkedListNode<PhieuMuon> phieuMuonNode = phieuMuons.First;
                    while (phieuMuonNode != null)
                    {
                        writer.WriteLine(phieuMuonNode.Value.PrintFile());
                        phieuMuonNode = phieuMuonNode.Next;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("\tPhieu muon erro: Loi ghi file!");
                return false;
            }
        }

        ///////////////////////

        /// <summary>
        /// Thêm sách mới vào danh sách.
        /// Nếu MaSach tồn tại kết thúc hàm, return false.
        /// Update danh sách và lấy lại dữ liệu.
        /// </summary>
        /// <param name="sach">Sách muốn thêm</param>
        /// <returns></returns>
        public bool AddSach(Sach sach)
        {
            if (FindSach(sach.MaSach) != null)
            {
                Console.WriteLine("\tKhong Them Duoc: Ma sach da ton tai!");
                return false;
            }
            try
            {
                sach.TinhTrang = 0;
                sachs.AddLast(sach);
                if (!UpdateSach())
                {
                    return false;
                }
                if (!SelectSach())
                {
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\tSach Error: Khong the them sach!");
                return false;
            }
            return true;

        }
        /// <summary>
        /// Thêm phiếu mượn.
        /// Update và lấy lại danh sách nếu thêm thành công.
        /// </summary>
        /// <param name="phieuMuon"></param>
        /// <returns></returns>
        public bool AddPhieuMuon(PhieuMuon phieuMuon)
        {
            try
            {

                if (FindBanDoc(phieuMuon.MaBD) == null)
                {
                    return false;
                }

                LinkedListNode<Sach> node;
                if ((node = FindSach(phieuMuon.MaSach)) != null)
                {
                    if (node.Value.TinhTrang != 0)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                DateTime dateTime = DateTime.Now;
                int x = dateTime.Hour * 1800 + dateTime.Minute * 60 + dateTime.Second;
                phieuMuon.SoPM = phieuMuon.SoPM + 1;
                phieuMuons.AddLast(phieuMuon);

                node.Value.TinhTrang = 1;
                phieuMuon.TinhTrang = 1;

                if (!UpdatePhieuMuon())
                {
                    return false;

                }

                UpdateSach();
                SelectPhieuMuon();
                SelectSach();
            }
            catch (Exception)
            {
                Console.WriteLine("\tPhieu muon error:Khong the them phieu muon!");
                return false;
            }
            return true;
        }
        ////////////////////////

        /// <summary>
        /// Kiểm tra đăng nhập.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool LoginUser(User user)
        {
            LinkedListNode<User> node = users.First;

            while (node != null)
            {
                if (node.Value.UserName.Equals(user.UserName) && node.Value.Password.Equals(user.Password))
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        ///////////////////////

        /// <summary>
        /// Tìm kiếm và trả về thông tin sách bằng mã sách.
        /// </summary>
        /// <param name="maSach"></param>
        /// <returns></returns>
        public LinkedListNode<Sach> FindSach(string maSach)
        {
            LinkedListNode<Sach> node = sachs.First;
            while (node != null)
            {
                if (node.Value.MaSach.Equals(maSach))
                {
                    return node;
                }
                node = node.Next;
            }
            return null;
        }
        /// <summary>
        /// Tìm kiếm số phiếu mượn và trả về thông tin phiếu mượn bằng mã phiếu mượn.
        /// </summary>
        /// <param name="soPM"></param>
        /// <returns></returns>
        public LinkedListNode<PhieuMuon> FindPhieu(int soPM)
        {
            LinkedListNode<PhieuMuon> node = phieuMuons.First;
            while (node != null)
            {
                if (node.Value.SoPM == soPM)
                {
                    return node;
                }
                node = node.Next;
            }
            return null;
        }
        /// <summary>
        /// tìm kiếm và trả về thông tin bạn đọc bằng mã bạn đọc
        /// </summary>
        /// <param name="maBanDoc"></param>
        /// <returns></returns>
        public LinkedListNode<BanDoc> FindBanDoc(string maBanDoc)
        {
            LinkedListNode<BanDoc> node = banDocs.First;
            while (node != null)
            {
                if (node.Value.MaBD.Equals(maBanDoc))
                {
                    return node;
                }
                node = node.Next;
            }
            return null;
        }

        ///////////////////////

        /// <summary>
        /// Xoá sách khỏi danh sách và update lại dữ liệu.
        /// Nếu sách đang được mượn sẽ dừng quá trình và thông báo.
        /// </summary>
        /// <param name="maSach"></param>
        /// <returns></returns>
        public bool RemoveSach(string maSach)
        {
            try
            {
                LinkedListNode<Sach> node = FindSach(maSach);
                if (node != null)
                {
                    if (node.Value.TinhTrang == 0)
                    {
                        sachs.Remove(node);
                        UpdateSach();
                        SelectSach();
                        return true;
                    }
                    Console.WriteLine("\n\tSACH DANG DUOC MUON!");
                    return false;
                }
                Console.WriteLine("\n\tKHONG TIM THAY SACH!");
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ////////////////////////


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sach"></param>
        /// <param name="banDoc"></param>
        /// <returns></returns>
        public bool MuonSach()
        {
            string maBD; string maSach;
            LinkedListNode<BanDoc> nodeBanDoc;
            LinkedListNode<Sach> nodeSach;
            int count = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n\tNHAP MA BAN DOC: ");
                Console.ForegroundColor = ConsoleColor.White;
                maBD = Console.ReadLine();
                nodeBanDoc = FindBanDoc(maBD);

                if (nodeBanDoc != null)
                {
                    count = 0;

                    Console.WriteLine();
                    TableSach();
                    Console.WriteLine();

                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("\tNHAP MA SACH: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        maSach = Console.ReadLine();
                        Console.WriteLine();

                        nodeSach = FindSach(maSach);
                        if (nodeSach != null)
                        {
                            if (nodeSach.Value.TinhTrang == 0)
                            {
                                PhieuMuon phieuMuon = new PhieuMuon(maBD, maSach);
                                if (AddPhieuMuon(phieuMuon) == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\tMuon thanh cong!");
                                    return true;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\tMuon that bai!");
                                    return false;
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\t--Sach da bi nguoi khac muon!");
                                return false;
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\tNhap Sai!!\n");
                        count++;
                    } while (nodeSach == null && count < 3);
                }

                if (count < 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tNhap Sai!!");
                    count++;
                }

            } while (nodeBanDoc == null && count < 3);

            if (count >= 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\t--NHAP SAI QUA 3 LAN! HUY!");
            }
            return false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maPhieu"></param>
        /// <returns></returns>
        public void TraSach()
        {
            int count = 0;
            int soPM;
            LinkedListNode<PhieuMuon> phieuMuon = null;
            LinkedListNode<Sach> nodeSach = null;

            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n\tNHAP MA PHIEU MUON: ");
                Console.ForegroundColor = ConsoleColor.White;
                soPM = int.Parse(Console.ReadLine());

                phieuMuon = FindPhieu(soPM);

                if (phieuMuon != null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\n\t{"Ma PM",-20}|{"Ma BD",-10}|{"Ma sach",-10}|{"Ngay muon",-12}|{"Ngay tra",-12}|{"Tinh trang",-10}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(phieuMuon.Value.ToString());

                    if (string.Compare(phieuMuon.Value.GetTinhTrang(), "[Da tra]") == 0)
                    {
                        Console.WriteLine("\n\t--BAN CHUA MUON QUYEN SACH NAO.");
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n\tXAC NHAN TRA SACH Y/N: ");
                    ConsoleKey key = Console.ReadKey().Key;
                    Console.WriteLine();

                    if (key == ConsoleKey.Y)
                    {
                        nodeSach = FindSach(phieuMuon.Value.MaSach);
                        phieuMuon.Value.TinhTrang = 0;
                        UpdatePhieuMuon();
                        nodeSach.Value.TinhTrang = 0;
                        UpdateSach();
                        Console.ResetColor();

                        Console.WriteLine("\t-- DA TRA THANH CONG.");
                        return;
                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tMa phieu muon sai! Nhap lai!");
                    count++;
                }
            } while (phieuMuon == null && count < 3);

            if (count >= 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tNhap sai qua 3 lan! Ve menu!");
            }
            return;
        }



        /////////////////////////
        // Print ds thông tin sách
        public void TableSach()
        {
            Console.WriteLine("\tDanh sach sach:");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t{"Ma sach",-10}|{"Ten sach",-10}|{"Tac gia",-15}|{"Nha xuat ban",-15}|{"Gia ban(VND)",-15}|{"Nam PH",-10}|{"So trang",-10}|{"Ngay nhap kho",-15}|{"Tinh trang",-10}");

            LinkedListNode<Sach> node = sachs.First;

            Console.ForegroundColor = ConsoleColor.Blue;
            while (node != null)
            {
                Console.WriteLine(node.Value.ToString());
                node = node.Next;
            }
            Console.ForegroundColor = ConsoleColor.Green;
        }

        // Print ds thôn tin phiếu mượn
        public void TablePhieu()
        {
            Console.WriteLine("\tDanh sach phieu muon:");
            Console.WriteLine($"\t{"Ma PM",-20}|{"Ma BD",-10}|{"Ma sach",-10}|{"Ngay muon",-12}|{"Ngay tra",-12}|{"Tinh trang",-10}");
            LinkedListNode<PhieuMuon> node = phieuMuons.First;
            while (node != null)
            {
                Console.WriteLine(node.Value.ToString());
                node = node.Next;
            }
        }

    }
}
