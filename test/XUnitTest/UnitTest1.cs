using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using NPOI.SS.Util;
using System.Diagnostics;

namespace XUnitTest
{
    public class UnitTest1
    {
        private async Task<MemoryStream> ExportWithTwoTier<T1, T2>(List<string> menus, IEnumerable<T1> parentList, Func<T1, IEnumerable<T2>> getChildList
                    , Func<T1, T2, int, string> getCellValue, Func<int, bool> getMerge)
        {
            //创建工作薄
            HSSFWorkbook wk = new HSSFWorkbook();
            //创建一个表
            ISheet tb = wk.CreateSheet();

            ICellStyle cellStyle = wk.CreateCellStyle();
            //设置单元格的样式：水平对齐居中
            cellStyle.VerticalAlignment = VerticalAlignment.Center;//垂直对齐
            cellStyle.Alignment = HorizontalAlignment.Center;//水平对齐
            cellStyle.IsHidden = false;
            IFont fontStyle = wk.CreateFont();
            fontStyle.FontName = "微软雅黑";//字体
            fontStyle.FontHeightInPoints = 11;//字号
            fontStyle.Boldweight = 700;//粗体

            cellStyle.SetFont(fontStyle);
            int maxColumn = menus.Count;

            //创建表头
            IRow index = tb.CreateRow(0);
            index.ZeroHeight = false;
            for (int i = 0; i < maxColumn; i++)
            {
                ICell cell = index.CreateCell(i);
                cell.SetCellValue(i + 1);
                cell.CellStyle = cellStyle;
            }

            IRow header = tb.CreateRow(1);
            header.ZeroHeight = false;
            header.HeightInPoints = 24;
            for (int i = 0; i < maxColumn; i++)
            {
                ICell cell = header.CreateCell(i);
                cell.SetCellValue(menus[i]);
                cell.CellStyle = cellStyle;
            }

            int count = 1;
            bool isMerge = true;
            int preRow = 0;
            foreach (var item in parentList)
            {
                IEnumerable<T2> childList = getChildList(item);
                foreach (var subItem in childList)
                {
                    count++;
                    IRow row = tb.CreateRow(count);
                    row.ZeroHeight = false;
                    if (count <= preRow)
                    {
                        isMerge = false;
                    }
                    else
                    {
                        isMerge = true;
                    }
                    for (int i = 0; i < maxColumn; i++)
                    {
                        //创建单元格
                        ICell cell = row.CreateCell(i);
                        string value = getCellValue(item, subItem, i);
                        cell.SetCellValue(value);//循环往单元格中添加数据
                        int childCount = childList.Count();
                        //合并单元格
                        if (isMerge && getMerge(i) && childCount > 1)
                        {
                            preRow = count + childCount - 1;
                            tb.AddMergedRegion(new CellRangeAddress(count, count + childCount - 1, i, i));
                        }
                    }
                }
            }

            //string fileName = name + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            MemoryStream ms = new MemoryStream();
            wk.Write(ms);
            ms.Position = 0;
            return ms;
        }

        [Fact]
        public void Test1()
        {
            /*
             * 问题点在于有很多需要Func获取的地方，本身可以直接给予字典类型数据。
             * 特别在合并单元格存在大量无用代码，并且每列都需要进行单独查询。其实
             * 可以直接将需要合并的列作为参数直接传入，而合并操作可以放在子类完成
             * 循环后然后对该父类进行合并，而不需要每一列都需要进行一次查询。
             */

            Stopwatch sw = Stopwatch.StartNew();

            IEnumerable<int> cells = Enumerable.Range(0, 20);
            Dictionary<int, IEnumerable<int>> data = new Dictionary<int, IEnumerable<int>>();
            foreach (int i in Enumerable.Range(1, 30000))
            {
                data.Add(i, new List<int> { 11, 22 });
            }

            long tick1 = sw.ElapsedMilliseconds;
            sw.Restart();

            using (MemoryStream ms = new MemoryStream())
            {
                HSSFWorkbook wk = new HSSFWorkbook();
                ISheet sheet = wk.CreateSheet();

                ICellStyle cellStyle = wk.CreateCellStyle();
                cellStyle.VerticalAlignment = VerticalAlignment.Center;
                cellStyle.Alignment = HorizontalAlignment.Center;
                cellStyle.IsHidden = false;
                IFont font = wk.CreateFont();
                font.FontName = "微软雅黑";
                font.FontHeightInPoints = 11;
                font.Boldweight = 700;
                cellStyle.SetFont(font);

                IRow index = sheet.CreateRow(0);
                index.ZeroHeight = false;
                foreach(int i in cells)
                {
                    ICell cell = index.CreateCell(i);
                    cell.SetCellValue($"Head {i}");
                    cell.CellStyle = cellStyle;
                }

                int rowNum = 1;
                foreach(var parentItem in data.Keys)
                {
                    foreach(var childItem in data[parentItem])
                    {
                        IRow row = sheet.CreateRow(rowNum++);
                        row.ZeroHeight = false;
                        for(var i = 0; i < cells.Count(); i++)
                        {
                            ICell cell = row.CreateCell(i);
                            cell.SetCellValue($"Parent:{parentItem},Child:{childItem},Cell:{i} test test test test test test test test test test");
                        }
                    }
                    sheet.AddMergedRegion(new CellRangeAddress(rowNum - 2, rowNum - 1, 19, 19));
                    sheet.AddMergedRegion(new CellRangeAddress(rowNum - 2, rowNum - 1, 18, 18));
                    sheet.AddMergedRegion(new CellRangeAddress(rowNum - 2, rowNum - 1, 17, 17));
                    sheet.AddMergedRegion(new CellRangeAddress(rowNum - 2, rowNum - 1, 16, 16));
                }
                wk.Write(ms);
                wk.Close();

                using (var fs = File.Open("D:\\Project\\WebSocketsCSharp\\test.xls", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                {
                    ms.Position = 0;
                    ms.CopyTo(fs);
                    fs.Flush();
                }
            }

            sw.Stop();
            var tick2 = sw.ElapsedMilliseconds;
        }
    }
}
