namespace INFASS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Registration(string tableName, string[] columns, string[] values)
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
    }
}