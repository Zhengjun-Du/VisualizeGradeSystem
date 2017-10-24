using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualizeGradeSystem.Utils
{
    public class Uploader
    {
        private string getFileName(string fileName)
        {
            string name = null;
            if (fileName.IndexOf('.') > 0)
            {
                string[] fs = fileName.Split('.');
                name = fs[0];
            }
            return name;
        }
        public void UploadExcel(HttpPostedFileBase file, string saveLocal)
        {
            string saveUrl = saveLocal;
            string Ext = System.IO.Path.GetExtension(file.FileName);
            string Name = getFileName(file.FileName);
            //string info="/UploadFiles/GradeExcel/" + Name + "." + Ext;
            //判断路径是否存在
            if (!System.IO.Directory.Exists(saveUrl))
            {
                //如果不存在则生成目录
                System.IO.Directory.CreateDirectory(saveUrl);
            }
            //保存文件
            file.SaveAs(saveUrl + Name + Ext);

            //return info;
        }
    }
}