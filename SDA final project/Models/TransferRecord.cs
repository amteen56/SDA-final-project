namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransferRecord")]
    public partial class TransferRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransferRecord()
        {
            Transfer_ShoeSizeColor = new HashSet<Transfer_ShoeSizeColor>();
        }

        [Key]
        public int transfer_Id { get; set; }

        public int? outlet_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateOfTransfer { get; set; }

        public int? shoesTransfered { get; set; }

        public virtual Outlet Outlet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transfer_ShoeSizeColor> Transfer_ShoeSizeColor { get; set; }
    }
}
