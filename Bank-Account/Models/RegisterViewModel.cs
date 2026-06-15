namespace Bank_Account.Models
{
    public class RegisterViewModel
    {
        public int id { get; set; }
        public string fullname {get; set;}
        public DateTime dob {get; set;}
        public string cnicnum {get; set;}
        public string phonenum { get; set; }
        public string email { get; set; }
        public string accounttype { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string otpcode { get; set; }
        public DateTime otpexpiry { get; set; }
        public bool isverified { get; set; }
        public bool agreeterm { get; set; }

    }


    public class OTPverifyModel
    { 
        public string email { get; set; }
        public string enteredOtp { get; set; }
    }
}
