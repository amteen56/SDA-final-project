using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SDA_final_project.Models;

namespace SDA_final_project.View_Modals
{
    public class VendorOrderViewModel
    { 
        public int vovm_Id { get; set; }

        public VendorOrder vendorOrder { get; set; }

        public List<Shoe> shoes { get; set; }

        public List<Size> sizes { get; set; }

        public List<Color> colors { get; set; }

        public List<Shoe> shoesList { get; set; }

        public List<Color> colorsList { get; set; }

        public List<Size> sizesList { get; set; }

        public List<Vendor> vendorsList { get; set; }

        public List<string> shoesToAdd { get; set; }

        public List<string> shoesCost { get; set; }

        public List<string> sizesToAdd { get; set; }

        public List<string> varieties { get; set; }

        public List<string> qty { get; set; }

        public List<RemainingPayment> remainings { get; set; }
        
    }
}