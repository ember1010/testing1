using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kaizen_Supplier_1._0
{
    public partial class FrmGetExportQty : Form
    {
        string Lot { get; set; }
        string ItemCode { get; set; }
        int CurrentQty { get; set; }

        public int exportQty = -1;
        public FrmGetExportQty()
        {
            InitializeComponent();
        }

        private void FrmGetExportQty_Load(object sender, EventArgs e)
        {
            richTextBox1.Text += ("Tên Lot: " + Lot);
            richTextBox1.Text += "\n";
            richTextBox1.Text += ("ItemCode: " + ItemCode);
            richTextBox1.Text += "\n";
            richTextBox1.Text += ("Số lượng còn: " + CurrentQty);
            richTextBox1.Text += "\n";
        }
        public FrmGetExportQty(int currentQty,string lot,string itemCode) : this(){
            Lot = lot;
            CurrentQty = currentQty;
            ItemCode = itemCode;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int getQty = -1;
            if (e.KeyCode == Keys.Enter)
            {
                if(int.TryParse(textBox1.Text,out getQty) && getQty > 0 && getQty <= CurrentQty && CurrentQty % getQty == 0)
                {
                    exportQty = getQty;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Số lượng không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                textBox1.Text = RemoveNonNumberCharacters(textBox1.Text);
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }
        private string RemoveNonNumberCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        private void FrmGetExportQty_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
