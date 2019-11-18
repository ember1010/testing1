using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_WareHouse
{
    public class DTO_Item
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ENGName { get; set; }
        public string Type { get; set; }
        public string OrderType { get; set; }
        public string Maker { get; set; }
        public string PackingUnit { get; set; }
        public string ExportUnit { get; set; }
        public int QtyTrans { get; set; }
        public int MOQ { get; set; }
        public string PurchaseCode { get; set; }
        public string PartNumber { get; set; }
        public string AddedTime { get; set; }
        public string TechInfo { get; set; }
        public DTO_Item (string code,string name,string engName, string type, string orderType, string maker,  string packingUnit,string exportUnit,int qtyTrans,int _MOQ,string purchaseCode,string partNumber,string techInfo) 
        {
            Code = code;
            Name = name;
            ENGName = engName;
            Type = type;
            OrderType = orderType;
            Maker = maker;
            PackingUnit = packingUnit;
            ExportUnit = exportUnit;
            QtyTrans = qtyTrans;
            MOQ = _MOQ;
            PurchaseCode = purchaseCode;
            PartNumber = partNumber;
            TechInfo = techInfo;
        }
        public DTO_Item()
        {

        }
    }
    public class DTO_Departurement
    {
        public string Code { get; set; }
        public string Eng { get; set; }
        public string Vie { get; set; }
    }
}
