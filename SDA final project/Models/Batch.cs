namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Batch")]
    public partial class Batch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Batch()
        {
            ShoeSizeColor_CustomerOrder = new HashSet<ShoeSizeColor_CustomerOrder>();
        }

        [Key]
        public int batch_Id { get; set; }

        public int? shoe_Id { get; set; }

        public int? cost { get; set; }

        [Column(TypeName = "date")]
        public DateTime? batchDate { get; set; }

        public int? vendorOrder_Id { get; set; }

        public virtual Shoe Shoe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoeSizeColor_CustomerOrder> ShoeSizeColor_CustomerOrder { get; set; }

        public virtual VendorOrder VendorOrder { get; set; }
    }
}
