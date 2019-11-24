using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL_WareHouse
{
    public class DBConnect
    {
        protected SqlConnection conn = new SqlConnection(@"Data Source=192.168.1.33\SQLEXPRESS;Initial Catalog=KAIZENDB;User ID=kaizen;Password=*963.*963.");
    }/*   */
    /*(@"Data Source=13.191.213.223\SQLEXPRESS;Initial Catalog=KAIZENDB;User ID=kaizen;Password=*963.*963.")*/
}
