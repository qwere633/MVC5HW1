using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5HW1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public 客戶聯絡人 Find(int id)
        {
            return this.All().Where(p => p.Id == id && p.是否已刪除 == false).FirstOrDefault();
        }
        public 客戶聯絡人 DeleteData(客戶聯絡人 客戶聯絡人)
        {
            客戶聯絡人.是否已刪除 = true;
            return 客戶聯絡人;
        }
        public IEnumerable<客戶聯絡人> DataIndex()
        {
            var Data = this.Where(p => p.是否已刪除 == false);
            return Data;
        }

        public IEnumerable<客戶聯絡人> QueryData(string Name, string Type, string Order)
        {
            var Data = this.DataIndex();
            if (!String.IsNullOrEmpty(Name))
            {
                Data = Data.Where(p => p.姓名.Contains(Name));
            }
            if (!String.IsNullOrEmpty(Type))
            {
                Data = Data.Where(p => p.職稱 == Type);
            }
            switch (Order)
            {
                case "職稱_desc":
                    Data = Data.OrderByDescending(s => s.職稱);
                    break;
                case "姓名":
                    Data = Data.OrderBy(s => s.姓名);
                    break;
                case "姓名_desc":
                    Data = Data.OrderByDescending(s => s.姓名);
                    break;
                case "Email":
                    Data = Data.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    Data = Data.OrderByDescending(s => s.Email);
                    break;
                case "手機":
                    Data = Data.OrderBy(s => s.手機);
                    break;
                case "手機_desc":
                    Data = Data.OrderByDescending(s => s.手機);
                    break;
                case "電話":
                    Data = Data.OrderBy(s => s.電話);
                    break;
                case "電話_desc":
                    Data = Data.OrderByDescending(s => s.電話);
                    break;
                case "客戶名稱":
                    Data = Data.OrderBy(s => s.客戶資料.客戶名稱);
                    break;
                case "客戶名稱_desc":
                    Data = Data.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
                default:
                    Data = Data.OrderBy(s => s.職稱);
                    break;
            }
            return Data;
        }

    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}