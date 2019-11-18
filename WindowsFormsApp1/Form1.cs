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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        BUS_User BUS_User = new BUS_User();
        BUS_ItemList BUS_ItemList = new BUS_ItemList();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PasteClipboard();
        }
        private void PasteClipboard()
        {

            try
            {
                string s = Clipboard.GetText();
                DataTable dt = new DataTable();
                List<string> rowContent = new List<string>();
                while (s.Contains("\r\n"))
                {
                    rowContent.Add(s.Substring(0, s.IndexOf("\r\n")));
                    s = s.Substring(s.IndexOf("\r\n") + 2);
                }
                int rowCount = rowContent.Count;
                string sampleRow = rowContent[0];
                int colCount = 0;
                while (sampleRow.Contains("\t"))
                {
                    colCount++;
                    sampleRow = sampleRow.Substring(sampleRow.IndexOf("\t") + 1);
                }
                for (int index = 0; index <= colCount; index++)
                {
                    dt.Columns.Add();
                }
                foreach (string cellContent in rowContent)
                {
                    string tempStr = cellContent;
                    DataRow row = dt.NewRow();
                    string[] cellArr = tempStr.Split('\t');
                    int cellIndex = 0;
                    foreach (string cell in cellArr)
                    {
                        row[cellIndex] = cell;
                        cellIndex++;
                    }
                    dt.Rows.Add(row);
                }
                dgData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int complete = 0, count = 0;
            foreach (DataGridViewRow row in dgData.Rows)
            {
                try{
                    DTO_Departurement dept = new DTO_Departurement();
                    dept.Code = row.Cells[0].Value.ToString();
                    dept.Eng = row.Cells[1].Value.ToString();
                    dept.Vie = row.Cells[2].Value.ToString();
                    if (BUS_User.addCustomer(dept))
                    {
                        complete++;

                    }
                }
                catch
                {

                }
                count++;
            }
            MessageBox.Show("Hoàn thành "+complete+" / "+(count));

            dgData.DataSource = BUS_User.getDeptTable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dgData.DataSource = BUS_ItemList.getCategoriesMenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dgData.DataSource = BUS_User.getDeptTable();
        }

        private void dgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgData_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            
        }

        private void dgData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            label1.Text = "Total Rows: " + dgData.Rows.Count;
        }

        private void dgData_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            label1.Text = "Total Rows: " + dgData.Rows.Count;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(dgData.SelectedRows.Count > 0)
            {
                int count = 0, complete = 0;
                foreach (DataGridViewRow row in dgData.SelectedRows)
                {
                    if (BUS_User.deleteDept(row.Cells[0].Value.ToString()))
                    {
                        complete++;
                    }
                    count++;
                }
                MessageBox.Show("Hoàn thành "+complete+"/"+count );
                dgData.DataSource = BUS_User.getDeptTable();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int complete = 0, count = 0;
            foreach (DataGridViewRow row in dgData.Rows)
            {
                try
                {    
                    
                    string categories = row.Cells[0].Value.ToString();
                    string menu = row.Cells[1].Value.ToString();
                    if (BUS_ItemList.AddCategoriesMenu(categories,menu))
                    {
                        complete++;
                    }
                }
                catch
                {

                }
                count++;
            }
            MessageBox.Show("Hoàn thành " + complete + " / " + (count));

            dgData.DataSource = BUS_ItemList.getCategoriesMenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dgData.SelectedRows.Count > 0)
            {
                int count = 0, complete = 0;
                foreach (DataGridViewRow row in dgData.SelectedRows)
                {
                    if (BUS_ItemList.DeleteCategoriesMenu(row.Cells[0].Value.ToString(),row.Cells[1].Value.ToString()))
                    {
                        complete++;
                    }
                    count++;
                }
                MessageBox.Show("Hoàn thành " + complete + "/" + count);
                dgData.DataSource = BUS_ItemList.getCategoriesMenu();
            }
        }

        private void btnGetItemList_Click(object sender, EventArgs e)
        {
            dgData.DataSource=BUS_ItemList.GetItemList();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddItemList_Click(object sender, EventArgs e)
        {

        }
    }
}
