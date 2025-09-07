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
    public partial class FormDangKyCB : Form
    {
        public FormDangKyCB()
        {
            InitializeComponent();
        }

        string Nguon = @"Data Source=CUSUHAO\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        SqlConnection KetNoi;
        SqlCommand ThucHien;
        SqlDataAdapter Doc;
        string lenh = @"";
        private readonly string email;

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Form1 j = new Form1();
            this.Hide();
            j.Show();
        }

        private void ButtonGui_Click(object sender, EventArgs e)
        {

            // Duyệt và validate từng phần tử
            if (!ValidateInput())
                return;

            KetNoi = new SqlConnection(Nguon);
            lenh = @"INSERT INTO DangKyQuanLy
             (Ho, Ten, NgaySinh, DiaChi, SDT, Email, TenDK, MatKhau)
             VALUES (@Ho,@Ten,@NgaySinh,@DiaChi,@SDT,@Email,@TenDK,@MatKhau)";

            ThucHien = new SqlCommand(lenh, KetNoi);
            ThucHien.Parameters.Add("@Ho", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@Ten", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@NgaySinh", SqlDbType.Date);
            ThucHien.Parameters.Add("@DiaChi", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@SDT", SqlDbType.Int);
            ThucHien.Parameters.Add("@Email", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@TenDK", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@MatKhau", SqlDbType.NVarChar);

            ThucHien.Parameters["@Ho"].Value = TextBoxHo.Text.Trim();
            ThucHien.Parameters["@Ten"].Value = TextBoxTen.Text.Trim();
            ThucHien.Parameters["@NgaySinh"].Value = DateTimePicker.Text;
            ThucHien.Parameters["@DiaChi"].Value = TextBoxDiaChi.Text.Trim();
            ThucHien.Parameters["@SDT"].Value = Convert.ToInt32(TextBoxSDT.Text);

            // Xử lý Email có thể null hoặc rỗng
            if (string.IsNullOrWhiteSpace(TextBoxEmail.Text))
            {
                ThucHien.Parameters["@Email"].Value = DBNull.Value;
            }
            else
            {
                ThucHien.Parameters["@Email"].Value = TextBoxEmail.Text.Trim();
            }

            ThucHien.Parameters["@TenDK"].Value = TextBoxTenTK.Text.Trim();
            ThucHien.Parameters["@MatKhau"].Value = TextBoxMatKhau.Text;

            try
            {
                KetNoi.Open();
                ThucHien.ExecuteNonQuery();
                KetNoi.Close();

                // Clear các textbox sau khi thêm thành công
                TextBoxHo.Text = "";
                TextBoxTen.Text = "";
                DateTimePicker.Text = "";
                TextBoxDiaChi.Text = "";
                TextBoxSDT.Text = "";
                TextBoxEmail.Text = "";
                TextBoxTenTK.Text = "";
                TextBoxMatKhau.Text = "";

                // Hiển thị thông báo đăng ký thành công
                DialogResult result = MessageBox.Show("Bạn đăng ký thành công!",
                                                     "Thông báo",
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Information);

                // Kiểm tra nếu người dùng ấn OK thì chuyển form
                if (result == DialogResult.OK)
                {
                    FormDangKyCB  f = new FormDangKyCB();
                    f.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (KetNoi.State == ConnectionState.Open)
                {
                    KetNoi.Close();
                }
            }
         }


        private bool ValidateInput()
        {
            // 1. Kiểm tra Họ
            if (string.IsNullOrWhiteSpace(TextBoxHo.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxHo.Focus();
                return false;
            }

            // 2. Kiểm tra Tên
            if (string.IsNullOrWhiteSpace(TextBoxTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxTen.Focus();
                return false;
            }

            // 3. Kiểm tra Ngày sinh
            DateTime ngaySinh;
            if (!DateTime.TryParse(DateTimePicker.Text, out ngaySinh))
            {
                MessageBox.Show("Ngày sinh không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DateTimePicker.Focus();
                return false;
            }

            // Kiểm tra tuổi (ví dụ: phải từ 16 tuổi trở lên)
            if (DateTime.Now.Year - ngaySinh.Year < 16)
            {
                MessageBox.Show("Phải từ 16 tuổi trở lên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DateTimePicker.Focus();
                return false;
            }

            // 4. Kiểm tra Địa chỉ
            if (string.IsNullOrWhiteSpace(TextBoxDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập Địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxDiaChi.Focus();
                return false;
            }

            // 5. Kiểm tra Số điện thoại
            if (string.IsNullOrWhiteSpace(TextBoxSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxSDT.Focus();
                return false;
            }

            int sdt;
            if (!int.TryParse(TextBoxSDT.Text, out sdt))
            {
                MessageBox.Show("Số điện thoại phải là số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxSDT.Focus();
                return false;
            }

            // Kiểm tra độ dài số điện thoại (10-11 số)
            if (TextBoxSDT.Text.Length < 10 || TextBoxSDT.Text.Length > 11)
            {
                MessageBox.Show("Số điện thoại phải có 10-11 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxSDT.Focus();
                return false;
            }

            // 6. Kiểm tra Email (không bắt buộc nhưng nếu nhập thì phải đúng định dạng)
            

            // 7. Kiểm tra Tên đăng nhập
            if (string.IsNullOrWhiteSpace(TextBoxTenTK.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxTenTK.Focus();
                return false;
            }

            // Kiểm tra độ dài tên đăng nhập
            if (TextBoxTenTK.Text.Length < 6)
            {
                MessageBox.Show("Tên đăng nhập phải có ít nhất 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxTenTK.Focus();
                return false;
            }

            // 8. Kiểm tra Mật khẩu
            if (string.IsNullOrWhiteSpace(TextBoxMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxMatKhau.Focus();
                return false;
            }

            // Kiểm tra độ mạnh mật khẩu
            if (TextBoxMatKhau.Text.Length < 8)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxMatKhau.Focus();
                return false;
            }

            return true; // Tất cả đều hợp lệ
        }

        private bool IsValidEmail(string text)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
       
    }
  }  

