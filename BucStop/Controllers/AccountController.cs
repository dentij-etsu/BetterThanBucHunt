using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using BucStop.Services;

namespace BucStop.Controllers
{
    public class AccountController : Controller
    {
        public string email { get; set; } = string.Empty;
        public AccessCode accessCode;

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email)
        {
            if (Regex.IsMatch(email, @"\b[A-Za-z0-9._%+-]+@etsu\.edu\b"))
            {

                accessCode = new AccessCode(email);

                SendEmail(accessCode.email, accessCode.code);

                // If authentication is successful, create a ClaimsPrincipal and sign in the user
                // ClaimsPrincipal is used to create a cookie to store the user's log in information
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.NameIdentifier, "user_id"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, "custom");
                var userPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in the user
                await HttpContext.SignInAsync("CustomAuthenticationScheme", userPrincipal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication failed, return to the login page with an error message
                ModelState.AddModelError(string.Empty, "Only ETSU students can play, sorry :(");
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CustomAuthenticationScheme");
            return RedirectToAction("Login");
        }

        public void SendEmail(string email, string code)
        {
            // Gmail credentials (replace with your Gmail address and password)
            string senderEmail = "bucstop24@gmail.com";
            string senderPassword = "akbr aulz okmf lack";

            // Recipient's email address
            string recipientEmail = email;

            // Create and configure the SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            // Create the email message
            MailMessage mail = new MailMessage(senderEmail, recipientEmail);
            mail.Subject = "Login to BucStop";
            mail.Body = "Welcome to BucStop!\n\nHere is your login code: " + code;

            try
            {
                // Send the email
                smtpClient.Send(mail);
                Console.WriteLine("Test email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send test email. Error: " + ex.Message);
            }

        }
    }
}
