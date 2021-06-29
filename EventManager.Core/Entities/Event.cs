using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventManager.Domain.Entities
{
    public class Event : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Identifier { get; set; }
        [Required]
        public string EventName { get; set; }
        [Required]
        public string EventDescription { get; set; }
        public string EventTimezone { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
