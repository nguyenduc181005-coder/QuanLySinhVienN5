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
    public partial class FormLop : Form
    {
        public FormLop()
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            FormNganh m = new FormNganh();
            m.Show();
            this.Hide();
        }

        private void ButtonThem_Click(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            lenh = @"INSERT INTO dbo.LopHoc
              (TenLop, ID_Nganh, GhiChu)
               VALUES (@TenLop,@ID_Nganh,@GhiChu)";

            ThucHien = new SqlCommand(lenh, KetNoi);
            ThucHien.Parameters.Add("@TenLop", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@ID_Nganh", SqlDbType.Int);
            ThucHien.Parameters.Add("@GhiChu", SqlDbType.NVarChar);


            ThucHien.Parameters["@TenLop"].Value = TextBoxTenLop.Text.Trim();
            ThucHien.Parameters["@ID_Nganh"].Value = IDNGANH;
            ThucHien.Parameters["@GhiChu"].Value = TextBoxLienHe.Text.Trim();


            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
             hienthi();
        }

        private void FormLop_Load(object sender, EventArgs e)
        {
            using (SqlConnection KetNoi = new SqlConnection(Nguon))
            {
                dataGridView2.Rows.Clear();
                string lenh = "SELECT TenNganh FROM Nganh";
                SqlCommand ThucHien = new SqlCommand(lenh, KetNoi);

                KetNoi.Open();
                SqlDataReader Doc = ThucHien.ExecuteReader();
                
                while (Doc.Read())
                {


                    ComboBoxTenNganh.Items.Add(Doc[0]);

                }

                KetNoi.Close();
                hienthi();
            }
        }
        int IDNGANH;
        private void ComboBoxTenNganh_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection KetNoi = new SqlConnection(Nguon))
            {
                dataGridView2.Rows.Clear();
                string lenh = "SELECT MaNganh FROM Nganh where (TenNganh =@TenNganh)";
                SqlCommand ThucHien = new SqlCommand(lenh, KetNoi);
                ThucHien.Parameters.Add("@TenNganh", SqlDbType.NVarChar);
                ThucHien.Parameters["@TenNganh"].Value = ComboBoxTenNganh.Text;

                KetNoi.Open();
                SqlDataReader Doc = ThucHien.ExecuteReader();

                while (Doc.Read())
                {

                    IDNGANH  = (int)(Doc[0]);
                   
                }

                KetNoi.Close();
            }
        }
        void hienthi()
        {
            using (SqlConnection KetNoi = new SqlConnection(Nguon))
            {
                dataGridView2.Rows.Clear();
                string lenh = "SELECT LopHoc.MaLop, LopHoc.TenLop, LopHoc.GhiChu, Nganh.MaNganh, Nganh.TenNganh FROM LopHoc INNER JOIN Nganh ON LopHoc.ID_Nganh = Nganh.MaNganh";

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
                    dataGridView2.Rows[i].Cells[4].Value = Doc[4];
                    i++;
                }

                KetNoi.Close();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
