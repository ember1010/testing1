using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO_WareHouse;

namespace DAL_WareHouse
{
    public class DAL_Warehouse_Data : DBConnect
    {
        public DataTable getWarehouseData()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 2000 W.[LOT_ID],W.[LOT_NUMBER],W.[ITEM_CODE],M.[NAME],W.[INPUT_DATE],W.[EXPORT_UNIT],W.[PO_NUMBER],M.[PURCHASE_CODE],M.[PART_NO],O.OPERATOR_ID,W.PRICE,W.MONEY_UNIT,W.QTY_TRANS
                                                    FROM [dbo].[WAREHOUSE] W
                                                    INNER JOIN [dbo].[MATERIAL_ITEM] M
                                                    ON W.[ITEM_CODE] = M.[ITEM_CODE]
                                                    INNER JOIN [dbo].[OPRECORD] O
                                                    ON W.[LOT_ID] = O.[LOT_ID]
                                                    WHERE W.ITEM_STATUS = 'ACTIVE' 
                                                    ORDER BY W.[INPUT_DATE] DESC", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public DataTable getWarehouseDataByLOT(string lot)
        {
            DataTable dtItemList = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(string.Format(@"SELECT TOP 2000 W.[LOT_ID],W.[LOT_NUMBER],W.[ITEM_CODE],M.[NAME],W.[INPUT_DATE],W.[EXPORT_UNIT],W.[PO_NUMBER],M.[PURCHASE_CODE],M.[PART_NO],O.OPERATOR_ID,W.PRICE,W.MONEY_UNIT,W.QTY_TRANS
                                                    FROM [dbo].[WAREHOUSE] W
                                                    INNER JOIN [dbo].[MATERIAL_ITEM] M
                                                    ON W.[ITEM_CODE] = M.[ITEM_CODE]
                                                    INNER JOIN [dbo].[OPRECORD] O
                                                    ON W.[LOT_ID] = O.[LOT_ID]
                                                    WHERE W.ITEM_STATUS = 'ACTIVE' AND W.LOT_ID = '{0}'
                                                    ORDER BY W.[INPUT_DATE] DESC", lot), conn);

                da.Fill(dtItemList);
            }
            catch { }
            return dtItemList;
        }
        public DataTable getWarehouseDataFull()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 2000 W.[LOT_ID],W.[LOT_NUMBER],W.[ITEM_CODE],M.[NAME],W.[INPUT_DATE],W.[OUTPUT_DATE],W.[OUTPUT_CUSTOMER],M.[EXPORT_UNIT],W.[PO_NUMBER],M.[PURCHASE_CODE],M.[PART_NO],W.[ITEM_STATUS]
                                                    FROM [dbo].[WAREHOUSE] W
                                                    INNER JOIN [dbo].[MATERIAL_ITEM] M
                                                    ON W.[ITEM_CODE] = M.[ITEM_CODE]
                                                    ORDER BY INPUT_DATE DESC", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public DataTable getWarehouseDataExport(DateTime dtFrom, DateTime dtTo)
        {
            string datetimeCondition = string.Format(@"( W.[OUTPUT_DATE] BETWEEN '{0} 00:00:00' AND '{1} 23:59:59')", dtFrom.ToShortDateString(), dtTo.ToShortDateString());
            string sqlString = string.Format(@"SELECT TOP 1000 W.[LOT_ID]
                                                                      ,W.[LOT_NUMBER]
                                                                      ,W.[ITEM_CODE]
                                                                      ,W.[INPUT_DATE]
                                                                      ,M.[NAME]
                                                                      ,M.[ITEM_TYPE]
                                                                      ,W.[OUTPUT_DATE]
                                                                      ,W.[OUTPUT_CUSTOMER]
                                                                      ,D.[NAME_VIE]
                                                                      ,W.[ITEM_STATUS]
                                                                      ,W.[EXPORT_UNIT]
                                                                      ,W.[PO_NUMBER]
                                                                      ,M.[PURCHASE_CODE]
                                                                      ,M.[PART_NO]
                                                                      ,W.[QTY_TRANS]
                                                                      ,W.[PRICE]
                                                                      ,W.[MONEY_UNIT]
                                                                      ,W.[PAPER_NO]
                                                    FROM [dbo].[WAREHOUSE] W
                                                    INNER JOIN [dbo].[MATERIAL_ITEM] M
                                                    ON W.[ITEM_CODE] = M.[ITEM_CODE]
                                                    INNER JOIN [dbo].[DEPARTMENT] D
                                                    ON W.[OUTPUT_CUSTOMER] = D.[CODE]
                                                    WHERE W.[ITEM_STATUS] = 'EXPORT' AND  {0}
                                                     ORDER BY INPUT_DATE DESC", datetimeCondition);
            SqlDataAdapter da = new SqlDataAdapter(sqlString, conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public DataTable getWarehouseDataActive(DateTime dtFrom, DateTime dtTo)
        {
            string datetimeCondition = string.Format(@"( W.[INPUT_DATE] BETWEEN '{0} 00:00:00' AND '{1} 23:59:59')", dtFrom.ToShortDateString(), dtTo.ToShortDateString());
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 1000    W.[LOT_ID]
                                                                      ,W.[LOT_NUMBER]
                                                                      ,W.[ITEM_CODE]
                                                                      ,W.[INPUT_DATE]
                                                                      ,W.[ITEM_STATUS]
                                                                      ,W.[EXPORT_UNIT]
                                                                      ,W.[PO_NUMBER]
                                                                      ,M.[PURCHASE_CODE]
                                                                      ,M.[PART_NO]
                                                                      ,W.[QTY_TRANS]
                                                                      ,W.[PRICE]
                                                                      ,W.[MONEY_UNIT]
                                                    FROM [dbo].[WAREHOUSE] W
                                                    INNER JOIN [dbo].[MATERIAL_ITEM] M
                                                    ON W.[ITEM_CODE] = M.[ITEM_CODE]
                                                    WHERE W.[ITEM_STATUS] = 'ACTIVE' AND " + datetimeCondition +
                                                    " ORDER BY INPUT_DATE DESC", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public DataTable getWarehouseDataImport(DateTime dtFrom, DateTime dtTo)
        {
            string datetimeCondition = string.Format(@"( W.[INPUT_DATE] BETWEEN '{0} 00:00:00' AND '{1} 23:59:59')", dtFrom.ToShortDateString(), dtTo.ToShortDateString());
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 1000    W.[LOT_ID]
                                                                      ,W.[LOT_NUMBER]
                                                                      ,W.[ITEM_CODE]
                                                                      ,W.[INPUT_DATE]
                                                                      ,W.[ITEM_STATUS]
                                                                      ,W.[EXPORT_UNIT]
                                                                      ,W.[PO_NUMBER]
                                                                      ,M.[PURCHASE_CODE]
                                                                      ,M.[PART_NO]
                                                                      ,W.[QTY_TRANS]
                                                                      ,W.[PRICE]
                                                                      ,W.[MONEY_UNIT]
                                                    FROM [dbo].[WAREHOUSE] W
                                                    INNER JOIN [dbo].[MATERIAL_ITEM] M
                                                    ON W.[ITEM_CODE] = M.[ITEM_CODE]
                                                    WHERE " + datetimeCondition +
                                                    " ORDER BY INPUT_DATE DESC", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public DataTable getCurrentWarehouseData()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 500 W.ITEM_CODE,M.NAME,W.Quantity,M.ENG_NAME,M.[MAKER],[PURCHASE_CODE],[PART_NO],[MOQ],[TECH_INFO] FROM 
	                                                                                    (SELECT _W.ITEM_CODE, sum(_W.QTY_TRANS) AS Quantity
	                                                                                    FROM (SELECT * FROM WAREHOUSE WHERE [ITEM_STATUS] = 'ACTIVE') _W
	                                                                                    GROUP BY _W.ITEM_CODE) W
                                                                                        INNER JOIN [dbo].[MATERIAL_ITEM] M
                                                                                        ON W.ITEM_CODE = M.ITEM_CODE
                                                                                        ", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public DataTable getpPOWarehouseData()
        {
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 500 W.PO_NUMBER,A.INPUT_DATE,M.ENG_NAME,W.ITEM_CODE,M.NAME,M.[TECH_INFO],A.EXPORT_UNIT,A.PRICE,A.MONEY_UNIT,W.Quantity,O.OPERATOR_ID,W.LOT_ID FROM
                                                                        (SELECT[ITEM_CODE], [PO_NUMBER], LOT_ID , sum(_W.QTY_TRANS) AS Quantity
                                                                        FROM (SELECT* FROM WAREHOUSE) _W
                                                                        GROUP BY[ITEM_CODE],[PO_NUMBER],LOT_ID) W
                                                                        INNER JOIN[dbo].[MATERIAL_ITEM] M
                                                                        ON W.ITEM_CODE = M.ITEM_CODE
                                                                        INNER JOIN(SELECT* FROM WAREHOUSE WHERE LOT_NUMBER = 1) A
                                                                        ON A.ITEM_CODE = W.ITEM_CODE AND A.PO_NUMBER = W.PO_NUMBER AND A.LOT_ID = W.LOT_ID
                                                                        INNER JOIN[dbo].[OPRECORD] O
                                                                        ON O.LOT_ID = W.LOT_ID AND O.ACTION_TYPE = 'INPUT NEW ITEM'
                                                                        ORDER BY A.INPUT_DATE DESC", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public DataTable getpPOWarehouseDataPO(string PO)
        {
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 500 W.PO_NUMBER,A.INPUT_DATE,M.ENG_NAME,W.ITEM_CODE,M.NAME,M.[TECH_INFO],A.EXPORT_UNIT,A.PRICE,A.MONEY_UNIT,W.Quantity,O.OPERATOR_ID,W.LOT_ID FROM
                                                                        (SELECT[ITEM_CODE], [PO_NUMBER], LOT_ID , sum(_W.QTY_TRANS) AS Quantity
                                                                        FROM (SELECT* FROM WAREHOUSE) _W
                                                                        GROUP BY[ITEM_CODE],[PO_NUMBER],LOT_ID) W
                                                                        INNER JOIN[dbo].[MATERIAL_ITEM] M
                                                                        ON W.ITEM_CODE = M.ITEM_CODE
                                                                        INNER JOIN(SELECT* FROM WAREHOUSE WHERE LOT_NUMBER = 1) A
                                                                        ON A.ITEM_CODE = W.ITEM_CODE AND A.PO_NUMBER = W.PO_NUMBER AND A.LOT_ID = W.LOT_ID
                                                                        INNER JOIN[dbo].[OPRECORD] O
                                                                        ON O.LOT_ID = W.LOT_ID AND O.ACTION_TYPE = 'INPUT NEW ITEM'
                                                                        WHERE A.PO_NUMBER = '" + PO+ "' ORDER BY A.INPUT_DATE DESC", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public DataTable getpPOWarehouseDataEngName(string engName)
        {
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 500 W.PO_NUMBER,A.INPUT_DATE,M.ENG_NAME,W.ITEM_CODE,M.NAME,M.[TECH_INFO],A.EXPORT_UNIT,A.PRICE,A.MONEY_UNIT,W.Quantity,O.OPERATOR_ID,W.LOT_ID FROM
                                                                        (SELECT[ITEM_CODE], [PO_NUMBER], LOT_ID , sum(_W.QTY_TRANS) AS Quantity
                                                                        FROM (SELECT* FROM WAREHOUSE) _W
                                                                        GROUP BY[ITEM_CODE],[PO_NUMBER],LOT_ID) W
                                                                        INNER JOIN[dbo].[MATERIAL_ITEM] M
                                                                        ON W.ITEM_CODE = M.ITEM_CODE
                                                                        INNER JOIN(SELECT* FROM WAREHOUSE WHERE LOT_NUMBER = 1) A
                                                                        ON A.ITEM_CODE = W.ITEM_CODE AND A.PO_NUMBER = W.PO_NUMBER AND A.LOT_ID = W.LOT_ID
                                                                        INNER JOIN[dbo].[OPRECORD] O
                                                                        ON O.LOT_ID = W.LOT_ID AND O.ACTION_TYPE = 'INPUT NEW ITEM'
                                                                        WHERE M.ENG_NAME = '" + engName + "' ORDER BY A.INPUT_DATE DESC", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public DataTable getpPOWarehouseDataItemCode(string itemCode)
        {
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 500 W.PO_NUMBER,A.INPUT_DATE,M.ENG_NAME,W.ITEM_CODE,M.NAME,M.[TECH_INFO],A.EXPORT_UNIT,A.PRICE,A.MONEY_UNIT,W.Quantity,O.OPERATOR_ID,W.LOT_ID FROM
                                                                        (SELECT[ITEM_CODE], [PO_NUMBER], LOT_ID , sum(_W.QTY_TRANS) AS Quantity
                                                                        FROM (SELECT* FROM WAREHOUSE) _W
                                                                        GROUP BY[ITEM_CODE],[PO_NUMBER],LOT_ID) W
                                                                        INNER JOIN[dbo].[MATERIAL_ITEM] M
                                                                        ON W.ITEM_CODE = M.ITEM_CODE
                                                                        INNER JOIN(SELECT* FROM WAREHOUSE WHERE LOT_NUMBER = 1) A
                                                                        ON A.ITEM_CODE = W.ITEM_CODE AND A.PO_NUMBER = W.PO_NUMBER AND A.LOT_ID = W.LOT_ID
                                                                        INNER JOIN[dbo].[OPRECORD] O
                                                                        ON O.LOT_ID = W.LOT_ID AND O.ACTION_TYPE = 'INPUT NEW ITEM'
                                                                        WHERE W.ITEM_CODE = '" + itemCode + "' ORDER BY A.INPUT_DATE DESC", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public DataTable getpPOWarehouseDataDateTime(DateTime fromTime,DateTime toTime)
        {
            string condition = string.Format(@"( A.[INPUT_DATE] BETWEEN '{0} 00:00:00' AND '{1} 23:59:59')", fromTime.ToShortDateString(), toTime.ToShortDateString());
            SqlDataAdapter da = new SqlDataAdapter(@"SELECT TOP 500 W.PO_NUMBER,A.INPUT_DATE,M.ENG_NAME,W.ITEM_CODE,M.NAME,M.[TECH_INFO],A.EXPORT_UNIT,A.PRICE,A.MONEY_UNIT,W.Quantity,O.OPERATOR_ID,W.LOT_ID FROM
                                                                        (SELECT[ITEM_CODE], [PO_NUMBER], LOT_ID , sum(_W.QTY_TRANS) AS Quantity
                                                                        FROM (SELECT* FROM WAREHOUSE) _W
                                                                        GROUP BY[ITEM_CODE],[PO_NUMBER],LOT_ID) W
                                                                        INNER JOIN[dbo].[MATERIAL_ITEM] M
                                                                        ON W.ITEM_CODE = M.ITEM_CODE
                                                                        INNER JOIN(SELECT* FROM WAREHOUSE WHERE LOT_NUMBER = 1) A
                                                                        ON A.ITEM_CODE = W.ITEM_CODE AND A.PO_NUMBER = W.PO_NUMBER AND A.LOT_ID = W.LOT_ID
                                                                        INNER JOIN[dbo].[OPRECORD] O
                                                                        ON O.LOT_ID = W.LOT_ID AND O.ACTION_TYPE = 'INPUT NEW ITEM'
                                                                        WHERE " + condition + " ORDER BY A.INPUT_DATE DESC", conn);
            DataTable dtItemList = new DataTable();
            da.Fill(dtItemList);
            return dtItemList;
        }
        public string addNewLOT(string itemCode, int quantity, string operatorID, string po, int price, string moneyUnit)
        {
            string result = "";
            try
            {
                string str;
                using (SqlCommand cmd = new SqlCommand("AddLot", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@itemCode", itemCode);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@operatorID", operatorID);
                    cmd.Parameters.AddWithValue("@po", po);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@moneyUnit", moneyUnit);
                    conn.Open();
                    var s = cmd.ExecuteNonQuery();
                    str = s.ToString();
                }
                SqlDataAdapter da = new SqlDataAdapter(string.Format(@"SELECT TOP 1 [LOT_ID] FROM [dbo].[WAREHOUSE] ORDER BY [INPUT_DATE] DESC"), conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                result = dt.Rows[0][0].ToString();
                return result;
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                str = "";
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public bool saveTempListItem(DTO_Warehouse_Export_Data item,string deptCode)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("TempExport", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LOT_ID", item.LOTID);
                    cmd.Parameters.AddWithValue("@LOT_NUMBER", item.LOTNumber);
                    cmd.Parameters.AddWithValue("@CODE", item.ValidCode);
                    cmd.Parameters.AddWithValue("@CUSTOMER", deptCode);
                    conn.Open();
                    if (cmd.ExecuteNonQuery() > 0) return true;                    
                }
                return false;
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                str = "";
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        public string getBarcodeString(int LOTID, int LOTNumber)
        {
            string sqlstr = string.Format(@"SELECT TOP 1 [LOT_ID],[LOT_NUMBER],[VALID_CODE]
                                                    FROM [dbo].[WAREHOUSE]                                                
                                                    WHERE LOT_ID = {0} AND LOT_NUMBER = {1}", LOTID, LOTNumber);
            SqlDataAdapter da = new SqlDataAdapter(sqlstr, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string lot = string.Format("{0:D8}", (int)dt.Rows[0][0]);
                string number = string.Format("{0:D4}", (int)dt.Rows[0][1]);
                string returnString = lot + "-" + number + "-" + dt.Rows[0][2].ToString();
                return returnString;
            }
            else return null;
        }
        public DataTable exportLOTNumber(int lot, int number, string validCode, string user,string reciever, string customer)
        {
            DataTable dt = new DataTable();
            try
            {
                int affectRow;
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ExportItem", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LOT_ID", lot);
                    cmd.Parameters.AddWithValue("@LOT_NUMBER", number);
                    cmd.Parameters.AddWithValue("@CUSTOMER", customer);
                    cmd.Parameters.AddWithValue("@CODE", validCode);
                    cmd.Parameters.AddWithValue("@RECIEVER", reciever);
                    cmd.Parameters.AddWithValue("@OPERATOR_ID", user);
                    affectRow = cmd.ExecuteNonQuery();
                }
                conn.Close();
                string sqlString = string.Format(@"SELECT TOP 1 W.[LOT_ID], W.[LOT_NUMBER], W.[ITEM_CODE], M.[NAME], W.[INPUT_DATE],W.[OUTPUT_DATE] ,W.[OUTPUT_CUSTOMER],D.NAME_VIE,W.[OUTPUT_RECIEVER],W.[ITEM_STATUS],W.PAPER_NO
                                                   FROM[dbo].[WAREHOUSE] W INNER JOIN[dbo].[MATERIAL_ITEM] M ON W.[ITEM_CODE] = M.[ITEM_CODE]
                                                    INNER JOIN[dbo].[DEPARTMENT] D
                                                    ON D.CODE = W.OUTPUT_CUSTOMER
                                                   WHERE  W.[LOT_ID] = {0} AND W.[LOT_NUMBER] = {1}", lot, number);
                SqlDataAdapter da = new SqlDataAdapter(sqlString, conn);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return null;
        }
        public DTO_Warehouse_Export_Data getExportItemInfo(int lot, int number, string code)
        {
            DTO_Warehouse_Export_Data lotItem = new DTO_Warehouse_Export_Data();
            try
            {
                string sqlString = string.Format(@"SELECT TOP 1 W.[LOT_ID],W.[LOT_NUMBER],W.[VALID_CODE],W.[ITEM_CODE],M.[NAME],M.[ENG_NAME],W.[EXPORT_UNIT],W.[QTY_TRANS],W.[INPUT_DATE],M.[MOQ],M.[TECH_INFO]
                                                   FROM[dbo].[WAREHOUSE] W
                                                   INNER JOIN[dbo].[MATERIAL_ITEM] M
                                                   ON W.[ITEM_CODE] = M.[ITEM_CODE]
                                                   WHERE W.ITEM_STATUS = 'ACTIVE' AND W.[LOT_ID] = {0} AND W.[LOT_NUMBER] = {1} AND W.[VALID_CODE] = '{2}'", lot, number, code);
                SqlDataAdapter da = new SqlDataAdapter(sqlString, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    lotItem.LOTID = int.Parse(dt.Rows[0][0].ToString());
                    lotItem.LOTNumber = int.Parse(dt.Rows[0][1].ToString());
                    lotItem.ValidCode = dt.Rows[0][2].ToString();
                    lotItem.ItemCode = dt.Rows[0][3].ToString();
                    lotItem.Name = dt.Rows[0][4].ToString();
                    lotItem.EngName = dt.Rows[0][5].ToString();
                    lotItem.ExportUnit = dt.Rows[0][6].ToString();
                    lotItem.QtyTrans = dt.Rows[0][7].ToString();
                    lotItem.InputDate = dt.Rows[0][8].ToString();
                    lotItem.MOQ = int.Parse(dt.Rows[0][9].ToString());
                    lotItem.TechInfo = dt.Rows[0][10].ToString();
                    return lotItem;
                }
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return null;
        }
        public List<string> getSavedDeparture()
        {
            List<string> list = new List<string>();
            try
            {
                string sqlString = string.Format(@"SELECT DISTINCT [OUTPUT_CUSTOMER] FROM [dbo].[WAREHOUSE] WHERE [ITEM_STATUS] = 'ACTIVE'  AND [OUTPUT_CUSTOMER] IS NOT NULL");
                SqlDataAdapter da = new SqlDataAdapter(sqlString, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row[0].ToString());
                }
                return list;
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return list;
        }
        public List<DTO_Warehouse_Export_Data> getTempListItem(string deptCode)
        {
            List<DTO_Warehouse_Export_Data> list = new List<DTO_Warehouse_Export_Data>();
            try
            {
                string sqlString = string.Format(@"SELECT TOP 500 W.[LOT_ID],W.[LOT_NUMBER],W.[VALID_CODE],W.[ITEM_CODE],M.[NAME],M.[ENG_NAME],W.[EXPORT_UNIT],W.[QTY_TRANS],W.[INPUT_DATE],M.[MOQ],M.[TECH_INFO]
                                                   FROM[dbo].[WAREHOUSE] W
                                                   INNER JOIN[dbo].[MATERIAL_ITEM] M
                                                   ON W.[ITEM_CODE] = M.[ITEM_CODE]
                                                   WHERE W.ITEM_STATUS = 'ACTIVE' AND W.[OUTPUT_CUSTOMER] = '{0}'", deptCode);
                SqlDataAdapter da = new SqlDataAdapter(sqlString, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    DTO_Warehouse_Export_Data item = new DTO_Warehouse_Export_Data();
                    item.LOTID = int.Parse(row[0].ToString());
                    item.LOTNumber = int.Parse(row[1].ToString());
                    item.ValidCode = row[2].ToString();
                    item.ItemCode = row[3].ToString();
                    item.Name = row[4].ToString();
                    item.EngName = row[5].ToString();
                    item.ExportUnit = row[6].ToString();
                    item.QtyTrans = row[7].ToString();
                    item.InputDate = row[8].ToString();
                    item.MOQ = int.Parse(row[9].ToString());
                    item.TechInfo = row[10].ToString();
                    list.Add(item);
                }
                return list;
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return list;
        }
    }
}
