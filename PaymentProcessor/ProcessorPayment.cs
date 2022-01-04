using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor
{
    public class ProcessorPayment : IProcessorPayment
    {
        public bool PaymentProcessor()
        {
            return true;
        }
    }
}
