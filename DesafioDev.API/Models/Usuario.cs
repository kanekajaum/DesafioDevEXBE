﻿using System.ComponentModel.DataAnnotations;

namespace DesafioDev.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string SenhaHash { get; set; }
    }
}