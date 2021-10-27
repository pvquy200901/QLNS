using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BTL_QLNS.BUS;
using System.Data.SqlClient;
using BTL_QLNS.MODEL;

namespace BTL_QLNS
{
    public partial class DangKy : Form
    {
        QLNS context = new QLNS();
        public DangKy()
        {
            InitializeComponent();
        }

        private void DangKy_Load(object sender, EventArgs e)
        {

        }
        
        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            DangNhap frmdn = new DangNhap();
            frmdn.Show();
            this.Hide();
        }

        private void btnDangky_Click(object sender, EventArgs e)
        {
            User_BUS ub = new User_BUS();
            try
            {
                if (txtNhaplai.Text == txtMatkhau.Text)
                {
                    ub.insertUser(txtTaikhoan.Text, txtMatkhau.Text, txtMaNv.Text);
                    MessageBox.Show("Đăng ký tài khoản thành công !");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mật khẩu nhập lại không đúng !");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Bạn nhập sai cú pháp !");
            }
           
        }
    }
}
