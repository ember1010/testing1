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
    public class BUS_ItemList
    {
        private DAL_ItemList dalWareHouse = new DAL_ItemList();
        private DAL_Categories_Menu dalCategoriesMenu = new DAL_Categories_Menu();
        public DataTable GetItemList()
        {
            return dalWareHouse.GetItemList();
        }
        public bool AddItemList(DTO_Item item)
        {
            return dalWareHouse.AddItemList(item);
        }
        public bool EditItemList(DTO_Item item,string itemCode)
        {
            return dalWareHouse.EditItem(item, itemCode);
        }
        public bool DeleteItemList(string itemCode)
        {
            return dalWareHouse.DeleteItem(itemCode);
        }
        public List<DTO_Item> getItemCodeName(string type)
        {
            return dalWareHouse.getItemCodeName(type);
        }
        public void getCategoriesMenu(Dictionary<string, List<string>> dictCategoriesMenu)
        {
            dalCategoriesMenu.GetCategoriesMenu(dictCategoriesMenu);
        }
        public DataTable getCategoriesMenu()
        {
            return dalCategoriesMenu.GetCategoriesMenu();
        }
        public bool AddCategoriesMenu(string categories,string menu)
        {
            return dalCategoriesMenu.AddCategoriesMenu(categories,menu);
        }
        public bool DeleteCategoriesMenu(string categories, string menu)
        {
            return dalCategoriesMenu.deleteCategoriesMenu(categories, menu);
        }
    }
}
