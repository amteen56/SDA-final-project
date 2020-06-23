using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDA_final_project.View_Modals;
using SDA_final_project.Models;

namespace SDA_final_project.Miscellaneous
{
    public class vendorOrderDataHandler
    {
        Habib habib;
        public vendorOrderDataHandler()
        {
             habib = HabibDataClass.Habib;
        }

        public void addNewShoes(List<string> shoes,List<string> shoesCost,List<string> variety,DateTime date)
        {
            int count = 0;
            int i = 0;
            foreach(string shoe in shoes)
            {
                //checked
                count = habib.Shoes.Where(c => c.shoeArticle == shoe).Count();
                if(count == 0)
                {
                    Shoe obj = new Shoe()
                    {
                        shoeArticle = shoe,
                        shoeCost = Convert.ToInt32(shoesCost[i]),
                        shoeVariety = variety[i],
                        UpdateDate = date
                    };

                    habib.Shoes.Add(obj);
                }
                else
                {
                    //checked
                    Shoe obj = habib.Shoes.Where(c => c.shoeArticle == shoe).First();
                    obj.UpdateDate = date;
                    obj.shoeCost = Convert.ToInt32(shoesCost[i]);
                }

                //addNewBatch(obj.shoe_Id,Convert.ToInt32(obj.shoeCost),obj.UpdateDate);

            }
            habib.SaveChanges();
        }

        public void addNewSizes(List<string> sizes)
        {
            int count = 0;
            foreach (string size in sizes)
            {
                int sizeParam = int.Parse(size);
                //checked
                count = habib.Sizes.Where(c => c.sizeNo == sizeParam).Count();

                if(count == 0)
                {
                    Size obj = new Size()
                    {
                        sizeNo = Convert.ToInt32(size)
                    };

                    habib.Sizes.Add(obj);
                }
            }

            habib.SaveChanges();
        }

        public void addNewColors(List<Color> colors)
        {
            int count = 0;
            foreach(Color color in colors)
            {
                //checked
                count = habib.Colors.Where(x => x.colorName == color.colorName).Count();

                if(count == 0)
                {
                    habib.Colors.Add(color);
                }
            }

            habib.SaveChanges();
        }

        public void addShoeSize(List<Shoe> shoes, List<Size> sizes)
        {
            for(int i = 0; i < shoes.Count; i++)
            {
                var shoeToFind = shoes[i].shoeArticle;
                var sizeToFind = sizes[i].sizeNo;

                //checked
                int count = habib.Shoe_Size.Where(c => c.Shoe.shoeArticle.Equals(shoeToFind) && c.Size.sizeNo == sizeToFind ).Count();

                if(count == 0)
                {
                    shoeToFind = shoes[i].shoeArticle;
                    sizeToFind = sizes[i].sizeNo;
                    Shoe_Size obj = new Shoe_Size()
                    {
                        //checked
                        shoe_Id = habib.Shoes.Where(c => c.shoeArticle == shoeToFind).First().shoe_Id,
                        size_Id = habib.Sizes.Where(c => c.sizeNo == sizeToFind).First().size_Id
                    };

                    habib.Shoe_Size.Add(obj);
                }
            }
            habib.SaveChanges();
        }

        public void addShoeSizeColor(List<Shoe> shoes, List<Size> sizes, List<Color> colors, List<string> qty)
        {
            for (int i = 0; i < shoes.Count; i++)
            {
                var shoeToFind = shoes[i].shoeArticle;
                var sizeToFind = sizes[i].sizeNo;
                //checked
                int id = habib.Shoe_Size.Where(c => c.Shoe.shoeArticle == shoeToFind  && c.Size.sizeNo == sizeToFind).First().shoeSize_Id;

                var colorToFind = colors[i].colorName;

                //checked
                int count = habib.Shoe_Size_Color.Where(c => c.shoeSize_Id == id && c.color_Id == habib.Colors.Where(x => x.colorName == colorToFind).FirstOrDefault().color_Id).Count();

                if (count == 0)
                {
                    Shoe_Size_Color obj = new Shoe_Size_Color()
                    {
                        shoeSize_Id = id,
                        //checked
                        color_Id = habib.Colors.Where(x => x.colorName == colorToFind).First().color_Id,
                        quantityInWH = Convert.ToInt32(qty[i])
                    };
                    habib.Shoe_Size_Color.Add(obj);
                }
                else
                {
                    //checked
                    Shoe_Size_Color obj = habib.Shoe_Size_Color.Where(c => c.shoeSize_Id == id && c.color_Id == habib.Colors.Where(x => x.colorName == colorToFind).FirstOrDefault().color_Id).First();

                    obj.quantityInWH += Convert.ToInt32(qty[i]);
                }
            }
            habib.SaveChanges();
        }

        public void addVendorOrderListDetails(int vendorOrderId, List<Shoe> shoes,List<Size> sizes, List<Color> colors, List<string> qty)
        {

            for(int i = 0; i < shoes.Count; i++)
            {
                var shoeToFind = shoes[i].shoeArticle;
                var sizeToFind = sizes[i].sizeNo;
                var colorToFind = colors[i].colorName;

                //checked
                int id = habib.Shoe_Size_Color.Where(c =>
                c.Shoe_Size.Shoe.shoeArticle == shoeToFind && c.Shoe_Size.Size.sizeNo == sizeToFind && c.Color.colorName == colorToFind
                ).First().shoeSizeColor_Id;

                //checked
                int costOfShoe = Convert.ToInt32(habib.Shoes.Where(c => c.shoeArticle == shoeToFind).First().shoeCost);

                VendorOrder_ShoeSizeColor obj = new VendorOrder_ShoeSizeColor()
                {
                    vendorOrder_Id = vendorOrderId,
                    cost = costOfShoe,
                    shoeSizeColor_Id = id,
                    quantity = Convert.ToInt32(qty[i])
                };

                habib.VendorOrder_ShoeSizeColor.Add(obj);
            }

            habib.SaveChanges();
            
        }

        public void addPayment(int orderId, int total, DateTime date)
        {
            VendorPayment payment = new VendorPayment() {
                vendorOrder_Id = orderId,
                amount = total,
                paymentDate = date,
                deleteStatus = "N"
            };


            habib.VendorPayments.Add(payment);
            habib.SaveChanges();
        }

        public void addPartialPayments(int vendorOrd_ID, List<RemainingPayment> remains)
        {
            foreach(RemainingPayment pay in remains)
            {
                RemainingPayment obj = new RemainingPayment()
                {
                    dueDate = pay.dueDate,
                    remainingAmount = pay.remainingAmount,
                    vendorOrder_Id = vendorOrd_ID
                };

                habib.RemainingPayments.Add(obj);
            }
            habib.SaveChanges();
        }

        public void addNewBatch(int shoeId, int cost, DateTime date, int vendorOrd_ID)
        {
            Batch batch = new Batch()
            {
                shoe_Id = shoeId,
                cost = cost,
                batchDate = date,
                vendorOrder_Id = vendorOrd_ID
            };

            habib.Batches.Add(batch);

            habib.SaveChanges();
        }

        public int getTotalQuantity(List<string> qty)
        {
            int total = 0;
            foreach(string a in qty)
            {
                total += Convert.ToInt32(a);
            }

            return total;
        }
    }
 
}