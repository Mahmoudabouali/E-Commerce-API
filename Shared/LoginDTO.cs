﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }
    }
}