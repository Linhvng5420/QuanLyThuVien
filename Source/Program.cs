using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace QLThuVien
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Khởi tạo
            QuanLy quanLy = new QuanLy();

            // 2. Nếu đăng nhập-Login thành công, Hiện menu
            if (Login(quanLy) == true)
            {

                ConsoleKey keyInput;

                //nhập phím không hỗ trợ, cho chạy lại
                do
                {
                    //hiển thị menu
                    Console.Clear();
                    PrintMenu("banner");
                    PrintMenu("quanly");

                    //đọc phím
                    keyInput = Console.ReadKey().Key;
                    Console.WriteLine();

                    //nếu phím es, thoát
                    Escape(keyInput);

                    //nếu phím chức năng, chạy menu
                    SelectMenu(keyInput, quanLy);

                } while (keyInput != ConsoleKey.Escape);
            }
        }

        //-----------------
        // Login
        static bool Login(QuanLy quanLy)
        {
            // 1. Khởi tạo
            int demNhapSai = 0; //đếm số lần nhập sai
            string name = "", password = "";
            User user = new User();

            // 2. Nhập TK, MK. Nếu thành công return
            do
            {
                //banner
                PrintMenu("login");

                //2.1 nhập name user, đọc phím, nếu phím khác Enter thì chạy lại
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\tUser: ");
                Console.ForegroundColor = ConsoleColor.White;
                ConsoleKeyInfo info = Console.ReadKey(true);

                while (info.Key != ConsoleKey.Enter)
                {
                    //nhấn Es để thoát
                    if (info.Key == ConsoleKey.Escape)
                    {
                        return false;
                    }

                    //Kiểm tra có phải là phím Backspace hay không
                    if (info.Key != ConsoleKey.Backspace)
                    {
                        name += info.KeyChar;
                        Console.Write(info.KeyChar);
                    }
                    else if (name.Length > 0)
                    {
                        //Bỏ ký tự cuối cùng
                        name = name.Substring(0, name.Length - 1);
                        Console.Write("\b \b");
                    }

                    info = Console.ReadKey(true);
                }

                //2.2 nhập pass, đọc phím, nếu phím khác Enter thì chạy lại
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\tPassword");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(": ");

                info = Console.ReadKey(true);

                while (info.Key != ConsoleKey.Enter)
                {
                    if (info.Key == ConsoleKey.Escape)
                    {
                        return false;
                    }

                    //Kiểm tra có phải là phím Backspace hay không
                    if (info.Key != ConsoleKey.Backspace)
                    {
                        password += info.KeyChar;

                        //Ẩn password
                        Console.Write('*');
                    }
                    else if (password.Length > 0)
                    {
                        //Bỏ ký tự cuối cùng
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b");
                    }
                    info = Console.ReadKey(true);
                }

                //2.3 kiểm tra và thông báo
                Console.WriteLine();
                user = new User(name, password);
                demNhapSai++;

                if (quanLy.LoginUser(user) == true)
                {
                    Console.WriteLine("\t\t\tDANG NHAP THANH CONG, NHAN PHIM BAT KY DE TIEP TUC...");
                    Console.ReadKey();
                    return true;
                }
                else
                {
                    Console.WriteLine("\tNHAP SAI USER HOAC PASS! VUI LONG NHAP LAI!");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (demNhapSai < 3);

            // 3. Nếu nhập sai từ 3 lần, báo lỗi và end.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tDANG NHAP THAT BAI! KET THUC CHUONG TRINH!");
            Console.ReadKey();
            return false;

        }

        // Print Banner - Menu
        static void PrintMenu(string str)
        {
            if (str == "banner")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t\t\t* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *");
                Console.WriteLine("\t\t\t* *                                                                             * *");
                Console.Write("\t\t\t* *                               ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("THU VIEN");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                                      * *");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t\t\t* *                                                                             * *");
                Console.WriteLine("\t\t\t* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *");
                Console.ForegroundColor = ConsoleColor.Green;
            }

            if (str == "quanly")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tNhan phim chon chuc nang: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\t1. QUAN LY SACH.");
                Console.WriteLine("\t2. QUAN LY PHIEU MUON.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tESC. THOAT.");
                Console.Write("\t");
            }

            if (str == "qlsach")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tNhan phim chon chuc nang: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\t1. THEM SACH.");
                Console.WriteLine("\t2. XOA SACH.");
                Console.WriteLine("\t3. QUAY LAI MENU.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tESC. THOAT.");
                Console.Write("\t");
            }

            if (str == "xacnhan")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n\tXAC NHAN Y/N: ");
                Console.ResetColor();
            }

            if (str == "esc")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\t1. QUAY LAI MENU");
                Console.WriteLine("\tESC. THOAT");
                Console.WriteLine("\t");
                Console.ResetColor();
            }

            if (str == "qlpm")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n\tNhan phim chon chuc nang: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\t1. MUON SACH.");
                Console.WriteLine("\t2. TRA SACH.");
                Console.WriteLine("\t3. QUAY LAI MENU.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tESC. THOAT.");
            }

            if (str == "login")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *");
                Console.Write("\t*                                ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("DANG NHAP HE THONG");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                               *");
                Console.WriteLine("\t* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *");
            }
        }

        // Escape
        static void Escape(ConsoleKey keyInput)
        {
            if (keyInput == ConsoleKey.Escape)
            {
                PrintMenu("xacnhan");

                keyInput = Console.ReadKey().Key;
                if (keyInput == ConsoleKey.Y)
                {
                    Environment.Exit(1);
                }
            }

            return;
        }

        // Select Menu
        static void SelectMenu(ConsoleKey keyInput, QuanLy quanLy)
        {
            //ConsoleKey keyInput;

            switch (keyInput)
            {
                // #1
                case ConsoleKey.D1:
                    Console.Clear();
                    QLSach(ref quanLy);
                    break;

                // #2
                case ConsoleKey.D2:
                    Console.Clear();
                    QLPhieuMuon(ref quanLy);
                    break;

                default:
                    Console.Clear();
                    break;
            }
            Console.Clear();
        }

        //-----------------
        // #1: Quản Lý Sách
        static void QLSach(ref QuanLy quanLy)
        {
            ConsoleKey keyInput;
            do
            {
                //banner, print thông tin sách, menu
                PrintMenu("banner");
                quanLy.TableSach();
                PrintMenu("qlsach");

                //nhập phím
                keyInput = Console.ReadKey().Key;

                //phím es thoát
                Escape(keyInput);

                //phím chức năng
                switch (keyInput)
                {
                    // Thêm
                    case ConsoleKey.D1:
                        Console.Clear();
                        PrintMenu("banner");
                        Them(ref quanLy);
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        PrintMenu("banner");
                        Xoa(ref quanLy);

                        break;
                    default:
                        break;
                }

                Console.Clear();
            } while (keyInput != ConsoleKey.D3);
        }

        //thêm
        static void Them(ref QuanLy quanLy)
        {
            ConsoleKey keyInput;

            quanLy.TableSach();
            Console.WriteLine();

            Sach sach = ThemSach();

            if (quanLy.AddSach(sach))
            {
                Console.WriteLine("\n\t\t--THEM SACH THANH CONG!");
            }

            PrintMenu("esc");
            do
            {
                ConsoleKeyInfo info = Console.ReadKey(true);

                keyInput = info.Key;

                Escape(keyInput);

            } while (keyInput != ConsoleKey.D1);
        }

        //thêm sách
        static Sach ThemSach()
        {
            // 1. Khai báo
            string maSach = "", tacGia = "", tenSach = "", nhaSX = "";
            int giaban = -1, nam = -1, soTrang = -1;

            // 2. Nhập, sai chạy lại
            do
            {
                //mã sách
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\tMa sach [ bat buoc ]: ");
                Console.ForegroundColor = ConsoleColor.White;
                maSach = Console.ReadLine();
                if (maSach == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tKhong nhap chuoi rong!");
                }
            } while (maSach == "");

            do
            {
                //tên sách
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\tTen sach [ bat buoc ]: ");
                Console.ForegroundColor = ConsoleColor.White;
                tenSach = Console.ReadLine();
                if (tenSach == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tKhong nhap chuoi rong!");
                }
            } while (tenSach == "");

            do
            {
                //tác giá
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\tTac gia [ bat buoc ]: ");
                Console.ForegroundColor = ConsoleColor.White;
                tacGia = Console.ReadLine();
                if (tacGia == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tKhong nhap chuoi rong!");
                }
            } while (tacGia == "");

            do
            {
                //nhà xuất bản
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\tNha xuat ban [ bat buoc ]: ");
                Console.ForegroundColor = ConsoleColor.White;
                nhaSX = Console.ReadLine();
                if (nhaSX == "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tKhong nhap chuoi rong!");
                }
            } while (nhaSX == "");

            do
            {
                //giá bán
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\tGia ban [ > 0 ]: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    giaban = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    giaban = -1;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tVui long nhap so [ > 0 ]!");
                }
            } while (giaban < 1);

            do
            {
                //năm phát hành
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\tNam phat hanh [ > 1899 ]: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    nam = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    nam = -1;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tVui long nhap nam YYYY > 1899");
                }
            } while (nam < 1900);

            do
            {
                //số trang sách
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\tSo trang [ > 0 ]: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    soTrang = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    soTrang = -1;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tVui long nhap so nguyen duong > 0.");
                }
            } while (soTrang < 1);

            // 3. Khởi tạo kdl sách
            Sach sach = new Sach(maSach, tenSach, tacGia, nhaSX, giaban, nam, soTrang);
            return sach;
        }

        //xoá
        static void Xoa(ref QuanLy quanLy)
        {

            ConsoleKey keyInput;

            quanLy.TableSach();

            Console.Write("\n\tNHAP MA SACH MUON XOA: ");
            string maSach = Console.ReadLine();

            if (quanLy.FindSach(maSach) != null)
            {
                PrintMenu("xacnhan");

                keyInput = Console.ReadKey().Key;
                if (keyInput == ConsoleKey.Y)
                {
                    if (quanLy.RemoveSach(maSach))
                    {
                        Console.WriteLine("\n\t-- SACH XOA THANH CONG!");
                    }
                }
            }
            else
            {
                Console.ResetColor();
                Console.WriteLine("\t-- KHONG TIM THAY SACH!");
            }

            PrintMenu("esc");
            do
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                keyInput = info.Key;

                Escape(keyInput);

            } while (keyInput != ConsoleKey.D1);
        }

        //-----------------
        // #2: Quản Lý Phiếu Mượn
        static void QLPhieuMuon(ref QuanLy quanLy)
        {
            ConsoleKey keyInput;

            do
            {
                Console.Clear();
                PrintMenu("banner");
                quanLy.TablePhieu();
                PrintMenu("qlpm");

                keyInput = Console.ReadKey(true).Key;
                Escape(keyInput);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Muon(ref quanLy, keyInput);
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        Tra(ref quanLy, keyInput);
                        break;

                    default:
                        break;
                }
            } while (keyInput != ConsoleKey.D3);

        }

        //mượn sách
        static void Muon(ref QuanLy quanLy, ConsoleKey keyInput)
        {
            PrintMenu("banner");
            quanLy.TablePhieu();
            PrintMenu("qlpm");

            quanLy.MuonSach();

            PrintMenu("esc");
            do
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                keyInput = info.Key;

                Escape(keyInput);
                Console.WriteLine();

            } while (keyInput != ConsoleKey.D1);
        }

        //trả sách
        static void Tra(ref QuanLy quanLy, ConsoleKey keyInput)
        {
            PrintMenu("banner");
            quanLy.TablePhieu();
            quanLy.TraSach();

            PrintMenu("esc");
            do
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                keyInput = info.Key;

                Escape(keyInput);
                Console.WriteLine();

            } while (keyInput != ConsoleKey.D1);
        }
    }
}
