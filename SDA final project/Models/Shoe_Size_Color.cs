namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Drawing;

    public partial class Shoe_Size_Color
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shoe_Size_Color()
        {
            Outlet_ShoeSizeColor = new HashSet<Outlet_ShoeSizeColor>();
            ShoeSizeColor_CustomerOrder = new HashSet<ShoeSizeColor_CustomerOrder>();
            Transfer_ShoeSizeColor = new HashSet<Transfer_ShoeSizeColor>();
            VendorOrder_ShoeSizeColor = new HashSet<VendorOrder_ShoeSizeColor>();
        }

        [Key]
        public int shoeSizeColor_Id { get; set; }

        public int? shoeSize_Id { get; set; }

        public int? color_Id { get; set; }

        public int? quantityInWH { get; set; }

        public virtual Color Color { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Outlet_ShoeSizeColor> Outlet_ShoeSizeColor { get; set; }

        public virtual Shoe_Size Shoe_Size { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoeSizeColor_CustomerOrder> ShoeSizeColor_CustomerOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transfer_ShoeSizeColor> Transfer_ShoeSizeColor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorOrder_ShoeSizeColor> VendorOrder_ShoeSizeColor { get; set; }
    }
}
