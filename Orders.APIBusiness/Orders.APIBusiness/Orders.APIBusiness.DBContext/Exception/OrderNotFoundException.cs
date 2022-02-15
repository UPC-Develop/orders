using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.APIBusiness.DBContext.FaultException
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string message)
        : base(message)
        {
        }
    }
}