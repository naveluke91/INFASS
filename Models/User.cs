namespace INFASS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public string Registration(string uname, string pwd, string confirmPwd)
        {
            string queryFormat = $"INSERT INTO Users(Username, Password, Confirmpassword)\nVALUES('{uname}', '{pwd}', '{confirmPwd}')";
            return queryFormat;
        }
    }
    
}
