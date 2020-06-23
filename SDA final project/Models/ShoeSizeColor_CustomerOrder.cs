namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShoeSizeColor_CustomerOrder
    {
        [Key]
        public int shoeSizeColorCustomerOrder_Id { get; set; }

        public int? price { get; set; }

        public int? quantity { get; set; }

        public int? customerOrder_Id { get; set; }

        public int? shoeSizeColor_Id { get; set; }

        public int? batch_Id { get; set; }

        public virtual Batch Batch { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; }

        public virtual Shoe_Size_Color Shoe_Size_Color { get; set; }
    }
}
