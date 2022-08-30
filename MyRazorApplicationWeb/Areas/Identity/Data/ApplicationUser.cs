﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyRazorApplicationWeb.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    [Required]
    public string FirsName { get; set; }
    [Required]
    public string LastName { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }


}

