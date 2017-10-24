using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using VisualizeGradeSystem.Utils.filters;
using VisualizeGradeSystem.Models.User;
using VisualizeGradeSystem.Utils;
using VisualizeGradeSystem.Models.Score;

namespace VisualizeGradeSystem.Controllers
{
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/
        ScoreContext sc = new ScoreContext();
        public ActionResult Index()
        {
            User u = (User)Session["User"];
            if (u != null)
            {
                ViewBag.account = u.user_account;
            }
            return View();
        }

        [UserAuthorize]
        public ActionResult UploadFilesPage()
        {
            User u = (User)Session["User"];
            ViewBag.account = u.user_account;
            return View();
        }
        public ActionResult ChooseSubjectPage()
        {
            User u = (User)Session["User"];
            ViewBag.account = u.user_account;
            return View();
        }
        [HttpPost]
        public ActionResult ChooseSubject(string subject)
        {
            string today=DateTime.Now.ToString("yyyy-MM-dd");
            Score[] array = sc.ScoreList.Where(s => s.uploadtime.Contains(today)).ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                Score s = array[i];
                s.subject=subject;
                sc.ScoreList.Attach(s);
                sc.Entry(s).State = System.Data.EntityState.Modified;
                sc.SaveChanges();
                //sc. = subject;
                //sc.SaveChanges();
            }
            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            HttpPostedFileBase file = Request.Files[0];
            string fileName = file.FileName;
            string saveUrl = Server.MapPath("/") + "UploadFiles/GradeExcel/";
            Uploader up = new Uploader();
            ReadExcel re = new ReadExcel();
            up.UploadExcel(file, saveUrl);//上传了
            Score[] scoreArray = re.Read(saveUrl + file.FileName);

            for (int i = 0; i < scoreArray.Length; i++)
            {
                scoreArray[i].uploadtime = DateTime.Now.ToString("yyyy-MM-dd");
                sc.ScoreList.Add(scoreArray[i]);
                sc.SaveChanges();
            }
            return RedirectToAction("ChooseSubjectPage", "Teacher");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("", "Student");
        }

    }
}
