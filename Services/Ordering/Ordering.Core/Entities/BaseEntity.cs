﻿namespace Ordering.Core.Entities
{
    public abstract class BaseEntity
    {
        //Protected set is made to use in the derived classes
        public int Id { get; protected set; }
        //Below Properties are Audit properties
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
