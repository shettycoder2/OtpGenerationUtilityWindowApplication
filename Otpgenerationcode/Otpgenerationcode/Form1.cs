using Otpgenerationcode.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otpgenerationcode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string otp = GenerateOTP(6); // Generate a 6-character OTP
            MessageBox.Show("Generated OTP" );

            // Send OTP via email
            SendOTP(textBox1.Text, otp);
           MessageBox.Show("OTP sent successfully!");
        }
        public static void SendOTP(string toEmail, string otp)
        {
            var fromAddress = new MailAddress("your email @gmail.com", "sagar joshi");
            var toAddress = new MailAddress(toEmail, "Recipient Name");
       //     const string fromPassword = "raghunath";
            const string subject = "Your OTP Code";
            string body = $"Your OTP code is: {otp}";

            var smtp = new SmtpClient
            {
               
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address.ToString(), "ljaz panj zdus paae") //app code is here  

            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {

                smtp.Send(message);
            }
        }
        public static string GenerateOTP(int length)
        {
            if (length <= 0) throw new ArgumentException("Length must be greater than 0.");

      
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[length];
                rng.GetBytes(tokenData);
               
          
                char[] chars = new char[length];
                for (int i = 0; i < length; i++)
                {
                    chars[i] = (char)('0' + (tokenData[i] % 10)); // Limit to 0-9
                }

                return new string(chars);
            }
        }
    }
}
