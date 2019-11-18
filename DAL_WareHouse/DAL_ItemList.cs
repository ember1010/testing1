using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO_WareHouse;

namespace DAL_WareHouse
{
    public class DAL_ItemList : DBConnect
    {
        public DataTable GetItemList()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 2000 * FROM .[dbo].[MATERIAL_ITEM]", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public List<DTO_Item> getItemCodeName(string type)
        {
            String queryString = string.Format(@"SELECT TOP 2000 * FROM.[dbo].[MATERIAL_ITEM] WHERE ITEM_TYPE = N'{0}' ORDER BY NAME ", type);
            SqlDataAdapter da = new SqlDataAdapter(queryString, conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            List<DTO_Item> list = new List<DTO_Item>();
            foreach (DataRow row in dtItemList.Rows)
            {
                DTO_Item item = new DTO_Item();
                item.Code = row[0].ToString();
                item.Name = row[1].ToString();
                item.ENGName = row[2].ToString();
                item.Type = row[3].ToString();
                item.OrderType = row[4].ToString();
                item.Maker = row[5].ToString();
                item.PackingUnit = row[6].ToString();
                item.ExportUnit = row[7].ToString();
                item.QtyTrans = Convert.ToInt16(row[8].ToString());
                item.AddedTime = row[9].ToString();
                item.PurchaseCode = row[10].ToString();
                item.OrderType = row[11].ToString();
                item.MOQ = Convert.ToInt16(row[12].ToString());
                item.TechInfo = row[13].ToString();
                list.Add(item);
            }
            return list;
        }
        public bool AddItemList(DTO_Item item)
        {
            try
            {
                // Ket noi

                conn.Open();
                // Query string - vì mình để TV_ID là identity (giá trị tự tăng dần) nên ko cần fải insert ID
                string SQL = string.Format("INSERT INTO MATERIAL_ITEM([ITEM_CODE],[NAME],[ENG_NAME],[ITEM_TYPE],[ORDER_TYPE],[MAKER],[PACKING_UNIT],[EXPORT_UNIT],[QTY_TRANS],[ADDED_TIME],[PURCHASE_CODE],[PART_NO],[MOQ],[TECH_INFO]) VALUES ('{0}',@Name,@EngName,N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',{8},CURRENT_TIMESTAMP,'{9}','{10}',{11},@TechInfo)", item.Code, item.Name, item.ENGName,item.Type, item.OrderType, item.Maker, item.PackingUnit,item.ExportUnit,item.QtyTrans,item.PurchaseCode,item.PartNumber,item.MOQ,item.TechInfo);
                //byte[] bytes = Encoding.Unicode.GetBytes(SQL);
                //SQL = Encoding.Unicode.GetString(bytes);
                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                
                
                SqlCommand cmd = new SqlCommand(SQL, conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Name";
                param.Value = item.Name;
                cmd.Parameters.Add(param);
                param = new SqlParameter();
                param.ParameterName = "@EngName";
                param.Value = item.ENGName;
                cmd.Parameters.Add(param);
                param = new SqlParameter();
                param.ParameterName = "@TechInfo";
                param.Value = item.TechInfo;
                cmd.Parameters.Add(param);

                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;

            }
            catch (Exception e)
            {

            }
            finally
            {
                // Dong ket noi
                conn.Close();
            }

            return false;
        }
        public bool EditItem(DTO_Item item, string itemCode)
        {
            try
            {
                // Ket noi
                conn.Open();

                // Query string
                string SQL = string.Format(@"UPDATE MATERIAL_ITEM SET ITEM_CODE = '{0}', NAME = @Name,ENG_NAME = @EngName,ITEM_TYPE = N'{3}',ORDER_TYPE = N'{4}', MAKER = N'{5}',[PACKING_UNIT] = N'{6}', EXPORT_UNIT = N'{7}',[QTY_TRANS] = {8}, ADDED_TIME = CURRENT_TIMESTAMP , PURCHASE_CODE = '{9}', PART_NO = '{10}',MOQ = {11},TECH_INFO = @TechInfo WHERE ITEM_CODE = '{13}'", item.Code, item.Name, item.ENGName, item.Type, item.OrderType, item.Maker, item.PackingUnit, item.ExportUnit, item.QtyTrans, item.PurchaseCode,item.PartNumber, item.MOQ, item.TechInfo,itemCode);

                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                SqlCommand cmd = new SqlCommand(SQL, conn);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Name";
                param.Value = item.Name;
                cmd.Parameters.Add(param);
                param = new SqlParameter();
                param.ParameterName = "@EngName";
                param.Value = item.ENGName;
                cmd.Parameters.Add(param);
                param = new SqlParameter();
                param.ParameterName = "@TechInfo";
                param.Value = item.TechInfo;
                cmd.Parameters.Add(param);
                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;

            }
            catch (Exception e)
            {

            }
            finally
            {
                // Dong ket noi
                conn.Close();
            }

            return false;
        }

        public bool DeleteItem(string itemCode)
        {
            try
            {
                // Ket noi
                conn.Open();

                // Query string - vì xóa chỉ cần ID nên chúng ta ko cần 1 DTO, ID là đủ
                string SQL = string.Format("DELETE FROM MATERIAL_ITEM WHERE ITEM_CODE = '{0}'", itemCode);

                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                SqlCommand cmd = new SqlCommand(SQL, conn);

                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;

            }
            catch (Exception e)
            {

            }
            finally
            {
                // Dong ket noi
                conn.Close();
            }

            return false;
        }
    }
    public class DAL_Categories_Menu : DBConnect
    {
        public void GetCategoriesMenu(Dictionary<string,List<string>> dictCategoriesMenu)
        {
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT [CATEGORIES],[MENU] FROM .[dbo].[CATEGORIES_MENU]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                if (dictCategoriesMenu.ContainsKey(row[0].ToString()))
                {
                    dictCategoriesMenu[row[0].ToString()].Add(row[1].ToString());
                }
                else
                {
                    List<string> listMenu = new List<string>();
                    listMenu.Add(row[1].ToString());
                    dictCategoriesMenu.Add(row[0].ToString(),listMenu);
                }
            }
        }
        public DataTable GetCategoriesMenu()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT [CATEGORIES],[MENU] FROM .[dbo].[CATEGORIES_MENU]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);            
            return dt;
        }
        public bool AddCategoriesMenu(string categories, string menu)
        {
            try
            {
                // Ket noi
                conn.Open();

                // Query string
                string SQL = string.Format(@"INSERT INTO [dbo].[CATEGORIES_MENU]  ([CATEGORIES],[MENU]) VALUES(N'{0}',N'{1}')", categories,menu);

                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                SqlCommand cmd = new SqlCommand(SQL, conn);

                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;

            }
            catch (Exception e)
            {

            }
            finally
            {
                // Dong ket noi
                conn.Close();
            }

            return false;
        }
        public bool deleteCategoriesMenu(string categories,string menu)
        {
            try
            {
                conn.Open();

                // Query string - vì xóa chỉ cần ID nên chúng ta ko cần 1 DTO, ID là đủ
                string SQL = string.Format(@"DELETE FROM [dbo].[CATEGORIES_MENU] WHERE [CATEGORIES] = N'{0}' AND [MENU] = N'{1}'", categories,menu);

                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                SqlCommand cmd = new SqlCommand(SQL, conn);
                int result = cmd.ExecuteNonQuery();
                // Query và kiểm tra
                conn.Close();
                if (result > 0) return true;

            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return false;

        }
    }
}

