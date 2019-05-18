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
    public class 客戶資料Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶資料Repository ClientRepo;
        // GET: 客戶資料
        public 客戶資料Controller()
        {
            ClientRepo = RepositoryHelper.Get客戶資料Repository();
        }
        public ActionResult Index(string Name,string Type,string Order)
        {
            var ClientData = ClientRepo.QueryData(Name,Type,Order);
            var TypeLst = new List<string>();
            var TypeQry = from p in ClientData
                          select p.客戶分類;
            TypeLst.AddRange(TypeQry.Distinct());
            ViewBag.Type = new SelectList(TypeLst);
            ViewBag.客戶名稱SortParm = String.IsNullOrEmpty(Order) ? "客戶名稱_desc" : "";
            ViewBag.統一編號SortParm = Order == "統一編號" ? "統一編號_desc" : "統一編號";
            ViewBag.電話SortParm = Order == "電話" ? "電話_desc" : "電話";
            ViewBag.傳真SortParm = Order == "傳真" ? "傳真_desc" : "傳真";
            ViewBag.地址SortParm = Order == "地址" ? "地址_desc" : "地址";
            ViewBag.EmailSortParm = Order == "Email" ? "Email_desc" : "Email";
            ViewBag.客戶分類SortParm = Order == "客戶分類" ? "客戶分類_desc" : "客戶分類";
            return View(ClientData.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = ClientRepo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,是否已刪除,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                ClientRepo.Add(客戶資料);
                ClientRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = ClientRepo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,是否已刪除,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                ClientRepo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                ClientRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = ClientRepo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = ClientRepo.Find(id);
            //db.客戶資料.Remove(客戶資料);
            ClientRepo.DeleteData(客戶資料);
            ClientRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ClientRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
