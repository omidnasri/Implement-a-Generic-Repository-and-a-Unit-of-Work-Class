namespace Karin.BaseConfig.SQL
{
    public class SqlQuery
    {
        public static string DropTableIfExist(string table)
        {
            return "IF OBJECT_ID('" + table + "') IS NOT NULL BEGIN DROP TABLE " + table + " END";
        }

        public static string DropRowFromTable(string table, int row)
        {
            return "DELETE FROM " + table + " WHERE id = '" + row + "'; ";
        }
    }
}
