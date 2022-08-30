using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorApplicationWeb.Areas.Identity.Data;
using Microsoft.Extensions.Configuration;
using ContosoUniversity;

namespace MyRazorApplicationWeb.Areas.Identity.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration Configuration;
        public ProfileModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }



        public string NameSort { get; set; }
        public string EmailSort { get; set; }
        public string LNameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        


        public PaginatedList<ApplicationUser> Students { get; set; }


        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            LNameSort = sortOrder == "Lname" ? "Lname_desc" : "Lname";
            EmailSort = sortOrder == "Email" ? "Email_desc" : "Email";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            
            CurrentSort = sortOrder;
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            IQueryable<ApplicationUser> studentsIQ = from s in _context.Users
                                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.Contains(searchString)
                                       || s.FirsName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.FirsName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.BirthDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.BirthDate);
                    break;
                case "Lname":
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
                case "Lname_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Email":
                    studentsIQ = studentsIQ.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.Email);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.FirsName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Students = await PaginatedList<ApplicationUser>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}

