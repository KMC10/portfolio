using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using KMC.Portfolio.Services;

namespace KMC.Portfolio.Pages
{
    public class ContactModel : PageModel
    {
        //Email Service 
        private readonly EmailService _emailService;

        public ContactModel(EmailService emailService)
        {
            _emailService = emailService;
        }


        [BindProperty]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        
        public string Message { get; set; }
        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            await _emailService.SendEmailAsync(
                Email,
                Name,
                Message);

            // ViewData["Message"] = "Your message was received!";

            Name = "";
            Email = "";
            Message = "";
            ModelState.Clear();
        }
    }
}
