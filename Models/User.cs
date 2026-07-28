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
            this.Username = uname;
            this.Password = pwd;
            this.ConfirmPassword = confirmPwd;

            string queryFormat = $"INSERT INTO Users(Username, Password, Confirmpassword)\nVALUES('{uname}', '{pwd}', '{confirmPwd}')";
            return queryFormat;
        }
    }
    
}
