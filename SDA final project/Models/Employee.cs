namespace SDA_final_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            CustomerOrders = new HashSet<CustomerOrder>();
        }

        [Key]
        public int employee_Id { get; set; }

        public string employeeName { get; set; }

        public string employeeContactNo { get; set; }

        public string employeeAddress { get; set; }

        public int? salary { get; set; }

        public string dateOfHiring { get; set; }

        public string typeOfEmployee { get; set; }

        public byte DeleteStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
    }
}
