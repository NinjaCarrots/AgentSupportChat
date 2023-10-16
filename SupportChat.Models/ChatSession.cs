using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportChat.Models
{
    public class ChatSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }
        [Required]
        public string? User { get; set; }
        public bool isActive { get; set; }
        [Required]
        public int AgentId { get; set; }
        public List<ChatMessage> Messages { get; set; }
        public bool IsActive { get; set; }
        public bool IsRefused { get; set; }
        public bool IsOverflow { get; set; }
    }
}
