using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
    public class MerchantAccount
    {
        public Boolean IsSubMerchant;
        public String Id;
        public MerchantAccountBusinessDetails BusinessDetails;
        public MerchantAccountFundingDetails FundingDetails;
        public MerchantAccountIndividualDetails IndividualDetails;
        public MerchantAccount MasterMerchantAccount;
        public MerchantAccountStatus Status;
    }
}
