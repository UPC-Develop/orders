using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orders.APIBusiness.DBContext.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.APIBusiness.DBContext.FaultException;
using DBEntity;

namespace Orders.APIBusiness.API.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    public class Product_OrderController : Controller
    {

        protected readonly IProduct_OrderRepository _Product_OrderRepository;

        public Product_OrderController(IProduct_OrderRepository Product_OrderRepository)
        {
            _Product_OrderRepository = Product_OrderRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpGet]
        [Route("list-history-orders")]
        public ActionResult List_History_Orders(Int32 customer_id, int active)
        {
            var ret = new BaseResponse();

            try
            {

                if (customer_id != 0 && active != 0)
                {
                    ret = _Product_OrderRepository.List_History_Orders(customer_id, active);
                }
                else {
                    throw new OrderBadRequestParamsException("Los parámetros enviados no son los correctos.");
                }

            }
            catch (OrderBadRequestParamsException ex) {
                ret.errorCode = "00001";
                ret.errorMessage = ex.Message.ToString();
                ret.isSuccess = false;
                ret.data = null;
            }
            catch (Exception ex) {
                throw new Exception();
            }

            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="campus_id"></param>
        /// <param name="product_id"></param>
        /// <param name="booking_date"></param>
        /// <param name="status"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpGet]
        [Route("list-booking-orders")]
        public ActionResult List_booking_Orders(Int32 campus_id, Int32 product_id, DateTime booking_date, String status, int active)
        {
            var ret = new BaseResponse();

            try
            {

                if (booking_date != null && !String.IsNullOrEmpty(status) && active != 0)
                {
                    ret = _Product_OrderRepository.List_Booking_Orders(campus_id, product_id, booking_date, status, active);
                }
                else
                {
                    throw new OrderBadRequestParamsException("Los parámetros enviados no son los correctos.");
                }

            }
            catch (OrderBadRequestParamsException ex)
            {
                ret.errorCode = "00001";
                ret.errorMessage = ex.Message.ToString();
                ret.isSuccess = false;
                ret.data = null;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            return Json(ret);
        }

    }
}
