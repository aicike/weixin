using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Aspose.Cells;

namespace Business.Common
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public class Common
    {

        #region------创建随机字符窜（不唯一）------------

        /// <summary>
        /// 创建随机字符窜（不唯一）
        /// </summary>
        /// <param name="str">随机数包含的字符 传"" 为默认a-z 1-9</param>
        /// <param name="length">返回的字符长度</param>
        /// <returns></returns>
        public string CreateRandom(string str, int length)
        {
            if (str == "")
            {
                str = "abcdefghijklmnopqrstuvwxyz0123456789";
            }
            Random rnd = new Random();
            string tmpstr = "";
            int iRandNum;
            for (int i = 0; i < length; i++)
            {
                iRandNum = rnd.Next(str.Length);
                tmpstr += str[iRandNum];
            }

            return tmpstr;
        }

        #endregion


        #region----------导出excel-------------
        public void Export(List<DataTable> dtList, string title, string path)
        {
            path = DisposePath(path);
            Workbook book = new Workbook();
            book.Worksheets.Clear();
            //遍历DataTable的泛型，将所有的DataTable填充到Sheet后保存到Excel文件中
            for (int index = 0; index < dtList.Count; index++)
            {
                DataTable dt = dtList[index];
                int sheetCount = 0;
                //如果此DataTable的数据行数大于65536，计算此DataTable需要填充的Sheet的个数
                if (dt.Rows.Count > 65535)
                {
                    if (dt.Rows.Count % 65535 != 0)
                    {
                        sheetCount = Convert.ToInt32(dt.Rows.Count / 65535) + 1;
                    }
                    else
                    {
                        sheetCount = Convert.ToInt32(dt.Rows.Count / 65535) / 65535;
                    }
                }
                //需要多Sheet填充的DataTable的导出处理
                if (sheetCount > 1)
                {
                    //将此DataTable按照65536的Excel极限数据行数进行切割导出到多Sheet
                    for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
                    {
                        Worksheet sheet = book.Worksheets.Add(dt.TableName + (index + 1).ToString() + "_" + (sheetIndex + 1).ToString());
                        int colCount = dt.Columns.Count;
                        //添加列头标题
                        for (int cIndex = 0; cIndex < colCount; cIndex++)
                        {
                            sheet.Cells[0, cIndex].PutValue(dt.Columns[cIndex].ToString());
                        }
                        if (sheetIndex + 1 == sheetCount)
                        {
                            //添加每一行的数据
                            for (int rIndex = 0; rIndex < dt.Rows.Count % 65535; rIndex++)
                            {
                                for (int cIndex = 0; cIndex < colCount; cIndex++)
                                {
                                    sheet.Cells[rIndex + 1, cIndex].PutValue(dt.Rows[65535 * sheetIndex + rIndex][cIndex].ToString());
                                }
                            }
                        }
                        else
                        {
                            //添加每一行的数据
                            for (int rIndex = 0; rIndex < 65535; rIndex++)
                            {
                                for (int cIndex = 0; cIndex < colCount; cIndex++)
                                {
                                    sheet.Cells[rIndex + 1, cIndex].PutValue(dt.Rows[65535 * sheetIndex + rIndex][cIndex].ToString());
                                }
                            }
                        }
                    }
                }
                else//不需要多Sheet填充的DataTable的导出处理
                {
                    Worksheet sheet = book.Worksheets.Add(dt.TableName + (index + 1).ToString());
                    FillSheet(dt, sheet);
                }
            }
            book.Save(path);
            book = null;
            GC.Collect();
        }
        private void FillSheet(DataTable dt, Worksheet sheet)
        {
            int colCount = dt.Columns.Count;
            int rowCount = dt.Rows.Count;
            //添加列头标题
            for (int i = 0; i < colCount; i++)
            {
                sheet.Cells[0, i].PutValue(dt.Columns[i].ToString());
            }
            //添加每一行的数据
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    sheet.Cells[i + 1, j].PutValue(dt.Rows[i][j].ToString());
                }
            }
        }
        /// <summary>
        /// 导出时的路径的处理
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string DisposePath(string path)
        {
            path = path.Replace("/", @"\");
            path = path.Substring(0, path.LastIndexOf("."));
            path += ".xls";
            return path;
        }
        #endregion
    }
}
