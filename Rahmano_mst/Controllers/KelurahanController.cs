using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Rahmano_mst.Models;

namespace Rahmano_mst.Controllers
{
    public class KelurahanController : Controller
    {
        kelurahanDB kabdb = new kelurahanDB();
        List<kelurahan> lprov = new List<kelurahan>();
        isiList lst = new isiList();
        public ActionResult Index()
        {
            List<provinsi> lsp = lst.lstProvinsi();
            provinsi prp = lsp[1];

            ViewBag.provinsi = new SelectList(lsp, "provinsi_id", "provinsi_name", prp.provinsi_id);
            ViewBag.kabupaten = new SelectList(lst.lstKabupaten(prp.provinsi_id), "kabupaten_id", "kabupaten_name", 0);
            ViewBag.kecamatan = new SelectList(lst.lstKecamatan(0), "kecamatan_id", "kecamatan_name", 0);

            return View();
        }
        public JsonResult SearchList(int id, int kid, int kcid, string kel)
        {
            return Json(new List<kelurahan>(kabdb.listKelurahan(id, kid, kcid, kel)));
        }
        public JsonResult getKabupaten(int id)
        {
            JsonResult js = Json(lst.lstKabupaten(id));
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return js;
        }
        public JsonResult getKecamatan(int id)
        {
            JsonResult js = Json(lst.lstKecamatan(id));
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return js;
        }
        public ActionResult Edit(int id = 0)
        {
            kelurahan kbt = kabdb.dataKelurahan(id);
            ViewBag.provinsi = new SelectList(lst.lstProvinsi(), "provinsi_id", "provinsi_name", kbt.provinsi_id);
            ViewBag.kabupaten = new SelectList(lst.lstKabupaten(kbt.provinsi_id), "kabupaten_id", "kabupaten_name", kbt.kabupaten_id);
            ViewBag.kecamatan = new SelectList(lst.lstKecamatan(kbt.kabupaten_id), "kecamatan_id", "kecamatan_name", kbt.kecamatan_id);

            return View(kbt);
        }
        public JsonResult Simpan()
        {
            if (Session["UID"] == null) { Session["UID"] = 1; }
            var serializer = new JavaScriptSerializer();
            kelurahan kab = serializer.Deserialize<kelurahan>(Request["kelurahan"].ToString());

            return Json(kabdb.simpanKelurahan(kab, Convert.ToInt16(Session["UID"])), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Hapus(int id)
        {
            if (Session["UID"] == null) { Session["UID"] = 1; }
            return Json(kabdb.hapusKelurahan(id, Convert.ToInt16(Session["UID"])), JsonRequestBehavior.AllowGet);
        }

    }
}
