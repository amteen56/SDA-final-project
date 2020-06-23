namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VendorOrder")]
    public partial class VendorOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VendorOrder()
        {
            Batches = new HashSet<Batch>();
            RemainingPayments = new HashSet<RemainingPayment>();
            VendorOrder_ShoeSizeColor = new HashSet<VendorOrder_ShoeSizeColor>();
            VendorPayments = new HashSet<VendorPayment>();
        }

        [Key]
        public int vendorOrder_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateOfOrder { get; set; }

        public int? totalAmount { get; set; }

        public int? amountRemaining { get; set; }

        public int? totalQty { get; set; }

        public int? vendor_Id { get; set; }

        public string paymentStatus { get; set; }

        public string paymentMode { get; set; }

        public string deleteStatus { get; set; }

        public string deleteRemarks { get; set; }

        [Column(TypeName = "date")]
        public DateTime? deleteDate { get; set; }

        public string paymentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Batch> Batches { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemainingPayment> RemainingPayments { get; set; }

        public virtual Vendor Vendor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorOrder_ShoeSizeColor> VendorOrder_ShoeSizeColor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorPayment> VendorPayments { get; set; }
    }
}
