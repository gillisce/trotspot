using HorseShow_Framework.Models;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace HorseShow_Framework.HelperClasses
{
    public class Email
    {

        public static void GenerateEmailContent(Person person, HorseRider horse, List<string> arrRegisteredClasses, string strDivisionName)
        {
            //var message = new MimeMessage();
            string body = "";

            //message.To.Add(new MailboxAddress(person.strFirstName, person.strEmail));
            //message.Subject = "Registration Details -  " + DateTime.Now.Year + "Kellis C.R.U.S.A.D.E Horse Show";

            //message.From.Add(new MailboxAddress("Kelli's Crusade - No Reply", "kcrusade@trotspottech.com"));
            string link = "http://www.kelliscrusade.org";

            body += "<p style=3D'font - family:Cambria; font - size:11pt'>Hello " + person.strFirstName + " " + person.strLastName + "!</p>";
            body += "<p style=3D'font - family:Cambria; font - size:11pt'>Thank you for registering for the 2019 Kelli's C.R.U.S.A.D.E horse show. We are so excited to have you attnend our show and appreciate your support! </ p > ";
            body += "<p style=3D'font - family:Cambria; font - size:11pt'>According to our records you have registered in the following division: " + strDivisionName + ".</ p > ";
            body += "<p style = 3D'font - family:Cambria; font - size:11pt'> According to our records, you have registered for the following classes: <br/> <ul> ";
            foreach (string item in arrRegisteredClasses)
            {
                body += "<li>" + item + "</li>";
            }
            body += "</ul></p>";
            body += "<p> You currently owe $" + horse.fltAmountDue + "</p>";
            body += "<p> Starting at 8:00 AM Saturday, June 29 you can begin to check in at the entry booth. You <strong>MUST</strong> check in, <strong>even if this information is correct so you can sign the waiver.</strong></p>";
            body += "If you need to adjust the classes you are registered for, please don't make another entry. We can correct it at the entry booth on the day of the show. <br/> To review the show bill for the entire day, please visit";
            body += "<a  href=\"" + link + "\"> www.kelliscrusade.org <a/>. <br/> Thank you very much! We will see you soon! </p>";

            //var builder = new BodyBuilder();
            //builder.HtmlBody = body;
            //message.Body = builder.ToMessageBody();

            //SendRegistrationEmail(message);


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("postmaster@trotspottech.com");
            mail.To.Add(person.strEmail);
            mail.CC.Add("kcrusaderegistration@trotspottech.com");
            mail.Subject = "Registration Details -  " + DateTime.Now.Year + "Kellis C.R.U.S.A.D.E Horse Show";
            mail.IsBodyHtml = true;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient("m06.internetmailserver.net")
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 587, // also tried 25 and 465, but they don't seem to work
                Timeout = 60000,
                UseDefaultCredentials = false
            };
            NetworkCredential credential = new NetworkCredential("postmaster@trotspottech.com", "#L1feisshort");

            smtp.Credentials = credential;
            smtp.Send(mail);

            return;

        }


        private static void SendRegistrationEmail(MimeMessage messageBody)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("m06.internetmailserver.net");
                client.Authenticate(new NetworkCredential("chris@trotspottech.com", "#0verMyHead"));
               // client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Send(messageBody);
                client.Disconnect(true);

            }
        }



    }
}