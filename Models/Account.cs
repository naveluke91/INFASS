namespace INFASS.Models
{
    public class Account
    {
        public string Insert(string tableName, string[] columns, string[] values)
        {
            string columnString = "";
            string valueString = "";

            for (int i = 0; i < columns.Length; i++)
            {
                columnString += columns[i] + (i < columns.Length - 1 ? ", " : "");
                valueString += $"'{values[i]}'" + (i < values.Length - 1 ? ", " : "");
            }

            return $"INSERT INTO {tableName} ({columnString})\nVALUES({valueString})";
        }

        public string Select(string tableName)
        {
            return $"SELECT * FROM {tableName}";
        }

        public string Delete(string tableName, int id)
        {
            return $"DELETE FROM {tableName}\nWHERE Id = {id}";
        }

        public string Update(string tableName, int id, string newUsername, string newPassword)
        {
            return $"UPDATE {tableName}\nSET Username = '{newUsername}', Password = '{newPassword}'\nWHERE Id = {id}";
        }
    }
}
