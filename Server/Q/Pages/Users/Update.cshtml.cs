


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Q.Data;
using Q.Model;

namespace Q.Pages.Users
{
    public class UpdateModel : PageModel
    {
        private readonly Q.Data.QContext _context;

        public UpdateModel(Q.Data.QContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User Users { get; set; }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_context.Users.Find(Users.Id) == null)
            {
                ModelState.AddModelError("IdError", "User with this id already exists");
                return Page();
            }
            else
            {
                List<User> users = (from u in _context.Users
                                    where u.Id == Users.Id
                                    select u).ToList();

                foreach (User u in users)
                {
                    u.Name = Users.Name;
                    u.PhoneNumber = Users.PhoneNumber;

                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
