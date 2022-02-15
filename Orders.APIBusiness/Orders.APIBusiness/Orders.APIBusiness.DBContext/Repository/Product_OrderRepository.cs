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
        public BaseResponse List_Booking_Orders(DateTime booking_date, string status, int active)
        {
            var productOrderList = new List<Product_OrderEntity>();
            var returnEntity = new BaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = @"usp_list_booking_orders";

                    var p = new DynamicParameters();

                    p.Add(name: "@booking_date", value: booking_date, dbType: DbType.Date, direction: ParameterDirection.Input);
                    p.Add(name: "@status", value: status, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@active", value: active, dbType: DbType.Int16, direction: ParameterDirection.Input);

                    productOrderList = db.Query<Product_OrderEntity>(sql: sql, param: p, commandType: CommandType.StoredProcedure).ToList();


                    if (productOrderList.Count > 0)
                    {

                        foreach (var item in productOrderList)
                        {

                            for (int i = item.start_hour.Hour; i <= item.end_hour.Hour; i++)
                            {
                                Product_OrderEntity productOrder = new Product_OrderEntity();
                                productOrder.booking_date = item.booking_date;
                                //productOrder.start_hour = 


                            }
                            //item.start_hour.Hour
                        }



                        returnEntity.isSuccess = true;
                        returnEntity.errorCode = string.Empty;
                        returnEntity.errorMessage = string.Empty;
                        returnEntity.data = productOrderList;
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
    }
}
