using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisualizeGradeSystem.Utils.filters;
using VisualizeGradeSystem.Models.User;
using System.Web.Security;
using VisualizeGradeSystem.Models.Score;

namespace VisualizeGradeSystem.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        ScoreContext sc = new ScoreContext();
        public ActionResult Index()
        {
            User u = (User)Session["User"];
            if (u != null)
            {
                ViewBag.account = u.user_account;
                ViewBag.name = u.user_name;
            }
            return View();
        }
        [UserAuthorize]
        public ActionResult PersonalLinePage()
        {
            User u = (User)Session["User"];
            ViewBag.account = u.user_account;
            return View();
        }
        [UserAuthorize]
        public ActionResult GradeClassChartPage()
        {
            User u = (User)Session["User"];
            ViewBag.account = u.user_account;
            return View();
        }
        //获得当前学号的的成绩
        [HttpPost]
        public JsonResult PersonalLine()
        {
            User u = (User)Session["User"];
            Score[] list = sc.ScoreList.Where(s => s.stu_id.Contains(u.user_account)).ToArray();
            List<double> scoreList = new List<double>();
            for(int i=0;i<list.Length;i++)
            {
                scoreList.Add(list[i].score);
            }
            return Json(scoreList);
        }
        //退出登录
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("", "Student");
        }
    }
}
