using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
   public class FeeModel
    {
       public FeeModel(int fee)
       {
           this.Amount = fee;
       }

       public int Amount { get; set; }
    }
}
