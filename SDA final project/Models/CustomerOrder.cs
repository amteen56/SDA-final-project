namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerOrder")]
    public partial class CustomerOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerOrder()
        {
            ShoeSizeColor_CustomerOrder = new HashSet<ShoeSizeColor_CustomerOrder>();
        }

        [Key]
        public int customerOrder_Id { get; set; }

        public DateTime dateOfOrder { get; set; }

        public int? totalAmount { get; set; }

        public int? discount { get; set; }

        public int? finalAmount { get; set; }

        public string customerName { get; set; }

        public string orderRemarks { get; set; }

        public string deleteStatus { get; set; }

        [Required]
        public string deleteRemarks { get; set; }

        public DateTime? deleteDate { get; set; }

        public int? employee_Id { get; set; }

        public string outletName { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoeSizeColor_CustomerOrder> ShoeSizeColor_CustomerOrder { get; set; }
    }
}
