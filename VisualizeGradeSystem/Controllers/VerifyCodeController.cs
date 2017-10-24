using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisualizeGradeSystem.Utils;

namespace VisualizeGradeSystem.Controllers
{
    public class VerifyCodeController : Controller
    {
        //
        // GET: /Verify/

        public ActionResult Index(string time)
        {
            string code = ValidateCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = ValidateCode.CreateValidateGraphic(code);
            return File(bytes, "image/jpeg");
        }

        [HttpPost]
        public ActionResult CheckVerifyCode(string verifycode)
        {
            if (Session["ValidateCode"].ToString() != verifycode)
            {
                return Content("验证码不正确，请重新输入!");
            }
            else
            {
                return Content(null);
            }
        }

    }
}
