using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO_WareHouse;
using BUS_WareHouse;

namespace Kaizen_Supplier_1._0
{
    public partial class AccountLog : Form
    {
        BUS_User BUS_User = new BUS_User();
        public DTO_User user;
        public AccountLog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtInputID.Text.Trim() == "" || txtInputPwd.Text.Trim() == "")
            {
                MessageBox.Show("Thêm không thành công", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                txtInputID.Text = "";
                txtInputPwd.Text = "";
                return;
            }
            string inputID = txtInputID.Text.Trim().ToUpper();
            string inputPwd = txtInputPwd.Text.Trim().ToUpper();
            user = BUS_User.getUser(inputID,inputPwd);
            if(user == null)
            {
                MessageBox.Show("Đăng nhập không thành công, sai thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                txtInputID.Text = "";
                txtInputPwd.Text = "";
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void AccountLog_Load(object sender, EventArgs e)
        {

        }

        private void txtInputPwd_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtInputPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void txtInputID_TextChanged(object sender, EventArgs e)
        {
            
            txtInputID.Text = RemoveSpecialCharacters(txtInputID.Text).ToUpper();
            txtInputID.SelectionStart = txtInputID.Text.Length;
        }
        private string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == '-')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Replace(@"--", @"-");
        }

        private void txtInputPwd_TextChanged(object sender, EventArgs e)
        {
            txtInputPwd.Text = RemoveSpecialCharacters(txtInputPwd.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
