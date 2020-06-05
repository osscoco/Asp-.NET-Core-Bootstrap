using Microsoft.AspNetCore.Mvc;
using TP_Corentin_Maxence.Models;
using MySql.Data.MySqlClient;
using MimeKit;
using System.Net.Mail;
using System.Net.Mime;
using Mailjet.Client;
using System.Configuration;
using Mailjet.Client.Resources;
using System;

namespace TP_Corentin_Maxence.Controllers
{
    public class ContactController : Controller
    {

        public IActionResult Index()
        {
            return View(new Models.Contact());
        }
        [HttpPost]
        public IActionResult Index(Models.Contact contact)
        {

            using (MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;pwd=root;database=joueur;persistsecurityinfo=True"))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into contact (fromcontact,corpcontact) VALUES (@fromcontact,@corpcontact)", conn);
                cmd.Parameters.AddWithValue("@fromcontact", contact.fromcontact);
                cmd.Parameters.AddWithValue("@corpcontact", contact.corpcontact);
                cmd.ExecuteNonQuery();
            }

            //
            try
            {
                MailMessage mailMsg = new MailMessage();

                mailMsg.To.Add(new MailAddress("c.jeannot@iia-laval.fr", "Titulaire"));

                mailMsg.From = new MailAddress(contact.fromcontact, "L'envoyeur");

                mailMsg.Subject = "Groupe BTS SIO SLAM E1 K";
                string text = contact.corpcontact;
                string html = @"<strong>Voici la demande du contact : </strong>" + contact.fromcontact + @"<br />" + contact.corpcontact;
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                SmtpClient smtpClient = new SmtpClient("in-v3.mailjet.com", Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("ea87cc78c5cdad1740c6f8ad8823ebfb", "7e720047387183570bc2c6fa7fa919d0");
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }





            //

            return RedirectToAction("Index");


        }



    }
}