using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerConsole.Enums;

namespace TaskTrackerConsole.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public Status TaskStatus { get; set; }

        public DateTime CreatedAt { get; set;}

        public DateTime UpdatedAt { get; set;}
    }
}