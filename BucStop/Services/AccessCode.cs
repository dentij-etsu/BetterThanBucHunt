namespace BucStop.Services
{
    public class AccessCode
    {
        // This the expiration time for the code in minutes
        double expirationTime = 10;

        public string email;
        public string code;
        public DateTime time;

        /// <summary>
        /// Non-parameterized constructor. This is initilized with an undefined email if used.
        /// </summary>
        public AccessCode()
        {
            time = DateTime.Now;
            time = time.AddMinutes(expirationTime);

            Random generator = new Random();
            code = generator.Next(0, 1000000).ToString("D6");

            email = "NO EMAIL DEFINED";
        }
        /// <summary>
        /// parameterized contructor
        /// </summary>
        /// <param name="email"> The user's email that will be attached to the access code </param>
        public AccessCode(string email)
        {
            time = DateTime.Now;
            time = time.AddMinutes(0.5);

            Random generator = new Random();
            code = generator.Next(0, 1000000).ToString("D6");

            this.email = email;
        }
        /// <summary>
        /// This method compares the expiration time of the current access code object against the current system time,
        /// to see if the access code is expired.
        /// </summary>
        /// <returns> true if the access code is expired </returns>
        public bool isExpired ()
        {
            return time <= DateTime.Now;
        }

    }
}
