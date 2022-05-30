﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Procurement.Models
{
    public class ShareholderModel
    {
        public string No { get; set; }
        public string Document_Type { get; set; }
        public int Entry_No { get; set; }
        public string Vendor_No { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string City { get; set; }
        public string Phone_No { get; set; }
        public string Nationality_ID { get; set; }
        public string Citizenship_Type { get; set; }
        public string ID_Passport_No { get; set; }
        public decimal? Entity_Ownership { get; set; }
        public string Share_Types { get; set; }
        public string No_of_Shares { get; set; }
        public string Nominal_Value_Share { get; set; }
        public string Total_Nominal_Value { get; set; }
        public string Ownership_Effective_Date { get; set; }
        public string Country_Region_Code { get; set; }
        public string Post_Code { get; set; }
        public string County { get; set; }
        public string E_Mail { get; set; }
        public int shareholdersDetails { get; set; }
        public string registrationNumber { get; set; }
        public string kraPin { get; set; }
        public string entityType { get; set; }
        public string Company_Type { get; set; }
    }
}