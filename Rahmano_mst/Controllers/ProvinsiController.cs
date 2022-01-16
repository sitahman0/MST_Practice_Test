using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Rahmano_mst.Models;

namespace Rahmano_mst.Controllers
{
    public class ProvinsiController : Controller
    {
        provinsiDB prodb = new provinsiDB();
        List<provinsi> lprov = new List<provinsi>();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SearchList(string prov)
        {
            return Json(new List<provinsi>(prodb.listProvinsi(prov)));
        }

        public ActionResult Edit(int id = 0)
        {
            return View(prodb.dataProvinsi(id));
        }
        public JsonResult Simpan()
        {
            if (Session["UID"] == null) { Session["UID"] = 1; }
            var serializer = new JavaScriptSerializer();
            provinsi prov = serializer.Deserialize<provinsi>(Request["provinsi"].ToString());

            return Json(prodb.simpanProvinsi(prov, Convert.ToInt16(Session["UID"])), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Hapus(int id)
        {
            if (Session["UID"] == null) { Session["UID"] = 1; }
            return Json(prodb.hapusProvinsi(id, Convert.ToInt16(Session["UID"])), JsonRequestBehavior.AllowGet);
        }
    }
}