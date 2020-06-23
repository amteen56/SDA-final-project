namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Shoe_Size
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shoe_Size()
        {
            Shoe_Size_Color = new HashSet<Shoe_Size_Color>();
        }

        [Key]
        public int shoeSize_Id { get; set; }

        public int? shoe_Id { get; set; }

        public int? size_Id { get; set; }

        public virtual Shoe Shoe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shoe_Size_Color> Shoe_Size_Color { get; set; }

        public virtual Size Size { get; set; }
    }
}
