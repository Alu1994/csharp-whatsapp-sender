using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsAppApi;

namespace WhatsappSender.Controllers
{
    public class WhatsappMessageController : Controller
    {
        public IActionResult Index()
        {
            string from = "5511988801976";
            string to = "5511988801976";

            string message = "mensagem do mats";

            var whatsApp = new WhatsApp(from, "", "", false, false);

            whatsApp.OnConnectSuccess += () =>
            {
                ViewBag.IsConnected = " IsConnected: sim ";

                whatsApp.OnLoginSuccess += (phoneNumber, data) =>
                {
                    whatsApp.SendMessage(to, message);
                    ViewBag.IsMessageSent = " IsMessageSent: sim ";
                };

                whatsApp.OnLoginFailed += (data) =>
                {
                    whatsApp.SendMessage(to, message);
                    ViewBag.IsLoginFailed = " IsLoginFailed: sim ";
                };

                whatsApp.Login();
            };

            whatsApp.OnConnectFailed += (ex) =>
            {
                ViewBag.IsConnectFailed = ex.Message;
            };

            return View();
        }
    }
}
