using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
   public class CardNonceModel
    {
        public CardNonceModel(String _cardNumber, String _ccv, String _postalCode, String _expirationDate)
        {
            this.cardNumber = _cardNumber;
            this.ccv = _ccv;
            this.postalCode = _postalCode;
            this.expirationDate = _expirationDate;
        }
        public String cardNumber { get; set; }
        public String ccv { get; set; }
        public String postalCode { get; set; }
        public String expirationDate { get; set; }
    }
}
