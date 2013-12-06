using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.Windows.Forms;
using System.IO;
using FISCA.Presentation.Controls;

namespace K12.Retake.Shinmin
{
    public class Utility
    {
        public static void CompletedXls(string inputReportName, Workbook inputXls)
        {
            string reportName = inputReportName;

            string path = Path.Combine(Application.StartupPath, "Reports");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path = Path.Combine(path, reportName + ".xls");

            Workbook wb = inputXls;

            if (File.Exists(path))
            {
                int i = 1;
                while (true)
                {
                    string newPath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + (i++) + Path.GetExtension(path);
                    if (!File.Exists(newPath))
                    {
                        path = newPath;
                        break;
                    }
                }
            }

            try
            {
                wb.Save(path, Aspose.Cells.FileFormatType.Excel2003);
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "另存新檔";
                sd.FileName = reportName + ".xls";
                sd.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        wb.Save(sd.FileName, Aspose.Cells.FileFormatType.Excel2003);

                    }
                    catch
                    {
                        MsgBox.Show("指定路徑無法存取。", "建立檔案失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 取得日期的中文星期
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDayWeekString(DateTime dt)
        {
            string retVal = "";
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    retVal = "日";
                    break;

                case DayOfWeek.Monday:
                    retVal = "一";
                    break;

                case DayOfWeek.Tuesday:
                    retVal = "二";
                    break;

                case DayOfWeek.Wednesday:
                    retVal = "三";
                    break;

                case DayOfWeek.Thursday:
                    retVal = "四";
                    break;

                case DayOfWeek.Friday:
                    retVal = "五";
                    break;

                case DayOfWeek.Saturday:
                    retVal = "六";
                    break;
            }
            return retVal;
        }
    }
}
