using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLySinhVienN5
{
    public partial class FormDangNhapCB : Form
    {
        public FormDangNhapCB()
        {
            InitializeComponent();
        }
        string Nguon = @"Data Source=CUSUHAO\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        SqlConnection KetNoi;
        SqlCommand ThucHien;
        SqlDataAdapter Doc;
        string lenh = @"";

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Form1 j = new Form1();
            this.Hide();
            j.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string tenDN = TextBoxTenDN.Text.Trim();
            string matKhau = TextBoxMK.Text.Trim();
            bool timThay = false;

            using (SqlConnection KetNoi = new SqlConnection(Nguon))
            {
                string lenh = "SELECT TenDK, MatKhau FROM DangKyQuanLy";
                SqlCommand ThucHien = new SqlCommand(lenh, KetNoi);

                KetNoi.Open();
                SqlDataReader Doc = ThucHien.ExecuteReader();

                while (Doc.Read())
                {
                    string tenTrongDB = Doc["TenDK"].ToString().Trim();
                    string mkTrongDB = Doc["MatKhau"].ToString().Trim();

                    if (tenDN == tenTrongDB && matKhau == mkTrongDB)
                    {
                        timThay = true;
                        break;
                    }
                }

                KetNoi.Close();
            }

            if (timThay)
            {
                DialogResult result = MessageBox.Show("Bạn đăng nhập thành công  ",
                                                    "Thông báo",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);

                // Kiểm tra nếu người dùng ấn OK thì chuyển form
                if (result == DialogResult.OK)
                {
                    FormThemSinhVien h = new FormThemSinhVien();
                    h.Show();
                    this.Hide();
                }
            }
            else
            {
                label2.Visible = true;
            }
        }
    }
}
