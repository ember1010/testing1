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
    public class DAL_User : DBConnect
    {
        public DTO_User getUser(string id, string password)
        {

            String queryString = string.Format(@"SELECT TOP 1 [ID],
                                                            [USER_TYPE]
                                                            FROM .[dbo].[OPERATOR_ID]
                                                            WHERE ID = '{0}' AND PWD = '{1}' ", id, password);
            SqlDataAdapter da = new SqlDataAdapter(queryString, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DTO_User user = new DTO_User();
            if (dt.Rows.Count > 0)
            {
                user.ID = dt.Rows[0][0].ToString();
                user.UserType = dt.Rows[0][1].ToString();
                return user;
            }
            else
            {
                return null;
            }
        }
        public DataTable getUserAll()
        {

            String queryString = string.Format(@"SELECT * FROM .[dbo].[OPERATOR_ID]");
            SqlDataAdapter da = new SqlDataAdapter(queryString, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public bool addUser(DTO_User user)
        {
            if (user.UserType != "ADMIN" && user.UserType != "OP") return false;
            try
            {
                conn.Open();
                string sqlStr = string.Format(@"INSERT INTO [dbo].[OPERATOR_ID]  ([ID],[PWD],[USER_TYPE]) VALUES('{0}','{1}','{2}')", user.ID,user.Password,user.UserType);
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
                else
                {
                    return false;
                }
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
        public bool deleteUser(string userName)
        {
            try
            {
                conn.Open();

                // Query string - vì xóa chỉ cần ID nên chúng ta ko cần 1 DTO, ID là đủ
                string SQL = string.Format(@"DELETE FROM .[dbo].[OPERATOR_ID] WHERE ID = '{0}'", userName);

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
        public List<DTO_Departurement> getDept()
        {

            String queryString = string.Format(@"SELECT TOP 1000 [CODE]
                                                              ,[NAME_ENG]
                                                              ,[NAME_VIE]
                                                          FROM .[dbo].[DEPARTMENT]");
            SqlDataAdapter da = new SqlDataAdapter(queryString, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<DTO_Departurement> listDept = new List<DTO_Departurement>();
            foreach (DataRow row in dt.Rows)
            {
                DTO_Departurement dept = new DTO_Departurement();
                dept.Code = row[0].ToString();
                dept.Eng = row[1].ToString();
                dept.Vie = row[2].ToString();
                listDept.Add(dept);
            }
            return listDept;

        }
        public DataTable getDeptTable()
        {

            String queryString = string.Format(@"SELECT TOP 1000 [CODE]
                                                              ,[NAME_ENG]
                                                              ,[NAME_VIE]
                                                          FROM .[dbo].[DEPARTMENT]");
            SqlDataAdapter da = new SqlDataAdapter(queryString, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;

        }
        public bool addCustomer(DTO_Departurement dept)
        {
            try
            {
                conn.Open();
                string sqlStr = string.Format(@"INSERT INTO [dbo].[DEPARTMENT]  ([CODE],[NAME_ENG],[NAME_VIE]) VALUES('{0}','{1}',N'{2}')", dept.Code, dept.Eng, dept.Vie);
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
                else
                {
                    return false;
                }
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
        public bool deleteDept(string deptCode)
        {
            try
            {
                conn.Open();

                // Query string - vì xóa chỉ cần ID nên chúng ta ko cần 1 DTO, ID là đủ
                string SQL = string.Format(@"DELETE FROM .[dbo].[DEPARTMENT] WHERE CODE = '{0}'", deptCode);

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
