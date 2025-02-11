﻿using Backend.Data.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTO.Auth
{
    public class RegisterUserBody
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        public bool IsClient { get; set; }
    }
}
