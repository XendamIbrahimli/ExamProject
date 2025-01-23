using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.Core.Models
{
    public class Agent
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int? DesignationId { get; set; }
        public Designation? Designation { get; set; }

    }
}
