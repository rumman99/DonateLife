//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class BloodBankStockDetailTable
    {
        public int BloodBankStockDetailID { get; set; }
        public int BloodBankStockID { get; set; }
        public int BloodGroupID { get; set; }
        public double Quantity { get; set; }
        public int BloodDonorID { get; set; }
        public System.DateTime BloodDonateDateTime { get; set; }
        public int CampaignID { get; set; }
    
        public virtual BloodDonorTable BloodDonorTable { get; set; }
        public virtual BloodGroupsTable BloodGroupsTable { get; set; }
        public virtual CampaignTable CampaignTable { get; set; }
        public virtual BloodBankStockTable BloodBankStockTable { get; set; }
    }
}
