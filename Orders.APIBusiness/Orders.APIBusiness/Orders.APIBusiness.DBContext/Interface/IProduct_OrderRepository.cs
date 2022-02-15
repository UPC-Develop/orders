using System;
using System.Collections.Generic;
using System.Text;
using DBEntity;

namespace Orders.APIBusiness.DBContext.Interface
{
    public interface IProduct_OrderRepository
    {
        public BaseResponse List_History_Orders(Int32 customer_id, int active);
        public BaseResponse List_Booking_Orders(DateTime booking_date, String status , int active);
    }
}
