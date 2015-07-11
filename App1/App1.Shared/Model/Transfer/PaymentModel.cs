using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
   public class PaymentModel
    {
        public PaymentModel(String nonce)
        {
            paymentMethod = nonce;
        }

       public String paymentMethod { get; set; }
    }
}
