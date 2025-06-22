using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Domain.Entities.Payment
{
    public class Payment
    {
        public int IdPayment { get; set; }
        public int IdOrder { get; set; }
        public double Amount { get; set; }
        public string? Method { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
