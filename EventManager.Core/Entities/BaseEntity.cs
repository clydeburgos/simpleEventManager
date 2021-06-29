using System;

namespace EventManager.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
