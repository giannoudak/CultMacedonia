﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

using CULTMACEDONIA_v2.Models;

namespace CULTMACEDONIA_v2.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SendMail(ContactFormViewModel form)
        {
            bool sent = false;
            string retValue = @CultResources.Shared.ContactSendFailure;
            if (!ModelState.IsValid)
            {
                return Content(retValue);
            }

            if (ModelState.IsValid)
            {


                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                string msg = string.Empty;
                try
                {
                    MailAddress fromAddress = new MailAddress(form.Email, form.Name);
                    message.From = fromAddress;
                    message.To.Add("giannoudak@gmail.com,stavroulakis06157@gmail.com");

                    message.Subject = String.Format("Request to Contact from {0}", form.Name); ;
                    message.IsBodyHtml = true;
                    message.Body = form.Message;
                    // We use gmail as our smtp client
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(
                        "giannoudak", "d@taforc3");

                    smtpClient.Send(message);
                    retValue = @CultResources.Shared.ContactSendSuccess;
                    sent = true;
                }
                catch (Exception ex)
                {
                    retValue = @CultResources.Shared.ContactSendFailure + " Error: " + ex.Message;
                    sent = false;
                }
            }

            return Json(new { retValue = retValue, sent = sent }, JsonRequestBehavior.AllowGet);


        }
    }
	
}