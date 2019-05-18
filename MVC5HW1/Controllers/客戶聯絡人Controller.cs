using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5HW1.Models;

namespace MVC5HW1.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶資料Repository ClientRepo;
        客戶聯絡人Repository ContactRepo;
        // GET: 客戶聯絡人
        public 客戶聯絡人Controller()
        {
            ClientRepo = RepositoryHelper.Get客戶資料Repository();
            ContactRepo = RepositoryHelper.Get客戶聯絡人Repository();
        }
        public ActionResult Index(string Name,string Types,string Order)
        {
            var ContactData = ContactRepo.DataIndex();
            var TypeLst = new List<string>();
            var TypeQry = from p in ContactData
                          select p.職稱;
            TypeLst.AddRange(TypeQry.Distinct());
            ViewBag.Type = new SelectList(TypeLst);
            ViewBag.職稱SortParm = String.IsNullOrEmpty(Order) ? "職稱_desc" : "";
            ViewBag.姓名SortParm = Order == "姓名" ? "姓名_desc" : "姓名";
            ViewBag.EmailSortParm = Order == "Email" ? "Email_desc" : "Email";
            ViewBag.手機SortParm = Order == "手機" ? "手機_desc" : "手機";
            ViewBag.電話SortParm = Order == "電話" ? "電話_desc" : "電話";           
            ViewBag.客戶名稱SortParm = Order == "客戶名稱" ? "客戶名稱_desc" : "客戶名稱";
            return View(ContactData.ToList());
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = ContactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(ClientRepo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                ContactRepo.Add(客戶聯絡人);
                ContactRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(ClientRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = ContactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(ClientRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                ContactRepo.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                ContactRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(ClientRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = ContactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = ContactRepo.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            ContactRepo.DeleteData(客戶聯絡人);
            ContactRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ContactRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
