using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL_WareHouse;
using DTO_WareHouse;

namespace BUS_WareHouse
{
    public class BUS_User
    {
        DAL_User dalUser = new DAL_User();
        public DTO_User getUser(string id, string password)
        {
            id = id.ToUpper();
            return dalUser.getUser(id, password);
        }
        public DataTable getUserAll()
        {
            return dalUser.getUserAll();
        }
        public bool addUser(DTO_User user)
        {
            user.ID = user.ID.ToUpper();
            user.UserType = user.UserType.ToUpper();
            return dalUser.addUser(user);
        }
        public bool deleteUser(string username)
        {
            return dalUser.deleteUser(username.ToUpper());
        }
        public bool addCustomer(DTO_Departurement dept)
        {
            return dalUser.addCustomer(dept);
        }
        public List<DTO_Departurement> getDept()
        {
            return dalUser.getDept();
        }
        public DataTable getDeptTable()
        {
            return dalUser.getDeptTable();
        }
        public bool deleteDept(string code)
        {
             return dalUser.deleteDept(code);
        }
        
    }
}
