using BanXeHoi.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanXeHoi.Controllers
{
    public class XeHoiController : Controller
    {
        // GET: XeHoi
        dbBanXeDataContext data = new dbBanXeDataContext();
        public ActionResult Index(int? page, string searchString)
        {
            ViewBag.Keyword = searchString;

            if (page == null) page = 1;
            var all_car = (from x in data.XeHois select x).OrderBy(m=>m.tenxe);
            if (!string.IsNullOrEmpty(searchString)) all_car= (IOrderedQueryable<XeHoi>)
                    all_car.Where(a=> a.tenxe.Contains(searchString));
            int pageSize = 3;
            int pageNum = page ?? 1;
            return View(all_car.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Detail(int id)
        {
            var d_car = data.XeHois.Where(m => m.maxe == id).First();
            return View(d_car);
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, XeHoi x)
        {
            var E_tenxe = collection["tenxe"];
            var E_hinh = collection["hinh"];
            var E_giaban = Convert.ToDecimal(collection["giaban"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["ngaycapnhat"]);
            var E_soluongton = Convert.ToInt32(collection["soluongton"]);
            if (string.IsNullOrEmpty(E_tenxe))
            {
                ViewData["Error"] = "Dont empty!";
            }
            else
            {
                x.tenxe = E_tenxe.ToString();
                x.hinh = E_hinh.ToString();
                x.giaban = E_giaban;
                x.ngaycapnhat = E_ngaycapnhat;
                x.soluongton = E_soluongton;
                data.XeHois.InsertOnSubmit(x);
                data.SubmitChanges();
                return RedirectToAction("Index", "XeHoi");
            }
            return this.Create();
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }

        public ActionResult Edit(int id)
        {
            var E_car = data.XeHois.First(m => m.maxe == id);
            return View(E_car);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_car = data.XeHois.First(m => m.maxe == id);
            var E_tenxe = collection["tenxe"];
            var E_hinh = collection["hinh"];
            var E_giaban = Convert.ToDecimal(collection["giaban"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["ngaycapnhat"]);
            var E_soluongton = Convert.ToInt32(collection["soluongton"]);
            E_car.maxe = id;
            if (string.IsNullOrEmpty(E_tenxe))
            {
                ViewData["Error"] = "Dont empty!";
            }
            else
            {
                E_car.tenxe = E_tenxe;
                E_car.hinh = E_hinh;
                E_car.giaban = E_giaban;
                E_car.ngaycapnhat = E_ngaycapnhat;
                E_car.soluongton = E_soluongton;
                UpdateModel(E_car);
                data.SubmitChanges();
                return RedirectToAction("Index", "XeHoi");
            }
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var D_car = data.XeHois.First(m => m.maxe == id);
            return View(D_car);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_car = data.XeHois.Where(m => m.maxe == id).First();
            data.XeHois.DeleteOnSubmit(D_car);
            data.SubmitChanges();
            return RedirectToAction("Index", "XeHoi");
        }
    }
}