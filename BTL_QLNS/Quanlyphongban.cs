using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BTL_QLNS.BUS;
using BTL_QLNS.MODEL;

namespace BTL_QLNS
{
   
    public partial class Quanlyphongban : Form
    {
        QLNS context = new QLNS();
        public Quanlyphongban()
        {
            InitializeComponent();
        }
        PhongBan_BUS pbb = new PhongBan_BUS();
        private void btnExit_Click(object sender, EventArgs e)
        {
            ManHinhChinh frmmch = new ManHinhChinh();
            frmmch.Show();
            this.Hide();
        }
        private void refresh()
        {
            txtMaPB.Text = "";
            txtMota.Text = "";
            txtSearch.Text = "";
            txtSoNV.Text = "";
            txtTenPB.Text = "";
        }
        private void BindGrid(List<PHONGBAN> listPhongBan)
        {
            dgvPhongban.Rows.Clear();
            foreach (var item in listPhongBan)
            {
                int index = dgvPhongban.Rows.Add();
                dgvPhongban.Rows[index].Cells[0].Value = item.id_Pb;
                dgvPhongban.Rows[index].Cells[1].Value = item.name_Pb;
                dgvPhongban.Rows[index].Cells[2].Value = item.sonv_Pb;
                dgvPhongban.Rows[index].Cells[3].Value = item.mota_Pb;
            }
        }
        private void reloadDGV()
        {
            List<PHONGBAN> listDuAn = context.PHONGBANs.ToList();
            BindGrid(listDuAn);
        }

        private bool check()
        {
            if (string.IsNullOrWhiteSpace(txtMaPB.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtMota.Text))
                return false;
            
            if (string.IsNullOrWhiteSpace(txtTenPB.Text))
                return false;

            return true;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManHinhChinh frmmch = new ManHinhChinh();
            frmmch.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<PHONGBAN> search = (from ele in context.PHONGBANs
                                     where ele.id_Pb.ToLower().Contains(txtSearch.Text.Trim()) || ele.name_Pb.ToLower().Contains(txtSearch.Text.Trim())
                                     select ele).ToList();
                BindGrid(search);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            int sonv = 0;
            try
            {
                if (check() == false)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                }
                else
                {

                    {
                        PHONGBAN s = new PHONGBAN()
                        {
                            id_Pb = txtMaPB.Text,
                            name_Pb = txtTenPB.Text,
                            sonv_Pb = sonv,
                            mota_Pb = txtMota.Text,
                        };
                        context.PHONGBANs.Add(s);
                        context.SaveChanges();
                        reloadDGV();
                        refresh();
                        MessageBox.Show("Thêm mới dữ liệu thành công");
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Dự án đã tồn tại");
                
            }
}
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (check() == false)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                }
                else
                {
                    PHONGBAN dbUpdate = context.PHONGBANs.FirstOrDefault(p => p.id_Pb == txtMaPB.Text);
                    if (dbUpdate != null)
                    {

                        dbUpdate.name_Pb = txtTenPB.Text;
                        dbUpdate.sonv_Pb = int.Parse(txtSoNV.Text);
                        dbUpdate.mota_Pb = txtMota.Text;
                        context.SaveChanges();
                        reloadDGV();
                        refresh();
                        MessageBox.Show("Sửa dữ liệu thành công");
                    }
                    else
                    {

                        MessageBox.Show("Không tìm thấy dự án cần sửa");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            PHONGBAN dbDelete = context.PHONGBANs.FirstOrDefault(p => p.id_Pb == txtMaPB.Text);
            if (dbDelete != null)
            {
                context.PHONGBANs.Remove(dbDelete);
                context.SaveChanges();
                reloadDGV();
                refresh();
                MessageBox.Show("Xóa dữ liệu thành công");
            }
            else
            {

                MessageBox.Show("Không tìm thấy dự án cần xóa");
            }
        }

        private void dgvPhongban_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    txtMaPB.Text = dgvPhongban.Rows[index].Cells[0].Value.ToString();
                    txtTenPB.Text = dgvPhongban.Rows[index].Cells[1].Value.ToString();
                    txtSoNV.Text = dgvPhongban.Rows[index].Cells[2].Value.ToString();
                    txtMota.Text = dgvPhongban.Rows[index].Cells[3].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Quanlyphongban_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    QLNS context = new QLNS();
                    List<PHONGBAN> listPhongBan = context.PHONGBANs.ToList();
                    List<NHANVIEN> listNhanVien = context.NHANVIENs.ToList();
                    BindGrid(listPhongBan);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtMaPB.Text = "";
            txtMota.Text = "";
            txtSearch.Text = "";
            txtSoNV.Text = "";
            txtTenPB.Text = "";
            Quanlyphongban_Load(sender, e);
        }
    }
}
