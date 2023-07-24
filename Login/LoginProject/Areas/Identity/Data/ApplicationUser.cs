using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace LoginProject.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    public string FirstName { get; set; }

    public string LastName { get; set; }
  
    public string? Title { get; set; }

    [DataType(DataType.Date)]
    public DateTime Dob { get; set; }

    public string? Address { get; set; }

    public string? Suburb { get; set; }
 
    public string? State { get; set; }

    public string Postcode { get; set; }

    public string PhoneNumber { get; set; }

    public bool Staff { get; set; }

}

