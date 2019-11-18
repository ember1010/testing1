using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_WareHouse
{
    public class DTO_Warehouse_Export_Data
    {
        public int LOTID { get; set; }
        public int LOTNumber { get; set; }
        public string ValidCode { get; set; }
        public string ItemCode { get; set; }
        public string Name { get; set; }
        public string EngName { get; set; }
        public string ExportUnit { get; set; }
        public string QtyTrans { get; set; }
        public string InputDate { get; set; }
        public int MOQ { get; set; }      
        public string TechInfo { get; set; }
    }
}
