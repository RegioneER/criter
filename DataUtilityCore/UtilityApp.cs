using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Web;
using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;
using DataLayer;
using System.Collections.Generic;
using DevExpress.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.ComponentModel;

namespace DataUtilityCore
{
    public class UtilityApp
    {
        public static void DisableAllControls(Control Page)
        {
            foreach (Control ctrl in Page.Controls)
            {
                if (ctrl is TextBox) ((TextBox)(ctrl)).Enabled = false;
                //else if (ctrl is Button) ((Button)(ctrl)).Enabled = false;
                else if (ctrl is ASPxComboBox) ((ASPxComboBox)(ctrl)).Enabled = false;
                else if (ctrl is DropDownList) ((DropDownList)(ctrl)).Enabled = false;
                else if (ctrl is ListBox) ((ListBox)(ctrl)).Enabled = false;
                else if (ctrl is CheckBox) ((CheckBox)(ctrl)).Enabled = false;
                else if (ctrl is CheckBoxList) ((CheckBoxList)(ctrl)).Enabled = false;
                else if (ctrl is RadioButton) ((RadioButton)(ctrl)).Enabled = false;
                else if (ctrl is RadioButtonList) ((RadioButtonList)(ctrl)).Enabled = false;
                else if (ctrl is LinkButton) ((LinkButton)(ctrl)).Enabled = false;
                else
                {
                    if (ctrl.Controls.Count > 0) DisableAllControls(ctrl);
                }
            }
        }
        public static void SiDisableAllControls(Control control)
        {
            if (control is Wizard)
            {
                var wizard = control as Wizard;
                //torno su a riabilitare tutti i parent
                SiEnableParents(wizard);
                
                foreach (Control child in wizard.WizardSteps)
                {
                    foreach (Control child1 in child.Controls)
                    {
                        SiDisableAllControls(child1);

                    }
                }
            }
            else
            {
                if (control is WebControl)
                    ((WebControl)(control)).Enabled = false;
                if (control is LinkButton)
                {
                    //evito i vari messaggi di conferma
                    (control as LinkButton).OnClientClick = "return false;";
                }

                if (control.Controls.Count > 0)
                {

                    foreach (Control child in control.Controls)
                    {
                        SiDisableAllControls(child);

                    }
                }
            }
        }

        public static void SiEnableParents(Control item)
        {
            if (item is WebControl)
            {
                ((WebControl)item).Enabled = true;
                if (item.Parent != null)
                {
                    SiEnableParents(item.Parent);
                }
            }
        }

        public static string BooleanFlagToImage(bool valore)
        {
            return (valore) ? "~/images/Buttons/switch-on-icon.png" : "~/images/Buttons/switch-off-icon.png";
        }

        public static string BooleanFlagToString(bool valore)
        {
            return (valore) ? "Si" : "No";
        }

        public static Control FindControlRecursive(Control control, string id)
        {
            if (control == null)
                return null;

            //try to find the control at the current level
            Control ctrl = control.FindControl(id);

            if (ctrl == null)
            {
                //search the children
                foreach (Control child in control.Controls)
                {
                    ctrl = FindControlRecursive(child, id);

                    if (ctrl != null)
                        break;
                }
            }
            return ctrl;
        }

        public static string GetCurrentPageName()
        {
            string sPath = HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = HttpContext.Current.Request.Url.Query;
            FileInfo oInfo = new FileInfo(sPath);
            string sRet = oInfo.Name + sQueryString;
            return sRet;
        }

        public static bool TransaleTrueFalse(bool value)
        {
            bool fbool = false;

            if (value)
            {
                fbool = false;
            }
            else
            {
                fbool = true;
            }

            return fbool;
        }

        public static bool ToBoolean(string value)
        {
            bool fbool = false;
            switch (value.ToLower())
            {
                case "si":
                    fbool = true;
                    break;
                case "no":
                    fbool = false;
                    break;
                case "1":
                    fbool = true;
                    break;
                case "0":
                    fbool = false;
                    break;
                case "false":
                    fbool = false;
                    break;
                case "true":
                    fbool = true;
                    break;
            }

            return fbool;
        }

        public static string ToImage(bool value)
        {
            string Image = "";
            if (value)
            {
                Image = "~/images/si.png";
            }
            else
            {
                Image = "~/images/no.png";
            }

            return Image;
        }

        public static string ToImageSpid(bool value)
        {
            string Image = "";
            if (value)
            {
                Image = "~/images/spid-logo-c-lb.png";
            }
            else
            {
                Image = "~/images/spid-level3-access-icon-a-lb.png";
            }

            return Image;
        }

        public static SqlDataReader GetDR(string sqlText)
        {
            SqlDataReader dr;
            SqlConnection conn = BuildConnection.GetConn();
            SqlCommand sqlCmd = new SqlCommand(sqlText, conn);
            sqlCmd.Connection.Open();
            dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public static SqlDataReader GetDRSP(string sp_name, params SqlParameter[] arrParam)
        {
            SqlDataReader dr;
            SqlConnection conn = BuildConnection.GetConn();
            SqlCommand cmd = new SqlCommand(sp_name, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@IDValutazionePompa", Parameter);
            if (arrParam != null)
            {
                foreach (SqlParameter param in arrParam)
                    cmd.Parameters.Add(param);
            }
            
            cmd.Connection.Open();
            dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dr;
        }

        protected DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            // Add columns by looping rows

            // Header row's first column is same as in inputTable
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

            // Header row's second column onwards, 'inputTable's first column taken
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }

            // Add rows by looping columns        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();

                // First column is inputTable's Header row's second column
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }

            return outputTable;
        }

        public static byte[] GetImageBytes(Stream stream)
        {
            byte[] buffer;

            using (Bitmap image = ResizeImage(stream))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Png);

                    //return the current position in the stream at the beginning
                    ms.Position = 0;

                    buffer = new byte[ms.Length];
                    ms.Read(buffer, 0, (int)ms.Length);
                    return buffer;
                }
            }
        }

        public static Bitmap ResizeImage(Stream stream)
        {
            System.Drawing.Image originalImage = Bitmap.FromStream(stream);

            int height = 250;
            int width = 250;

            double ratio = Math.Min(originalImage.Width, originalImage.Height) / (double)Math.Max(originalImage.Width, originalImage.Height);

            if (originalImage.Width > originalImage.Height)
            {
                height = Convert.ToInt32(height * ratio);
            }
            else
            {
                width = Convert.ToInt32(width * ratio);
            }

            Bitmap scaledImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, width, height);

                return scaledImage;
            }
        }

        public static string GetPrimaLetteraMaiuscola(string stringa)
        {
            string primalettera = "";
            string ilresto = "";
            if ((stringa != "") && (stringa != " "))
            {
                stringa = stringa.Trim();
                primalettera = (stringa.Substring(0, 1)).ToUpper();
                ilresto = (stringa.Substring(1, (stringa.Length - 1)));
                stringa = primalettera + ilresto;
            }
            return stringa;
        }

        public static string removeDoubleBackslashes(string input)
        {
            char[] separator = new char[1] { '\\' };
            string result = "";
            string[] subResult = input.Split(separator);
            for (int i = 0; i <= subResult.Length - 1; i++)
            {
                result = i < subResult.Length - 1 ? result + subResult[i] + "\\" : result + subResult[i];
            }
            return result;   
        }
        
        public static int? ParseNullableInt(string value)
        {
            int intValue;
            if (int.TryParse(value, out intValue))
                return intValue;
            return null;
        }
                
        public static DateTime? ParseNullableDatetime(string value)
        {
            DateTime datetimeValue;
            if (DateTime.TryParse(value, out datetimeValue))
                return datetimeValue;
            return null;
        }

        public static DateTime AddWorkingDays(DateTime startDate, int workingDays)
        {
            DateTime currentDate = startDate;
            int addedDays = 0;

            while (addedDays < workingDays)
            {
                currentDate = currentDate.AddDays(1);

                // Salta i giorni del weekend (sabato e domenica)
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    addedDays++;
                }
            }

            return currentDate;
        }

        public static decimal? ParseNullableDecimal(string value)
        {
            decimal intValue;
            if (decimal.TryParse(value, out intValue))
                return intValue;
            return null;
        }

        public static bool AreNumbersTheSame(decimal[] numbers, int duplicateCount, decimal numberCompare)
        {
            var numberCounts = 0;
            bool fOccurence = false;

            for (int i = 0; i < numbers.Length; i++)
            {
                var current = numbers[i];
                if (current <= numberCompare)
                {
                    numberCounts++;
                }
            }
            if (numberCounts >= duplicateCount)
            {
                fOccurence = true;
            }

            return fOccurence;
        }
        
        public static byte[] EncodeToBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static DateTime? CheckValidDatetimeWithMinValue(string datetimeValue)
        {
            DateTime dateValue;
            DateTime dataMinima = new DateTime(1950, 01, 01);

            DateTime? datetimeResult = null;

            var validDate = DateTime.TryParse(datetimeValue, out dateValue);
            if (validDate)
            {
                int res = DateTime.Compare(dateValue, dataMinima);
                if (res > 0)
                {
                    //OK
                    datetimeResult = dateValue;
                }
            }
            
            return datetimeResult;
        }

        #region PAGINATION AND SORTING DATAGRID/GRIDVIEW

        public static string CheckSortExpression(string newValue, object oldValue)
        {
            if (oldValue == null) return newValue + " ASC";
            if (oldValue.ToString() == String.Empty) return newValue + " ASC";

            string oldNomeCampo = oldValue.ToString().Replace(" DESC", "").Replace(" ASC", "").TrimEnd(' ', ';').TrimStart(' ').ToLower();
            string oldOrder;
            if (oldValue.ToString().IndexOf(" DESC") > 2)
                oldOrder = "DESC";
            else
                oldOrder = "ASC";

            string newNomeCampo = newValue.Replace(" DESC", "").Replace(" ASC", "").TrimEnd(' ', ';').TrimStart(' ').ToLower();
            string newOrder;
            if (newValue.IndexOf(" DESC") > 2)
                newOrder = "DESC";
            else
                newOrder = "ASC";

            if (oldNomeCampo.Equals(newNomeCampo))
            {
                if (oldOrder == "DESC")
                    return newNomeCampo + " ASC";
                else
                    return newNomeCampo + " DESC";
            }
            else
                return newValue;
        }

        public static int BindControl(dynamic dataControl, string sql, string SortExpression)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, BuildConnection.GetConn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (SortExpression.Length > 0)
                dt.DefaultView.Sort = SortExpression;

            DataView Source = dt.DefaultView;

            int totRecords = dt.Rows.Count;

            dataControl.DataSource = Source;
            dataControl.DataBind();

            return totRecords;
        }

        public static int BindControl(bool reload, DataGrid datagrid, string sql, string SortExpression, int currentPage, int pageSize)
        {
            // determinazione dell'ordinamento iniziale
            string orderFields = GetOrderFields(datagrid, ref sql, SortExpression);

            // è il primo caricamento: quindi leggo i records totali
            if (reload)
            {
                int count = GetVic(sql);
                datagrid.AllowCustomPaging = true;
                datagrid.VirtualItemCount = count;
            }

            datagrid.CurrentPageIndex = currentPage;

            SqlCommand cmd = BuildCommand(sql, orderFields, currentPage, pageSize);
            cmd.CommandTimeout = int.Parse(ConfigurationManager.AppSettings["CommandTimeOutTime"]);

            cmd.Connection.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
            {
                datagrid.DataSource = dr;
                datagrid.DataBind();
            }

            return datagrid.VirtualItemCount;

        }

        public static int BindControl(bool reload, StateBag ViewState, GridView grid, string sql, string SortExpression, int currentPage, int pageSize)
        {
            // determinazione dell'ordinamento iniziale
            string orderFields = GetOrderFields(grid, ref sql, SortExpression);

            // determinazione del VirtualItemCount (vic) solo la prima volta
            int vic;
            string key = "vic_" + grid.ID;

            if (reload)
            {
                vic = GetVic(sql);
                ViewState[key] = vic;
            }
            else
            {
                vic = (int)ViewState[key];
            }

            grid.PageIndex = currentPage;

            // Creo un ODS da codice (la GridView vuole così) --------------------------------------------
            ObjectDataSource ods = new ObjectDataSource();

            //setting the necessary properties (these will interact with our TableAdapter !)
            ods.ID = "ods" + grid.ID;
            ods.EnablePaging = grid.AllowPaging; //the paging of ODS depends on the paging of the GridView
            ods.TypeName = "MyTableAdapter"; //be sure to prefix the namespace of your application !!! e.g. MyApp.MyTableAdapter
            ods.SelectMethod = "GetData";
            ods.SelectCountMethod = "VirtualItemCount";
            ods.StartRowIndexParameterName = "startRow";
            ods.MaximumRowsParameterName = "maxRows";
            ods.EnableViewState = false;
            grid.DataSource = ods;

            SqlCommand cmd = BuildCommand(sql, orderFields, currentPage, pageSize);

            cmd.Connection.Open();
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection))
            {
                ods.ObjectCreating += delegate(object sender, ObjectDataSourceEventArgs e)
                {
                    e.ObjectInstance = new MyTableAdapter(dr);
                };
                grid.DataBind();
            }

            return vic;
        }

        private static int GetVic(string sql)
        {
            int count;
            // visto che è la prima volta imposto il VirtualItemCount nella grid
            string sqlCount = String.Format("WITH a AS({0}) SELECT COUNT(*) FROM a;", sql);
            SqlCommand cmdCount = new SqlCommand(sqlCount, BuildConnection.GetConn());
            cmdCount.CommandTimeout = int.Parse(ConfigurationManager.AppSettings["CommandTimeOutTime"]);
            cmdCount.Connection.Open();
            count = (int)cmdCount.ExecuteScalar();
            cmdCount.Connection.Close();
            return count;
        }

        private static string GetOrderFields(WebControl ctrl, ref string sql, string SortExpression)
        {
            string originalSql = sql;
            string orderFields;
            // ordinamento preso dalla query iniziale
            int iniOrderBy = sql.IndexOf("ORDER BY");
            if (iniOrderBy >= 0)
            {
                sql = sql.Substring(0, iniOrderBy).TrimEnd(' ', ';').TrimEnd();

                if (SortExpression.Length > 0)
                {
                    // non è la prima volta: prendo l'ordinamento selezionato dall'utente
                    orderFields = SortExpression;
                    //// verifico l'esistenza di un'ordinamento precedente ed eventualmente lo giro
                    //string oldSort = ctrl.Attributes["ASort"];
                    //if (oldSort != null && oldSort.Length > 0)
                    //{
                    //   orderFields = CheckSortExpression(orderFields, oldSort);
                    //   ctrl.Attributes["ASort"] = orderFields;
                    //}
                    //else 
                    //{
                    //   ctrl.Attributes.Add("ASort", orderFields);
                    //}

                }
                else
                {
                    // è la prima volta: prendo l'ordinamento originale della query
                    orderFields = originalSql.Substring(iniOrderBy + 8).TrimStart().TrimEnd(' ', ';').TrimEnd();
                    SortExpression = orderFields;
                }
            }
            else
            {
                throw new ApplicationException("La query non ha un ordinamento predefinito");
            }
            return orderFields;
        }

        private static SqlCommand BuildCommand(string sql, string orderFields, int currentPage, int pageSize)
        {
            string sqlPage = String.Format(" WITH a AS ({0}),  b AS ( SELECT ROW_NUMBER() OVER (ORDER BY {1}) AS Row, * FROM a ) " +
            "SELECT * FROM b WHERE Row between (@PageIndex - 1) * @PageSize + 1 AND @PageIndex*@PageSize ORDER BY {1};", sql, orderFields);

            SqlCommand cmd = new SqlCommand(sqlPage, BuildConnection.GetConn());

            cmd.Parameters.AddWithValue("@PageIndex", currentPage + 1);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            return cmd;
        }

        public static void SetVisuals(DataGrid grid)
        {
            SetVisuals(grid, -1, null, null);
        }
        public static void SetVisuals(DataGrid datagrid, int totRecords, Label lblCount, string itemsName)
        {
            if (lblCount != null) lblCount.Visible = true;

            if (datagrid.Items.Count == 0)
            {
                //se non esistono record, faccio sparire l'1 che viene fuori
                datagrid.PagerStyle.Visible = false;
                datagrid.Visible = false;
                if ((lblCount != null) || (lblCount.Text!=""))
                {
                    lblCount.Text = String.Format("{0}&nbsp;{1}", totRecords, itemsName);
                    lblCount.CssClass = "GridLabelNoCount";
                }
            }
            else
            {
                lblCount.Text = String.Format("{0}&nbsp;{1}", totRecords, itemsName);
                datagrid.Visible = true;
                lblCount.CssClass = "GridLabelCount";
                if (totRecords < datagrid.PageSize)
                {
                    //se i record sono meno di PageSize, non mostro l'1
                    datagrid.PagerStyle.Visible = false;
                }
                else
                {
                    datagrid.PagerStyle.Visible = true;
                }
            }
        }

        public static void SetVisuals(GridView grid)
        {
            SetVisuals(grid, -1, null, null);
        }
        public static void SetVisuals(GridView datagrid, int totRecords, Label lblCount, string itemsName)
        {
            if (lblCount != null) lblCount.Visible = true;

            if (datagrid.Rows.Count == 0)
            {
                //se non esistono record, faccio sparire l'1 che viene fuori
                datagrid.PagerSettings.Visible = false;
                datagrid.Visible = false;
                if (lblCount != null)
                {
                    lblCount.Text = String.Format("NON CI SONO {0} CORRISPONDENTI AI PARAMETRI IMPOSTATI", itemsName);
                    lblCount.CssClass = "GridLabelNoCount";
                }
            }
            else
            {
                lblCount.Text = String.Format("{0}&nbsp;&nbsp;{1} CORRISPONDENTI AI PARAMETRI IMPOSTATI", totRecords, itemsName);
                datagrid.Visible = true;
                lblCount.CssClass = "GridLabelCount";
                if (totRecords < datagrid.PageSize)
                {
                    //se i record sono meno di PageSize, non mostro l'1
                    datagrid.PagerSettings.Visible = false;
                }
                else
                {
                    datagrid.PagerSettings.Visible = true;
                }
            }

        }

        #endregion

        #region FORMATTING DATAGRID VALUE
        public static string ToCurrency(object value)
        {
            if (value == null || value == DBNull.Value) return null;

            decimal dec;
            if (decimal.TryParse(value.ToString(), System.Globalization.NumberStyles.Currency, new System.Globalization.CultureInfo("en-US"), out dec))
            {
                return String.Format("{0:c}", dec, new System.Globalization.CultureInfo("it-IT"));
            }
            else
                return value.ToString();
        }

        public static string FormatAnnoMese(object value)
        {
            string annoMesef = String.Empty;

            if (null == value || DBNull.Value == value) return annoMesef;

            string anno = value.ToString().Substring(0, 4);
            int mese = int.Parse(value.ToString().Replace(anno, ""));
            annoMesef = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(mese) + " " + anno;
            return annoMesef;
        }

        public static string ToDate(object value)
        {
            if (value == null || value == DBNull.Value) return null;

            string sdataPri = value.ToString();
            string annoPri = sdataPri.Substring(0, 4);
            string mesePri = sdataPri.Substring(4, 2);
            string giornoPri = sdataPri.Substring(6, 2);
            value = String.Format("{0}/{1}/{2}", giornoPri, mesePri, annoPri);

            return value.ToString();
        }

        #endregion

        #region ATTRIBUZIONE IMMAGINE O MESSAGGI

        public static string ValueSiNo(string value)
        {
            string result = "";
            switch (value.ToLower())
            {
                case "-1":
                    result = "Non classificabile";
                    break;
                case "1":
                    result = "Si";
                    break;
                case "0":
                    result = "No";
                    break;
            }

            return result;
        }

        public static string ToSiNo(string value)
        {
            string Valore = "";
            if (value.ToString() == "") return String.Empty;
            if ((value.ToString() == "True"))
            {
                Valore = "Si";
            }
            else
            {
                Valore = "No";
            }

            return Valore;
        }

        public static string ToHandImage(string value)
        {
            string Image = "";
            if (value == "") return String.Empty;
            if ((value == "Si") || (value == "True"))
            {
                Image = "<img src='images/buttons/HandUp.png' align='middle' alt='Si' />";
            }
            else
            {
                Image = "<img src='images/buttons/HandDown.png' align='middle' alt='No' />";
            }

            return Image;
        }

        #endregion

        public static string EscapeLikeValue(string valueWithoutWildcards)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                    sb.Append("[").Append(c).Append("]");
                else if (c == '\'')
                    sb.Append("''");
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        //#region CHECK C.FISCALE PERSONA

        //public static bool CheckCodiceFiscale(string CodiceFiscale)
        //{
        //    bool result = false;
        //    const int caratteri = 16;

        //    if (CodiceFiscale == null)
        //        return result;

        //    if (CodiceFiscale.Length < caratteri)
        //        return result;
        //    string codicefiscale = CodiceFiscale.ToUpper();
        //    const string listaPosizione = "A0B1C2D3E4F5G6H7I8J9KLMNOPQRSTUVWXYZ";
        //    char[] listaControllo = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        //    int[] listaPari = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
        //    int[] listaDispari = { 1, 1, 0, 0, 5, 5, 7, 7, 9, 9, 13, 13, 15, 15, 17, 17, 19, 19, 21, 21, 2, 4, 18, 20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23 };

        //    // check della correttezza formale del codice fiscale
        //    Regex regex = new Regex(@"^[A-Z]{6}[\d]{2}[A-Z][\d]{2}[A-Z][\d]{3}[A-Z]$");
        //    Match m = regex.Match(codicefiscale);

        //    result = m.Success;

        //    if (result)
        //    {
        //        int somma = 0;
        //        for (int i = 0; i < 15; i++)
        //        {
        //            char[] c = codicefiscale.Substring(i, 1).ToCharArray();
        //            int j = listaPosizione.IndexOf(c[0]);
        //            if (j < 0)
        //            {
        //                result = false;
        //                break;
        //            }
        //            if (((i + 1) % 2) == 0)
        //                somma = somma + listaPari[j];
        //            else
        //                somma = somma + listaDispari[j];
        //        }
        //        if (result) result = ((listaControllo[somma % 26]).ToString() == codicefiscale.Substring(15, 1));
        //    }
        //    return result;
        //}

        //public static bool CheckCodiceFiscaleDaProvare(string CodiceFiscale)
        //{
        //    bool result = false;
        //    const int caratteri = 16;

        //    if (CodiceFiscale == null)
        //        return result;

        //    if (CodiceFiscale.Length < caratteri)
        //        return result;

        //    //La RegEx completa, che verifica anche la validità della data di nascita, la conformità di nome e cognome, la pertinenza del codice del comune o dello stato di nascita, tenendo in considerazione anche delle combinazioni alternative in caso di omocodia
        //    string codicefiscale = CodiceFiscale.ToUpper();
        //    Regex regex = new Regex(@"/^(?:[B-DF-HJ-NP-TV-Z](?:[AEIOU]{2}|[AEIOU]X)|[AEIOU]{2}X|[B-DF-HJ-NP-TV-Z]{2}[A-Z]){2}[\dLMNP-V]{2}(?:[A-EHLMPR-T](?:[04LQ][1-9MNP-V]|[1256LMRS][\dLMNP-V])|[DHPS][37PT][0L]|[ACELMRT][37PT][01LM])(?:[A-MZ][1-9MNP-V][\dLMNP-V]{2}|[A-M][0L](?:[\dLMNP-V][1-9MNP-V]|[1-9MNP-V][0L]))[A-Z]$/i");
        //    Match m = regex.Match(codicefiscale);
        //    result = m.Success;
                        
        //    return result;
        //}

        //#endregion

        #region CHECK P.IVA

        private static int[] ListaPari = { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };

        public static bool CheckPartitaIva(string PartitaIva)
        {

            // normalizziamo la cifra

            if (PartitaIva.Length < 11)

                PartitaIva = PartitaIva.PadLeft(11, '0');

            // lunghezza errata non fa neanche il controllo

            if (PartitaIva.Length != 11)

                return false;

            int Somma = 0;

            for (int k = 0; k < 11; k++)
            {

                string s = PartitaIva.Substring(k, 1);

                // otteniamo contemporaneamente

                // il valore, la posizione e testiamo se ci sono

                // caratteri non numerici

                int i = "0123456789".IndexOf(s);

                if (i == -1)

                    return false;

                int x = int.Parse(s);

                if (k % 2 == 1) // Pari perchè iniziamo da zero

                    x = ListaPari[i];

                Somma += x;

            }

            return ((Somma % 10 == 0) && (Somma != 0));

        }

        #endregion

        public static object[] GetGeocodingAddress(string indirizzo, string cap, string citta, string paese)
        {
            object[] outVal = new object[2];
            outVal[0] = null;  //Latitudine
            outVal[1] = null;  //Longitudine

            string indirizzoCompleto = String.Format("{0},{1},{2}", indirizzo + ", ", cap + " " + citta + ", ", paese);

            var infoAddress = Google.GoogleMaps.GetGeocodingAddress(indirizzoCompleto);

            if (infoAddress != null)
            {
                if (infoAddress.Status == "OK")
                {
                    outVal[0] = infoAddress.Latitude;
                    outVal[1] = infoAddress.Longitude;
                }
            }

            return outVal;
        }

        public static string GetUserIP()
        {
            string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string SanitizeInput(string input, SanitizeType type)
        {
            string pattern = "";
            switch (type)
            {
                case (SanitizeType.FieldOnly): pattern = @"([<>=+""'%;()&])"; break;
                case (SanitizeType.SelectList): pattern = @"(<>=+;&)"; break;
                case (SanitizeType.WhereClause): pattern = @"("";)"; break;
                case (SanitizeType.Select): pattern = @"[alter][drop][create][insert][update][delete];"; break;
            }

            if (input == null || input.Length == 0) return "";
            input = input.Trim();
            Regex badCharReplace = new Regex(pattern);
            string goodChars = badCharReplace.Replace(input, "");
            return goodChars;
        }

        public static DateTime? GetDataFromCodiceFiscale(string codiceFiscale)
        {
            try
            {
                Dictionary<string, string> month = new Dictionary<string, string>();
                // To Upper
                codiceFiscale = codiceFiscale.ToUpper();
                month.Add("A", "01");
                month.Add("B", "02");
                month.Add("C", "03");
                month.Add("D", "04");
                month.Add("E", "05");
                month.Add("H", "06");
                month.Add("L", "07");
                month.Add("M", "08");
                month.Add("P", "09");
                month.Add("R", "10");
                month.Add("S", "11");
                month.Add("T", "12");
                // Get Date
                string date = codiceFiscale.Substring(6, 5);
                int y = int.Parse(date.Substring(0, 2));
                string yy = ((y < 9) ? "20" : "19") + y.ToString("00");
                string m = month[date.Substring(2, 1)];
                int d = int.Parse(date.Substring(3, 2));
                if (d > 31)
                    d -= 40;
                string data = string.Format("{0}/{1}/{2}", d.ToString("00"), m, yy);
                return DateTime.Parse(data);
            }
            catch
            {
                return null;
            }
        }

        public static int? GetIDProvinciaFromSigla(string siglaProvincia)
        {
            int? iDProvincia = null;
            if (!string.IsNullOrEmpty(siglaProvincia))
            {
                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var province = ctx.SYS_Province.Where(r => r.SiglaProvincia == siglaProvincia).FirstOrDefault();
                    if (province != null)
                    {
                        iDProvincia = province.IDProvincia;
                    }
                }
            }
            
            return iDProvincia;
        }

        public static string GetComuneFromCodiceCatastale(string codiceCatastale)
        {
            string comune = string.Empty;
            if (!string.IsNullOrEmpty(codiceCatastale))
            {
                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var codiciCatastali = ctx.SYS_CodiciCatastali.Where(r => r.CodiceCatastale == codiceCatastale & r.fAttivo == true).FirstOrDefault();
                    if (codiciCatastali != null)
                    {
                        comune = codiciCatastali.Comune;
                    }
                }
            }

            return comune;
        }
        
        public static void SetLogApplicationError()
        {
            string mess = string.Empty;
            Exception myError = HttpContext.Current.Server.GetLastError();
            mess += "Messaggio Errore:" + myError.Message;
            mess += "- Sorgente Errore:" + myError.Source;
            mess += "- Errore InnerException:" + myError.InnerException.ToString();
            mess += "- Errore Stack Trace:" + myError.StackTrace;
            mess += "- Errore TargetSite:" + myError.TargetSite.ToString();
            mess += "- Errore Data:" + myError.Data.ToString();
            mess += "- Errore HelpLink:" + myError.HelpLink;
            
            Logger.LogNote(TipoEvento.ErroreApplicativo, TipoOggetto.Pagina, mess);
        }

        public static string GetEnumDescription(int typeofRaccomandataInt)
        {
            Enum.EnumTypeofRaccomandata typeofRaccomandata = Enum.EnumTypeofRaccomandata.TypePianificazioneIspezioneConferma;
            if (typeofRaccomandataInt == 3)
            {
                typeofRaccomandata = Enum.EnumTypeofRaccomandata.TypePianificazioneIspezioneConferma;
            }
            else if (typeofRaccomandataInt == 4)
            {
                typeofRaccomandata = Enum.EnumTypeofRaccomandata.TypePianificazioneIspezioneAnnulla;
            }
            else if (typeofRaccomandataInt == 5)
            {
                typeofRaccomandata = Enum.EnumTypeofRaccomandata.TypePianificazioneIspezioneRipianificazione;
            }
            
            FieldInfo fi = typeofRaccomandata.GetType().GetField(typeofRaccomandata.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return typeofRaccomandata.ToString();
            }
        }

        public static void BulkInsert<T>(string connection, string tableName, IList<T> list)
        {
            using (var bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = tableName;

                var table = new DataTable();
                var props = TypeDescriptor.GetProperties(typeof(T))
                                           //Dirty hack to make sure we only have system data types 
                                           //i.e. filter out the relationships/collections
                                           .Cast<PropertyDescriptor>()
                                           .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                                           .ToArray();

                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }

                var values = new object[props.Length];
                foreach (var item in list)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }

                bulkCopy.WriteToServer(table);
            }
        }

        public static string GetFileNameFromUrl(string url)
        {
            Uri uri = new Uri(url);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);

            return filename;
        }

        public static string GetMinutesToHours(int minutes)
        {
            var time = TimeSpan.FromMinutes(minutes);
            return time.TotalHours.ToString();
        }


    }

    /// <summary>
    /// Questa classe statica che consente di effettuare le seguenti operazioni:
    /// 
    /// - Calcolare il Codice Fiscale di un cittadino italiano (al netto di possibili generalità false e/o casi di omocodia - vedi sotto).
    /// - Effettuare il Controllo Formale di un Codice Fiscale (inclusi quelli assegnati per omocodia).
    /// - Effettuare il Controllo Formale di un Codice Fiscale e la corrispondenza rispetto ai dati anagrafici indicati (inclusi quelli assegnati per omocodia).
    /// 
    /// IMPORTANTE: Questa classe non effettua alcuna connessione ai database dell'Anagrafe tributaria. Di conseguenza:
    /// 
    /// - i Codici Fiscali generati potrebbero non corrispondere effettivamente a quelli reali.
    /// - il Controllo formale effettuato non garantisce che il Codice Fiscale sia relativo a una persona realmente esistente o esistita.
    /// - il Controllo di corrispondenza effettuato non garantisce che il Codice Fiscale sia effettivamente quello della persona indicata.
    /// 
    /// Si ricorda che l'unico modo per avere questo tipo di garanzie è utilizzare gli strumenti di VERIFICA 
    /// forniti dall'Agenzia delle Entrate e/o dall'Anagrafe tributaria, come ad esempio:
    /// 
    /// - Verifica del Codice Fiscale:
    /// https://telematici.agenziaentrate.gov.it/VerificaCF/Scegli.do?parameter=verificaCf
    /// 
    /// - Verifica e corrispondenza del Codice Fiscale con i dati anagrafici di una persona fisica:
    /// https://telematici.agenziaentrate.gov.it/VerificaCF/Scegli.do?parameter=verificaCfPf
    /// 
    /// Per ulteriori informazioni sui casi di Omocodia, si consiglia inoltre di leggere il testo seguente:
    /// http://www.agenziaentrate.gov.it/wps/content/Nsilib/Nsi/Home/CosaDeviFare/Richiedere/Codice+fiscale+e+tessera+sanitaria/Richiesta+TS_CF/SchedaI/FAQ+sul+Codice+Fiscale/
    /// </summary>
    public static class CodiceFiscale
    {
        #region Private Members
        private static readonly string Months = "ABCDEHLMPRST";
        private static readonly string Vocals = "AEIOU";
        private static readonly string Consonants = "BCDFGHJKLMNPQRSTVWXYZ";
        private static readonly string OmocodeChars = "LMNPQRSTUV";
        private static readonly int[] ControlCodeArray = new[] { 1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 2, 4, 18, 20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23 };
        private static readonly Regex CheckRegex = new Regex(@"^[A-Z]{6}[\d]{2}[A-Z][\d]{2}[A-Z][\d]{3}[A-Z]$");
        #endregion Private Members

        #region Public Methods
        /// <summary>
        /// Costruisce un codice fiscale "formalmente corretto" sulla base dei parametri indicati.
        /// 
        /// - Il codice ISTAT, relativo al comune di nascita, può essere recuperato da questo elenco:
        ///   http://www.agenziaentrate.gov.it/wps/content/Nsilib/Nsi/Strumenti/Codici+attivita+e+tributo/Codici+territorio/Comuni+italia+esteri/
        ///   
        /// IMPORTANTE: Si ricorda che il Codice Fiscale generato potrebbe non corrispondere effettivamente a quello reale.
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <param name="cognome">Cognome</param>
        /// <param name="dataDiNascita">Data di nascita</param>
        /// <param name="genere">Genere ('M' o 'F')</param>
        /// <param name="codiceISTAT">Codice ISTAT (1 lettera e 3 numeri. Es.: H501 per Roma)</param>
        /// <returns>Un Codice Fiscale "formalmente corretto", calcolato sulla base dei parametri indicati.</returns>
        public static string CalcolaCodiceFiscale(string nome, string cognome, DateTime dataDiNascita, char genere, string codiceISTAT)
        {
            if (String.IsNullOrEmpty(nome)) throw new NotSupportedException("ERRORE: Il parametro 'nome' è obbligatorio.");
            if (String.IsNullOrEmpty(cognome)) throw new NotSupportedException("ERRORE: Il parametro 'cognome' è obbligatorio.");
            if (genere != 'M' && genere != 'F') throw new NotSupportedException("ERRORE: Il parametro 'genere' deve essere 'M' oppure 'F'.");
            if (String.IsNullOrEmpty(codiceISTAT)) throw new NotSupportedException("ERRORE: Il parametro 'codiceISTAT' è obbligatorio.");

            string cf = String.Format("{0}{1}{2}{3}",
                                         CalcolaCodiceCognome(cognome),
                                         CalcolaCodiceNome(nome),
                                         CalcolaCodiceDataDiNascitaGenere(dataDiNascita, genere),
                                         codiceISTAT
                                        );
            cf += CalcolaCarattereDiControllo(cf);
            return cf;
        }

        /// <summary>
        /// Effettua un "controllo formale" del Codice Fiscale indicato secondo i seguenti criteri:
        /// 
        /// - Controlla che non sia un valore nullo/vuoto.
        /// - Controlla che il codice sia coerente con le specifiche normative per i Codici Fiscali (inclusi possibili casi di omocodia).
        /// - Controlla che il carattere di controllo sia coerente rispetto al Codice Fiscale indicato.
        /// 
        /// IMPORTANTE: Si ricorda che, anche se il Codice Fiscale risulta "formalmente corretto", 
        /// non ci sono garanzie che si tratti di un Codice Fiscale relativo a una persona realmente esistente o esistita.
        /// </summary>
        /// <param name="cf">il codice fiscale da controllare</param>
        /// <returns>TRUE se il codice è formalmente corretto, FALSE in caso contrario</returns>
        public static bool ControlloFormale(string cf)
        {
            if (String.IsNullOrEmpty(cf) || cf.Length < 16) return false;
            cf = Normalize(cf, false);
            if (!CheckRegex.Match(cf).Success)
            {
                // Regex failed: it can be either an omocode or an invalid Fiscal Code
                string cf_NoOmocodia = SostituisciLettereOmocodia(cf);
                if (!CheckRegex.Match(cf_NoOmocodia).Success) return false; // invalid Fiscal Code
            }
            return cf[15] == CalcolaCarattereDiControllo(cf.Substring(0, 15));
        }

        /// <summary>
        /// Effettua un "controllo formale" del Codice Fiscale indicato secondo i seguenti criteri:
        /// 
        /// - Controlla che non sia un valore nullo/vuoto.
        /// - Controlla che il codice sia coerente con le specifiche normative per i Codici Fiscali (inclusi possibili casi di omocodia).
        /// - Controlla che il carattere di controllo sia coerente rispetto al Codice Fiscale indicato.
        /// - Controlla la corrispondenza tra il codice fiscale e i dati anagrafici indicati.
        /// 
        /// IMPORTANTE: Si ricorda che, anche se il Codice Fiscale risulta "formalmente corretto", 
        /// non ci sono garanzie che si tratti di un Codice Fiscale relativo a una persona realmente esistente o esistita.
        /// </summary>
        /// <param name="cf">il codice fiscale da controllare</param>
        /// <param name="nome">Nome</param>
        /// <param name="cognome">Cognome</param>
        /// <param name="dataDiNascita">Data di nascita</param>
        /// <param name="genere">Genere ('M' o 'F')</param>
        /// <param name="codiceISTAT">Codice ISTAT (1 lettera e 3 numeri. Es.: H501 per Roma)</param>
        /// <returns>TRUE se il codice è formalmente corretto, FALSE in caso contrario</returns>
        public static bool ControlloFormaleOK(string cf, string nome, string cognome, DateTime dataDiNascita, char genere, string codiceISTAT)
        {
            if (String.IsNullOrEmpty(cf) || cf.Length < 16) return false;
            cf = Normalize(cf, false);
            string cf_NoOmocodia = string.Empty;
            if (!CheckRegex.Match(cf).Success)
            {
                // Regex failed: it can be either an omocode or an invalid Fiscal Code
                cf_NoOmocodia = SostituisciLettereOmocodia(cf);
                if (!CheckRegex.Match(cf_NoOmocodia).Success) return false; // invalid Fiscal Code
            }
            else cf_NoOmocodia = cf;

            // NOTE: 
            // - 'fc' è il codice fiscale inserito (potrebbe contenere lettere al posto di numeri per omocodia)
            // - 'cf_NoOmocodia' è il codice fiscale epurato di eventuali modifiche dovute a omocodia.

            if (String.IsNullOrEmpty(nome) || cf_NoOmocodia.Substring(3, 3) != CalcolaCodiceNome(nome)) return false;
            if (String.IsNullOrEmpty(cognome) || cf_NoOmocodia.Substring(0, 3) != CalcolaCodiceCognome(cognome)) return false;
            if (cf_NoOmocodia.Substring(6, 5) != CalcolaCodiceDataDiNascitaGenere(dataDiNascita, genere)) return false;
            if (String.IsNullOrEmpty(codiceISTAT) || cf_NoOmocodia.Substring(11, 4) != Normalize(codiceISTAT, false)) return false;

            // Il carattere di controllo, in caso di omocodia, è anch'esso calcolato sul codice fiscale modificato, quindi occorre utilizzare quest'ultimo.
            if (cf[15] != CalcolaCarattereDiControllo(cf.Substring(0, 15))) return false;

            return true;
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Calcola le 3 lettere del cognome indicato, utilizzate per il calcolo del Codice Fiscale.
        /// </summary>
        /// <param name="s">Il cognome della persona</param>
        /// <returns>Le 3 lettere che saranno utilizzate per il calcolo del Codice Fiscale</returns>
        private static string CalcolaCodiceCognome(string s)
        {
            s = Normalize(s, true);
            string code = string.Empty;
            int i = 0;

            // pick Consonants
            while ((code.Length < 3) && (i < s.Length))
            {
                for (int j = 0; j < Consonants.Length; j++)
                {
                    if (s[i] == Consonants[j]) code += s[i];
                }
                i++;
            }
            i = 0;

            // pick Vocals (if needed)
            while (code.Length < 3 && i < s.Length)
            {
                for (int j = 0; j < Vocals.Length; j++)
                {
                    if (s[i] == Vocals[j]) code += s[i];
                }
                i++;
            }

            // add trailing X (if needed)
            return (code.Length < 3) ? code.PadRight(3, 'X') : code;
        }

        /// <summary>
        /// Calcola le 3 lettere del nome indicato, utilizzate per il calcolo del Codice Fiscale.
        /// </summary>
        /// <param name="s">Il nome della persona</param>
        /// <returns>Le 3 lettere che saranno utilizzate per il calcolo del Codice Fiscale</returns>
        private static string CalcolaCodiceNome(string s)
        {
            s = Normalize(s, true);
            string code = string.Empty;
            string cons = string.Empty;
            int i = 0;
            while ((cons.Length < 4) && (i < s.Length))
            {
                for (int j = 0; j < Consonants.Length; j++)
                {
                    if (s[i] == Consonants[j]) cons = cons + s[i];
                }
                i++;
            }

            code = (cons.Length > 3)
                // if we have 4 or more consonants we need to pick 1st, 3rd and 4th
                ? cons[0].ToString() + cons[2].ToString() + cons[3].ToString()
                // otherwise we pick them all
                : code = cons;

            i = 0;
            // add Vocals (if needed)
            while ((code.Length < 3) && (i < s.Length))
            {
                for (int j = 0; j < Vocals.Length; j++)
                {
                    if (s[i] == Vocals[j]) code += s[i];
                }
                i++;
            }

            // add trailing X (if needed)
            return (code.Length < 3) ? code.PadRight(3, 'X') : code;
        }


        /// <summary>
        /// Calcola le 5 lettere relative a data di nascita e genere, utilizzate per il calcolo del Codice Fiscale.
        /// </summary>
        /// <param name="d">La data di nascita</param>
        /// <param name="g">Il genere ('M' o 'F')</param>
        /// <returns>Le 5 lettere che saranno utilizzate per il calcolo del Codice Fiscale.</returns>
        private static string CalcolaCodiceDataDiNascitaGenere(DateTime d, char g)
        {
            string code = d.Year.ToString().Substring(2);
            code += Months[d.Month - 1];
            if (g == 'M' || g == 'm') code += (d.Day <= 9) ? "0" + d.Day.ToString() : d.Day.ToString();
            else if (g == 'F' || g == 'f') code += (d.Day + 40).ToString();
            else throw new NotSupportedException("ERROR: genere must be either 'M' or 'F'.");
            return code;
        }

        /// <summary>
        /// Calcola il carattere di controllo sulla base dei precedenti 15 caratteri del Codice Fiscale.
        /// </summary>
        /// <param name="f15">I primi 15 caratteri del Codice Fiscale (ovvero tutti tranne il Carattere di Controllo)</param>
        /// <returns>Il carattere di controllo da utilizzare per il calcolo del Codice Fiscale</returns>
        private static char CalcolaCarattereDiControllo(string f15)
        {
            int tot = 0;
            byte[] arrCode = Encoding.ASCII.GetBytes(f15.ToUpper());
            for (int i = 0; i < f15.Length; i++)
            {
                if ((i + 1) % 2 == 0) tot += (char.IsLetter(f15, i))
                    ? arrCode[i] - (byte)'A'
                    : arrCode[i] - (byte)'0';
                else tot += (char.IsLetter(f15, i))
                    ? ControlCodeArray[(arrCode[i] - (byte)'A')]
                    : ControlCodeArray[(arrCode[i] - (byte)'0')];
            }
            tot %= 26;
            char l = (char)(tot + 'A');
            return l;
        }

        /// <summary>
        /// Sostituisce le lettere utilizzate per modificare il Codice Fiscale in caso di omocodia (se presenti) con i relativi numeri.
        /// </summary>
        /// <param name="cf">Fiscal Code potentially containing omocode chars</param>
        /// <returns>Il Codice Fiscale epurato dalle eventuali modifiche dovute a casi di omocodia (da utilizzare per il calcolo di nome, cognome et. al.)</returns>
        private static string SostituisciLettereOmocodia(string cf)
        {
            char[] cfChars = cf.ToCharArray();
            int[] pos = new[] { 6, 7, 9, 10, 12, 13, 14 };
            foreach (int i in pos) if (!Char.IsNumber(cfChars[i])) cfChars[i] = OmocodeChars.IndexOf(cfChars[i]).ToString()[0];
            return new string(cfChars);
        }

        /// <summary>
        /// Effettua varie operazioni di normalizzazione su una stringa, rimuovendo spazi e/o caratteri non utilizzati.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="normalizeDiacritics">TRUE per sostituire le lettere accentate con il loro equivalente non accentato</param>
        /// <returns></returns>
        private static string Normalize(string s, bool normalizeDiacritics)
        {
            if (String.IsNullOrEmpty(s)) return s;
            s = s.Trim().ToUpper();
            if (normalizeDiacritics)
            {
                string src = "ÀÈÉÌÒÙàèéìòù";
                string rep = "AEEIOUAEEIOU";
                for (int i = 0; i < src.Length; i++) s = s.Replace(src[i], rep[i]);
                return s;
            }
            return s;
        }
        #endregion Private Methods
    }

    public enum SanitizeType
    {
        FieldOnly,
        WhereClause,
        SelectList,
        Select,
        NumericField,
        StringField,
        DateTimeField
    }

    public class MyTableAdapter
    {
        SqlDataReader dr;
        int vic;

        public MyTableAdapter(SqlDataReader dr)
        {
            this.dr = dr;
        }

        public SqlDataReader GetData()
        {
            return dr;
        }

        public int VirtualItemCount()
        {
            return vic;
        }

        public SqlDataReader GetData(int startRow, int maxRows)
        {
            return dr;
        }
    }

    public class CredentialsCryptography
    {
        #region Encryption/Decryption
        private static string ENCRYPTION_KEY = ConfigurationManager.AppSettings["EncryptionKey"].ToString();
        private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());

        public static string Encrypt(string inputText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] plainText = Encoding.Unicode.GetBytes(inputText);
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string inputText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(inputText);
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new byte[encryptedData.Length];
                        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }

        #endregion
    }

    public class FilterRows : IEnumerable
    {
        DataView dataView;
        private int rowsToShow;

        public FilterRows(DataView dataView, int rowsToShow)
        {
            this.rowsToShow = rowsToShow;
            this.dataView = dataView;
        }

        public IEnumerator GetEnumerator()
        {
            return new PageOfData(this.dataView.GetEnumerator(), this.rowsToShow);
        }
        
        internal class PageOfData : IEnumerator
        {
            private IEnumerator e;
            private int cnt = 0;
            private int rowsToShow;

            internal PageOfData(IEnumerator e, int rowsToShow)
            {
                this.rowsToShow = rowsToShow;
                this.e = e;
            }

            public object Current
            {
                get { return e.Current; }
            }

            public bool MoveNext()
            {
                // If we've hit out limit return false
                if (cnt >= rowsToShow)
                    return false;

                // Track the current row
                cnt++;

                return e.MoveNext();
            }

            public void Reset()
            {
                e.Reset();
                cnt = 0;
            }
        }
    }

    public interface IControlBase
    {
        void ReloadControl();
    }

    public class MyListCache
    {
        private List<object> _MyList = null;
        public List<object> MyList(string keyCache)
        {
            if (_MyList == null)
            {
                _MyList = (HttpContext.Current.Cache[keyCache] as List<object>);
                if (_MyList == null)
                {
                    _MyList = new List<object>();
                    HttpContext.Current.Cache.Insert(keyCache, _MyList, null, DateTime.Now.AddHours(2), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            }
            return _MyList;
        }

        public void ClearList(string keyCache)
        {
            HttpContext.Current.Cache.Remove(keyCache);
        }
    }

}