using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5HW1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public 客戶資料 Find(int id)
        {
            return this.All().Where(p => p.Id == id && p.是否已刪除==false).FirstOrDefault();
        }
        public 客戶資料 DeleteData(客戶資料 客戶資料)
        {
            客戶資料.是否已刪除 = true;
            return 客戶資料;
        }
        public IEnumerable<客戶資料> ClientIndex() {
            var ClientData = this.Where(p => p.是否已刪除==false);
            return ClientData;
        }

        public IEnumerable<客戶資料> QueryData(string Name,string Type,string Order) {
            var ClientData = this.ClientIndex();           
            if (!String.IsNullOrEmpty(Name))
            {
                ClientData = ClientData.Where(p => p.客戶名稱.Contains(Name));
            }
            if (!String.IsNullOrEmpty(Type))
            {
                ClientData = ClientData.Where(p => p.客戶分類 == Type);
            }
            switch (Order)
            {
                case "客戶名稱_desc":
                    ClientData = ClientData.OrderByDescending(s => s.客戶名稱);
                    break;
                case "統一編號":
                    ClientData = ClientData.OrderBy(s => s.統一編號);
                    break;
                case "統一編號_desc":
                    ClientData = ClientData.OrderByDescending(s => s.統一編號);
                    break;
                case "電話":
                    ClientData = ClientData.OrderBy(s => s.電話);
                    break;
                case "電話_desc":
                    ClientData = ClientData.OrderByDescending(s => s.電話);
                    break;
                case "傳真":
                    ClientData = ClientData.OrderBy(s => s.傳真);
                    break;
                case "傳真_desc":
                    ClientData = ClientData.OrderByDescending(s => s.傳真);
                    break;
                case "地址":
                    ClientData = ClientData.OrderBy(s => s.地址);
                    break;
                case "地址_desc":
                    ClientData = ClientData.OrderByDescending(s => s.地址);
                    break;
                case "Email":
                    ClientData = ClientData.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    ClientData = ClientData.OrderByDescending(s => s.Email);
                    break;
                case "客戶分類":
                    ClientData = ClientData.OrderBy(s => s.客戶分類);
                    break;
                case "客戶分類_desc":
                    ClientData = ClientData.OrderByDescending(s => s.客戶分類);
                    break;
                default:
                    ClientData = ClientData.OrderBy(s => s.客戶名稱);
                    break;
            }
            return ClientData;
        }
	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}