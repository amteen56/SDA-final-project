namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vendor")]
    public partial class Vendor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendor()
        {
            VendorOrders = new HashSet<VendorOrder>();
        }

        [Key]
        public int vendor_Id { get; set; }

        [Required]
        public string vendorName { get; set; }

        public string vendorCompany { get; set; }

        public string vendorContactNo { get; set; }

        public string vendorAddress { get; set; }

        public byte DeleteStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorOrder> VendorOrders { get; set; }
    }
}
