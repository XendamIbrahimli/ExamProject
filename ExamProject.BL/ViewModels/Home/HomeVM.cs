using ExamProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.BL.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Agent>? Agents { get; set; } 
    }
}
