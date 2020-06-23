namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RemainingPayment")]
    public partial class RemainingPayment
    {
        [Key]
        public int remaining_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dueDate { get; set; }

        public int? vendorOrder_Id { get; set; }

        public int? remainingAmount { get; set; }

        public virtual VendorOrder VendorOrder { get; set; }
    }
}
