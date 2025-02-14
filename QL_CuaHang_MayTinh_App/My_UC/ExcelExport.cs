﻿using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace QL_CuaHang_MayTinh_App.My_UC
{
    public class ExcelExport
    {
        #region ---- Constants ----

        private const int ROW_MAXIMUM = 200;
        private const int COL_MAXIMUM = 256;

        private const string FONT_NAME = "Arial";
        private const int HEADER_FONT_SIZE = 16;
        private const int SUBHEADER_FONT_SIZE = 13;
        private const int CAPTION_FONT_SIZE = 10;
        private const int CONTENT_FONT_SIZE = 10;

        #endregion

        #region ---- Member variables ----

        private IWorkbook _workBook;

        #endregion

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelExport"/> class.
        /// </summary>
        public ExcelExport()
        {

        }

        #endregion

        #region ---- Private methods ----

        private void WriteColumHeader(IWorksheet xlsSheet, int startRow, int startCol, string[] arrColName, int[] arrColWidth, int rowHeight)
        {
            for (int i = 0; i < arrColName.Length; i++)
            {
                xlsSheet.Range[startRow, startCol + i].Text = arrColName[i];
                xlsSheet.Range[startRow, startCol + i].ColumnWidth = arrColWidth[i];
            }

            xlsSheet.Range[startRow, 1].RowHeight = rowHeight;
            CellStyle(xlsSheet, startRow, startCol, startRow, startCol + arrColName.Length, FONT_NAME, CAPTION_FONT_SIZE, true, false);
            xlsSheet.Range[startRow, startCol, startRow, startCol + arrColName.Length].HorizontalAlignment = ExcelHAlign.HAlignCenter;
            xlsSheet.Range[startRow, startCol, startRow, startCol + arrColName.Length].VerticalAlignment = ExcelVAlign.VAlignCenter;
            xlsSheet.Range[startRow, startCol, startRow, startCol + arrColName.Length].WrapText = true;
        }

        /// <summary>
        /// Draws the table border.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        /// <param name="lineStyle">The line style.</param>
        /// <Author>LONG LY</Author>
        /// <Date>25/07/2011</Date>
        private void DrawTableBorder(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, ExcelLineStyle lineStyle)
        {
            xlsSheet.IsGridLinesVisible = false;

            xlsSheet[startRow, startCol, endRow, endCol].CellStyle.Borders.LineStyle = lineStyle;
            xlsSheet[startRow, startCol, endRow, endCol].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            xlsSheet[startRow, startCol, endRow, endCol].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
            xlsSheet[startRow, startCol, endRow, endCol].CellStyle.Borders.ColorRGB = Color.Black;

            xlsSheet.Range[startRow, startCol, endRow, endCol].WrapText = true;
        }

        #region ---- Format -----

        /// <summary>
        /// Colses the alighment.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="HAlight">The H alight.</param>
        private void ColsAlighment(IWorksheet xlsSheet, int[] cols, ExcelHAlign HAlight)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                ColAlighment(xlsSheet, cols[i], HAlight);
            }
        }

        /// <summary>
        /// Cols the alighment.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="col">The start col.</param>
        /// <param name="HAlight">The H alight.</param>
        private void ColAlighment(IWorksheet xlsSheet, int col, ExcelHAlign HAlight)
        {
            xlsSheet.Range[1, col, ROW_MAXIMUM, col].CellStyle.HorizontalAlignment = HAlight;
            xlsSheet.Range[1, col, ROW_MAXIMUM, col].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
        }

        /// <summary>
        /// Cells the alighment.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        /// <param name="HAlight">The H alight.</param>
        /// <param name="VAlight">The V alight.</param>
        private void CellAlighment(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, ExcelHAlign HAlight, ExcelVAlign VAlight)
        {
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.HorizontalAlignment = HAlight;
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.VerticalAlignment = VAlight;
        }

        /// <summary>
        /// Cells the style.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        /// <param name="isBold">if set to <c>true</c> [is bold].</param>
        private void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, bool isBold)
        {
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.Font.Bold = isBold;
        }

        /// <summary>
        /// Cells the style.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        /// <param name="isBold">if set to <c>true</c> [is bold].</param>
        /// <param name="isItalic">if set to <c>true</c> [is italic].</param>
        private void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, bool isBold, bool isItalic)
        {
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, isBold);
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.Font.Italic = isItalic;
        }

        /// <summary>
        /// Cells the style.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        /// <param name="color">The color.</param>
        private void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, ExcelKnownColors color)
        {
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.Font.Color = color;
        }

        /// <summary>
        /// Cells the style.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        /// <param name="isBold">if set to <c>true</c> [is bold].</param>
        /// <param name="color">The color.</param>
        private void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, bool isBold, ExcelKnownColors color)
        {
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, isBold);
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, color);
        }

        /// <summary>
        /// Cells the style.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        /// <param name="isBold">if set to <c>true</c> [is bold].</param>
        /// <param name="isItalic">if set to <c>true</c> [is italic].</param>
        /// <param name="color">The color.</param>
        private void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, bool isBold, bool isItalic, ExcelKnownColors color)
        {
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, isBold, isItalic);
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, color);
        }

        /// <summary>
        /// Cells the style.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        /// <param name="fontName">Name of the font.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="isBold">if set to <c>true</c> [is bold].</param>
        /// <param name="isItalic">if set to <c>true</c> [is italic].</param>
        private void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, string fontName, int fontSize, bool isBold, bool isItalic)
        {
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.Font.FontName = fontName;
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.Font.Size = fontSize;
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, isBold, isItalic);
        }

        /// <summary>
        /// Cells the style back ground.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        private void CellStyleBackGround(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol)
        {
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.ColorIndex = ExcelKnownColors.Grey_25_percent;
        }

        #endregion ---- Format -----

        /// <summary>
        /// Pages the setup.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="PageOrientation">The page orientation.</param>
        /// <param name="isSmall">if set to <c>true</c> [is small].</param>
        private void PageSetup(IWorksheet xlsSheet, ExcelPageOrientation PageOrientation, bool isSmall)
        {
            // Setting the file name in the Footer
            xlsSheet.PageSetup.RightFooter = "&P";
            // Setting Page Number
            xlsSheet.PageSetup.AutoFirstPageNumber = false;
            xlsSheet.PageSetup.FirstPageNumber = 1;
            // Setting Page Margins
            xlsSheet.PageSetup.TopMargin = 0.35;
            xlsSheet.PageSetup.BottomMargin = 0.5;
            xlsSheet.PageSetup.LeftMargin = 0.35;
            xlsSheet.PageSetup.RightMargin = 0.2;

            xlsSheet.PageSetup.HeaderMargin = 0.3;
            xlsSheet.PageSetup.FooterMargin = 0.5;
            // Setting the Paper Type
            if (isSmall)
            {
                xlsSheet.PageSetup.PaperSize = ExcelPaperSize.PaperA5;
            }
            else
            {
                xlsSheet.PageSetup.PaperSize = ExcelPaperSize.PaperA4;
            }

            // Setting the Page Orientation as Portrait or Landscape
            xlsSheet.PageSetup.Orientation = PageOrientation;
        }

        /// <summary>
        /// Saves the excel.
        /// </summary>
        /// <param name="isPrint">if set to <c>true</c> [is print].</param>
        /// <returns></returns>
        private string SaveExcel(ExcelEngine xslEngine, bool isPrint, string defaultName = "", bool usingStyle = false)
        {
            string result = string.Empty;

            try
            {
                if (isPrint)
                {
                    result = Path.GetTempFileName() + ".xls";
                    _workBook.SaveAs(result);
                }
                else
                {
                    string extension = "xsl";
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Files(*.xls)|*.xls";
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.DefaultExt = "." + extension;
                    saveFileDialog.FileName = defaultName;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.CheckPathExists)
                    {
                        _workBook.Version = (ExcelVersion.Excel97to2003);
                        _workBook.SaveAs(saveFileDialog.FileName);
                        if (MessageBox.Show("Mở file vừa xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process proc = new System.Diagnostics.Process();
                            proc.StartInfo.FileName = saveFileDialog.FileName;

                            result = saveFileDialog.FileName;

                            proc.Start();
                        }
                    }
                }

                _workBook.Close();
            }
            catch
            { }
            finally
            {
                xslEngine.Dispose();
            }

            return result;
        }

        #endregion

        #region ---- Export Excel Template ----

        #region ---- Constants ----

        #region ---- Name Template ----


        public const string DoanhThu = "DoanhThu";
        #endregion

        #region ---- Variables ----

        // Utility                        
        private const string TMP_SHEET = "TMP";
        private const string TMP_ROW = "[TMP]";

        #endregion

        #endregion

        #region ---- Private methods ----

        private string SaveFile(bool pIsPrint = true)
        {
            string result = string.Empty;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = Constants.FILTER_EXCEL;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = Constants.FILE_EXT_XLS;

            if (!pIsPrint && saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.CheckPathExists)
            {
                result = saveFileDialog.FileName;
            }

            return result;
        }


        public void OpenFile(string pPathFile)
        {
            if (string.IsNullOrEmpty(pPathFile))
            {
                return;
            }

            if (MessageBox.Show("Bạn muốn mở file", "Thông tin", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = pPathFile;
                proc.Start();
            }
        }


        private static void Replace(IWorksheet workSheet, string findValue, string replaceValue)
        {
            // Find and replace
            if (workSheet != null && !string.IsNullOrEmpty(findValue))
            {
                // Get current cells
                IRange[] cells = workSheet.Range.Cells;
                IRange range = null;

                // Loop cells to replace
                for (int i = 0; i < cells.Count(); i++)
                {
                    // Current cell
                    range = cells[i];

                    // Find and replace values
                    if (range != null && range.DisplayText.Contains(findValue))
                    {
                        range.Text = range.Text.Replace(findValue, replaceValue);
                        break;
                    }
                }
            }
        }

        public static void PrintExcel(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = null;

            try
            {
                wb = excelApp.Workbooks.Open(fileName);

                if (wb != null)
                {
                    // Show print preview
                    excelApp.Visible = true;
                    wb.PrintPreview(true);
                }
            }
            catch (Exception ex)
            {
                //ShowMessage
            }
            finally
            {
                // Cleanup:
                GC.Collect();
                GC.WaitForPendingFinalizers();

                wb.Close(false, Type.Missing, Type.Missing);
                Marshal.FinalReleaseComObject(wb);

                excelApp.Quit();
                Marshal.FinalReleaseComObject(excelApp);
            }
        }


        private void BuildReplacerCurrentDate(ref Dictionary<string, string> pReplacer)
        {
            if (pReplacer != null)
            {
                DateTime currentDate = DateTime.Now;
                string ngay = "Ngày " + currentDate.Day + " tháng " + currentDate.Month + " năm " + currentDate.Year;
                pReplacer.Add("%NgayThangNam", ngay);
                pReplacer.Add("%TongSo", "10");
            }
        }



        private bool OutGroupReport<T>(List<IGrouping<int, T>> groupData, Dictionary<string, string> replaceValues,
                                        string groupBox, string viewName, bool isPrintPreview, ref string fileName, string groupName)
        {
            string file = string.Empty;
            bool result = false;

            // Get template stream
            MemoryStream stream = GetTemplateStream(viewName);

            // Check if data is null
            if (stream == null)
            {
                return false;
            }

            // Create excel engine
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workBook = engine.Excel.Workbooks.Open(stream);

            // Get sheets
            IWorksheet workSheet = workBook.Worksheets[0];
            IWorksheet tmpSheet = workBook.Worksheets.Create(TMP_SHEET);

            // Copy template of group to temporary sheet
            IRange range = workSheet.Range[groupBox];
            int rowCount = range.Rows.Count();
            IRange tmpRange = tmpSheet.Range[groupBox];
            range.CopyTo(tmpRange, ExcelCopyRangeOptions.All);

            // Replace value
            if (replaceValues != null && replaceValues.Count > 0)
            {
                // Find and replace values
                foreach (KeyValuePair<string, string> replacer in replaceValues)
                {
                    Replace(workSheet, replacer.Key, replacer.Value);
                }
            }

            // Loop data
            for (int i = groupData.Count - 1; i >= 0; i--)
            {
                IGrouping<int, T> group = groupData[i];
                List<T> listMember = group.ToList();

                // Create template maker
                ITemplateMarkersProcessor markProcess = workSheet.CreateTemplateMarkersProcessor();

                // Fill data into templates
                if (listMember.Count > 0)
                {
                    //markProcess.AddVariable(groupName,CacheData.GetTenChucDanh(group.Key));
                    //  Replace(workSheet, groupName, CacheData.GetTenChucDanh(group.Key));
                    markProcess.AddVariable(viewName, listMember);
                    markProcess.ApplyMarkers(UnknownVariableAction.ReplaceBlank);
                }
                else
                {
                    markProcess.AddVariable(groupName, string.Empty);
                    markProcess.ApplyMarkers(UnknownVariableAction.Skip);
                }

                // Insert template rows
                if (i > 0)
                {
                    workSheet.InsertRow(range.Row, rowCount);
                    tmpRange.CopyTo(workSheet.Range[groupBox], ExcelCopyRangeOptions.All);
                }
            }

            // Find row
            IRange[] rowSet = workSheet.FindAll(TMP_ROW, ExcelFindType.Text);

            //// Delete row
            for (int i = rowSet.Count() - 1; i >= 0; i--)
            {
                range = rowSet[i];

                // Delete
                if (range != null)
                {
                    workSheet.DeleteRow(range.Row);
                }
            }


            fileName = Path.GetTempFileName() + Constants.FILE_EXT_XLS;


            // Remove temporary sheet
            workBook.Worksheets.Remove(tmpSheet);

            // Output file
            if (!FileCommon.IsFileOpenOrReadOnly(fileName))
            {
                workBook.SaveAs(fileName);
                result = true;
            }

            // Close
            workBook.Close();
            engine.Dispose();

            // Print preview
            if (result && isPrintPreview)
            {
                PrintExcel(fileName);
                File.Delete(fileName);
            }

            return result;
        }


        private bool OutReport<T>(List<IGrouping<string, T>> groupData, Dictionary<string, string> replaceValues,
                                    string groupBox, string viewName, bool isPrintPreview, string fileName)
        {
            string file = string.Empty;
            bool result = false;

            // Get template stream
            MemoryStream stream = GetTemplateStream(viewName);

            // Check if data is null
            if (stream == null)
            {
                return false;
            }

            // Create excel engine
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workBook = engine.Excel.Workbooks.Open(stream);

            // Get sheets
            IWorksheet workSheet = workBook.Worksheets[0];
            IWorksheet tmpSheet = workBook.Worksheets.Create(TMP_SHEET);

            // Copy template of group to temporary sheet
            IRange range = workSheet.Range[groupBox];
            int rowCount = range.Rows.Count();
            IRange tmpRange = tmpSheet.Range[groupBox];
            range.CopyTo(tmpRange, ExcelCopyRangeOptions.All);

            // Replace value
            if (replaceValues != null && replaceValues.Count > 0)
            {
                // Find and replace values
                foreach (KeyValuePair<string, string> replacer in replaceValues)
                {
                    Replace(workSheet, replacer.Key, replacer.Value);
                }
            }

            // Loop data
            for (int i = groupData.Count - 1; i >= 0; i--)
            {
                IGrouping<string, T> group = groupData[i];
                List<T> listMember = group.ToList();

                // Create template maker
                ITemplateMarkersProcessor markProcess = workSheet.CreateTemplateMarkersProcessor();

                // Fill data into templates
                if (listMember.Count > 0)
                {
                    markProcess.AddVariable(viewName, listMember);
                    markProcess.ApplyMarkers();
                }
                else
                {
                    markProcess.ApplyMarkers(UnknownVariableAction.Skip);
                }

                // Insert template rows
                if (i > 0)
                {
                    workSheet.InsertRow(range.Row, rowCount);
                    tmpRange.CopyTo(workSheet.Range[groupBox], ExcelCopyRangeOptions.All);
                }
            }

            // Find row
            IRange[] rowSet = workSheet.FindAll(TMP_ROW, ExcelFindType.Text);

            // Delete row
            for (int i = rowSet.Count() - 1; i >= 0; i--)
            {
                range = rowSet[i];

                // Delete
                if (range != null)
                {
                    workSheet.DeleteRow(range.Row);
                }
            }

            // Get file name
            if (isPrintPreview)
            {
                file = Path.GetTempFileName() + Constants.FILE_EXT_XLS;
            }
            else
            {
                file = fileName;
            }

            // Remove temporary sheet
            workBook.Worksheets.Remove(tmpSheet);

            // Output file
            if (!FileCommon.IsFileOpenOrReadOnly(file))
            {
                workBook.SaveAs(file);
                result = true;
            }

            // Close
            workBook.Close();
            engine.Dispose();

            // Print preview
            if (result && isPrintPreview)
            {
                PrintExcel(file);
                File.Delete(file);
            }

            return result;
        }


        private MemoryStream GetTemplateStream(string viewName)
        {
            MemoryStream stream = null;
            byte[] arrByte = new byte[0];

            //Create Temp Folder if it does not exist
            if (!Directory.Exists(Global.AppPath + Constants.FOLDER_TEMPLATES))
            {
                Directory.CreateDirectory(Global.AppPath + Constants.FOLDER_TEMPLATES);
            }

            switch (viewName)
            {
                #region ---- Lấy file report----
                case "DoanhThu":
                    // path += Constants.FILE_KEHOACHTD;
                    arrByte = File.ReadAllBytes(Global.AppPath + "DoanhThu.xls").ToArray();
                    break;
                    #endregion
            }
            // Get stream
            if (arrByte.Length > 0)
            {
                stream = new MemoryStream(arrByte);
            }

            return stream;
        }

        private bool ReplaceDataWorkSheet(Dictionary<string, string> replaceValues, string viewName, bool isPrintPreview, ref string fileName)
        {
            string file = string.Empty;
            bool result = false;

            // Get template stream
            MemoryStream stream = GetTemplateStream(viewName);

            // Check if data is null
            if (stream == null)
            {
                return false;
            }

            // Create excel engine
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workBook = engine.Excel.Workbooks.Open(stream);
            IWorksheet workSheet = workBook.Worksheets[0];
            ITemplateMarkersProcessor markProcessor = workSheet.CreateTemplateMarkersProcessor();

            // Replace value
            if (replaceValues != null && replaceValues.Count > 0)
            {
                // Find and replace values
                foreach (KeyValuePair<string, string> replacer in replaceValues)
                {
                    Replace(workSheet, replacer.Key, replacer.Value);
                }
            }


            file = Path.GetTempFileName() + Constants.FILE_EXT_XLS;

            fileName = file;

            // Output file
            if (!FileCommon.IsFileOpenOrReadOnly(file))
            {
                workBook.SaveAs(file);
                result = true;
            }

            // Close
            workBook.Close();
            engine.Dispose();

            // Print preview
            if (result && isPrintPreview)
            {
                PrintExcel(file);
                File.Delete(file);
            }

            return result;
        }
        #endregion
        private bool OutSimpleReport<T>(List<T> dataSource, Dictionary<string, string> replaceValues, string viewName, bool isPrintPreview, ref string fileName)
        {
            string file = string.Empty;
            bool result = false;

            try
            {
                // Get template stream
                MemoryStream stream = GetTemplateStream(viewName);

                // Check if data is null
                if (stream == null)
                {
                    return false;
                }

                // Create excel engine
                ExcelEngine engine = new ExcelEngine();
                IWorkbook workBook = engine.Excel.Workbooks.Open(stream);

                // Get sheet for the report
                IWorksheet workSheet = workBook.Worksheets[0]; // Giả sử sheet ở vị trí 0

                // Create template markers processor
                ITemplateMarkersProcessor markProcessor = workSheet.CreateTemplateMarkersProcessor();

                // Replace values
                if (replaceValues != null && replaceValues.Count > 0)
                {
                    foreach (KeyValuePair<string, string> replacer in replaceValues)
                    {
                        Replace(workSheet, replacer.Key, replacer.Value);
                    }
                }

                // Fill variables with data source
                markProcessor.AddVariable(viewName, dataSource);
                markProcessor.ApplyMarkers();

                // Delete temporary row
                IRange range = workSheet.FindFirst(TMP_ROW, ExcelFindType.Text);
                if (range != null)
                {
                    workSheet.DeleteRow(range.Row);
                }

                // Generate temporary file name
                file = Path.GetTempFileName() + Constants.FILE_EXT_XLS;

                fileName = file;

                // Save the workbook
                if (!FileCommon.IsFileOpenOrReadOnly(file))
                {
                    workBook.SaveAs(file);
                    result = true;
                }

                // Close the workbook
                workBook.Close();
                engine.Dispose();
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"Lỗi khi xuất file Excel: {ex.Message}");
                result = false;
            }
            finally
            {
                // Print preview if requested
                if (result && isPrintPreview)
                {
                    PrintExcel(file);
                    File.Delete(file);
                }
            }

            return result;
        }


        public bool ExportDoanhThu(List<banhang> dataSourceBanHang, List<dathang> dataSourceDatHang, ref string fileName, bool isPrintPreview, DateTime month)
        {
            // Combine data from banhang and dathang
            List<DoanhThu> doanhThuList = new List<DoanhThu>();
           
            // Filter and add data for "Offline"
            var offlineData = dataSourceBanHang.Where(bh => bh.NgayBan?.Month == month.Month)
              .Select(bh => new DoanhThu
              {
                  STT1 = "", // Placeholder for STT
                  MaBanHang = bh.MaBanHang,
                  NgayBan = bh.NgayBan?.Date ?? DateTime.Now.Date,
                  TongTien = bh.TongTien,
                  Loai = "Offline",
                  MaDH = "xoa" // Không cần gán giá trị cho MaDH 
              }).ToList();

            // Filter and add data for "Online"
            var onlineData = dataSourceDatHang.Where(dh => dh.NgayDatHang?.Month == month.Month)
              .Select(dh => new DoanhThu
              {
                  STT1 = "", // Placeholder for STT
                  MaDH = dh.MaDH,
                  NgayDatHang = dh.NgayDatHang?.Date ?? DateTime.Now.Date,
                  ThanhTien = dh.ThanhTien,
                  Loai = "Online",
                  MaBanHang = "xoa" // Không cần gán giá trị cho MaBanHang
              }).ToList();

            // Combine the data
            doanhThuList.AddRange(offlineData);
            doanhThuList.AddRange(onlineData);

            // Update STT for Offline
            int offlineSTT = 1;
            foreach (var item in doanhThuList.Where(dt => dt.Loai == "Offline"))
            {
                item.STT1 = offlineSTT.ToString(); // Update STT for Offline
                offlineSTT++;
            }

            // Update STT for Online
            int onlineSTT = 1;
            foreach (var item in doanhThuList.Where(dt => dt.Loai == "Online"))
            {
                item.STT1 = onlineSTT.ToString(); // Update STT for Online
                onlineSTT++;
            }

            // Calculate total orders and total revenue for Offline and Online separately
            int tongHoaDonOffline = doanhThuList.Where(x => x.Loai == "Offline").Count();
            double tongTienOffline = doanhThuList.Where(x => x.Loai == "Offline").Sum(x => x.TongTien);
            int tongHoaDonOnline = doanhThuList.Where(x => x.Loai == "Online").Count();
            double tongTienOnline = doanhThuList.Where(x => x.Loai == "Online").Sum(x => x.ThanhTien);
            double tongTien = tongTienOffline + tongTienOnline;

            // Define a method to format currency
            string FormatCurrency(double amount)
            {
                return string.Format("{0:N0} VND", amount);
            }

            // Create replacer for Excel template
            Dictionary<string, string> replacer = new Dictionary<string, string>();
            replacer.Add("%NgayThangNam", "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year);
            replacer.Add("%TongSo", (tongHoaDonOffline + tongHoaDonOnline).ToString());
            replacer.Add("%TongHoaDon", tongHoaDonOffline.ToString()); // Tổng hóa đơn Offline
            replacer.Add("%TongDonHang", tongHoaDonOnline.ToString()); // Tổng hóa đơn Online
            replacer.Add("%TongTienOffline", FormatCurrency(tongTienOffline)); // Tổng doanh thu Offline
            replacer.Add("%TongTienOnline", FormatCurrency(tongTienOnline)); // Tổng doanh thu Online
            replacer.Add("%TongTien", FormatCurrency(tongTien)); // Tổng doanh thu

            // Export data to Excel using OutSimpleReport
            bool exportResult = OutSimpleReport(doanhThuList, replacer, "DoanhThu", isPrintPreview, ref fileName);

            // Xóa các dòng có giá trị là "xoa" trong file Excel
            if (exportResult)
            {
                DeleteRowsWithText(fileName, "xoa"); // Cột chỉ định (index bắt đầu từ 0)
            }

            return exportResult;
        }




        private void DeleteRowsWithText(string fileName, string textToDelete)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Open(fileName);
                IWorksheet worksheet = workbook.Worksheets[0];

                // Chỉ số cột thứ hai (cột B) - lưu ý rằng chỉ số cột bắt đầu từ 1
                int columnIndex = 2;

                // Duyệt từ dòng cuối cùng đến dòng đầu tiên để tránh thay đổi chỉ số dòng khi xóa
                for (int i = worksheet.UsedRange.LastRow; i >= 1; i--)
                {
                    IRange cell = worksheet.Range[i, columnIndex];
                    if (cell.Text == textToDelete)
                    {
                        worksheet.DeleteRow(i);
                    }
                }

                workbook.Save();
                workbook.Close();
            }
        }





        #endregion
    }
}
