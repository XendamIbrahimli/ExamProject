using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.BL.ViewModels.AgentVM
{
    public class AgentUpdateVM
    {
        [Required, MaxLength(32)]
        public string Fullname { get; set; } = null!;
        public IFormFile ImageUrl { get; set; } = null!;
        public int DesignationId { get; set; }
    }
}
