using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using RoomReservationManagement.GeneralClasses;

namespace RoomReservationManagement.GeneralClasses
{

    /*
     * This class is used to send out emails
     */
    public class EmailHelper
    {
        public ErrorLogging errlogger = new ErrorLogging();


		/// <summary>
		/// Function will send an email.
		/// </summary>
		/// <param name="toAddress"></param>
		/// <param name="message"></param>
		/// <param name="subject"></param>
        public void sendEmail(string toAddress, string message, string subject)
        {
            try
            {

               
                MailMessage mail = new MailMessage("Batemandonotreply@gmail.com", toAddress);
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("Batemandonotreply@gmail.com", "capstoneCS490!");
                mail.Subject = subject;
                mail.Body = message;
                client.Send(mail);
            }
            catch(Exception e)
            {
                errlogger.log_error("roomReservation", "EmailHelper", "sendEmail", e.Message);
            }

        }



    }
}