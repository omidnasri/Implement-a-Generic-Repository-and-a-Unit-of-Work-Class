using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karin.HandleException
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string Details(Exception ex)
        {
            string errorDetails = string.Empty;
            if (ex != null)
            {
                errorDetails = ex.Message;
                if (ex.InnerException != null)
                {
                    errorDetails += "<br/>" + ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                        errorDetails += "<br/>" + ex.InnerException.InnerException.Message;
                }
            }

            //var context = new NezamApp.AppData.NezamDBEntities();

            errorDetails = DeleteErrorCheck(errorDetails);
            errorDetails = DuplicateErrorCheck(errorDetails);
            errorDetails = CheckConnectionError(errorDetails);

            return errorDetails.Replace("'", "&apos;").Replace(".", "&middot;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("(", "").Replace(")", "").Replace("errorDetails", "errorDetails()");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errDetail"></param>
        /// <returns></returns>
        private static string DeleteErrorCheck(string errDetail)
        {
            if (errDetail.Contains("DELETE statement conflicted"))
            {
                errDetail = errDetail.Replace("An error occurred while updating the entries. See the inner exception for details.<br/>", "");
                string col = errDetail.Substring(errDetail.IndexOf("column '", StringComparison.Ordinal) + 8, errDetail.IndexOf("'.", StringComparison.Ordinal) - errDetail.IndexOf("column '", StringComparison.Ordinal) - 8);
                string table = errDetail.Substring(errDetail.IndexOf("\"dbo.", StringComparison.Ordinal) + 5, errDetail.IndexOf("\", c", StringComparison.Ordinal) - errDetail.IndexOf("\"dbo.", StringComparison.Ordinal) - 5);
                string pattern = "There is no possibility of deleting this item." +
                                 "<div class=\"form-group\" style=\"margin-top: 20px;\">" +
                                 "<a onclick=\"errorDetails()\">More details</a></div>" +
                                 "<div class=\"form-group\" id=\"DetailCode\" style=\"display: none; direction: ltr;\">" + errDetail + "</div>";
                return pattern;
            }
            return errDetail;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errDetail"></param>
        /// <returns></returns>
        private static string DuplicateErrorCheck(string errDetail)
        {
            if (errDetail.Contains("duplicate key"))
            {
                errDetail = errDetail.Replace("An error occurred while updating the entries. See the inner exception for details.<br/>", "");
                string table = errDetail.Substring(errDetail.IndexOf("'dbo.", StringComparison.Ordinal) + 5, errDetail.IndexOf(".\r\n", StringComparison.Ordinal) - errDetail.IndexOf("'dbo.", StringComparison.Ordinal) - 5);
                string newDetail = "Can not insert duplicate value for a key column in the table " + table;

                return newDetail + "<br/><br/>" + errDetail;
            }
            return errDetail;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errDetail"></param>
        /// <returns></returns>
        private static string CheckConnectionError(string errDetail)
        {
            string newDetail = string.Empty;
            if (errDetail.Contains("SQL Server service has been paused"))
                newDetail = "The database connection is not possible. Please check database connection";
            if (errDetail.Contains("server was not found"))
                newDetail = "The database connection is not possible. Please check database connection";
            if (newDetail != "")
                return newDetail + "<br/><br/>" + errDetail;
            return errDetail;
        }
    }
}
