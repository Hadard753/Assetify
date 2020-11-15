using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assetify.Models
{
    public class User : UserWithoutPassword
    {
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}
