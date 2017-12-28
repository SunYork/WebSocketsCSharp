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
            //����������
            HSSFWorkbook wk = new HSSFWorkbook();
            //����һ����
            ISheet tb = wk.CreateSheet();

            ICellStyle cellStyle = wk.CreateCellStyle();
            //���õ�Ԫ�����ʽ��ˮƽ�������
            cellStyle.VerticalAlignment = VerticalAlignment.Center;//��ֱ����
            cellStyle.Alignment = HorizontalAlignment.Center;//ˮƽ����
            cellStyle.IsHidden = false;
            IFont fontStyle = wk.CreateFont();
            fontStyle.FontName = "΢���ź�";//����
            fontStyle.FontHeightInPoints = 11;//�ֺ�
            fontStyle.Boldweight = 700;//����

            cellStyle.SetFont(fontStyle);
            int maxColumn = menus.Count;

            //������ͷ
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
                        //������Ԫ��
                        ICell cell = row.CreateCell(i);
                        string value = getCellValue(item, subItem, i);
                        cell.SetCellValue(value);//ѭ������Ԫ�����������
                        int childCount = childList.Count();
                        //�ϲ���Ԫ��
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
            Stopwatch sw = Stopwatch.StartNew();

            IEnumerable<int> cells = Enumerable.Range(1, 20);
            Dictionary<int, IEnumerable<int>> data = new Dictionary<int, IEnumerable<int>>();
            foreach (int i in Enumerable.Range(1, 10000))
            {
                data.Add(i, new List<int> { 11, 22 });
            }

            long tick1 = sw.ElapsedMilliseconds;
            sw.Restart();

            using (MemoryStream ms = new MemoryStream())
            {
                HSSFWorkbook wk = new HSSFWorkbook(ms);
                ISheet sheet = wk.CreateSheet();

                ICellStyle cellStyle = wk.CreateCellStyle();
                cellStyle.VerticalAlignment = VerticalAlignment.Center;
                cellStyle.Alignment = HorizontalAlignment.Center;
                cellStyle.IsHidden = false;
                IFont font = wk.CreateFont();
                font.FontName = "΢���ź�";
                font.FontHeightInPoints = 11;
                font.Boldweight = 700;
                cellStyle.SetFont(font);

                IRow index = sheet.CreateRow(0);
                index.ZeroHeight = false;
                foreach(int i in cells)
                {
                    ICell cell = index.CreateCell(i);
                    cell.SetCellValue(i);
                    cell.CellStyle = cellStyle;
                }
            }

            sw.Stop();
        }
    }
}
