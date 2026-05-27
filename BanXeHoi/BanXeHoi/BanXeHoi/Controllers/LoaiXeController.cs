using BanXeHoi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanXeHoi.Controllers
{
    public class LoaiXeController : Controller
    {
        // GET: LoaiXe
        dbBanXeDataContext data = new dbBanXeDataContext();
        public ActionResult Index()
        {
            var all_theloai = from Loai in data.Loais select Loai;
            return View(all_theloai);
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Loai l)
        {
            var E_tenloai = collection["tenloai"];



            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Dont empty!";
            }
            else
            {
                l.tenloai = E_tenloai.ToString();

                data.Loais.InsertOnSubmit(l);
                data.SubmitChanges();
                return RedirectToAction("Index", "LoaiXe");
            }
            return this.Create();
        }



        public ActionResult Delete(int id)
        {
            var D_loaixe = data.Loais.First(m => m.maloai == id);
            return View(D_loaixe);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_loaixe = data.Loais.Where(m => m.maloai == id).First();
            data.Loais.DeleteOnSubmit(D_loaixe);
            data.SubmitChanges();
            return RedirectToAction("Index", "LoaiXe");
        }

        public ActionResult Edit(int id)
        {
            var E_loaixe = data.Loais.First(m => m.maloai == id);
            return View(E_loaixe);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_loaixe = data.Loais.First(m => m.maloai == id);
            var E_tenloai = collection["tenloai"];

            E_loaixe.maloai = id;
            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Dont empty!";
            }
            else
            {
                E_loaixe.tenloai = E_tenloai;

                UpdateModel(E_loaixe);
                data.SubmitChanges();
                return RedirectToAction("Index", "LoaiXe");
            }
            return this.Edit(id);
        }
    }
}