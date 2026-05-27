using BanXeHoi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanXeHoi.Controllers
{
    public class ThongKeXeController : Controller
    {
        // GET: ThongKeXe
        dbBanXeDataContext data = new dbBanXeDataContext();
        public ActionResult ThongKe()
        {
            var car_tk = from c in data.XeHois
                            join s in data.ChiTietDonHangs on c.maxe equals s.maxe
                            group s by new { c.tenxe } into g
                            select new ThongKe
                            {
                                xe = g.Key.tenxe,
                                soluong = g.Sum(s => s.soluong)
                            };

            return View(car_tk.ToList());
        }
    }
}