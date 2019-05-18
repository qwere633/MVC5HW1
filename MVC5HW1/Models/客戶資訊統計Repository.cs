using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5HW1.Models
{   
	public  class 客戶資訊統計Repository : EFRepository<客戶資訊統計>, I客戶資訊統計Repository
	{

	}

	public  interface I客戶資訊統計Repository : IRepository<客戶資訊統計>
	{

	}
}