using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5HW1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public 客戶銀行資訊 Find(int id)
        {
            return this.All().Where(p => p.Id == id && p.是否已刪除 == false).FirstOrDefault();
        }
        public 客戶銀行資訊 DeleteData(客戶銀行資訊 客戶銀行資訊)
        {
            客戶銀行資訊.是否已刪除 = true;
            return 客戶銀行資訊;
        }
        public IEnumerable<客戶銀行資訊> DataIndex()
        {
            var Data = this.Where(p => p.是否已刪除 == false);
            return Data;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}