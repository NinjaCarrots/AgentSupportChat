using System.ComponentModel.DataAnnotations;

namespace SupportChat.Models
{
    public class Seniority
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double SeniorityMultiplier { get; set; }
        public int MaxConcurrentChats { get; set; }
        public bool TeamLeadResponsibilities { get; set; }
        public TimeSpan ShiftStartTime { get; set; }
        public TimeSpan ShiftEndTime { get; set; }
    }
}
