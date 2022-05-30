﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Procurement.Models
{
    public class VendorPreferenceModel
    {
        public string AGPO_Certificate_No { get; set; }
        public string Document_No { get; set; }
        public string Vendor_No { get; set; }
        public string Vendor_Category { get; set; }
        public string Certifying_Agency { get; set; }
        public string Products_Service_Category { get; set; }
        public string Certificate_Expiry_Date { get; set; }
        public string Certificate_Effective_Date { get; set; }
        public string End_Date { get; set; }
        public string Registered_Special_Group { get; set; }
    }
}