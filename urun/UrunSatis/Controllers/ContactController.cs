using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using urun.Models;
using urun.Repositories;
using urun.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace urun.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public ContactController(ContactRepository contactRepository, IMapper mapper, INotyfService notyf)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddContact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = _mapper.Map<Contact>(model);
                contact.IsRead = false;
                contact.Created = DateTime.Now;
                contact.Updated = DateTime.Now;

                await _contactRepository.AddAsync(contact);
                _notyf.Success("Mesajınız başarıyla gönderildi.");
                return RedirectToAction("Index", "Home");
            }
            return View("Index", model);
        }



        [HttpPost]
        public async Task<IActionResult> Send(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = _mapper.Map<Contact>(model);
                contact.IsRead = false;
                contact.Created = DateTime.Now;
                contact.Updated = DateTime.Now;

                await _contactRepository.AddAsync(contact);
                _notyf.Success("Mesajınız başarıyla gönderildi.");
                return RedirectToAction("Index", "Home");
            }
            return View("Index", model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Messages()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _contactRepository.GetAllAsync();
            var messageModels = _mapper.Map<List<ContactModel>>(messages);
            return Json(new { data = messageModels });
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var message = await _contactRepository.GetByIdAsync(id);
            if (message != null)
            {
                message.IsRead = true;
                message.Updated = DateTime.Now;
                await _contactRepository.UpdateAsync(message);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactRepository.DeleteAsync(id);
            _notyf.Success("Mesaj başarıyla silindi.");
            return Json(new { success = true });
        }
    }
} 