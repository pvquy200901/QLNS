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

    public partial class Quanlyduan : Form
    {
        QLNS context = new QLNS();
        public Quanlyduan()
        {
            InitializeComponent();
        }

        private void Quanlyduan_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    QLNS context = new QLNS();
                    List<DUAN> listDuAn = context.DUANs.ToList();     
                    List<NHANVIEN> listNhanVien = context.NHANVIENs.ToList();
                    BindGrid(listDuAn);

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

        private int GetSelectedRow(string id_Da)
        {
            for (int i = 0; i < dgvDuAn.Rows.Count; i++)
            {
                if (dgvDuAn.Rows[i].Cells[0].Value.ToString() == id_Da)
                {
                    return i;
                }
            }
            return -1;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            ManHinhChinh frmmch = new ManHinhChinh();
            frmmch.Show();
            this.Hide();
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
                List<DUAN> search = (from ele in context.DUANs
                                         where ele.id_Da.ToLower().Contains(txtSearch.Text.Trim()) || ele.name_Da.ToLower().Contains(txtSearch.Text.Trim())
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
                        DUAN s = new DUAN()
                        {
                            id_Da = txtMaDA.Text,
                            name_Da = txtTenDA.Text,
                            sonv_Da = sonv,
                            mota_Da = txtMotaDA.Text,                       
                        };
                        context.DUANs.Add(s);
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

        private void refresh()
        {
            txtMaDA.Text = "";
            txtTenDA.Text = "";
            txtSoNVDA.Text = "";
            txtMotaDA.Text = "";
        }

        private void reloadDGV()
        {
            List<DUAN> listDuAn = context.DUANs.ToList();
            BindGrid(listDuAn);
        }

        private bool check()
        {
            if (string.IsNullOrWhiteSpace(txtMaDA.Text))
                return false;
            if (string.IsNullOrWhiteSpace(txtMotaDA.Text))
                return false;        
            if (string.IsNullOrWhiteSpace(txtTenDA.Text))
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
                    DUAN dbUpdate = context.DUANs.FirstOrDefault(p => p.id_Da == txtMaDA.Text);
                    if (dbUpdate != null)
                    {

                        dbUpdate.name_Da = txtTenDA.Text;
                        dbUpdate.sonv_Da = int.Parse(txtSoNVDA.Text);
                        dbUpdate.mota_Da = txtMotaDA.Text;
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
            DUAN dbDelete = context.DUANs.FirstOrDefault(p => p.id_Da == txtMaDA.Text);
            if (dbDelete != null)
            {
                context.DUANs.Remove(dbDelete);
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

        
        private void BindGrid(List<DUAN> listDuAn)
        {
            dgvDuAn.Rows.Clear();
            foreach (var item in listDuAn)
            {
                int index = dgvDuAn.Rows.Add();
                dgvDuAn.Rows[index].Cells[0].Value = item.id_Da;
                dgvDuAn.Rows[index].Cells[1].Value = item.name_Da;
                dgvDuAn.Rows[index].Cells[2].Value = item.sonv_Da;
                dgvDuAn.Rows[index].Cells[3].Value = item.mota_Da;
            }
        }

        private void dgvDuAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    txtMaDA.Text = dgvDuAn.Rows[index].Cells[0].Value.ToString();
                    txtTenDA.Text = dgvDuAn.Rows[index].Cells[1].Value.ToString();
                    txtSoNVDA.Text = dgvDuAn.Rows[index].Cells[2].Value.ToString();
                    txtMotaDA.Text = dgvDuAn.Rows[index].Cells[3].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }



        private void btnreset_Click(object sender, EventArgs e)
        {
            txtMaDA.Text = "";
            txtMotaDA.Text = "";
            txtSearch.Text = "";
            txtTenDA.Text = "";
            txtSoNVDA.Text = "";
            Quanlyduan_Load(sender, e);
        }
    }
}
