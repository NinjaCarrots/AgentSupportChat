using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportChat.Core.Domain
{
    public abstract class BaseEntity
    {
        [Required]
        public bool Active { get; set; }
        [Required]
        public bool Deleted { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
