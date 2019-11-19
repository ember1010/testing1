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
    public class BUS_Warehouse_Data
    {
        DAL_Warehouse_Data dalWarehouseData = new DAL_Warehouse_Data();
        public DataTable getWarehouseData()
        {
            return dalWarehouseData.getWarehouseData();
        }
        public DataTable getWarehouseDataByLOT(string lot)
        {
            return dalWarehouseData.getWarehouseDataByLOT(lot);
        }
        public DataTable getWarehouseDataFull()
        {
            return dalWarehouseData.getWarehouseDataFull();
        }
        public DataTable getWarehouseDataExport(DateTime dtFrom, DateTime dtTo)
        {
            return dalWarehouseData.getWarehouseDataExport(dtFrom,dtTo);
        }
        public DataTable getWarehouseDataActive(DateTime dtFrom, DateTime dtTo)
        {
            return dalWarehouseData.getWarehouseDataActive(dtFrom,dtTo);
        }
        public DataTable getWarehouseDataImport(DateTime dtFrom, DateTime dtTo)
        {
            return dalWarehouseData.getWarehouseDataImport(dtFrom, dtTo);
        }
        public DataTable getCurrentWarehouseData()
        {
            return dalWarehouseData.getCurrentWarehouseData();
        }
        public DataTable getPOWarehouseData()
        {
            return dalWarehouseData.getpPOWarehouseData();
        }
        public DataTable getPOWarehouseDataPO(string PO)
        {
            return dalWarehouseData.getpPOWarehouseDataPO(PO);
        }
        public DataTable getPOWarehouseDataCatalog(string Catalog)
        {
            return dalWarehouseData.getpPOWarehouseDataEngName(Catalog);
        }
        public DataTable getPOWarehouseDataItemCode(string itemCode)
        {
            return dalWarehouseData.getpPOWarehouseDataItemCode(itemCode);
        }
        public DataTable getPOWarehouseDataDateTime(string po,string name,string itemCode,DateTime fromTime,DateTime toTime)
        {
            return dalWarehouseData.getpPOWarehouseDataDateTime(po,name,itemCode,fromTime, toTime);
        }
        public string addLot(string lotID,int quantity,string operatorID,string po,int price,string moneyUnit)
        {
            return dalWarehouseData.addNewLOT(lotID,quantity,operatorID,po,price,moneyUnit);
        }
        public string getBarcodeString(int lot, int number)
        {
            return dalWarehouseData.getBarcodeString(lot, number);
        }
        public DataTable exportLotNumber(int lot,int number,string code,string user,string customer,string reciever,DataTable dt)
        {
            DataTable newTable = dalWarehouseData.exportLOTNumber(lot, number, code, user,reciever, customer);
            if (dt != null)
            {
                dt.Merge(newTable);
            }
            else
            {
                dt = newTable;
            }
            return dt;
        }
        public DTO_Warehouse_Export_Data getExportInfomation(int lot,int number,string code)
        {
            return dalWarehouseData.getExportItemInfo(lot, number,code);
        }
        public List<DTO_Warehouse_Export_Data> getTempListItem(string deptCode)
        {
            return dalWarehouseData.getTempListItem(deptCode);
        }
        public bool saveTempListItem(DTO_Warehouse_Export_Data item,string deptCode)
        {
            return dalWarehouseData.saveTempListItem(item,deptCode);
        }
        public List<string> getSavedDeparture()
        {
            return dalWarehouseData.getSavedDeparture();
        }
    }
}
