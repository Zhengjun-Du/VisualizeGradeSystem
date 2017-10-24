using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

using NPOI.SS.UserModel;
using VisualizeGradeSystem.Models.Score;
using System.IO;

namespace VisualizeGradeSystem.Utils
{
    //把Excel里面的指定行读入数据库
    public class ReadExcel
    {
        public Score[] Read(string filepath)
        {
            IWorkbook wk = null;
            //Score[] scoreArray;
            List<Score> list = new List<Score>();
            string extension = Path.GetExtension(filepath);
//            try
//            {
                FileStream fs = File.OpenRead(filepath);
                if (extension.Equals(".xls"))
                {
                    //把xls文件中的数据写入wk中
                    wk = new HSSFWorkbook(fs);
                }
                else
                {
                    //把xlsx文件中的数据写入wk中
                    wk = new XSSFWorkbook(fs);
                }
                fs.Close();
                //读取当前表数据
                ISheet sheet = wk.GetSheetAt(6);
                IRow row = null;  //读取当前行数据
                //LastRowNum 是当前表的总行数-1（注意）
                //int offset = 0;
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    row = sheet.GetRow(i);  //读取当前行数据
                    if (row != null)
                    {
                        //LastCellNum 是当前行的总列数
                        Score s = new Score();
                        //string str = GetCellValue(row.GetCell(0)).ToString(); 
                        s.score_id = i - 2;
                        s.score_gradeid = GetCellValue(row.GetCell(0)).ToString();//考号
                        s.stu_name = GetCellValue(row.GetCell(1)).ToString();//姓名
                        s.stu_id = GetCellValue(row.GetCell(2)).ToString();//学号
                        s.score = Convert.ToDouble(GetCellValue(row.GetCell(4)).ToString());//成绩
                        s.updatetime = GetCellValue(row.GetCell(9)).ToString();//考试系统的更新时间
                        s.stu_class = GetCellValue(row.GetCell(12)).ToString();//考生的所属班级
                        s.stu_depart = GetCellValue(row.GetCell(13)).ToString(); //考生所属院系
                        list.Add(s);
                    }
                }
//            }

//            catch (Exception e)
//            {
                //只在Debug模式下才输出
//                Console.WriteLine(e.Message);
//            }
            //IEnumerable<Score> productList =  new
            return list.ToArray();
        }

        //获取cell的数据，并设置为对应的数据类型
        private object GetCellValue(ICell cell)
        {
            object value = null;
            try
            {
                if (cell.CellType != CellType.Blank)
                {
                    switch (cell.CellType)
                    {
                        case CellType.Numeric:
                            // Date comes here
                            if (DateUtil.IsCellDateFormatted(cell))
                            {
                                value = cell.DateCellValue;
                            }
                            else
                            {
                                // Numeric type
                                value = cell.NumericCellValue;
                            }
                            break;
                        case CellType.Boolean:
                            // Boolean type
                            value = cell.BooleanCellValue;
                            break;
                        case CellType.Formula:
                            value = cell.CellFormula;
                            break;
                        default:
                            // String type
                            value = cell.StringCellValue;
                            break;
                    }
                }
            }
            catch (Exception)
            {
                value = "";
            }
            return value;
        }
    }
}