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
    public partial class FormNganh : Form
    {
        public FormNganh()
        {
            InitializeComponent();
        }
        string Nguon = @"Data Source=CUSUHAO\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        SqlConnection KetNoi;
        SqlCommand ThucHien;
        SqlDataAdapter Doc;
        string lenh = @"";

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            FormThemSinhVien k = new FormThemSinhVien();
            k.Show();
            this.Hide();
        }

        private void ButtonThem_Click(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            lenh = @"INSERT INTO Nganh
                  (TenNganh, GhiChu)
                  VALUES (@TenNganh,@GhiChu)";

            ThucHien = new SqlCommand(lenh, KetNoi);
            ThucHien.Parameters.Add("@TenNganh", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@GhiChu", SqlDbType.NVarChar);


            ThucHien.Parameters["@TenNganh"].Value = TextBoxTenNganh.Text.Trim();
            ThucHien.Parameters["@GhiChu"].Value = TextBoxLienHe.Text.Trim();


            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            hienthi();
        }

        void hienthi()
        {
            using (SqlConnection KetNoi = new SqlConnection(Nguon))
            {
                dataGridView1.Rows.Clear();
                string lenh = "SELECT MaNganh, TenNganh, GhiChu FROM Nganh";
                SqlCommand ThucHien = new SqlCommand(lenh, KetNoi);

                KetNoi.Open();
                SqlDataReader Doc = ThucHien.ExecuteReader();
                int i = 0;
                while (Doc.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = Doc[0];
                    dataGridView1.Rows[i].Cells[1].Value = Doc[1];
                    dataGridView1.Rows[i].Cells[2].Value = Doc[2];
                    i++;


                }

                KetNoi.Close();
            }
        }

        private void FormNganh_Load(object sender, EventArgs e)
        {
            hienthi();
        }

        private void ButtonSua_Click(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            lenh = @"UPDATE Nganh
            SET       TenNganh = @TenNganh, GhiChu = @GhiChu
             WHERE (MaNganh = @Original_MaNganh)";

            ThucHien = new SqlCommand(lenh, KetNoi);
            ThucHien.Parameters.Add("@TenNganh", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@Original_MaNganh", SqlDbType.NVarChar);

            ThucHien.Parameters["@TenNganh"].Value = TextBoxTenNganh.Text.Trim();
            ThucHien.Parameters["@GhiChu"].Value = TextBoxLienHe.Text.Trim();
            ThucHien.Parameters["@Original_MaNganh"].Value = dataGridView1.CurrentRow.Cells[0].Value;

            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            hienthi();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBoxTenNganh.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TextBoxLienHe.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void ButtonXoa_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("bạn có muốn xóa" + TextBoxTenNganh.Text, "chú ý ",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                KetNoi = new SqlConnection(Nguon);
                lenh = @"DELETE FROM Nganh
                WHERE (MaNganh = @Original_MaNganh)";
                ThucHien = new SqlCommand(lenh, KetNoi);
                ThucHien.Parameters.Add("@Original_MaNganh", SqlDbType.Int);
                ThucHien.Parameters["@Original_MaNganh"].Value = dataGridView1.CurrentRow.Cells[0].Value;
                KetNoi.Open();
                ThucHien.ExecuteNonQuery();
                KetNoi.Close();
                hienthi();
                TextBoxTenNganh.Text = "";
                TextBoxLienHe.Text = "";
            }
            else { }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            FormLop j = new FormLop();
            j.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FormMonHoc i = new FormMonHoc();
            i.Show();
            this.Hide();
        }
    }
}
