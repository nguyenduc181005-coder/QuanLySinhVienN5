using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVienN5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void đăngKýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDangKyCB h = new FormDangKyCB();
            h.Show();
            this.Hide();
         }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormThemSinhVien j = new FormThemSinhVien();
            j.Show();
            this.Hide();
        }
    }
}
