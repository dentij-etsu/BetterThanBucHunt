namespace BucStop.Services
{
    public class AccessCode
    {
        // This the expiration time for the code in minutes
        double expirationTime = 10;

        public string email;
        public string code;
        public DateTime time;


        public AccessCode()
        {
            time = DateTime.Now;
            time = time.AddMinutes(expirationTime);

            Random generator = new Random();
            code = generator.Next(0, 1000000).ToString("D6");

            email = "NO EMAIL DEFINED";
        }

        public AccessCode(string email)
        {
            time = DateTime.Now;
            time = time.AddMinutes(expirationTime);

            Random generator = new Random();
            code = generator.Next(0, 1000000).ToString("D6");

            this.email = email;
        }


    }
}
