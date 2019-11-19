using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS_WareHouse;
using DTO_WareHouse;
using QRCoder;

namespace Kaizen_Supplier_1._0
{
    public partial class Form1 : Form
    {
        DTO_User LoginUser;
        Dictionary<string, List<string>> dicCategoriesMenu = new Dictionary<string, List<string>>();
        BUS_ItemList busItemList = new BUS_ItemList();
        List<DTO_Departurement> listDept = new List<DTO_Departurement>();
        BUS_Warehouse_Data busWarehouseData = new BUS_Warehouse_Data();
        BUS_User busUser = new BUS_User();
        List<DTO_Item> listItemNewItem;
        List<DTO_Warehouse_Export_Data> lstExportItem = new List<DTO_Warehouse_Export_Data>();
        int barcodeImageMargin = 5;
        public Form1()
        {
            InitializeComponent();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

            dataGridView1.DataSource = busItemList.GetItemList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = busItemList.GetItemList();
            dataGridView1.ClearSelection();
            ClearTextBoxTab2();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int qtyTrans, MOQ = 1;
            int.TryParse(txtMOQ.Text, out MOQ);
            if (txtItemCode.Text.Length == 4
                && txtItemName.Text != ""
                && cbItemOrderType.Text != ""
                && cbItemType.Text != ""
                && cbPackingUnit.Text != ""
                && cbExportUnit.Text != ""
                && int.TryParse(txtQtyTrans.Text, out qtyTrans)
                && (txtPurchaseCode.Text.Length == 9
                && ((txtPurchaseCode.Text.ToCharArray()[0] == 'N' && cbItemOrderType.Text == "CATALOG")
                || (txtPurchaseCode.Text.ToCharArray()[0] == 'Q' && cbItemOrderType.Text == "BÁO GIÁ")
                || (txtPurchaseCode.Text == "X00000000" && cbItemOrderType.Text == "EBS")
                )))
            {
                // Tạo DTo

                txtItemCode.Text = txtItemCode.Text.ToUpper();
                DTO_Item item = new DTO_Item(); // Vì ID tự tăng nên để ID số gì cũng dc
                item.Code = txtItemCode.Text;
                item.Name = txtItemName.Text;
                item.ENGName = txtItemEngName.Text;
                item.Type = cbItemType.Text;
                item.OrderType = cbItemOrderType.Text;
                item.Maker = txtItemMaker.Text;
                item.PackingUnit = cbPackingUnit.Text;
                item.ExportUnit = cbExportUnit.Text;
                item.QtyTrans = qtyTrans;
                item.MOQ = MOQ;
                item.PurchaseCode = txtPurchaseCode.Text;
                item.PartNumber = txtItemPartNumber.Text;
                item.TechInfo = txtNoteInfo.Text;
                // Them
                if (busItemList.AddItemList(item))
                {
                    MessageBox.Show("Thêm thành công", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    dataGridView1.DataSource = busItemList.GetItemList();
                    dataGridView1.ClearSelection();
                }
                else
                {
                    MessageBox.Show("Thêm ko thành công", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {

                string condition = Environment.NewLine;
                if (!(txtItemCode.Text.Length == 4)) condition += " - " + "ITEM CODE" + Environment.NewLine;
                if (!(txtItemName.Text != "")) condition += " - " + "Tên vật tư" + Environment.NewLine;
                if (!(cbExportUnit.Text != "")) condition += " - " + "Đơn vị xuất" + Environment.NewLine;
                if (!(cbItemType.Text != "")) condition += " - " + "Loại vật tư" + Environment.NewLine;
                if (!(cbItemOrderType.Text != "")) condition += " - " + "Loại đặt hàng" + Environment.NewLine;
                if (!(int.TryParse(txtQtyTrans.Text, out qtyTrans))) condition += " - " + "Hệ số quy đổi" + Environment.NewLine;
                if (!(cbPackingUnit.Text != "")) condition += " - " + "Đơn vị đóng gói" + Environment.NewLine;
                if (!((txtPurchaseCode.Text.ToCharArray()[0] == 'N' && cbItemOrderType.Text == "CATALOG")
                || (txtPurchaseCode.Text.ToCharArray()[0] == 'Q' && cbItemOrderType.Text == "BÁO GIÁ")
                || (txtPurchaseCode.Text == "X00000000" && cbItemOrderType.Text == "EBS"))) condition += " - " + "Mã mua hàng" + Environment.NewLine;
                MessageBox.Show("Xin hãy nhập đầy đủ" + condition, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            DTO_Item item = new DTO_Item();
            item.Code = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            item.Name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            item.ENGName = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            item.Type = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            item.OrderType = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            item.Maker = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            item.PackingUnit = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            item.ExportUnit = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            item.QtyTrans = Convert.ToInt16(dataGridView1.SelectedRows[0].Cells[8].Value.ToString());
            item.AddedTime = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            item.PurchaseCode = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
            item.PartNumber = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
            item.MOQ = Convert.ToInt16(dataGridView1.SelectedRows[0].Cells[12].Value.ToString());
            item.TechInfo = dataGridView1.SelectedRows[0].Cells[13].Value.ToString();
            txtItemCode.Text = item.Code;
            txtItemName.Text = item.Name;
            txtItemEngName.Text = item.ENGName;
            cbItemType.SelectedIndex = cbItemType.Items.IndexOf(item.Type);
            cbItemOrderType.SelectedIndex = cbItemOrderType.Items.IndexOf(item.OrderType);
            txtItemMaker.Text = item.Maker;
            cbPackingUnit.SelectedIndex = cbPackingUnit.Items.IndexOf(item.PackingUnit);
            cbExportUnit.SelectedIndex = cbExportUnit.Items.IndexOf(item.ExportUnit);
            txtQtyTrans.Text = item.QtyTrans.ToString();
            txtPurchaseCode.Text = item.PurchaseCode;
            txtItemPartNumber.Text = item.PartNumber;
            txtMOQ.Text = item.MOQ.ToString();
            txtNoteInfo.Text = item.TechInfo;
            if (cbItemOrderType.Text == "EBS")
            {
                txtPurchaseCode.Enabled = false;
                txtPurchaseCode.Text = "X00000000";
            }
            else if (cbItemOrderType.Text == "BÁO GIÁ")
            {
                txtPurchaseCode.Enabled = false;
                txtPurchaseCode.Text = "X00000000";
            }
            else
            {
                txtPurchaseCode.Enabled = true;
                txtPurchaseCode.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn item để sửa chữa");
                return;
            }
            int qtyTrans, MOQ = 1;
            int.TryParse(txtMOQ.Text, out MOQ);
            if (txtItemCode.Text.Length == 4 && txtItemName.Text != "" && cbExportUnit.Text != "" && cbItemType.Text != "" && cbPackingUnit.Text != "" && cbExportUnit.Text != "" && int.TryParse(txtQtyTrans.Text, out qtyTrans) && ((txtPurchaseCode.Text.ToCharArray()[0] == 'N' && cbItemOrderType.Text == "CATALOG")
                || (txtPurchaseCode.Text.ToCharArray()[0] == 'Q' && cbItemOrderType.Text == "BÁO GIÁ")
                || (txtPurchaseCode.Text == "X00000000" && cbItemOrderType.Text == "EBS")))
            {
                DTO_Item item = new DTO_Item();
                item.Code = txtItemCode.Text;
                item.Name = txtItemName.Text;
                item.ENGName = txtItemEngName.Text;
                item.Type = cbItemType.Text;
                item.OrderType = cbItemOrderType.Text;
                item.Maker = txtItemMaker.Text;
                item.PackingUnit = cbPackingUnit.Text;
                item.ExportUnit = cbExportUnit.Text;
                item.QtyTrans = qtyTrans;
                item.MOQ = MOQ;
                item.PurchaseCode = txtPurchaseCode.Text;
                item.PartNumber = txtItemPartNumber.Text;
                item.TechInfo = txtNoteInfo.Text;
                if (busItemList.EditItemList(item, dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Đã sửa thành công", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    dataGridView1.DataSource = busItemList.GetItemList();
                    dataGridView1.ClearSelection();
                    ClearTextBoxTab2();
                }
                else
                {
                    MessageBox.Show("Sửa không thành công", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                string condition = Environment.NewLine;
                if (!(txtItemCode.Text.Length == 4)) condition += " - " + "ITEM CODE" + Environment.NewLine;
                if (!(txtItemName.Text != "")) condition += " - " + "Tên vật tư" + Environment.NewLine;
                if (!(cbExportUnit.Text != "")) condition += " - " + "Đơn vị xuất" + Environment.NewLine;
                if (!(cbItemType.Text != "")) condition += " - " + "Loại vật tư" + Environment.NewLine;
                if (!(cbItemOrderType.Text != "")) condition += " - " + "Loại đặt hàng" + Environment.NewLine;
                if (!(int.TryParse(txtQtyTrans.Text, out qtyTrans))) condition += " - " + "Hệ số quy đổi" + Environment.NewLine;
                if (!(cbPackingUnit.Text != "")) condition += " - " + "Đơn vị đóng gói" + Environment.NewLine;
                if (!((txtPurchaseCode.Text.ToCharArray()[0] == 'N' && cbItemOrderType.Text == "CATALOG")
                || (txtPurchaseCode.Text.ToCharArray()[0] == 'Q' && cbItemOrderType.Text == "BÁO GIÁ")
                || (txtPurchaseCode.Text == "X00000000" && cbItemOrderType.Text == "EBS"))) condition += " - " + "Mã mua hàng" + Environment.NewLine;

                MessageBox.Show("Thông tin đưa ra có vấn đề!" + condition, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn item để xóa chữa");
                return;
            }
            string selectedItemToDelete = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            if (busItemList.DeleteItemList(selectedItemToDelete))
            {
                MessageBox.Show("Đã xóa thành công", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                dataGridView1.DataSource = busItemList.GetItemList();
                dataGridView1.ClearSelection();
                ClearTextBoxTab2();
            }
            else
            {
                MessageBox.Show("Xóa không thành công", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private void ClearTextBoxTab2()
        {
            txtItemCode.Clear();
            txtItemName.Clear();
            txtItemEngName.Clear();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                dataGridView1.DataSource = busItemList.GetItemList();
                dataGridView1.ClearSelection();
                ClearTextBoxTab2();
            }
            if (tabControl1.SelectedIndex == 0)
            {

            }
            if (tabControl1.SelectedIndex == 1)
            {
                txtBarcodeInput.Select();
            }
            if (tabControl1.SelectedIndex == 3)
            {
                //dgvWarehouseHistory.DataSource = busWarehouseData.getWarehouseDataFull();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoginFormLoading();
            CheckBoxLoginAuthorize();
            dataGridView1.DataSource = busItemList.GetItemList();
            dataGridView1.ClearSelection();
            //dataGridView2.DataSource = busWarehouseData.getWarehouseData();
            //dataGridView2.ClearSelection();
            listDept = busUser.getDept();
            foreach (DTO_Departurement dept in listDept)
            {
                cbCustomer.Items.Add(dept.Vie);
            }
            busItemList.getCategoriesMenu(dicCategoriesMenu);
            foreach (KeyValuePair<string, List<string>> keyValuePair in dicCategoriesMenu)
            {
                if (keyValuePair.Key == "Loại vật tư")
                {
                    foreach (string menu in keyValuePair.Value)
                    {
                        cbItemType.Items.Add(menu);
                        cbInputItemType.Items.Add(menu);
                    }
                }
                else if (keyValuePair.Key == "Đơn vị xuất")
                {
                    foreach (string menu in keyValuePair.Value)
                    {
                        cbExportUnit.Items.Add(menu);
                        cbPackingUnit.Items.Add(menu);
                    }
                }
                else if (keyValuePair.Key == "Tiền tệ")
                {
                    foreach (string menu in keyValuePair.Value)
                    {
                        if (!cbMoneyUnit.Items.Contains(menu)) cbMoneyUnit.Items.Add(menu);
                    }
                }
                else if (keyValuePair.Key == "Loại đặt hàng")
                {
                    foreach (string menu in keyValuePair.Value)
                    {
                        if (!cbItemOrderType.Items.Contains(menu)) cbItemOrderType.Items.Add(menu);
                    }
                }
            }
        }
        private void CheckBoxLoginAuthorize()
        {
            try {
                if (LoginUser.UserType.ToUpper() != "ADMIN")
                {
                    tabControl1.TabPages.Remove(OverallData);
                    tabControl1.TabPages.Remove(ItemList);
                    tabControl1.TabPages.Remove(History);
                    tabControl1.TabPages.Remove(QuickInput);
                }
            }
            catch { }

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedIndex < listBox1.Items.Count)
            {
                int index = listBox1.SelectedIndex;
                txtItemInfoCode.Text = listItemNewItem[index].Code;
                txtItemInfoName.Text = listItemNewItem[index].Name;
                txtItemInfoOrderType.Text = listItemNewItem[index].OrderType;
                txtItemInfoPackingUnit.Text = listItemNewItem[index].ExportUnit;
                txtItemInfoCataloge.Text = listItemNewItem[index].ENGName;
                txtItemInfoPackingUnit.Text = listItemNewItem[index].PackingUnit;
                txtItemInfoExportUnit.Text = listItemNewItem[index].ExportUnit;
                txtItemInfoQtyTrans.Text = listItemNewItem[index].QtyTrans.ToString();
                txtItemInfoPurchaseCode.Text = listItemNewItem[index].PurchaseCode;
                txtItemInfoPartNumber.Text = listItemNewItem[index].PartNumber;
                txtItemInfoMOQ.Text = listItemNewItem[index].MOQ.ToString();
                txtItemInfoTechInfo.Text = listItemNewItem[index].TechInfo;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listItemNewItem = busItemList.getItemCodeName(cbInputItemType.Text);
            foreach (DTO_Item item in listItemNewItem)
            {
                listBox1.Items.Add(item.Name);
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            cbInputItemType.SelectedIndex = -1;
            cbInputItemType.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DTO_User user = LoginUser;
            if (user == null)
            {
                MessageBox.Show("Đăng nhập không hợp lệ", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            int quantity, price;

            if (txtInputPO.Text.Length == 15 && (txtInputPO.Text.ToCharArray()[0] == 'C' || txtInputPO.Text.ToCharArray()[0] == 'M' || txtInputPO.Text.Substring(0,3) == "EBS") && cbInputItemType.Text != "" && listBox1.SelectedIndex >= 0 && int.TryParse(txtInputQuantity.Text, out quantity) && int.TryParse(txtInputPrice.Text, out price) && quantity >= 1 && quantity <= 2000 && price >= 0 && cbMoneyUnit.Text != "")
            {
                if (quantity / listItemNewItem[listBox1.SelectedIndex].QtyTrans <= 0 || quantity % listItemNewItem[listBox1.SelectedIndex].QtyTrans != 0)
                {
                    MessageBox.Show("Số lượng nhập không hợp lệ", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                int barcodeQty = quantity / listItemNewItem[listBox1.SelectedIndex].QtyTrans;
                if (user.UserType != "ADMIN" && user.UserType != "OP")
                {
                    MessageBox.Show("Không có quyền thực hiện", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                int index = listBox1.SelectedIndex;
                string newLot = busWarehouseData.addLot(listItemNewItem[index].Code, barcodeQty, user.ID, txtInputPO.Text, price, cbMoneyUnit.Text);
                if (newLot != "")
                {
                    MessageBox.Show("Thêm LOT thành công", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    dataGridView2.DataSource = busWarehouseData.getWarehouseDataByLOT(newLot.ToString());
                    dataGridView2.ClearSelection();
                    dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Descending);
                    if (barcodeQty > dataGridView2.Rows.Count) barcodeQty = dataGridView2.Rows.Count;
                    for (int i = 0; i < barcodeQty; i++)
                    {
                        dataGridView2.Rows[i].Selected = true;
                    }
                    return;

                }
                MessageBox.Show("Thêm không thành công", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                txtInputQuantity.Clear();
                listBox1.SelectedIndex = -1;
                return;
            }
            else
            {
                MessageBox.Show("Thông tin LOT mới không hợp lệ!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count < 0 || dataGridView2.SelectedRows.Count > 60)
            {
                MessageBox.Show("Số lượng chọn không cho phép", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            Dictionary<int[], Bitmap> dictLotIDNumber = new Dictionary<int[], Bitmap>();
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                int[] arrLotIDNumber = new int[2];
                arrLotIDNumber[0] = int.Parse(row.Cells[0].Value.ToString());
                arrLotIDNumber[1] = int.Parse(row.Cells[1].Value.ToString());
                string itemCode = row.Cells[2].Value.ToString();
                Bitmap bmp = getQRCode(3, busWarehouseData.getBarcodeString(arrLotIDNumber[0], arrLotIDNumber[1]), itemCode);
                //bmp = new Bitmap(bmp, new Size((int)(bmp.Width / 1.10), (int)(bmp.Height / 1.10)));
                dictLotIDNumber.Add(arrLotIDNumber, bmp);
            }
            int count = 0;
            Bitmap A4 = new Bitmap((int)(210 * 3), (int)(297 * 3.55));
            Graphics g = Graphics.FromImage(A4);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Point p = new Point(barcodeImageMargin, barcodeImageMargin);

            foreach (KeyValuePair<int[], Bitmap> keyValuePair in dictLotIDNumber)
            {
                if (p.X + keyValuePair.Value.Size.Width + barcodeImageMargin > A4.Size.Width)
                {
                    p.X = barcodeImageMargin;
                    p.Y += keyValuePair.Value.Size.Height + barcodeImageMargin;
                }
                g.DrawImage(keyValuePair.Value, p);
                g.Flush();
                p.X += keyValuePair.Value.Size.Width + barcodeImageMargin;
                count++;
            }



            Clipboard.SetDataObject(A4, false);
            MessageBox.Show("Copy thành công " + count + " item", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        private Bitmap getQRCode(int size, string content, string itemCode)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(size);
            RectangleF rectf = new RectangleF(0, 1, qrCodeImage.Width, qrCodeImage.Height);

            // Create graphic object that will draw onto the bitmap
            Graphics g = Graphics.FromImage(qrCodeImage);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Far


            };

            // Draw the text onto the image
            g.DrawString(content, new System.Drawing.Font("Arial", 7), Brushes.Black, rectf, format);
            rectf.Y = -1;
            format.LineAlignment = StringAlignment.Near;
            g.DrawString(itemCode, new System.Drawing.Font("Arial", 7), Brushes.Black, rectf, format);
            // Flush all graphics changes to the bitmap
            g.Flush();
            return qrCodeImage;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            txtSelectedBarcodeObj.Text = dataGridView2.SelectedRows.Count.ToString();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void txtBarcodeInput_KeyDown(object sender, KeyEventArgs e)
        {

            txtBarcodeInput.Enabled = false;
            string barcode = InputExportBarcode(e);
            txtBarcodeInput.Enabled = true;
            lstExportBarcode.Sorted = true;
            lstExportBarcode.SelectedIndex = lstExportBarcode.Items.IndexOf(barcode);
            txtBarcodeInput.Select();
        }
        private string InputExportBarcode(KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return "";
            if (txtBarcodeInput.Text == "") return "";
            string inputString = txtBarcodeInput.Text;
            inputString = inputString.Trim();
            if (lstExportBarcode.Items.Contains(inputString))
            {
                txtBarcodeInput.Text = "";
                lstExportBarcode.SelectedIndex = lstExportBarcode.Items.IndexOf(inputString);
                MessageBox.Show("Barcode đã tồn tại", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return "";
            }
            inputString = inputString.Trim();
            inputString = RemoveSpecialCharacters(inputString);
            int inputLot;
            int inputNumber;
            string validCode;
            try
            {
                string[] splitArr = inputString.Split('-');
                inputLot = int.Parse(splitArr[0]);
                inputNumber = int.Parse(splitArr[1]);
                validCode = splitArr[2];
            }
            catch
            {
                MessageBox.Show("Barcode không hợp lệ", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                txtBarcodeInput.Text = "";
                return "";
            }
            finally
            {

            }
            txtBarcodeInput.Text = "";
            DTO_Warehouse_Export_Data itemInfo = busWarehouseData.getExportInfomation(inputLot, inputNumber, validCode);
            if (itemInfo == null)
            {
                MessageBox.Show("Item không tồn tại trên hệ thống", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return "";

            }
            lstExportItem.Add(itemInfo);
            lstExportBarcode.Items.Add(inputString);
            updateExportItemTable(lstExportItem);
            return inputString;
        }
        private void updateExportItemTable(List<DTO_Warehouse_Export_Data> list)
        {
            DataTable table = new DataTable("DisplayTable");
            DataColumn column;
            DataRow row;
            Dictionary<string, int> dicQuantity = new Dictionary<string, int>();
            foreach (DTO_Warehouse_Export_Data item in list)
            {
                if (dicQuantity.ContainsKey(item.ItemCode + " - " + item.Name))
                {
                    dicQuantity[item.ItemCode + " - " + item.Name] += 1;
                }
                else
                {
                    dicQuantity.Add(item.ItemCode + " - " + item.Name, 1);
                }
            }
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Hạng mục";
            column.AutoIncrement = false;
            column.ReadOnly = true;
            column.Unique = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Số lượng";
            column.AutoIncrement = false;
            column.ReadOnly = true;
            column.Unique = false;
            table.Columns.Add(column);

            foreach (KeyValuePair<string, int> keyValue in dicQuantity)
            {
                row = table.NewRow();
                row["Hạng mục"] = keyValue.Key;
                row["Số lượng"] = keyValue.Value;
                table.Rows.Add(row);
            }
            dgvExportTotal.DataSource = table;
        }

        private void lstExportBarcode_DoubleClick(object sender, EventArgs e)
        {
            int indexOfRemove = lstExportBarcode.SelectedIndex;
            string removeItem = lstExportBarcode.Items[indexOfRemove].ToString();
            string[] splitArr = removeItem.Split('-');
            lstExportBarcode.Items.RemoveAt(indexOfRemove);
            foreach (DTO_Warehouse_Export_Data item in lstExportItem)
            {
                if (item.LOTID == int.Parse(splitArr[0]) && item.LOTNumber == int.Parse(splitArr[1]))
                {
                    lstExportItem.Remove(item);
                    break;
                }
            }
            updateExportItemTable(lstExportItem);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            lstExportBarcode.Items.Clear();
            lstExportItem.Clear();
            updateExportItemTable(lstExportItem);
            txtBarcodeInput.Select();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DTO_User user = LoginUser;
            if (user == null)
            {
                MessageBox.Show("Đăng nhập không hợp lệ", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            string reciever;
            using (RecieverInput frm = new RecieverInput())
            {
                var result = frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    reciever = frm.reciever;
                }
                else return;
            }
            reciever = RemoveSpecialCharacters(reciever);
            DTO_Departurement customer = dept(cbCustomer.Text);
            if (lstExportBarcode.Items.Count > 0 && cbCustomer.Text != "" && reciever.Length == 9)
            {
                if (user.UserType != "ADMIN" && user.UserType != "OP")
                {
                    MessageBox.Show("Không có quyền thực hiện", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                DataTable dt = null;
                int complete = 0, count = 0;
                foreach (DTO_Warehouse_Export_Data item in lstExportItem)
                {
                    int lot = item.LOTID;
                    int number = item.LOTNumber;
                    string validCode = item.ValidCode.Trim();
                    string customerCode = customer.Code;
                    dt = busWarehouseData.exportLotNumber(lot, number, validCode, user.ID, customerCode, reciever, dt);
                    lstExportBarcode.Items.RemoveAt(lstExportBarcode.Items.IndexOf(string.Format("{0:D8}", lot) + "-" + string.Format("{0:D4}", number) + "-" + validCode));
                    count++;
                }
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["ITEM_STATUS"].ToString() == "EXPORT")
                        {
                            complete++;
                        }
                    }
                }
                lstExportItem.Clear();
                updateExportItemTable(lstExportItem);
                dgvExportResult.DataSource = dt;
                MessageBox.Show("Xuất hàng thành công " + complete + "/" + count, "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Không có item để xuất hoặc không đủ thông tin", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
        }
        private DTO_Departurement dept(string name)
        {
            foreach (DTO_Departurement departurement in listDept)
            {
                if (departurement.Eng == name) return departurement;
                else if (departurement.Vie == name) return departurement;

            }
            return null;
        }

        private void btnWarehouseHistory_Click(object sender, EventArgs e)
        {
            dgvWarehouseHistory.DataSource = busWarehouseData.getWarehouseDataFull();
        }

        private void btnImportQuery_Click(object sender, EventArgs e)
        {

            dgvWarehouseHistory.DataSource = busWarehouseData.getWarehouseDataImport(Convert.ToDateTime(dtpFromDate.Text), Convert.ToDateTime(dtpToDate.Text));
        }
        private void btnExportQuery_Click(object sender, EventArgs e)
        {
            dgvWarehouseHistory.DataSource = busWarehouseData.getWarehouseDataExport(Convert.ToDateTime(dtpFromDate.Text), Convert.ToDateTime(dtpToDate.Text));
        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            txtPurchaseCode.Text = RemoveSpecialCharacters(txtPurchaseCode.Text);
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if ((Convert.ToDateTime(dtpToDate.Text) - Convert.ToDateTime(dtpFromDate.Text)).TotalDays > 7 || (Convert.ToDateTime(dtpToDate.Text) - Convert.ToDateTime(dtpFromDate.Text)).TotalDays < 0)
            {
                MessageBox.Show("Nhập quá 7 ngày hoặc sai Logic");
                dtpFromDate.Text = dtpToDate.Text;
            }

        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            dtpFromDate.Text = dtpToDate.Text;
        }
        private void LoginFormLoading()
        {
            using (AccountLog AccFrm = new AccountLog())
            {
                var result = AccFrm.ShowDialog();
                if (AccFrm.DialogResult == DialogResult.OK)
                {
                    LoginUser = AccFrm.user;
                    lbUserID.Text = "Tên đăng nhập: " + LoginUser.ID;
                    lbUserPermission.Text = "Quyền hạn: " + LoginUser.UserType;
                }
                else
                {
                    this.Close();
                }
            }

        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            txtItemCode.Text = txtItemCode.Text.ToUpper();
            txtItemCode.SelectionStart = txtItemCode.Text.Length;
            txtItemCode.Text = RemoveSpecialCharacters(txtItemCode.Text);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            DataTable dt = busWarehouseData.getWarehouseDataByLOT(txtLotFindBarcodePrinting.Text);
            if (dt.Rows.Count < 1) return;
            dataGridView2.DataSource = dt;
            dataGridView2.SelectAll();
            txtLotFindBarcodePrinting.Text = "";
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void txtLotFindBarcodePrinting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button5_Click_1(sender, e);
            }
        }

        private void txtIDNewLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void txtPwdNewLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbDeptCode.Text = listDept[cbCustomer.SelectedIndex].Code;
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderText = translateTableColumn(column);
            }
        }
        private string translateTableColumn(DataGridViewColumn column)
        {
            if (column.HeaderText == "ITEM_CODE") return "MaVatTu";
            if (column.HeaderText == "NAME") return "Ten";
            if (column.HeaderText == "CATALOG_NAME") return "TenCataloge";
            if (column.HeaderText == "ITEM_TYPE") return "LoaiVatTu";
            return column.HeaderText;
        }

        private void lstExportBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExportBarcode.SelectedIndex >= 0)
            {
                string[] arr = lstExportBarcode.SelectedItem.ToString().Split('-');
                int lot = int.Parse(arr[0]);
                int number = int.Parse(arr[1]);
                DTO_Warehouse_Export_Data item = null;
                foreach (DTO_Warehouse_Export_Data data in lstExportItem)
                {
                    if (data.LOTID == lot && data.LOTNumber == number)
                    {
                        item = data;
                        break;
                    }
                }
                if (item == null) return;
                txtOutputCode.Text = item.ItemCode;
                txtOutputName.Text = item.Name;
                txtOutputCataloge.Text = item.EngName;
                txtOutputExportUnit.Text = item.ExportUnit;
                txtOutputQtyTrans.Text = item.QtyTrans;
                txtOutputInputTime.Text = item.InputDate;
                txtOutputMOQ.Text = item.MOQ.ToString();
                txtOutputTechInfo.Text = item.TechInfo;
            }
        }

        private void btnCurrentWarehouse_Click(object sender, EventArgs e)
        {
            dgvSumaryData.DataSource = null;
            dgvSumaryData.DataSource = busWarehouseData.getCurrentWarehouseData();
            dgvSumaryData.ClearSelection();
            dgvSumaryData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void btnPONumber_Click(object sender, EventArgs e)
        {
            dgvSumaryData.DataSource = null;
            dgvSumaryData.DataSource = busWarehouseData.getPOWarehouseData();
            dgvSumaryData.ClearSelection();
            dgvSumaryData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void txtExportPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPOQuery_TextChanged(object sender, EventArgs e)
        {
            txtPOQuery.Text = RemoveSpecialCharacters(txtPOQuery.Text);
        }

        private void txtPOQuery_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtPOQuery.Enabled = false;
                dgvSumaryData.DataSource = null;
                dgvSumaryData.DataSource = busWarehouseData.getPOWarehouseDataPO(txtPOQuery.Text);
                dgvSumaryData.ClearSelection();
                dgvSumaryData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                txtPOQuery.Text = "";
                txtPOQuery.Enabled = true;
                txtPOQuery.Select();
            }


        }

        private void txtCatalogQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCatalogQuery.Enabled = false;
                dgvSumaryData.DataSource = null;
                dgvSumaryData.DataSource = busWarehouseData.getPOWarehouseDataCatalog(txtCatalogQuery.Text);
                dgvSumaryData.ClearSelection();
                dgvSumaryData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                txtCatalogQuery.Text = "";
                txtCatalogQuery.Enabled = true;
                txtCatalogQuery.Select();
            }
        }

        private void txtItemCodeQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItemCodeQuery.Enabled = false;
                dgvSumaryData.DataSource = null;
                dgvSumaryData.DataSource = busWarehouseData.getPOWarehouseDataItemCode(txtItemCodeQuery.Text);
                dgvSumaryData.ClearSelection();
                dgvSumaryData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                txtItemCodeQuery.Text = "";
                txtItemCodeQuery.Enabled = true;
                txtItemCodeQuery.Select();
            }
        }

        private void btnTimeQuery_Click(object sender, EventArgs e)
        {
            txtItemCodeQuery.Enabled = false;
            dgvSumaryData.DataSource = null;
            dgvSumaryData.DataSource = busWarehouseData.getPOWarehouseDataDateTime(txtPOQuery.Text,txtCatalogQuery.Text,txtItemCodeQuery.Text,dtpFromTimeQuery.Value, dtpToTimeQuery.Value);
            dgvSumaryData.ClearSelection();
            dgvSumaryData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            txtItemCodeQuery.Text = "";
            txtItemCodeQuery.Enabled = true;
            txtItemCodeQuery.Select();
        }

        private void dtpToFromQuery_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtpFromTimeQuery.Text).Date > DateTime.Now.Date)
            {
                dtpFromTimeQuery.Text = DateTime.Now.ToString();
                dtpToTimeQuery.Text = dtpFromTimeQuery.Text;
            }
            else
            {
                dtpToTimeQuery.Text = dtpFromTimeQuery.Text;

            }
        }

        private void dtpToTimeQuery_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtpToTimeQuery.Text).Date > DateTime.Now.Date && (Convert.ToDateTime(dtpToTimeQuery.Text) - Convert.ToDateTime(dtpFromTimeQuery.Text)).TotalDays > 7 && (Convert.ToDateTime(dtpToTimeQuery.Text) - Convert.ToDateTime(dtpFromTimeQuery.Text)).TotalDays < 0)
            {
                dtpFromTimeQuery.Text = DateTime.Now.ToString();
                dtpToTimeQuery.Text = dtpFromTimeQuery.Text;
            }

        }

        private void button11_Click(object sender, EventArgs e)
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

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void dgData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lbQuickRowCount.Text = "Tổng hàng = " + dgData.Rows.Count;
        }

        private void dgData_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            lbQuickRowCount.Text = "Tổng hàng = " + dgData.Rows.Count;
        }

        private void btnQuickAddDept_Click(object sender, EventArgs e)
        {
            if (dgData.Columns.Count != 3)
            {
                MessageBox.Show("Số cột không hợp lệ!");
                dgData.DataSource = busUser.getDeptTable();
                return;

            }
            int complete = 0, count = 0;
            DialogResult dialogResult = MessageBox.Show("Xac nhan thuc hien", "Lua chon", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    try
                    {
                        DTO_Departurement dept = new DTO_Departurement();
                        dept.Code = row.Cells[0].Value.ToString();
                        dept.Eng = row.Cells[1].Value.ToString();
                        dept.Vie = row.Cells[2].Value.ToString();
                        if (busUser.addCustomer(dept))
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
                dgData.DataSource = busUser.getDeptTable();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
            
        }

        private void btnQuickDeleteDept_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Xac nhan thuc hien", "Lua chon", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (dgData.SelectedRows.Count > 0)
                {
                    int count = 0, complete = 0;
                    foreach (DataGridViewRow row in dgData.SelectedRows)
                    {
                        if (busUser.deleteDept(row.Cells[0].Value.ToString()))
                        {
                            complete++;
                        }
                        count++;
                    }
                    MessageBox.Show("Hoàn thành " + complete + "/" + count);
                    dgData.DataSource = busUser.getDeptTable();
                }
            }
            else if (dialogResult == DialogResult.No)
            {

            }
            
        }

        private void btnQuickAddCategory_Click(object sender, EventArgs e)
        {
            if (dgData.Columns.Count != 2)
            {
                MessageBox.Show("Số cột không hợp lệ!");
                dgData.DataSource = busItemList.getCategoriesMenu();
                return;
            }
            int complete = 0, count = 0;
            DialogResult dialogResult = MessageBox.Show("Xac nhan thuc hien", "Lua chon", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    try
                    {

                        string categories = row.Cells[0].Value.ToString();
                        string menu = row.Cells[1].Value.ToString();
                        if (busItemList.AddCategoriesMenu(categories, menu))
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

                dgData.DataSource = busItemList.getCategoriesMenu();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
            
        }

        private void btnQuickDeleteCategory_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Xac nhan thuc hien", "Lua chon", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (dgData.SelectedRows.Count > 0)
                {
                    int count = 0, complete = 0;
                    foreach (DataGridViewRow row in dgData.SelectedRows)
                    {
                        if (busItemList.DeleteCategoriesMenu(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString()))
                        {
                            complete++;
                        }
                        count++;
                    }
                    MessageBox.Show("Hoàn thành " + complete + "/" + count);
                    dgData.DataSource = busItemList.getCategoriesMenu();
                }
            }
            else if (dialogResult == DialogResult.No)
            {

            }
            
        }

        private void btnQuickAddItemList_Click(object sender, EventArgs e)
        {

            if (dgData.Columns.Count != 13)
            {
                MessageBox.Show("Số cột không hợp lệ!");
                dgData.DataSource = busItemList.GetItemList();
                return;
            }
            int complete = 0, count = 0;
            DialogResult dialogResult = MessageBox.Show("Xac nhan thuc hien", "Lua chon", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    DTO_Item item = new DTO_Item();
                    try
                    {
                        item.Code = row.Cells[0].Value.ToString();
                        item.Name = row.Cells[1].Value.ToString();
                        item.ENGName = row.Cells[2].Value.ToString();
                        item.Type = row.Cells[3].Value.ToString();
                        item.OrderType = row.Cells[4].Value.ToString();
                        item.Maker = row.Cells[5].Value.ToString();
                        item.PackingUnit = row.Cells[6].Value.ToString();
                        item.ExportUnit = row.Cells[7].Value.ToString();
                        item.QtyTrans = Convert.ToInt16(row.Cells[8].Value.ToString());
                        item.PurchaseCode = row.Cells[9].Value.ToString();
                        item.PartNumber = row.Cells[10].Value.ToString();
                        item.MOQ = Convert.ToInt16(row.Cells[11].Value.ToString());
                        item.TechInfo = row.Cells[12].Value.ToString();

                        if (busItemList.AddItemList(item))
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

                dgData.DataSource = busItemList.GetItemList();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
            
        }

        private void btnQuickDeleteItemList_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Xac nhan thuc hien", "Lua chon", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (dgData.SelectedRows.Count > 0)
                {
                    int count = 0, complete = 0;
                    foreach (DataGridViewRow row in dgData.SelectedRows)
                    {
                        if (busItemList.DeleteItemList(row.Cells[0].Value.ToString()))
                        {
                            complete++;
                        }
                        count++;
                    }
                    MessageBox.Show("Hoàn thành " + complete + "/" + count);
                    dgData.DataSource = busItemList.GetItemList();
                }
            }
            else if (dialogResult == DialogResult.No)
            {

            }
            
        }

        private void btnQuickAddUser_Click(object sender, EventArgs e)
        {
            int complete = 0, count = 0;
            foreach (DataGridViewRow row in dgData.Rows)
            {
                DTO_User user = new DTO_User();
                try
                {
                    user.ID = row.Cells[0].Value.ToString();
                    user.Password = row.Cells[1].Value.ToString();
                    user.UserType = row.Cells[2].Value.ToString();
                    if (busUser.addUser(user))
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

            dgData.DataSource = busUser.getUserAll();
        }

        private void btnQuickDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgData.SelectedRows.Count > 0)
            {
                int count = 0, complete = 0;
                foreach (DataGridViewRow row in dgData.SelectedRows)
                {
                    if (busUser.deleteUser(row.Cells[0].Value.ToString()))
                    {
                        complete++;
                    }
                    count++;
                }
                MessageBox.Show("Hoàn thành " + complete + "/" + count);
                dgData.DataSource = busUser.getUserAll();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgData.DataSource = null;
        }

        private void btnOutputLoad_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            List<string> listItem = busWarehouseData.getSavedDeparture();
            foreach (string str in listItem)
            {
                foreach (DTO_Departurement dept in listDept)
                {
                    if (dept.Code == str)
                    {
                        listBox2.Items.Add(dept.Vie);
                        break;
                    }
                }
            }

        }
        private string getDeptCodeByVie(string vie)
        {
            foreach (DTO_Departurement dept in listDept)
            {
                if (dept.Vie == vie) return dept.Code;
            }
            return "";
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void btnOutputSave_Click(object sender, EventArgs e)
        {
            if (cbCustomer.Text == "")
            {
                MessageBox.Show("Không thể lưu do chưa chọn bộ phận");
                return;
            }
            int count = 0, complete = 0;
            foreach (DTO_Warehouse_Export_Data item in lstExportItem)
            {
                count++;
                if (busWarehouseData.saveTempListItem(item, listDept[cbCustomer.SelectedIndex].Code)) complete++;
            }
            MessageBox.Show("Đã hoàn thành " + complete + " / " + count);
        }

        private void txtItemInfoQtyTrans_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                lstExportItem = busWarehouseData.getTempListItem(getDeptCodeByVie(listBox2.SelectedItem.ToString()));
                lstExportBarcode.Items.Clear();
                foreach (DTO_Warehouse_Export_Data item in lstExportItem)
                {
                    lstExportBarcode.Items.Add(string.Format(@"{0:D8}-{1:D4}-{2}", item.LOTID, item.LOTNumber, item.ValidCode)); //string.Format("{0:D8}", lot) + "-" + string.Format("{0:D4}", number) + "-" + validCode
                }
                updateExportItemTable(lstExportItem);
            }
        }

        private void cbItemOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbItemOrderType.Text == "EBS")
            {
                txtPurchaseCode.Enabled = false;
                txtPurchaseCode.Text = "X00000000";
            }
            else if (cbItemOrderType.Text == "BÁO GIÁ")
            {
                txtPurchaseCode.Enabled = false;
                txtPurchaseCode.Text = "X00000000";
            }
            else
            {
                txtPurchaseCode.Enabled = true;
                txtPurchaseCode.Text = "";
            }
        }
        private string RemoveSpecialCharacters(string str)
        {
            Dictionary<string,string> filterStringDict = new Dictionary<string, string>();
            filterStringDict.Add("--","-");
            filterStringDict.Add("\"", ""); 
            filterStringDict.Add("!", "");
            filterStringDict.Add("@", "");
            filterStringDict.Add("#", "");
            filterStringDict.Add("$", "");
            filterStringDict.Add("%", "");
            filterStringDict.Add("^", "");
            filterStringDict.Add("&", "");
            filterStringDict.Add("*", "");
            filterStringDict.Add("(", "");
            filterStringDict.Add(")", "");
            filterStringDict.Add("{", "");
            filterStringDict.Add("}", "");
            filterStringDict.Add("+", "");
            filterStringDict.Add("=", "");
            filterStringDict.Add(":", "");
            filterStringDict.Add(";", "");
            filterStringDict.Add("'", "");
            filterStringDict.Add("?", "");
            filterStringDict.Add("<", "");
            filterStringDict.Add(">", "");
            filterStringDict.Add(",", "");
            filterStringDict.Add(".", "");
            filterStringDict.Add("/", "");

            foreach(KeyValuePair<string,string> replacePair in filterStringDict)
            {
                str = str.Replace(replacePair.Key,replacePair.Value);
            }
            return str;
        }

        private void txtInputQuantity_TextChanged(object sender, EventArgs e)
        {
            txtInputQuantity.Text = RemoveSpecialCharacters(txtInputQuantity.Text);
        }

        private void txtInputPO_TextChanged(object sender, EventArgs e)
        {
            txtInputPO.Text = RemoveSpecialCharacters(txtInputPO.Text);
        }

        private void txtInputPrice_TextChanged(object sender, EventArgs e)
        {
            txtInputPrice.Text = RemoveSpecialCharacters(txtInputPrice.Text);
        }

        private void txtLotFindBarcodePrinting_TextChanged(object sender, EventArgs e)
        {
            txtLotFindBarcodePrinting.Text = RemoveSpecialCharacters(txtLotFindBarcodePrinting.Text);
        }

        private void txtBarcodeInput_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            txtItemName.Text = RemoveSpecialCharacters(txtItemName.Text);
        }

        private void txtItemEngName_TextChanged(object sender, EventArgs e)
        {
            txtItemEngName.Text = RemoveSpecialCharacters(txtItemEngName.Text);
        }

        private void txtItemMaker_TextChanged(object sender, EventArgs e)
        {
            txtItemMaker.Text = RemoveSpecialCharacters(txtItemMaker.Text);
        }

        private void txtQtyTrans_TextChanged(object sender, EventArgs e)
        {
            txtQtyTrans.Text = RemoveSpecialCharacters(txtQtyTrans.Text);
        }

        private void txtItemPartNumber_TextChanged(object sender, EventArgs e)
        {
            txtItemPartNumber.Text = RemoveSpecialCharacters(txtItemPartNumber.Text);
        }

        private void txtMOQ_TextChanged(object sender, EventArgs e)
        {
            txtMOQ.Text = RemoveSpecialCharacters(txtMOQ.Text);
        }

        private void txtNoteInfo_TextChanged(object sender, EventArgs e)
        {
            txtNoteInfo.Text = RemoveSpecialCharacters(txtNoteInfo.Text);
        }

        private void txtCatalogQuery_TextChanged(object sender, EventArgs e)
        {
            txtCatalogQuery.Text = RemoveSpecialCharacters(txtCatalogQuery.Text);
        }

        private void txtItemCodeQuery_TextChanged(object sender, EventArgs e)
        {
            txtItemCodeQuery.Text = RemoveSpecialCharacters(txtItemCodeQuery.Text);
        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void ItemList_Click(object sender, EventArgs e)
        {

        }
    }
}
