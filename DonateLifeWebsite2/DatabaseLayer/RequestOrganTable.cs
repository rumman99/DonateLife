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
    
    public partial class RequestOrganTable
    {
        public int RequestOrganID { get; set; }
        public System.DateTime RequestDate { get; set; }
        public int RequestByOrganID { get; set; }
        public int AcceptedOrganID { get; set; }
        public int RequiredOrganID { get; set; }
        public int RequestTypeOrganID { get; set; }
        public int AcceptedTypeOrganID { get; set; }
        public int RequestStatusOrganID { get; set; }
        public System.DateTime ExpectedDate { get; set; }
        public string RequestDetails { get; set; }
    
        public virtual AcceptedTypeOrganTable AcceptedTypeOrganTable { get; set; }
        public virtual RequestStatusOrganTable RequestStatusOrganTable { get; set; }
        public virtual RequestTypeOrganTable RequestTypeOrganTable { get; set; }
    }
}