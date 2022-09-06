﻿namespace University.DataAccess.Models.DataModels
{
    using System.ComponentModel.DataAnnotations;
    public class User : BaseEntity
    {
        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;        

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
