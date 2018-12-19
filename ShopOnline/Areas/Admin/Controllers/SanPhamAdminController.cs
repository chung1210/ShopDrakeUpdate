using ShopOnline.Models.BUS;
using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class SanPhamAdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: Admin/SanPhamAdmin
        public ActionResult Index()
        {

            return View(ShopOnlineBUS.DanhSachSP());
        }

        // GET: Admin/SanPhamAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/SanPhamAdmin/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSanPham = new SelectList(LoaiSanPhamBUS.DanhSach(), "MaLoaiSanPham", "TenLoaiSanPham");
          
            return View();
        }

        // POST: Admin/SanPhamAdmin/Create
        [HttpPost]
        public ActionResult Create(SanPham sp)
        {
            try
            {
                var hpf = HttpContext.Request.Files[0];
                if(hpf.ContentLength>0)
                {
                    string fileName = sp.MaSanPham;
                    string fullPathWithFileName = "~/Asset/img/" + fileName + ".png";
                    hpf.SaveAs(Server.MapPath(fullPathWithFileName));
                    sp.HinhChinh = sp.MaSanPham + ".png";
                }
                var hpf1 = HttpContext.Request.Files[1];
                if (hpf1.ContentLength > 0)
                {
                    string fileName = sp.MaSanPham;
                    string fullPathWithFileName = "~/Asset/img/" + fileName + ".png";
                    hpf1.SaveAs(Server.MapPath(fullPathWithFileName));
                    sp.Hinh1 = sp.MaSanPham + "_1.png";
                }
                sp.TinhTrang = "0";
                sp.SoLuongDaBan = 0;
                // TODO: Add insert logic here
                ShopOnlineBUS.InsertSP(sp);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/SanPhamAdmin/Edit/5
        public ActionResult Edit(String id)
        {
            ViewBag.MaLoaiSanPham = new SelectList(LoaiSanPhamBUS.DanhSach(), "MaLoaiSanPham", "TenLoaiSanPham");
            return View(ShopOnlineBUS.ChiTiet(id));
        }

        // POST: Admin/SanPhamAdmin/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(String id, SanPham sp)
        {
            var tam = ShopOnlineBUS.ChiTiet(id);
            try
            {
                var hpf = HttpContext.Request.Files[0];
                if (hpf.ContentLength > 0)
                {
                    string fileName = sp.MaSanPham;
                    string fullPathWithFileName = "~/Asset/img/" + fileName + ".png";
                    hpf.SaveAs(Server.MapPath(fullPathWithFileName));
                    sp.HinhChinh = sp.MaSanPham + ".png";
                } else { sp.HinhChinh = tam.HinhChinh; }
                var hpf1 = HttpContext.Request.Files[1];
                if (hpf1.ContentLength > 0)
                {
                    string fileName = sp.MaSanPham;
                    string fullPathWithFileName = "~/Asset/img/" + fileName + ".png";
                    hpf1.SaveAs(Server.MapPath(fullPathWithFileName));
                    sp.Hinh1 = sp.MaSanPham + "_1.png";
                } else { sp.Hinh1 = tam.Hinh1; }
                if(sp.SoLuongDaBan>10000)
                {
                    sp.SoLuongDaBan = 0;
                } else { sp.SoLuongDaBan = tam.SoLuongDaBan; }
                if(sp.LuotView>10000) { sp.LuotView = 0; } else { sp.LuotView = tam.LuotView; }
                sp.TinhTrang = tam.TinhTrang;
                ShopOnlineBUS.UpdateSP(id, sp);
                // TODO: Add insert logic here
               

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/SanPhamAdmin/Delete/5
        public ActionResult Delete(String id)
        {

            return View(ShopOnlineBUS.ChiTiet(id));
        }

        // POST: Admin/SanPhamAdmin/Delete/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Delete(String id,SanPham sp)
        {
            var tam = ShopOnlineBUS.ChiTiet(id);
            try
            {
               
                if (tam.SoLuongDaBan > 10000)
                {
                    tam.SoLuongDaBan = 0;
                }
              
                if (tam.LuotView > 10000) { tam.LuotView = 0; } 
                if(tam.TinhTrang=="1          ") { tam.TinhTrang = "0           "; }
                else
                {
                    tam.TinhTrang = "1        ";
                }
                ShopOnlineBUS.UpdateSP(id, tam);

                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
