using System;
using System.Collections.Generic;
using System.Text;
using Orders.APIBusiness.DBContext.Interface;
using DBEntity;
using Orders.APIBusiness.DBEntity.Model;
using Dapper;
using System.Data;
using DBContext;
using System.Linq;
using Orders.APIBusiness.DBContext.FaultException;
namespace Orders.APIBusiness.DBContext.Repository
{
    public class Product_OrderRepository : BaseRepository, IProduct_OrderRepository
    {
        public BaseResponse List_Booking_Orders(Int32 campus_id, Int32 product_id,  DateTime booking_date, string status, int active)
        {
            var productOrderList = new List<Product_OrderEntity>();
            var newProductOrderList = new List<Product_OrderEntity>();
           
            var returnEntity = new BaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_list_booking_orders";

                    var p = new DynamicParameters();

                    p.Add(name: "@campus_id", value: campus_id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add(name: "@product_id", value: product_id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add(name: "@booking_date", value: booking_date, dbType: DbType.Date, direction: ParameterDirection.Input);
                    p.Add(name: "@status", value: status, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@active", value: active, dbType: DbType.Int16, direction: ParameterDirection.Input);

                    productOrderList = db.Query<Product_OrderEntity>(sql: sql, param: p, commandType: CommandType.StoredProcedure).ToList();


                    

                    if (productOrderList.Count > 0)
                    {
                        newProductOrderList = GetBookingAll(booking_date, productOrderList);
                        returnEntity.isSuccess = true;
                        returnEntity.errorCode = string.Empty;
                        returnEntity.errorMessage = string.Empty;
                        returnEntity.data = newProductOrderList;
                    }
                    else
                    {
                        throw new OrderNotFoundException("No se encuentran ordenes.");
                    }
                }
            }
            catch (OrderNotFoundException ex)
            {
                returnEntity.isSuccess = false;
                returnEntity.errorCode = "0002";
                returnEntity.errorMessage = ex.Message;
                returnEntity.data = null;
            }

            catch (Exception ex)
            {
                returnEntity.isSuccess = false;
                returnEntity.errorCode = "0003";
                returnEntity.errorMessage = ex.Message;
                returnEntity.data = null;
            }

            return returnEntity;
        }

        public BaseResponse List_History_Orders(Int32 customer_id, int active)
        {

            var productOrderEntity = new List<Product_OrderEntity>();
            var returnEntity = new BaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_List_History_Order";

                    var p = new DynamicParameters();

                    p.Add(name: "@customer_id", value: customer_id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add(name: "@active", value: active, dbType: DbType.Int16, direction: ParameterDirection.Input);

                    productOrderEntity = db.Query<Product_OrderEntity>(sql: sql, param: p, commandType: CommandType.StoredProcedure).ToList();

        
                    if (productOrderEntity.Count > 0)
                    {
                        returnEntity.isSuccess = true;
                        returnEntity.errorCode = string.Empty;
                        returnEntity.errorMessage = string.Empty;
                        returnEntity.data = productOrderEntity;
                    }
                    else
                    {
                        throw new OrderNotFoundException("No se encuentran ordenes para el cliente.");
                    }
                }
            }
            catch (OrderNotFoundException ex)
            {
                returnEntity.isSuccess = false;
                returnEntity.errorCode = "0002";
                returnEntity.errorMessage = ex.Message;
                returnEntity.data = null;
            }

            catch (Exception ex)
            {
                returnEntity.isSuccess = false;
                returnEntity.errorCode = "0003";
                returnEntity.errorMessage = ex.Message;
                returnEntity.data = null;
            }

            return returnEntity;

        }

        private List<Product_OrderEntity> GetBookingAll(DateTime booking_date, List<Product_OrderEntity> productOrderList) {

            var allProductOrderList = new List<Product_OrderEntity>();
            var newProductOrderList = new List<Product_OrderEntity>();

            DateTime bookingDateStartHour = booking_date.AddHours(9);

            for (int i = 0; i < 9; i++)
            {

                Product_OrderEntity productOrder = new Product_OrderEntity();

                productOrder.booking_date = booking_date;
                productOrder.start_hour = bookingDateStartHour.AddHours(i);
                productOrder.end_hour = productOrder.start_hour.AddHours(1);
                productOrder.bflag = "n";
                allProductOrderList.Add(productOrder);
            }

            foreach (var item in productOrderList)
            {
                int j = 1;
                for (int i = item.start_hour.Hour; i < item.end_hour.Hour; i++)
                {

                    Product_OrderEntity productOrder = new Product_OrderEntity();
                    productOrder.booking_date = item.booking_date;
                    productOrder.start_hour = item.start_hour.AddHours(j - 1);
                    productOrder.end_hour = item.start_hour.AddHours(j);
                    newProductOrderList.Add(productOrder);
                    j = j + 1;
                }
            }

            foreach (var i in newProductOrderList)
            {

                foreach (var j in allProductOrderList)
                {
                    if (j.start_hour.Equals(i.start_hour) && j.end_hour.Equals(i.end_hour))
                    {
                        j.bflag = "y";
                    }
                }
            } 

            return allProductOrderList;
        }

    }
}
