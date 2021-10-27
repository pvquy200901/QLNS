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
using Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
using BTL_QLNS.MODEL;
using System.Globalization;
using System.Configuration;

namespace BTL_QLNS
{
    
    public partial class Quanlynhanvien : Form
    {
        QLNS context = new QLNS();
        public Quanlynhanvien()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ManHinhChinh frmmch = new ManHinhChinh();
            frmmch.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Quanlynhanvien_Load(object sender, EventArgs e)
        {
            try
            {
                QLNS context = new QLNS();
                List<DUAN> listDuAn = context.DUANs.ToList();
                List<PHONGBAN> listPhongBan = context.PHONGBANs.ToList();
                List<NHANVIEN> listNhanVien = context.NHANVIENs.ToList();
                FillDuAnCombobox(listDuAn);
                FillPhongBanCombobox(listPhongBan);
                BindGrid(listNhanVien);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BindGrid(List<NHANVIEN> listNhanVien)
        {
            dgvNhanVien.Rows.Clear();
            foreach (var item in listNhanVien)
            {
                int index = dgvNhanVien.Rows.Add();
                dgvNhanVien.Rows[index].Cells[0].Value = item.id_Nv;
                dgvNhanVien.Rows[index].Cells[1].Value = item.name_Nv;
                dgvNhanVien.Rows[index].Cells[2].Value = item.ngaysinh_Nv;
                dgvNhanVien.Rows[index].Cells[3].Value = item.diachi_Nv;
                dgvNhanVien.Rows[index].Cells[4].Value = item.luong_Nv;
                dgvNhanVien.Rows[index].Cells[5].Value = item.PHONGBAN.name_Pb;
                dgvNhanVien.Rows[index].Cells[6].Value = item.DUAN.name_Da; 
            }
        }

        private void FillPhongBanCombobox(List<PHONGBAN> listPhongBan)
        {
            this.cbxPhongban.DataSource = listPhongBan;
            this.cbxPhongban.DisplayMember = "name_Pb";
            this.cbxPhongban.ValueMember = "id_Pb";
        }

        private void FillDuAnCombobox(List<DUAN> listDuAn)
        {
            this.cbxDuan.DataSource = listDuAn;
            this.cbxDuan.DisplayMember = "name_Da";
            this.cbxDuan.ValueMember = "id_Da";
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            ManHinhChinh frmmch = new ManHinhChinh();
            frmmch.Show();
            this.Hide();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            try
            {
                if (check() == false)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                }
                else
                {
                    int selectedRow = GetSelectedRow(txtMaNv.Text);
                    if (selectedRow == -1)
                    {
                        NHANVIEN s = new NHANVIEN()
                        {
                           id_Nv = txtMaNv.Text,
                           name_Nv = txtTenNv.Text,
                           diachi_Nv = txtDiachi.Text,
                           luong_Nv = int.Parse(txtLuong.Text),
                           ngaysinh_Nv = dtpNgaysinh.Value,
                           id_Da = cbxDuan.SelectedValue.ToString(),
                           id_Pb = cbxPhongban.SelectedValue.ToString()
                        };
                        context.NHANVIENs.Add(s);
                        context.SaveChanges();
                        reloadDGV();
                        refresh();
                        MessageBox.Show("Thêm mới dữ liệu thành công");
                    }
                    else
                    {

                        MessageBox.Show("Sinh Viên đã tồn tại");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void refresh()
        {
            txtMaNv.Text = "";
            txtTenNv.Text = "";
            txtDiachi.Text = "";
            txtLuong.Text = "";
            txtSearch.Text = "";
        }

        private void reloadDGV()
        {
            List<NHANVIEN> listNV = context.NHANVIENs.ToList();
            BindGrid(listNV);
        }

        private int GetSelectedRow(string id_Nv)
        {
            for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
            {
                if (dgvNhanVien.Rows[i].Cells[0].Value.ToString() == id_Nv)
                {
                    return i;
                }
            }
            return -1;
        }

        private bool check()
        {
            if (string.IsNullOrWhiteSpace(txtDiachi.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtLuong.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtMaNv.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtTenNv.Text))
                return false;
            if (string.IsNullOrWhiteSpace(dtpNgaysinh.Text))
                return false;

            return true;
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
                    NHANVIEN dbUpdate = context.NHANVIENs.FirstOrDefault(p => p.id_Nv == txtMaNv.Text);
                    if (dbUpdate != null)
                    {

                        dbUpdate.name_Nv = txtTenNv.Text;
                        dbUpdate.diachi_Nv = txtDiachi.Text;
                        dbUpdate.luong_Nv = int.Parse(txtLuong.Text);
                        dbUpdate.ngaysinh_Nv = dtpNgaysinh.Value;
                        dbUpdate.id_Pb = cbxPhongban.SelectedValue.ToString();
                        dbUpdate.id_Da = cbxDuan.SelectedValue.ToString();
                        context.SaveChanges();
                        reloadDGV();
                        refresh();
                        MessageBox.Show("Sửa dữ liệu thành công");
                    }
                    else
                    {

                        MessageBox.Show("Không tìm thấy sinh viên cần sửa");
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
            NHANVIEN dbDelete = context.NHANVIENs.FirstOrDefault(p => p.id_Nv == txtMaNv.Text);
            if (dbDelete != null)
            {
                context.NHANVIENs.Remove(dbDelete);
                context.SaveChanges();
                reloadDGV();
                refresh();
                MessageBox.Show("Xóa dữ liệu thành công");
            }
            else
            {

                MessageBox.Show("Không tìm thấy nhân viên cần xóa");
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvNhanVien.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
                {
                    txtMaNv.Text = dgvNhanVien.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    txtTenNv.Text = dgvNhanVien.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                    txtDiachi.Text = dgvNhanVien.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                    txtLuong.Text = dgvNhanVien.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                    cbxPhongban.SelectedIndex = cbxPhongban.FindString(dgvNhanVien.Rows[e.RowIndex].Cells["id_Pb"].FormattedValue.ToString());
                    cbxDuan.SelectedIndex = cbxDuan.FindString(dgvNhanVien.Rows[e.RowIndex].Cells["id_Da"].FormattedValue.ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
       
        NhanVien_BUS nvb = new NhanVien_BUS();
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                List<NHANVIEN> search = (from ele in context.NHANVIENs
                                         where ele.name_Nv.ToLower().Contains(txtSearch.Text.Trim()) || ele.id_Nv.ToLower().Contains(txtSearch.Text.Trim())
                                         select ele).ToList();
                BindGrid(search);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtDiachi.Text = "";
            txtLuong.Text = "";
            txtMaNv.Text = "";
            txtSearch.Text = "";
            txtTenNv.Text = "";
            Quanlynhanvien_Load(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
