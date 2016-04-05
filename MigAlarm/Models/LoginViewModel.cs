﻿using System.ComponentModel.DataAnnotations;

namespace MigAlarm.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RedirectUrl { get; set; }
    }
}