using BusinessLogicLayer.Abstract;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Data;


namespace PresentationLayer.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly ApplicationDbContext _context;
        public UserController(IUserRepo userRepo, ApplicationDbContext context)
        {
            _userRepo = userRepo;
            _context = context;
        }
        
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");
            }
            return View();  
        }

        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepo.Users.FirstOrDefaultAsync(x => x.UserNickName == model.UserNickName || x.UserEmail == model.UserEmail);
                if (user == null)
                {
                    _userRepo.CreateNewUser(new User
                    {
                        UserNickName =model.UserNickName,
                        UserName = model.UserName,
                        UserSurname = model.UserSurname,
                        UserDescription = model.UserDescription,
                        UserEmail = model.UserEmail,
                        Password = model.Password,
                        UserCreatedDate = DateTime.Now,
                        CheckedByAdmin = false,
                    });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya Email başka bir kullanıcı tarafından kullanılıyor");
                };
                return RedirectToAction("Login");
            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

             var isUser = _userRepo.Users.FirstOrDefault(x => x.UserNickName == model.UserNickName && x.Password == model.Password);

            if (ModelState.IsValid)
            {
              

            if (isUser.CheckedByAdmin == false)
            {
                return RedirectToAction("Index", "Home");
            }
             
                if (isUser != null)
                {
                    var userClaims = new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));

                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserNickName ?? ""));

                    userClaims.Add(new Claim(ClaimTypes.Email, isUser.UserEmail ?? ""));

                    if (isUser.UserNickName == "adminolankisi" && isUser.UserId == 1 && isUser.UserName == "admininadi")
                    {

                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                  new ClaimsPrincipal(claimsIdentity),
                                                  authProperties);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya Şifreniz yanlış");
                }

            }

            return View(model);
        }


        public IActionResult Profile()
        {
            string username = User.Identity.Name;

            var user = _userRepo.Users.FirstOrDefault(x => x.UserName == username);

            return View(user);
            
        }

        public async Task<IActionResult> Messages()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var user = await _userRepo.FindByIdAsync(userId);
            var receivedMessages = await _context.Messages
                                                .Include(m => m.MessageSender)
                                                .Where(m => m.MessageReceiverId == userId)
                                                .ToListAsync();

            var viewModel = new ProfileMessageViewModel
            {
                User = user,
                ReceivedMessages = receivedMessages
            };

            return View(viewModel);

        }

        [HttpGet]
        [Route("User")]
        public IActionResult Search(String searchString)
        {


            var users = from u in _userRepo.Users
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.Contains(searchString));

                var foundUser = users.FirstOrDefault();
                if (foundUser != null)
                {
                    return RedirectToAction("RelatedInfo", new { id = foundUser.UserId });
                }
            }

            return View(users.ToList());


        }

        [HttpGet]
        [Route("User/RelatedInfo/{id}")]
        public ActionResult RelatedInfo(int id)
        {
            var user = _userRepo.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound(); 
            }

            return View(user);
        }

        [Authorize(Policy = "EditProfilePolicy")]

        public ActionResult Update(int id)
        {
            var user = _userRepo.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }


            var viewModel = new UserUpdateViewModel
            {
                UserId = user.UserId,
                UserNickName = user.UserNickName,
                UserName = user.UserName,
                UserSurname = user.UserSurname,
                UserDescription = user.UserDescription,
                UserEmail = user.UserEmail,
                Password = user.Password,

            };

            return View(viewModel);

        }

        [HttpPost]

        public ActionResult Update(UserUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new User
            {
                UserId = viewModel.UserId,
                UserNickName = viewModel.UserNickName,
                UserName = viewModel.UserName,
                UserSurname = viewModel.UserSurname,
                UserDescription = viewModel.UserDescription,
                UserEmail = viewModel.UserEmail,
                Password = viewModel.Password,

            };
            _userRepo.UpdateUser(user);


            return RedirectToAction("Login", "Users", new { id = viewModel.UserId });
        }

    }
}
