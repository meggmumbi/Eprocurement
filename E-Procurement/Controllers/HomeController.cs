using E_Procurement.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.HtmlControls;

namespace E_Procurement.Controllers
{
    public class 
        
        HomeController : Controller
    {
        public ClientContext SPClientContext { get; set; }

        public Web SPWeb { get; set; }

        public string SPErrorMsg { get; set; }

        public ActionResult Index()
        {

            List<TenderModel> list = new List<TenderModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var today = DateTime.Today;
                var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status == "Published" && x.Procurement_Method == "Open Tender" /*&& x.Submission_End_Date >= today*/).ToList();
                foreach (var tenders in query)
                {
                    TenderModel tender = new TenderModel();
                    tender.Code = tenders.Code;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Solicitation_Type = tenders.Solicitation_Type;
                    tender.External_Document_No = tenders.External_Document_No;
                    tender.Procurement_Type = tenders.Procurement_Type;
                    tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                    tender.Project_ID = tenders.Project_ID;
                    tender.Tender_Name = tenders.Tender_Name;
                    tender.Tender_Summary = tenders.Tender_Summary;
                    tender.Description = tenders.Description;
                    tender.Document_Date = tenders.Document_Date;
                    tender.Status = tenders.Status;
                    tender.Name = tenders.Name;
                    tender.Submission_End_Date = tenders.Submission_End_Date;
                    tender.Submission_Start_Date = tenders.Submission_Start_Date;
                    tender.Published = tenders.Published;
                    list.Add(tender);
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(list);
        }        
         public ActionResult InvitationforPrequalifications()
        {

            List<IFPRequestsModel> list = new List<IFPRequestsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var today = DateTime.Today;
                var query = nav.InvitationfForPrequalification.Where(x => x.Published == true && x.RFI_Document_Status == "Published" && x.Status == "Released" &&x.Document_Type== "Invitation For Prequalification"&&x.Submission_End_Date>=today && x.Type == "Sub IFP").ToList();
                foreach (var tenders in query)
                {
                    IFPRequestsModel tender = new IFPRequestsModel();
                    tender.Code = tenders.Code;
                    tender.Status = tenders.RFI_Document_Status;
                    tender.Tender_Summary = tenders.Tender_Summary;
                    tender.External_Document_No = tenders.External_Document_No;
                    tender.Procurement_Type = tenders.Procurement_Type;
                    tender.Description = tenders.Description;
                    tender.Name = tenders.Name;
                    tender.Submission_Start_Date = Convert.ToString(tenders.Submission_Start_Date);
                    tender.Submission_Start_Time = tenders.Submission_Start_Time;
                    tender.Description = tenders.Description;
                    tender.Document_Date = Convert.ToString(tenders.Document_Date);
                    tender.Status = tenders.Status;
                    tender.Name = tenders.Name;
                    tender.Published = Convert.ToBoolean(tenders.Published);
                    list.Add(tender);
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(list);
        }
        private static List<TenderModel> GetActiveTenderDetail()
        {
            List<TenderModel> list = new List<TenderModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var today = DateTime.Today;
                var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status=="Published" &&x.Procurement_Method== "Open Tender" /*&&x.Submission_End_Date>=today*/).ToList();
                foreach (var tenders in query)
                {
                    TenderModel tender = new TenderModel();
                    tender.Code = tenders.Code;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Solicitation_Type = tenders.Solicitation_Type;
                    tender.External_Document_No = tenders.External_Document_No;
                    tender.Procurement_Type = tenders.Procurement_Type;
                    tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                    tender.Project_ID = tenders.Project_ID;
                    tender.Tender_Name = tenders.Tender_Name;
                    tender.Tender_Summary = tenders.Tender_Summary;
                    tender.Description = tenders.Description;
                    tender.Document_Date = tenders.Document_Date;
                    tender.Status = tenders.Status;
                    tender.Name = tenders.Name;
                    tender.Submission_End_Date = tenders.Submission_End_Date;
                    tender.Submission_Start_Date = tenders.Submission_Start_Date;
                    tender.Published = tenders.Published;
                    list.Add(tender);
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return list;
        }
        [HttpGet]
        public ActionResult ViewActiveTender(string tendernumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<ViewActiveTenderModel> list = new List<ViewActiveTenderModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var today = DateTime.Today;
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status=="Published" && x.Code == tendernumber &&x.Procurement_Method== "Open Tender" &&x.Submission_End_Date>=today).ToList();
                    foreach (var tenders in query)
                    {
                        ViewActiveTenderModel tender = new ViewActiveTenderModel();
                        tender.Code = tenders.Code;
                        tender.Procurement_Method = tenders.Procurement_Method;
                        tender.Solicitation_Type = tenders.Solicitation_Type;
                        tender.External_Document_No = tenders.External_Document_No;
                        tender.Procurement_Type = tenders.Procurement_Type;
                        tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                        tender.Project_ID = tenders.Project_ID;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Description = tenders.Description;
                        tender.Target_Bidder_Group = tenders.Target_Bidder_Group;
                        tender.Tender_Summary = tenders.Tender_Summary;
                        tender.Document_Date = Convert.ToString(tenders.Document_Date);
                        tender.Status = tenders.Status;
                        tender.Name = tenders.Name;
                        tender.Submission_End_Date = tenders.Submission_End_Date;
                        tender.Submission_Start_Date = tenders.Submission_Start_Date;
                        tender.Published = tenders.Published;
                        list.Add(tender);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                return View(list);
            }
        }
     
        public ActionResult ViewEprequalifications(string tendernumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                dynamic model = new ExpandoObject();
                model.SingleTenders = GetSingleTenderDetails(tendernumber);
                model.ViewTenderAddendums = GetSingleTenderAddendum(tendernumber);
                model.TenderDocument = GetRequiredTenderDocuments(tendernumber);
                model.UploadedDocument = PopulateTenderDocumentsfromSpTable(tendernumber);
                return View(model);
            }
        }
        public ActionResult NavigationMenu() {
            return View();
        }
       
        public ActionResult NavigationFooter()
        {
            return View();
        }
        [HandleError]
        public ActionResult ActiveRFQs()
        {
            if (Session["vendorNo"] == null)
            {
                RedirectToAction("Login", "Home");
            }

            List<ActiveRfqModel> list = new List<ActiveRfqModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                string vendorNo = Session["vendorNo"].ToString();
                var today = DateTime.Today;
                var query = nav.InvitationforRFQ.Where(x => x.Published == true && x.Vendor_No == vendorNo && (x.Document_Status == "Published" || x.Document_Status == "Cancelled") && x.Status == "Released" && x.Procurement_Method == "RFQ" && x.Submission_End_Date >= today).ToList();

                foreach (var tenders in query)
                {
                    ActiveRfqModel tender = new ActiveRfqModel();
                    tender.Code = tenders.Code;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Solicitation_Type = tenders.Solicitation_Type;
                    tender.External_Document_No = tenders.External_Document_No;
                    tender.Procurement_Type = tenders.Procurement_Type;
                    tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                    tender.Project_ID = tenders.Project_ID;
                    tender.Tender_Name = tenders.Tender_Name;
                    tender.Tender_Summary = tenders.Tender_Summary;
                    tender.Description = tenders.Description;
                    tender.Document_Date = tenders.Document_Date;
                    tender.Status = tenders.Status;
                    tender.Name = tenders.Name;
                    tender.Submission_End_Date = tenders.Submission_End_Date;
                    tender.Submission_Start_Date = tenders.Submission_Start_Date;
                    tender.Published = tenders.Published;
                    list.Add(tender);
                }
                   
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(list);
        }
        [HandleError]
        public ActionResult AppliedQuotations()
        {
            if (Session["vendorNo"] == null)
            {
                RedirectToAction("Login", "Home");
            }

            List<ActiveRfqModel> list = new List<ActiveRfqModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                string vendorNo = Session["vendorNo"].ToString();
                var today = DateTime.Today;
                var query = nav.InvitationforRFQ.Where(x => x.Published == true && x.Vendor_No == vendorNo && x.Document_Status == "Published" && x.Status == "Released" && x.Procurement_Method == "RFQ" &&x.Submission_End_Date>=today).ToList();
                foreach (var tenders in query)
                {
                    ActiveRfqModel tender = new ActiveRfqModel();
                    tender.Code = tenders.Code;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Solicitation_Type = tenders.Solicitation_Type;
                    tender.External_Document_No = tenders.External_Document_No;
                    tender.Procurement_Type = tenders.Procurement_Type;
                    tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                    tender.Project_ID = tenders.Project_ID;
                    tender.Tender_Name = tenders.Tender_Name;
                    tender.Tender_Summary = tenders.Tender_Summary;
                    tender.Description = tenders.Description;
                    tender.Document_Date = tenders.Document_Date;
                    tender.Status = tenders.Status;
                    tender.Name = tenders.Name;
                    tender.Submission_End_Date = tenders.Submission_End_Date;
                    tender.Submission_Start_Date = tenders.Submission_Start_Date;
                    tender.Published = tenders.Published;
                    list.Add(tender);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(list);
        }
        public ActionResult ViewSingleTender(string tendernumber)
        {
                dynamic model = new ExpandoObject();
            model.SingleTenders = GetSingleTenderDetails(tendernumber);
            model.ViewTenderAddendums = GetSingleTenderAddendum(tendernumber);
            model.TenderPurchaseLines = GetTenderPurchaseLines(tendernumber);
            model.TenderSecurity = GetBidSecurity(tendernumber);
            model.TenderEvaluationSummery = GetTenderEvaluationSummery(tendernumber);
            model.TenderDocument = GetRequiredTenderDocuments(tendernumber);
            model.TenderskeystaffDetails = GetKeyStaffTenderPersonnel(tendernumber);
            model.RequredDocuemnts = GetIFSRequiredEquipments(tendernumber);
            model.TenderEvaluationCreteria = GetTenderEvaluationCreteria(tendernumber);
            model.UploadedDocument = PopulateTenderDocumentsfromSpTable(tendernumber);
            return View(model);
            
            
        }
        public ActionResult DocumentsSourcesList()
        {

            List<DropdownListsModel> documentssources = new List<DropdownListsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.TenderDocumentsSource.ToList();
                foreach (var documents in query)
                {
                    DropdownListsModel document = new DropdownListsModel();
                    document.Code = documents.Code;
                    document.Description = documents.Description;
                    documentssources.Add(document);
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(documentssources);
        }
        public ActionResult YearCodesList()
        {
            List<DropdownListsModel> yearcodes = new List<DropdownListsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.CalenderYearCodesList.ToList();
                foreach (var years in query)
                {
                    DropdownListsModel year = new DropdownListsModel();
                    year.Code = years.Code;
                    year.Description = years.Description;
                    yearcodes.Add(year);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(yearcodes);
        }
        public ActionResult EquipmentCategoriesLists()
        {
            List<EquipmentCategories> equipmentcategories = new List<EquipmentCategories>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var Categories = nav.WorkEquipmentCategories.ToList();
                foreach (var equptypes in Categories)
                {
                    EquipmentCategories equptype = new EquipmentCategories();
                    equptype.Code = equptypes.Code;
                    equptype.Description = equptypes.Description;
                    equipmentcategories.Add(equptype);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(equipmentcategories);
        }
        public ActionResult ProjectRole()
        {
            List<ProjectRole> projectRole = new List<ProjectRole>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var projectRoles = nav.ProjectRoleCode.Where(r=>r.Blocked==false).ToList();
                foreach (var projectRol in projectRoles)
                {
                    ProjectRole equptype = new ProjectRole();
                    equptype.projectroleCode = projectRol.Project_Role_Code;
                    equptype.description = projectRol.Title_Designation_Description;
                    projectRole.Add(equptype);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(projectRole);
        }

        public ActionResult ViewRFISpecificRequirement(string CategoryID)
        {
            List<FRISpecificRequirementModel> requirements = new List<FRISpecificRequirementModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var Categories = nav.RFICategoryRequirement.Where(x=>x.Category_ID== CategoryID).ToList();
                foreach (var categorylist in Categories)
                {
                    FRISpecificRequirementModel category1 = new FRISpecificRequirementModel();
                    category1.Category_ID = categorylist.Category_ID;
                    category1.Description = categorylist.Description;
                    category1.Requirement_Type = categorylist.Requirement_Type;
                    requirements.Add(category1);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(requirements);
        }

        public ActionResult PrequalifiedresponsibilityCenters(string documentNo, string category)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                var nav = NavConnection.ReturnNav();
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                dynamic model = new ExpandoObject();
                model.responsibility = responsibilitycenterdetails(documentNo, category,vendorNo);
                model.category = viewcatefories(documentNo,vendorNo);
                ViewBag.documentNo = documentNo;
                ViewBag.category = category;
                //var Categories = nav.IFPResponcRC.Where(x =>x.Document_No== documentNo && x.Procurement_Category== category && x.Vendor_No== vendorNo && x.Document_Type== "IFP Response").ToList();
                // foreach (var categorylist in Categories)
                // {
                //     RFIResponceResponsibilityCenter rc = new RFIResponceResponsibilityCenter();
                //     rc.DocumentNo = categorylist.Document_No;
                //     rc.Category_ID = categorylist.Procurement_Category;
                //     rc.ResponsibilityCenterCode = categorylist.Responsibility_Center_Code;
                //     rc.constituencyCode = categorylist.Constituency_Code;
                //     rc.Description = categorylist.Description;
                //     requirements.Add(rc);
                // }


                return View(model);
            }
            //catch (Exception e)
            //{

            //    throw;
            //}
           
        }

        private static List<RFIResponceResponsibilityCenter> responsibilitycenterdetails(string documentNo, string category,string vendorNo)
        {
            List<RFIResponceResponsibilityCenter> list = new List<RFIResponceResponsibilityCenter>();
            try
            {
                var nav = NavConnection.ReturnNav();
                //string vendorNo = Session["vendorNo"].ToString();
                var Categories = nav.IFPResponcRC.Where(x => x.Document_No == documentNo && x.Procurement_Category == category && x.Vendor_No == vendorNo && x.Document_Type == "IFP Response").ToList();
                foreach (var categorylist in Categories)
                {
                    RFIResponceResponsibilityCenter response = new RFIResponceResponsibilityCenter();
                    response.DocumentNo = categorylist.Document_No;
                    response.Category_ID = categorylist.Procurement_Category;
                    response.ResponsibilityCenterCode = categorylist.Responsibility_Center_Code;
                    response.constituencyCode = categorylist.Constituency_Code;
                    response.Description = categorylist.Description;

                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<CategoryResponce> viewcatefories(string documentNo, string vendorNo)
        {
            List<CategoryResponce> requirements = new List<CategoryResponce>();
            try
            {
                var nav = NavConnection.ReturnNav();
                //var vendorNo = Convert.ToString(Session["vendorNo"]);
                var Categories = nav.IFPResponseLine.Where(x => x.Vendor_No == vendorNo && x.Document_No == documentNo).ToList();
                foreach (var categorylist in Categories)
                {
                    CategoryResponce category1 = new CategoryResponce();
                    category1.categoryId = categorylist.Procurement_Category;
                    category1.categoryDescription = categorylist.Category_Description;
                    category1.documentNo = categorylist.Document_No;


                    requirements.Add(category1);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return requirements;
        }
        public ActionResult ViewSelectedCategories(string prequalificationNo, string InvitationNumber)
        {
            List<CategoryResponce> requirements = new List<CategoryResponce>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var vendorNo = Convert.ToString(Session["vendorNo"]);
               // string inviteNo = Request.QueryString["InvitationNumber"];
                ViewBag.invitationNo = InvitationNumber;
                ViewBag.prequalificationNo = prequalificationNo;
                var Categories = nav.IFPResponseLine.Where(x => x.Vendor_No == vendorNo && x.Document_No == prequalificationNo).ToList();
                foreach (var categorylist in Categories)
                {
                    CategoryResponce category1 = new CategoryResponce();
                    category1.categoryId = categorylist.Procurement_Category;
                    category1.categoryDescription = categorylist.Category_Description;
                    category1.documentNo = categorylist.Document_No;
                    category1.region = categorylist.Description;
                    category1.constituency = categorylist.Constituency;
                    category1.rfiNumber = categorylist.RFI_Document_No;


                    requirements.Add(category1);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(requirements);
        }

        public ActionResult viewselectedCategoriesRegistration(string prequalificationNo, string InvitationNumber)
        {
            List<CategoryResponce> requirements = new List<CategoryResponce>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                // string inviteNo = Request.QueryString["InvitationNumber"];
                ViewBag.invitationNo = InvitationNumber;
                ViewBag.prequalificationNo = prequalificationNo;
                var Categories = nav.IFPResponseLine.Where(x => x.Vendor_No == vendorNo && x.Document_No == prequalificationNo).ToList();
                foreach (var categorylist in Categories)
                {
                    CategoryResponce category1 = new CategoryResponce();
                    category1.categoryId = categorylist.Procurement_Category;
                    category1.categoryDescription = categorylist.Category_Description;
                    category1.documentNo = categorylist.Document_No;
                    category1.region = categorylist.Description;
                    category1.constituency = categorylist.Constituency;
                    category1.rfiNumber = categorylist.RFI_Document_No;


                    requirements.Add(category1);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(requirements);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prequalificationNo"></param>
        /// <param name="InvitationNumber"></param>
        /// <returns></returns>
        public ActionResult ViewSelectedRegistrationCategories(string prequalificationNo, string InvitationNumber)
        {
            List<CategoryResponce> requirements = new List<CategoryResponce>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                // string inviteNo = Request.QueryString["InvitationNumber"];
                ViewBag.invitationNo = InvitationNumber;
                ViewBag.prequalificationNo = prequalificationNo;
                var Categories = nav.IFPResponseLine.Where(x => x.Vendor_No == vendorNo && x.Document_No == prequalificationNo).ToList();
                foreach (var categorylist in Categories)
                {
                    CategoryResponce category1 = new CategoryResponce();
                    category1.categoryId = categorylist.Procurement_Category;
                    category1.categoryDescription = categorylist.Category_Description;
                    category1.documentNo = categorylist.Document_No;
                    category1.region = categorylist.Description;
                    category1.constituency = categorylist.Constituency;
                    category1.rfiNumber = categorylist.RFI_Document_No;


                    requirements.Add(category1);
                }


            }
            catch (Exception e)
            {


                throw;
            }
            return View(requirements);
        }
        public ActionResult PrequalificationAttachedDocuments(string InvitationNumber,string prequalificationNo)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                ViewBag.prequalificationNo = prequalificationNo;
                ViewBag.invitationNo = InvitationNumber;
                model.Response = ResponseDetails(InvitationNumber, vendorNo);
                model.RequiredDocuments = RequiredDocumentsDetails(InvitationNumber, vendorNo);
                model.SpecificRequiredDocuments = SpecificRequiredDocuments(InvitationNumber, vendorNo);
                // model.PrequalificationUploadedDocuments = PrequalificationUploaded(prequalificationNo, vendorNo);
                model.UploadedDocument = AttachedPrequalificationDocuments(prequalificationNo);
                //model.PrequalificationUploadedDocuments = AttachedPrequalificationDocuments(prequalificationNo);

                return View(model);
            }
        }
        public ActionResult RegistrationAttachDocuments(string InvitationNumber, string prequalificationNo)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();               
                ViewBag.prequalificationNo = prequalificationNo;
                ViewBag.invitationNo = InvitationNumber;
                model.Response = ResponseDetails(InvitationNumber, vendorNo);
                model.RequiredDocuments = RegistrationRequiredDocumentsDetails(InvitationNumber, vendorNo);
                model.SpecificRequiredDocuments = RegistrationSpecificRequiredDocuments(InvitationNumber, vendorNo);
                model.PrequalificationUploadedDocuments = PrequalificationUploaded(prequalificationNo, vendorNo);

                return View(model);
            }
        }

        public ActionResult supplierDocuments()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.UploadedDocuments = AlreadyRegisteredDocumentsDetails(vendorNo);
                model.RequiredDocuments = RegistrationRequiredDocumentsDetails(vendorNo);

                return View(model);
            }
        }

        public ActionResult TenderRFQAttachDocument(string tenderNo, string Response)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                ViewBag.prequalificationNo = tenderNo;
                ViewBag.invitationNo = Response;
                model.RequiredDocuments = GetRequiredTenderDocuments(tenderNo);
                model.RequredDocuemnts = GetIFSRequiredEquipments(tenderNo);
                model.Response = RegistrationResponseDetails(tenderNo, vendorNo);
                model.BidDetails = GetBidResponseDetails(tenderNo, vendorNo);
                ViewBag.TenderNo = Response;
                model.AttachedBiddDocuments = GetBidAttachedDocumentsDetails(tenderNo, vendorNo);


                return View(model);
            }
        }

        public ActionResult TenderAttachDocument(string tenderNo, string Response)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                ViewBag.prequalificationNo = tenderNo;
                ViewBag.invitationNo = Response;
                model.RequiredDocuments = GetRequiredTenderDocuments(tenderNo);
                model.RequredDocuemnts = GetIFSRequiredEquipments(tenderNo);
                model.Response = RegistrationResponseDetails(tenderNo, vendorNo);
                model.BidDetails = GetBidResponseDetails(tenderNo, vendorNo);
                ViewBag.TenderNo = Response;
                model.AttachedBiddDocuments = GetBidAttachedDocumentsDetails(tenderNo, vendorNo);
                return View(model);
            }
        }
        public ActionResult RgistrationAttachedDocuments(string InvitationNumber, string prequalificationNo)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();               
                ViewBag.prequalificationNo = prequalificationNo;
                ViewBag.invitationNo = InvitationNumber;
                model.Response = ResponseDetails(InvitationNumber, vendorNo);
                model.RequiredDocuments = RegistrationRequiredDocumentsDetails(InvitationNumber);
                model.SpecificRequiredDocuments = SpecificRequiredDocuments(InvitationNumber, vendorNo);
                model.PrequalificationUploadedDocuments = PrequalificationUploaded(prequalificationNo, vendorNo);

                return View(model);
            }
        }

        public ActionResult PastExperienceCountries()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<CountriesModel> list = new List<CountriesModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.Countries.ToList();
                    foreach (var countries in query)
                    {
                        CountriesModel country = new CountriesModel();
                        country.Code = countries.Code;
                        country.Name = countries.Name;
                        list.Add(country);
                    }
                }
                catch (Exception e)
                {

                    throw;
                }

                return View(list);
            }

        }
        
        public ActionResult EditBalanceSheetYearCodeList()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<DropdownListsModel> yearcodes = new List<DropdownListsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.CalenderYearCodesList.ToList();
                    foreach (var years in query)
                    {
                        DropdownListsModel year = new DropdownListsModel();
                        year.Code = years.Code;
                        year.Description = years.Description;
                        yearcodes.Add(year);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(yearcodes);
            }
        }
        public ActionResult EditIncomeYearCodeList()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<DropdownListsModel> yearcodes = new List<DropdownListsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.CalenderYearCodesList.ToList();
                    foreach (var years in query)
                    {
                        DropdownListsModel year = new DropdownListsModel();
                        year.Code = years.Code;
                        year.Description = years.Description;
                        yearcodes.Add(year);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(yearcodes);
            }
        }
        public ActionResult IncomeYearCodes()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<DropdownListsModel> yearcodes = new List<DropdownListsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.CalenderYearCodesList.ToList();
                    foreach (var years in query)
                    {
                        DropdownListsModel year = new DropdownListsModel();
                        year.Code = years.Code;
                        year.Description = years.Description;
                        yearcodes.Add(year);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(yearcodes);
            }
        }
        public ActionResult IncomeYearCodesList()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<DropdownListsModel> yearcodes = new List<DropdownListsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.CalenderYearCodesList.ToList();
                    foreach (var years in query)
                    {
                        DropdownListsModel year = new DropdownListsModel();
                        year.Code = years.Code;
                        year.Description = years.Description;
                        yearcodes.Add(year);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(yearcodes);
            }
        }
        public ActionResult MyTransactions()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.BidDetails = GetBidResponseDetails(vendorNo);
                model.PurchaseOders = GetPurchaseOrders(vendorNo);
                model.PurchaseContracts = GetPurchaseContracts(vendorNo);
                return View(model);
            }

        }
        private static List<PurchaseContracts> GetPurchaseContracts(string vendorNo)
        {
            List<PurchaseContracts> list = new List<PurchaseContracts>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.PurchaseHeader.Where(x => x.Document_Type == "Blanket Order" && x.Pay_to_Vendor_No == vendorNo).ToList();
                    foreach (var contracts in query)
                    {
                        PurchaseContracts contract = new PurchaseContracts();
                        contract.No = contracts.No;
                        contract.DocumentDate = Convert.ToString(contracts.Document_Date);
                        contract.Purchaseordertype = contracts.Order_types;
                        contract.Region = contracts.Region;
                        contract.Description = contracts.Description;
                        contract.Amount_including_vat = Convert.ToString(contracts.Amount_Including_VAT);
                        contract.Amount = Convert.ToString(contracts.Amount);
                        contract.Contract_Description = contracts.Contract_Description;
                        contract.currency_code = contracts.Currency_Code;
                        contract.Contract_Start_Date = Convert.ToString(contracts.Contract_Start_Date);
                        contract.Contract_End_Date = Convert.ToString(contracts.Contract_End_Date);
                        contract.Awarded_Tender_Sum = Convert.ToString(contracts.Award_Tender_Sum_Inc_Taxes);
                        contract.Location_Code = contracts.Location_Code;
                        contract.Tender_Id = contracts.Invitation_For_Supply_No;
                        contract.Conttract_type = contracts.Contract_Type;
                        contract.currency_code = contracts.Currency_Code;
                        contract.currency_code = contracts.Currency_Code;
                        list.Add(contract);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return list;
            
        }
        private static List<BidResponseDetailsModel> GetBidResponseDetails(string vendorNo)
        {
           
                List<BidResponseDetailsModel> list = new List<BidResponseDetailsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.PurchaseHeader.Where(x => x.Document_Type == "Quote" && x.Vendor_No == vendorNo).ToList();
                    foreach (var bids in query)
                    {
                        BidResponseDetailsModel bid = new BidResponseDetailsModel();
                        bid.No = bids.No;
                        bid.Document_Date = Convert.ToString(bids.Document_Date);
                        bid.Bidder_type = bids.Bidder_Type;
                        bid.Invitation_For_Supply_No = bids.Invitation_For_Supply_No;
                        bid.Tender_Description = bids.Tender_Description;
                        bid.Tender_Name = bids.Tender_Name;
                        bid.Location_Code = bids.Location_Code;
                        bid.Currency_Code = bids.Currency_Code;
                        bid.Amount = Convert.ToString(bids.Amount);
                        bid.Amount_Including_VAT = Convert.ToString(bids.Amount_Including_VAT);
                        bid.Document_Status = bids.Status;
                        list.Add(bid);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return list;
            
        }
        private static List<PurchaseOrders> GetPurchaseOrders(string vendorNo)
        {

            List<PurchaseOrders> list = new List<PurchaseOrders>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.PurchaseHeader.Where(x => x.Document_Type == "Order" && x.Pay_to_Vendor_No == vendorNo).ToList();
                foreach (var orders in query)
                {
                    PurchaseOrders order = new PurchaseOrders();
                    order.No = orders.No;
                    order.DocumentDate = Convert.ToString(orders.Document_Date);
                    order.Purchaseordertype = orders.Order_types;
                    order.Region = orders.Region;
                    order.Description = orders.Description;
                    order.Amount_including_vat = Convert.ToString(orders.Amount_Including_VAT);
                    order.Amount = Convert.ToString(orders.Amount);
                    order.currency_code = orders.Currency_Code;
                    list.Add(order);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        public JsonResult SubmitTenderResponse(string tendernumber)
        {
            try
            {
                
                var nav = new NavConnection().ObjNav();
                var vendorNo = Session["vendorNo"].ToString();
                var nav1 = NavConnection.ReturnNav();
               
                var status = nav.FnSubmitTenderResponse(vendorNo, tendernumber);
               
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        Session["BideResponseNumber"] = nav.FngetBidResponseNumber(tendernumber, vendorNo);
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    case "found":
                        Session["BideResponseNumber"] = nav.FngetBidResponseNumber(tendernumber, vendorNo);
                        return Json("found*" + res[1], JsonRequestBehavior.AllowGet);
                    case "profileincomplete":
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SubmitRfiResponse(RfiResponseTModel rfimodel)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();

                if (rfimodel.constituency == null)
                {
                    rfimodel.constituency = "";
                }
                if (rfimodel.registrationPeriod == null)
                {
                    rfimodel.registrationPeriod = "";
                }



                var nav = new NavConnection().ObjNav();


                var status = nav.FnPrequalificationResponseDetails(vendorNo, rfimodel.RfiDocumentNo, rfimodel.RepFullName, rfimodel.RepDesignation, rfimodel.RfiDocApplicationNo,rfimodel.Region,rfimodel.constituency,rfimodel.registrationPeriod);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult InsertResponseLines(RfiResponseTModel postData)
        {
            try
            {
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                int i = 0;


                string results_0 = (dynamic)null;
                string results_1 = (dynamic)null;
                if (postData.constituency == null)
                {
                    postData.constituency = "";
                }

                List<string> AllSelectedCategoriesLists = postData.ProcurementCategory.ToList();
                //Loop and insert records.
                foreach (var iteminlist in AllSelectedCategoriesLists)
                {
                    var selectedcategory = iteminlist;
                    var nav = new NavConnection().ObjNav();
                    var status = nav.FnInsertRFIResponseLines(postData.DocumentNo, selectedcategory, postData.RfiDocumentNo, vendorNo,postData.Region,postData.constituency,1);
                    var res = status.Split('*');
                    results_0 = res[0];
                    results_1 = res[1];
                }
                switch (results_0)
                {
                    case "success":
                        return Json("success*" + results_1, JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + results_1, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InsertIFRResponseLines(RfiResponseTModel postData)
        {
            try
            {
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                int i = 0;


                string results_0 = (dynamic)null;
                string results_1 = (dynamic)null;
                if (postData.constituency == null)
                {
                    postData.constituency = "";
                }

                List<string> AllSelectedCategoriesLists = postData.ProcurementCategory.ToList();
                //Loop and insert records.
                foreach (var iteminlist in AllSelectedCategoriesLists)
                {
                    var selectedcategory = iteminlist;
                    var nav = new NavConnection().ObjNav();
                    var status = nav.FnInsertIFRResponseLines(postData.DocumentNo, selectedcategory, postData.RfiDocumentNo, vendorNo, postData.Region, postData.constituency, 1);
                    var res = status.Split('*');
                    results_0 = res[0];
                    results_1 = res[1];
                }
                switch (results_0)
                {
                    case "success":
                        return Json("success*" + results_1, JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + results_1, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult submitResponibilityCenters(RFIResponceResponsibilityCenter responsibilitycenter)
        {
            try
            {
                var vendorNo = Convert.ToString(Session["vendorNo"].ToString());
                var nav = new NavConnection().ObjNav();

                if (responsibilitycenter.constituencyCode == null)
                {
                    responsibilitycenter.constituencyCode = "";
                }

                //string DocNo = HttpContext.Request.QueryString["documentNo"];
               // string categoryId = HttpContext.Request.QueryString["category"];


                var status = nav.FnInsertRFIResponseResponsibilityCenter(responsibilitycenter.DocumentNo, responsibilitycenter.Category_ID,vendorNo, responsibilitycenter.ResponsibilityCenterCode,responsibilitycenter.constituencyCode);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult ApplyforPreQualifc(PrequalifiedCategories postData)
        {
            var results = (dynamic)null;
            try
            {
                List<string> AllSelectedCategoriesLists = postData.AllSelectedCategories.ToList();

                foreach (var iteminlist in AllSelectedCategoriesLists)
                {

                    var selectedcategory = iteminlist;
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = new NavConnection().ObjNav();
                    var status = nav.FnApplyPreQualification(vendorNo, selectedcategory);
                    string[] info = status.Split('*');
                    results = info[0];
                    switch (info[0])
                    {
                        case "success":
                            return Json("success*" + info[1], JsonRequestBehavior.AllowGet);

                        default:
                            return Json("danger*" + info[1], JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
            return results;
        }
        public ActionResult ViewPurchaseContract(string tendernumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.SingleTenders = GetSingleTenderDetails(tendernumber);
                model.TenderAddendums = GetSingleTenderAddendum(tendernumber);
                model.TenderPurchaseLines = GetTenderPurchaseLines(tendernumber);
                model.TenderSecurity = GetBidSecurity(tendernumber);
                model.TenderDocument = GetRequiredTenderDocuments(tendernumber);
                model.TenderskeystaffDetails = GetKeyStaffTenderPersonnel(tendernumber);
                model.RequredDocuemnts = GetIFSRequiredEquipments(tendernumber);
                model.TenderEvaluationCreteria = GetTenderEvaluationCreteria(tendernumber);
                return View(model);
            }

        }
        public ActionResult ViewPurchaseOrders(string tendernumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.SingleTenders = GetSingleTenderDetails(tendernumber);
                model.TenderAddendums = GetSingleTenderAddendum(tendernumber);
                model.TenderPurchaseLines = GetTenderPurchaseLines(tendernumber);
                model.TenderSecurity = GetBidSecurity(tendernumber);
                model.TenderDocument = GetRequiredTenderDocuments(tendernumber);
                model.TenderskeystaffDetails = GetKeyStaffTenderPersonnel(tendernumber);
                model.RequredDocuemnts = GetIFSRequiredEquipments(tendernumber);
                model.TenderEvaluationCreteria = GetTenderEvaluationCreteria(tendernumber);
                return View(model);
            }

        }
        public ActionResult ViewExpressionInterest(string tendernumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.SingleTenders = GetSingleTenderDetails(tendernumber);
                model.TenderAddendums = GetSingleTenderAddendum(tendernumber);
                model.TenderPurchaseLines = GetTenderPurchaseLines(tendernumber);
                model.TenderSecurity = GetBidSecurity(tendernumber);
                model.TenderDocument = GetRequiredTenderDocuments(tendernumber);
                model.TenderskeystaffDetails = GetKeyStaffTenderPersonnel(tendernumber);
                model.RequredDocuemnts = GetIFSRequiredEquipments(tendernumber);
                model.TenderEvaluationCreteria = GetTenderEvaluationCreteria(tendernumber);
                return View(model);
            }

        }
        public ActionResult ViewAwadedContracts(string tendernumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.SingleTenders = GetSingleTenderDetails(tendernumber);
                model.TenderAddendums = GetSingleTenderAddendum(tendernumber);
                model.TenderPurchaseLines = GetTenderPurchaseLines(tendernumber);
                model.TenderSecurity = GetBidSecurity(tendernumber);
                model.TenderDocument = GetRequiredTenderDocuments(tendernumber);
                model.TenderskeystaffDetails = GetKeyStaffTenderPersonnel(tendernumber);
                model.RequredDocuemnts = GetIFSRequiredEquipments(tendernumber);
                model.TenderEvaluationCreteria = GetTenderEvaluationCreteria(tendernumber);
                return View(model);
            }

        }
        public ActionResult TenderResponseForm(string tendernumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.SingleTenders = GetSingleTenderDetails(tendernumber);
                model.TenderAddendums = GetSingleTenderAddendum(tendernumber);
                model.TenderPurchaseLines = GetTenderPurchaseLines(tendernumber);
                model.TenderSecurity = GetBidSecurity(tendernumber);
                model.TenderDocument = GetRequiredTenderDocuments(tendernumber);
                model.TenderskeystaffDetails = GetKeyStaffTenderPersonnel(tendernumber);
                model.RequredDocuemnts = GetIFSRequiredEquipments(tendernumber);
                model.RequredDocuemnts = GetIFSRequiredEquipments(tendernumber);
                model.TenderEvaluationCreteria = GetTenderEvaluationCreteria(tendernumber);
                model.TenderEvaluationSummery = GetTenderEvaluationSummery(tendernumber);
                model.UploadedDocument = PopulateTenderDocumentsfromSpTable(tendernumber);
                return View(model);
            }

        }

        [HandleError]
        public ActionResult RFQResponseForm(string tendernumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.SingleTenders = GetSingleRFQDetails(tendernumber);
                model.TenderPurchaseLines = GetTenderPurchaseLines(tendernumber);
                model.TenderAddendums = GetSingleTenderAddendum(tendernumber);
                model.TenderSecurity = GetBidSecurity(tendernumber);
                model.TenderDocument = GetRequiredTenderDocuments(tendernumber);
                model.RequiredDocuments = GetRequiredTenderDocuments(tendernumber);
                model.RequredDocuemnts = GetIFSRequiredEquipments(tendernumber);
                model.UploadedDocument = PopulateTenderDocumentsfromSpTable(tendernumber);
                return View(model);
            }

        }
        public JsonResult TenderFinancialResponse(List<Financials> finance)
        {
            
            var results = (dynamic)null;
            try
            {
                if (finance == null)
                {
                    finance = new List<Financials>();
                }
                foreach (Financials financedetail in finance)
                {

                    string BillNumber = financedetail.billNo;
                    decimal ResponseQuantity = 0;
                    if (financedetail.quantity > 0)
                    {
                        ResponseQuantity = financedetail.quantity;
                    }
                    decimal ResponsePrice = 0;
                    if (financedetail.price > 0)
                    {
                        ResponsePrice = financedetail.price;
                    }

                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = new NavConnection().ObjNav();
                    var status = nav.FnInsertPurchaseLinesResponseDetails(vendorNo, financedetail.documentNo, financedetail.billNo, financedetail.quantity, financedetail.price);
                    var res = status.Split('*');
                    results = res[0];
                }

            }
            catch (Exception ex)
            {
                results = ex.Message;
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult PrequalifiedAttachDocuments(List<prequalifiedDocuments> finance)
        //{
        //    var results = (dynamic)null;
        //    try
        //    {
        //        if (finance == null)
        //        {
        //            finance = new List<prequalifiedDocuments>();
        //        }
        //        foreach (prequalifiedDocuments financedetail in finance)
        //        {
                   

        //            var vendorNo = Convert.ToString(Session["vendorNo"]);
        //            var nav = new NavConnection().ObjNav();
        //            string storedFilename = "";

        //            int errCounter = 0, succCounter = 0;
        //            DateTime startdate, enddate;
        //            CultureInfo usCulture = new CultureInfo("es-ES");
        //            DateTime dtofIssue = DateTime.Now;
        //            DateTime expiryDate = DateTime.Now;
        //            if (financedetail.issueDate == null && financedetail.expirydate == null)
        //            {
        //                dtofIssue = DateTime.Parse(financedetail.issueDate, usCulture.DateTimeFormat);
        //                expiryDate = DateTime.Parse(financedetail.expirydate, usCulture.DateTimeFormat);
        //            }
                    

        //            if (financedetail.browsedDoc == null)
        //            {
        //                errCounter++;
        //                return Json("danger*browsedfilenull", JsonRequestBehavior.AllowGet);
        //            }

        //            if (vendorNo.Contains(":"))
        //                vendorNo = vendorNo.Replace(":", "[58]");
        //            vendorNo = vendorNo.Replace("/", "[47]");

        //            if (financedetail.procurementDocumentType.Contains("/"))
        //                financedetail.procurementDocumentType = financedetail.procurementDocumentType.Replace("/", "_");

        //            FileInfo fi = new FileInfo(financedetail.browsedDoc);
                 
        //            //string fileName0 = Path.GetFileName(financedetail.browsedDoc.FileName);
        //            //string ext0 = _getFileextension(financedetail.browsedDoc);
        //            string fileName0 = fi.Name;
        //            string ext0 = fi.Extension;
        //            string savedF0 = vendorNo + "_" + fileName0 + ext0;

        //            bool up2Sharepoint = _UploadSupplierPrequalificationsDocumentToSharepoint(financedetail.applicationNO, financedetail.browsedDoc, financedetail.procurementDocumentType);
        //            if (up2Sharepoint == true)
        //            {
        //                string filename = vendorNo + "_" + fileName0;
        //                string sUrl = ConfigurationManager.AppSettings["S_URL"];
        //                string defaultlibraryname = "Procurement%20Documents/";
        //                string customlibraryname = "Invitation For Prequalification";
        //                string sharepointLibrary = defaultlibraryname + customlibraryname;
        //                financedetail.applicationNO = financedetail.applicationNO.Replace('/', '_');
        //                financedetail.applicationNO = financedetail.applicationNO.Replace(':', '_');
        //                //Sharepoint File Link
        //                string sharepointlink = sUrl + sharepointLibrary + "/" + financedetail.applicationNO + "/" + filename;
        //                string fsavestatus = nav.FnInsertPrequalificatinDocuments(vendorNo, financedetail.procurementDocumentType, "", financedetail.certificateNo, dtofIssue, expiryDate, filename, financedetail.applicationNO, sharepointlink);
        //                var splitanswer = fsavestatus.Split('*');
        //                results = splitanswer[0];
        //                //switch (splitanswer[0])
        //                //{
        //                //    case "success":
        //                //        return Json("success*" + succCounter, JsonRequestBehavior.AllowGet);
        //                //    default:
        //                //        return Json("danger*" + succCounter, JsonRequestBehavior.AllowGet);
        //                //}
        //            }
        //            else
        //            {
        //                return Json("sharepointError*", JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        results = ex.Message;
        //    }
        //    return Json(results, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult FnUploadBidResponseDocumentsTender(List<prequalifiedDocuments> finance)
        {
            var results = (dynamic)null;
            try
            {
                if (finance == null)
                {
                    finance = new List<prequalifiedDocuments>();
                }
                foreach (prequalifiedDocuments financedetail in finance)
                {


                    var vendorNo = Convert.ToString(Session["vendorNo"]);
                    var nav = new NavConnection().ObjNav();
                    string storedFilename = "";

                    int errCounter = 0, succCounter = 0;
                    DateTime startdate, enddate;
                    CultureInfo usCulture = new CultureInfo("es-ES");
                    DateTime dtofIssue = DateTime.Now;
                    DateTime expiryDate = DateTime.Now;
                    if (financedetail.issueDate == null && financedetail.expirydate == null)
                    {
                        dtofIssue = DateTime.Parse(financedetail.issueDate, usCulture.DateTimeFormat);
                        expiryDate = DateTime.Parse(financedetail.expirydate, usCulture.DateTimeFormat);
                    }


                    if (financedetail.browsedDoc == null)
                    {
                        errCounter++;
                        return Json("danger*browsedfilenull", JsonRequestBehavior.AllowGet);
                    }

                    if (vendorNo.Contains(":"))
                        vendorNo = vendorNo.Replace(":", "[58]");
                    vendorNo = vendorNo.Replace("/", "[47]");

                    if (financedetail.procurementDocumentType.Contains("/"))
                        financedetail.procurementDocumentType = financedetail.procurementDocumentType.Replace("/", "_");

                    FileInfo fi = new FileInfo(financedetail.browsedDoc);

                    //string fileName0 = Path.GetFileName(financedetail.browsedDoc.FileName);
                    //string ext0 = _getFileextension(financedetail.browsedDoc);
                    string fileName0 = fi.Name;
                    string ext0 = fi.Extension;
                    string savedF0 = vendorNo + "_" + fileName0 + ext0;

                    bool up2Sharepoint = _UploadSupplierTenderDocumentToSharepoint(financedetail.applicationNO, financedetail.browsedDoc, financedetail.procurementDocumentType);
                    if (up2Sharepoint == true)
                    {
                        string filename = vendorNo + "_" + fileName0;
                        string sUrl = ConfigurationManager.AppSettings["S_URL"];
                        string defaultlibraryname = "Procurement%20Documents/";
                        string customlibraryname = "Tender Bid Reponses";
                        string sharepointLibrary = defaultlibraryname + customlibraryname;
                        financedetail.applicationNO = financedetail.applicationNO.Replace('/', '_');
                        financedetail.applicationNO = financedetail.applicationNO.Replace(':', '_');
                        //Sharepoint File Link
                        string sharepointlink = sUrl + sharepointLibrary + "/" + financedetail.applicationNO + "/" + filename;

                        if (financedetail.certificateNo == null)
                        {
                            financedetail.certificateNo = "";
                        }

                        //                    string fsavestatus = nav.FnInsertBidReponseDocuments(vendorNo, typauploadselect, filedescription, certificatenumber, dtofIssue, expiryDate, filename, BidResponseNumber);

                        string fsavestatus = nav.FnInsertBidReponseDocuments(vendorNo, financedetail.procurementDocumentType, "", financedetail.certificateNo, dtofIssue, expiryDate, filename, financedetail.applicationNO, sharepointlink);
                        var splitanswer = fsavestatus.Split('*');
                        results = splitanswer[0];
                        //switch (splitanswer[0])
                        //{
                        //    case "success":
                        //        return Json("success*" + succCounter, JsonRequestBehavior.AllowGet);
                        //    default:
                        //        return Json("danger*" + succCounter, JsonRequestBehavior.AllowGet);
                        //}
                    }
                    else
                    {
                        return Json("sharepointError*", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RFQFinancialResponse(List<Financials> finance)
        {

            var results = (dynamic)null;
            try
            {
                if (finance == null)
                {
                    finance = new List<Financials>();
                }
                foreach (Financials financedetail in finance)
                {

                    string BillNumber = financedetail.billNo;
                    decimal ResponsePrice = 0;
                    if (financedetail.price > 0)
                    {
                        ResponsePrice = financedetail.price;
                    }
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = new NavConnection().ObjNav();
                    var status = nav.FnInsertPurchaseLinesResponseRFQDetails(vendorNo, financedetail.documentNo, financedetail.billNo,financedetail.price);
                    var res = status.Split('*');
                    results = res[0];
                }

            }
            catch (Exception ex)
            {
                results = ex.Message;
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        private static List<TenderEvalCriteriaModel> GetTenderEvaluationCreteria(string tendernumber)
        {

            List<TenderEvalCriteriaModel> list = new List<TenderEvalCriteriaModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var today = DateTime.Today;
                var tenderquery = nav.invitetoTenders.Where(x => x.Code == tendernumber && x.Published == true && x.Document_Status == "Published" &&x.Procurement_Method== "Open Tender").ToList();
                var BidTemplateID = "";
                foreach (var tenders in tenderquery)
                {
                   BidTemplateID = tenders.Bid_Scoring_Template;
                }
                if (BidTemplateID != null)
                {
                    var query = nav.bidscoringTemplate.Where(x => x.Code == BidTemplateID).ToList();
                    foreach (var bidtemplates in query)
                    {
                        TenderEvalCriteriaModel template = new TenderEvalCriteriaModel();
                        template.Code = bidtemplates.Code;
                        template.Template_type = bidtemplates.Template_type;
                        template.Document_No = bidtemplates.Document_No;
                        template.Description = bidtemplates.Description;                       
                        template.Default_Procurement_Type = bidtemplates.Default_Procurement_Type;
                        template.Total_Preliminary_Checks_Score = Convert.ToString(bidtemplates.Total_Preliminary_Checks_Score);
                        template.Total_Technical_Evaluation = Convert.ToString(bidtemplates.Total_Technical_Evaluation);
                        template.Total_Financial_Evaluation = Convert.ToString(bidtemplates.Total_Financial_Evaluation);
                        template.Total_Assigned_Score_Weight = Convert.ToString(bidtemplates.Total_Assigned_Score_Weight);
                        template.Default_YES_Bid_Rating_Score = Convert.ToString(bidtemplates.Default_YES_Bid_Rating_Score);
                        template.NO_Bid_Rating_Response_Value = bidtemplates.NO_Bid_Rating_Response_Value;
                        template.V1_POOR_Option_Text_Bid_Score = Convert.ToString(bidtemplates.V1_POOR_Option_Text_Bid_Score);
                        template.V3_GOOD_Option_Text_Bid_Score = Convert.ToString(bidtemplates.V3_GOOD_Option_Text_Bid_Score);
                        template.V2_FAIR_Option_Text_Bid_Score = Convert.ToString(bidtemplates.V2_FAIR_Option_Text_Bid_Score);
                        template.V4_VERY_GOOD_Text_Bid_Score = Convert.ToString(bidtemplates.V4_VERY_GOOD_Text_Bid_Score);
                        template.V5_EXCELLENT_Text_Bid_Score = Convert.ToString(bidtemplates.V5_EXCELLENT_Text_Bid_Score);
                        list.Add(template);
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<TenderEvalCriteriaModel> GetTenderEvaluationSummery(string tendernumber)
        {

            List<TenderEvalCriteriaModel> list = new List<TenderEvalCriteriaModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var today = DateTime.Today;
                var tenderquery = nav.invitetoTenders.Where(x => x.Code == tendernumber && x.Published == true && x.Document_Status == "Published" && x.Procurement_Method == "Open Tender").ToList();
                var BidTemplateID = "";
                foreach (var tenders in tenderquery)
                {
                    BidTemplateID = tenders.Bid_Scoring_Template;
                }
                if (BidTemplateID != null)
                {
                    var query = nav.bidscoreRequirement.Where(x => x.Template_ID == BidTemplateID).ToList();
                    foreach (var bidtemplates in query)
                    {
                        TenderEvalCriteriaModel template = new TenderEvalCriteriaModel();
                        template.Code = bidtemplates.Template_ID;
                        template.EvaluationType = bidtemplates.Evaluation_Type;
                        template.EvaluationRequirement = bidtemplates.Evaluation_Requirement;
                        template.requirementType = bidtemplates.Requirement_Type;
                        template.contractRefClause = bidtemplates.Contract_Ref_Clause;

                        list.Add(template);
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<IfsRequiredEquipmentsModel> GetIFSRequiredEquipments(string tendernumber)
        {
            List<IfsRequiredEquipmentsModel> list = new List<IfsRequiredEquipmentsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifsEquipspecs.Where(x => x.Document_No == tendernumber).ToList();
                foreach (var ifsdocuments in query)
                {
                    IfsRequiredEquipmentsModel document = new IfsRequiredEquipmentsModel();
                    document.Document_No = ifsdocuments.Document_No;
                    document.Equipment_Type_Code = ifsdocuments.Equipment_Type_Code;
                    document.Procurement_Document_Type_ID = Convert.ToString(ifsdocuments.Equipment_Type_Code);
                    document.Description = ifsdocuments.Description;
                    document.Category = ifsdocuments.Category;
                    document.Minimum_Required_Qty = Convert.ToString(ifsdocuments.Minimum_Required_Qty);
                    list.Add(document);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return list;
        }
        private static List<TenderKeyStaffModel> GetKeyStaffTenderPersonnel(string tendernumber)
        {
            List<TenderKeyStaffModel> list = new List<TenderKeyStaffModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifsKeyStaff.Where(x => x.IFS_Code == tendernumber).ToList();
                foreach (var tenderstaffs in query)
                {
                    TenderKeyStaffModel staff = new TenderKeyStaffModel();
                    staff.IFS_Code = tenderstaffs.IFS_Code;
                    staff.Staff_Role_Code = Convert.ToString(tenderstaffs.Staff_Role_Code);
                    staff.Title_Designation_Description = tenderstaffs.Title_Designation_Description;
                    staff.Min_No_of_Recomm_Staff = Convert.ToString(tenderstaffs.Min_No_of_Recomm_Staff);
                    staff.Requirement_Type = tenderstaffs.Requirement_Type;
                    staff.Staff_Category = tenderstaffs.Staff_Category;
                    list.Add(staff);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<IfsDocumentTModel> GetRequiredTenderDocuments(string tendernumber)
        {
            List<IfsDocumentTModel> list = new List<IfsDocumentTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifsReqDocuments.Where(x => x.Document_No == tendernumber).ToList();
                foreach (var documents in query)
                {
                    IfsDocumentTModel document = new IfsDocumentTModel();
                    document.Document_No = documents.Document_No;
                    document.Procurement_Document_Type_ID = Convert.ToString(documents.Procurement_Document_Type_ID);
                    document.Description = documents.Description;
                    document.Track_Certificate_Expiry = Convert.ToString(documents.Track_Certificate_Expiry);
                    document.instructions = documents.Guidelines_Instruction;
                    if (document.Track_Certificate_Expiry == "True")
                    {

                        document.Track_Certificate_Expiry = "Yes";
                    }
                    else
                    {
                        document.Track_Certificate_Expiry=  "No";
                    }
                    document.Requirement_Type = documents.Requirement_Type;
                    document.Special_Group_Requirement = Convert.ToString(documents.Special_Group_Requirement);
                    document.Specialized_Provider_Req = Convert.ToString(documents.Specialized_Provider_Req);
                    list.Add(document);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<PurchaseCodeLinesModel> GetTenderPurchaseLines(string tendernumber)
        {
            List<PurchaseCodeLinesModel> list = new List<PurchaseCodeLinesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.purchaseCodeLines.Where(x => x.Standard_Purchase_Code == tendernumber).ToList();
                foreach (var tenderitems in query)
                {
                    PurchaseCodeLinesModel items = new PurchaseCodeLinesModel();
                    items.Standard_Purchase_Code = tenderitems.Standard_Purchase_Code;
                    items.Line_No = Convert.ToString(tenderitems.Line_No);
                    items.Type = tenderitems.Type;
                    items.No = tenderitems.No;
                    items.Description = tenderitems.Description;
                    items.Quantity = Convert.ToString(tenderitems.Quantity);
                    items.Amount_Excl_VAT = Convert.ToString(tenderitems.Amount_Excl_VAT);
                    items.Unit_of_Measure_Code = tenderitems.Unit_of_Measure_Code;
                    items.Item_Category = tenderitems.Item_Category;
                    list.Add(items);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<BidSecurityModel> GetBidSecurity(string tendernumber)
        {
            List<BidSecurityModel> list = new List<BidSecurityModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifsSecurities.Where(x => x.IFS_Code == tendernumber).ToList();
                foreach (var securities in query)
                {
                    BidSecurityModel security = new BidSecurityModel();
                    security.IFS_Code = securities.IFS_Code;
                    security.Form_of_Security = Convert.ToString(securities.Form_of_Security);
                    security.Security_Type = securities.Security_Type;
                    security.Required_at_Bid_Submission = Convert.ToString(securities.Required_at_Bid_Submission);
                    security.Description = securities.Description;
                    security.Security_Amount_LCY = Convert.ToString(securities.Security_Amount_LCY);
                    security.Bid_Security_Validity_Expiry = Convert.ToString(securities.Bid_Security_Validity_Expiry);
                    security.Nature_of_Security = securities.Nature_of_Security;
                    list.Add(security);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<SingleTenderModel> GetSingleTenderDetails(string tendernumber)
        {
            List<SingleTenderModel> list = new List<SingleTenderModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var today = DateTime.Today;
                var query = nav.invitetoTenders.Where(x => x.Code == tendernumber && x.Published == true && x.Document_Status=="Published" &&x.Procurement_Method== "Open Tender").ToList();
                foreach (var tenders in query)
                {
                    SingleTenderModel tender = new SingleTenderModel();
                    tender.Code = tenders.Code;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Solicitation_Type = tenders.Solicitation_Type;
                    tender.External_Document_No = tenders.External_Document_No;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Invitation_Notice_Type = tenders.Invitation_Notice_Type;
                    tender.Procurement_Type = tenders.Procurement_Type;
                    tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                    tender.Project_ID = tenders.Project_ID;
                    tender.Tender_Name = tenders.Tender_Name;
                    tender.Responsibility_Center = tenders.Responsibility_Center;
                    tender.Tender_Summary = tenders.Tender_Summary;
                    tender.Description = tenders.Description;
                    tender.Document_Status = tenders.Document_Status;
                    tender.Target_Bidder_Group = tenders.Target_Bidder_Group;
                    tender.Tender_Validity_Duration = tenders.Tender_Validity_Duration;
                    tender.Tender_Validity_Expiry_Date = Convert.ToString(tenders.Tender_Validity_Expiry_Date);
                    tender.Bid_Selection_Method = tenders.Bid_Selection_Method;
                    tender.Location_Code = tenders.Location_Code;
                    tender.Requisition_Product_Group = tenders.Requisition_Product_Group;
                    tender.Language_Code = tenders.Language_Code;
                    tender.Mandatory_Special_Group_Reserv =tenders.PP_Preference_Reservation_Code;
                    tender.Document_Date = Convert.ToDateTime(tenders.Document_Date);
                    tender.Status = tenders.Status;
                    tender.Name = tenders.Name;
                    tender.Location_Name = tenders.Location_Name;
                    tender.Lot_No = tenders.Lot_No;
                    tender.Submission_End_Date = Convert.ToDateTime(tenders.Submission_End_Date);
                    tender.Submission_EndTime = Convert.ToString(tenders.Submission_End_Time);
                    tender.Submission_Start_Date = Convert.ToDateTime(tenders.Submission_Start_Date);
                    tender.Date_Created = Convert.ToDateTime(tenders.Created_Date_Time);
                    tender.Mandatory_Pre_bid_Visit_Date = Convert.ToDateTime(tenders.Mandatory_Pre_bid_Visit_Date);
                    tender.Prebid_Meeting_Address = tenders.Prebid_Meeting_Address;
                    tender.Phone_No = tenders.Phone_No;
                    tender.Bid_Opening_Time = tenders.Bid_Opening_Time;
                    tender.Procuring_Entity_Name_Contact = tenders.Procuring_Entity_Name_Contact;
                    tender.Primary_Tender_Submission = tenders.Primary_Tender_Submission;
                    tender.Performance_Security_Required =Convert.ToString(tenders.Performance_Security_Required);
                    if (tender.Performance_Security_Required == "True")
                    {

                        tender.Performance_Security_Required = "Yes";
                    }
                    else
                    {
                        tender.Performance_Security_Required = "No";
                    }
                    tender.Bid_Tender_Security_Required =Convert.ToString(tenders.Bid_Tender_Security_Required);
                    if (tender.Bid_Tender_Security_Required == "True")
                    {

                        tender.Bid_Tender_Security_Required = "Yes";
                    }
                    else
                    {
                        tender.Bid_Tender_Security_Required = "No";
                    }
                    tender.Bid_Scoring_Template = tenders.Bid_Scoring_Template;
                    tender.Bid_Security = Convert.ToString(tenders.Bid_Security);
                    tender.Performance_Security = Convert.ToString(tenders.Performance_Security);
                    tender.Bid_Security_Amount_LCY = Convert.ToDecimal(tenders.Bid_Security_Amount_LCY);
                    tender.Advance_Payment_Security_Req = Convert.ToString(tenders.Advance_Payment_Security_Req);
                    tender.Bid_Security_Validity_Duration = tenders.Bid_Security_Validity_Duration;
                    tender.Advance_Amount_Limit = Convert.ToDecimal(tenders.Advance_Amount_Limit);
                    tender.Insurance_Cover_Required =Convert.ToString(tenders.Insurance_Cover_Required);
                    if (tender.Insurance_Cover_Required == "True")
                    {

                        tender.Insurance_Cover_Required = "Yes";
                    }
                    else
                    {
                        tender.Insurance_Cover_Required = "No";
                    }
                    tender.Appointer_of_Bid_Arbitrator = tenders.Appointer_of_Bid_Arbitrator;
                    tender.Published = Convert.ToString(tenders.Published);
                    tender.Bid_Envelop_Type = tenders.Bid_Envelop_Type;
                    tender.Bid_Security_Expiry_Date = tenders.Bid_Security_Expiry_Date;
                    tender.Bid_Opening_Date = Convert.ToString(tenders.Bid_Opening_Date);
                    tender.Sealed_Bids =Convert.ToString(tenders.Sealed_Bids);
                    if (tender.Sealed_Bids == "True")
                    {

                        tender.Sealed_Bids = "Yes";
                    }
                    else
                    {
                        tender.Sealed_Bids = "No";
                    }
                    tender.Address = tenders.Address;
                    tender.Post_Code = tenders.Post_Code;
                    tender.City = tenders.City;
                    tender.Country_Region_Code = tenders.Country_Region_Code;
                    tender.Tender_Box_Location_Code = tenders.Tender_Box_Location_Code;
                    tender.Bid_Opening_Venue = tenders.Bid_Opening_Venue;
                    tender.Bank_Account_Name = tenders.Bank_Account_Name;
                    tender.Bid_Charge_Bank_Code = tenders.Bid_Charge_Bank_Code;
                    tender.Bid_Charge_Bank_Branch = tenders.Bid_Charge_Bank_Branch;
                    tender.Address_2 = tenders.Address_2;
                    tender.Advance_Payment_Security = Convert.ToDecimal(tenders.Advance_Payment_Security);
                    tender.Bid_Charge_Code = tenders.Bid_Charge_Code;
                    tender.Bid_Charge_Bank_Code = tenders.Bid_Charge_Bank_Code;
                    tender.Bid_Charge_LCY = tenders.Bid_Charge_LCY;
                    tender.Bank_Name = tenders.Bank_Name;
                    tender.Bank_Account_Name = tenders.Bank_Account_Name;
                    tender.Submission_EndTime = Convert.ToString(tenders.Submission_End_Time);
                    tender.Bid_Charge_Bank_Branch = tenders.Bid_Charge_Bank_Branch;
                    tender.Bid_Charge_Bank_A_C_No = tenders.Bid_Charge_Bank_A_C_No;
                    list.Add(tender);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<SingleTenderModel> GetSingleRFQDetails(string tendernumber)
        {
            List<SingleTenderModel> list = new List<SingleTenderModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var today = DateTime.Today;
                var query = nav.invitetoTenders.Where(x => x.Code == tendernumber && x.Published == true && x.Document_Status == "Published" && x.Procurement_Method == "RFQ" && x.Submission_End_Date >= today).ToList();
                foreach (var tenders in query)
                {
                    SingleTenderModel tender = new SingleTenderModel();
                    tender.Code = tenders.Code;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Solicitation_Type = tenders.Solicitation_Type;
                    tender.External_Document_No = tenders.External_Document_No;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Invitation_Notice_Type = tenders.Invitation_Notice_Type;
                    tender.Procurement_Type = tenders.Procurement_Type;
                    tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                    tender.Project_ID = tenders.Project_ID;
                    tender.Tender_Name = tenders.Tender_Name;
                    tender.Responsibility_Center = tenders.Responsibility_Center;
                    tender.Tender_Summary = tenders.Tender_Summary;
                    tender.Description = tenders.Description;
                    tender.Document_Status = tenders.Document_Status;
                    tender.Target_Bidder_Group = tenders.Target_Bidder_Group;
                    tender.Tender_Validity_Duration = tenders.Tender_Validity_Duration;
                    tender.Tender_Validity_Expiry_Date = Convert.ToString(tenders.Tender_Validity_Expiry_Date);
                    tender.Bid_Selection_Method = tenders.Bid_Selection_Method;
                    tender.Location_Code = tenders.Location_Code;
                    tender.Requisition_Product_Group = tenders.Requisition_Product_Group;
                    tender.Language_Code = tenders.Language_Code;
                    tender.Mandatory_Special_Group_Reserv = tenders.PP_Preference_Reservation_Code;
                    tender.Document_Date = Convert.ToDateTime(tenders.Document_Date);
                    tender.Status = tenders.Status;
                    tender.Name = tenders.Name;
                    tender.Lot_No = tenders.Lot_No;
                    tender.Submission_End_Date = Convert.ToDateTime(tenders.Submission_End_Date);
                    tender.Submission_EndTime = Convert.ToString(tenders.Submission_End_Time);
                    tender.Submission_Start_Date = Convert.ToDateTime(tenders.Submission_Start_Date);
                    tender.Date_Created = Convert.ToDateTime(tenders.Created_Date_Time);
                    tender.Mandatory_Pre_bid_Visit_Date = Convert.ToDateTime(tenders.Mandatory_Pre_bid_Visit_Date);
                    tender.Prebid_Meeting_Address = tenders.Prebid_Meeting_Address;
                    tender.Phone_No = tenders.Phone_No;
                    tender.Bid_Opening_Time = tenders.Bid_Opening_Time;
                    tender.Procuring_Entity_Name_Contact = tenders.Procuring_Entity_Name_Contact;
                    tender.Primary_Tender_Submission = tenders.Primary_Tender_Submission;
                    tender.Performance_Security_Required = Convert.ToString(tenders.Performance_Security_Required);
                    tender.deliveryDate = Convert.ToDateTime(tenders.Sample_Delivery_Date).ToString("MM/dd/yyyy");
                    if (tender.Performance_Security_Required == "True")
                    {

                        tender.Performance_Security_Required = "Yes";
                    }
                    else
                    {
                        tender.Performance_Security_Required = "No";
                    }
                    tender.Bid_Tender_Security_Required = Convert.ToString(tenders.Bid_Tender_Security_Required);
                    if (tender.Bid_Tender_Security_Required == "True")
                    {

                        tender.Bid_Tender_Security_Required = "Yes";
                    }
                    else
                    {
                        tender.Bid_Tender_Security_Required = "No";
                    }
                    tender.Bid_Scoring_Template = tenders.Bid_Scoring_Template;
                    tender.Bid_Security = Convert.ToString(tenders.Bid_Security);
                    tender.Performance_Security = Convert.ToString(tenders.Performance_Security);
                    tender.Bid_Security_Amount_LCY = Convert.ToDecimal(tenders.Bid_Security_Amount_LCY);
                    tender.Advance_Payment_Security_Req = Convert.ToString(tenders.Advance_Payment_Security_Req);
                    tender.Bid_Security_Validity_Duration = tenders.Bid_Security_Validity_Duration;
                    tender.Advance_Amount_Limit = Convert.ToDecimal(tenders.Advance_Amount_Limit);
                    tender.Insurance_Cover_Required = Convert.ToString(tenders.Insurance_Cover_Required);
                    if (tender.Insurance_Cover_Required == "True")
                    {

                        tender.Insurance_Cover_Required = "Yes";
                    }
                    else
                    {
                        tender.Insurance_Cover_Required = "No";
                    }
                    tender.Appointer_of_Bid_Arbitrator = tenders.Appointer_of_Bid_Arbitrator;
                    tender.Published = Convert.ToString(tenders.Published);
                    tender.Bid_Envelop_Type = tenders.Bid_Envelop_Type;
                    tender.Bid_Security_Expiry_Date = tenders.Bid_Security_Expiry_Date;
                    tender.Bid_Opening_Date = Convert.ToString(tenders.Bid_Opening_Date);
                    tender.Sealed_Bids = Convert.ToString(tenders.Sealed_Bids);
                    if (tender.Sealed_Bids == "True")
                    {

                        tender.Sealed_Bids = "Yes";
                    }
                    else
                    {
                        tender.Sealed_Bids = "No";
                    }
                    tender.Address = tenders.Address;
                    tender.Post_Code = tenders.Post_Code;
                    tender.City = tenders.City;
                    tender.Country_Region_Code = tenders.Country_Region_Code;
                    tender.Tender_Box_Location_Code = tenders.Tender_Box_Location_Code;
                    tender.Bid_Opening_Venue = tenders.Bid_Opening_Venue;
                    tender.Bank_Account_Name = tenders.Bank_Account_Name;
                    tender.Bid_Charge_Bank_Code = tenders.Bid_Charge_Bank_Code;
                    tender.Bid_Charge_Bank_Branch = tenders.Bid_Charge_Bank_Branch;
                    tender.Address_2 = tenders.Address_2;
                    tender.Advance_Payment_Security = Convert.ToDecimal(tenders.Advance_Payment_Security);
                    tender.Bid_Charge_Code = tenders.Bid_Charge_Code;
                    tender.Bid_Charge_Bank_Code = tenders.Bid_Charge_Bank_Code;
                    tender.Bid_Charge_LCY = tenders.Bid_Charge_LCY;
                    tender.Bank_Name = tenders.Bank_Name;
                    tender.Bank_Account_Name = tenders.Bank_Account_Name;
                    tender.Submission_EndTime = Convert.ToString(tenders.Submission_End_Time);
                    tender.Bid_Charge_Bank_Branch = tenders.Bid_Charge_Bank_Branch;
                    tender.Bid_Charge_Bank_A_C_No = tenders.Bid_Charge_Bank_A_C_No;
                    list.Add(tender);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<SingleTenderAddendumModel> GetSingleTenderAddendum(string tendernumber)
        {
            List<SingleTenderAddendumModel> list = new List<SingleTenderAddendumModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.tenderAddendums.Where(x => x.Invitation_Notice_No == tendernumber).ToList();
                foreach (var addendums in query)
                {
                    SingleTenderAddendumModel addendum = new SingleTenderAddendumModel();
                    addendum.Addendum_Notice_No = addendums.Addendum_Notice_No;
                    addendum.Invitation_Notice_No = addendums.Invitation_Notice_No;
                    addendum.Document_Date = Convert.ToString(addendums.Document_Date);
                    addendum.Addendum_Instructions = addendums.Addendum_Instructions;
                    addendum.Primary_Addendum_Type_ID = addendums.Primary_Addendum_Type_ID;
                    addendum.Addendum_Type_Description = addendums.Addendum_Type_Description;
                    addendum.Tender_No = addendums.Tender_No;
                    addendum.Tender_Description = addendums.Tender_Description;
                    addendum.Responsibility_Center = addendums.Responsibility_Center;
                    addendum.Description = addendums.Description;
                    addendum.New_Submission_Start_Date = Convert.ToString(addendums.New_Submission_Start_Date);
                    addendum.Status = addendums.Status;
                    addendum.Original_Submission_Start_Date = Convert.ToString(addendums.Original_Submission_Start_Date);
                    addendum.New_Submission_End_Time = addendums.New_Submission_End_Time;
                    addendum.Original_Submission_End_Date = Convert.ToString(addendums.Original_Submission_End_Date);
                    addendum.Original_Bid_Opening_Date = Convert.ToString(addendums.Original_Bid_Opening_Date);
                    addendum.New_Bid_Opening_Date = Convert.ToString(addendums.New_Bid_Opening_Date);
                    addendum.Original_Bid_Opening_Time = addendums.Original_Bid_Opening_Time;
                    addendum.Original_Prebid_Meeting_Date = Convert.ToString(addendums.Original_Prebid_Meeting_Date);
                    addendum.New_Prebid_Meeting_Date = Convert.ToString(addendums.New_Prebid_Meeting_Date);
                    addendum.Original_Bid_Opening_Date = Convert.ToString(addendums.Original_Bid_Opening_Date);
                    list.Add(addendum);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<SingleTenderAddendumModel> GetSinglePrequalificatinAddendum(string tendernumber)
        {
            List<SingleTenderAddendumModel> list = new List<SingleTenderAddendumModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.tenderAddendums.Where(x => x.Notice_No == tendernumber).ToList();
                foreach (var addendums in query)
                {
                    SingleTenderAddendumModel addendum = new SingleTenderAddendumModel();
                    addendum.Addendum_Notice_No = addendums.Addendum_Notice_No;
                    addendum.Invitation_Notice_No = addendums.Invitation_Notice_No;
                    addendum.Document_Date = Convert.ToString(addendums.Document_Date);
                    addendum.Addendum_Instructions = addendums.Addendum_Instructions;
                    addendum.Primary_Addendum_Type_ID = addendums.Primary_Addendum_Type_ID;
                    addendum.Addendum_Type_Description = addendums.Addendum_Type_Description;
                    addendum.Tender_No = addendums.Tender_No;
                    addendum.Tender_Description = addendums.Tender_Description;
                    addendum.Responsibility_Center = addendums.Responsibility_Center;
                    addendum.Description = addendums.Description;
                    addendum.New_Submission_Start_Date = Convert.ToString(addendums.New_Submission_Start_Date);
                    addendum.Status = addendums.Status;
                    addendum.Original_Submission_Start_Date = Convert.ToString(addendums.Original_Submission_Start_Date);
                    addendum.New_Submission_End_Time = addendums.New_Submission_End_Time;
                    addendum.Original_Submission_End_Date = Convert.ToString(addendums.Original_Submission_End_Date);
                    addendum.Original_Bid_Opening_Date = Convert.ToString(addendums.Original_Bid_Opening_Date);
                    addendum.New_Bid_Opening_Date = Convert.ToString(addendums.New_Bid_Opening_Date);
                    addendum.Original_Bid_Opening_Time = addendums.Original_Bid_Opening_Time;
                    addendum.Original_Prebid_Meeting_Date = Convert.ToString(addendums.Original_Prebid_Meeting_Date);
                    addendum.New_Prebid_Meeting_Date = Convert.ToString(addendums.New_Prebid_Meeting_Date);
                    addendum.Original_Bid_Opening_Date = Convert.ToString(addendums.Original_Bid_Opening_Date);
                    list.Add(addendum);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        public ActionResult MyStatement()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Vendors = GetVendors(vendorNo);
                model.Statement = GetVendorsStatement(vendorNo);
                return View(model);
            }

        }
        public ActionResult MyAccount()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Vendors = GetVendors(vendorNo);
                model.BanksDetails = GetBanks(vendorNo);
                model.StakeholdersDetails = GetStakeholders(vendorNo);
               // model.Beneficiaries = GetStakeholders(vendorNo);
                model.PrequalifcationHistory = GetPrequalificationHistory(vendorNo);
                model.PastExperience = GetVendorPastExeprience(vendorNo);
                model.litigationhistory = GetVendorLitigationHistoryDetails(vendorNo);
                model.balancesheet = GetVendorBalanaceDetails(vendorNo);
                model.incomestatement = GetVendorIncomeStatementDetails(vendorNo);
                return View(model);
            }
        }
        public ActionResult eBidding()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                List<TenderModel> list = new List<TenderModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var today = DateTime.Today;
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status=="Published" &&x.Procurement_Method== "Open Tender" &&x.Submission_End_Date>=today).ToList();
                    foreach (var tenders in query)
                    {
                        TenderModel tender = new TenderModel();
                        tender.Code = tenders.Code;
                        tender.Procurement_Method = tenders.Procurement_Method;
                        tender.Solicitation_Type = tenders.Solicitation_Type;
                        tender.External_Document_No = tenders.External_Document_No;
                        tender.Procurement_Type = tenders.Procurement_Type;
                        tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                        tender.Project_ID = tenders.Project_ID;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Tender_Summary = tenders.Tender_Summary;
                        tender.Description = tenders.Description;
                        tender.Document_Date = tenders.Document_Date;
                        tender.Status = tenders.Status;
                        tender.Name = tenders.Name;
                        tender.Submission_End_Date = tenders.Submission_End_Date;
                        tender.Submission_Start_Date = tenders.Submission_Start_Date;
                        tender.Published = tenders.Published;
                        list.Add(tender);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult ActiveTenderNotices()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<TenderModel> list = new List<TenderModel>();
             
                    
                    var nav = NavConnection.ReturnNav();
                    var today = DateTime.Today;
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status== "Published" &&x.Procurement_Method=="Open Tender").ToList();
                    //&&x.Submission_End_Date>=today
                    foreach (var tenders in query)
                    {
                        TenderModel tender = new TenderModel();
                        tender.Code = tenders.Code;
                        tender.Procurement_Method = tenders.Procurement_Method;
                        tender.Solicitation_Type = tenders.Solicitation_Type;
                        tender.External_Document_No = tenders.External_Document_No;
                        tender.Procurement_Type = tenders.Procurement_Type;
                        tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                        tender.Project_ID = tenders.Project_ID;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Tender_Summary = tenders.Tender_Summary;
                        tender.Description = tenders.Description;
                        tender.Document_Date = tenders.Document_Date;
                        tender.Status = tenders.Status;
                        tender.Name = tenders.Name;
                        tender.Submission_End_Date = tenders.Submission_End_Date;
                        tender.Submission_Start_Date = tenders.Submission_Start_Date;
                        tender.Published = tenders.Published;
                        list.Add(tender);
                    }


              
                return View(list);

            }
        }
        public ActionResult Dashboard()
        {
            if (Session["vendorNo"] != null)
            {
                dynamic model = new ExpandoObject();
                model.ActiveTenders = GetActiveTenderDetail();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ePrequalifications()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Message = "ePrequalifications";
                List<IFPRequestsModel> list = new List<IFPRequestsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var today = DateTime.Today;
                    var query = nav.InvitationPrequalification.Where(x => x.Status == "Released" && x.Published == true && x.Document_Type == "Invitation For Prequalification" &&x.Submission_End_Date>=today).ToList();
                    foreach (var invitations in query)
                    {
                        IFPRequestsModel invite = new IFPRequestsModel();
                        invite.Code = invitations.Code;
                        invite.Description = invitations.Description;
                        invite.Tender_Box_Location_Code = invitations.Tender_Box_Location_Code;
                        invite.External_Document_No = invitations.External_Document_No;
                        invite.Tender_Summary = invitations.Tender_Summary;
                        invite.Submission_End_Date = Convert.ToString(invitations.Submission_End_Date);
                        invite.Submission_Start_Date = Convert.ToString(invitations.Submission_Start_Date);
                        list.Add(invite);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult SupplierRegistration()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                ViewBag.prequalificationNo = vendorNo;
                dynamic model = new ExpandoObject();
                model.Vendors = GetVendors(vendorNo);
                model.BanksDetails = GetBanks(vendorNo);
                model.StakeholdersDetails = GetStakeholders(vendorNo);
                model.PrequalifcationHistory = GetPrequalificationHistory(vendorNo);
                model.PastExperience = GetVendorPastExeprience(vendorNo);
                model.litigationhistory = GetVendorLitigationHistoryDetails(vendorNo);
                model.balancesheet = GetVendorBalanaceDetails(vendorNo);
                model.incomestatement = GetVendorIncomeStatementDetails(vendorNo);
                model.VendorProfessionalStaff = GetVendorProfessionalStaff(vendorNo);
                model.UploadedDocuments = AlreadyRegisteredDocumentsDetails(vendorNo);
                model.RequiredDocuments = RegistrationRequiredDocumentsDetails(vendorNo);
                return View(model);

            }
        }

        public ActionResult ViewPrequalifications(string InvitationNumber, string scoringTemplate)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.PrequalificationDetails = GetPrequalificationsDetails(InvitationNumber);
                model.Categories = GetPrequalificationCategories(InvitationNumber);
                model.TenderAddendums = GetSinglePrequalificatinAddendum(InvitationNumber);
                model.RequiredDocuments = PrequalificationsRequiredDocumentsDetails(InvitationNumber);
                model.AttachedDocuments = PopulatePrequalificationDocuments(InvitationNumber);
                model.RFI_SCORING_TEMPLATE = EvaluationTemplate(scoringTemplate);
                return View(model);
            }

        }
        public ActionResult ViewRegistration(string InvitationNumber, string scoringTemplate)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.PrequalificationDetails = GetPrequalificationsDetails(InvitationNumber);
                model.Categories = GetPrequalificationCategories(InvitationNumber);
                model.TenderAddendums = GetSinglePrequalificatinAddendum(InvitationNumber);
                model.RequiredDocuments = RegistrationRequiredDocumentsDetails(InvitationNumber);
                model.AttachedDocuments = PopulatePrequalificationDocuments(InvitationNumber);
                model.RFI_SCORING_TEMPLATE = EvaluationTemplate(scoringTemplate);
                return View(model);
            }

        }
        public ActionResult ViewPrequalificationsAdvert(string InvitationNumber)
        {
          
                
                dynamic model = new ExpandoObject();
                model.PrequalificationDetails = GetPrequalificationsDetails(InvitationNumber);
                model.Categories = GetPrequalificationCategories(InvitationNumber);
                model.RequiredDocuments = PrequalificationsRequiredDocumentsDetails(InvitationNumber);
                model.AttachedDocuments = PopulatePrequalificationDocuments(InvitationNumber);
                return View(model);
            

        }
        public ActionResult ViewSubmittedPrequalifications(string IFPNumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.PrequalificationGeneralDetails = GetPrequalificationsGeneralDetails(IFPNumber,vendorNo);
                model.Categories = GetSubmittedPrequalificationCategories(IFPNumber,vendorNo);
                model.AttachedDocuments = GetAttachedPrequalificationsDocuments(IFPNumber,vendorNo);
                return View(model);
            }

        }
        public ActionResult ViewSubmittedOpenTenderResponses(string responseNumber, string tenderNo)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.ResponseDetails = GetResponseGeneralDetails(responseNumber, vendorNo);
                model.BidPersonnel = GetBidResponsePersonnel(responseNumber);
                model.BidPastExperiencent = GetBidResponsePastExperience(responseNumber, vendorNo);
                model.BidEquipments = GetBidResponseEquipments(responseNumber);
                model.SingleTenders = GetSingleTenderDetails(tenderNo);
                model.AttachedDocuments = GetAttachedPrequalificationsDocuments(responseNumber, vendorNo);
                model.AttachedBiddDocuments = GetBidAttachedDocumentsDetails(responseNumber, vendorNo);
                return View(model);
            }

        }
        public ActionResult ViewSubmittedRFQResponses(string responseNumber, string tenderNo)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.ResponseDetails = GetResponseGeneralDetails(responseNumber, vendorNo);
                model.financialresponse = GetFinancialResponse(responseNumber, vendorNo);
                model.SingleTenders = GetSingleRFQDetails(tenderNo);
                model.AttachedDocuments = GetAttachedPrequalificationsDocuments(responseNumber, vendorNo);
                model.AttachedBiddDocuments = GetBidAttachedDocumentsDetails(responseNumber, vendorNo);
                return View(model);
            }

        }
        private static List<PrequalificationGeneralDetailsModel> GetPrequalificationsGeneralDetails(string IFPNumber,string vendorNo)
        {
            List<PrequalificationGeneralDetailsModel> list = new List<PrequalificationGeneralDetailsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.RFIResponses.Where(x => x.Vendor_No == vendorNo && x.Document_No == IFPNumber).ToList();
                foreach (var responses in query)
                {
                    PrequalificationGeneralDetailsModel response = new PrequalificationGeneralDetailsModel();
                    response.Created_Date = Convert.ToString(responses.Created_Date);
                    response.Created_Time = responses.Created_Time;
                    response.Date_Submitted =Convert.ToString(responses.Date_Submitted);
                    response.Final_Evaluation_Score = Convert.ToString(responses.Final_Evaluation_Score);
                    response.Special_Group_Category = responses.Special_Group_Category;
                    response.Special_Group_Vendor = Convert.ToString(responses.Special_Group_Vendor);
                    response.E_Mail = Convert.ToString(responses.E_Mail);
                    response.Post_Code = responses.Post_Code;
                    response.Country_Region_Code = responses.Country_Region_Code;
                    response.City = responses.City;
                    response.Vendor_Address = responses.Vendor_Address;
                    response.Vendor_Address_2 = responses.Vendor_Address_2;
                    response.Vendor_Representative_Name = responses.Vendor_Representative_Name;
                    response.Vendor_Repr_Designation = responses.Vendor_Repr_Designation;
                    response.RFI_Document_No = responses.RFI_Document_No;
                    response.Vendor_No = responses.Vendor_No;
                    response.Vendor_Name = responses.Vendor_Name;
                    response.Document_No = responses.Document_No;
                    response.Document_Date = Convert.ToString(responses.Document_Date);
                    response.Document_Type = responses.Document_Type;
                    list.Add(response);
                   }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<SubmittedTenderResponse> GetResponseGeneralDetails(string responseNumber, string vendorNo)
        {
            List<SubmittedTenderResponse> list = new List<SubmittedTenderResponse>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidResponsesDetails.Where(x => x.Vendor_No == vendorNo && x.No == responseNumber).ToList();
                foreach (var responses in query)
                {
                    SubmittedTenderResponse response = new SubmittedTenderResponse();
                    response.InvitationNo = responses.Invitation_For_Supply_No;
                    response.vendorNo = responses.Vendor_No;
                    response.VendorName = responses.Pay_to_Name;
                    response.ResponseNo = responses.No;
                    response.ResponsibilityCentre = responses.Responsibility_Center;
                    response.tenderDescription = responses.Tender_Description;
                    response.BidRepName = responses.Bidder_Representative_Name;
                    response.bidderRepDesignation = responses.Bidder_Representative_Desgn;
                    response.bidderRepAddress = responses.Bidder_Representative_Address;
                    response.BidderWittnesName = responses.Bidder_Witness_Name;
                    response.VAT_Registration_No = responses.VAT_Registration_No;
                    response.Bidder_Witness_Designation = responses.Bidder_Witness_Designation;
                    response.Bid_Charge_LCY = Convert.ToString(responses.Bid_Charge_LCY);
                    response.Bidder_witness_Address = responses.Bidder_Witness_Address;
                    response.Bid_Charge_Code = responses.Bid_Charge_Code;
                    response.Payment_Reference_No = responses.Payment_Reference_No;
                    response.BiddeType = responses.Bidder_Type;
                    response.jointPartnerVenture = responses.Joint_Venture_Partner;
                    response.TenderDocumentSources = responses.Tender_Document_Source;                   

                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<SubmittedTenderResponse> GetFinancialResponse(string responseNumber, string vendorNo)
        {
            List<SubmittedTenderResponse> list = new List<SubmittedTenderResponse>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidResponseItemLines.Where(x => x.Buy_from_Vendor_No == vendorNo && x.Document_No == responseNumber).ToList();
                foreach (var responses in query)
                {
                    SubmittedTenderResponse response = new SubmittedTenderResponse();
                    response.itemDescription = responses.Description;
                    response.itemLocation = responses.Location_Code;
                    response.itemQuantity = Convert.ToDecimal(responses.Quantity);
                    response.directunitcost = Convert.ToDecimal( responses.Direct_Unit_Cost_Inc_VAT);
                 

                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<SubmittedPrequalificationCategoriesModel> GetSubmittedPrequalificationCategories(string IFPNumber, string vendorNo)
        {
            List<SubmittedPrequalificationCategoriesModel> list = new List<SubmittedPrequalificationCategoriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.IFPResponseLines.Where(x => x.Vendor_No == vendorNo && x.Document_No == IFPNumber).ToList();
                foreach (var responses in query)
                {
                    SubmittedPrequalificationCategoriesModel response = new SubmittedPrequalificationCategoriesModel();
                    response.Evaluation_Decision = Convert.ToString(responses.Evaluation_Decision);
                    response.Evaluation_Score =Convert.ToString(responses.Evaluation_Score);
                    response.Prequalification_End_Date = Convert.ToString(responses.Prequalification_End_Date);
                    response.Prequalification_Start_Date = Convert.ToString(responses.Prequalification_Start_Date);
                    response.Restricted_RC_ID = responses.Restricted_RC_ID;
                    response.Restricted_Responsbility_Cente = Convert.ToString(responses.Restricted_Responsbility_Cente);
                    response.Global_RC_Prequalification = Convert.ToString(responses.Global_RC_Prequalification);
                    response.Unique_Category_Requirements = Convert.ToString(responses.Unique_Category_Requirements);                 
                    response.Special_Group_Reservation =Convert.ToString(responses.Special_Group_Reservation);
                    if (response.Special_Group_Reservation == "True")
                    {

                        response.Special_Group_Reservation = "Yes";
                    }
                    else
                    {
                        response.Special_Group_Reservation = "No";
                    }
                    response.Vendor_No = responses.Vendor_No;
                    response.RFI_Document_No = responses.RFI_Document_No;
                    response.Category_Description = responses.Category_Description;
                    response.Procurement_Category = responses.Procurement_Category;
                    response.Document_No = responses.Document_No;
                    response.RFI_Document_No = responses.RFI_Document_No;
                    response.Vendor_No = responses.Vendor_No;
                    response.Document_Type = responses.Document_Type;
                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<IfPDocumentsTModel> GetAttachedPrequalificationsDocuments(string IFPNumber, string vendorNo)
        {
            List<IfPDocumentsTModel> list = new List<IfPDocumentsTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.RFIResponseDocuments.Where(x => x.Vendor_No == vendorNo && x.Document_No == IFPNumber).ToList();
                foreach (var responses in query)
                {
                    IfPDocumentsTModel response = new IfPDocumentsTModel();
                    response.Document_No = Convert.ToString(responses.Document_No);
                    response.Description = Convert.ToString(responses.Document_Description);
                    response.Issue_Date = Convert.ToString(responses.Issue_Date);
                    response.Expiry_Date = Convert.ToString(responses.Expiry_Date);
                    response.Vendor_No = responses.Vendor_No;
                    response.Certificate_No = Convert.ToString(responses.Certificate_No);
                    response.File_Extension = Convert.ToString(responses.File_Extension);
                    response.File_Name = Convert.ToString(responses.File_Name);
                    response.File_Type = responses.File_Type;
                    list.Add(response);
                 }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<VendorStatementModel> GetVendorsStatement(string vendorNo)
        {
            List<VendorStatementModel> list = new List<VendorStatementModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorStatetement.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var statetements in query)
                {
                    VendorStatementModel statetement = new VendorStatementModel();
                    statetement.Vendor_No = statetements.Vendor_No;
                    statetement.Document_Type = statetements.Document_Type;
                    statetement.Posting_Date = Convert.ToDateTime(statetements.Posting_Date).ToString("dd-MM-yy");
                    statetement.Description = statetements.Description;
                    statetement.Document_No = statetements.Document_No;
                    statetement.Vendor_Name = statetements.Vendor_Name;
                    statetement.Amount = Convert.ToString(statetements.Amount).Replace("-"," ");
                    statetement.Remaining_Amount = Convert.ToString(statetements.Remaining_Amount).Replace("-", " "); ;
                    statetement.Amount_LCY = Convert.ToString(statetements.Amount_LCY);
                    statetement.Bal_Account_No = statetements.Bal_Account_No;
                    statetement.Bal_Account_Type = statetements.Bal_Account_Type;
                    statetement.Transaction_No = Convert.ToString(statetements.Transaction_No);
                    statetement.Debit_Amount_LCY = Convert.ToString(statetements.Debit_Amount_LCY);
                    statetement.Credit_Amount_LCY = Convert.ToString(statetements.Credit_Amount_LCY);
                    statetement.Document_Date = Convert.ToString(statetements.Document_Date);
                    statetement.External_Document_No = statetements.External_Document_No;
                    statetement.Remaining_Amt_LCY = Convert.ToString(statetements.Remaining_Amt_LCY);
                    list.Add(statetement);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        [HttpGet]
        public ActionResult DisplayPdfInIframe(string PrequalificationNumber)
        {
            try
            {

                var nav = new NavConnection().ObjNav();
                var vendorNo = Session["vendorNo"].ToString();
                String status = nav.FnGeneratePrequalificationPreviewReport(vendorNo, PrequalificationNumber);


            }
            catch
            {

            }

            string filePath = "~/Downloads/" + PrequalificationNumber + ".pdf";
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = PrequalificationNumber + ".pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Pdf);

        }
        [HttpGet]
        public ActionResult DisplayPdfInIframeRegistration(string bidresponseNo)
        {
            try
            {
                var nav = new NavConnection().ObjNav();
                var vendorNo = Session["vendorNo"].ToString();
                String status = nav.FnGenerateRegistrationPreviewReport(vendorNo, bidresponseNo);


            }
            catch
            {

            }

            string filePath = "~/Downloads/" + bidresponseNo + ".pdf";
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = bidresponseNo + ".pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Pdf);

        }

        [HttpGet]
        public ActionResult DisplayPdfInIframeQuotation(string bidresponseNo)
        {
          
            try
            {
                var nav = new NavConnection().ObjNav();
                var vendorNo = Session["vendorNo"].ToString();
                if(bidresponseNo == null)
                {
                    bidresponseNo = Session["RFQBideResponseNumber"].ToString();
                }
                
                String status = nav.FnGenerateRFQPreviewReport(vendorNo, bidresponseNo);


            }
            catch
            {

            }

            string filePath = "~/Downloads/" + bidresponseNo + ".pdf";
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = bidresponseNo + ".pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Pdf);

        }
        [HttpGet]
        public ActionResult DisplayPdfInIframeTender(string bidresponseNo)
        {
           
            try
            {
                var nav = new NavConnection().ObjNav();

                var vendorNo = Session["vendorNo"].ToString();

                if (bidresponseNo == null)
                {
                    bidresponseNo = Session["BideResponseNumber"].ToString();
                }

                String status = nav.FnGenerateRFQPreviewReport(vendorNo, bidresponseNo);


            }
            catch(Exception e)
            {

            }


            bidresponseNo=Regex.Replace(bidresponseNo, ":","");


            string filePath = "~/Downloads/RFQ/" + bidresponseNo + ".pdf";
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = bidresponseNo + ".pdf",
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
            return File(filePath, System.Net.Mime.MediaTypeNames.Application.Pdf);

        }
        private static List<RegistrationDocumentsModel> AlreadyRegisteredDocumentsDetails(string vendorNo)
        {
            List<RegistrationDocumentsModel> list = new List<RegistrationDocumentsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorFiledRegDocument.Where(x => x.Vendor_No == vendorNo && x.Procurement_Process == "Registration").ToList();
                foreach (var filedocuments in query)
                {
                    RegistrationDocumentsModel documents = new RegistrationDocumentsModel();
                    documents.Procurement_Process = filedocuments.Procurement_Process;
                    documents.Procurement_Document_Type_ID = filedocuments.Procurement_Document_Type_ID;
                    documents.Description = filedocuments.Description;
                    documents.Date_Filed = Convert.ToString(filedocuments.Date_Filed);
                    documents.Certificate_No = filedocuments.Certificate_No;
                    documents.Issue_Date = Convert.ToString(filedocuments.Issue_Date);
                    documents.Expiry_Date = Convert.ToString(filedocuments.Expiry_Date);
                    documents.File_Name = filedocuments.File_Name;
                    documents.File_Type = filedocuments.File_Type;
                    documents.File_Extension = filedocuments.File_Extension;
                    documents.entryNo = filedocuments.Entry_No;
                    list.Add(documents);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        
        private static List<PreQualificationRequiredDocuments> PrequalificationUploaded(string PrequalificationNo, string vendorNo)
        {
            List<PreQualificationRequiredDocuments> list = new List<PreQualificationRequiredDocuments>();
            try
            {
                var nav = NavConnection.ReturnNav();

                var query = nav.RFIResponseDocuments.Where(x =>x.Document_No== PrequalificationNo && x.Procurement_Process == "Prequalification").ToList();
                foreach (var filedocuments in query)
                {
                    PreQualificationRequiredDocuments documents = new PreQualificationRequiredDocuments();
                    documents.Procurement_Process = filedocuments.Procurement_Process;
                    documents.Procurement_Document_Type_ID = filedocuments.Procurement_Document_Type_ID;
                    documents.Description = filedocuments.Document_Description;
                    documents.Date_Filed = Convert.ToString(filedocuments.Date_Filed);
                    documents.Certificate_No = filedocuments.Certificate_No;
                    documents.Issue_Date = Convert.ToString(filedocuments.Issue_Date);
                    documents.Expiry_Date = Convert.ToString(filedocuments.Expiry_Date);
                    documents.File_Name = filedocuments.File_Name;
                    documents.File_Type = filedocuments.File_Type;
                    documents.File_Extension = filedocuments.File_Extension;
                    list.Add(documents);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<AttachedBidDocuments> GetBidAttachedDocumentsDetails(string BidResponseNumber, string vendorNo)
        {
            List<AttachedBidDocuments> list = new List<AttachedBidDocuments>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidReponseAttachedDocuments.Where(x => x.No == BidResponseNumber && x.Vendor_No == vendorNo).ToList();
                foreach (var filedocuments in query)
                {
                    AttachedBidDocuments documents = new AttachedBidDocuments();
                    documents.Procurement_Process = filedocuments.Procurement_Process;
                    documents.entryNumber = filedocuments.Entry_No;
                    documents.Procurement_Document_Type_ID = filedocuments.Procurement_Document_Type_ID;
                    documents.Description = filedocuments.Description;
                    documents.Date_Filed = Convert.ToString(filedocuments.Date_Filed);
                    documents.Certificate_No = filedocuments.Certificate_No;
                    documents.Issue_Date = Convert.ToString(filedocuments.Issue_Date);
                    documents.Expiry_Date = Convert.ToString(filedocuments.Expiry_Date);
                    documents.File_Name = filedocuments.File_Name;
                    documents.File_Type = filedocuments.File_Type;
                    documents.File_Extension = filedocuments.File_Extension;
                    list.Add(documents);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<PreQualificationRequiredDocuments> PrequalificationsRequiredDocumentsDetails(string InvitationNumber)
        {
            List<PreQualificationRequiredDocuments> list = new List<PreQualificationRequiredDocuments>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifpReqDocuments.Where(x => x.Document_Type == "Invitation For Prequalification" && x.Document_No == InvitationNumber).ToList();
                foreach (var filedocuments in query)
                {
                    PreQualificationRequiredDocuments documents = new PreQualificationRequiredDocuments();
                    documents.Document_No = filedocuments.Document_No;
                    documents.Description = filedocuments.Description;
                    documents.Document_Type = filedocuments.Document_Type;
                    documents.Procurement_Document_Type_ID = filedocuments.Procurement_Document_Type_ID;
                    documents.Requirement_Type = filedocuments.Requirement_Type;
                    documents.SpecialGroupRequirement = Convert.ToString(filedocuments.Special_Group_Requirement);
                    if (documents.SpecialGroupRequirement == "True")
                    {

                        documents.SpecialGroupRequirement = "Yes";
                    }
                    else
                    {
                        documents.SpecialGroupRequirement = "No";
                    }
                    documents.SpecialisedRequirement = Convert.ToString(filedocuments.Specialized_Provider_Req);
                    list.Add(documents);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<RegistrationRequiredDocumentsModel> RegistrationRequiredDocumentsDetails(string vendorNo)
        {
            List<RegistrationRequiredDocumentsModel> list = new List<RegistrationRequiredDocumentsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.eProcDocuments.Where(x => x.Procurement_Process == "Registration").ToList();
                foreach (var filedocuments in query)
                {
                    RegistrationRequiredDocumentsModel documents = new RegistrationRequiredDocumentsModel();
                    documents.Template_ID = filedocuments.Template_ID;
                    documents.Description = filedocuments.Description;
                    documents.Procurement_Document_Type = filedocuments.Procurement_Document_Type;
                    documents.Requirement_Type = filedocuments.Requirement_Type;
                    documents.instructions = filedocuments.Guidelines_Instruction;
                    list.Add(documents);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }

        public JsonResult GetVendorKeyProfessionalStaff(string vendorNo)
        {

            List<ProfessionalStaffModel> staffDetails = new List<ProfessionalStaffModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorProffessionalStaff.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var staffs in query)
                {
                    ProfessionalStaffModel staff = new ProfessionalStaffModel();
                    staff.StaffNumber = staffs.Staff_Number;
                    staff.StaffName = staffs.Staff_Name;
                    staff.StaffDateofBirth = Convert.ToString(staffs.Date_of_Birth);
                    staff.StaffEmail = staffs.E_Mail;
                    staff.StaffDesignation = staffs.Current_Designation;
                    staff.Years_With_the_Firm = Convert.ToString(staffs.Years_With_the_Firm);
                    staff.Post_Code = staffs.Post_Code;
                    staff.Address_2 = staffs.Address_2;
                    staff.StaffPhonenumber = staffs.Phone_No;
                    staff.City = staffs.City;
                    staff.Citizenship_Type = staffs.Citizenship_Type;
                    staff.Staff_Category = staffs.Staff_Category;
                    staff.Country_Region_Code = staffs.Country_Region_Code;
                    staff.StaffProfession = staffs.Proffesion;
                    staff.StaffJoiningDate = Convert.ToString(staffs.Joining_Date);
                    staffDetails.Add(staff);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return Json(staffDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetKeyProfessionalStaff()
        {

            List<ProfessionalStaffModel> staffDetails = new List<ProfessionalStaffModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var vendorNo = Session["vendorNo"].ToString();
                var query = nav.VendorProffessionalStaff.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var staffs in query)
                {
                    ProfessionalStaffModel staff = new ProfessionalStaffModel();
                    staff.StaffNumber = staffs.Staff_Number;
                    staff.StaffName = staffs.Staff_Name;
                    staff.StaffDateofBirth = Convert.ToString(staffs.Date_of_Birth);
                    staff.StaffEmail = staffs.E_Mail;
                    staff.StaffDesignation = staffs.Current_Designation;
                    staff.Years_With_the_Firm = Convert.ToString(staffs.Years_With_the_Firm);
                    staff.Post_Code = staffs.Post_Code;
                    staff.Address_2 = staffs.Address_2;
                    staff.StaffPhonenumber = staffs.Phone_No;
                    staff.City = staffs.City;
                    staff.Citizenship_Type = staffs.Citizenship_Type;
                    staff.Staff_Category = staffs.Staff_Category;
                    staff.Country_Region_Code = staffs.Country_Region_Code;
                    staff.StaffProfession = staffs.Proffesion;
                    staff.StaffJoiningDate = Convert.ToString(staffs.Joining_Date);
                    staffDetails.Add(staff);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return Json(staffDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVendorKeyProffessionalStaffDetails()
        {

            List<ProfessionalStaffModel> staffDetails = new List<ProfessionalStaffModel>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorProffessionalStaff.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var staffs in query)
                {
                    ProfessionalStaffModel staff = new ProfessionalStaffModel();
                    staff.StaffNumber = staffs.Staff_Number;
                    staff.StaffName = staffs.Staff_Name;
                    staff.StaffDateofBirth = Convert.ToString(staffs.Date_of_Birth);
                    staff.StaffEmail = staffs.E_Mail;
                    staff.StaffDesignation = staffs.Current_Designation;
                    staff.Years_With_the_Firm = Convert.ToString(staffs.Years_With_the_Firm);
                    staff.Post_Code = staffs.Post_Code;
                    staff.Address_2 = staffs.Address_2;
                    staff.StaffPhonenumber = staffs.Phone_No;
                    staff.City = staffs.City;
                    staff.Citizenship_Type = staffs.Citizenship_Type;
                    staff.Staff_Category = staffs.Staff_Category;
                    staff.Country_Region_Code = staffs.Country_Region_Code;
                    staff.StaffProfession = staffs.Proffesion;
                    staff.StaffJoiningDate = Convert.ToString(staffs.Joining_Date);
                    staffDetails.Add(staff);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return Json(staffDetails, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetBidPersonnelDetails(string BidResponseNumber)
        {
            List<BidResponsePersonnel> list = new List<BidResponsePersonnel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidKeyStaff.Where(x => x.No == BidResponseNumber).ToList();
                foreach (var equipments in query)
                {
                    BidResponsePersonnel equipment = new BidResponsePersonnel();
                    equipment.No = equipments.No;
                    equipment.StaffCategory = equipments.Staff_Category;
                    equipment.StaffName = equipments.Staff_Name;
                    equipment.ProjectRoleCode = equipments.Proposed_Project_Role_ID;
                    equipment.RequiredProfession = equipments.Required_Project_Role;
                    equipment.EmailAddress = equipments.E_Mail;
                    equipment.EmploymentType = equipments.Employment_Type;
                    equipment.Entry_No = Convert.ToString(equipments.Entry_No);
                    list.Add(equipment);
                }

            }


            catch (Exception e)
            {

                throw;
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetVendorBankAccounts()
        {

            List<BankModel> BankAccounts = new List<BankModel>();
            try
            {
                
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorBankAccounts.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var vendorbanks in query)
                {
                    BankModel banks = new BankModel();
                    banks.BankCode = vendorbanks.Code;
                    banks.BankName = vendorbanks.Name;
                    banks.Post_Code = vendorbanks.Post_Code;
                    banks.Contact = vendorbanks.Contact;
                    banks.CurrencyCode = vendorbanks.Currency_Code;
                    banks.BankAccountNo = vendorbanks.Bank_Account_No;
                    banks.Bank_Branch_No = vendorbanks.Bank_Branch_No;
                    banks.City = vendorbanks.City;
                    BankAccounts.Add(banks);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return Json(BankAccounts, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetShareholderDetails()
        {
            List<ShareholderModel> ownersdetails = new List<ShareholderModel>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorShareholderDetails.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var owners in query)
                {
                    ShareholderModel owner = new ShareholderModel();
                    owner.Entry_No = owners.Entry_No;
                    owner.Vendor_No = owners.Vendor_No;
                    owner.Name = owners.Name;
                    owner.Address = owners.Address;
                    owner.Address_2 = owners.Address_2;
                    owner.City = owners.City;
                    owner.Entity_Ownership = owners.Entity_Ownership;
                    owner.Phone_No = owners.Phone_No;
                    owner.Nationality_ID = owners.Nationality_ID;
                    owner.ID_Passport_No = owners.ID_Passport_No;
                    owner.Share_Types = owners.Share_Types;
                    owner.Citizenship_Type = owners.Citizenship_Type;
                    owner.Country_Region_Code = owners.Country_Region_Code;
                    owner.Post_Code = owners.Post_Code;
                    owner.Country_Region_Code = owners.Country_Region_Code;
                    owner.County = owners.County;
                    owner.E_Mail = owners.E_Mail;
                    ownersdetails.Add(owner);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return Json(ownersdetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBeneficiaryDetails()
        {

            List<BeneficiarryModel> BeneficiaryDetails = new List<BeneficiarryModel>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorBeneficiaries.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var beneficiaries in query)
                {
                    BeneficiarryModel beneficiary = new BeneficiarryModel();
                    beneficiary.Entry_No = beneficiaries.Entry_No;
                    beneficiary.Name = beneficiaries.Name;
                    beneficiary.idType = beneficiaries.ID_Type;
                    beneficiary.idpassportNumber = beneficiaries.ID_Passport_No;
                    beneficiary.Phonenumber = Convert.ToInt32(beneficiaries.Phone_No);
                    beneficiary.Email = beneficiaries.Email;
                    beneficiary.AllocatedShares = Convert.ToDecimal(beneficiaries.Allocated_Shares);
                    beneficiary.BeneficiaryType = beneficiaries.Type;
                    BeneficiaryDetails.Add(beneficiary);
                }


            }
            catch (Exception e)
            {

                throw;
            }

            return Json(BeneficiaryDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVendorLitigationHistory()
        {
            List<LitigationModel> litigationdetails = new List<LitigationModel>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorLitigationHistory.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var litigations in query)
                {
                    LitigationModel litigation = new LitigationModel();
                    litigation.Entry_No = litigations.Entry_No;
                    litigation.DisputeDescription = litigations.Dispute_Matter;
                    litigation.CategoryofDispute = litigations.Category_of_Matter;
                    litigation.Year = litigations.Year;
                    litigation.TheotherDisputeparty = litigations.Other_Dispute_Party;
                    litigation.DisputeAmount = litigations.Dispute_Amount_LCY;
                    litigation.Thirdparty = litigations.V3rd_Party_Entity;
                    litigation.AwardType = litigations.Award_Type;
                    litigationdetails.Add(litigation);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return Json(litigationdetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVendorPastExperience()
        {
            List<PastXprModel> pastexperiencedetails = new List<PastXprModel>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorPastExperience.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var pastexperiences in query)
                {
                    PastXprModel pastexperience = new PastXprModel();
                    pastexperience.ClientName = pastexperiences.Client_Name;
                    pastexperience.Entry_No = pastexperiences.Entry_No;
                    pastexperience.Address = pastexperiences.Address;
                    pastexperience.Assignment_Name = pastexperiences.Assignment_Name;
                    pastexperience.ProjectScope = pastexperiences.Project_Scope_Summary;
                    pastexperience.ProjectStartDate = Convert.ToString(pastexperiences.Assignment_Start_Date);
                    pastexperience.ProjectEndDate = Convert.ToString(pastexperiences.Assignment_End_Date);
                    pastexperience.Country = pastexperiences.Country_Region_Code;
                    pastexperience.ProjectValue = pastexperiences.Assignment_Value_LCY;
                    pastexperience.Contract_Ref = pastexperiences.Contract_Ref_No;
                    pastexperience.Delivery_Location = pastexperiences.Delivery_Location;
                    pastexperience.Contact_Person = pastexperiences.Primary_Contact_Person;
                    pastexperiencedetails.Add(pastexperience);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(pastexperiencedetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBidEquipmentsDetails(string BidResponseNumber)
        {
            List<BidEquipmentsSpecificationModel> equipmentsdetails = new List<BidEquipmentsSpecificationModel>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.BidEquipmentSpecifications.Where(x => x.No == BidResponseNumber).ToList();
                foreach (var equipments in query)
                {
                    BidEquipmentsSpecificationModel equipment = new BidEquipmentsSpecificationModel();
                    equipment.No = equipments.No;
                    equipment.Equipment_Type_Code = Convert.ToString(equipments.Equipment_Type_Code);
                    equipment.Ownership_Type = equipments.Ownership_Type;
                    equipment.Equipment_Serial = equipments.Equipment_Serial;
                    equipment.Equipment_Condition_Code = equipments.Equipment_Condition_Code;
                    equipment.Equipment_Usability_Code = equipments.Equipment_Usability_Code;
                    equipment.Qty_of_Equipment = Convert.ToDecimal(equipments.Qty_of_Equipment);
                    equipment.Description = equipments.Description;
                    equipment.Years_of_Previous_Use = Convert.ToString(equipments.Years_of_Previous_Use);
                    equipment.Entry_No = Convert.ToString(equipments.Entry_No);
                    equipmentsdetails.Add(equipment);
                }
             }
            catch (Exception e)
            {

                throw;
            }
            return Json(equipmentsdetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVendorBideResponsPastExperience(string BidResponseNumber)
        {
            List<BidPastExperienceModel> pastexperiencedetails = new List<BidPastExperienceModel>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.BidPastExperiences.Where(x => x.No == BidResponseNumber && x.Vendor_No == vendorNo).ToList();
                foreach (var pastexperiences in query)
                {
                    BidPastExperienceModel pastexperience = new BidPastExperienceModel();
                    pastexperience.No = pastexperiences.No;
                    pastexperience.Address = Convert.ToString(pastexperiences.Address);
                    pastexperience.Vendor_No = pastexperiences.Vendor_No;
                    pastexperience.Client_Name = Convert.ToString(pastexperiences.Client_Name);
                    pastexperience.City = Convert.ToString(pastexperiences.City);
                    pastexperience.Address_2 = Convert.ToString(pastexperiences.Address_2);
                    pastexperience.Phone_No = Convert.ToString(pastexperiences.Phone_No);
                    pastexperience.Nationality_ID = Convert.ToString(pastexperiences.Nationality_ID);
                    pastexperience.Citizenship_Type = Convert.ToString(pastexperiences.Citizenship_Type);
                    pastexperience.Entity_Ownership = Convert.ToString(pastexperiences.Entity_Ownership);
                    pastexperience.Share_Types = Convert.ToString(pastexperiences.Share_Types);
                    pastexperience.No_of_Shares = Convert.ToString(pastexperiences.No_of_Shares);
                    pastexperience.Nominal_Value_Share = Convert.ToString(pastexperiences.Nominal_Value_Share);
                    pastexperience.Total_Nominal_Value = Convert.ToString(pastexperiences.Total_Nominal_Value);
                    pastexperience.Ownership_Effective_Date = Convert.ToString(pastexperiences.Ownership_Effective_Date);
                    pastexperience.Country_Region_Code = Convert.ToString(pastexperiences.Country_Region_Code);
                    pastexperience.Post_Code = Convert.ToString(pastexperiences.Post_Code);
                    pastexperience.County = Convert.ToString(pastexperiences.County);
                    pastexperience.E_Mail = Convert.ToString(pastexperiences.E_Mail);
                    pastexperience.Blocked = Convert.ToString(pastexperiences.Blocked);
                    pastexperience.No_Series = Convert.ToString(pastexperiences.No_Series);
                    pastexperience.Primary_Contact_Person = Convert.ToString(pastexperiences.Primary_Contact_Person);
                    pastexperience.Primary_Contact_Tel = Convert.ToString(pastexperiences.Primary_Contact_Tel);
                    pastexperience.Primary_Contact_Designation = Convert.ToString(pastexperiences.Primary_Contact_Designation);
                    pastexperience.Primary_Contact_Email = Convert.ToString(pastexperiences.Primary_Contact_Email);
                    pastexperience.Project_Scope_Summary = Convert.ToString(pastexperiences.Project_Scope_Summary);
                    pastexperience.Delivery_Location = Convert.ToString(pastexperiences.Delivery_Location);
                    pastexperience.Contract_Ref_No = Convert.ToString(pastexperiences.Contract_Ref_No);
                    pastexperience.Assignment_Start_Date = Convert.ToString(pastexperiences.Assignment_Start_Date);
                    pastexperience.Assignment_End_Date = Convert.ToString(pastexperiences.Assignment_End_Date);
                    pastexperience.Assignment_Value_LCY = Convert.ToString(pastexperiences.Assignment_Value_LCY);
                    pastexperience.Assignment_Status = Convert.ToString(pastexperiences.Assignment_Status);
                    pastexperience.Project_Completion_Value = Convert.ToString(pastexperiences.Project_Completion_Value);
                    pastexperience.Project_Completion_Work = Convert.ToString(pastexperiences.Project_Completion_Work);
                    pastexperience.Assignment_Project_Name = Convert.ToString(pastexperiences.Assignment_Project_Name);
                    pastexperiencedetails.Add(pastexperience);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return Json(pastexperiencedetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBidResponsePricingDetails(string BidResponseNumber)
        {
            List<BidResponseItemLinesModel> list = new List<BidResponseItemLinesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidResponseItemLines.Where(x => x.Document_No == BidResponseNumber).ToList();
                foreach (var prices in query)
                {
                    BidResponseItemLinesModel price = new BidResponseItemLinesModel();
                    price.No = prices.No;
                    price.Document_Type = Convert.ToString(prices.Document_Type);
                    price.Buy_from_Vendor_No = prices.Buy_from_Vendor_No;
                    price.Document_No = Convert.ToString(prices.Document_No);
                    price.Line_No = Convert.ToString(prices.Line_No);
                    price.Type = Convert.ToString(prices.Type);
                    price.Location_Code = Convert.ToString(prices.Location_Code);
                    price.Expected_Receipt_Date = Convert.ToString(prices.Expected_Receipt_Date);
                    price.Description = Convert.ToString(prices.Description);
                    price.Description_2 = Convert.ToString(prices.Description_2);
                    price.Unit_of_Measure = Convert.ToString(prices.Unit_of_Measure);
                    price.Quantity = Convert.ToString(prices.Quantity);
                    price.Amount = Convert.ToString(prices.Amount);
                    price.Amount_Including_VAT = Convert.ToString(prices.Amount_Including_VAT);
                    price.Unit_Price_LCY = Convert.ToString(prices.Unit_Price_LCY);
                    price.Direct_Unit_Cost = Convert.ToString(prices.Direct_Unit_Cost);
                    price.VAT = Convert.ToString(prices.VAT);
                    list.Add(price);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBidResponseIncomeStatementDetails(string BidResponseNumber)
        {
            List<BidResponseAuditIncomeStatements> list = new List<BidResponseAuditIncomeStatements>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.BidAuditedIncomeStatement.Where(x => x.No == BidResponseNumber && x.Vendor_No == vendorNo).ToList();
                foreach (var incomestatements in query)
                {
                    BidResponseAuditIncomeStatements incomestatement = new BidResponseAuditIncomeStatements();
                    incomestatement.No = incomestatements.No;
                    incomestatement.Audit_Year_Code_Reference = Convert.ToString(incomestatements.Audit_Year_Code_Reference);
                    incomestatement.Vendor_No = incomestatements.Vendor_No;
                    incomestatement.Total_Revenue_LCY = Convert.ToString(incomestatements.Total_Revenue_LCY);
                    incomestatement.Total_COGS_LCY = Convert.ToString(incomestatements.Total_COGS_LCY);
                    incomestatement.Gross_Margin_LCY = Convert.ToString(incomestatements.Gross_Margin_LCY);
                    incomestatement.Total_Operating_Expenses_LCY = Convert.ToString(incomestatements.Total_Operating_Expenses_LCY);
                    incomestatement.Operating_Income_EBIT_LCY = Convert.ToString(incomestatements.Operating_Income_EBIT_LCY);
                    incomestatement.Other_Non_operating_Re_Exp_LCY = Convert.ToString(incomestatements.Other_Non_operating_Re_Exp_LCY);
                    incomestatement.Interest_Expense_LCY = Convert.ToString(incomestatements.Interest_Expense_LCY);
                    incomestatement.Income_Before_Taxes_LCY = Convert.ToString(incomestatements.Income_Before_Taxes_LCY);
                    incomestatement.Income_Tax_Expense_LCY = Convert.ToString(incomestatements.Income_Tax_Expense_LCY);
                    incomestatement.Net_Income_from_Ops_LCY = Convert.ToString(incomestatements.Net_Income_from_Ops_LCY);
                    incomestatement.Below_the_line_Items_LCY = Convert.ToString(incomestatements.Below_the_line_Items_LCY);
                    incomestatement.Net_Income = Convert.ToString(incomestatements.Net_Income);
                    incomestatement.Document_Type = Convert.ToString(incomestatements.Document_Type);
                    list.Add(incomestatement);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTenderSecurityDetails(string BidResponseNumber)
        {
            List<BidResponseContractSecurity> list = new List<BidResponseContractSecurity>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.BidContractsSecurities.Where(x => x.No == BidResponseNumber && x.Vendor_No == vendorNo).ToList();
                foreach (var securities in query)
                {
                    BidResponseContractSecurity security = new BidResponseContractSecurity();
                    security.No = securities.No;
                    security.Document_Type = Convert.ToString(securities.Document_Type);
                    security.IFS_Code = securities.IFS_No;
                    security.Document_Type = Convert.ToString(securities.Document_Type);
                    security.Vendor_No = Convert.ToString(securities.Vendor_No);
                    security.Security_Type = Convert.ToString(securities.Security_Type);
                    security.Issuer_Institution_Type = Convert.ToString(securities.Issuer_Institution_Type);
                    security.Issuer_Registered_Offices = Convert.ToString(securities.Issuer_Registered_Offices);
                    security.Description = Convert.ToString(securities.Description);
                    security.Security_Amount_LCY = Convert.ToString(securities.Security_Amount_LCY);
                    security.Bid_Security_Effective_Date = Convert.ToString(securities.Bid_Security_Effective_Date);
                    security.Bid_Security_Validity_Expiry = Convert.ToString(securities.Bid_Security_Validity_Expiry);
                    security.Security_ID = Convert.ToString(securities.Security_ID);
                    security.Security_Closure_Date = Convert.ToString(securities.Security_Closure_Date);
                    security.Security_Closure_Voucher_No = Convert.ToString(securities.Security_Closure_Voucher_No);
                    security.Security_Closure_Type = Convert.ToString(securities.Security_Closure_Type);
                    security.Form_of_Security = Convert.ToString(securities.Form_of_Security);
                    security.Issuer_Guarantor_Name = Convert.ToString(securities.Issuer_Guarantor_Name);
                    list.Add(security);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBidResponseBalanceSheetDetails(string BidResponseNumber)
        {
            List<BidResponseAuditBalanceSheet> list = new List<BidResponseAuditBalanceSheet>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.BidAuditedBalanaceSheet.Where(x => x.No == BidResponseNumber && x.Vendor_No == vendorNo).ToList();
                foreach (var balancesheets in query)
                {
                    BidResponseAuditBalanceSheet balancesheet = new BidResponseAuditBalanceSheet();
                    balancesheet.No = balancesheets.No;
                    balancesheet.Audit_Year_Code_Reference = Convert.ToString(balancesheets.Audit_Year_Code_Reference);
                    balancesheet.Vendor_No = balancesheets.Vendor_No;
                    balancesheet.Current_Assets_LCY = Convert.ToString(balancesheets.Current_Assets_LCY);
                    balancesheet.Fixed_Assets_LCY = Convert.ToString(balancesheets.Fixed_Assets_LCY);
                    balancesheet.Total_Assets_LCY = Convert.ToString(balancesheets.Total_Assets_LCY);
                    balancesheet.Current_Liabilities_LCY = Convert.ToString(balancesheets.Current_Liabilities_LCY);
                    balancesheet.Long_term_Liabilities_LCY = Convert.ToString(balancesheets.Long_term_Liabilities_LCY);
                    balancesheet.Total_Liabilities_LCY = Convert.ToString(balancesheets.Total_Liabilities_LCY);
                    balancesheet.Debt_Ratio = Convert.ToString(balancesheets.Debt_Ratio);
                    balancesheet.Working_Capital_LCY = Convert.ToString(balancesheets.Working_Capital_LCY);
                    balancesheet.Owners_Equity_LCY = Convert.ToString(balancesheets.Owners_Equity_LCY);
                    balancesheet.Current_Ratio = Convert.ToString(balancesheets.Current_Ratio);
                    balancesheet.Assets_To_Equity_Ratio = Convert.ToString(balancesheets.Assets_To_Equity_Ratio);
                    balancesheet.Debt_To_Equity_Ratio = Convert.ToString(balancesheets.Debt_To_Equity_Ratio);
                    list.Add(balancesheet);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBalanceSheetDetails()
        {
            List<BalanceSheetTModel> list = new List<BalanceSheetTModel>();
            try
            {

                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.vendorBalancesheet.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var balance in query)
                {
                    BalanceSheetTModel balancesheet = new BalanceSheetTModel();
                    balancesheet.Audit_Year_Code_Reference = balance.Audit_Year_Code_Reference;
                    balancesheet.Current_Assets_LCY = balance.Current_Assets_LCY;
                    balancesheet.Fixed_Assets_LCY = balance.Fixed_Assets_LCY;
                    balancesheet.Total_Assets_LCY = balance.Total_Assets_LCY;
                    balancesheet.Current_Liabilities_LCY = balance.Current_Liabilities_LCY;
                    balancesheet.Long_term_Liabilities_LCY = balance.Long_term_Liabilities_LCY;
                    balancesheet.Total_Liabilities_LCY = balance.Total_Liabilities_LCY;
                    balancesheet.Owners_Equity_LCY = balance.Owners_Equity_LCY;
                    balancesheet.Debt_Ratio = balance.Debt_Ratio;
                    balancesheet.Working_Capital_LCY = balance.Working_Capital_LCY;
                    balancesheet.Assets_To_Equity_Ratio = balance.Assets_To_Equity_Ratio;
                    balancesheet.Debt_To_Equity_Ratio = balance.Debt_To_Equity_Ratio;
                    list.Add(balancesheet);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIncomeStatementSheetDetails()
        {
            List<IncomeStatementTModel> list = new List<IncomeStatementTModel>();
            try
            {

                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.vendorIncomestatement.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var incomestatement in query)
                {
                    IncomeStatementTModel income = new IncomeStatementTModel();
                    income.Audit_Year_Code_Reference = incomestatement.Audit_Year_Code_Reference;
                    income.Total_Revenue_LCY = incomestatement.Total_Revenue_LCY;
                    income.Total_COGS_LCY = incomestatement.Total_COGS_LCY;
                    income.Gross_Margin_LCY = incomestatement.Gross_Margin_LCY;
                    income.Total_Operating_Expenses_LCY = incomestatement.Total_Operating_Expenses_LCY;
                    income.Operating_Income_EBIT_LCY = incomestatement.Operating_Income_EBIT_LCY;
                    income.Other_Non_operating_Re_Exp_LCY = incomestatement.Other_Non_operating_Re_Exp_LCY;
                    income.Interest_Expense_LCY = incomestatement.Interest_Expense_LCY;
                    income.Income_Before_Taxes_LCY = incomestatement.Income_Before_Taxes_LCY;
                    income.Income_Tax_Expense_LCY = incomestatement.Income_Tax_Expense_LCY;
                    income.Net_Income_from_Ops_LCY = incomestatement.Net_Income_from_Ops_LCY;
                    income.Net_Income = incomestatement.Net_Income;
                    list.Add(income);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierRgistrationDocuments()
        {
            List<RegistrationDocumentsModel> list = new List<RegistrationDocumentsModel>();
            try
            {

                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorFiledRegDocument.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var documents in query)
                {
                    RegistrationDocumentsModel document = new RegistrationDocumentsModel();
                    document.Certificate_No = documents.Certificate_No;
                    document.Procurement_Document_Type = documents.Procurement_Document_Type_ID;
                    document.Procurement_Document_Type_ID = documents.Procurement_Document_Type_ID;
                    document.Description = documents.Description;
                    document.Issue_Date = Convert.ToString(documents.Issue_Date);
                    document.Expiry_Date = Convert.ToString(documents.Expiry_Date);
                    document.File_Name = documents.File_Name;
                    document.Date_Filed = Convert.ToString(documents.Date_Filed);
                    list.Add(document);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPrequalificationsDocuments( string PrequalificationNumber)
        {
            List<PreQualificationRequiredDocuments> list = new List<PreQualificationRequiredDocuments>();
            try
            {

                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.RFIResponseDocuments.Where(x =>x.Document_No== PrequalificationNumber&& x.Vendor_No == vendorNo&&x.Procurement_Process== "Prequalification").ToList();
                foreach (var documents in query)
                {
                    PreQualificationRequiredDocuments document = new PreQualificationRequiredDocuments();
                    document.Certificate_No = documents.Certificate_No;
                    document.Procurement_Document_Type = documents.Procurement_Document_Type_ID;
                    document.Procurement_Document_Type_ID = documents.Procurement_Document_Type_ID;
                    document.Description = documents.Document_Description;
                    document.Issue_Date = Convert.ToString(documents.Issue_Date);
                    document.Expiry_Date = Convert.ToString(documents.Expiry_Date);
                    document.File_Name = documents.File_Name;
                    document.Date_Filed = Convert.ToString(documents.Date_Filed);
                    list.Add(document);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterSupplier(VendorModel vendormodel)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
               
                var nav = new NavConnection().ObjNav();
                DateTime myOpsdate, myIncopdate;
                CultureInfo usCulture = new CultureInfo("es-ES");
                myOpsdate = DateTime.Parse(vendormodel.OpsDate, usCulture.DateTimeFormat);
                myIncopdate = DateTime.Parse(vendormodel.DateofIncorporation, usCulture.DateTimeFormat);

                DateTime effectivedate, expirydate;               
                effectivedate = DateTime.Parse(vendormodel.Certificate_Effective_Date, usCulture.DateTimeFormat);
                expirydate = DateTime.Parse(vendormodel.Certificate_Expiry_Date, usCulture.DateTimeFormat);

                bool haveAgpo = false;
                if (vendormodel.Haveagpo == "0")
                {
                    haveAgpo = false;
                }
                else if (vendormodel.Haveagpo == "1")
                {
                    haveAgpo = true;
                }

                var status = nav.FnSupplierRegistration(vendormodel.BusinessType, vendormodel.VendorType, vendormodel.OwnerType, myIncopdate, myOpsdate, vendormodel.LanguageCode, vendorNo, vendormodel.CertofIncorporation, vendormodel.Certifcate_No, vendormodel.Registered_Specia_Group,
                vendormodel.Products_Service_Category, effectivedate, expirydate,haveAgpo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult BusinessProfile(VendorModel vendormodel)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();

                if (vendormodel.Vision == null)
                {
                    vendormodel.Vision = " ";
                }
                if (vendormodel.Mision == null)
                {
                    vendormodel.Mision = " ";
                }
                if (vendormodel.IndustryGroup == null)
                {
                    vendormodel.IndustryGroup = " ";
                }
                if (vendormodel.PhysicalLocation == null)
                {
                    vendormodel.PhysicalLocation = "";
                }

                var nav = new NavConnection().ObjNav();
               
                var status = nav.FnSupplierBusinessProfile(vendormodel.IndustryGroup,vendormodel.CompanySize,vendormodel.NominalCap,vendormodel.Vision,vendormodel.Mision,vendormodel.PhysicalLocation,vendormodel.MaxBizValue,vendormodel.MobileNo,vendormodel.NatureofBz, vendorNo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult CommunicationDetails(VendorModel vendormodel)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();

                if (vendormodel.WebUrl == null)
                {
                    vendormodel.WebUrl = " ";
                }
                //if (vendormodel.HouseNo == null)
                //{
                //    vendormodel.HouseNo = " ";
                //}
                if (vendormodel.Fax == null)
                {
                    vendormodel.Fax = " ";
                }
                if (vendormodel.WebUrl == null)
                {
                    vendormodel.WebUrl = " ";
                }
                if (vendormodel.PlotNo == null)
                {
                    vendormodel.PlotNo = " ";
                }
                //if (vendormodel.StreetorRoad == null)
                //{
                //    vendormodel.StreetorRoad = " ";
                //}





                var nav = new NavConnection().ObjNav();
               
                CultureInfo usCulture = new CultureInfo("es-ES");
              

                var status = nav.FnSupplierCommunicationDetails(vendormodel.PostaCode, vendormodel.CountryofOrigin,vendormodel.PoBox, vendormodel.PostaCity, vendormodel.WebUrl,vendormodel.TelephoneNo, vendormodel.HouseNo,vendormodel.FloorNo,vendormodel.PlotNo,vendormodel.StreetorRoad,vendormodel.Fax ,vendorNo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterSupplierBankDetails(BankModel bankmodel)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var vendorName = Session["name"].ToString();
                var nav = new NavConnection().ObjNav();
                
                if (bankmodel.BankName == null)
                {
                    bankmodel.BankName = "";
                }

                if (bankmodel.Post_Code == null)
                {
                    bankmodel.Post_Code = "";
                }

                if (bankmodel.Phone_No == null)
                {
                    bankmodel.Phone_No = "";
                }

                var status = nav.FnInsertBankDetails(vendorNo, bankmodel.BankCode, bankmodel.BankName, bankmodel.CurrencyCode, bankmodel.BankAccountNo, bankmodel.Bank_Branch_No, bankmodel.Phone_No, bankmodel.CountryCode, bankmodel.Post_Code);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult DeleteBankDetails(string bankcode)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();

                var status = nav.FnDeleteBank(vendorNo, bankcode);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*"+ res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*"+ res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*"+ ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult DeleteBidSecurityDetails(string bidresponsNumber, string securityid)
        {
           
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var securityNumber = Convert.ToInt32(securityid);
                    var nav = new NavConnection().ObjNav();
                    var status = nav.FnDeleteBidResponTenderSecurity(vendorNo, bidresponsNumber, securityNumber);
                    var res = status.Split('*');
                    switch (res[0])
                    {
                        case "success":
                            return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                        default:
                            return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

                }
            
         

        }
        
        public JsonResult DeleteKeyProffessionalStaffDetails(string staffnumber)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteStaffExperience(vendorNo, staffnumber);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult DeleteBidResponseStaffDetails(int staffnumber)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteBidResponsePersonel(vendorNo, staffnumber);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeleteEquipmentDetails(string serialnumber)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteBidResponseEquipmentDetails(vendorNo, serialnumber);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeleteShareholdersDetails(int shareholderCode)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteShareholder(vendorNo, shareholderCode);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult DeleteBeneficiaryDetails(int beneficiaryCode)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteBeneficiary(vendorNo, beneficiaryCode);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeleteLitigationHistoryDetails(int litigationCode)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteLitigationHistoryDetails(vendorNo, litigationCode);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeleteResponsibilityCenter(String regionCode)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteResponsibilityCenter(vendorNo, regionCode);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeletePastExperienceDetails(int pastExperienceCode)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeletePastExperienceDetails(vendorNo, pastExperienceCode);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeleteBalanceSheetDetails(string yearCode)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteBalanceSheetDetails(vendorNo, yearCode);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeletePastExperienceDetailsDetails(string BidRespNumber)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteBidResponsPastExperienceDetails(vendorNo, BidRespNumber);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeleteBidResponseBalanceSheetDetails(string BidRespNumber, string yearCode)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteBidResponsBalanceSheetDetails(vendorNo, yearCode, BidRespNumber);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeleteIncomeStatementDetails(string yearCode)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteIncomeStatementDetails(vendorNo, yearCode);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeleteBidResponseIncomeStatementDetails(string BidRespNumber, string yearCode)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteBidResponsIncomeStatamentDetails(vendorNo, yearCode, BidRespNumber);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult DeleteBidTenderSecurityDetails(string BidRespNumber, string Securityid)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var securityNumber = Convert.ToInt32(Securityid);
                var nav = new NavConnection().ObjNav();
                var status = nav.FnDeleteBidResponTenderSecurity(vendorNo, BidRespNumber, securityNumber);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterBidSecurityDetails(BidResponseContractSecurity bidsecurityModel,HttpPostedFileBase browsedfile)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var bidresponsenumber = Session["BideResponseNumber"].ToString();
                DateTime validity, effectivedate;
                CultureInfo usCulture = new CultureInfo("es-ES");
                validity = DateTime.Parse(bidsecurityModel.Bid_Security_Validity_Expiry, usCulture.DateTimeFormat);
                effectivedate = DateTime.Parse(bidsecurityModel.Bid_Security_Effective_Date, usCulture.DateTimeFormat);
                var nav = new NavConnection().ObjNav();
                var status = nav.FnInsertBidResponseSecurityDetails(vendorNo, bidsecurityModel.No, bidsecurityModel.IFS_No,
                    bidsecurityModel.Form_of_Security, Convert.ToInt32(bidsecurityModel.Issuer_Institution_Type), Convert.ToInt32(bidsecurityModel.Security_Type), bidsecurityModel.Issuer_Guarantor_Name,
                   bidsecurityModel.Issuer_Registered_Offices, bidsecurityModel.Description, Convert.ToDecimal(bidsecurityModel.Security_Amount_LCY), effectivedate, validity);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterSupplierShareholderDetails(ShareholderModel shareholderModel)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();

                if (shareholderModel.registrationNumber == null)
                {
                    shareholderModel.registrationNumber = "";
                }
                if (shareholderModel.entityType == null)
                {
                    shareholderModel.entityType = "0";
                }
                if (shareholderModel.Company_Type == null)
                {
                    shareholderModel.Company_Type = "";
                }
                if (shareholderModel.kraPin == null)
                {
                    shareholderModel.kraPin = "";
                }
                if (shareholderModel.ID_Passport_No == null)
                {
                    shareholderModel.ID_Passport_No = "";
                }
                if (shareholderModel.Nationality_ID == null)
                {
                    shareholderModel.Nationality_ID = "";
                }


                var status = nav.FnInsertDirectorDetails(vendorNo, shareholderModel.Name, shareholderModel.ID_Passport_No, Convert.ToInt32(shareholderModel.Citizenship_Type),
                   Convert.ToDecimal(shareholderModel.Entity_Ownership), shareholderModel.Phone_No, shareholderModel.Address, shareholderModel.E_Mail, shareholderModel.Nationality_ID,shareholderModel.shareholdersDetails,
                   shareholderModel.registrationNumber,shareholderModel.kraPin,shareholderModel.entityType, Convert.ToInt32(shareholderModel.Company_Type));
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterSupplierBeneficiaries(BeneficiarryModel beneficiaryModel)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();

                var status = nav.FnInsertBeneficiaries(vendorNo, beneficiaryModel.Name,Convert.ToInt32(beneficiaryModel.BeneficiaryType),Convert.ToInt32(beneficiaryModel.idType),
                   beneficiaryModel.idpassportNumber, beneficiaryModel.Phonenumber, beneficiaryModel.Email, beneficiaryModel.AllocatedShares);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterLitigationHistoryDetails(LitigationModel litigationmodels)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();

                var status = nav.FnInsertLitigationHistoryDetails(vendorNo, litigationmodels.DisputeDescription, Convert.ToInt32(litigationmodels.CategoryofDispute), litigationmodels.Year,
                 litigationmodels.TheotherDisputeparty, Convert.ToDecimal(litigationmodels.DisputeAmounts), Convert.ToInt32(litigationmodels.AwardType));
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterPastExperienceDetails(PastExperienceModel pastexperience)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                DateTime startdate, enddate;
                CultureInfo usCulture = new CultureInfo("es-ES");
                startdate = DateTime.Parse(pastexperience.Assignment_Start_Date, usCulture.DateTimeFormat);
                enddate = DateTime.Parse(pastexperience.Assignment_End_Date, usCulture.DateTimeFormat);
                var nav = new NavConnection().ObjNav();
                var status = nav.FnInsertPastExperienceDetails(vendorNo, pastexperience.Client_Name, pastexperience.Address, pastexperience.Assignment_Project_Name,
                 pastexperience.Project_Scope_Summary, startdate, enddate, Convert.ToDecimal(pastexperience.Assignment_Value_LCY));
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterBalanceSheetDetails(BalanceSheetTModel balancesheet)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnInsertBalanceSheet(balancesheet.Audit_Year_Code_Reference, Convert.ToDecimal(balancesheet.Total_Assets_LCY), Convert.ToDecimal(balancesheet.Fixed_Assets_LCY),
                 Convert.ToDecimal(balancesheet.Current_Liabilities_LCY), Convert.ToDecimal(balancesheet.Long_term_Liabilities_LCY), Convert.ToDecimal(balancesheet.Owners_Equity_LCY), vendorNo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult BidResponseBalanceSheetDetails(BalanceSheetTModel balancesheet)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnBidResponseInsertBalanceSheet(balancesheet.No, balancesheet.Audit_Year_Code_Reference, Convert.ToDecimal(balancesheet.Total_Assets_LCY), Convert.ToDecimal(balancesheet.Fixed_Assets_LCY),
                 Convert.ToDecimal(balancesheet.Current_Liabilities_LCY), Convert.ToDecimal(balancesheet.Long_term_Liabilities_LCY), Convert.ToDecimal(balancesheet.Owners_Equity_LCY), vendorNo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult BidResponsePriceDetails(BidResponseItemLinesModel pricedetails)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnInsertPurchaseLinesDetails(vendorNo, pricedetails.Document_No, Convert.ToInt32(pricedetails.Line_No), pricedetails.No,
                Convert.ToDecimal(pricedetails.Direct_Unit_Cost), Convert.ToDecimal(pricedetails.Quantity));
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult ViewPrequalificationsConstituencies()
        {
            List<ConstituenciesModel> constituencies = new List<ConstituenciesModel>();
            try
            {


            }
            catch (Exception e)
            {

                throw;
            }
            return View(constituencies);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult BidResponsePastExperienceDetails(BidPastExperienceModel pastexperience)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                DateTime startdate, enddate;
                CultureInfo usCulture = new CultureInfo("es-ES");
                startdate = DateTime.Parse(pastexperience.Assignment_Start_Date, usCulture.DateTimeFormat);
                enddate = DateTime.Parse(pastexperience.Assignment_End_Date, usCulture.DateTimeFormat);
                var nav = new NavConnection().ObjNav();
                var status = nav.FnBidResponsePastExperience(pastexperience.No, pastexperience.Client_Name, pastexperience.Address, pastexperience.Phone_No,
                 pastexperience.Country_Region_Code, pastexperience.Primary_Contact_Email, pastexperience.Project_Scope_Summary, pastexperience.Assignment_Project_Name,
                 pastexperience.Contract_Ref_No, Convert.ToDecimal(pastexperience.Assignment_Value_LCY), Convert.ToInt32(pastexperience.Assignment_Status), startdate, enddate, vendorNo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult BidResponseIncomeStatementDetails(IncomeStatementTModel incomestatement)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnBidResponseInsertIncomestatement(incomestatement.No, incomestatement.Audit_Year_Code_Reference, Convert.ToDecimal(incomestatement.Total_Revenue_LCY), Convert.ToDecimal(incomestatement.Total_COGS_LCY),
                 Convert.ToDecimal(incomestatement.Total_Operating_Expenses_LCY), Convert.ToDecimal(incomestatement.Other_Non_operating_Re_Exp_LCY), Convert.ToDecimal(incomestatement.Interest_Expense_LCY), vendorNo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterIncomeStatementDetails(IncomeStatementTModel incomestatement)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnInsertIncomestatement(incomestatement.Audit_Year_Code_Reference, Convert.ToDecimal(incomestatement.Total_Revenue_LCY), Convert.ToDecimal(incomestatement.Total_COGS_LCY),
                 Convert.ToDecimal(incomestatement.Total_Operating_Expenses_LCY), Convert.ToDecimal(incomestatement.Other_Non_operating_Re_Exp_LCY), Convert.ToDecimal(incomestatement.Interest_Expense_LCY), vendorNo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterSpecialGroupDetails(VendorSpecialGroupModel specialgroup)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                DateTime effectivedate, expirydate;
                CultureInfo usCulture = new CultureInfo("es-ES");
                effectivedate = DateTime.Parse(specialgroup.Certificate_Effective_Date, usCulture.DateTimeFormat);
                expirydate = DateTime.Parse(specialgroup.Certificate_Expiry_Date, usCulture.DateTimeFormat);
                var nav = new NavConnection().ObjNav();
                var status = nav.FnAddVendorSpecialGroupDetails(vendorNo, specialgroup.Certifcate_No, specialgroup.Registered_Specia_Group,
                specialgroup.Products_Service_Category, effectivedate, expirydate);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterKeyPersonnelDetails(StaffEntryTModel staffmodel)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                CultureInfo usCulture = new CultureInfo("es-ES");
                var stfDateofbirth = DateTime.Parse(staffmodel.StaffDateofBirth, usCulture.DateTimeFormat);
                var stfJoiningDate = DateTime.Parse(staffmodel.StaffJoiningDate, usCulture.DateTimeFormat);

                var status = nav.FnInsertStaffEntry(vendorNo, staffmodel.StaffName, staffmodel.StaffProfession, staffmodel.StaffDesignation, staffmodel.StaffPhonenumber,
                staffmodel.StaffNationality, stfDateofbirth, staffmodel.StaffEmail, stfJoiningDate, staffmodel.StaffYearswithfirm, staffmodel.StaffNumber,staffmodel.employmentType);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult SupplierProfile()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Vendors = GetVendors(vendorNo);
                model.BanksDetails = GetBanks(vendorNo);
                model.StakeholdersDetails = GetStakeholders(vendorNo);
                model.Litigations =GetVendorLitigationHistoryDetails(vendorNo);
                model.VendorPastExperience = GetVendorPastExeprience(vendorNo);
                model.Vendorbalancesheet = GetVendorBalanaceDetails(vendorNo);
                model.Vendorincomestatement = GetVendorIncomeStatementDetails(vendorNo);
                model.VendorProfessionalStaff = GetVendorProfessionalStaff(vendorNo);
                model.AttachedDocuments = PopulateSupplierRegistrationDocuments(vendorNo);
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult ViewSingleAddendumNotice(string AddendumNumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Addendum = GetAddendumDetails(AddendumNumber);
                
                model.Summary = GetAddendumAmmendmentDetails(AddendumNumber);
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult RespondTenderWizard(string respondtendernumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["BideResponseNumber"] == null)
            { 
                    return RedirectToAction("ActiveTenderNotices", "Home");
             }
          
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                var BidResponseNumber = Session["BideResponseNumber"].ToString();
                dynamic model = new ExpandoObject();
                ViewBag.prequalificationNo = BidResponseNumber;
                model.BidDetails = GetBidResponseDetails(BidResponseNumber, vendorNo);
                model.BidEquipments = GetBidResponseEquipments(BidResponseNumber);
                model.BidBalnaceSheet = GetBidResponseBalanceSheet(BidResponseNumber, vendorNo);
                model.BidIncomeStatement = GetBidResponseIncomeStatement(BidResponseNumber, vendorNo);
                model.BidPastExperiencent = GetBidResponsePastExperience(BidResponseNumber, vendorNo);
                model.BidPersonnel = GetBidResponsePersonnel(BidResponseNumber);
                model.BidPricinginformation = GetBidResponsePricingInformation(BidResponseNumber);
                model.RequiredDocuments = GetRequiredTenderDocuments(respondtendernumber);
                model.RequredDocuemnts = GetIFSRequiredEquipments(respondtendernumber);
                model.TenderskeystaffDetails = GetKeyStaffTenderPersonnel(respondtendernumber);
                model.BidSecurity = GetBidSecurityResponse(BidResponseNumber, vendorNo);
                model.AttachedBiddDocuments = GetBidAttachedDocumentsDetails(BidResponseNumber, vendorNo);

                return View(model);
            }
        }
        private static List<BidResponseContractSecurity> GetBidSecurityResponse(string BidResponseNumber, string vendorNo)
        {
            List<BidResponseContractSecurity> list = new List<BidResponseContractSecurity>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidContractsSecurities.Where(x => x.No == BidResponseNumber && x.Vendor_No == vendorNo).ToList();
                foreach (var securities in query)
                {
                    BidResponseContractSecurity security = new BidResponseContractSecurity();
                    security.No = securities.No;
                    security.Document_Type = Convert.ToString(securities.Document_Type);
                    security.IFS_Code = securities.IFS_No;
                    security.Document_Type = Convert.ToString(securities.Document_Type);
                    security.Vendor_No = Convert.ToString(securities.Vendor_No);
                    security.Security_Type = Convert.ToString(securities.Security_Type);
                    security.Issuer_Institution_Type = Convert.ToString(securities.Issuer_Institution_Type);
                    security.Issuer_Registered_Offices = Convert.ToString(securities.Issuer_Registered_Offices);
                    security.Description = Convert.ToString(securities.Description);
                    security.Security_Amount_LCY = Convert.ToString(securities.Security_Amount_LCY);
                    security.Bid_Security_Effective_Date = Convert.ToString(securities.Bid_Security_Effective_Date);
                    security.Bid_Security_Validity_Expiry = Convert.ToString(securities.Bid_Security_Validity_Expiry);
                    security.Security_ID = Convert.ToString(securities.Security_ID);
                    security.Security_Closure_Date = Convert.ToString(securities.Security_Closure_Date);
                    security.Security_Closure_Voucher_No = Convert.ToString(securities.Security_Closure_Voucher_No);
                    security.Security_Closure_Type = Convert.ToString(securities.Security_Closure_Type);
                    security.Form_of_Security = Convert.ToString(securities.Form_of_Security);
                    security.Issuer_Guarantor_Name = Convert.ToString(securities.Issuer_Guarantor_Name);
                    list.Add(security);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<BidResponseItemLinesModel> GetBidResponsePricingInformation(string BidResponseNumber)
        {
            List<BidResponseItemLinesModel> list = new List<BidResponseItemLinesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidResponseItemLines.Where(x => x.Document_No == BidResponseNumber).ToList();
                foreach (var prices in query)
                {
                    BidResponseItemLinesModel price = new BidResponseItemLinesModel();
                    price.No = prices.No;
                    price.BoqNumber = prices.BoQ_No;
                    price.ContactTyoe = prices.Contract_Type;
                    price.Document_Type = Convert.ToString(prices.Document_Type);
                    price.Buy_from_Vendor_No = prices.Buy_from_Vendor_No;
                    price.Document_No = Convert.ToString(prices.Document_No);
                    price.Line_No = Convert.ToString(prices.Line_No);
                    price.Type = Convert.ToString(prices.Type);
                    price.Location_Code = Convert.ToString(prices.Location_Code);
                    price.Expected_Receipt_Date = Convert.ToString(prices.Expected_Receipt_Date);
                    price.Description = Convert.ToString(prices.Description);
                    price.Description_2 = Convert.ToString(prices.Description_2);
                    price.Unit_of_Measure = Convert.ToString(prices.Unit_of_Measure);
                    price.Quantity = Convert.ToString(prices.Quantity);
                    price.Amount = Convert.ToString(prices.Amount);
                    price.Amount_Including_VAT = Convert.ToString(prices.Amount_Including_VAT);
                    price.Unit_Price_LCY = Convert.ToString(prices.Unit_Price_LCY);
                    price.Direct_Unit_Cost = Convert.ToString(prices.Direct_Unit_Cost_Inc_VAT);
                    price.VAT = Convert.ToString(prices.VAT);
                    list.Add(price);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }

        private static List<BidPastExperienceModel> GetBidResponsePastExperience(string BidResponseNumber, string vendorNo)
        {
            List<BidPastExperienceModel> list = new List<BidPastExperienceModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidPastExperiences.Where(x => x.No == BidResponseNumber && x.Vendor_No == vendorNo).ToList();
                foreach (var pastexperiences in query)
                {
                    BidPastExperienceModel pastexperience = new BidPastExperienceModel();
                    pastexperience.No = pastexperiences.No;
                    pastexperience.Address = Convert.ToString(pastexperiences.Address);
                    pastexperience.Vendor_No = pastexperiences.Vendor_No;
                    pastexperience.Client_Name = Convert.ToString(pastexperiences.Client_Name);
                    pastexperience.City = Convert.ToString(pastexperiences.City);
                    pastexperience.Address_2 = Convert.ToString(pastexperiences.Address_2);
                    pastexperience.Phone_No = Convert.ToString(pastexperiences.Phone_No);
                    pastexperience.Nationality_ID = Convert.ToString(pastexperiences.Nationality_ID);
                    pastexperience.Citizenship_Type = Convert.ToString(pastexperiences.Citizenship_Type);
                    pastexperience.Entity_Ownership = Convert.ToString(pastexperiences.Entity_Ownership);
                    pastexperience.Share_Types = Convert.ToString(pastexperiences.Share_Types);
                    pastexperience.No_of_Shares = Convert.ToString(pastexperiences.No_of_Shares);
                    pastexperience.Nominal_Value_Share = Convert.ToString(pastexperiences.Nominal_Value_Share);
                    pastexperience.Total_Nominal_Value = Convert.ToString(pastexperiences.Total_Nominal_Value);
                    pastexperience.Ownership_Effective_Date = Convert.ToString(pastexperiences.Ownership_Effective_Date);
                    pastexperience.Country_Region_Code = Convert.ToString(pastexperiences.Country_Region_Code);
                    pastexperience.Post_Code = Convert.ToString(pastexperiences.Post_Code);
                    pastexperience.County = Convert.ToString(pastexperiences.County);
                    pastexperience.E_Mail = Convert.ToString(pastexperiences.E_Mail);
                    pastexperience.Blocked = Convert.ToString(pastexperiences.Blocked);
                    pastexperience.No_Series = Convert.ToString(pastexperiences.No_Series);
                    pastexperience.Primary_Contact_Person = Convert.ToString(pastexperiences.Primary_Contact_Person);
                    pastexperience.Primary_Contact_Tel = Convert.ToString(pastexperiences.Primary_Contact_Tel);
                    pastexperience.Primary_Contact_Designation = Convert.ToString(pastexperiences.Primary_Contact_Designation);
                    pastexperience.Primary_Contact_Email = Convert.ToString(pastexperiences.Primary_Contact_Email);
                    pastexperience.Project_Scope_Summary = Convert.ToString(pastexperiences.Project_Scope_Summary);
                    pastexperience.Delivery_Location = Convert.ToString(pastexperiences.Delivery_Location);
                    pastexperience.Contract_Ref_No = Convert.ToString(pastexperiences.Contract_Ref_No);
                    pastexperience.Assignment_Start_Date = Convert.ToString(pastexperiences.Assignment_Start_Date);
                    pastexperience.Assignment_End_Date = Convert.ToString(pastexperiences.Assignment_End_Date);
                    pastexperience.Assignment_Value_LCY = Convert.ToString(pastexperiences.Assignment_Value_LCY);
                    pastexperience.Assignment_Status = Convert.ToString(pastexperiences.Assignment_Status);
                    pastexperience.Project_Completion_Value = Convert.ToString(pastexperiences.Project_Completion_Value);
                    pastexperience.Project_Completion_Work = Convert.ToString(pastexperiences.Project_Completion_Work);
                    pastexperience.Assignment_Project_Name = Convert.ToString(pastexperiences.Assignment_Project_Name);
                    list.Add(pastexperience);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<BidResponseAuditIncomeStatements> GetBidResponseIncomeStatement(string BidResponseNumber, string vendorNo)
        {
            List<BidResponseAuditIncomeStatements> list = new List<BidResponseAuditIncomeStatements>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidAuditedIncomeStatement.Where(x => x.No == BidResponseNumber && x.Vendor_No == vendorNo).ToList();
                foreach (var incomestatements in query)
                {
                    BidResponseAuditIncomeStatements incomestatement = new BidResponseAuditIncomeStatements();
                    incomestatement.No = incomestatements.No;
                    incomestatement.Audit_Year_Code_Reference = Convert.ToString(incomestatements.Audit_Year_Code_Reference);
                    incomestatement.Vendor_No = incomestatements.Vendor_No;
                    incomestatement.Total_Revenue_LCY = Convert.ToString(incomestatements.Total_Revenue_LCY);
                    incomestatement.Total_COGS_LCY = Convert.ToString(incomestatements.Total_COGS_LCY);
                    incomestatement.Gross_Margin_LCY = Convert.ToString(incomestatements.Gross_Margin_LCY);
                    incomestatement.Total_Operating_Expenses_LCY = Convert.ToString(incomestatements.Total_Operating_Expenses_LCY);
                    incomestatement.Operating_Income_EBIT_LCY = Convert.ToString(incomestatements.Operating_Income_EBIT_LCY);
                    incomestatement.Other_Non_operating_Re_Exp_LCY = Convert.ToString(incomestatements.Other_Non_operating_Re_Exp_LCY);
                    incomestatement.Interest_Expense_LCY = Convert.ToString(incomestatements.Interest_Expense_LCY);
                    incomestatement.Income_Before_Taxes_LCY = Convert.ToString(incomestatements.Income_Before_Taxes_LCY);
                    incomestatement.Income_Tax_Expense_LCY = Convert.ToString(incomestatements.Income_Tax_Expense_LCY);
                    incomestatement.Net_Income_from_Ops_LCY = Convert.ToString(incomestatements.Net_Income_from_Ops_LCY);
                    incomestatement.Below_the_line_Items_LCY = Convert.ToString(incomestatements.Below_the_line_Items_LCY);
                    incomestatement.Net_Income = Convert.ToString(incomestatements.Net_Income);
                    incomestatement.Document_Type = Convert.ToString(incomestatements.Document_Type);
                    list.Add(incomestatement);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<BidResponseAuditBalanceSheet> GetBidResponseBalanceSheet(string BidResponseNumber, string vendorNo)
        {
            List<BidResponseAuditBalanceSheet> list = new List<BidResponseAuditBalanceSheet>();
            try
            {

                var nav = NavConnection.ReturnNav();
                var query = nav.BidAuditedBalanaceSheet.Where(x => x.No == BidResponseNumber && x.Vendor_No == vendorNo).ToList();
                foreach (var balancesheets in query)
                {
                    BidResponseAuditBalanceSheet balancesheet = new BidResponseAuditBalanceSheet();
                    balancesheet.No = balancesheets.No;
                    balancesheet.Audit_Year_Code_Reference = Convert.ToString(balancesheets.Audit_Year_Code_Reference);
                    balancesheet.Vendor_No = balancesheets.Vendor_No;
                    balancesheet.Owners_Equity_LCY = Convert.ToString(balancesheets.Owners_Equity_LCY);
                    balancesheet.Current_Assets_LCY = Convert.ToString(balancesheets.Current_Assets_LCY);
                    balancesheet.Fixed_Assets_LCY = Convert.ToString(balancesheets.Fixed_Assets_LCY);
                    balancesheet.Total_Assets_LCY = Convert.ToString(balancesheets.Total_Assets_LCY);
                    balancesheet.Current_Liabilities_LCY = Convert.ToString(balancesheets.Current_Liabilities_LCY);
                    balancesheet.Long_term_Liabilities_LCY = Convert.ToString(balancesheets.Long_term_Liabilities_LCY);
                    balancesheet.Total_Liabilities_LCY = Convert.ToString(balancesheets.Total_Liabilities_LCY);
                    balancesheet.Debt_Ratio = Convert.ToString(balancesheets.Debt_Ratio);
                    balancesheet.Working_Capital_LCY = Convert.ToString(balancesheets.Working_Capital_LCY);
                    balancesheet.Current_Ratio = Convert.ToString(balancesheets.Current_Ratio);
                    balancesheet.Assets_To_Equity_Ratio = Convert.ToString(balancesheets.Assets_To_Equity_Ratio);
                    balancesheet.Debt_To_Equity_Ratio = Convert.ToString(balancesheets.Debt_To_Equity_Ratio);
                    list.Add(balancesheet);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<BidEquipmentsSpecificationModel> GetBidResponseEquipments(string BidResponseNumber)
        {
            List<BidEquipmentsSpecificationModel> list = new List<BidEquipmentsSpecificationModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidEquipmentSpecifications.Where(x => x.No == BidResponseNumber).ToList();
                foreach (var equipments in query)
                {
                    BidEquipmentsSpecificationModel equipment = new BidEquipmentsSpecificationModel();
                    equipment.No = equipments.No;
                    equipment.Equipment_Type_Code = Convert.ToString(equipments.Equipment_Type_Code);
                    equipment.Ownership_Type = equipments.Ownership_Type;
                    equipment.Equipment_Serial = equipments.Equipment_Serial;
                    equipment.Equipment_Condition_Code = equipments.Equipment_Condition_Code;
                    equipment.Equipment_Usability_Code = equipments.Equipment_Usability_Code;
                    equipment.Qty_of_Equipment = Convert.ToDecimal(equipments.Qty_of_Equipment);
                    equipment.Description = equipments.Description;
                    equipment.Years_of_Previous_Use = Convert.ToString(equipments.Years_of_Previous_Use);
                    equipment.Entry_No = Convert.ToString(equipments.Entry_No);
                    list.Add(equipment);
                }

            }


            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<BidResponsePersonnel> GetBidResponsePersonnel(string BidResponseNumber)
        {
            List<BidResponsePersonnel> list = new List<BidResponsePersonnel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidKeyStaff.Where(x => x.No == BidResponseNumber).ToList();
                foreach (var equipments in query)
                {
                    BidResponsePersonnel equipment = new BidResponsePersonnel();
                    equipment.No = equipments.No;
                    equipment.StaffCategory = equipments.Staff_Category;
                    equipment.StaffName = equipments.Staff_Name;
                    equipment.ProjectRoleCode = equipments.Proposed_Project_Role_ID;
                    equipment.RequiredProfession = equipments.Required_Project_Role;
                    equipment.EmailAddress = equipments.E_Mail;                   
                    equipment.EmploymentType = equipments.Employment_Type;                   
                    equipment.Entry_No = Convert.ToString(equipments.Entry_No);
                    list.Add(equipment);
                }

            }


            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        public JsonResult EditBidIncomeStatementetails(IncomeStatementTModel incomestatement)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnEditInsertIncomestatementDetails(incomestatement.No, incomestatement.Audit_Year_Code_Reference, Convert.ToDecimal(incomestatement.Total_Revenue_LCY), Convert.ToDecimal(incomestatement.Total_COGS_LCY),
                 Convert.ToDecimal(incomestatement.Total_Operating_Expenses_LCY), Convert.ToDecimal(incomestatement.Other_Non_operating_Re_Exp_LCY), Convert.ToDecimal(incomestatement.Interest_Expense_LCY), vendorNo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":

                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult EditBidBalanceSheetDetails(BalanceSheetTModel balancesheet)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnEditBidResponseInsertBalanceSheet(balancesheet.No, balancesheet.Audit_Year_Code_Reference, Convert.ToDecimal(balancesheet.Total_Assets_LCY), Convert.ToDecimal(balancesheet.Fixed_Assets_LCY),
                 Convert.ToDecimal(balancesheet.Current_Liabilities_LCY), Convert.ToDecimal(balancesheet.Long_term_Liabilities_LCY), Convert.ToDecimal(balancesheet.Owners_Equity_LCY), vendorNo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":

                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddBidEquipmentsSpecificationDetails(string No,HttpPostedFileBase browsedfile,string Equipment_Type_Code,string Ownership_Type, string Years_of_Previous_Use,string Equipment_Condition_Code,
          string Equipment_Usability_Code,string Equipment_Serial,string Qty_of_Equipment)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                int errCounter = 0;

                if (browsedfile == null)
                {
                    errCounter++;
                    return Json("danger*browsedfilenull", JsonRequestBehavior.AllowGet);
                }

                string fileName0 = Path.GetFileName(browsedfile.FileName);
                string ext0 = _getFileextension(browsedfile);
                string savedF0 = vendorNo + "_" + fileName0 + ext0;

                bool up2Sharepoint = _UploadSupplierDocumentToSharepoint(vendorNo, browsedfile, Equipment_Type_Code);


                if (up2Sharepoint == true)
                {
                    string filename = vendorNo + "_" + fileName0;
                    string sUrl = ConfigurationManager.AppSettings["S_URL"];
                    string defaultlibraryname = "Procurement%20Documents/";
                    string customlibraryname = "Vendor Card";
                    string sharepointLibrary = defaultlibraryname + customlibraryname;
                    string sharepointlink = sUrl + sharepointLibrary + "/" + vendorNo + "/" + filename;


                    var status = nav.FnInsertBidEquipmentsDetails(vendorNo, No, Equipment_Type_Code, Convert.ToInt32(Ownership_Type), Convert.ToDecimal(Years_of_Previous_Use),
                        Convert.ToInt32(Equipment_Condition_Code), Convert.ToInt32(Equipment_Usability_Code), Equipment_Serial, Convert.ToDecimal(Qty_of_Equipment), sharepointlink, filename);
                    var res = status.Split('*');
                    switch (res[0])
                    {
                        case "success":

                            return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                        default:
                            return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("sharepointError*", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddBidPersonnelDetails(string No,string StaffName,string StaffCategory,string EmploymentType,string EmailAddress,string Profession,string ProjectRoleCode, string RequiredProfession,HttpPostedFileBase browsedfile)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                int errCounter = 0;

                if (browsedfile == null)
                {
                    errCounter++;
                    return Json("danger*browsedfilenull", JsonRequestBehavior.AllowGet);
                }

                string fileName0 = Path.GetFileName(browsedfile.FileName);
                string ext0 = _getFileextension(browsedfile);
                string savedF0 = vendorNo + "_" + fileName0 + ext0;

                bool up2Sharepoint = _UploadSupplierDocumentToSharepoint(vendorNo, browsedfile, StaffName);


                if (up2Sharepoint == true)
                {
                    string filename = vendorNo + "_" + fileName0;
                    string sUrl = ConfigurationManager.AppSettings["S_URL"];
                    string defaultlibraryname = "Procurement%20Documents/";
                    string customlibraryname = "Vendor Card";
                    string sharepointLibrary = defaultlibraryname + customlibraryname;
                    string sharepointlink = sUrl + sharepointLibrary + "/" + vendorNo + "/" + filename;




                    var status = nav.FnInsertBidPersonnelDetails(vendorNo, No, StaffName, Convert.ToInt32(StaffCategory), Convert.ToInt32(EmploymentType),
                     EmailAddress, Profession, ProjectRoleCode, RequiredProfession, sharepointlink, filename);
                    var res = status.Split('*');
                    switch (res[0])
                    {
                        case "success":

                            return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                        default:
                            return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("sharepointError*", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddBidResponseGeneralDetails(BidResponseInsertDataTModel bidmodel,string JointVenture)
        {
            if (bidmodel.PaymentReference == null)
            {
                bidmodel.PaymentReference = "";
            }
            if (JointVenture == null)
            {
                JointVenture = "";
            }
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnInserBidGeneralDetails(vendorNo, bidmodel.BidRespNumber, bidmodel.BidderRepName, bidmodel.BidderRepDesignation, bidmodel.BidderRepAddress,
                    bidmodel.BidderWitnessName, bidmodel.BidderWitnessDesignation, bidmodel.BidderWitnessAddress, bidmodel.PaymentReference, bidmodel.BidderType, bidmodel.TenderDocumentsSource, JointVenture);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":

                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddRFQBidResponseGeneralDetails(BidResponseInsertDataTModel bidmodel)
        {
            
            try
            {
                //var vendorNo = Session["vendorNo"].ToString();
                //var nav = new NavConnection().ObjNav();
                //var status = nav.FnInserRFQBidGeneralDetails(vendorNo, bidmodel.BidRespNumber, bidmodel.BidderRepName, bidmodel.BidderRepDesignation, bidmodel.BidderRepAddress);
                //var res = status.Split('*');
                //switch (res[0])
                //{
                //    case "success":

                      return Json("success*" +  JsonRequestBehavior.AllowGet);

                //    default:
                //        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        private static List<IFPRequestsModel> GetPrequalificationsDetails(string InvitationNumber)
        {
            List<IFPRequestsModel> list = new List<IFPRequestsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.IFPRequests.Where(x => x.Code == InvitationNumber).ToList();
                foreach (var invitations in query)
                {
                    IFPRequestsModel invitation = new IFPRequestsModel();
                    invitation.Code = invitations.Code;
                    invitation.Document_Date = Convert.ToString(invitations.Document_Date);
                    invitation.Period_End_Date = Convert.ToDateTime(invitations.Period_End_Date).ToString("MM/dd/yyyy");
                    invitation.Period_Start_Date = Convert.ToDateTime(invitations.Period_Start_Date).ToString("MM/dd/yyyy");
                    invitation.Description = invitations.Description;
                    invitation.Tender_Box_Location_Code = invitations.Tender_Box_Location_Code;
                    invitation.Tender_Summary = Convert.ToString(invitations.Tender_Summary);
                    invitation.Status = Convert.ToString(invitations.Status);
                    invitation.Primary_Target_Vendor_Cluster = Convert.ToString(invitations.Primary_Target_Vendor_Cluster);
                    invitation.External_Document_No = Convert.ToString(invitations.External_Document_No);
                    invitation.Document_Type = invitations.Document_Type;
                    invitation.Submission_End_Date = Convert.ToDateTime(invitations.Submission_End_Date).ToString("MM/dd/yyyy");
                    invitation.Submission_Start_Date = Convert.ToDateTime(invitations.Submission_Start_Date).ToString("MM/dd/yyyy"); 
                    invitation.Submission_Start_Time = invitations.Submission_Start_Time;
                    invitation.Vendor_Address = invitations.Address;
                    invitation.Vendor_Address2 = invitations.Address_2;
                    invitation.Region = invitations.Country_Region_Code;
                    invitation.Submission_End_Date = Convert.ToDateTime(invitations.Submission_End_Date).ToString("MM/dd/yyyy"); 
                    list.Add(invitation);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<IFPRequestsModel> EvaluationTemplate(string scoringTemplate)
        {
            List<IFPRequestsModel> list = new List<IFPRequestsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.EvaluationCriteria.Where(x => x.Template_ID == scoringTemplate).ToList();
                foreach (var invitations in query)
                {
                    IFPRequestsModel invitation = new IFPRequestsModel();
                    invitation.Code = invitations.Template_ID;                   
                    invitation.criteriaGroupId = invitations.Criteria_Group_ID;
                    invitation.templateDescription = invitations.Evaluation_Requirement;
                    invitation.evaluationType = invitations.Evaluation_Type;
                    invitation.totalweight = Convert.ToString(invitations.Assigned_Weight);
                   
                    list.Add(invitation);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<BidResponseDetailsModel> GetBidResponseDetails(string BidResponseNumber, string vendorNo)
        {
            List<BidResponseDetailsModel> list = new List<BidResponseDetailsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BidResponsesDetails.Where(x => x.No == BidResponseNumber && x.Vendor_No == vendorNo).ToList();
                foreach (var bidreponse in query)
                {
                    BidResponseDetailsModel biddetail = new BidResponseDetailsModel();
                    biddetail.No = bidreponse.No;
                    biddetail.Bidder_Representative_Name = Convert.ToString(bidreponse.Bidder_Representative_Name);
                    biddetail.Invitation_For_Supply_No = bidreponse.Invitation_For_Supply_No;
                    biddetail.Plot_No = bidreponse.Plot_No;
                    biddetail.Tender_Description = bidreponse.Tender_Description;
                    biddetail.Bankers_Name = bidreponse.Bankers_Name;
                    biddetail.Bankers_Branch = bidreponse.Bankers_Branch;
                    biddetail.KNTC_Agent = Convert.ToString(bidreponse.KNTC_Agent);
                    biddetail.Nominal_Capital_LCY = Convert.ToString(bidreponse.Nominal_Capital_LCY);
                    biddetail.Business_Type = Convert.ToString(bidreponse.Business_Type);
                    biddetail.Issued_Capital_LCY = Convert.ToString(bidreponse.Issued_Capital_LCY);
                    biddetail.Status = bidreponse.Status;
                    biddetail.Bidder_Representative_Address = bidreponse.Bidder_Representative_Address;
                    biddetail.Bidder_Representative_Desgn = Convert.ToString(bidreponse.Bidder_Representative_Desgn);
                    biddetail.Bidder_Witness_Name = bidreponse.Bidder_Witness_Name;
                    biddetail.Tender_Document_Source = Convert.ToString(bidreponse.Tender_Document_Source);
                    biddetail.Document_Status = bidreponse.Document_Status;
                    biddetail.Bid_Charge_Code = bidreponse.Bid_Charge_Code;
                    biddetail.Bid_Charge_LCY = Convert.ToString(bidreponse.Bid_Charge_LCY);
                    biddetail.Payment_Reference_No = Convert.ToString(bidreponse.Payment_Reference_No);
                    biddetail.Posted_Direct_Income_Voucher = Convert.ToString(bidreponse.Posted_Direct_Income_Voucher);
                    biddetail.Pay_to_Vendor_No = Convert.ToString(bidreponse.Pay_to_Vendor_No);
                    biddetail.Pay_to_Name = bidreponse.Pay_to_Name;
                    biddetail.Currency_Code = bidreponse.Currency_Code;
                    biddetail.Amount = Convert.ToString(bidreponse.Amount);
                    biddetail.Location_Code = bidreponse.Location_Code;
                    biddetail.Amount_Including_VAT = Convert.ToString(bidreponse.Amount_Including_VAT);
                    biddetail.VAT_Registration_No = bidreponse.VAT_Registration_No;
                    biddetail.Purchaser_Code = bidreponse.Purchaser_Code;
                    biddetail.Pay_to_Address = bidreponse.Pay_to_Address;
                    biddetail.Pay_to_City = bidreponse.Pay_to_City;
                    biddetail.Pay_to_Post_Code = bidreponse.Pay_to_Post_Code;
                    biddetail.Pay_to_Country_Region_Code = bidreponse.Pay_to_Country_Region_Code;
                    biddetail.Pay_to_Address_2 = bidreponse.Pay_to_Address_2;
                    biddetail.Language_Code = bidreponse.Language_Code;
                    biddetail.Bidder_type = bidreponse.Bidder_Type;
                    biddetail.Responsibility_Center = bidreponse.Responsibility_Center;
                    biddetail.Bidder_witness_Address = bidreponse.Bidder_Witness_Address;
                    list.Add(biddetail);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<SingleAddendumNoticeModel> GetAddendumDetails(string AddendumNumber)
        {
            List<SingleAddendumNoticeModel> list = new List<SingleAddendumNoticeModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.tenderAddendums.Where(x => x.Addendum_Notice_No == AddendumNumber && x.Document_Status == "Published" && x.Status == "Released").ToList();
                foreach (var tenders in query)
                {
                    SingleAddendumNoticeModel tender = new SingleAddendumNoticeModel();
                    tender.Addendum_Notice_No = tenders.Addendum_Notice_No;
                    tender.Document_Date = Convert.ToString(tenders.Document_Date);
                    tender.Description = tenders.Description;
                    tender.Addendum_Instructions = tenders.Addendum_Instructions;
                    tender.Primary_Addendum_Type_ID = tenders.Primary_Addendum_Type_ID;
                    tender.Addendum_Type_Description = tenders.Addendum_Type_Description;
                    tender.Tender_No = tenders.Tender_No;
                    tender.Invitation_Notice_No = tenders.Invitation_Notice_No;
                    tender.Tender_Description = tenders.Tender_Description;
                    tender.Responsibility_Center = tenders.Responsibility_Center;
                    tender.New_Submission_Start_Date = Convert.ToDateTime(tenders.New_Submission_Start_Date).ToString("MM/dd/yyyy");
                    tender.New_Submission_End_Date = Convert.ToDateTime(tenders.New_Submission_End_Date).ToString("MM/dd/yyyy");
                    tender.Status = tenders.Status;
                    tender.Original_Submission_End_Date = Convert.ToDateTime(tenders.Original_Submission_End_Date).ToString("MM/dd/yyyy");
                    tender.Original_Submission_End_Time = tenders.Original_Submission_End_Time;
                    tender.Original_Bid_Opening_Date = Convert.ToDateTime(tenders.Original_Bid_Opening_Date).ToString("MM/dd/yyyy");
                    tender.Original_Bid_Opening_Time = tenders.Original_Bid_Opening_Time;
                    tender.New_Bid_Opening_Time = tenders.New_Bid_Opening_Time;
                    tender.New_Bid_Opening_Date = Convert.ToDateTime(tenders.New_Bid_Opening_Date).ToString("MM/dd/yyyy");
                    tender.Original_Prebid_Meeting_Date = Convert.ToDateTime(tenders.Original_Prebid_Meeting_Date).ToString("MM/dd/yyyy");
                    tender.Document_Status = tenders.Document_Status;
                    tender.New_Submission_End_Time = Convert.ToString(tenders.New_Submission_End_Time);
                    tender.Original_Prebid_Meeting_Date = Convert.ToDateTime(tenders.Original_Prebid_Meeting_Date).ToString("MM/dd/yyyy");

                    list.Add(tender);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<AddendumAmmendmentModel> GetAddendumAmmendmentDetails(string AddendumNumber)
        {
            List<AddendumAmmendmentModel> list = new List<AddendumAmmendmentModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.AddendumAmmendments.Where(x => x.Addendum_Notice_No == AddendumNumber).ToList();
                foreach (var tenders in query)
                {
                    AddendumAmmendmentModel tender = new AddendumAmmendmentModel();
                    tender.Addendum_Notice_No = tenders.Addendum_Notice_No;
                    tender.Amended_Section_of_Tender_Doc = Convert.ToString(tenders.Amended_Section_of_Tender_Doc);
                    tender.Amendment_Description = tenders.Amendment_Description;
                    tender.Amendment_Type = tenders.Amendment_Type;
                    list.Add(tender);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<VendorModel> GetVendors(string vendorNo)
        {

            List<VendorModel> vendorsDetails = new List<VendorModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.eProVendorQT.Where(x => x.No == vendorNo).ToList();
                foreach (var vendors in query)
                {
                    VendorModel vendor = new VendorModel();
                    vendor.Vendor_No = vendors.No;
                    vendor.VAT_Registration_No = vendors.VAT_Registration_No;
                    vendor.Name = vendors.Name;
                    vendor.LanguageCode = vendors.Language_Code;
                    vendor.BusinessType = vendors.Business_Type;
                    vendor.Vendor_Type = vendors.Supplier_Category;
                    vendor.Owner_Type = vendors.Ownership_Type;
                    vendor.OpsDate = Convert.ToString(vendors.Operations_Start_Date);
                    vendor.CertofIncorporation = vendors.Registration_Incorporation_No;
                    vendor.Mision = vendors.Mission_Statement;
                    vendor.Vision = vendors.Vision_Statement;
                    vendor.DateofIncorporation = Convert.ToString(vendors.Reg_Incorporation_Date);
                    vendor.PoBox = vendors.Post_Code;
                    vendor.City = vendors.City;
                    vendor.Country = vendors.Country_Region_Code;
                    vendor.WebUrl = vendors.Website_Url;
                    vendor.PhysicalLocation = vendors.Address;
                    vendor.E_mail = vendors.E_Mail;
                    vendor.Phone_No = vendors.Phone_No;
                    vendor.Currency = vendors.Currency_Code;
                    vendor.Balance = Convert.ToString(vendors.Balance_LCY);
                    vendor.Address = Convert.ToString(vendors.Address);
                    vendor.Physical_location = Convert.ToString(vendors.Location_Code);
                    vendor.IndustryGroup = vendors.Industry_Group;
                    vendor.Supplier_Type = vendors.Supplier_Type;
                    vendor.LanguageCode = vendors.Language_Code;
                    vendor.Address = vendors.Address;
                    vendor.PostaCode = vendors.Post_Code;
                    vendor.PostaCity = vendors.City;
                    vendor.Address_2 = vendors.Address_2;
                    vendor.HouseNo = vendors.Building_House_No;
                    vendor.FloorNo = vendors.Floor;
                    vendor.PlotNo = vendors.Plot_No;
                    vendor.StreetorRoad = vendors.Street;
                    vendor.CompanySize = vendors.Company_Size;
                    vendor.NominalCap = Convert.ToDecimal(vendors.Nominal_Capital_LCY);
                    vendor.Address_2 = vendors.Address_2;
                    vendor.Dealer_Type = Convert.ToString(vendors.Dealer_Type);
                    vendor.MaxBizValue = Convert.ToDecimal(vendors.Max_Value_of_Business);
                    vendor.NatureofBz = vendors.Nature_of_Business;
                    vendor.Fax = vendors.Fax_No;
                    vendor.RegistrationNo = vendors.Registration_Incorporation_No;
                    vendor.Primary_Contact_No = vendors.Primary_Contact_No;
                    vendor.Signatory_Designation = vendors.Signatory_Designation;
                    vendor.Balance = Convert.ToString(vendors.Balance_LCY);
                    vendor.CountryofOrigin = Convert.ToString(vendors.Country_Region_Code);
                    vendor.Issued_Capital = Convert.ToString(vendors.Issued_Capital_LCY);
                    vendorsDetails.Add(vendor);
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

            return vendorsDetails;
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SubmitPrequalificationsDetails(PreQualificationModel PrequalificationNumber)
        {
            try
            {
                string vendorNumber = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnSubmitPrequalificationResponse(vendorNumber, PrequalificationNumber.Ref_No);
                var res = status.Split('*');
                if (res[0] == "success")
                {
                    return Json("success*" + JsonRequestBehavior.AllowGet);
                }
                if (res[0] == "mandatory")
                {
                    return Json("mandatory*" + res[1], JsonRequestBehavior.AllowGet);
                }
                if (res[0] == "danger")
                {
                    return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
                return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex, JsonRequestBehavior.AllowGet);
            }

        }
        [HandleError]
        public JsonResult SubmitQuotationResponse(string tendernumber)
        {
            try
            {
                string vendorNumber = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = nav.FnSubmitRFQResponse(vendorNumber, tendernumber);
                var res = status.Split('*');
                if (res[0] == "success")
                {
                    Session["RFQBideResponseNumber"] = nav.FngetBidResponseNumber(tendernumber, vendorNumber);
                    return Json("success*" + JsonRequestBehavior.AllowGet);
                }
                if (res[0] == "found")
                {
                    Session["RFQBideResponseNumber"] = nav.FngetBidResponseNumber(tendernumber, vendorNumber);
                    return Json("found*" + res[1], JsonRequestBehavior.AllowGet);
                }
                if (res[0] == "danger")
                {
                    return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
                if (res[0] == "profileincomplete")
                {
                    return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
                
                return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public ActionResult RespondQuotationWizard()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["RFQBideResponseNumber"] == null)
            {
                return RedirectToAction("ActiveRFQs", "Home");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                string BidResponseNumber = Session["RFQBideResponseNumber"].ToString();
                dynamic model = new ExpandoObject();
                ViewBag.prequalificationNo = BidResponseNumber;
                model.BidDetails = GetBidResponseDetails(BidResponseNumber, vendorNo);
                model.BidEquipments = GetBidResponseEquipments(BidResponseNumber);
                model.BidPersonnel = GetBidResponsePersonnel(BidResponseNumber);
                model.BidBalnaceSheet = GetBidResponseBalanceSheet(BidResponseNumber, vendorNo);
                model.BidIncomeStatement = GetBidResponseIncomeStatement(BidResponseNumber, vendorNo);
                model.BidPastExperiencent = GetBidResponsePastExperience(BidResponseNumber, vendorNo);
                model.BidPricinginformation = GetBidResponsePricingInformation(BidResponseNumber);
                model.RequiredDocuments = GetRequiredTenderDocuments(BidResponseNumber);
                model.BidSecurity = GetBidSecurityResponse(BidResponseNumber, vendorNo);
                model.AttachedBiddDocuments = GetBidAttachedDocumentsDetails(BidResponseNumber, vendorNo);

                return View(model);
            }        
    }
        private static List<PrequalifiedCategoriesModel> GetPrequalificationCategories(string InvitationNumber)
        {

            List<PrequalifiedCategoriesModel> prequalificationDetails = new List<PrequalifiedCategoriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.PrequalificationCategories.Where(x => x.Document_No == InvitationNumber).ToList();
                foreach (var preferences in query)
                {
                    PrequalifiedCategoriesModel preference = new PrequalifiedCategoriesModel();
                    preference.Document_No = preferences.Document_No;
                    preference.Document_Type = preferences.Document_Type;
                    preference.Procurement_Type = preferences.Procurement_Type;
                    preference.Procurement_Category_Code = preferences.Prequalification_Category_ID;
                    preference.Description = preferences.Description;
                    preference.Start_Date = Convert.ToString(preferences.Period_Start_Date);
                    preference.End_Date = Convert.ToString(preferences.Period_End_Date);
                    preference.Submission_End_Date = Convert.ToString(preferences.Submission_End_Date);
                    preference.Submission_End_Time = Convert.ToString(preferences.Submission_End_Time);
                    preference.Submission_Start_Date = Convert.ToString(preferences.Submission_Start_Date);
                    preference.Submission_Start_Time = preferences.Submission_Start_Time;
                    preference.Document_No = preferences.Document_No;
                    preference.Application_Location = Convert.ToString(preferences.Applicable_Location);
                    preference.Special_Group = Convert.ToString(preferences.Special_Group_Reservation);
                    if (preference.Special_Group == "True")
                    {

                        preference.Special_Group = "Yes";
                    }
                    else
                    {
                        preference.Special_Group = "No";
                    }
                    prequalificationDetails.Add(preference);
                }


            }
            catch (Exception ex)
            {

                throw;
            }

            return prequalificationDetails;
        }
        private static List<PrequalifiedCategoriesModel> GetPrequalificationHistory(string vendorNo)
        {

            List<PrequalifiedCategoriesModel> prequalificationDetails = new List<PrequalifiedCategoriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorPrequalificationEntries.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var preferences in query)
                {
                    PrequalifiedCategoriesModel preference = new PrequalifiedCategoriesModel();
                    preference.Vendor_No = preferences.Vendor_No;
                    preference.IFP_No = preferences.IFP_No;
                    preference.Procurement_Type = preferences.Procurement_Type;
                    preference.Procurement_Category_Code = preferences.Procurement_Category_Code;
                    preference.Description = preferences.Description;
                    preference.Start_Date = Convert.ToString(preferences.Start_Date);
                    preference.End_Date = Convert.ToString(preferences.End_Date);
                    preference.Document_Type = preferences.Document_Type;
                    preference.Document_No = preferences.Document_No;
                    preference.Posting_Date = Convert.ToString(preferences.Posting_Date);
                    prequalificationDetails.Add(preference);
                }


            }
            catch (Exception e)
            {

                throw;
            }

            return prequalificationDetails;
        }
        private static List<LitigationModel> GetVendorLitigationHistoryDetails(string vendorNo)
        {

            List<LitigationModel> litigationDetailsHistory = new List<LitigationModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorLitigationHistory.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var listigationhistory in query)
                {
                    LitigationModel litigations = new LitigationModel();
                    litigations.Entry_No = listigationhistory.Entry_No;
                    litigations.DisputeDescription = listigationhistory.Dispute_Matter;
                    litigations.CategoryofDispute = listigationhistory.Category_of_Matter;
                    litigations.TheotherDisputeparty = listigationhistory.Other_Dispute_Party;
                    litigations.AwardType = listigationhistory.Award_Type;
                    litigations.DisputeAmount = listigationhistory.Dispute_Amount_LCY;
                    litigations.Year = listigationhistory.Year;
                    litigationDetailsHistory.Add(litigations);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return litigationDetailsHistory;
        }
        private static List<BalanceSheetTModel> GetVendorBalanaceDetails(string vendorNo)
        {

            List<BalanceSheetTModel> balancesheetdetails = new List<BalanceSheetTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.vendorBalancesheet.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var balance in query)
                {
                    BalanceSheetTModel balancesheet = new BalanceSheetTModel();
                    balancesheet.Audit_Year_Code_Reference = balance.Audit_Year_Code_Reference;
                    balancesheet.Current_Assets_LCY = balance.Current_Assets_LCY;
                    balancesheet.Fixed_Assets_LCY = balance.Fixed_Assets_LCY;
                    balancesheet.Total_Assets_LCY = balance.Total_Assets_LCY;
                    balancesheet.Current_Liabilities_LCY = balance.Current_Liabilities_LCY;
                    balancesheet.Long_term_Liabilities_LCY = balance.Long_term_Liabilities_LCY;
                    balancesheet.Total_Liabilities_LCY = balance.Total_Liabilities_LCY;
                    balancesheet.Owners_Equity_LCY = balance.Owners_Equity_LCY;
                    balancesheet.Debt_Ratio = balance.Debt_Ratio;
                    balancesheet.Working_Capital_LCY = balance.Working_Capital_LCY;
                    balancesheet.Assets_To_Equity_Ratio = balance.Assets_To_Equity_Ratio;
                    balancesheet.Debt_To_Equity_Ratio = balance.Debt_To_Equity_Ratio;
                    balancesheetdetails.Add(balancesheet);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return balancesheetdetails;
        }
        private static List<ProfessionalStaffModel> GetVendorProfessionalStaff(string vendorNo)
        {

            List<ProfessionalStaffModel> staffDetails = new List<ProfessionalStaffModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorProffessionalStaff.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var staffs in query)
                {
                    ProfessionalStaffModel staff = new ProfessionalStaffModel();
                    staff.StaffNumber = staffs.Staff_Number;
                    staff.StaffName = staffs.Staff_Name;
                    staff.StaffDateofBirth = Convert.ToString(staffs.Date_of_Birth);
                    staff.StaffEmail = staffs.E_Mail;
                    staff.StaffDesignation = staffs.Current_Designation;
                    staff.Years_With_the_Firm =Convert.ToString(staffs.Years_With_the_Firm);
                    staff.Post_Code = staffs.Post_Code;
                    staff.Address_2 = staffs.Address_2;
                    staff.StaffPhonenumber = staffs.Phone_No;
                    staff.City = staffs.City;
                    staff.Citizenship_Type = staffs.Citizenship_Type;
                    staff.Staff_Category = staffs.Staff_Category;
                    staff.Country_Region_Code = staffs.Country_Region_Code;
                    staff.StaffProfession = staffs.Proffesion;
                    staff.StaffJoiningDate =Convert.ToString(staffs.Joining_Date);
                    staffDetails.Add(staff);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return staffDetails;
        }
      
        private static List<IncomeStatementTModel> GetVendorIncomeStatementDetails(string vendorNo)
        {

            List<IncomeStatementTModel> incomestatementdetails = new List<IncomeStatementTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.vendorIncomestatement.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var incomestatement in query)
                {
                    IncomeStatementTModel income = new IncomeStatementTModel();
                    income.Audit_Year_Code_Reference = incomestatement.Audit_Year_Code_Reference;
                    income.Total_Revenue_LCY = incomestatement.Total_Revenue_LCY;
                    income.Total_COGS_LCY = incomestatement.Total_COGS_LCY;
                    income.Gross_Margin_LCY = incomestatement.Gross_Margin_LCY;
                    income.Total_Operating_Expenses_LCY = incomestatement.Total_Operating_Expenses_LCY;
                    income.Operating_Income_EBIT_LCY = incomestatement.Operating_Income_EBIT_LCY;
                    income.Other_Non_operating_Re_Exp_LCY = incomestatement.Other_Non_operating_Re_Exp_LCY;
                    income.Interest_Expense_LCY = incomestatement.Interest_Expense_LCY;
                    income.Income_Before_Taxes_LCY = incomestatement.Income_Before_Taxes_LCY;
                    income.Income_Tax_Expense_LCY = incomestatement.Income_Tax_Expense_LCY;
                    income.Net_Income_from_Ops_LCY = incomestatement.Net_Income_from_Ops_LCY;
                    income.Net_Income = incomestatement.Net_Income;
                    incomestatementdetails.Add(income);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return incomestatementdetails;
        }
        private static List<PastExperienceModel> GetVendorPastExeprience(string vendorNo)
        {

            List<PastExperienceModel> pastexperienceDetails = new List<PastExperienceModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorPastExperience.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var past in query)
                {
                    PastExperienceModel pastexperience = new PastExperienceModel();
                    pastexperience.Entry_No = past.Entry_No;
                    pastexperience.Vendor_No = past.Vendor_No;
                    pastexperience.Client_Name = past.Client_Name;
                    pastexperience.Address = past.Address;
                    pastexperience.City = past.City;
                    pastexperience.Phone_No = past.Phone_No;
                    pastexperience.Nationality_ID = past.Nationality_ID;
                    pastexperience.Date_of_Birth = Convert.ToString(past.Date_of_Birth);
                    pastexperience.Entity_Ownership = Convert.ToString(past.Entity_Ownership);
                    pastexperience.Primary_Contact_Person = past.Primary_Contact_Person;
                    pastexperience.Assignment_Start_Date = Convert.ToString(past.Assignment_Start_Date);
                    pastexperience.Assignment_End_Date = Convert.ToString(past.Assignment_End_Date);
                    pastexperience.Total_Nominal_Value = Convert.ToString(past.Total_Nominal_Value);
                    pastexperience.Assignment_Value_LCY = Convert.ToString(past.Assignment_Value_LCY);
                    pastexperience.Total_Nominal_Value = Convert.ToString(past.Total_Nominal_Value);
                    pastexperience.Assignment_Status = past.Assignment_Status;
                    pastexperience.Assignment_Project_Name = past.Assignment_Name;
                    pastexperience.Contract_Ref_No = past.Contract_Ref_No;
                    pastexperience.Delivery_Location = past.Delivery_Location;
                    pastexperience.Project_Scope_Summary = past.Project_Scope_Summary;
                    pastexperienceDetails.Add(pastexperience);
                }


            }
            catch (Exception e)
            {

                throw;
            }

            return pastexperienceDetails;
        }
        
        private static List<DirectorModel> GetStakeholders(string vendorNo)
        {

            List<DirectorModel> DirectorDetails = new List<DirectorModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorShareholderDetails.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var shareholders in query)
                {
                    DirectorModel shareholder = new DirectorModel();
                    shareholder.Entry_No = shareholders.Entry_No;
                    shareholder.Fullname = shareholders.Name;
                    shareholder.CitizenshipType = shareholders.Citizenship_Type;
                    shareholder.OwnershipPercentage = shareholders.Entity_Ownership;
                    shareholder.Phonenumber = shareholders.Phone_No;
                    shareholder.Address = shareholders.Address;
                    shareholder.PostCode = shareholders.Post_Code;
                    shareholder.Email = shareholders.E_Mail;
                    shareholder.IdNumber = shareholders.ID_Passport_No;
                    shareholder.Nationality = shareholders.Nationality_ID;
                    DirectorDetails.Add(shareholder);
                }


            }
            catch (Exception e)
            {

                throw;
            }

            return DirectorDetails;
        }
        private static List<BeneficiarryModel> GetBeneficiaries(string vendorNo)
        {

            List<BeneficiarryModel> BeneficiaryDetails = new List<BeneficiarryModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorBeneficiaries.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var beneficiaries in query)
                {
                    BeneficiarryModel beneficiary = new BeneficiarryModel();
                    beneficiary.Entry_No = beneficiaries.Entry_No;
                    beneficiary.Name = beneficiaries.Name;
                    beneficiary.idType = beneficiaries.ID_Type;
                    beneficiary.idpassportNumber = beneficiaries.ID_Passport_No;
                    beneficiary.Phonenumber = Convert.ToInt32(beneficiaries.Phone_No);
                    beneficiary.Email = beneficiaries.Email;
                    beneficiary.AllocatedShares = Convert.ToDecimal(beneficiaries.Allocated_Shares);
                    beneficiary.BeneficiaryType = beneficiaries.Type;
                    BeneficiaryDetails.Add(beneficiary);
                }


            }
            catch (Exception e)
            {

                throw;
            }

            return BeneficiaryDetails;
        }

        private static List<BankModel> GetBanks(string vendorNo)
        {

            List<BankModel> BankDetails = new List<BankModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorBankAccounts.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var banks in query)
                {
                    BankModel bank = new BankModel();
                    bank.BankCode = banks.Bank_Code;
                    bank.BankName = banks.Bank_Account_Name;
                    bank.BankAccountNo = banks.Bank_Account_No;                   
                    bank.Bank_Branch_No = banks.Bank_Branch_No;
                    bank.bankBranchName = banks.Bank_Branch_Name;
                    bank.CurrencyCode = banks.Currency_Code;
                    bank.Contact = banks.Contact;
                    bank.Phone_No = banks.Phone_No;
                    
                    //bank.bankBranchName = banks.Bank_Branch_Name;
                    bank.CountryCode = banks.Country_Region_Code;
                    bank.City = banks.City;
                    bank.Post_Code = banks.Post_Code;
                    BankDetails.Add(bank);
                }

            }
            catch (Exception e)
            {

                throw;
            }

            return BankDetails;
        }

        public ActionResult ResetPassword()
        {

            return View();
        }
        public ActionResult ChangePassword()
        {

            return View();
        }
        public ActionResult Register()
        {
           
                var nav = NavConnection.ReturnNav();
                var query = nav.ProcurementSetup;
                foreach (var terms in query)
                {
                    ViewBag.Terms = terms.Terms_and_Conditions;
                }

                
           
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult ResetSupplierPassword(ResetPasswordModel passwordmodel)
        {
            try
            {
                var nav = new NavConnection().ObjNav();
                var status = nav.FnResetPassword(passwordmodel.emailaddress);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);
                    case "emailnotfound":
                        return Json("emailnotfound*" + res[1], JsonRequestBehavior.AllowGet);
                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult ChangeSupplierPassword(ResetPasswordModel passwordmodel)
        {
            try
            {
                var nav = new NavConnection().ObjNav();
                var status = nav.FnChangePassword( passwordmodel.emailaddress, passwordmodel.oldpassword, passwordmodel.newpassword, passwordmodel.confirmpassword);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);
                    case "passwordmismatch":
                        return Json("passwordmismatch*" + res[1], JsonRequestBehavior.AllowGet);
                    case "worngcurrentpassword":
                        return Json("worngcurrentpassword*" + res[1], JsonRequestBehavior.AllowGet);
                    case "novendorfound":
                        return Json("worngcurrentpassword*" + res[1], JsonRequestBehavior.AllowGet);
                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult SupplierFirstRegistration(SignupModel signupmodel)
        {
            try
            {
                var nav = new NavConnection().ObjNav();
                var status = nav.FnReqforRegistration(signupmodel.VendorName, signupmodel.Phonenumber, signupmodel.Email, signupmodel.KraPin, signupmodel.ContactName);

                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UserRegistration(string tbusinessname, string ttaxpinnumber, string tcontactperson, string tprimaryemail, string tmobilephone, string tterms)
        {

            try
            {
                var nav = new NavConnection().ObjNav();
                var status = nav.FnReqforRegistration(tbusinessname, tmobilephone, tprimaryemail, ttaxpinnumber, tcontactperson);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*" + res[1], JsonRequestBehavior.AllowGet);

                    default:
                        return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult OpenRFQs()
        {

            return View();
        }
        public ActionResult PrequalifiedCategories()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<PrequalifiedCategoriesModel> list = new List<PrequalifiedCategoriesModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.VendorPrequalificationEntries.Where(x => x.Vendor_No == vendorNo).ToList();
                    foreach (var responses in query)
                    {
                        PrequalifiedCategoriesModel response = new PrequalifiedCategoriesModel();
                        response.Entry_No = Convert.ToString(responses.Entry_No);
                        response.IFP_No = responses.IFP_No;
                        response.Vendor_No = responses.Vendor_No;
                        response.Procurement_Type = responses.Procurement_Type;
                        response.Procurement_Category_Code = responses.Procurement_Category_Code;
                        response.Description = responses.Description;
                        response.Start_Date = Convert.ToString(responses.Start_Date);
                        response.Start_Date = Convert.ToString(responses.End_Date);
                        response.Block = Convert.ToString(responses.Blocked);
                        response.Date_Block = Convert.ToString(responses.Date_Blocked);
                        response.Document_Type = responses.Document_Type;
                        response.Document_No = responses.Document_No;
                        list.Add(response);
                    }

                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult RegistrationCategories()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string Res = "";
                string[] accountsFieldsSeparators = new string[] { "*" };
                List<string> list = new List<string>();

                var list2 = new List<PrequalifiedCategoriesModel>();
                //List<PrequalifiedCategoriesModel> list = new List<PrequalifiedCategoriesModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = new NavConnection().ObjNav();
                     nav.FnGetRegisteredCategories(ref Res,vendorNo);

                    string[] ResultArray = Res.Split(new string[] { "::::" }, StringSplitOptions.RemoveEmptyEntries);

                    list = ResultArray.ToList();
                    foreach (string item in list)
                    {
                        string[] categories = item.Split(accountsFieldsSeparators, System.StringSplitOptions.None);

                        list2.Add(new PrequalifiedCategoriesModel
                        {
                            Procurement_Type = categories[0],
                            Procurement_Category_Code = categories[1],
                            Description= categories[2],
                            Start_Date = categories[3],
                            End_Date = categories[4],

                        });

                    }

                    //var res = status.Split('*');
                    //var nav = NavConnection.ReturnNav();
                    //var query = nav.VendorPrequalificationEntries.Where(x => x.Vendor_No == vendorNo).ToList();
                    //foreach (var responses in query)
                    //{
                    //    PrequalifiedCategoriesModel response = new PrequalifiedCategoriesModel();
                    //    response.Entry_No = Convert.ToString(responses.Entry_No);
                    //    response.IFP_No = responses.IFP_No;
                    //    response.Vendor_No = responses.Vendor_No;
                    //    response.Procurement_Type = responses.Procurement_Type;
                    //    response.Procurement_Type = responses.Procurement_Category_Code;
                    //    response.Description = responses.Description;
                    //    response.Start_Date = Convert.ToString(responses.Start_Date);
                    //    response.Start_Date = Convert.ToString(responses.End_Date);
                    //    response.Block = Convert.ToString(responses.Blocked);
                    //    response.Date_Block = Convert.ToString(responses.Date_Blocked);
                    //    response.Document_Type = responses.Document_Type;
                    //    response.Document_No = responses.Document_No;
                    //    list.Add(response);
                    //}

                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list2);
            }
        }
        [HttpGet]
        public ActionResult RFIResponseForm(string InvitationNumber, string PrequalificationNo)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                try
                {
                    var nav = NavConnection.ReturnNav();

                    var query = nav.Vendors.Where(x => x.No == Convert.ToString(Session["vendorNo"]));
                    foreach (var responses in query)
                    {

                        if (responses.Profile_Complete == true)
                        {

                            var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Vendors = GetVendors(vendorNo);
                model.Response = ResponseDetails(InvitationNumber, vendorNo);
                model.GoodsServices = GoodsServicesDetails(InvitationNumber, vendorNo);
                model.Services = ServicesDetails(InvitationNumber, vendorNo);
                model.StakeholdersDetails = GetStakeholders(vendorNo);
                model.Beneficiaries = GetBeneficiaries(vendorNo);
                model.balancesheet = GetVendorBalanaceDetails(vendorNo);
                model.VendorProfessionalStaff = GetVendorProfessionalStaff(vendorNo);
                model.incomestatement = GetVendorIncomeStatementDetails(vendorNo);
                model.PastExperience = GetVendorPastExeprience(vendorNo);
                model.litigationhistory = GetVendorLitigationHistoryDetails(vendorNo);
                model.Works = WorksDetails(InvitationNumber, vendorNo);
                            // model.RequiredDocuments = RequiredDocumentsDetails(InvitationNumber, vendorNo);
                            // model.PrequalificationUploadedDocuments = PrequalificationUploaded(InvitationNumber, vendorNo);
                            return View(model);
                        }

                        else if (responses.Profile_Complete == false)
                        {
                            TestModel model = new TestModel();
                            model.ShowDialog = true;
                        }
                    }
                }
                catch
                {

                }

                return RedirectToAction("SupplierRegistration", "Home");
            }
        }

        public ActionResult RegistrationFormResponce(string InvitationNumber, string PrequalificationNo)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                try
                {
                    var nav = NavConnection.ReturnNav();

                    var query = nav.Vendors.Where(x=>x.No==Convert.ToString(Session["vendorNo"]));
                    foreach (var responses in query)
                    {

                        if (responses.Profile_Complete == true)
                        {


                            var vendorNo = Session["vendorNo"].ToString();
                            dynamic model = new ExpandoObject();
                            model.Vendors = GetVendors(vendorNo);
                            model.Response = RegistrationResponseDetails(InvitationNumber, vendorNo);
                            model.GoodsServices = GoodsServicesDetails(InvitationNumber, vendorNo);
                            model.Services = ServicesDetails(InvitationNumber, vendorNo);
                            model.StakeholdersDetails = GetStakeholders(vendorNo);
                            model.Beneficiaries = GetBeneficiaries(vendorNo);
                            model.balancesheet = GetVendorBalanaceDetails(vendorNo);
                            model.VendorProfessionalStaff = GetVendorProfessionalStaff(vendorNo);
                            model.incomestatement = GetVendorIncomeStatementDetails(vendorNo);
                            model.PastExperience = GetVendorPastExeprience(vendorNo);
                            model.litigationhistory = GetVendorLitigationHistoryDetails(vendorNo);
                            model.Works = WorksDetails(InvitationNumber, vendorNo);
                            //model.RequiredDocuments = RequiredDocumentsDetails(InvitationNumber, vendorNo);
                            //model.PrequalificationUploadedDocuments = PrequalificationUploaded(PrequalificationNo, vendorNo);

                            return View(model);
                        }
                        
                        else if (responses.Profile_Complete == false)
                        {
                            TestModel model = new TestModel();
                            model.ShowDialog = true;
                        }
                    }
                }
                catch
                {

                }

                return RedirectToAction("SupplierRegistration", "Home");
            }
    }


        private static List<IFPRequestsModel> ResponseDetails(string tenderresponseNumber, string vendorNo)
        {
            List<IFPRequestsModel> list = new List<IFPRequestsModel>();
            try
            {
                // Create Ne Prequalification Number
                //var vendorNo = Session["vendorNo"].ToString();
                var navCodeunit = new NavConnection().ObjNav();
                var status = navCodeunit.FnInsertRFIresponseHeader(vendorNo, tenderresponseNumber);
                var res = status.Split('*');
                //if (res[0] == "success")
                //{
                //    //Populate Records
                    var nav = NavConnection.ReturnNav();
                    var query = nav.InvitationPrequalification.Where(x => x.Code == tenderresponseNumber && x.Status == "Released" && x.Published == true).ToList();
                    foreach (var responses in query)
                    {
                        IFPRequestsModel response = new IFPRequestsModel();
                        response.Code = responses.Code;
                        response.PrequalificationNo = res[1];
                        response.Document_Type = responses.Document_Type;
                        response.Vendor_Address = responses.Address;
                        response.Primary_Target_Vendor_Cluster = responses.Primary_Target_Vendor_Cluster;
                        response.Vendor_Address2 = responses.Address_2;
                        response.Document_Date = Convert.ToString(responses.Document_Date);
                        response.Period_Start_Date = Convert.ToString(responses.Period_Start_Date);
                        response.Period_End_Date = Convert.ToString(responses.Period_End_Date);
                        response.Vendor_Address2 = responses.Address_2;
                        response.Tender_Box_Location_Code = responses.Tender_Box_Location_Code;
                        response.Tender_Summary = responses.Tender_Summary;
                        response.Region = responses.Responsibility_Centre;
                        response.constituency = responses.Constituency;
                        response.RFI_Documents_No = responses.Code;
                        list.Add(response);
                    }
                //}
                //   else if (res[0] == "profileincomplete")
                //    {

                    
                //    errorMessage("Hello World!");


                //}
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return list;
        }

        private static void errorMessage(string v)
        {
            Console.WriteLine(v);
        }

     
        private static List<IFPRequestsModel> RegistrationResponseDetails( string vendorNo, string tenderresponseNumber)
        {
            List<IFPRequestsModel> list = new List<IFPRequestsModel>();
            try
            {
                // Create Ne Prequalification Number
                //var vendorNo = Session["vendorNo"].ToString();
                var navCodeunit = new NavConnection().ObjNav();
                var status = navCodeunit.FnInsertRFregresponseHeader(tenderresponseNumber,vendorNo);
                var res = status.Split('*');
                //Populate Records
                var nav = NavConnection.ReturnNav();
                var query = nav.InvitationPrequalification.Where(x => x.Code == vendorNo && x.Document_Type=="Invitation for Registation" && x.Status == "Released" && x.Published == true).ToList();
                foreach (var responses in query)
                {
                    IFPRequestsModel response = new IFPRequestsModel();
                    response.Code = responses.Code;
                    response.PrequalificationNo = res[1];
                    response.Document_Type = responses.Document_Type;
                    response.Vendor_Address = responses.Address;
                    response.Primary_Target_Vendor_Cluster = responses.Primary_Target_Vendor_Cluster;
                    response.Vendor_Address2 = responses.Address_2;
                    response.Document_Date = Convert.ToString(responses.Document_Date);
                    response.Period_Start_Date = Convert.ToString(responses.Period_Start_Date);
                    response.Period_End_Date = Convert.ToString(responses.Period_End_Date);
                    response.Vendor_Address2 = responses.Address_2;
                    response.Tender_Box_Location_Code = responses.Tender_Box_Location_Code;
                    response.Tender_Summary = responses.Tender_Summary;
                    response.Region = responses.Responsibility_Centre;
                    response.constituency = responses.Constituency;
                    response.RFI_Documents_No = responses.Code;
                    list.Add(response);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return list;
        }
        private static List<GoodsServicesModel> GoodsServicesDetails(string tenderresponseNumber, string vendorNo)
        {
            List<GoodsServicesModel> list = new List<GoodsServicesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.RFIPreqList.Where(x => x.Document_No == tenderresponseNumber && x.Procurement_Type == "GOODS").ToList();
                foreach (var responses in query)
                {
                    GoodsServicesModel response = new GoodsServicesModel();
                    response.Document_No = responses.Document_No;
                    response.Prequalification_Category_ID = responses.Prequalification_Category_ID;
                    response.Description = responses.Description;
                    response.Period_Start_Date = Convert.ToString(responses.Period_Start_Date);
                    response.Submission_Start_Date = Convert.ToString(responses.Submission_Start_Date);
                    response.Submission_End_Date = Convert.ToString(responses.Submission_End_Date);
                    response.Applicable_Location = responses.Applicable_Location;
                    response.Restricted_RC_Type = responses.Restricted_RC_Type;
                    response.SpecialGroupReservations = Convert.ToString(responses.Special_Group_Reservation);
                    if (response.SpecialGroupReservations == "True")
                    {

                        response.SpecialGroupReservations = "Yes";
                    }
                    else
                    {
                        response.SpecialGroupReservations = "No";
                    }
                    response.Procurement_Type = responses.Procurement_Type;
                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }

        private static List<ServicesModel> ServicesDetails(string tenderresponseNumber, string vendorNo)
        {
            List<ServicesModel> list = new List<ServicesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.RFIPreqList.Where(x => x.Document_No == tenderresponseNumber && x.Procurement_Type == "SERVICES").ToList();
                foreach (var responses in query)
                {
                    ServicesModel response = new ServicesModel();
                    response.Document_No = responses.Document_No;
                    response.Prequalification_Category_ID = responses.Prequalification_Category_ID;
                    response.Description = responses.Description;
                    response.Period_Start_Date = Convert.ToString(responses.Period_Start_Date);
                    response.Submission_Start_Date = Convert.ToString(responses.Submission_Start_Date);
                    response.Submission_End_Date = Convert.ToString(responses.Submission_End_Date);
                    response.Applicable_Location = responses.Applicable_Location;
                    response.Restricted_RC_Type = responses.Restricted_RC_Type;
                    response.SpecialGroupReservations = Convert.ToString(responses.Special_Group_Reservation);
                    if (response.SpecialGroupReservations == "True")
                    {

                        response.SpecialGroupReservations = "Yes";
                    }
                    else
                    {
                        response.SpecialGroupReservations = "No";
                    }
                    response.Procurement_Type = responses.Procurement_Type;
                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<WorksModel> WorksDetails(string tenderresponseNumber, string vendorNo)
        {
            List<WorksModel> list = new List<WorksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.RFIPreqList.Where(x => x.Document_No == tenderresponseNumber && x.Procurement_Type == "WORKS").ToList();
                foreach (var responses in query)
                {
                    WorksModel response = new WorksModel();
                    response.Document_No = responses.Document_No;
                    response.Prequalification_Category_ID = responses.Prequalification_Category_ID;
                    response.Description = responses.Description;
                    response.Period_Start_Date = Convert.ToString(responses.Period_Start_Date);
                    response.Submission_Start_Date = Convert.ToString(responses.Submission_Start_Date);
                    response.Submission_End_Date = Convert.ToString(responses.Submission_End_Date);
                    response.Applicable_Location = responses.Applicable_Location;
                    response.Restricted_RC_Type = responses.Restricted_RC_Type;
                    response.SpecialGroupReservations = Convert.ToString(responses.Special_Group_Reservation);
                    if (response.SpecialGroupReservations == "True")
                    {

                        response.SpecialGroupReservations = "Yes";
                    }
                    else
                    {
                        response.SpecialGroupReservations = "No";
                    }
                    response.Procurement_Type = responses.Procurement_Type;
                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<DocumentsTModel> RequiredDocumentsDetails(string tenderresponseNumber, string vendorNo)
        {
            List<DocumentsTModel> list = new List<DocumentsTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifpReqDocuments.Where(x => x.Document_No == tenderresponseNumber && x.Document_Type == "Invitation for Prequalification" && x.Specialized_Provider_Req == false).ToList();
                foreach (var responses in query)
                {
                    DocumentsTModel response = new DocumentsTModel();
                    response.Document_No = responses.Document_No;
                    response.Procurement_Document_Type_ID = responses.Procurement_Document_Type_ID;
                    response.Requirement_Type = responses.Requirement_Type;
                    response.Description = responses.Description;
                    response.Tracks_Certificate_Expiry = Convert.ToString(responses.Track_Certificate_Expiry);
                    if (response.Tracks_Certificate_Expiry == "True")
                    {

                        response.Tracks_Certificate_Expiry = "Yes";
                    }
                    else
                    {
                        response.Tracks_Certificate_Expiry = "No";
                    }
                    response.SpecialGroupRequirement = Convert.ToString(responses.Special_Group_Requirement);
                    if (response.SpecialGroupRequirement == "True")
                    {

                        response.SpecialGroupRequirement = "Yes";
                    }
                    else
                    {
                        response.SpecialGroupRequirement = "No";
                    }
                    response.SpecialisedRequirement = Convert.ToString(responses.Specialized_Provider_Req);
                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<DocumentsTModel> SpecificRequiredDocuments(string tenderresponseNumber, string vendorNo)
        {
            List<DocumentsTModel> list = new List<DocumentsTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifpReqDocuments.Where(x => x.Document_No == tenderresponseNumber && x.Document_Type == "Invitation For Prequalification" && x.Specialized_Provider_Req==true).ToList();
                foreach (var responses in query)
                {
                    DocumentsTModel response = new DocumentsTModel();
                    response.Document_No = responses.Document_No;
                    response.Procurement_Document_Type_ID = responses.Procurement_Document_Type_ID;
                    response.Requirement_Type = responses.Requirement_Type;
                    response.Description = responses.Description;
                    response.category = responses.Linked_To_category_No;
                    response.Tracks_Certificate_Expiry = Convert.ToString(responses.Track_Certificate_Expiry);
                    if (response.Tracks_Certificate_Expiry == "True")
                    {

                        response.Tracks_Certificate_Expiry = "Yes";
                    }
                    else
                    {
                        response.Tracks_Certificate_Expiry = "No";
                    }
                    response.SpecialGroupRequirement = Convert.ToString(responses.Special_Group_Requirement);
                    if (response.SpecialGroupRequirement == "True")
                    {

                        response.SpecialGroupRequirement = "Yes";
                    }
                    else
                    {
                        response.SpecialGroupRequirement = "No";
                    }
                    response.SpecialisedRequirement = Convert.ToString(responses.Specialized_Provider_Req);
                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }

        private static List<DocumentsTModel> RegistrationRequiredDocumentsDetails(string tenderresponseNumber, string vendorNo)
        {
            List<DocumentsTModel> list = new List<DocumentsTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifpReqDocuments.Where(x => x.Document_No == tenderresponseNumber && x.Document_Type == "Invitation for Registation" && x.Specialized_Provider_Req == false).ToList();
                foreach (var responses in query)
                {
                    DocumentsTModel response = new DocumentsTModel();
                    response.Document_No = responses.Document_No;
                    response.Procurement_Document_Type_ID = responses.Procurement_Document_Type_ID;
                    response.Requirement_Type = responses.Requirement_Type;
                    response.Description = responses.Description;
                    response.Tracks_Certificate_Expiry = Convert.ToString(responses.Track_Certificate_Expiry);
                    if (response.Tracks_Certificate_Expiry == "True")
                    {

                        response.Tracks_Certificate_Expiry = "Yes";
                    }
                    else
                    {
                        response.Tracks_Certificate_Expiry = "No";
                    }
                    response.SpecialGroupRequirement = Convert.ToString(responses.Special_Group_Requirement);
                    if (response.SpecialGroupRequirement == "True")
                    {

                        response.SpecialGroupRequirement = "Yes";
                    }
                    else
                    {
                        response.SpecialGroupRequirement = "No";
                    }
                    response.SpecialisedRequirement = Convert.ToString(responses.Specialized_Provider_Req);
                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<DocumentsTModel> RegistrationSpecificRequiredDocuments(string tenderresponseNumber, string vendorNo)
        {
            List<DocumentsTModel> list = new List<DocumentsTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifpReqDocuments.Where(x => x.Document_No == tenderresponseNumber && x.Document_Type == "Invitation for Registation" && x.Specialized_Provider_Req == true).ToList();
                foreach (var responses in query)
                {
                    DocumentsTModel response = new DocumentsTModel();
                    response.Document_No = responses.Document_No;
                    response.Procurement_Document_Type_ID = responses.Procurement_Document_Type_ID;
                    response.Requirement_Type = responses.Requirement_Type;
                    response.Description = responses.Description;
                    response.category = responses.Linked_To_category_No;
                    response.Tracks_Certificate_Expiry = Convert.ToString(responses.Track_Certificate_Expiry);
                    if (response.Tracks_Certificate_Expiry == "True")
                    {

                        response.Tracks_Certificate_Expiry = "Yes";
                    }
                    else
                    {
                        response.Tracks_Certificate_Expiry = "No";
                    }
                    response.SpecialGroupRequirement = Convert.ToString(responses.Special_Group_Requirement);
                    if (response.SpecialGroupRequirement == "True")
                    {

                        response.SpecialGroupRequirement = "Yes";
                    }
                    else
                    {
                        response.SpecialGroupRequirement = "No";
                    }
                    response.SpecialisedRequirement = Convert.ToString(responses.Specialized_Provider_Req);
                    list.Add(response);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        public ActionResult CompanySize()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<BusinessSizeModel> CompanySize = new List<BusinessSizeModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.companySizes.ToList();
                    foreach (var companysize in query)
                    {
                        BusinessSizeModel bizsizes = new BusinessSizeModel();
                        bizsizes.Code = companysize.Code;
                        bizsizes.Description = companysize.Description;
                        CompanySize.Add(bizsizes);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(CompanySize);
            }
        }
        public ActionResult DocumentTemplateDroplist( )
        {
            List<DocumentsTModel> AllDocuments = new List<DocumentsTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifpReqDocuments.Where(x=> x.Document_Type == "Invitation For Prequalification").ToList();
                foreach (var documents in query)
                {
                    DocumentsTModel document = new DocumentsTModel();
                    document.Procurement_Document_Type_ID = documents.Procurement_Document_Type_ID;
                    document.Description = documents.Description;
                    AllDocuments.Add(document);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(AllDocuments);
        }
        public ActionResult TenderDocumentTemplateDroplist()
        {
            List<DocumentsTModel> AllDocuments = new List<DocumentsTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var TenderNumber= Request.QueryString["respondtendernumber"]; 
                var query = nav.ifsReqDocuments.Where(x => x.Document_No == TenderNumber).ToList();
                foreach (var documents in query)
                {
                    DocumentsTModel document = new DocumentsTModel();
                    document.Procurement_Document_Type_ID = documents.Procurement_Document_Type_ID;
                    document.Description = documents.Description;
                    AllDocuments.Add(document);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(AllDocuments);
        }
        public ActionResult PrequalificationDropDownListDocs()
        {
           List<DocumentsTModel> list = new List<DocumentsTModel>();           
            try
                {
                  string InvitationNumber = Request.QueryString["InvitationNumber"];
                  var nav = NavConnection.ReturnNav();
                    var query = nav.ifpReqDocuments.Where(x => x.Document_No == InvitationNumber && x.Document_Type == "Invitation for Prequalification").ToList();
                    foreach (var filedocuments in query)
                    {
                        DocumentsTModel documents = new DocumentsTModel();
                        documents.Document_Type = filedocuments.Document_Type;
                        documents.Procurement_Document_Type_ID = filedocuments.Procurement_Document_Type_ID;
                        documents.Description = filedocuments.Description;
                        documents.Requirement_Type = Convert.ToString(filedocuments.Requirement_Type);
                        list.Add(documents);

                    }
                }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(list);
        }
        public ActionResult RegisterDocumentTemplateDroplist()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<RegistrationRequiredDocumentsModel> AllDocuments = new List<RegistrationRequiredDocumentsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.eProcDocuments.Where(x => x.Procurement_Process == "Registration").ToList();
                    foreach (var documents in query)
                    {
                        RegistrationRequiredDocumentsModel filedocuments = new RegistrationRequiredDocumentsModel();
                        filedocuments.Template_ID = documents.Template_ID;
                        filedocuments.Description = documents.Description;
                        filedocuments.Procurement_Document_Type = documents.Procurement_Document_Type;
                        filedocuments.Requirement_Type = documents.Requirement_Type;
                        AllDocuments.Add(filedocuments);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(AllDocuments);
            }
        }
        public ActionResult PostalCodeList()
        {
            List<DropdownListsModel> postacode = new List<DropdownListsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.postcodes.ToList();
                foreach (var postacodes in query)
                {
                    DropdownListsModel postcodes = new DropdownListsModel();
                    postcodes.Code = postacodes.Code;
                    postcodes.City = postacodes.City;
                    postcodes.CountryName = postacodes.Country_Region_Code;
                    postacode.Add(postcodes);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(postacode);
        }
        public ActionResult BankCodesList()
        {
            List<DropdownListsModel> postacode = new List<DropdownListsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.postcodes.ToList();
                foreach (var postacodes in query)
                {
                    DropdownListsModel postcodes = new DropdownListsModel();
                    postcodes.Code = postacodes.Code;
                    postcodes.City = postacodes.City;
                    postcodes.CountryName = postacodes.Country_Region_Code;
                    postacode.Add(postcodes);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(postacode);
        }

        public ActionResult selectRegion()
        {
            List<ResponsibilityCenter> postacode = new List<ResponsibilityCenter>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ResponsibilityCenters.Where(r=>r.Operating_Unit_Type=="Region").ToList();
                foreach (var postacodes in query)
                {
                    ResponsibilityCenter postcodes = new ResponsibilityCenter();
                    postcodes.code = postacodes.Code;
                    postcodes.name = postacodes.Name;
                    //postcodes.CountryName = postacodes.Country_Region_Code;
                    postacode.Add(postcodes);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(postacode);
        }
        public ActionResult PrequalificationConstituencies()
        {
            List<ResponsibilityCenter> postacode = new List<ResponsibilityCenter>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ResponsibilityCenters.Where(r => r.Operating_Unit_Type == "Constituency").ToList();
                foreach (var postacodes in query)
                {
                    ResponsibilityCenter postcodes = new ResponsibilityCenter();
                    postcodes.code = postacodes.Code;
                    postcodes.name = postacodes.Name;
                    //postcodes.CountryName = postacodes.Country_Region_Code;
                    postacode.Add(postcodes);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(postacode);
        }
        public JsonResult SelectedPosta(string postcode)
        {
            List<DropdownListsModel> postacode = new List<DropdownListsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.postcodes.ToList();
                foreach (var postacodes in query)
                {
                    DropdownListsModel postcodes = new DropdownListsModel();
                    postcodes.Code = postacodes.Code;
                    postcodes.City = postacodes.City;
                    postcodes.CountryName = postacodes.Country_Region_Code;
                    postacode.Add(postcodes);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            var result = (from a in postacode where a.Code == postcode select a.City).FirstOrDefault();

            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("notfound", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectedBidSecurity(string postcode)
        {
            List<TenderSecurityTypes> list = new List<TenderSecurityTypes>();
            //List<DropdownListsModel> postacode = new List<DropdownListsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.TenderSecurityTypes.ToList();
                foreach (var securitytypes in query)
                {
                    TenderSecurityTypes securitytype = new TenderSecurityTypes();
                    securitytype.Code = securitytypes.Code;
                    securitytype.Security_Type = securitytypes.Security_Type;
                    securitytype.Description = securitytypes.Description;
                    securitytype.Nature_of_Security = securitytypes.Nature_of_Security;
                    list.Add(securitytype);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            var result = (from a in list where a.Code == postcode select a.Description).FirstOrDefault();

            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("notfound", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectedBank(string bankcode)
        {
            var details = new Object();
            List<BankModel> bank = new List<BankModel>();
            try
            {

                var nav = NavConnection.ReturnNav();
                var query = nav.BankBranches.Where(x=>x.Code==bankcode).ToList();
                foreach (var bankacodes in query)
                {
                    BankModel bankcodes = new BankModel();
                    bankcodes.BankCode = bankacodes.Code;
                    bankcodes.BankName = bankacodes.Bank_Name;
                    bankcodes.bankBranchName = bankacodes.Branch_Name;
                    bankcodes.Bank_Branch_No = bankacodes.Bank_Branch_No;
                    bank.Add(bankcodes);



                }
                //details = new {branchcode = bank}


            }
            catch (Exception e)
            {

                throw;
            }
            var result = (from a in bank where a.BankCode == bankcode select new SelectListItem { Text = a.bankBranchName, Value = a.Bank_Branch_No }).Distinct().ToList().FirstOrDefault();
            //var branchname = (from a in bank where a.Bank_Branch_No == bankcode select a.bankBranchName).FirstOrDefault();

            if (bank != null)
            {
                return Json(bank, JsonRequestBehavior.AllowGet);

            }
            return Json("notfound", JsonRequestBehavior.AllowGet);
        }

        public JsonResult selectedResponsibilityCenter(string regionCode)
        {
            List<ResponsibilityCenter> RC = new List<ResponsibilityCenter>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ResponsibilityCenters.Where(r=>r.Operating_Unit_Type== "Constituency").ToList();
                foreach (var bankacodes in query)
                {
                    ResponsibilityCenter regionCde = new ResponsibilityCenter();
                    regionCde.code = bankacodes.Code;
                    regionCde.name = bankacodes.Name;
                    regionCde.locationCode = bankacodes.Location_Code;
                    //regionCode.bankBranchName = bankacodes.Branch_Name;
                    RC.Add(regionCde);



                }


            }
            catch (Exception e)
            {

                throw;
            }
            var result = (from a in RC where a.locationCode == regionCode select a.constituencyCode).FirstOrDefault();
           

            if (result != null )
            {
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            return Json("notfound", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSelectedDocumentExpiryDate(string SelectedDocument)
        {
            List<DocumentsTModel> expiryDate = new List<DocumentsTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.eProcDocuments.Where(x => x.Procurement_Process == "Registration"&&x.Description==SelectedDocument).ToList();
                foreach (var documents in query)
                {
                    if (documents.Track_Certificate_Expiry == true)
                    {
                        return Json("success*", JsonRequestBehavior.AllowGet);
                    }else
                    {
                        return Json("danger*", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("danger*", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public JsonResult GetSelectedPrequalificationDocumentExpiryDate(string SelectedDocument)
        {
            List<DocumentsTModel> expiryDate = new List<DocumentsTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifpReqDocuments.Where(x =>x.Document_Type == "Invitation For Prequalification"&&x.Description== SelectedDocument).ToList();
                foreach (var documents in query)
                {
                    if (documents.Track_Certificate_Expiry == true)
                    {
                        return Json("success*", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("danger*", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("danger*", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public JsonResult GetSelectedTenderDocumentDocumentExpiryDate(string SelectedDocument)
        {
            List<DocumentsTModel> expiryDate = new List<DocumentsTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ifpReqDocuments.Where(x => x.Document_No == SelectedDocument).ToList();
                foreach (var documents in query)
                {
                    if (documents.Track_Certificate_Expiry == true)
                    {
                        return Json("success*", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("danger*", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json("danger*", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public ActionResult RFIResponse()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<IFPRequestsModel> list = new List<IFPRequestsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var today = DateTime.Today;
                    //var query = nav.InvitationPrequalification.Where(x => x.Status == "Released" && x.Published == true && x.Document_Type == "Invitation For Prequalification"&&x.Submission_End_Date>=today && x.Type== "Sub IFP").ToList();
                    var query = nav.InvitationPrequalification.Where(x => x.Status == "Released" && x.Published == true && x.Document_Type == "Invitation For Prequalification"  && x.Type == "Sub IFP").ToList();
                    foreach (var invitations in query)
                    {
                        IFPRequestsModel invite = new IFPRequestsModel();
                        invite.Code = invitations.Code;
                        invite.Description = invitations.Description;
                        invite.Tender_Box_Location_Code = invitations.Responsibility_Centre;
                        invite.External_Document_No = invitations.External_Document_No;
                        invite.Tender_Summary = invitations.Tender_Summary;
                      //  invite.Submission_End_Date = Convert.ToString(invitations.Submission_End_Date);
                      //  invite.Submission_Start_Date = Convert.ToString(invitations.Submission_Start_Date);
                        invite.bidscoringTemplate = invitations.RFI_Scoring_Template;
                        list.Add(invite);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult RegistrationForm()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<IFPRequestsModel> list = new List<IFPRequestsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var today = DateTime.Today;
                    var query = nav.InvitationPrequalification.Where(x => x.Status == "Released" && x.Published == true && x.Document_Type == "Invitation for Registation" && x.Submission_End_Date >= today && x.Type == "Sub IFP").ToList();
                    foreach (var invitations in query)
                    {
                        IFPRequestsModel invite = new IFPRequestsModel();
                        invite.Code = invitations.Code;
                        invite.Description = invitations.Description;
                        invite.Tender_Box_Location_Code = invitations.Responsibility_Centre;
                        invite.External_Document_No = invitations.External_Document_No;
                        invite.Tender_Summary = invitations.Tender_Summary;
                        invite.Submission_End_Date = Convert.ToString(invitations.Submission_End_Date);
                        invite.Submission_Start_Date = Convert.ToString(invitations.Submission_Start_Date);
                        invite.bidscoringTemplate = invitations.RFI_Scoring_Template;
                        invite.Code = invitations.Code;
                        list.Add(invite);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult SubmittedResponses()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<SubmittedResponsesModel> list = new List<SubmittedResponsesModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.rfiResponseT.Where(x => x.Vendor_No == vendorNo && x.Document_Type == "IFP Response").ToList();
                    foreach (var responses in query)
                    {
                        SubmittedResponsesModel response = new SubmittedResponsesModel();
                        response.Code = responses.Document_No;
                        response.Documents_Date = Convert.ToString(responses.Document_Date);
                        response.RfiDocumentNo = responses.RFI_Document_No;
                        response.Vendor_Representative_Name = responses.Vendor_Representative_Name;
                        response.Vendor_Repr_Designation = responses.Vendor_Repr_Designation;
                        response.Final_Evaluation_Score = responses.Final_Evaluation_Score;
                        response.Date_Submitted = Convert.ToString(responses.Date_Submitted); ;
                        list.Add(response);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult SubmittedTenders()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<SubmittedTenderResponse> list = new List<SubmittedTenderResponse>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.BidResponseList.Where(x => x.Buy_from_Vendor_No == vendorNo && x.PP_Procurement_Method == "Open Tender").ToList();
                    foreach (var responses in query)
                    {
                        SubmittedTenderResponse response = new SubmittedTenderResponse();
                        response.ResponseNo = responses.No;
                        response.vendorNo = responses.Buy_from_Vendor_No;
                        response.VendorName = responses.Pay_to_Name;
                        response.status = responses.Status;
                        response.DocumentStatus = responses.Document_Status;
                        response.invitationNoticeType = responses.Invitation_Notice_Type;
                        response.InvitationNo = responses.Invitation_For_Supply_No;
                        response.tenderDescription = responses.Tender_Description;
                        response.endDate = Convert.ToDateTime(responses.Due_Date).ToString("MM/dd/yyyy");
                        list.Add(response);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult submittedRFQ()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<SubmittedTenderResponse> list = new List<SubmittedTenderResponse>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.BidResponseList.Where(x => x.Buy_from_Vendor_No == vendorNo && x.PP_Procurement_Method == "RFQ").ToList();
                    foreach (var responses in query)
                    {
                        SubmittedTenderResponse response = new SubmittedTenderResponse();
                        response.ResponseNo = responses.No;
                        response.vendorNo = responses.Buy_from_Vendor_No;
                        response.VendorName = responses.Pay_to_Name;
                        response.DocumentStatus = responses.Document_Status;
                        response.invitationNoticeType = responses.Invitation_Notice_Type;
                        response.InvitationNo = responses.Invitation_For_Supply_No;
                        response.tenderDescription = responses.Tender_Description;
                        response.endDate = Convert.ToDateTime(responses.Due_Date).ToString("MM/dd/yyyy");
                        list.Add(response);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult SubmittedRegistrationResponses()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<SubmittedResponsesModel> list = new List<SubmittedResponsesModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.rfiResponseT.Where(x => x.Vendor_No == vendorNo && x.Document_Type == "IFR Response").ToList();
                    foreach (var responses in query)
                    {
                        SubmittedResponsesModel response = new SubmittedResponsesModel();
                        response.Code = responses.Document_No;
                        response.Documents_Date = Convert.ToString(responses.Document_Date);
                        response.RfiDocumentNo = responses.RFI_Document_No;
                        response.Vendor_Representative_Name = responses.Vendor_Representative_Name;
                        response.Vendor_Repr_Designation = responses.Vendor_Repr_Designation;
                        response.Final_Evaluation_Score = responses.Final_Evaluation_Score;
                        response.Date_Submitted = Convert.ToString(responses.Date_Submitted); ;
                        list.Add(response);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult TendersLists()
        {
            List<TenderModel> list = new List<TenderModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status=="Published"&&x.Procurement_Method=="Open Tender").ToList();
                foreach (var tenders in query)
                {
                    TenderModel tender = new TenderModel();
                    tender.Code = tenders.Code;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Solicitation_Type = tenders.Solicitation_Type;
                    tender.External_Document_No = tenders.External_Document_No;
                    tender.Procurement_Type = tenders.Procurement_Type;
                    tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                    tender.Project_ID = tenders.Project_ID;
                    tender.Requisition_Product_Group = tenders.Requisition_Product_Group;
                    tender.Tender_Name = tenders.Tender_Name;
                    tender.Tender_Summary = tenders.Tender_Summary;
                    tender.Description = tenders.Description;
                    tender.Document_Date = tenders.Document_Date;
                    tender.Status = tenders.Status;
                    tender.Name = tenders.Name;
                    tender.Submission_End_Date = tenders.Submission_End_Date;
                    tender.Submission_Start_Date = tenders.Submission_Start_Date;
                    tender.Published = tenders.Published;
                    list.Add(tender);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult SingleTender()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                return View();
            }
        }
        public ActionResult EditSupplierProfile()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult SpecialGroupTenders()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<TenderModel> list = new List<TenderModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status == "Published" && x.Mandatory_Special_Group_Reserv == true && x.Status=="Released" &&x.Procurement_Method=="Open Tender").ToList();
                    foreach (var tenders in query)
                    {
                        TenderModel tender = new TenderModel();
                        tender.Code = tenders.Code;
                        tender.Procurement_Method = tenders.Procurement_Method;
                        tender.Solicitation_Type = tenders.Solicitation_Type;
                        tender.External_Document_No = tenders.External_Document_No;
                        tender.Procurement_Type = tenders.Procurement_Type;
                        tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                        tender.Project_ID = tenders.Project_ID;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Tender_Summary = tenders.Tender_Summary;
                        tender.Description = tenders.Description;
                        tender.Document_Date = tenders.Document_Date;
                        tender.Status = tenders.Status;
                        tender.Name = tenders.Name;
                        tender.Submission_End_Date = tenders.Submission_End_Date;
                        tender.Submission_Start_Date = tenders.Submission_Start_Date;
                        tender.Bid_Scoring_Template = tenders.Bid_Scoring_Template;
                        tender.Published = tenders.Published;
                        list.Add(tender);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult TendersbyRegions()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<TenderModel> list = new List<TenderModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var today = DateTime.Today;
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status=="Published" &&x.Status=="Released" &&x.Procurement_Method=="Open Tender" && x.Submission_End_Date>=today ).ToList();
                    foreach (var tenders in query)
                    {
                        TenderModel tender = new TenderModel();
                        tender.Code = tenders.Code;
                        tender.Procurement_Method = tenders.Procurement_Method;
                        tender.Solicitation_Type = tenders.Solicitation_Type;
                        tender.External_Document_No = tenders.External_Document_No;
                        tender.Procurement_Type = tenders.Procurement_Type;
                        tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                        tender.Project_ID = tenders.Project_ID;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Tender_Summary = tenders.Tender_Summary;
                        tender.Description = tenders.Description;
                        tender.Document_Date = tenders.Document_Date;
                        tender.Status = tenders.Status;
                        tender.Name = tenders.Name;
                        tender.Submission_End_Date = tenders.Submission_End_Date;
                        tender.Submission_Start_Date = tenders.Submission_Start_Date;
                        tender.Published = tenders.Published;
                        list.Add(tender);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult TendersbyClosingDates()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {

                List<TenderModel> list = new List<TenderModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    DateTime Today = DateTime.Now;
                    var today = DateTime.Now.ToString("dd/MM/yyyy");
                    var ranges = new List<int?> { 0, 14 };
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status=="Published"&&x.Procurement_Method=="Open Tender").ToList();
                    foreach (var tenders in query)
                    {
                        TenderModel tender = new TenderModel();
                        tender.Code = tenders.Code;
                        tender.Procurement_Method = tenders.Procurement_Method;
                        tender.Solicitation_Type = tenders.Solicitation_Type;
                        tender.External_Document_No = tenders.External_Document_No;
                        tender.Procurement_Type = tenders.Procurement_Type;
                        tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                        tender.Project_ID = tenders.Project_ID;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Tender_Summary = tenders.Tender_Summary;
                        tender.Description = tenders.Description;
                        tender.Document_Date = tenders.Document_Date;
                        tender.Status = tenders.Status;
                        tender.Name = tenders.Name;
                        tender.Submission_End_Date = tenders.Submission_End_Date;
                        tender.Submission_Start_Date = tenders.Submission_Start_Date;
                        tender.Published = tenders.Published;
                        list.Add(tender);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        private static List<TenderModel> GetAllTendersClosingToday()
        {
            List<TenderModel> list = new List<TenderModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                DateTime Today = DateTime.Now;
                var today = DateTime.Today;
                var ranges = new List<int?> { 0, 14 };
                var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status =="Published" && x.Submission_End_Date == Today &&x.Procurement_Method=="Open Tender").ToList();
                foreach (var tenders in query)
                {
                    TenderModel tender = new TenderModel();
                    tender.Code = tenders.Code;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Solicitation_Type = tenders.Solicitation_Type;
                    tender.External_Document_No = tenders.External_Document_No;
                    tender.Procurement_Type = tenders.Procurement_Type;
                    tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                    tender.Project_ID = tenders.Project_ID;
                    tender.Tender_Name = tenders.Tender_Name;
                    tender.Tender_Summary = tenders.Tender_Summary;
                    tender.Description = tenders.Description;
                    tender.Document_Date = tenders.Document_Date;
                    tender.Status = tenders.Status;
                    tender.Name = tenders.Name;
                    tender.Submission_End_Date = tenders.Submission_End_Date;
                    tender.Submission_Start_Date = tenders.Submission_Start_Date;
                    tender.Published = tenders.Published;
                    list.Add(tender);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return list;
        }
        private static List<TenderModel> GetAllTendersClosing7Todays()
        {
            List<TenderModel> list = new List<TenderModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                DateTime Today = DateTime.Now;
                var today = DateTime.Today;
                var ranges = new List<int?> { 0, 7 };
                var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status == "Published" && x.Submission_End_Date == Today &&x.Procurement_Method=="Open Tender").ToList();
                foreach (var tenders in query)
                {
                    TenderModel tender = new TenderModel();
                    tender.Code = tenders.Code;
                    tender.Procurement_Method = tenders.Procurement_Method;
                    tender.Solicitation_Type = tenders.Solicitation_Type;
                    tender.External_Document_No = tenders.External_Document_No;
                    tender.Procurement_Type = tenders.Procurement_Type;
                    tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                    tender.Project_ID = tenders.Project_ID;
                    tender.Tender_Name = tenders.Tender_Name;
                    tender.Tender_Summary = tenders.Tender_Summary;
                    tender.Description = tenders.Description;
                    tender.Document_Date = tenders.Document_Date;
                    tender.Status = tenders.Status;
                    tender.Name = tenders.Name;
                    tender.Submission_End_Date = tenders.Submission_End_Date;
                    tender.Submission_Start_Date = tenders.Submission_Start_Date;
                    tender.Published = tenders.Published;
                    list.Add(tender);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return list;
        }
        public ActionResult ActiveTenderAddedum()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<TenderAddendums> list = new List<TenderAddendums>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.tenderAddendums.Where(x => x.Document_Status == "Published" && x.Status == "Released").ToList();
                    foreach (var tenders in query)
                    {
                        TenderAddendums tender = new TenderAddendums();
                        tender.Addendum_Notice_No = tenders.Addendum_Notice_No;
                        tender.Document_Date = Convert.ToString(tenders.Document_Date);
                        tender.Description = tenders.Description;
                        tender.Addendum_Instructions = tenders.Addendum_Instructions;
                        tender.Primary_Addendum_Type_ID = tenders.Primary_Addendum_Type_ID;
                        tender.Addendum_Type_Description = tenders.Addendum_Type_Description;
                        tender.Tender_No = tenders.Tender_No;
                        tender.Invitation_Notice_No = tenders.Invitation_Notice_No;
                        tender.Tender_Description = tenders.Tender_Description;
                        tender.Responsibility_Center = tenders.Responsibility_Center;
                        tender.New_Submission_Start_Date = Convert.ToString(tenders.New_Submission_Start_Date);
                        tender.New_Submission_End_Date = Convert.ToString(tenders.New_Submission_End_Date);
                        tender.Status = tenders.Status;
                        tender.Original_Submission_End_Time = tenders.Original_Submission_End_Time;
                        tender.Original_Bid_Opening_Date = Convert.ToString(tenders.Original_Bid_Opening_Date);
                        tender.New_Bid_Opening_Time = tenders.New_Bid_Opening_Time;
                        tender.Original_Prebid_Meeting_Date = Convert.ToString(tenders.Original_Prebid_Meeting_Date);
                        tender.Document_Status = tenders.Document_Status;
                        tender.Original_Prebid_Meeting_Date = Convert.ToString(tenders.Original_Prebid_Meeting_Date);
                        list.Add(tender);
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult ClosedTenders()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<TenderModel> list = new List<TenderModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status== "Cancelled" &&x.Procurement_Method=="Open Tender").ToList();
                    foreach (var tenders in query)
                    {
                        TenderModel tender = new TenderModel();
                        tender.Code = tenders.Code;
                        tender.Procurement_Method = tenders.Procurement_Method;
                        tender.Solicitation_Type = tenders.Solicitation_Type;
                        tender.External_Document_No = tenders.External_Document_No;
                        tender.Procurement_Type = tenders.Procurement_Type;
                        tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                        tender.Project_ID = tenders.Project_ID;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Tender_Summary = tenders.Tender_Summary;
                        tender.Description = tenders.Description;
                        tender.Document_Date = tenders.Document_Date;
                        tender.Status = tenders.Status;
                        tender.Name = tenders.Name;
                        tender.Submission_End_Date = tenders.Submission_End_Date;
                        tender.Submission_Start_Date = tenders.Submission_Start_Date;
                        tender.Published = tenders.Published;
                        list.Add(tender);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult ActiveExpressionInterest()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<TenderModel> list = new List<TenderModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var today = DateTime.Today;
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status=="Published" &&x.Procurement_Method== "Open Tender"&&x.Submission_End_Date>=today).ToList();
                    foreach (var tenders in query)
                    {
                        TenderModel tender = new TenderModel();
                        tender.Code = tenders.Code;
                        tender.Procurement_Method = tenders.Procurement_Method;
                        tender.Solicitation_Type = tenders.Solicitation_Type;
                        tender.External_Document_No = tenders.External_Document_No;
                        tender.Procurement_Type = tenders.Procurement_Type;
                        tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                        tender.Project_ID = tenders.Project_ID;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Tender_Summary = tenders.Tender_Summary;
                        tender.Description = tenders.Description;
                        tender.Document_Date = tenders.Document_Date;
                        tender.Status = tenders.Status;
                        tender.Name = tenders.Name;
                        tender.Submission_End_Date = tenders.Submission_End_Date;
                        tender.Submission_Start_Date = tenders.Submission_Start_Date;
                        tender.Published = tenders.Published;
                        list.Add(tender);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult ContractAwards()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<TenderModel> list = new List<TenderModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var today = DateTime.Today;
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status=="Published" &&x.Procurement_Method== "Open Tender" && x.Submission_End_Date>=today).ToList();
                    foreach (var tenders in query)
                    {
                        TenderModel tender = new TenderModel();
                        tender.Code = tenders.Code;
                        tender.Procurement_Method = tenders.Procurement_Method;
                        tender.Solicitation_Type = tenders.Solicitation_Type;
                        tender.External_Document_No = tenders.External_Document_No;
                        tender.Procurement_Type = tenders.Procurement_Type;
                        tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                        tender.Project_ID = tenders.Project_ID;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Tender_Summary = tenders.Tender_Summary;
                        tender.Description = tenders.Description;
                        tender.Document_Date = tenders.Document_Date;
                        tender.Status = tenders.Status;
                        tender.Name = tenders.Name;
                        tender.Submission_End_Date = tenders.Submission_End_Date;
                        tender.Submission_Start_Date = tenders.Submission_Start_Date;
                        tender.Published = tenders.Published;
                        list.Add(tender);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult DebarmentList()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<TenderVDerbarmentTModel> list = new List<TenderVDerbarmentTModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.ifsVendorDebarment.Where(x => x.Vendor_No == vendorNo).ToList();
                    foreach (var tenders in query)
                    {
                        TenderVDerbarmentTModel tender = new TenderVDerbarmentTModel();
                        tender.Entry_no = Convert.ToString(tenders.Entry_no);
                        tender.Source_Voucher_No = tenders.Source_Voucher_No;
                        tender.Document_Type = tenders.Document_Type;
                        tender.Reason_Code = tenders.Reason_Code;
                        tender.Firm_Name = tenders.Firm_Name;
                        tender.Reason_Code = tenders.Reason_Code;
                        tender.Description = tenders.Description;
                        tender.Ineligibility_End_Date = Convert.ToString(tenders.Ineligibility_End_Date);
                        tender.Ineligibility_Start_Date = Convert.ToString(tenders.Ineligibility_Start_Date);
                        tender.Reinstatement_Date = Convert.ToString(tenders.Reinstatement_Date);
                        tender.Blocked = Convert.ToBoolean(tenders.Blocked);
                        tender.Country_Region_Code = tenders.Country_Region_Code;
                        tender.Address = tenders.Address;
                        tender.Incorporation_Reg_No = tenders.Incorporation_Reg_No;
                        tender.Tax_Registration_PIN_No = tenders.Tax_Registration_PIN_No;
                        list.Add(tender);
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult AnnouncementsList()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                List<TenderModel> list = new List<TenderModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var today = DateTime.Today;
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status=="Published" &&x.Procurement_Method== "Open Tender" && x.Submission_End_Date>=today).ToList();
                    foreach (var tenders in query)
                    {
                        TenderModel tender = new TenderModel();
                        tender.Code = tenders.Code;
                        tender.Procurement_Method = tenders.Procurement_Method;
                        tender.Solicitation_Type = tenders.Solicitation_Type;
                        tender.External_Document_No = tenders.External_Document_No;
                        tender.Procurement_Type = tenders.Procurement_Type;
                        tender.Procurement_Category_ID = tenders.Procurement_Category_ID;
                        tender.Project_ID = tenders.Project_ID;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Tender_Summary = tenders.Tender_Summary;
                        tender.Description = tenders.Description;
                        tender.Document_Date = tenders.Document_Date;
                        tender.Status = tenders.Status;
                        tender.Name = tenders.Name;
                        tender.Submission_End_Date = tenders.Submission_End_Date;
                        tender.Submission_Start_Date = tenders.Submission_Start_Date;
                        tender.Published = tenders.Published;
                        list.Add(tender);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult LanguageCode()
        {
            List<LanguageModel> list = new List<LanguageModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.languages.ToList();
                foreach (var language in query)
                {
                    LanguageModel lenguage = new LanguageModel();
                    lenguage.Code = language.Code;
                    lenguage.Name = language.Name;
                    list.Add(lenguage);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult VendorSpecialGroup()
        {
            List<SpecialGroupCategoryModel> list = new List<SpecialGroupCategoryModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.SpecialGroupCategories.ToList();
                foreach (var groups in query)
                {
                    SpecialGroupCategoryModel group = new SpecialGroupCategoryModel();
                    group.Code = groups.Code;
                    group.Name = groups.Description;
                    list.Add(group);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }

        public ActionResult supplierCategories()
        {
            List<SupplierCategory> list = new List<SupplierCategory>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.suppliercategory.ToList();
                foreach (var groups in query)
                {
                    SupplierCategory group = new SupplierCategory();
                    group.code = groups.Category_Code;
                    group.description = groups.Description;
                    list.Add(group);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }



        public ActionResult IndustryGroups()
        {
            List<IndustryGroupModel> list = new List<IndustryGroupModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.IndustryGroup.ToList();
                foreach (var industry in query)
                {
                    IndustryGroupModel industries = new IndustryGroupModel();
                    industries.Code = industry.Code;
                    industries.Description = industry.Description;
                    list.Add(industries);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult BusinessTypes()
        {
            List<BusinessTypesModel> list = new List<BusinessTypesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.businessTypes.ToList();
                foreach (var biztypes in query)
                {
                    BusinessTypesModel businesstypes = new BusinessTypesModel();
                    businesstypes.Code = biztypes.Code;
                    businesstypes.Description = biztypes.Description;
                    businesstypes.Ownership_Type = biztypes.Ownership_Type;
                    list.Add(businesstypes);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult BidSecuritiesList()
        {
            List<CountriesModel> list = new List<CountriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.Countries.ToList();
                foreach (var countries in query)
                {
                    CountriesModel country = new CountriesModel();
                    country.Code = countries.Code;
                    country.Name = countries.Name;
                    list.Add(country);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult ViewBankCodesList()
        {
            List<BankModel> list = new List<BankModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.BankCodes.ToList();
                foreach (var banks in query)
                {
                    BankModel bank = new BankModel();
                    bank.BankCode = banks.Code;
                    bank.BankName = banks.Bank_Name;
                    list.Add(bank);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult CurrencyCodeLists()
        {
            List<CurrencyModel> list = new List<CurrencyModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.Currencies.ToList();
                foreach (var currencies in query)
                {
                    CurrencyModel currency = new CurrencyModel();
                    currency.Code = currencies.Code;
                    currency.Description = currencies.Description;
                    list.Add(currency);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult EntityType()
        {
            List<EntityType> list = new List<EntityType>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.businessTypes.ToList();
                foreach (var currencies in query)
                {
                    EntityType currency = new EntityType();
                    currency.Code = currencies.Code;
                    currency.Description = currencies.Description;
                    list.Add(currency);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult VendorBanksCountryList()
        {
            List<CountriesModel> list = new List<CountriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.Countries.ToList();
                foreach (var countries in query)
                {
                    CountriesModel country = new CountriesModel();
                    country.Code = countries.Code;
                    country.Name = countries.Name;
                    list.Add(country);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult TenderSecurityLists()
        {

            List<TenderSecurityTypes> list = new List<TenderSecurityTypes>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.TenderSecurityTypes.ToList();
                foreach (var securitytypes in query)
                {
                    TenderSecurityTypes securitytype = new TenderSecurityTypes();
                    securitytype.Code = securitytypes.Code;
                    securitytype.Security_Type = securitytypes.Security_Type;
                    securitytype.Description = securitytypes.Description;
                    securitytype.Nature_of_Security = securitytypes.Nature_of_Security;
                    list.Add(securitytype);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult CountriesList()
        {
            List<CountriesModel> list = new List<CountriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.Countries.ToList();
                foreach (var countries in query)
                {
                    CountriesModel country = new CountriesModel();
                    country.Code = countries.Code;
                    country.Name = countries.Name;
                    list.Add(country);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult RegionsList()
        {
            List<CountriesModel> list = new List<CountriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.Regions.ToList();
                foreach (var regions in query)
                {
                    CountriesModel region = new CountriesModel();
                    region.CountyCode = regions.Code;
                    region.CountyName = regions.Description;
                    list.Add(region);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult ResponsibilityCenters()
        {
            List<ResponsibilityCenter> list = new List<ResponsibilityCenter>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ResponsibilityCenters.Where(r=>r.Operating_Unit_Type == "Region").ToList();
                foreach (var responsibilityCent in query)
                {
                    ResponsibilityCenter RC = new ResponsibilityCenter();
                    RC.code = responsibilityCent.Code;
                    RC.name = responsibilityCent.Name;
                    list.Add(RC);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult ViewPrequalificationsRegions()
        {
            List<CountriesModel> list = new List<CountriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.Regions.ToList();
                foreach (var regions in query)
                {
                    CountriesModel region = new CountriesModel();
                    region.CountyCode = regions.Code;
                    region.CountyName = regions.Description;
                    list.Add(region);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }

        public ActionResult RegistrationPeriod()
        {
            List<CountriesModel> list = new List<CountriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.registrationPeriod.ToList();
                foreach (var regions in query)
                {
                    CountriesModel region = new CountriesModel();
                    region.RegistrationCode = regions.Code;
                    region.RegistrationDescription = regions.Description;
                    list.Add(region);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }


        public ActionResult KeyStaffCountriesList()
        {
            List<CountriesModel> list = new List<CountriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.Countries.ToList();
                foreach (var countries in query)
                {
                    CountriesModel country = new CountriesModel();
                    country.Code = countries.Code;
                    country.Name = countries.Name;
                    list.Add(country);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public JsonResult FnUploadmandatoryDoc(HttpPostedFileBase browsedfile, string typauploadselect, string filedescription, string certificatenumber, DateTime dateofissue, DateTime expirydate)
        {
            try
            {
                
                
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                var nav = new NavConnection().ObjNav();
                string storedFilename = "";
                DateTime startdate, enddate;
                CultureInfo usCulture = new CultureInfo("es-ES");
                int errCounter = 0, succCounter = 0;
                if (dateofissue == null && expirydate == null)
                {
                    dateofissue =Convert.ToDateTime( "");
                    expirydate = Convert.ToDateTime("");
                }

                if (browsedfile == null)
                {
                    errCounter++;
                    return Json("danger*browsedfilenull", JsonRequestBehavior.AllowGet);
                }

                if (vendorNo.Contains(":"))
                    vendorNo = vendorNo.Replace(":", "[58]");
                vendorNo = vendorNo.Replace("/", "[47]");

                if (filedescription.Contains("/"))
                    filedescription = filedescription.Replace("/", "_");

                if (typauploadselect.Contains("/"))
                    typauploadselect = typauploadselect.Replace("/", "_");


                string fileName0 = Path.GetFileName(browsedfile.FileName);
                string ext0 = _getFileextension(browsedfile);
                string savedF0 = vendorNo + "_" + fileName0 + ext0;

                bool up2Sharepoint = _UploadSupplierDocumentToSharepoint(vendorNo, browsedfile, filedescription);
                if (up2Sharepoint == true)
                {
                    string filename = vendorNo + "_" + fileName0 ;
                    string sUrl = ConfigurationManager.AppSettings["S_URL"];
                    string defaultlibraryname = "Procurement%20Documents/";
                    string customlibraryname = "Vendor Card";
                    string sharepointLibrary = defaultlibraryname + customlibraryname;
                    vendorNo = vendorNo.Replace('/', '_');
                    vendorNo = vendorNo.Replace(':', '_');
                    //Sharepoint File Link
                    string sharepointlink = sUrl + sharepointLibrary + "/" + vendorNo + "/" + filename;
                    string fsavestatus = nav.FnInsertFiledetails(vendorNo, typauploadselect, filedescription,
                       certificatenumber, dateofissue, expirydate, filename, sharepointlink);
                    var splitanswer = fsavestatus.Split('*');
                    switch (splitanswer[0])
                    {
                        case "success":
                            return Json("success*" + succCounter, JsonRequestBehavior.AllowGet);
                        default:
                            return Json("danger*" + succCounter, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("sharepointError*", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult FnUploadmandatoryRegistrationDocuments(HttpPostedFileBase browsedfile, string typauploadselect,
          string filedescription, string certificatenumber, string dateofissue, string expirydate)
        {
            try
            {
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                var nav = new NavConnection().ObjNav();
                string storedFilename = "";

                int errCounter = 0, succCounter = 0;
                DateTime startdate, enddate;
                CultureInfo usCulture = new CultureInfo("es-ES");
                DateTime dtofIssue = DateTime.Now;
                DateTime expiryDate = DateTime.Now;
                if (dateofissue == null && expirydate == null)
                {
                    dtofIssue = DateTime.Parse(dateofissue, usCulture.DateTimeFormat);
                    expiryDate = DateTime.Parse(expirydate, usCulture.DateTimeFormat);
                }


                if (browsedfile == null)
                {
                    errCounter++;
                    return Json("danger*browsedfilenull", JsonRequestBehavior.AllowGet);
                }

                if (vendorNo.Contains(":"))
                    vendorNo = vendorNo.Replace(":", "[58]");
                vendorNo = vendorNo.Replace("/", "[47]");

                if (filedescription.Contains("/"))
                    filedescription = filedescription.Replace("/", "_");

                if (typauploadselect.Contains("/"))
                    typauploadselect = typauploadselect.Replace("/", "_");


                string fileName0 = Path.GetFileName(browsedfile.FileName);
                string ext0 = _getFileextension(browsedfile);
                string savedF0 = vendorNo + "_" + fileName0 + ext0;

                bool up2Sharepoint = _UploadSupplierDocumentToSharepoint(vendorNo, browsedfile, filedescription);
                if (up2Sharepoint == true)
                {
                    string filename = vendorNo + "_" + fileName0;
                    string sUrl = ConfigurationManager.AppSettings["S_URL"];
                    string defaultlibraryname = "Procurement%20Documents/";
                    string customlibraryname = "Vendor Card";
                    string sharepointLibrary = defaultlibraryname + customlibraryname;
                    string sharepointlink = sUrl + sharepointLibrary + "/" + vendorNo + "/" + filename;


                    string fsavestatus = nav.FnInsertFiledetails(vendorNo, typauploadselect, filedescription, certificatenumber, dtofIssue, expiryDate, filename, sharepointlink);
                    var splitanswer = fsavestatus.Split('*');
                    switch (splitanswer[0])
                    {
                        case "success":
                            return Json("success*" + succCounter, JsonRequestBehavior.AllowGet);
                        default:
                            return Json("danger*" + succCounter, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("sharepointError*", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult FnUploadmandatoryPrequalificationDoc(string prequalificationNumber, HttpPostedFileBase browsedfile, string typauploadselect,
            string filedescription, string certificatenumber, string dateofissue, string expirydate)
        {
            try
            {
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                var nav = new NavConnection().ObjNav();
                string storedFilename = "";

                int errCounter = 0, succCounter = 0;
                DateTime startdate, enddate;
                CultureInfo usCulture = new CultureInfo("es-ES");
                DateTime dtofIssue = DateTime.Now;
                DateTime expiryDate = DateTime.Now;
                if (dateofissue == null && expirydate == null)
                {
                    dtofIssue = DateTime.Parse(dateofissue, usCulture.DateTimeFormat);
                    expiryDate = DateTime.Parse(expirydate, usCulture.DateTimeFormat);
                }


                if (browsedfile == null)
                {
                    errCounter++;
                    return Json("danger*browsedfilenull", JsonRequestBehavior.AllowGet);
                }

                if (vendorNo.Contains(":"))
                    vendorNo = vendorNo.Replace(":", "[58]");
                vendorNo = vendorNo.Replace("/", "[47]");

                if (filedescription.Contains("/"))
                    filedescription = filedescription.Replace("/", "_");

                if (typauploadselect.Contains("/"))
                    typauploadselect = typauploadselect.Replace("/", "_");


                string fileName0 = Path.GetFileName(browsedfile.FileName);
                string ext0 = _getFileextension(browsedfile);
                string savedF0 = vendorNo + "_" + fileName0 + ext0;

                bool up2Sharepoint = _UploadSupplierPrequalificationsDocumentToSharepoint(prequalificationNumber, browsedfile, filedescription);
                if (up2Sharepoint == true)
                {
                    string filename = vendorNo + "_" + fileName0;
                    string sUrl = ConfigurationManager.AppSettings["S_URL"];
                    string defaultlibraryname = "Procurement%20Documents/";
                    string customlibraryname = "Invitation For Prequalification";
                    string sharepointLibrary = defaultlibraryname + customlibraryname;
                    prequalificationNumber = prequalificationNumber.Replace('/', '_');
                    prequalificationNumber = prequalificationNumber.Replace(':', '_');
                    
                    string sharepointlink = sUrl + sharepointLibrary + "/" + prequalificationNumber + "/" + filename;
                    string fsavestatus = nav.FnInsertPrequalificatinDocuments(vendorNo, typauploadselect, filedescription, certificatenumber, dtofIssue, expiryDate, filename, prequalificationNumber, sharepointlink);
                    var splitanswer = fsavestatus.Split('*');
                    switch (splitanswer[0])
                    {
                        case "success":
                            return Json("success*" + succCounter, JsonRequestBehavior.AllowGet);
                        default:
                            return Json("danger*" + succCounter, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("sharepointError*", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult FnUploadBidResponseDocumentsc(string BidResponseNumber,HttpPostedFileBase browsedfile, string prodocType,
            string filedescription, string certificatenumber, string dateofissue, string expirydate)
        {
            try
            {
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                var nav = new NavConnection().ObjNav();
                string storedFilename = "";
                CultureInfo usCulture = new CultureInfo("en-ES");
                int errCounter = 0, succCounter = 0;
                DateTime dtofIssue = DateTime.Now;
                DateTime expiryDate = DateTime.Now;
                if (dateofissue == null && expirydate == null)
                {
                    dtofIssue = DateTime.Parse(dateofissue, usCulture.DateTimeFormat);
                    expiryDate = DateTime.Parse(expirydate, usCulture.DateTimeFormat);
                }


                if (browsedfile == null)
                {
                    errCounter++;
                    return Json("danger*browsedfilenull", JsonRequestBehavior.AllowGet);
                }

                if (vendorNo.Contains(":"))
                    vendorNo = vendorNo.Replace(":", "[58]");
                vendorNo = vendorNo.Replace("/", "[47]");

                if (filedescription.Contains("/"))
                    filedescription = filedescription.Replace("/", "_");

                if (prodocType.Contains("/"))
                    prodocType = prodocType.Replace("/", "_");


                string fileName0 = Path.GetFileName(browsedfile.FileName);
                string ext0 = _getFileextension(browsedfile);
                string savedF0 = vendorNo + "_" + prodocType + ext0;

                bool up2Sharepoint = _UploadTenderDocumentToSharepoint(vendorNo, browsedfile, prodocType);
                if (up2Sharepoint == true)
                {
                string filename = vendorNo + "_" + fileName0;
                    string sUrl = ConfigurationManager.AppSettings["S_URL"];
                    string defaultlibraryname = "Procurement%20Documents/";
                    string customlibraryname = "Tender Bid Reponses";
                    string sharepointLibrary = defaultlibraryname + customlibraryname;
                    BidResponseNumber = BidResponseNumber.Replace('/', '_');
                    BidResponseNumber = BidResponseNumber.Replace(':', '_');
                    //Sharepoint File Link
                    string sharepointlink = sUrl + sharepointLibrary + "/" + BidResponseNumber + "/" + filename;

                    string fsavestatus = nav.FnInsertBidReponseDocuments(vendorNo, prodocType, filedescription, certificatenumber, dtofIssue, expiryDate, filename, BidResponseNumber, sharepointlink);
                    var splitanswer = fsavestatus.Split('*');
                    switch (splitanswer[0])
                    {
                        case "success":
                            return Json("success*" + succCounter, JsonRequestBehavior.AllowGet);
                        default:
                            return Json("danger*" + succCounter, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("sharepointError*", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public string _getFileextension(HttpPostedFileBase filename)
        {
            return (Path.GetExtension(filename.FileName));
        }
        public JsonResult TenderDocChecker(string tendorNo)
        {
            try
            {
                var fileVirtualPath = (dynamic)null;
                if (tendorNo.Contains(":"))
                    tendorNo = tendorNo.Replace(":", "[58]");
                tendorNo = tendorNo.Replace("/", "[47]");
                fileVirtualPath = HostingEnvironment.MapPath(@"~/Downloads/Tenders/" + tendorNo + "/" + string.Format("{0}.pdf", tendorNo));
                string fileBytes = "";
                if (fileBytes != null)
                {
                    return Json("filefound", JsonRequestBehavior.AllowGet);
                }
                return Json("filenotfound", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("filenotfound", JsonRequestBehavior.AllowGet);
                // return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        protected bool _UploadSupplierDocumentToSharepoint(string vendorNumber, HttpPostedFileBase browsedFile, string filedescription)
        {
            bool fileuploadSuccess = false;
            string sUrl = ConfigurationManager.AppSettings["S_URL"];
            string tfilename = browsedFile.FileName;
            string defaultlibraryname = "Procurement%20Documents/";
            string customlibraryname = "Vendor Card";
            string sharepointLibrary = defaultlibraryname + customlibraryname;
            vendorNumber = vendorNumber.Replace('/', '_');
            vendorNumber = vendorNumber.Replace(':', '_');

            if (!string.IsNullOrWhiteSpace(sUrl) && !string.IsNullOrWhiteSpace(sharepointLibrary) && !string.IsNullOrWhiteSpace(tfilename))
            {
                string username = ConfigurationManager.AppSettings["S_USERNAME"];
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                bool bbConnected = Connect(sUrl, username, password, domainname);
                try
                {
                    if (bbConnected)
                    {
                        Uri uri = new Uri(sUrl);
                        string sSpSiteRelativeUrl = uri.AbsolutePath;
                        string uploadfilename = vendorNumber + "_" + browsedFile.FileName;
                        Stream uploadfileContent = browsedFile.InputStream;
                        var sDocName = UploadSupplierRegFile(uploadfileContent, uploadfilename, sSpSiteRelativeUrl, sharepointLibrary, vendorNumber);

                        //SharePoint Link to be added to Navison Card
                        string sharepointlink = sUrl + sharepointLibrary + "/" + vendorNumber + "/" + uploadfilename;

                        if (!string.IsNullOrWhiteSpace(sDocName))
                        {
                            //var nav = NavConnection.ReturnNav();
                            //string vendorNumberIdentity = vendorNumber;
                            //string status = nav.FnrfiResponsetLinks(vendorNumberIdentity, uploadfilename, sharepointlink); 
                            fileuploadSuccess = true;
                        }
                    }
                }
                catch (Exception)
                {
                    // throw;
                }
            }
            return fileuploadSuccess;
        }

        protected bool _UploadTenderDocumentToSharepoint(string vendorNumber, HttpPostedFileBase browsedFile, string filedescription)
        {
            bool fileuploadSuccess = false;
            string sUrl = ConfigurationManager.AppSettings["S_URL"];
            string tfilename = browsedFile.FileName;
            string defaultlibraryname = "Procurement%20Documents/";
            string customlibraryname = "Tender Bid Reponses";
            string sharepointLibrary = defaultlibraryname + customlibraryname;
            vendorNumber = vendorNumber.Replace('/', '_');
            vendorNumber = vendorNumber.Replace(':', '_');

            if (!string.IsNullOrWhiteSpace(sUrl) && !string.IsNullOrWhiteSpace(sharepointLibrary) && !string.IsNullOrWhiteSpace(tfilename))
            {
                string username = ConfigurationManager.AppSettings["S_USERNAME"];
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                bool bbConnected = Connect(sUrl, username, password, domainname);
                try
                {
                    if (bbConnected)
                    {
                        Uri uri = new Uri(sUrl);
                        string sSpSiteRelativeUrl = uri.AbsolutePath;
                        string uploadfilename = vendorNumber + "_" + browsedFile.FileName;
                        Stream uploadfileContent = browsedFile.InputStream;
                        var sDocName = UploadSupplierTenderFile(uploadfileContent, uploadfilename, sSpSiteRelativeUrl, sharepointLibrary, vendorNumber);

                        //SharePoint Link to be added to Navison Card
                        string sharepointlink = sUrl + sharepointLibrary + "/" + vendorNumber + "/" + uploadfilename;

                        if (!string.IsNullOrWhiteSpace(sDocName))
                        {
                            //var nav = NavConnection.ReturnNav();
                            //string vendorNumberIdentity = vendorNumber;
                            //string status = nav.FnrfiResponsetLinks(vendorNumberIdentity, uploadfilename, sharepointlink); 
                            fileuploadSuccess = true;
                        }
                    }
                }
                catch (Exception)
                {
                    // throw;
                }
            }
            return fileuploadSuccess;
        }
        protected bool _UploadSupplierPrequalificationsDocumentToSharepoint(string prequalificationNumber, HttpPostedFileBase browsedFile, string filedescription)
        {
            bool fileuploadSuccess = false;
            string sUrl = ConfigurationManager.AppSettings["S_URL"];
            string tfilename = browsedFile.FileName;
            string defaultlibraryname = "Procurement%20Documents/";
            string customlibraryname = "Invitation For Prequalification";
            string sharepointLibrary = defaultlibraryname + customlibraryname;
            prequalificationNumber = prequalificationNumber.Replace('/', '_');
            prequalificationNumber = prequalificationNumber.Replace(':', '_');

            if (!string.IsNullOrWhiteSpace(sUrl) && !string.IsNullOrWhiteSpace(sharepointLibrary) && !string.IsNullOrWhiteSpace(tfilename))
            {
                string username = ConfigurationManager.AppSettings["S_USERNAME"];
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                bool bbConnected = Connect(sUrl, username, password, domainname);
                try
                {
                    if (bbConnected)
                    {
                        Uri uri = new Uri(sUrl);
                        string sSpSiteRelativeUrl = uri.AbsolutePath;
                        string uploadfilename = prequalificationNumber + "_" + browsedFile.FileName;
                        Stream uploadfileContent = browsedFile.InputStream;
                        var sDocName = UploadSupplierPrequalificationFile(uploadfileContent, uploadfilename, sSpSiteRelativeUrl, sharepointLibrary, prequalificationNumber);
                        //SharePoint Link to be added to Navison Card
                        
                        string sharepointlink = sUrl + sharepointLibrary + "/" + prequalificationNumber + "/" + uploadfilename;

                        if (!string.IsNullOrWhiteSpace(sDocName))
                        {
                            //var nav = NavConnection.ReturnNav();
                            //string vendorNumberIdentity = vendorNumber;
                            //string status = nav.FnrfiResponsetLinks(vendorNumberIdentity, uploadfilename, sharepointlink); 
                            fileuploadSuccess = true;
                        }
                    }
                }
                catch (Exception)
                {
                    // throw;
                }
            }
            return fileuploadSuccess;
        }
        protected bool _UploadSupplierTenderDocumentToSharepoint(string prequalificationNumber, string browsedFile, string filedescription)
        {
            FileInfo fi = new FileInfo(browsedFile);
            bool fileuploadSuccess = false;
            string sUrl = ConfigurationManager.AppSettings["S_URL"];
            //string fileName0 = fi.Name;
            string ext0 = fi.Extension;
            string tfilename = fi.Name;
            string defaultlibraryname = "Procurement%20Documents/";
            string customlibraryname = "Tender Bid Reponses";
            string sharepointLibrary = defaultlibraryname + customlibraryname;
            prequalificationNumber = prequalificationNumber.Replace('/', '_');
            prequalificationNumber = prequalificationNumber.Replace(':', '_');

            if (!string.IsNullOrWhiteSpace(sUrl) && !string.IsNullOrWhiteSpace(sharepointLibrary) && !string.IsNullOrWhiteSpace(tfilename))
            {
                string username = ConfigurationManager.AppSettings["S_USERNAME"];
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                bool bbConnected = Connect(sUrl, username, password, domainname);
                try
                {
                    if (bbConnected)
                    {
                        Uri uri = new Uri(sUrl);
                        string sSpSiteRelativeUrl = uri.AbsolutePath;
                        var vendorNo = Convert.ToString(Session["vendorNo"]);
                        string uploadfilename = vendorNo + "_" + fi.Name;
                        //byte[] doc = Encoding.ASCII.GetBytes(browsedFile);
                        // byte[] byteArray = Convert.FromBase64String(browsedFile);
                        byte[] byteArray = Encoding.ASCII.GetBytes(browsedFile);
                        MemoryStream DocStream = new MemoryStream(byteArray);
                        Stream uploadfileContent = DocStream;
                        var sDocName = UploadSupplierTenderFile(uploadfileContent, uploadfilename, sSpSiteRelativeUrl, sharepointLibrary, prequalificationNumber);

                        //SharePoint Link to be added to Navison Card
                        string sharepointlink = sUrl + sharepointLibrary + "/" + prequalificationNumber + "/" + uploadfilename;

                        if (!string.IsNullOrWhiteSpace(sDocName))
                        {
                            //var nav = NavConnection.ReturnNav();
                            //string vendorNumberIdentity = vendorNumber;
                            //string status = nav.FnrfiResponsetLinks(vendorNumberIdentity, uploadfilename, sharepointlink); 
                            fileuploadSuccess = true;
                        }
                    }
                }
                catch (Exception)
                {
                    // throw;
                }
            }
            return fileuploadSuccess;
        }

        public JsonResult CheckTenderDocumentOnSharepoint(string rfiNumber)
        {
            using (ClientContext ctx = new ClientContext(ConfigurationManager.AppSettings["S_URL"]))
            {
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string account = ConfigurationManager.AppSettings["S_USERNAME"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                var secret = new SecureString();

                List<SharePointTModel> alldocuments = new List<SharePointTModel>();

                foreach (char c in password)
                {
                    secret.AppendChar(c);
                }
                // context.Credentials = CredentialCache.DefaultNetworkCredentials;
                ctx.Credentials = new NetworkCredential(account, secret, domainname);
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();
                List list = ctx.Web.Lists.GetByTitle("Procurement%20Documents");

                //Get Unique rfiNumber
                string uniquerfiNumber = rfiNumber;
                uniquerfiNumber = uniquerfiNumber.Replace('/', '_');
                uniquerfiNumber = uniquerfiNumber.Replace(':', '_');

                ctx.Load(list);
                ctx.Load(list.RootFolder);
                ctx.Load(list.RootFolder.Folders);
                ctx.Load(list.RootFolder.Files);
                ctx.ExecuteQuery();
                FolderCollection allFolders = list.RootFolder.Folders;
                foreach (Folder folder in allFolders)
                {
                    if (folder.Name == "Vendor Card")
                    {
                        ctx.Load(folder.Folders);
                        ctx.ExecuteQuery();
                        var uniquerfiNumberFolders = folder.Folders;
                        foreach (Folder rfinumber in uniquerfiNumberFolders)
                        {
                            if (rfinumber.Name == uniquerfiNumber)
                            {
                                ctx.Load(rfinumber.Files);
                                ctx.ExecuteQuery();

                                FileCollection rfinumberFiles = rfinumber.Files;
                                foreach (Microsoft.SharePoint.Client.File file in rfinumberFiles)
                                {
                                    ctx.ExecuteQuery();
                                    alldocuments.Add(new SharePointTModel { FileName = file.Name });

                                }
                            }
                        }
                    }
                }
                return Json(alldocuments, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PullRfIDocumentsfromSharePoint(string rfiNumber)
        {
            using (ClientContext ctx = new ClientContext(ConfigurationManager.AppSettings["S_URL"]))
            {
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string account = ConfigurationManager.AppSettings["S_USERNAME"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                var secret = new SecureString();

                List<SharePointTModel> alldocuments = new List<SharePointTModel>();

                foreach (char c in password)
                {
                    secret.AppendChar(c);
                }

                ctx.Credentials = new NetworkCredential(account, secret, domainname);
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();
                List list = ctx.Web.Lists.GetByTitle("Procurement%20Documents");

                //Get Unique rfiNumber
                string uniquerfiNumber = rfiNumber;
                uniquerfiNumber = uniquerfiNumber.Replace('/', '_');
                uniquerfiNumber = uniquerfiNumber.Replace(':', '_');

                ctx.Load(list);
                ctx.Load(list.RootFolder);
                ctx.Load(list.RootFolder.Folders);
                ctx.Load(list.RootFolder.Files);
                ctx.ExecuteQuery();

                FolderCollection allFolders = list.RootFolder.Folders;
                foreach (Folder folder in allFolders)
                {
                    if (folder.Name == "Procurement%20Documents")
                    {
                        ctx.Load(folder.Folders);
                        ctx.ExecuteQuery();
                        var uniquerfiNumberFolders = folder.Folders;

                        foreach (Folder folders in uniquerfiNumberFolders)
                        {
                            if (folders.Name == "Vendor Card")
                            {
                                ctx.Load(folders.Folders);
                                ctx.ExecuteQuery();
                                var uniqueittpnumberSubFolders = folders.Folders;

                                foreach (Folder rfinumber in uniqueittpnumberSubFolders)
                                {
                                    if (rfinumber.Name == uniquerfiNumber)
                                    {
                                        ctx.Load(rfinumber.Files);
                                        ctx.ExecuteQuery();

                                        FileCollection rfinumberFiles = rfinumber.Files;
                                        foreach (Microsoft.SharePoint.Client.File file in rfinumberFiles)
                                        {
                                            ctx.ExecuteQuery();
                                            alldocuments.Add(new SharePointTModel { FileName = file.Name });

                                        }
                                    }
                                }

                            }
                        }

                    }
                }
                return Json(alldocuments, JsonRequestBehavior.AllowGet);
            }
        }
        public string UploadSupplierPrequalificationFile(Stream fs, string sFileName, string sSpSiteRelativeUrl, string sLibraryName, string vendorNumber)
        {
            string sDocName = string.Empty;
            vendorNumber = vendorNumber.Replace('/', '_');
            vendorNumber = vendorNumber.Replace(':', '_');
            string parent_folderName = "Invitation For Prequalification";
            string subFolderName = vendorNumber;
            //+ "/"+ VendorNumber;
            string filelocation = sLibraryName + "/" + subFolderName;
            try
            {
                // if a folder doesn't exists, create it
                var listTitle = "Procurement Documents";
                if (!FolderExists(SPClientContext.Web, listTitle, parent_folderName + "/" + subFolderName))
                    CreateFolder(SPClientContext.Web, listTitle, parent_folderName + "/" + subFolderName);

                if (SPWeb != null)
                {

                    var sFileUrl = String.Format("{0}/{1}/{2}", sSpSiteRelativeUrl, filelocation, sFileName);
                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(SPClientContext, sFileUrl, fs, true);
                    SPClientContext.ExecuteQuery();
                    sDocName = sFileName;

                }


            }

            catch (Exception ex)
            {
                sDocName = string.Empty;
            }
            return sDocName;
        }

        public string UploadSupplierTenderFile(Stream fs, string sFileName, string sSpSiteRelativeUrl, string sLibraryName, string vendorNumber)
        {
            string sDocName = string.Empty;
            vendorNumber = vendorNumber.Replace('/', '_');
            vendorNumber = vendorNumber.Replace(':', '_');
            string parent_folderName = "Tender Bid Reponses";
            string subFolderName = vendorNumber;
            //+ "/"+ VendorNumber;
            string filelocation = sLibraryName + "/" + subFolderName;
            try
            {
                // if a folder doesn't exists, create it
                var listTitle = "Procurement Documents";
                if (!FolderExists(SPClientContext.Web, listTitle, parent_folderName + "/" + subFolderName))
                    CreateFolder(SPClientContext.Web, listTitle, parent_folderName + "/" + subFolderName);

                if (SPWeb != null)
                {

                    var sFileUrl = String.Format("{0}/{1}/{2}", sSpSiteRelativeUrl, filelocation, sFileName);
                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(SPClientContext, sFileUrl, fs, true);
                    SPClientContext.ExecuteQuery();
                    sDocName = sFileName;

                }


            }

            catch (Exception ex)
            {
                sDocName = string.Empty;
            }
            return sDocName;
        }

        public string UploadSupplierRegFile(Stream fs, string sFileName, string sSpSiteRelativeUrl, string sLibraryName, string vendorNumber)
        {
            string sDocName = string.Empty;
            vendorNumber = vendorNumber.Replace('/', '_');
            vendorNumber = vendorNumber.Replace(':', '_');
            string parent_folderName = "Vendor Card";
            string subFolderName = vendorNumber;
            //+ "/"+ VendorNumber;
            string filelocation = sLibraryName + "/" + subFolderName;
            try
            {
                // if a folder doesn't exists, create it
                var listTitle = "Procurement Documents";
                if (!FolderExists(SPClientContext.Web, listTitle, parent_folderName + "/" + subFolderName))
                    CreateFolder(SPClientContext.Web, listTitle, parent_folderName + "/" + subFolderName);

                if (SPWeb != null)
                {

                    var sFileUrl = String.Format("{0}/{1}/{2}", sSpSiteRelativeUrl, filelocation, sFileName);
                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(SPClientContext, sFileUrl, fs, true);
                    SPClientContext.ExecuteQuery();
                    sDocName = sFileName;

                }


            }

            catch (Exception ex)
            {
                sDocName = string.Empty;
            }
            return sDocName;
        }

        public static bool FolderExists(Web web, string listTitle, string folderUrl)
        {
            var list = web.Lists.GetByTitle(listTitle);
            var folders = list.GetItems(CamlQuery.CreateAllFoldersQuery());
            web.Context.Load(list.RootFolder);
            web.Context.Load(folders);
            web.Context.ExecuteQuery();
            var folderRelativeUrl = string.Format("{0}/{1}", list.RootFolder.ServerRelativeUrl, folderUrl);
            return Enumerable.Any(folders, folderItem => (string)folderItem["FileRef"] == folderRelativeUrl);
        }

        private static void CreateFolder(Web web, string listTitle, string folderName)
        {
            var list = web.Lists.GetByTitle(listTitle);
            var folderCreateInfo = new ListItemCreationInformation
            {
                UnderlyingObjectType = FileSystemObjectType.Folder,
                LeafName = folderName
            };
            var folderItem = list.AddItem(folderCreateInfo);
            folderItem.Update();
            web.Context.ExecuteQuery();
        }
        public bool Connect(string SPURL, string SPUserName, string SPPassWord, string SPDomainName)
        {

            bool bConnected = false;

            try
            {
                ////Sharepoint Onpremise
                SPClientContext = new ClientContext(SPURL);

                SPClientContext.Credentials = new NetworkCredential(SPUserName, SPPassWord, SPDomainName);

                SPClientContext.RequestTimeout = 1000000;

                SPWeb = SPClientContext.Web;

                SPClientContext.Load(SPWeb);

                SPClientContext.ExecuteQuery();

                bConnected = true;


                //Sharepoint Online
                //SPClientContext = new ClientContext(SPURL);
                //SPClientContext.RequestTimeout = 1000000;
                //var passWord = new SecureString();
                //foreach (char c in SPPassWord.ToCharArray()) passWord.AppendChar(c);
                //SPClientContext.Credentials = new SharePointOnlineCredentials(SPUserName, passWord);
                //SPWeb = SPClientContext.Web;
                //SPClientContext.Load(SPWeb);
                //SPClientContext.ExecuteQuery();


                bConnected = true;

            }

            catch (Exception ex)
            {

                bConnected = false;

                SPErrorMsg = ex.Message;
                Response.Write(ex.Message.ToString());

            }

            return bConnected;

        }
        public string UploadFile(Stream fs, string sFileName, string sSpSiteRelativeUrl, string sLibraryName, string rfidocnumber, string vendorNumber)
        {
            string sDocName = string.Empty;
            rfidocnumber = rfidocnumber.Replace('/', '_');
            rfidocnumber = rfidocnumber.Replace(':', '_');

            string parent_folderName = "Vendor Card";
            // string parent_folderName2 = "KeRRA/RFI Response Card/"+ rfidocnumber;

            string subFolderName = rfidocnumber;
            // string subFolderName2 = vendorNumber;

            string filelocation = sLibraryName + "/" + subFolderName;
            try
            {

                // if a folder doesn't exists, create it
                var listTitle = "Procurement Documents";
                if (!FolderExists(SPClientContext.Web, listTitle, parent_folderName + "/" + subFolderName))
                    CreateFolder(SPClientContext.Web, listTitle, parent_folderName + "/" + subFolderName);

                //Creating a folder inside a subfolder
                // if (!FolderExists(WsConfig.SPClientContext.Web, listTitle, parent_folderName2 + "/" + subFolderName2))
                // CreateFolder(WsConfig.SPClientContext.Web, listTitle, parent_folderName2 + "/" + subFolderName2);

                if (SPWeb != null)
                {
                    var sFileUrl = string.Format("{0}/{1}/{2}", sSpSiteRelativeUrl, filelocation, sFileName);
                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(SPClientContext, sFileUrl, fs, true);
                    SPClientContext.ExecuteQuery();
                    sDocName = sFileName;
                }
            }

            catch (Exception ex)
            {
                sDocName = string.Empty;
            }
            return sDocName;
        }

        [HttpGet]
        
        public ActionResult DownloadRegDocumentsfromSharepoint(string TenderNumber, String filenames)
        {
            var vendorNo = Convert.ToString(Session["vendorNo"]);
            //var sharepointUrl =
            //using (ClientContext ctx = new ClientContext(sharepointUrl))
            //{
              
               // var secret = new SecureString();
                //foreach (char c in password)
                //{
                //    secret.AppendChar(c);
                //}
                http://ihub/sites/Intranet//ERP DOCUMENTS/Procurement and Sourcing/R48/Invitation To Tender/

                try
                {

                    //SharePoint Credentials  
                    var sharepointUrl = ConfigurationManager.AppSettings["S_URL"];
                    var fileName = filenames;
 
                    String leaveNo = TenderNumber;
                    string password = ConfigurationManager.AppSettings["S_PWD"];
                    string account = ConfigurationManager.AppSettings["S_USERNAME"];
                    string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                    var filePath = sharepointUrl + "ERP%20DOCUMENTS/Procurement and Sourcing/R48/Invitation To Tender/"+ leaveNo + "/" + fileName;

                    var secret = new SecureString();
                    foreach (char c in password)
                    {
                        secret.AppendChar(c);
                    }

                    var onlineCredentials = new NetworkCredential(account, password, domainname);
                    ClientContext clientContext = new ClientContext(filePath);
                    var url = string.Format("{0}", filePath);
                    WebRequest request = WebRequest.Create(new Uri(url, UriKind.Absolute));
                    request.Credentials = onlineCredentials;
                    WebResponse response = request.GetResponse();
                    Stream fs = response.GetResponseStream() as Stream;
                    using (FileStream localfs = System.IO.File.OpenWrite(Server.MapPath("~/Downloads/") + "/" + Path.GetFileName(filePath)))
                    {
                        CopyStream(fs, localfs);

                    }
                    string filename = Path.GetFileName(filePath);
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
                    string aaa = Server.MapPath("~/Downloads/" + filename);
                    Response.TransmitFile(Server.MapPath("~/Downloads/" + filename));
                    Response.End();

                   // ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#downloadFileModal').modal('hide')", true);

                }
                catch (Exception ex)
                {
                   // documents.InnerHtml = "<div class='alert alert-danger'>'" + ex.Message + "'<a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a></div>";
                }
            
       
                return RedirectToAction("ViewSingleTender", "Home");

            }
        
    public static void CopyStream(Stream inputStream, Stream outputStream)
    {
        inputStream.CopyTo(outputStream, 4096);
    }



    [HttpPost]
        [AllowAnonymous]
        public ActionResult Download(SharePointTModel documentdetails)
        {
            var sharepointUrl = ConfigurationManager.AppSettings["S_URL"];
            using (ClientContext ctx = new ClientContext(sharepointUrl))
            {
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string account = ConfigurationManager.AppSettings["S_USERNAME"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                var secret = new SecureString();
                foreach (char c in password)
                {
                    secret.AppendChar(c);
                }

                try
                {
                    ctx.Credentials = new NetworkCredential(account, secret, domainname);
                    ctx.Load(ctx.Web);
                    ctx.ExecuteQuery();
                    List list = ctx.Web.Lists.GetByTitle("Procurement Documents");
                    FileCollection files = list.RootFolder.Folders.GetByUrl("/sites/KeRRA/Procurement Documents/Invitation To Tender/" + documentdetails.TenderNumber).Files;
                    ctx.Load(files);
                    ctx.ExecuteQuery();
                    foreach (Microsoft.SharePoint.Client.File file in files)
                    {
                        if (file.Name == documentdetails.FileName)
                        {
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + documentdetails.FileName);
                            Response.ContentType = "application/pdf";
                            Response.TransmitFile(documentdetails.FileName);
                            Response.End();
                        }
                        else
                        {
                            return Json("danger*", JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json("danger*", JsonRequestBehavior.AllowGet);
                }

            }
            return Json("success*", JsonRequestBehavior.AllowGet);
        }
        public JsonResult TestDownload(SharePointTModel documentdetails)
        {
            try
            {

                String FilePath = @"http://192.168.1.121/sites/Intranet/Procurement%20Documents/Invitation%20To%20Tender/" + documentdetails.TenderNumber + "/" + documentdetails.FileName;
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + documentdetails.FileName);
                response.WriteFile(FilePath);
                response.Flush();
                response.End();

            }
            catch (Exception ex)
            {
                return Json("danger*", JsonRequestBehavior.AllowGet);
            }
            return Json("success*", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DownloadTenderDocumentsfromSharepoint(SharePointTModel documentdetails)
        {
            //var sharepointUrl = ConfigurationManager.AppSettings["S_URL"];
            //using (ClientContext ctx = new ClientContext(sharepointUrl))
            //{
            //    string password = ConfigurationManager.AppSettings["S_PWD"];
            //    string account = ConfigurationManager.AppSettings["S_USERNAME"];
            //    string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
            //    var secret = new SecureString();
            //    foreach (char c in password)
            //    {
            //        secret.AppendChar(c);
            //    }

            //    try
            //    {
            //        ctx.Credentials = new NetworkCredential(account, secret, domainname);
            //        ctx.Load(ctx.Web);
            //        ctx.ExecuteQuery();
            //        String FilePath= "/sites/KeRRA/Procurement Documents/Invitations To Tender/" + documentdetails.TenderNumber + "/" + documentdetails.FileName;
            //        String FileRlativeUrlPath =Convert.ToString(ctx.Web.GetFileByServerRelativeUrl(FilePath));
            //        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            //        response.ClearContent();
            //        response.Clear();
            //        response.ContentType = "application/octet-stream";
            //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + documentdetails.FileName);
            //        response.WriteFile(FileRlativeUrlPath);
            //        response.Flush();
            //        response.End();
            //        List list = ctx.Web.Lists.GetByTitle("Procurement Documents");
            //        FileCollection files = list.RootFolder.Folders.GetByUrl("/sites/KeRRA/Procurement Documents/Invitations To Tender/" + documentdetails.TenderNumber).Files;
            //        ctx.Load(files);
            //        ctx.ExecuteQuery();
            //        foreach (Microsoft.SharePoint.Client.File file in files)
            //        {
            //            FileInformation fileinfo = Microsoft.SharePoint.Client.File.OpenBinaryDirect(ctx, file.ServerRelativeUrl);
            //            ctx.ExecuteQuery();
            //            using (FileStream filestream = new FileStream("E:/SharepointDemo/" + "\\" + file.Name, FileMode.Create))
            //            {
            //                fileinfo.Stream.CopyTo(filestream);
            //                return Json("success*", JsonRequestBehavior.AllowGet);
            //            }

            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        return Json("sharepointConnection*", JsonRequestBehavior.AllowGet);
            //    }
            //}
            //return Json("success*", JsonRequestBehavior.AllowGet);
            var vendorNo = Convert.ToString(Session["vendorNo"]);
            var sharepointUrl = ConfigurationManager.AppSettings["S_URL"];
            using (ClientContext ctx = new ClientContext(sharepointUrl))
            {
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string account = ConfigurationManager.AppSettings["S_USERNAME"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                var secret = new SecureString();
                foreach (char c in password)
                {
                    secret.AppendChar(c);
                }

                try
                {
                    ctx.Credentials = new NetworkCredential(account, secret, domainname);
                    ctx.Load(ctx.Web);
                    ctx.ExecuteQuery();
                    string DocumentLink = sharepointUrl + "ERP Documents/" + "Procurement and Sourcing/R48/Invitation To Tender/" + vendorNo + "/" + documentdetails.TenderNumber;



                    DownloadFile(DocumentLink, ctx.Credentials, "E:/SharepointDemo/" + "\\" + documentdetails.TenderNumber);
                   
                }
                catch (Exception ex)
                {
                    
                }
            }

            return View();
        }
        public static bool DownloadFile(string webUrl, ICredentials credentials, string fileRelativeUrl)
        {
            bool downloaded = false;
            using (var client = new WebClient())
            {
                try
                {
                    client.Headers.Add("Accept", "application/pdf");
                    client.Headers.Add("Content-Disposition", "attachment; filename=" + fileRelativeUrl);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/pdf; charset=utf-8");
                    client.Headers.Add("X-FORMS_BASED_AUTH_ACCEPTED", "f");
                    client.Headers.Add("User-Agent: Other");
                    client.Credentials = credentials;
                    client.DownloadFile(webUrl, fileRelativeUrl);
                    downloaded = true;
                }
                catch (Exception)
                {
                    downloaded = false;
                }
            }
            return downloaded;
        }
        public JsonResult DeleteRegDocfromSharepoint(string filename, int entryNo)
        {
            var vendorNo = Convert.ToString(Session["vendorNo"]);
            var sharepointUrl = ConfigurationManager.AppSettings["S_URL"];
            using (ClientContext ctx = new ClientContext(sharepointUrl))
            {
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string account = ConfigurationManager.AppSettings["S_USERNAME"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                var secret = new SecureString();
                foreach (char c in password)
                {
                    secret.AppendChar(c);
                }
                try
                {
                    ctx.Credentials = new NetworkCredential(account, secret, domainname);
                    ctx.Load(ctx.Web);
                    ctx.ExecuteQuery();

                    Uri uri = new Uri(sharepointUrl);
                    string sSpSiteRelativeUrl = uri.AbsolutePath;
                    string filePath = sSpSiteRelativeUrl + "Procurement Documents/Vendor Card/" + vendorNo + "/" + filename;
                    var file = ctx.Web.GetFileByServerRelativeUrl(filePath);
                    ctx.Load(file, f => f.Exists);
                    file.DeleteObject();
                    ctx.ExecuteQuery();
                    var nav = new NavConnection().ObjNav();
                    var deleteDoc = nav.FnDelDocument(vendorNo, entryNo);

                    if (!file.Exists)
                    {
                        return Json("filenotfound*", JsonRequestBehavior.AllowGet);
                    }
                    return Json("success*", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult DeleteBidRespDocfromSharepoint(string filename, int entryNo)
        {
            var vendorNo = Convert.ToString(Session["vendorNo"]);
            var sharepointUrl = ConfigurationManager.AppSettings["S_URL"];
            using (ClientContext ctx = new ClientContext(sharepointUrl))
            {
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string account = ConfigurationManager.AppSettings["S_USERNAME"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                var secret = new SecureString();
                foreach (char c in password)
                {
                    secret.AppendChar(c);
                }
                try
                {
                    ctx.Credentials = new NetworkCredential(account, secret, domainname);
                    ctx.Load(ctx.Web);
                    ctx.ExecuteQuery();

                    Uri uri = new Uri(sharepointUrl);
                    string sSpSiteRelativeUrl = uri.AbsolutePath;
                    string filePath = sSpSiteRelativeUrl + "Procurement Documents/Tender Bid Reponses/" + vendorNo + "/" + filename;
                    var file = ctx.Web.GetFileByServerRelativeUrl(filePath);
                    ctx.Load(file, f => f.Exists);
                    file.DeleteObject();
                    ctx.ExecuteQuery();
                    var nav = new NavConnection().ObjNav();
                    var deleteDoc = nav.FnDelBidRespDocument(vendorNo, entryNo);

                    if (!file.Exists)
                    {
                        return Json("filenotfound*", JsonRequestBehavior.AllowGet);
                    }
                    return Json("success*", JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("danger*" + ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
        }
        private static List<SharePointTModel> PopulateTenderDocumentsfromSpTable(string ittpnumber)
        {
            List<SharePointTModel> alldocuments = new List<SharePointTModel>();
            using (ClientContext ctx = new ClientContext(ConfigurationManager.AppSettings["S_URL"]))
            {
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string account = ConfigurationManager.AppSettings["S_USERNAME"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                var secret = new SecureString();

                var arraydocs = new List<string>();

                foreach (char c in password)
                {
                    secret.AppendChar(c);
                }
                ctx.Credentials = new NetworkCredential(account, secret, domainname);
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();
                List list = ctx.Web.Lists.GetByTitle("ERP Documents");
                //Get Unique IttNumber
                string uniqueittpnumber = ittpnumber;
                uniqueittpnumber = uniqueittpnumber.Replace('/', '_');
                uniqueittpnumber = uniqueittpnumber.Replace(':', '_');

                ctx.Load(list);
                ctx.Load(list.RootFolder);
                ctx.Load(list.RootFolder.Folders);
                ctx.Load(list.RootFolder.Files);
                ctx.ExecuteQuery();

                FolderCollection allFolders = list.RootFolder.Folders;
                List<string> allFiles = new List<string>();
                foreach (Folder folder in allFolders)
                {
                    if (folder.Name == "Procurement and Sourcing")
                    {
                        
                        ctx.Load(folder.Folders);
                        ctx.ExecuteQuery();
                        FolderCollection innerFolders = folder.Folders;
                        foreach (Folder folder1 in innerFolders)
                        {

                            if (folder1.Name == "R48")
                            {
                                ctx.Load(folder1.Folders);
                                ctx.ExecuteQuery();
                                FolderCollection inner2Folders = folder1.Folders;
                                foreach (Folder folder2 in inner2Folders)
                                {
                                    if (folder2.Name == "Invitation To Tender")
                                    {
                                        ctx.Load(folder2.Folders);
                                        ctx.ExecuteQuery();
                                        var uniqueittpnumberFolders = folder2.Folders;
                                        foreach (Folder noticefolder in uniqueittpnumberFolders)
                                        {
                                            if (noticefolder.Name == uniqueittpnumber)
                                            {
                                                ctx.Load(noticefolder.Files);
                                                ctx.ExecuteQuery();
                                                FileCollection ittnumberFiles = noticefolder.Files;
                                                foreach (Microsoft.SharePoint.Client.File file in ittnumberFiles)
                                                {
                                                    ctx.ExecuteQuery();
                                                    alldocuments.Add(new SharePointTModel { FileName = file.Name });

                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }


                        //var uniqueittpnumberFolders = folder.Folders;
                        //foreach (Folder noticefolder in uniqueittpnumberFolders)
                        //{
                        //    if (noticefolder.Name == uniqueittpnumber)
                        //    {
                        //        ctx.Load(noticefolder.Files);
                        //        ctx.ExecuteQuery();
                        //        FileCollection ittnumberFiles = noticefolder.Files;
                        //        foreach (Microsoft.SharePoint.Client.File file in ittnumberFiles)
                        //        {
                        //            ctx.ExecuteQuery();
                        //            alldocuments.Add(new SharePointTModel { FileName = file.Name });

                        //        }
                        //    }
                        //}

                    }
                }
                return alldocuments;
            }
        }
        private static List<SharePointTModel> AttachedPrequalificationDocuments(string prequalificationNo)
        {
            List<SharePointTModel> alldocuments = new List<SharePointTModel>();
            using (ClientContext ctx = new ClientContext(ConfigurationManager.AppSettings["S_URL"]))
            {
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string account = ConfigurationManager.AppSettings["S_USERNAME"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                var secret = new SecureString();

                var arraydocs = new List<string>();

                foreach (char c in password)
                {
                    secret.AppendChar(c);
                }
                ctx.Credentials = new NetworkCredential(account, secret, domainname);
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();
                List list = ctx.Web.Lists.GetByTitle("Procurement Documents");
                //Get Unique IttNumber
                string uniqueittpnumber = prequalificationNo;
                uniqueittpnumber = uniqueittpnumber.Replace('/', '_');
                uniqueittpnumber = uniqueittpnumber.Replace(':', '_');

                ctx.Load(list);
                ctx.Load(list.RootFolder);
                ctx.Load(list.RootFolder.Folders);
                ctx.Load(list.RootFolder.Files);
                ctx.ExecuteQuery();

                FolderCollection allFolders = list.RootFolder.Folders;
                List<string> allFiles = new List<string>();
                foreach (Folder folder in allFolders)
                {
                    if (folder.Name == "Invitation For Prequalification")
                    {

                        ctx.Load(folder.Folders);
                        ctx.ExecuteQuery();
                        var uniqueittpnumberFolders = folder.Folders;
                        foreach (Folder noticefolder in uniqueittpnumberFolders)
                        {
                            if (noticefolder.Name == uniqueittpnumber)
                            {
                                ctx.Load(noticefolder.Files);
                                ctx.ExecuteQuery();
                                FileCollection ittnumberFiles = noticefolder.Files;
                                foreach (Microsoft.SharePoint.Client.File file in ittnumberFiles)
                                {
                                    ctx.ExecuteQuery();
                                    alldocuments.Add(new SharePointTModel { FileName = file.Name });

                                }
                            }
                        }

                    }

                }
                return alldocuments;
            }
        }
        private static List<SharePointTModel> PopulateSupplierRegistrationDocuments(string ittpnumber)
        {
            List<SharePointTModel> alldocuments = new List<SharePointTModel>();
            using (ClientContext ctx = new ClientContext(ConfigurationManager.AppSettings["S_URL"]))
            {
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string account = ConfigurationManager.AppSettings["S_USERNAME"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                var secret = new SecureString();

                var arraydocs = new List<string>();

                foreach (char c in password)
                {
                    secret.AppendChar(c);
                }
                ctx.Credentials = new NetworkCredential(account, secret, domainname);
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();
                List list = ctx.Web.Lists.GetByTitle("Procurement Documents");
                //Get Unique IttNumber
                string uniqueittpnumber = ittpnumber;
                uniqueittpnumber = uniqueittpnumber.Replace('/', '_');
                uniqueittpnumber = uniqueittpnumber.Replace(':', '_');

                ctx.Load(list);
                ctx.Load(list.RootFolder);
                ctx.Load(list.RootFolder.Folders);
                ctx.Load(list.RootFolder.Files);
                ctx.ExecuteQuery();

                FolderCollection allFolders = list.RootFolder.Folders;
                List<string> allFiles = new List<string>();
                foreach (Folder folder in allFolders)
                {
                    if (folder.Name == "Vendor Card")
                    {
                        ctx.Load(folder.Folders);
                        ctx.ExecuteQuery();
                        var uniqueittpnumberFolders = folder.Folders;
                        foreach (Folder noticefolder in uniqueittpnumberFolders)
                        {
                            if (noticefolder.Name == uniqueittpnumber)
                            {
                                ctx.Load(noticefolder.Files);
                                ctx.ExecuteQuery();
                                FileCollection ittnumberFiles = noticefolder.Files;
                                foreach (Microsoft.SharePoint.Client.File file in ittnumberFiles)
                                {
                                    ctx.ExecuteQuery();
                                    alldocuments.Add(new SharePointTModel { FileName = file.Name });

                                }
                            }
                        }

                    }
                }
                return alldocuments;
            }
        }
        private static List<SharePointTModel> PopulatePrequalificationDocuments(string ittpnumber)
        {
            List<SharePointTModel> alldocuments = new List<SharePointTModel>();
            using (ClientContext ctx = new ClientContext(ConfigurationManager.AppSettings["S_URL"]))
            {
                string password = ConfigurationManager.AppSettings["S_PWD"];
                string account = ConfigurationManager.AppSettings["S_USERNAME"];
                string domainname = ConfigurationManager.AppSettings["S_DOMAIN"];
                var secret = new SecureString();

                var arraydocs = new List<string>();

                foreach (char c in password)
                {
                    secret.AppendChar(c);
                }
                ctx.Credentials = new NetworkCredential(account, secret, domainname);
                ctx.Load(ctx.Web);
                ctx.ExecuteQuery();
                List list = ctx.Web.Lists.GetByTitle("Procurement Documents");
                //Get Unique IttNumber
                string uniqueittpnumber = ittpnumber;
                uniqueittpnumber = uniqueittpnumber.Replace('/', '_');
                uniqueittpnumber = uniqueittpnumber.Replace(':', '_');

                ctx.Load(list);
                ctx.Load(list.RootFolder);
                ctx.Load(list.RootFolder.Folders);
                ctx.Load(list.RootFolder.Files);
                ctx.ExecuteQuery();

                FolderCollection allFolders = list.RootFolder.Folders;
                List<string> allFiles = new List<string>();
                foreach (Folder folder in allFolders)
                {
                    if (folder.Name == "Invitation For Prequalification")
                    {
                        ctx.Load(folder.Folders);
                        ctx.ExecuteQuery();
                        var uniqueittpnumberFolders = folder.Folders;
                        foreach (Folder noticefolder in uniqueittpnumberFolders)
                        {
                            if (noticefolder.Name == uniqueittpnumber)
                            {
                                ctx.Load(noticefolder.Files);
                                ctx.ExecuteQuery();
                                FileCollection ittnumberFiles = noticefolder.Files;
                                foreach (Microsoft.SharePoint.Client.File file in ittnumberFiles)
                                {
                                    ctx.ExecuteQuery();
                                    alldocuments.Add(new SharePointTModel { FileName = file.Name });

                                }
                            }
                        }

                    }
                }
                return alldocuments;
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var nav = NavConnection.ReturnNav();
                    var user = nav.PortalUsers.Where(x => x.Authentication_Email == model.Email && x.Password_Value == model.Password&&x.Record_Type== "Vendor").ToList();
                    var result = user.FirstOrDefault();
                    if (result != null)
                    {
                        foreach (var supplier in user)
                        {
                            string fname = supplier.Full_Name;
                            string username = supplier.User_Name;
                            string phoneNumber = supplier.Mobile_Phone_No;
                            Session["prequalified"] = supplier.State;
                            Session["email"] = supplier.Authentication_Email;
                            Session["password"] = supplier.Password_Value;
                            Session["name"] = supplier.Full_Name;
                            Session["email"] = supplier.Authentication_Email;
                            Session["userNo"] = supplier.Record_ID;
                            Session["vendorNo"] = supplier.Record_ID;
                            Session["username"] = username;
                            Session["fullname"] = fname;
                            Session["PhoneNumber"] = phoneNumber;
                      

                        }
                        //check if the contact is registered in the vendor table
                        var vendor = nav.eProVendorQT.ToList();
                        var vendorDetails = (from a in vendor where a.No == (string)Session["vendorNo"] select a).ToList();
                        if (result.State == "Enabled" &&result.Change_Password==true)
                        {
                            foreach (var vendordetail in vendorDetails)
                            {
                                Session["vendorNo"] = vendordetail.No;
                                Session["vendorName"] = vendordetail.Name;
                                Session["userNo"] = vendordetail.No;
                                Session["vatNumber"] = vendordetail.VAT_Registration_No;
                            }

                            

                            var query = nav.Vendors.Where(x => x.No == Convert.ToString(Session["vendorNo"]));
                            foreach (var responses in query)
                            {

                                if (responses.Profile_Complete == true)
                                {

                                    return RedirectToAction("Dashboard", "Home");
                                }
                                else if (responses.Profile_Complete == false)
                                {
                                    return RedirectToAction("SupplierRegistration", "Home");
                                }
                            }
                        }
                        else
                        {
                            return RedirectToAction("ChangePassword", "Home");
                        }
                        if (result.State != "Enabled")
                        {
                            TempData["error"] = "Your account is deactivated";

                        }
                    }
                    else
                    {
                        TempData["error"] = "The Email Address or Password provided is incorrect. Kindly try Again with the Correct Credentials";
                    }
                }
            }

            catch (Exception ex)
            {
                TempData["error"] = ex.Message;

            }
            return View(model);
        }


        public JsonResult SubmitRegistrationDetails()
        {
            try
            {
                var vendorNo = Convert.ToString(Session["vendorNo"]);
                var nav = new NavConnection().ObjNav();
                var status = nav.FnCompleteSupplierReg(vendorNo);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        return Json("success*", JsonRequestBehavior.AllowGet);

                    default:
                       return Json("danger*" + res[1], JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("danger", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Checkout()
        {
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.AppendHeader("Cache-Control", "no-store");
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }

}