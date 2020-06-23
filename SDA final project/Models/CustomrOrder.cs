namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomrOrder")]
    public partial class CustomrOrder
    {
        [Key]
        public int customOrder_Id { get; set; }

        public string customerName { get; set; }

        public string customerContactNo { get; set; }

        public string articleNo { get; set; }

        public string colorName { get; set; }

        public string description { get; set; }

        public int? size { get; set; }

        public int? advance { get; set; }

        public int? balance { get; set; }

        [Column(TypeName = "date")]
        public DateTime deliveryDate { get; set; }

        public string outletName { get; set; }

        public string orderStatus { get; set; }
    }
}
