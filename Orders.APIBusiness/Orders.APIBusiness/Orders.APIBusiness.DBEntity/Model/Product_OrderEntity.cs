using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.APIBusiness.DBEntity.Model
{
    public class Product_OrderEntity
    {
        public Int32 order_id { get; set; }
        public String code { get; set; }
        public DateTime order_date { get; set; }
        public DateTime booking_date { get; set; }
        public String status { get; set; }
        public String campus_description { get; set; }
        public String product_category_description { get; set; }
        public String product_description { get; set; }
        public Int32 quantity { get; set; }
        public Decimal price { get; set; }
        public Decimal sub_total { get; set; }
        public Decimal discount { get; set; }
        public Decimal total_tax { get; set; }
        public Decimal total { get; set; }
        public String currency_type { get; set; }
        public DateTime start_hour { get; set; }
        public DateTime end_hour { get; set; }
    }
}
