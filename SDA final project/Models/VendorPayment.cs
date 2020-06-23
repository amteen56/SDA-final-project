namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VendorPayment")]
    public partial class VendorPayment
    {
        [Key]
        public int vendorPayment_Id { get; set; }

        public int? vendorOrder_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? paymentDate { get; set; }

        public int? amount { get; set; }

        public string deleteStatus { get; set; }

        public string deleteRemarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime? deleteDate { get; set; }

        public virtual VendorOrder VendorOrder { get; set; }
    }
}
