using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportChat.Models
{
    public class Agent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Efficiency { get; set; }
        public int Capacity { get; set; }
        public int Concurrency { get; set; }
        public bool isShiftActive { get; set; }
        public int SeniorityId { get; set; }
        public Seniority? Seniority { get; set; }
        public List<ChatSession> ActiveChat = new List<ChatSession>();
    }
}
