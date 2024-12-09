using AspNetCoreHero.ToastNotification.Abstractions;
using urun.Models;
using urun.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace urun.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ContactRepository _contactRepository;
        private readonly INotyfService _notyf;

        public AdminController(ContactRepository contactRepository, INotyfService notyf)
        {
            _contactRepository = contactRepository;
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _contactRepository.GetAllAsync();
            return Json(new { data = contacts });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                await _contactRepository.DeleteAsync(id);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContactStatus(int id, bool isRead)
        {
            try
            {
                var contact = await _contactRepository.GetByIdAsync(id);
                if (contact != null)
                {
                    contact.IsRead = isRead;
                    await _contactRepository.UpdateAsync(contact);
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
    }
} 