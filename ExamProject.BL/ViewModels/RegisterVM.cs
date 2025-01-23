using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.BL.ViewModels
{
    public class RegisterVM
    {
        [Required,MaxLength(32)]
        public string Username { get; set; } = null!;
        [Required,MaxLength(128),EmailAddress]
        public string Email { get; set; } = null!;
        [Required,MaxLength(32),DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required, MaxLength(32), DataType(DataType.Password),Compare("Password")]
        public string RePassword { get; set; } = null!;
    }
}
