using BusinessLogicLayer.Abstract;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    public class MessageController : Controller
    {
      

 
        private readonly IUserRepo _userManager;
        private readonly ApplicationDbContext _context;

        public MessageController(IUserRepo userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int receiverId, string content)
        {
            var senderId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");

            var message = new Message
            {
                MessageSenderId = senderId,
                MessageReceiverId = receiverId,
                MessageContent = content,
                MessageDateTime = DateTime.Now
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }

}

