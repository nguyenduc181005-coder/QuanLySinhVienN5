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
    public partial class FormMonHoc : Form
    {
        public FormMonHoc()
        {
            InitializeComponent();
        }
        string Nguon = @"Data Source=CUSUHAO\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        SqlConnection KetNoi;
        SqlCommand ThucHien;
        SqlDataAdapter Doc;
        string lenh = @"";

        private void ButtonThem_Click(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            lenh = @"INSERT INTO MonHoc
             (TenMonHoc, SoTin, Thoigian)
            VALUES (@TenMonHoc,@SoTin,@Thoigian)";

            ThucHien = new SqlCommand(lenh, KetNoi);
            ThucHien.Parameters.Add("@TenMonHoc", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@SoTin", SqlDbType.Int);
            ThucHien.Parameters.Add("@Thoigian", SqlDbType.Date);


            ThucHien.Parameters["@TenMonHoc"].Value = TextBoxTenMH.Text.Trim();
            ThucHien.Parameters["@SoTin"].Value = TextBoxTinChi.Text.Trim();
            ThucHien.Parameters["@Thoigian"].Value = DateTimePicker1.Text.Trim();


            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            hienthi();
        }
        
        void hienthi()
        {
            using (SqlConnection KetNoi = new SqlConnection(Nguon))
            {
                dataGridView2.Rows.Clear();
                string lenh = "SELECT MaMonHoc, TenMonHoc, SoTin, Thoigian FROM MonHoc";
                SqlCommand ThucHien = new SqlCommand(lenh, KetNoi);

                KetNoi.Open();
                SqlDataReader Doc = ThucHien.ExecuteReader();
                int i = 0;
                while (Doc.Read())
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = Doc[0];
                    dataGridView2.Rows[i].Cells[1].Value = Doc[1];
                    dataGridView2.Rows[i].Cells[2].Value = Doc[2];
                    dataGridView2.Rows[i].Cells[3].Value = Doc[3];
                    i++;


                }

                KetNoi.Close();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBoxTenMH.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            TextBoxTinChi.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            DateTimePicker1.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
        }

        private void ButtonSua_Click(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            lenh = @"UPDATE MonHoc
             SET       TenMonHoc = @TenMonHoc, SoTin = @SoTin, Thoigian = @Thoigian
             WHERE (MaMonHoc = @Ma)";

            ThucHien = new SqlCommand(lenh, KetNoi);
            ThucHien.Parameters.Add("@TenMonHoc", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@SoTin", SqlDbType.Int);
            ThucHien.Parameters.Add("@Thoigian", SqlDbType.Date);
            ThucHien.Parameters.Add("@Ma", SqlDbType.NVarChar);


            ThucHien.Parameters["@TenMonHoc"].Value = TextBoxTenMH.Text.Trim();
            ThucHien.Parameters["@SoTin"].Value = TextBoxTinChi.Text.Trim();
            ThucHien.Parameters["@Thoigian"].Value = DateTimePicker1.Text.Trim();
            ThucHien.Parameters["@Ma"].Value = dataGridView2.CurrentRow.Cells[0].Value;
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            hienthi();
        }

        private void FormMonHoc_Load(object sender, EventArgs e)
        {
            hienthi();
        }

        private void ButtonXoa_Click(object sender, EventArgs e)
        {

            DialogResult d = MessageBox.Show("bạn có muốn xóa" + TextBoxTenMH.Text, "chú ý ",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                KetNoi = new SqlConnection(Nguon);
                lenh = @"DELETE FROM MonHoc
              WHERE (MaMonHoc = @Ma)";
                ThucHien = new SqlCommand(lenh, KetNoi);
                ThucHien.Parameters.Add("@Ma", SqlDbType.Int);
                ThucHien.Parameters["@Ma"].Value = dataGridView2.CurrentRow.Cells[0].Value;
                KetNoi.Open();
                ThucHien.ExecuteNonQuery();
                KetNoi.Close();
                hienthi();
                TextBoxTenMH.Text = "";
                TextBoxTinChi.Text = "";
                DateTimePicker1.Text = "";
            }
            else { }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            FormNganh k = new FormNganh();
            k.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            FormLop h = new FormLop();
            h.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            FormThemSinhVien n = new FormThemSinhVien();
            n.Show();
            this.Hide();
        }
    }
}
