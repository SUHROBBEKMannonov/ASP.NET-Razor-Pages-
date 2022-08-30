using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorApplicationWeb.Areas.Identity.Data;

namespace MyRazorApplicationWeb.Areas.Identity.Pages.Account
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUser modelUsers { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(string id)
        {
            modelUsers = _db.Users.Find(id);
        }
        public async Task<IActionResult> OnPost(ApplicationUser modelUsers)
        {



            var categoryFromDb = _db.Users.Find(modelUsers.Id);
            if (categoryFromDb != null)
            {
                _db.Users.Remove(categoryFromDb);
                await _db.SaveChangesAsync();
                //TempData["success"] = "User is deleted successfully!!!";
                return RedirectToPage("./Profile");
            }


            return Page();

        }
    }
}

