using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.APIBusiness.DBContext.FaultException
{
    public class OrderBadRequestParamsException : Exception
    {
        public OrderBadRequestParamsException(string message)
        : base(message)
        {

        }
    }
}