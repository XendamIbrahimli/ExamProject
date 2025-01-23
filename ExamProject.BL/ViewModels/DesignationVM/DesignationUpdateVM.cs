using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.BL.ViewModels.DesignationVM
{
    public class DesignationUpdateVM
    {
        [Required, MaxLength(32)]
        public string Name { get; set; } = null!;
    }
}
