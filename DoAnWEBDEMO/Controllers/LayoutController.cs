using DoAnWEBDEMO.ApplicationDB;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWEBDEMO.Controllers
{
    public class LayoutController : Controller
    {

        private readonly ApplicationDb _db;

        public LayoutController(ApplicationDb db)
        {
            _db = db;
        }

        public JsonResult getDataHeader()
        {
            var data = _db.Header
                        .OrderBy(h => h.ID).ToList();


            if (data != null)
            {
                return Json(data);
            }    
            return Json(null);
        }

        public JsonResult getDataFooter()
        {
            var data = _db.Footer
                        .Where(ft => ft.TrangThaiHienThi == 1)
                        .OrderBy(h => h.ID).ToList();
            if (data != null)
                return Json(data);

            return Json(null);
        }

     
    }
}
