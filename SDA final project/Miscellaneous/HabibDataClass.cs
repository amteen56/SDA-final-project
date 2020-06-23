using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDA_final_project.Models;

namespace SDA_final_project.Miscellaneous
{
    public class HabibDataClass
    {
        //use this code for every....
        //Habib object =  HabibDataClass.Habib;
        public static Habib instance;

        public static Habib Habib{
            get {
                if(instance == null)
                {
                    instance = new Habib();
                }
                return instance;
            }
        }
    }
}