namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Outlet")]
    public partial class Outlet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Outlet()
        {
            Outlet_ShoeSizeColor = new HashSet<Outlet_ShoeSizeColor>();
            TransferRecords = new HashSet<TransferRecord>();
        }

        [Key]
        public int outlet_Id { get; set; }

        public string outletName { get; set; }

        public string outletAddress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Outlet_ShoeSizeColor> Outlet_ShoeSizeColor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferRecord> TransferRecords { get; set; }
    }
}
