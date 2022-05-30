using E_Procurement.Models;
using E_Procurement.Models.Contractor;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Procurement.Controllers
{
    public class ContractorController : Controller
    {
        // GET: Contractor
        public ActionResult HomePage()
        {

            List<ProjectTasksModel> list = new List<ProjectTasksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.JobTask.ToList();
                foreach (var projecttaks in query)
                {
                    ProjectTasksModel projecttask = new ProjectTasksModel();
                    projecttask.Job_No = projecttaks.Job_No;
                    projecttask.Description = projecttaks.Description;
                    projecttask.Department_Code = projecttaks.Department_Code;
                    projecttask.Directorate_Code = projecttaks.Directorate_Code;
                    projecttask.Division = projecttaks.Division;
                    projecttask.Commitments = Convert.ToString(projecttaks.Commitments);
                    projecttask.Start_Point_Km = Convert.ToString(projecttaks.Start_Point_Km);
                    projecttask.End_Point_Km = Convert.ToString(projecttaks.End_Point_Km);
                    projecttask.Start_Date = Convert.ToString(projecttaks.Start_Date);
                    projecttask.End_Date = Convert.ToString(projecttaks.End_Date);
                    projecttask.Funding_Source = projecttaks.Funding_Source;
                    projecttask.Procurement_Method = projecttaks.Procurement_Method;
                    projecttask.Surface_Types = projecttaks.Surface_Types;
                    projecttask.Road_Condition = projecttaks.Road_Condition;
                    projecttask.Completed_Length_Km = Convert.ToString(projecttaks.Completed_Length_Km);
                    projecttask.Roads_Category = projecttaks.Roads_Category;
                    projecttask.Fund_Type = projecttaks.Fund_Type;
                    projecttask.Execution_Method = projecttaks.Execution_Method;
                    projecttask.Region = projecttaks.Global_Dimension_1_Code;
                    projecttask.Constituency = projecttaks.Global_Dimension_2_Code;
                    projecttask.Execution_Method = projecttaks.Execution_Method;
                    list.Add(projecttask);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult CompletedContracts()
        {
            List<ContractsModel> list = new List<ContractsModel>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.PurchaseHeader.Where(x => x.Document_Type == "Blanket Order"&&x.Pay_to_Vendor_No== vendorNo && x.Contract_Status== "Signed").ToList();
                foreach (var allcontracts in query)
                {
                    ContractsModel contract = new ContractsModel();
                    contract.Document_Type = allcontracts.Document_Type;
                    contract.Buy_from_Vendor_No = allcontracts.Buy_from_Vendor_No;
                    contract.No = allcontracts.No;
                    contract.Pay_to_Name = allcontracts.Pay_to_Name;
                    contract.Pay_to_Vendor_No = allcontracts.Pay_to_Vendor_No;
                    contract.Pay_to_Name_2 = allcontracts.Pay_to_Name_2;
                    contract.Pay_to_Address = allcontracts.Pay_to_Address;
                    contract.Pay_to_Address_2 = allcontracts.Pay_to_Address_2;
                    contract.Pay_to_Contact = allcontracts.Pay_to_Contact;
                    contract.Your_Reference = allcontracts.Your_Reference;
                    contract.Order_Date = Convert.ToString(allcontracts.Order_Date);
                    contract.Posting_Date = Convert.ToString(allcontracts.Posting_Date);
                    contract.Location_Code = allcontracts.Location_Code;
                    contract.Region_Code = allcontracts.Region;
                    contract.Link_Name = allcontracts.Link_Name;
                    contract.Works_Length = Convert.ToString(allcontracts.Works_Length_Km);
                    contract.Invatition_for_supply = allcontracts.Invitation_For_Supply_No;
                    contract.Tender_Description = allcontracts.Tender_Name;
                    contract.Link_Name = allcontracts.Link_Name;
                    contract.Contract_Description = allcontracts.Contract_Description;
                    contract.Contract_End_Date = Convert.ToString(allcontracts.Contract_End_Date);
                    contract.Contract_Start_Date = Convert.ToString(allcontracts.Contract_Start_Date);
                    contract.Contract_Value = Convert.ToString(allcontracts.Approved_Requisition_Amount);
                    list.Add(contract);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);

        }
        public ActionResult ProjectsPendingMinutes()
        {
            List<ActiveContracts> list = new List<ActiveContracts>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.invitetoTenders.Where(x => x.Published == true && x.Document_Status == "Published").ToList();
                foreach (var tenders in query)
                {
                    ActiveContracts tender = new ActiveContracts();
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
        public ActionResult UpcomingMeetingsAppointments()
        {
            List<ActiveContracts> list = new List<ActiveContracts>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.invitetoTenders.Where(x => x.Published == true && x.Tender_Name != "").ToList();
                foreach (var tenders in query)
                {
                    ActiveContracts tender = new ActiveContracts();
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
        public ActionResult ActiveContracts()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.VendorActiveContracts = GetActiveContracts(vendorNo);
                model.CancelledContracts = GetCancelledActiveContracts(vendorNo);
                model.Vendors = GetVendors(vendorNo);
                return View(model);
            }
        }
        public ActionResult HealthandSafetyMeetings()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.VendorActiveContracts = GetActiveContracts(vendorNo);
                model.CancelledContracts = GetCancelledActiveContracts(vendorNo);
                model.Vendors = GetVendors(vendorNo);
                return View(model);
            }
        }
        public ActionResult AccidentandIncidents()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.VendorActiveContracts = GetActiveContracts(vendorNo);
                model.CancelledContracts = GetCancelledActiveContracts(vendorNo);
                model.Vendors = GetVendors(vendorNo);
                return View(model);
            }
        }
        public ActionResult EmergencyDrillLog()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.VendorActiveContracts = GetActiveContracts(vendorNo);
                model.CancelledContracts = GetCancelledActiveContracts(vendorNo);
                model.Vendors = GetVendors(vendorNo);
                return View(model);
            }
        }
        public ActionResult InductionBriefingLogs()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.VendorActiveContracts = GetActiveContracts(vendorNo);
                model.CancelledContracts = GetCancelledActiveContracts(vendorNo);
                model.Vendors = GetVendors(vendorNo);
                return View(model);
            }
        }
        public ActionResult WorkSiteInspections()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.VendorActiveContracts = GetActiveContracts(vendorNo);
                model.CancelledContracts = GetCancelledActiveContracts(vendorNo);
                model.Vendors = GetVendors(vendorNo);
                return View(model);
            }
        }
        public ActionResult WorkPermitsApplication()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.VendorActiveContracts = GetActiveContracts(vendorNo);
                model.CancelledContracts = GetCancelledActiveContracts(vendorNo);
                model.Vendors = GetVendors(vendorNo);
                return View(model);
            }
        }
        private static List<ContractsModel> GetActiveContracts(string vendorNo)
        {
                List<ContractsModel> list = new List<ContractsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.PurchaseHeader.Where(x => x.Document_Type == "Blanket Order" && x.Pay_to_Vendor_No== vendorNo &&x.Contract_Status== "Signed").ToList();
                    foreach (var allcontracts in query)
                    {
                        ContractsModel contract = new ContractsModel();
                        contract.Document_Type = allcontracts.Document_Type;
                        contract.Buy_from_Vendor_No = allcontracts.Buy_from_Vendor_No;
                        contract.No = allcontracts.No;
                        contract.Pay_to_Name = allcontracts.Pay_to_Name;
                        contract.Pay_to_Vendor_No = allcontracts.Pay_to_Vendor_No;
                        contract.Pay_to_Name_2 = allcontracts.Pay_to_Name_2;
                        contract.Pay_to_Address = allcontracts.Pay_to_Address;
                        contract.Pay_to_Address_2 = allcontracts.Pay_to_Address_2;
                        contract.Pay_to_Contact = allcontracts.Pay_to_Contact;
                        contract.Your_Reference = allcontracts.Your_Reference;
                        contract.Order_Date = Convert.ToString(allcontracts.Order_Date);
                        contract.Posting_Date = Convert.ToString(allcontracts.Posting_Date);
                        contract.Location_Code = allcontracts.Location_Code;
                        contract.Region_Code = allcontracts.Region;
                        contract.Link_Name = allcontracts.Link_Name;
                        contract.Works_Length = Convert.ToString(allcontracts.Works_Length_Km);
                        contract.Invatition_for_supply = allcontracts.Invitation_For_Supply_No;
                        contract.Tender_Description = allcontracts.Tender_Name;
                        contract.Link_Name = allcontracts.Link_Name;
                        contract.Contract_Description = allcontracts.Contract_Description;
                        contract.Contract_End_Date = Convert.ToString(allcontracts.Contract_End_Date);
                        contract.Contract_Start_Date = Convert.ToString(allcontracts.Contract_Start_Date);
                        contract.Contract_Value = Convert.ToString(allcontracts.Approved_Requisition_Amount);
                        list.Add(contract);
                }


            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<ContractsModel> GetCancelledActiveContracts(string vendorNo)
        {
            List<ContractsModel> list = new List<ContractsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.PurchaseHeader.Where(x => x.Document_Type == "Blanket Order" && x.Pay_to_Vendor_No == vendorNo && x.Contract_Status == "Cancelled").ToList();
                foreach (var allcontracts in query)
                {
                    ContractsModel contract = new ContractsModel();
                    contract.Document_Type = allcontracts.Document_Type;
                    contract.Buy_from_Vendor_No = allcontracts.Buy_from_Vendor_No;
                    contract.No = allcontracts.No;
                    contract.Pay_to_Name = allcontracts.Pay_to_Name;
                    contract.Pay_to_Vendor_No = allcontracts.Pay_to_Vendor_No;
                    contract.Pay_to_Name_2 = allcontracts.Pay_to_Name_2;
                    contract.Pay_to_Address = allcontracts.Pay_to_Address;
                    contract.Pay_to_Address_2 = allcontracts.Pay_to_Address_2;
                    contract.Pay_to_Contact = allcontracts.Pay_to_Contact;
                    contract.Your_Reference = allcontracts.Your_Reference;
                    contract.Order_Date = Convert.ToString(allcontracts.Order_Date);
                    contract.Posting_Date = Convert.ToString(allcontracts.Posting_Date);
                    contract.Location_Code = allcontracts.Location_Code;
                    contract.Region_Code = allcontracts.Region;
                    contract.Link_Name = allcontracts.Link_Name;
                    contract.Works_Length = Convert.ToString(allcontracts.Works_Length_Km);
                    contract.Invatition_for_supply = allcontracts.Invitation_For_Supply_No;
                    contract.Tender_Description = allcontracts.Tender_Name;
                    contract.Link_Name = allcontracts.Link_Name;
                    contract.Contract_Description = allcontracts.Contract_Description;
                    contract.Contract_End_Date = Convert.ToString(allcontracts.Contract_End_Date);
                    contract.Contract_Start_Date = Convert.ToString(allcontracts.Contract_Start_Date);
                    contract.Contract_Value = Convert.ToString(allcontracts.Approved_Requisition_Amount);
                    list.Add(contract);
                }


            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        public ActionResult NewMeasurementsSheets()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Response = GetVendors(vendorNo);
              

                return View(model);
            }
        }
        public JsonResult GetWEPKeyProfessionalStaff(string ExecutionPlanNumber)
        {

            List<WEPContractorTeamModel> list = new List<WEPContractorTeamModel>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.WEPContractorTeam.Where(x => x.Document_No == ExecutionPlanNumber && x.Contractor_No == vendorNo).ToList();
                foreach (var orders in query)
                {
                    WEPContractorTeamModel order = new WEPContractorTeamModel();
                    order.Document_No = orders.Document_No;
                    order.Contractor_No = Convert.ToString(orders.Contractor_No);
                    order.Name = orders.Name;
                    order.Address = orders.Address;
                    order.Address_2 = orders.Address_2;
                    order.City = orders.City;
                    order.Post_Code = orders.Post_Code;
                    order.Country_Region_Code = orders.Country_Region_Code;
                    order.Role_Code = orders.Role_Code;
                    order.Designation = orders.Designation;
                    order.Effective_Date = Convert.ToString(orders.Effective_Date);
                    order.Expiry_Date = Convert.ToString(orders.Expiry_Date);
                    order.Staff_Category = orders.Staff_Category;
                    order.Contractor_Staff_No = orders.Contractor_Staff_No;
                    list.Add(order);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWEPEquipmentsRegisterDetails(string ExecutionPlanNumber)
        {

            List<WEPContractorEquipment> list = new List<WEPContractorEquipment>();
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.WEPContractorEquipment.Where(x => x.Document_No == ExecutionPlanNumber && x.Contractor_No == vendorNo).ToList();
                foreach (var projects in query)
                {
                    WEPContractorEquipment project = new WEPContractorEquipment();
                    project.Document_No = projects.Document_No;
                    project.Document_Type = projects.Document_Type;
                    project.Contractor_No = Convert.ToString(projects.Contractor_No);
                    project.Equipment_No = Convert.ToString(projects.Equipment_No);
                    project.Equipment_Type_Code = Convert.ToString(projects.Equipment_Type_Code);
                    project.Description = projects.Description;
                    project.Ownership_Type = Convert.ToString(projects.Ownership_Type);
                    project.Equipment_Serial_No = projects.Equipment_Serial_No;
                    project.Equipment_Usability_Code = projects.Equipment_Usability_Code;
                    project.Years_of_Previous_Use = Convert.ToString(projects.Years_of_Previous_Use);
                    project.Equipment_Condition_Code = projects.Equipment_Condition_Code;
                    list.Add(project);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWEPProjectScheduleDetails(string ExecutionPlanNumber)
        {

            List<WEPExecutionLinesModel> list = new List<WEPExecutionLinesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.WEPExecutionSchedule.Where(x => x.Document_No == ExecutionPlanNumber).ToList();
                foreach (var projects in query)
                {
                    WEPExecutionLinesModel project = new WEPExecutionLinesModel();
                    project.Document_No = projects.Document_No;
                    project.Job_No = projects.Job_No;
                    project.Job_Task_No = Convert.ToString(projects.Job_Task_No);
                    project.Scheduled_End_Date = Convert.ToString(projects.Scheduled_End_Date);
                    project.Scheduled_Start_Date = Convert.ToString(projects.Scheduled_Start_Date);
                    project.Description = projects.Description;
                    project.Budget_Total_Cost = Convert.ToString(projects.Budget_Total_Cost);
                    project.Job_Task_Type = projects.Job_Task_Type;
                    list.Add(project);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApprovedDailyWorkRecords()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ActiveContracts> list = new List<ActiveContracts>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Tender_Name != "").ToList();
                    foreach (var tenders in query)
                    {
                        ActiveContracts tender = new ActiveContracts();
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
        public ActionResult PendingEngineerDailyWorks()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ActiveContracts> list = new List<ActiveContracts>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Tender_Name != "").ToList();
                    foreach (var tenders in query)
                    {
                        ActiveContracts tender = new ActiveContracts();
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
        public ActionResult DraftDailyWorksRecords()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ActiveContracts> list = new List<ActiveContracts>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Tender_Name != "").ToList();
                    foreach (var tenders in query)
                    {
                        ActiveContracts tender = new ActiveContracts();
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
        public ActionResult ApprovedExecutionsPlans()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<WorkExecutionPlanModel> list = new List<WorkExecutionPlanModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.WorkExecutionPlan.Where(x => x.Contractor_No == vendorNo &&x.Status== "Released").ToList();
                    foreach (var roadworks in query)
                    {
                        WorkExecutionPlanModel roadwork = new WorkExecutionPlanModel();
                        roadwork.Document_No = roadworks.Document_No;
                        roadwork.Commencement_Order_ID = roadworks.Commencement_Order_ID;
                        roadwork.Description = roadworks.Description;
                        roadwork.Project_ID = roadworks.Project_ID;
                        roadwork.Region_ID = roadworks.Region_ID;
                        roadwork.Document_Date = Convert.ToString(roadworks.Document_Date);
                        roadwork.Purchase_Contract_ID = roadworks.Purchase_Contract_ID;
                        roadwork.Contractor_Name = roadworks.Contractor_Name;
                        roadwork.Project_Name = roadworks.Project_Name;
                        roadwork.Road_Code = Convert.ToString(roadworks.Road_Code);
                        roadwork.Road_Section_No = Convert.ToString(roadworks.Road_Section_No);
                        roadwork.Document_Date = Convert.ToString(roadworks.Document_Date);
                        roadwork.Project_Start_Date = Convert.ToString(roadworks.Project_Start_Date);
                        roadwork.Project_End_Date = Convert.ToString(roadworks.Project_End_Date);
                        roadwork.Status = Convert.ToString(roadworks.Status);
                        list.Add(roadwork);
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult PendingExecutionsPlans()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<WorkExecutionPlanModel> list = new List<WorkExecutionPlanModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.WorkExecutionPlan.Where(x => x.Contractor_No == vendorNo && x.Status == "Released").ToList();
                    foreach (var roadworks in query)
                    {
                        WorkExecutionPlanModel roadwork = new WorkExecutionPlanModel();
                        roadwork.Document_No = roadworks.Document_No;
                        roadwork.Commencement_Order_ID = roadworks.Commencement_Order_ID;
                        roadwork.Description = roadworks.Description;
                        roadwork.Project_ID = roadworks.Project_ID;
                        roadwork.Region_ID = roadworks.Region_ID;
                        roadwork.Document_Date = Convert.ToString(roadworks.Document_Date);
                        roadwork.Purchase_Contract_ID = roadworks.Purchase_Contract_ID;
                        roadwork.Contractor_Name = roadworks.Contractor_Name;
                        roadwork.Project_Name = roadworks.Project_Name;
                        roadwork.Road_Code = Convert.ToString(roadworks.Road_Code);
                        roadwork.Road_Section_No = Convert.ToString(roadworks.Road_Section_No);
                        roadwork.Document_Date = Convert.ToString(roadworks.Document_Date);
                        roadwork.Project_Start_Date = Convert.ToString(roadworks.Project_Start_Date);
                        roadwork.Project_End_Date = Convert.ToString(roadworks.Project_End_Date);
                        roadwork.Status = Convert.ToString(roadworks.Status);
                        list.Add(roadwork);


                    }

                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult ViewUpcomingMeeting()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                return View();
            }

        }

        public ActionResult WorkRegionList()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<Models.Contractor.CountriesModel> list = new List<Models.Contractor.CountriesModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.Regions.ToList();
                    foreach (var regions in query)
                    {
                        Models.Contractor.CountriesModel region = new Models.Contractor.CountriesModel();
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
        }
        public ActionResult ContractorEquipmentTypeLists()
        {
            List<ContractorEquipmentTypeModel> list = new List<ContractorEquipmentTypeModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.WEPWorksEquipmentType.ToList();
                foreach (var equipmentstypes in query)
                {
                    ContractorEquipmentTypeModel type = new ContractorEquipmentTypeModel();
                    type.Code = equipmentstypes.Code;
                    type.Description = equipmentstypes.Description;
                    list.Add(type);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(list);
        }
        public JsonResult RegisterWorkExecutionPlan(string OrderNumber)
        {

            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                DateTime startdate, enddate;
                var nav = new NavConnection().ObjNav();
                var status = nav.FnCreatWorkExecutionScheduleDetails(vendorNo, OrderNumber);
                var res = status.Split('*');
                switch (res[0])
                {
                    case "success":
                        Session["WorkExecutionPlanNumber"] = nav.FngetWorkExecutionPlanNumber(vendorNo, OrderNumber);
                        Session["WorkExecutionPlanProjectNumber"] = nav.FngetWorkExecutionPlanProjectNumber(vendorNo, OrderNumber);
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
        public JsonResult RegisterWorkExecutionPlanScheduleDetails(WEPExecutionLinesModel weplines)
        {

            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                DateTime startdate, enddate;
                CultureInfo usCulture = new CultureInfo("es-ES");
                startdate = DateTime.Parse(weplines.Scheduled_Start_Date, usCulture.DateTimeFormat);
                enddate = DateTime.Parse(weplines.Scheduled_End_Date, usCulture.DateTimeFormat);
                var nav = new NavConnection().ObjNav();
                var status = nav.FnSubmitWorkExecutionPlanScheduleDetails(weplines.Document_No, weplines.Job_No, weplines.Job_Task_No, startdate,enddate);
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
        public JsonResult RegisterWorkExecutionPlanContractorTeamDetails(WEPContractorTeamModel team)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                DateTime startdate;
                CultureInfo usCulture = new CultureInfo("es-ES");
                startdate = DateTime.Parse(team.Effective_Date, usCulture.DateTimeFormat);
                var nav = new NavConnection().ObjNav();
                var status = "";/*nav.FnSubmitWorkExecutionPlanContractorTeam(team.Document_No,vendorNo, team.Contractor_Staff_No, team.Name, team.Emailaddress, team.Address, team.Address_2, team.City, team.Country_Region_Code, team.Post_Code, team.Telephone, team.Designation,Convert.ToInt32(team.Staff_Category), startdate);*/
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
        public ActionResult GoPrevious1()
        {
            int step;
            try
            {
                step = Convert.ToInt32(Request.QueryString["step"].Trim());
            }
            catch (Exception ex)
            {
                step = 0;
            }
            int step1 = 1;
            return RedirectToAction("NewDailyWorkRecords", "Contractor", new { step = step1 });
        }
        public ActionResult GoPrevious2()
        {
            int step;
            try
            {
                step = Convert.ToInt32(Request.QueryString["step"].Trim());
            }
            catch (Exception ex)
            {
                step = 0;
            }
            int step1 = 2;
            return RedirectToAction("NewDailyWorkRecords", "Contractor", new { step = step1 });
        }
        public ActionResult GoPrevious3()
        {
            int step;
            try
            {
                step = Convert.ToInt32(Request.QueryString["step"].Trim());
            }
            catch (Exception ex)
            {
                step = 0;
            }
            int step1 = 3;
            return RedirectToAction("NewDailyWorkRecords", "Contractor", new { step = step1 });
        }
        public ActionResult GoNext()
        {
            int step;
            try
            {
                step = Convert.ToInt32(Request.QueryString["step"].Trim());
            }
            catch (Exception ex)
            {
                step = 0;
            }
            int step1 = step + 1;
            return RedirectToAction("NewDailyWorkRecords", "Contractor", new { step= step1 });
        }
        public ActionResult GoNextStep2()
        {
            int step;
            try
            {
                step = Convert.ToInt32(Request.QueryString["step"].Trim());
            }
            catch (Exception ex)
            {
                step = 0;
            }
            int step1 = 2;
            return RedirectToAction("NewDailyWorkRecords", "Contractor", new { step = step1 });
        }
        public ActionResult GoNextStep3()
        {
            int step;
            try
            {
                step = Convert.ToInt32(Request.QueryString["step"].Trim());
            }
            catch (Exception ex)
            {
                step = 0;
            }
            int step1 = 3;
            return RedirectToAction("NewDailyWorkRecords", "Contractor", new { step = step1 });
        }
        public ActionResult GoNextStep4()
        {
            int step;
            try
            {
                step = Convert.ToInt32(Request.QueryString["step"].Trim());
            }
            catch (Exception ex)
            {
                step = 0;
            }
            int step1 = 4;
            return RedirectToAction("NewDailyWorkRecords", "Contractor", new { step = step1 });
        }
        public ActionResult GoNextStep5()
        {
            int step;
            try
            {
                step = Convert.ToInt32(Request.QueryString["step"].Trim());
            }
            catch (Exception ex)
            {
                step = 0;
            }
            int step1 = 5;
            return RedirectToAction("NewDailyWorkRecords", "Contractor", new { step = step1 });
        }
        public ActionResult NavigationMenu()
        {
            return View();
        }
        public ActionResult NavigationFooter()
        {
            return View();
        }
        public ActionResult NewProjectMeetingRegister()
        {
            return View();
        }
        public ActionResult NewDailyWorkRecords()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RegisterWorkExecutionPlanEquipmentsDetails(WEPContractorEquipment equipments)
        {
            try
            {
                var vendorNo = Session["vendorNo"].ToString();
                var nav = new NavConnection().ObjNav();
                var status = "";
                    //nav.FnSubmitWorkExecutionPlanEquipments(equipments.Document_No, vendorNo, equipments.Equipment_No, equipments.Equipment_Type_Code, equipments.Description, Convert.ToInt32(equipments.Ownership_Type), 
                    //equipments.Equipment_Serial_No, Convert.ToInt32(equipments.Equipment_Usability_Code), Convert.ToInt32(equipments.Years_of_Previous_Use), Convert.ToInt32(equipments.Equipment_Condition_Code),equipments.Equipment_Availability);
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
        public ActionResult ContractorStaffCountryList()
        {
            List<Models.Contractor.CountriesModel> list = new List<Models.Contractor.CountriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.Countries.ToList();
                foreach (var countries in query)
                {
                    Models.Contractor.CountriesModel country = new Models.Contractor.CountriesModel();
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
        public ActionResult ContractorPostCodesList()
        {
            List<Models.Contractor.DropdownListsModel> postacode = new List<Models.Contractor.DropdownListsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.postcodes.ToList();
                foreach (var postacodes in query)
                {
                    Models.Contractor.DropdownListsModel postcodes = new Models.Contractor.DropdownListsModel();
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
        public ActionResult ProjectStaffRoleCodeList()
        {
            List<Models.Contractor.DropdownListsModel> postacode = new List<Models.Contractor.DropdownListsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ProjectStaffRoleCode.ToList();
                foreach (var rolecodes in query)
                {
                    Models.Contractor.DropdownListsModel role = new Models.Contractor.DropdownListsModel();
                    role.Code = rolecodes.Staff_Role_Code;
                    role.Description = rolecodes.Designation;
                    postacode.Add(role);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(postacode);
        }
        public ActionResult ProjectTasksList()
        {
            List<ProjectTasksModel> list = new List<ProjectTasksModel>();
            try
            {
                var ProjectNumber = Session["WorkExecutionPlanProjectNumber"].ToString();
                var nav = NavConnection.ReturnNav();
                var query = nav.JobTask.Where(X => X.Job_No == ProjectNumber).ToList();
                foreach (var tasks in query)
                {
                    ProjectTasksModel task = new ProjectTasksModel();
                    task.Job_Task_No = tasks.Job_Task_No;
                    task.Description = tasks.Description;
                    list.Add(task);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return View(list);
        }
        public ActionResult FileNewWorkExecutionPlan()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var ExecutionPlanNumber = Session["WorkExecutionPlanNumber"].ToString();
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.GeneralDetails = GetProjectExecutionPlanDetails(ExecutionPlanNumber, vendorNo);
                model.WEPTeam = GetWEPContractorTeamDetails(ExecutionPlanNumber, vendorNo);
                model.WEPLines = GetWEPExecutionPlanLinesDetails(ExecutionPlanNumber, vendorNo);
                model.PlanningLines = GetJobPlanningLinesDetails(ExecutionPlanNumber);
                model.Equipment = GetWEPExecutionPlanEquipments(ExecutionPlanNumber, vendorNo);
                model.Vendors = GetVendors(vendorNo);
                return View(model);
            }

        }
        public ActionResult ViewWorkExecutionPlan(string ExecutionPlanNumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.GeneralDetails = GetProjectExecutionPlanDetails(ExecutionPlanNumber, vendorNo);
                model.WEPTeam = GetWEPContractorTeamDetails(ExecutionPlanNumber, vendorNo);
                model.WEPLines = GetWEPExecutionPlanLinesDetails(ExecutionPlanNumber, vendorNo);
                model.PlanningLines = GetJobPlanningLinesDetails(ExecutionPlanNumber);
                model.Equipment = GetWEPExecutionPlanEquipments(ExecutionPlanNumber, vendorNo);
                model.Vendors = GetVendors(vendorNo);
                return View(model);
            }

        }
        public ActionResult ViewOrdertoCommence(string OrderNumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.GeneralDetails = GetOrdertoCommenceDetails(OrderNumber);
                model.InternalTeam = GetProjectInternalTeamDetails(OrderNumber);
                model.PlannedMeetings = GetPCOPlannedMeeting(OrderNumber);
                model.RequiredDocuments = GetPCORequiredDocuments(OrderNumber);
                return View(model);
            }

        }
        private static List<WEPExecutionLinesModel> GetWEPExecutionPlanLinesDetails(string ExecutionPlanNumber,string vendorNo)
        {

            List<WEPExecutionLinesModel> list = new List<WEPExecutionLinesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.WEPExecutionSchedule.Where(x => x.Document_No == ExecutionPlanNumber).ToList();
                foreach (var projects in query)
                {
                    WEPExecutionLinesModel project = new WEPExecutionLinesModel();
                    project.Document_No = projects.Document_No;
                    project.Job_No = projects.Job_No;
                    project.Job_Task_No = Convert.ToString(projects.Job_Task_No);
                    project.Scheduled_End_Date = Convert.ToString(projects.Scheduled_End_Date);
                    project.Scheduled_Start_Date = Convert.ToString(projects.Scheduled_Start_Date);
                    project.Description = projects.Description;
                    project.Budget_Total_Cost = Convert.ToString(projects.Budget_Total_Cost);
                    project.Job_Task_Type = projects.Job_Task_Type;
                    list.Add(project);
                }


            }
            catch (Exception e)
            {

                throw;
            }

            return list;

        }
        private static List<WEPContractorEquipment> GetWEPExecutionPlanEquipments(string ExecutionPlanNumber,string vendorNo)
        {

            List<WEPContractorEquipment> list = new List<WEPContractorEquipment>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.WEPContractorEquipment.Where(x => x.Document_No == ExecutionPlanNumber&&x.Contractor_No== vendorNo).ToList();
                foreach (var projects in query)
                {
                    WEPContractorEquipment project = new WEPContractorEquipment();
                    project.Document_No = projects.Document_No;
                    project.Document_Type = projects.Document_Type;
                    project.Contractor_No = Convert.ToString(projects.Contractor_No);
                    project.Equipment_No = Convert.ToString(projects.Equipment_No);
                    project.Equipment_Type_Code = Convert.ToString(projects.Equipment_Type_Code);
                    project.Description = projects.Description;
                    project.Ownership_Type = Convert.ToString(projects.Ownership_Type);
                    project.Equipment_Serial_No = projects.Equipment_Serial_No;
                    project.Equipment_Usability_Code = projects.Equipment_Usability_Code;
                    project.Years_of_Previous_Use =Convert.ToString(projects.Years_of_Previous_Use);
                    project.Equipment_Condition_Code = projects.Equipment_Condition_Code;
                    list.Add(project);
                }
              }
            catch (Exception ex)
            {

                throw;
            }

            return list;

        }
        private static List<ProjectWorksModel> GetProjectDetails(string vendorNo,string ProjectNumber)
        {

            List<ProjectWorksModel> list = new List<ProjectWorksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.jobs.Where(x => x.No == ProjectNumber &&x.Contractor_No== vendorNo).ToList();
                foreach (var projects in query)
                {
                    ProjectWorksModel project = new ProjectWorksModel();
                    
                    project.Project_No = projects.Project_No;
                    project.No = projects.No;
                    project.Project_Start_Date = Convert.ToString(projects.Starting_Date);
                    project.Project_End_Date = Convert.ToString(projects.Ending_Date);
                    project.Description = projects.Description;
                    project.Person_Responsible = projects.Person_Responsible;
                    project.Project_Manager = projects.Person_Responsible;
                    project.Contractor_No = projects.Contractor_No;
                    project.Contractor_Name = projects.Contractor_Name;
                    project.Road_Section_No = projects.Road_Section_No;
                    project.Status = projects.Status;
                    project.IFS_Code = projects.IFS_Code;
                    project.Road_Class_ID = projects.Road_Class_ID;
                    project.Road_Code = projects.Road_Code;
                    project.Road_Section_No = projects.Road_Section_No;
                    project.Funding_Source = projects.Funding_Source;
                    project.Section_Name = projects.Section_Name;
                    project.Section_Name = projects.Section_Name;
                    project.Total_Road_Section_Length_KM =Convert.ToString( projects.Total_Road_Section_Length_KM);
                    project.Constituency_ID = projects.Constituency_ID;
                    project.Creation_Date = Convert.ToString(projects.Creation_Date);
                    project.Contract_Start_Date = Convert.ToString(projects.Contract_Start_Date);
                    project.Contract_End_Date = Convert.ToString(projects.Contract_End_Date);
                    project.Directorate_Code = projects.Directorate_Code;
                    list.Add(project);

                }


            }
            catch (Exception e)
            {

                throw;
            }

            return list;

        }
        private static List<ProjectWorksModel> GetCompletedProjecttDetails(string vendorNo, string ProjectNumber)
        {

            List<ProjectWorksModel> list = new List<ProjectWorksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.jobs.Where(x => x.No == ProjectNumber && x.Contractor_No == vendorNo).ToList();
                foreach (var projects in query)
                {
                    ProjectWorksModel project = new ProjectWorksModel();

                    project.Project_No = projects.Project_No;
                    project.No = projects.No;
                    project.Project_Start_Date = Convert.ToString(projects.Starting_Date);
                    project.Project_End_Date = Convert.ToString(projects.Ending_Date);
                    project.Description = projects.Description;
                    project.Person_Responsible = projects.Person_Responsible;
                    project.Project_Manager = projects.Person_Responsible;
                    project.Contractor_No = projects.Contractor_No;
                    project.Contractor_Name = projects.Contractor_Name;
                    project.Road_Section_No = projects.Road_Section_No;
                    project.Status = projects.Status;
                    project.IFS_Code = projects.IFS_Code;
                    project.Road_Class_ID = projects.Road_Class_ID;
                    project.Road_Code = projects.Road_Code;
                    project.Road_Section_No = projects.Road_Section_No;
                    project.Funding_Source = projects.Funding_Source;
                    project.Section_Name = projects.Section_Name;
                    project.Section_Name = projects.Section_Name;
                    project.Total_Road_Section_Length_KM = Convert.ToString(projects.Total_Road_Section_Length_KM);
                    project.Constituency_ID = projects.Constituency_ID;
                    project.Creation_Date = Convert.ToString(projects.Creation_Date);
                    project.Contract_Start_Date = Convert.ToString(projects.Contract_Start_Date);
                    project.Contract_End_Date = Convert.ToString(projects.Contract_End_Date);
                    project.Directorate_Code = projects.Directorate_Code;
                    list.Add(project);

                }


            }
            catch (Exception e)
            {

                throw;
            }

            return list;

        }
        private static List<ProjectWorkExecutionPlanModel> GetProjectExecutionPlanDetails(string ExecutionPlanNumber,string vendorNo)
        {
            List<ProjectWorkExecutionPlanModel> list = new List<ProjectWorkExecutionPlanModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.WorkExecutionPlan.Where(x => x.Document_No == ExecutionPlanNumber &&x.Contractor_No== vendorNo).ToList();
                foreach (var orders in query)
                {
                    ProjectWorkExecutionPlanModel order = new ProjectWorkExecutionPlanModel();
                    order.Document_No = orders.Document_No;
                    order.Document_Date = Convert.ToString(orders.Document_Date);
                    order.Commencement_Order_ID = orders.Commencement_Order_ID;
                    order.Project_ID = orders.Project_ID;
                    order.Description = orders.Description;
                    order.Contractor_No = orders.Contractor_No;
                    order.Contractor_Name = orders.Contractor_Name;
                    order.Project_Name = orders.Project_Name;
                    order.Project_Name = orders.Project_Name;
                    order.Road_Code = orders.Road_Code;
                    order.Road_Section_No = orders.Road_Section_No;
                    order.Section_Name = orders.Section_Name;
                    order.Project_Start_Date = Convert.ToString(orders.Project_Start_Date);
                    order.Region_ID = orders.Region_ID;
                    order.Directorate_ID = orders.Directorate_ID;
                    order.Constituency_ID = orders.Constituency_ID;
                    order.Department_ID = orders.Department_ID;
                    order.status = orders.Status;
                    list.Add(order);
             }


            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<OrderToCommenceModel> GetOrdertoCommenceDetails(string OrderNumber)
        {
            List<OrderToCommenceModel> list = new List<OrderToCommenceModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.CommencementOrders.Where(x => x.Notice_of_Award_No == OrderNumber).ToList();
                foreach (var orders in query)
                {
                    OrderToCommenceModel order = new OrderToCommenceModel();
                    order.Notice_No = orders.Notice_of_Award_No;
                    order.Document_Date = Convert.ToString(orders.Document_Date);
                    order.Purchase_Contract_ID = orders.Purchase_Contract_ID;
                    order.Project_ID = orders.Project_ID;
                    order.Description = orders.Description;
                    order.Staff_Appointment_Voucher_No = orders.Staff_Appointment_Voucher_No;
                    order.Contractor_No = orders.Contractor_No;
                    order.Contractor_Name = orders.Contractor_Name;
                    order.Vendor_Address = orders.Vendor_Address;
                    order.Vendor_Address_2 = orders.Vendor_Address_2;
                    order.Vendor_Post_Code = orders.Vendor_Post_Code;
                    order.Status = orders.Status;
                    order.Vendor_City = orders.Vendor_City;
                    order.Primary_E_mail = orders.Primary_E_mail;
                    order.IFS_Code = orders.IFS_Code;
                    order.Tender_Name = orders.Tender_Name;
                    order.Project_Name = orders.Project_Name;
                    order.Road_Code = orders.Road_Code;
                    order.Road_Section_No = orders.Road_Section_No;
                    order.Section_Name = orders.Section_Name;
                    order.Project_Start_Date = Convert.ToString(orders.Project_Start_Date);
                    order.Project_End_Date = Convert.ToString(orders.Project_End_Date);
                    order.Region_ID = orders.Region_ID;
                    order.Directorate_ID = orders.Directorate_ID;
                    order.Constituency_ID = orders.Constituency_ID;
                    order.Department_ID = orders.Department_ID;
                    order.Contract_No = orders.Purchase_Contract_ID;
                    list.Add(order);

                }


            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<JobPlanningLinesModel> GetJobPlanningLinesDetails(string ProjectNumber)
        {
            List<JobPlanningLinesModel> list = new List<JobPlanningLinesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.JobPlaningLines.Where(x => x.Job_No == ProjectNumber).ToList();
                foreach (var orders in query)
                {
                    JobPlanningLinesModel order = new JobPlanningLinesModel();
                    order.Description = orders.Description;
                    order.Line_Amount_LCY = Convert.ToString(orders.Line_Amount_LCY);
                    order.Unit_Cost = Convert.ToString(orders.Unit_Cost);
                    order.Total_Cost = Convert.ToString(orders.Total_Cost);
                    order.Unit_Price = Convert.ToString(orders.Unit_Price);
                    order.Total_Price = Convert.ToString(orders.Total_Price);
                    order.Line_No = Convert.ToString(orders.Line_No);
                    order.Job_No = orders.Job_No;
                    order.Job_Task_No = orders.Job_Task_No;
                    order.Planning_Date = Convert.ToString(orders.Planning_Date);
                    order.Document_No = orders.Document_No;
                    order.Type = orders.Type;
                    order.Description = orders.Description;
                    order.Quantity = Convert.ToString(orders.Quantity);
                    order.Direct_Unit_Cost_LCY = Convert.ToString(orders.Direct_Unit_Cost_LCY);
                    order.Unit_Cost_LCY = Convert.ToString(orders.Unit_Cost_LCY);
                    order.Total_Cost_LCY = Convert.ToString(orders.Total_Cost_LCY);
                    order.Document_Date = Convert.ToString(orders.Document_Date);
                    order.Line_Amount = Convert.ToString(orders.Line_Amount);
                    order.Line_Discount_Amount = Convert.ToString(orders.Line_Discount_Amount);
                    order.Line_Discount_Amount_LCY = Convert.ToString(orders.Line_Discount_Amount_LCY);
                    list.Add(order);

                }


            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<PCORequredDocumentsModel> GetPCORequiredDocuments(string OrderNumber)
        {
            List<PCORequredDocumentsModel> list = new List<PCORequredDocumentsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.PCORequredDocuments.Where(x => x.Notice_No == OrderNumber).ToList();
                foreach (var orders in query)
                {
                    PCORequredDocumentsModel order = new PCORequredDocumentsModel();
                    order.Notice_No = orders.Notice_No;
                    order.Description = Convert.ToString(orders.Description);
                    order.Document_Type = Convert.ToString(orders.Document_Type);
                    order.Requirement_Type = orders.Requirement_Type;
                    order.Guidelines_Instructions = Convert.ToString(orders.Guidelines_Instructions);
                    order.Due_Date =Convert.ToString(orders.Due_Date);
                    list.Add(order);
              }
            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<PCOPlannedMeetingModel> GetPCOPlannedMeeting(string OrderNumber)
        {
            List<PCOPlannedMeetingModel> list = new List<PCOPlannedMeetingModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.PCOPlannedMeeting.Where(x => x.Notice_No == OrderNumber).ToList();
                foreach (var orders in query)
                {
                    PCOPlannedMeetingModel order = new PCOPlannedMeetingModel();
                    order.Notice_No = orders.Notice_No;
                    order.Meeting_Type = orders.Meeting_Type;
                    order.Description = Convert.ToString(orders.Description);
                    order.Start_Date = Convert.ToString(orders.Start_Date);
                    order.Start_Time = orders.Start_Time;
                    order.End_Date = Convert.ToString(orders.End_Date);
                    order.End_Time = orders.End_Time;
                    order.Venue_Location = orders.Venue_Location;
                    list.Add(order);
             }
            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<WEPContractorTeamModel> GetWEPContractorTeamDetails(string ExecutionPlanNumber,string vendorNo)
        {
            List<WEPContractorTeamModel> list = new List<WEPContractorTeamModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.WEPContractorTeam.Where(x => x.Document_No == ExecutionPlanNumber&&x.Contractor_No== vendorNo).ToList();
                foreach (var orders in query)
                {
                    WEPContractorTeamModel order = new WEPContractorTeamModel();
                    order.Document_No = orders.Document_No;
                    order.Contractor_No = Convert.ToString(orders.Contractor_No);
                    order.Name = orders.Name;
                    order.Address = orders.Address;
                    order.Address_2 = orders.Address_2;
                    order.City = orders.City;
                    order.Post_Code = orders.Post_Code;
                    order.Country_Region_Code = orders.Country_Region_Code;
                    order.Role_Code = orders.Role_Code;
                    order.Designation = orders.Designation;
                    order.Effective_Date =Convert.ToString(orders.Effective_Date);
                    order.Expiry_Date =Convert.ToString(orders.Expiry_Date);
                    order.Staff_Category = orders.Staff_Category;
                    order.Contractor_Staff_No = orders.Contractor_Staff_No;
                    list.Add(order);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        private static List<ProjectinternalTeamModel> GetProjectInternalTeamDetails(string CommencmentOrderNumber)
        {
            List<ProjectinternalTeamModel> list = new List<ProjectinternalTeamModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ProjectInternalTeam.Where(x => x.Commencement_Order_No == CommencmentOrderNumber).ToList();
                foreach (var orders in query)
                {
                    ProjectinternalTeamModel order = new ProjectinternalTeamModel();
                    order.Commencement_Order_No = orders.Commencement_Order_No;
                    order.Resource_No = Convert.ToString(orders.Resource_No);
                    order.Name = orders.Name;
                    order.Email = orders.Email;
                    order.Address = orders.Address;
                    order.Address_2 = orders.Address_2;
                    order.City = orders.City;
                    order.Post_Code = orders.Post_Code;
                    order.Country_Region_Code = orders.Country_Region_Code;
                    order.Phone_No = orders.Phone_No;
                    order.Role_Code = orders.Role_Code;
                    order.Designation = orders.Designation;
                    list.Add(order);
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }
        public ActionResult DraftWorkExecutionPlans()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<WorkExecutionPlanModel> list = new List<WorkExecutionPlanModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.WorkExecutionPlan.Where(x => x.Contractor_No == vendorNo && x.Status == "Open").ToList();
                    foreach (var roadworks in query)
                    {
                        WorkExecutionPlanModel roadwork = new WorkExecutionPlanModel();
                        roadwork.Document_No = roadworks.Document_No;
                        roadwork.Commencement_Order_ID = roadworks.Commencement_Order_ID;
                        roadwork.Description = roadworks.Description;
                        roadwork.Project_ID = roadworks.Project_ID;
                        roadwork.Region_ID = roadworks.Region_ID;
                        roadwork.Document_Date = Convert.ToString(roadworks.Document_Date);
                        roadwork.Purchase_Contract_ID = roadworks.Purchase_Contract_ID;
                        roadwork.Contractor_Name = roadworks.Contractor_Name;
                        roadwork.Project_Name = roadworks.Project_Name;
                        roadwork.Road_Code = Convert.ToString(roadworks.Road_Code);
                        roadwork.Road_Section_No = Convert.ToString(roadworks.Road_Section_No);
                        roadwork.Document_Date = Convert.ToString(roadworks.Document_Date);
                        roadwork.Project_Start_Date = Convert.ToString(roadworks.Project_Start_Date);
                        roadwork.Project_End_Date = Convert.ToString(roadworks.Project_End_Date);
                        roadwork.Status = Convert.ToString(roadworks.Status);
                        list.Add(roadwork);
                    }

                    }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult CancelledContracts()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ContractsModel> list = new List<ContractsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.PurchaseHeader.Where(x => x.Document_Type == "Blanket Order" && x.Contract_Description != "").ToList();
                    foreach (var allcontracts in query)
                    {
                        ContractsModel contract = new ContractsModel();
                        contract.Document_Type = allcontracts.Document_Type;
                        contract.Buy_from_Vendor_No = allcontracts.Buy_from_Vendor_No;
                        contract.No = allcontracts.No;
                        contract.Pay_to_Name = allcontracts.Pay_to_Name;
                        contract.Pay_to_Vendor_No = allcontracts.Pay_to_Vendor_No;
                        contract.Pay_to_Name_2 = allcontracts.Pay_to_Name_2;
                        contract.Pay_to_Address = allcontracts.Pay_to_Address;
                        contract.Pay_to_Address_2 = allcontracts.Pay_to_Address_2;
                        contract.Pay_to_Contact = allcontracts.Pay_to_Contact;
                        contract.Your_Reference = allcontracts.Your_Reference;
                        contract.Order_Date = Convert.ToString(allcontracts.Order_Date);
                        contract.Posting_Date = Convert.ToString(allcontracts.Posting_Date);
                        contract.Location_Code = allcontracts.Location_Code;
                        contract.Region_Code = allcontracts.Region;
                        contract.Link_Name = allcontracts.Link_Name;
                        contract.Works_Length = Convert.ToString(allcontracts.Works_Length_Km);
                        contract.Invatition_for_supply = allcontracts.Invitation_For_Supply_No;
                        contract.Tender_Description = allcontracts.Tender_Name;
                        contract.Link_Name = allcontracts.Link_Name;
                        contract.Contract_Description = allcontracts.Contract_Description;
                        contract.Contract_End_Date = Convert.ToString(allcontracts.Contract_End_Date);
                        contract.Contract_Start_Date = Convert.ToString(allcontracts.Contract_Start_Date);
                        contract.Contract_Value = Convert.ToString(allcontracts.Approved_Requisition_Amount);
                        list.Add(contract);
                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }

        public ActionResult CompletedProjectWorks()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.OngoingWorks = GetOngoingProjectsWorks(vendorNo);
                model.CompletedWorks = GetCompletedProjectsWorks(vendorNo);
                return View(model);
            }
        }  
        public ActionResult RegisterUpcomingMeeting()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                return View();
            }
        }
        public ActionResult OngoingProjectWorks()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.OngoingWorks = GetOngoingProjectsWorks(vendorNo);
                model.CompletedWorks = GetCompletedProjectsWorks(vendorNo);
                return View(model);
            }
        }
        private static List<ProjectWorksModel> GetOngoingProjectsWorks(string VendorNo)
        {
            List<ProjectWorksModel> list = new List<ProjectWorksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.ProjectsWorks.Where(x =>x.Contractor_No== VendorNo);
                foreach (var works in query)
                {
                    ProjectWorksModel work = new ProjectWorksModel();
                    work.No = works.No;
                    work.Road_Section_No = works.Road_Section_No;
                    work.Search_Description = works.Search_Description;
                    work.Description = works.Description;
                    work.Description_2 = works.Description_2;
                    work.Bill_to_Customer_No = works.Bill_to_Customer_No;
                    work.Creation_Date = Convert.ToString(works.Creation_Date);
                    work.Ending_Date = Convert.ToString(works.Ending_Date);
                    work.Status = works.Status;
                    work.Person_Responsible = works.Person_Responsible;
                    work.Project_Manager = works.Project_Manager;
                    work.Project_Budget = Convert.ToString(works.Project_Budget);
                    work.Actual_Project_Costs = Convert.ToString(works.Actual_Project_Costs);
                    work.Project_Start_Date = Convert.ToString(works.Project_Start_Date);
                    work.Project_End_Date = Convert.ToString(works.Project_End_Date);
                    work.Road_Length_KM = Convert.ToString(works.Road_Length_KM);
                    work.Funding_Source = works.Funding_Source;
                    work.Project_Category = works.Project_Category;
                    work.Road_Project_Sub_Category = works.Road_Project_Sub_Category;
                    work.Road_Project_Type = works.Road_Project_Type;
                    work.Road_Code = works.Road_Code;
                    work.Record_Type = works.Record_Type;
                    work.Road_Project_Catergory = works.Road_Project_Category;
                    work.Road_Class_ID = works.Road_Class_ID;
                    work.Section_Name = works.Section_Name;
                    work.County_ID = works.County_ID;
                    work.Region_ID = works.Region_ID;
                    work.Section_Start_Chainage_Km = Convert.ToString(works.Section_Start_Chainage_Km);
                    work.Section_End_Chainage_KM = Convert.ToString(works.Section_End_Chainage_KM);
                    work.Total_Road_Section_Length_KM = Convert.ToString(works.Total_Road_Section_Length_KM);
                    work.Contractor_No = works.Contractor_No;
                    work.Contractor_Name = works.Contractor_Name;
                    work.Contract_Start_Date = Convert.ToString(works.Contract_Start_Date);
                    work.Contract_End_Date = Convert.ToString(works.Contract_End_Date);
                    work.Notice_of_Award_Date = Convert.ToString(works.Notice_of_Award_Date);
                    work.IFS_Code = works.IFS_Code;
                    work.Project_Commencement_Order = works.Project_Commencement_Order;
                    work.Road_Works_Category = works.Road_Works_Category;
                    work.Road_Project_Category = works.Road_Project_Category;
                    list.Add(work);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return list;

        }
        private static List<ProjectWorksModel> GetCompletedProjectsWorks(string VendorNo)
        {
            List<ProjectWorksModel> list = new List<ProjectWorksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.jobs.Where(x => x.Contractor_No == VendorNo).ToList();
                foreach (var projects in query)
                {
                    ProjectWorksModel project = new ProjectWorksModel();

                    project.Project_No = projects.Project_No;
                    project.No = projects.No;
                    project.Project_Start_Date = Convert.ToString(projects.Starting_Date);
                    project.Project_End_Date = Convert.ToString(projects.Ending_Date);
                    project.Description = projects.Description;
                    project.Person_Responsible = projects.Person_Responsible;
                    project.Project_Manager = projects.Person_Responsible;
                    project.Contractor_No = projects.Contractor_No;
                    project.Contractor_Name = projects.Contractor_Name;
                    project.Road_Section_No = projects.Road_Section_No;
                    project.Status = projects.Status;
                    project.IFS_Code = projects.IFS_Code;
                    project.Road_Class_ID = projects.Road_Class_ID;
                    project.Road_Code = projects.Road_Code;
                    project.Road_Section_No = projects.Road_Section_No;
                    project.Funding_Source = projects.Funding_Source;
                    project.Section_Name = projects.Section_Name;
                    project.Section_Name = projects.Section_Name;
                    project.Total_Road_Section_Length_KM = Convert.ToString(projects.Total_Road_Section_Length_KM);
                    project.Constituency_ID = projects.Constituency_ID;
                    project.Creation_Date = Convert.ToString(projects.Creation_Date);
                    project.Contract_Start_Date = Convert.ToString(projects.Contract_Start_Date);
                    project.Contract_End_Date = Convert.ToString(projects.Contract_End_Date);
                    project.Directorate_Code = projects.Directorate_Code;
                    list.Add(project);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return list;

        }
        public ActionResult PendingCommencementWorksOrders()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ActiveContracts> list = new List<ActiveContracts>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Tender_Name != "").ToList();
                    foreach (var tenders in query)
                    {
                        ActiveContracts tender = new ActiveContracts();
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
        public ActionResult MyTransactions()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<BidResponseDetailsModel> list = new List<BidResponseDetailsModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var vendorNo = Session["vendorNo"].ToString();
                    var query = nav.PurchaseHeader.Where(x => x.Buy_from_Vendor_No ==vendorNo && x.Document_Type == "Quote").ToList();
                    foreach (var tenders in query)
                    {
                        BidResponseDetailsModel tender = new BidResponseDetailsModel();
                        tender.No = tenders.No;
                        tender.Invitation_For_Supply_No = tenders.Invitation_For_Supply_No;
                        tender.Pay_to_Vendor_No = tenders.Buy_from_Vendor_No;
                        tender.Bidder_type = tenders.Bidder_Type;
                        tender.Tender_Description = tenders.Tender_Description;
                        tender.Tender_Name = tenders.Tender_Name;
                        tender.Location_Code = tenders.Location_Code;
                        tender.Amount = Convert.ToString(tenders.Amount);
                        tender.Document_Date = Convert.ToString(tenders.Document_Date);
                        tender.Status = tenders.Status;
                        tender.Amount_Including_VAT = Convert.ToString(tenders.Amount_Including_VAT);
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
        public ActionResult ViewProjectContracts()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ActiveContracts> list = new List<ActiveContracts>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Tender_Name != "").ToList();
                    foreach (var tenders in query)
                    {
                        ActiveContracts tender = new ActiveContracts();
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
        public ActionResult MyStatement()
        {
            var vendorNo = Session["vendorNo"].ToString();
            dynamic model = new ExpandoObject();
            model.Vendors = GetVendors(vendorNo);
            model.Statement = GetVendorsStatement(vendorNo);
            return View(model);

        }
        public ActionResult RoadGeneralDetails(string roadLinkCode)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Vendors = GetVendors(vendorNo);
                model.RoadLinkCode = GetRoadInventoryGeneralDetails(roadLinkCode);
                model.RoadSections = GetRoadInventorySectionDetails(roadLinkCode);
                //model.RoadStructures = GetRoadInventoryStructuresDetails(roadLinkCode);
                model.RoadEnvirons = GetRoadInventoryEnvironsDetails(roadLinkCode);
                model.RoadConditions = GetRoadInventoryConditionsDetails(roadLinkCode);
                return View(model);
            }

        }
        private static List<RoadsInventoryModel> GetRoadInventoryGeneralDetails(string roadLinkCode)
        {
            List<RoadsInventoryModel> list = new List<RoadsInventoryModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.RoadInventory.Where(x => x.Road_Code == roadLinkCode).ToList();
                foreach (var roadlinks in query)
                {
                    RoadsInventoryModel roadlink = new RoadsInventoryModel();
                    roadlink.Road_Code = roadlinks.Road_Code;
                    roadlink.Link_Name = roadlinks.Link_Name;
                    roadlink.Road_Category = roadlinks.Road_Category;
                    roadlink.Carriageway_Type = roadlinks.Carriageway_Type;
                    roadlink.Primary_County_ID = roadlinks.Primary_County_ID;
                    roadlink.Start_Chainage_KM = Convert.ToString(roadlinks.Start_Chainage_KM);
                    roadlink.End_Chainage_KM = Convert.ToString(roadlinks.End_Chainage_KM);
                    roadlink.Gazetted_Road_Length_KMs = Convert.ToString(roadlinks.Gazetted_Road_Length_KMs);
                    roadlink.No_of_Road_Sections = Convert.ToString(roadlinks.No_of_Road_Sections);
                    roadlink.General_Road_Surface_Condition = roadlinks.General_Road_Surface_Condition;
                    roadlink.Start_Point_Longitude = Convert.ToString(roadlinks.Start_Point_Longitude);
                    roadlink.Start_Point_Latitude = Convert.ToString(roadlinks.Start_Point_Latitude);
                    roadlink.End_Point_Longitude = Convert.ToString(roadlinks.End_Point_Longitude);
                    roadlink.End_Point_Latitude = Convert.ToString(roadlinks.End_Point_Latitude);
                    roadlink.Paved_Road_Length_Km = Convert.ToString(roadlinks.Paved_Road_Length_Km);
                    roadlink.Paved_Road_Length = Convert.ToString(roadlinks.Paved_Road_Length);
                    roadlink.Unpaved_Road_Length = Convert.ToString(roadlinks.Unpaved_Road_Length);
                    roadlink.Original_Road_Agency = Convert.ToString(roadlinks.Original_Road_Agency);
                    list.Add(roadlink);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<RoadLinkConditionModel> GetRoadInventoryConditionsDetails(string roadLinkCode)
        {
            List<RoadLinkConditionModel> list = new List<RoadLinkConditionModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.PavementSurfaceConditions.Where(x => x.Road_Code == roadLinkCode).ToList();
                foreach (var roadlinks in query)
                {
                    RoadLinkConditionModel roadlink = new RoadLinkConditionModel();
                    roadlink.Enrty_No = Convert.ToString(roadlinks.Enrty_No);
                    roadlink.Posting_Date = Convert.ToString(roadlinks.Posting_Date);
                    roadlink.Document_No = roadlinks.Document_No;
                    roadlink.Road_Code = Convert.ToString(roadlinks.Road_Code);
                    roadlink.Road_Section_No = Convert.ToString(roadlinks.Road_Section_No);
                    roadlink.Pavement_Surface_Type = Convert.ToString(roadlinks.Pavement_Surface_Type);
                    roadlink.Pavement_Category = Convert.ToString(roadlinks.Pavement_Category);
                    roadlink.Start_Chainage = Convert.ToString(roadlinks.Start_Chainage);
                    roadlink.End_Chainage = Convert.ToString(roadlinks.End_Chainage);
                    roadlink.Road_Length_Kms = Convert.ToString(roadlinks.Road_Length_Kms);
                    roadlink.Road_Class_ID = Convert.ToString(roadlinks.Road_Class_ID);
                    roadlink.Constituency_ID = Convert.ToString(roadlinks.Constituency_ID);
                    roadlink.County_ID = Convert.ToString(roadlinks.County_ID);
                    roadlink.Region_ID = Convert.ToString(roadlinks.Region_ID);
                    list.Add(roadlink);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<RoadSectionsModel> GetRoadInventorySectionDetails(string roadLinkCode)
        {
            List<RoadSectionsModel> list = new List<RoadSectionsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.RoadsSections.Where(x => x.Road_Code == roadLinkCode).ToList();
                foreach (var roadlinksections in query)
                {
                    RoadSectionsModel roadlinksection = new RoadSectionsModel();
                    roadlinksection.Road_Code = roadlinksections.Road_Code;
                    roadlinksection.Road_Section_No = roadlinksections.Road_Section_No;
                    roadlinksection.Road_Category = roadlinksections.Road_Category;
                    roadlinksection.CarriageAwayType = roadlinksections.Carriageway_Type;
                    roadlinksection.Constituency = roadlinksections.Primary_Constituency_ID;
                    roadlinksection.Region = Convert.ToString(roadlinksections.Primary_Region_ID);
                    roadlinksection.Start_Chainage = Convert.ToString(roadlinksections.Start_Chainage_KM);
                    roadlinksection.End_Chainage = Convert.ToString(roadlinksections.End_Chainage_KM);
                    roadlinksection.Total_Road_Length = Convert.ToString(roadlinksections.Total_Road_Length_KMs);
                    roadlinksection.Start_Point_Longitude = Convert.ToString(roadlinksections.Start_Point_Longitude);
                    roadlinksection.Start_Point_Latitude = Convert.ToString(roadlinksections.Start_Point_Latitude);
                    roadlinksection.End_Point_Longitude = Convert.ToString(roadlinksections.End_Point_Longitude);
                    roadlinksection.End_Point_Latitude = Convert.ToString(roadlinksections.End_Point_Latitude);
                    roadlinksection.Paved_Road_Lenght_Km = Convert.ToString(roadlinksections.Paved_Road_Length_Km);
                    roadlinksection.Paved_Road_Length = Convert.ToString(roadlinksections.Paved_Road_Length);
                    roadlinksection.UnPaved_Road_Length = Convert.ToString(roadlinksections.Unpaved_Road_Length);
                    list.Add(roadlinksection);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
 
        private static List<RoadLinkEnvironsModel> GetRoadInventoryEnvironsDetails(string roadLinkCode)
        {
            List<RoadLinkEnvironsModel> list = new List<RoadLinkEnvironsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.RoadEnvirons.ToList();
                foreach (var roadlinkenvirons in query)
                {
                    RoadLinkEnvironsModel roadlinkenviron = new RoadLinkEnvironsModel();
                   // roadlinkenviron.Entry_No = Convert.ToString(roadlinkenvirons.Entry_No);
                    roadlinkenviron.Road_Environ_Category = roadlinkenvirons.Road_Environ_Category;
                    roadlinkenviron.Description = roadlinkenvirons.Description;
                    roadlinkenviron.Road_Code = roadlinkenvirons.Road_Code;
                    roadlinkenviron.Road_Section_No = roadlinkenvirons.Road_Section_No;
                    roadlinkenviron.Connected_to_Road_Link = Convert.ToString(roadlinkenvirons.Connected_to_Road_Link);
                    roadlinkenviron.Location_Details = roadlinkenvirons.Location_Details;
                    roadlinkenviron.Road_Class_ID = roadlinkenvirons.Road_Class_ID;
                    roadlinkenviron.Constituency_ID = roadlinkenvirons.Constituency_ID;
                    roadlinkenviron.Region_ID = roadlinkenvirons.Region_ID;
                    list.Add(roadlinkenviron);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        public ActionResult PendingTasks()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ProjectTasksModel> list = new List<ProjectTasksModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.JobTask.ToList();
                    foreach (var projecttaks in query)
                    {
                        ProjectTasksModel projecttask = new ProjectTasksModel();
                        projecttask.Job_No = projecttaks.Job_No;
                        projecttask.Description = projecttaks.Description;
                        projecttask.Department_Code = projecttaks.Department_Code;
                        projecttask.Directorate_Code = projecttaks.Directorate_Code;
                        projecttask.Division = projecttaks.Division;
                        projecttask.Commitments = Convert.ToString(projecttaks.Commitments);
                        projecttask.Start_Point_Km = Convert.ToString(projecttaks.Start_Point_Km);
                        projecttask.End_Point_Km = Convert.ToString(projecttaks.End_Point_Km);
                        projecttask.Start_Date = Convert.ToString(projecttaks.Start_Date);
                        projecttask.End_Date = Convert.ToString(projecttaks.End_Date);
                        projecttask.Funding_Source = projecttaks.Funding_Source;
                        projecttask.Procurement_Method = projecttaks.Procurement_Method;
                        projecttask.Surface_Types = projecttaks.Surface_Types;
                        projecttask.Road_Condition = projecttaks.Road_Condition;
                        projecttask.Completed_Length_Km = Convert.ToString(projecttaks.Completed_Length_Km);
                        projecttask.Roads_Category = projecttaks.Roads_Category;
                        projecttask.Fund_Type = projecttaks.Fund_Type;
                        projecttask.Execution_Method = projecttaks.Execution_Method;
                        projecttask.Region = projecttaks.Global_Dimension_1_Code;
                        projecttask.Constituency = projecttaks.Global_Dimension_2_Code;
                        projecttask.Execution_Method = projecttaks.Execution_Method;
                        list.Add(projecttask);
                    }

                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult NoticeofCompletedWorks()
        {
          return  View();

        }
        public ActionResult CompletedTasks()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ProjectTasksModel> list = new List<ProjectTasksModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.JobTask.ToList();
                    foreach (var projecttaks in query)
                    {
                        ProjectTasksModel projecttask = new ProjectTasksModel();
                        projecttask.Job_No = projecttaks.Job_No;
                        projecttask.Description = projecttaks.Description;
                        projecttask.Department_Code = projecttaks.Department_Code;
                        projecttask.Directorate_Code = projecttaks.Directorate_Code;
                        projecttask.Division = projecttaks.Division;
                        projecttask.Commitments = Convert.ToString(projecttaks.Commitments);
                        projecttask.Start_Point_Km = Convert.ToString(projecttaks.Start_Point_Km);
                        projecttask.End_Point_Km = Convert.ToString(projecttaks.End_Point_Km);
                        projecttask.Start_Date = Convert.ToString(projecttaks.Start_Date);
                        projecttask.End_Date = Convert.ToString(projecttaks.End_Date);
                        projecttask.Funding_Source = projecttaks.Funding_Source;
                        projecttask.Procurement_Method = projecttaks.Procurement_Method;
                        projecttask.Surface_Types = projecttaks.Surface_Types;
                        projecttask.Road_Condition = projecttaks.Road_Condition;
                        projecttask.Completed_Length_Km = Convert.ToString(projecttaks.Completed_Length_Km);
                        projecttask.Roads_Category = projecttaks.Roads_Category;
                        projecttask.Fund_Type = projecttaks.Fund_Type;
                        projecttask.Execution_Method = projecttaks.Execution_Method;
                        projecttask.Region = projecttaks.Global_Dimension_1_Code;
                        projecttask.Constituency = projecttaks.Global_Dimension_2_Code;
                        projecttask.Execution_Method = projecttaks.Execution_Method;
                        list.Add(projecttask);
                    }

                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }
        public ActionResult AllTasks(string ProjectCode)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.ActiveProjectTasksDetails = GetProjectTasksDetails(ProjectCode);
                model.OpenProjectTasksDetails = GetOpenProjectTasksDetails(ProjectCode);
                model.PendingProjectTasksDetails = GetPendingProjectTasksDetails(ProjectCode);
                model.CompletedProjectTasksDetails = GetCompletedProjectTasksDetails(ProjectCode);
                return View(model);

            }
        }

        public ActionResult ProjectUpcomingMeetings()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                return View();
            }
        }
        public ActionResult DraftStatusReports()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ProjectTasksModel> list = new List<ProjectTasksModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.JobTask.ToList();
                    foreach (var projecttaks in query)
                    {
                        ProjectTasksModel projecttask = new ProjectTasksModel();
                        projecttask.Job_No = projecttaks.Job_No;
                        projecttask.Description = projecttaks.Description;
                        projecttask.Department_Code = projecttaks.Department_Code;
                        projecttask.Directorate_Code = projecttaks.Directorate_Code;
                        projecttask.Division = projecttaks.Division;
                        projecttask.Commitments = Convert.ToString(projecttaks.Commitments);
                        projecttask.Start_Point_Km = Convert.ToString(projecttaks.Start_Point_Km);
                        projecttask.End_Point_Km = Convert.ToString(projecttaks.End_Point_Km);
                        projecttask.Start_Date = Convert.ToString(projecttaks.Start_Date);
                        projecttask.End_Date = Convert.ToString(projecttaks.End_Date);
                        projecttask.Funding_Source = projecttaks.Funding_Source;
                        projecttask.Procurement_Method = projecttaks.Procurement_Method;
                        projecttask.Surface_Types = projecttaks.Surface_Types;
                        projecttask.Road_Condition = projecttaks.Road_Condition;
                        projecttask.Completed_Length_Km = Convert.ToString(projecttaks.Completed_Length_Km);
                        projecttask.Roads_Category = projecttaks.Roads_Category;
                        projecttask.Fund_Type = projecttaks.Fund_Type;
                        projecttask.Execution_Method = projecttaks.Execution_Method;
                        projecttask.Region = projecttaks.Global_Dimension_1_Code;
                        projecttask.Constituency = projecttaks.Global_Dimension_2_Code;
                        projecttask.Execution_Method = projecttaks.Execution_Method;
                        list.Add(projecttask);
                    }

                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }

        }
        private static List<ProjectTasksModel> GetCompletedProjectTasksDetails(string ProjectCode)
        {
            List<ProjectTasksModel> list = new List<ProjectTasksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.JobTask.ToList();
                foreach (var projecttaks in query)
                {
                    ProjectTasksModel projecttask = new ProjectTasksModel();
                    projecttask.Job_No = projecttaks.Job_No;
                    projecttask.Description = projecttaks.Description;
                    projecttask.Department_Code = projecttaks.Department_Code;
                    projecttask.Directorate_Code = projecttaks.Directorate_Code;
                    projecttask.Division = projecttaks.Division;
                    projecttask.Commitments = Convert.ToString(projecttaks.Commitments);
                    projecttask.Start_Point_Km = Convert.ToString(projecttaks.Start_Point_Km);
                    projecttask.End_Point_Km = Convert.ToString(projecttaks.End_Point_Km);
                    projecttask.Start_Date = Convert.ToString(projecttaks.Start_Date);
                    projecttask.End_Date = Convert.ToString(projecttaks.End_Date);
                    projecttask.Funding_Source = projecttaks.Funding_Source;
                    projecttask.Procurement_Method = projecttaks.Procurement_Method;
                    projecttask.Surface_Types = projecttaks.Surface_Types;
                    projecttask.Road_Condition = projecttaks.Road_Condition;
                    projecttask.Completed_Length_Km = Convert.ToString(projecttaks.Completed_Length_Km);
                    projecttask.Roads_Category = projecttaks.Roads_Category;
                    projecttask.Fund_Type = projecttaks.Fund_Type;
                    projecttask.Execution_Method = projecttaks.Execution_Method;
                    projecttask.Region = projecttaks.Global_Dimension_1_Code;
                    projecttask.Constituency = projecttaks.Global_Dimension_2_Code;
                    projecttask.Execution_Method = projecttaks.Execution_Method;
                    list.Add(projecttask);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<ProjectTasksModel> GetPendingProjectTasksDetails(string ProjectCode)
        {
            List<ProjectTasksModel> list = new List<ProjectTasksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.JobTask.ToList();
                foreach (var projecttaks in query)
                {
                    ProjectTasksModel projecttask = new ProjectTasksModel();
                    projecttask.Job_No = projecttaks.Job_No;
                    projecttask.Description = projecttaks.Description;
                    projecttask.Department_Code = projecttaks.Department_Code;
                    projecttask.Directorate_Code = projecttaks.Directorate_Code;
                    projecttask.Division = projecttaks.Division;
                    projecttask.Commitments = Convert.ToString(projecttaks.Commitments);
                    projecttask.Start_Point_Km = Convert.ToString(projecttaks.Start_Point_Km);
                    projecttask.End_Point_Km = Convert.ToString(projecttaks.End_Point_Km);
                    projecttask.Start_Date = Convert.ToString(projecttaks.Start_Date);
                    projecttask.End_Date = Convert.ToString(projecttaks.End_Date);
                    projecttask.Funding_Source = projecttaks.Funding_Source;
                    projecttask.Procurement_Method = projecttaks.Procurement_Method;
                    projecttask.Surface_Types = projecttaks.Surface_Types;
                    projecttask.Road_Condition = projecttaks.Road_Condition;
                    projecttask.Completed_Length_Km = Convert.ToString(projecttaks.Completed_Length_Km);
                    projecttask.Roads_Category = projecttaks.Roads_Category;
                    projecttask.Fund_Type = projecttaks.Fund_Type;
                    projecttask.Execution_Method = projecttaks.Execution_Method;
                    projecttask.Region = projecttaks.Global_Dimension_1_Code;
                    projecttask.Constituency = projecttaks.Global_Dimension_2_Code;
                    projecttask.Execution_Method = projecttaks.Execution_Method;
                    list.Add(projecttask);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<ProjectTasksModel> GetOpenProjectTasksDetails(string ProjectCode)
        {
            List<ProjectTasksModel> list = new List<ProjectTasksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.JobTask.ToList();
                foreach (var projecttaks in query)
                {
                    ProjectTasksModel projecttask = new ProjectTasksModel();
                    projecttask.Job_No = projecttaks.Job_No;
                    projecttask.Description = projecttaks.Description;
                    projecttask.Department_Code = projecttaks.Department_Code;
                    projecttask.Directorate_Code = projecttaks.Directorate_Code;
                    projecttask.Division = projecttaks.Division;
                    projecttask.Commitments = Convert.ToString(projecttaks.Commitments);
                    projecttask.Start_Point_Km = Convert.ToString(projecttaks.Start_Point_Km);
                    projecttask.End_Point_Km = Convert.ToString(projecttaks.End_Point_Km);
                    projecttask.Start_Date = Convert.ToString(projecttaks.Start_Date);
                    projecttask.End_Date = Convert.ToString(projecttaks.End_Date);
                    projecttask.Funding_Source = projecttaks.Funding_Source;
                    projecttask.Procurement_Method = projecttaks.Procurement_Method;
                    projecttask.Surface_Types = projecttaks.Surface_Types;
                    projecttask.Road_Condition = projecttaks.Road_Condition;
                    projecttask.Completed_Length_Km = Convert.ToString(projecttaks.Completed_Length_Km);
                    projecttask.Roads_Category = projecttaks.Roads_Category;
                    projecttask.Fund_Type = projecttaks.Fund_Type;
                    projecttask.Execution_Method = projecttaks.Execution_Method;
                    projecttask.Region = projecttaks.Global_Dimension_1_Code;
                    projecttask.Constituency = projecttaks.Global_Dimension_2_Code;
                    projecttask.Execution_Method = projecttaks.Execution_Method;
                    list.Add(projecttask);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        private static List<ProjectTasksModel> GetProjectTasksDetails(string ProjectCode)
        {
            List<ProjectTasksModel> list = new List<ProjectTasksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.JobTask.ToList();
                foreach (var projecttaks in query)
                {
                    ProjectTasksModel projecttask = new ProjectTasksModel();
                    projecttask.Job_No = projecttaks.Job_No;
                    projecttask.Description = projecttaks.Description;
                    projecttask.Department_Code = projecttaks.Department_Code;
                    projecttask.Directorate_Code = projecttaks.Directorate_Code;
                    projecttask.Division = projecttaks.Division;
                    projecttask.Commitments = Convert.ToString(projecttaks.Commitments);
                    projecttask.Start_Point_Km = Convert.ToString(projecttaks.Start_Point_Km);
                    projecttask.End_Point_Km = Convert.ToString(projecttaks.End_Point_Km);
                    projecttask.Start_Date = Convert.ToString(projecttaks.Start_Date);
                    projecttask.End_Date = Convert.ToString(projecttaks.End_Date);
                    projecttask.Funding_Source = projecttaks.Funding_Source;
                    projecttask.Procurement_Method = projecttaks.Procurement_Method;
                    projecttask.Surface_Types = projecttaks.Surface_Types;
                    projecttask.Road_Condition = projecttaks.Road_Condition;
                    projecttask.Completed_Length_Km = Convert.ToString(projecttaks.Completed_Length_Km);
                    projecttask.Roads_Category = projecttaks.Roads_Category;
                    projecttask.Fund_Type = projecttaks.Fund_Type;
                    projecttask.Execution_Method = projecttaks.Execution_Method;
                    projecttask.Region = projecttaks.Global_Dimension_1_Code;
                    projecttask.Constituency = projecttaks.Global_Dimension_2_Code;
                    projecttask.Execution_Method = projecttaks.Execution_Method;
                    list.Add(projecttask);
                }

            }
            catch (Exception e)
            {

                throw;
            }
            return list;
        }
        public ActionResult RoadsInventory()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<RoadsInventoryModel> list = new List<RoadsInventoryModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.RoadInventory.ToList();
                    foreach (var roads in query)
                    {
                        RoadsInventoryModel road = new RoadsInventoryModel();
                        road.Road_Code = roads.Road_Code;
                        road.Link_Name = roads.Link_Name;
                        road.Road_Category = roads.Road_Category;
                        road.Carriageway_Type = roads.Carriageway_Type;
                        road.Primary_County_ID = roads.Primary_County_ID;
                        road.Start_Chainage_KM = Convert.ToString(roads.Start_Chainage_KM);
                        road.End_Chainage_KM = Convert.ToString(roads.End_Chainage_KM);
                        road.Gazetted_Road_Length_KMs = Convert.ToString(roads.Gazetted_Road_Length_KMs);
                        road.No_of_Road_Sections = Convert.ToString(roads.No_of_Road_Sections);
                        road.General_Road_Surface_Condition = roads.General_Road_Surface_Condition;
                        road.Start_Point_Longitude = Convert.ToString(roads.Start_Point_Longitude);
                        road.End_Point_Longitude = Convert.ToString(roads.End_Point_Longitude);
                        road.Paved_Road_Length_Km = Convert.ToString(roads.Paved_Road_Length_Km);
                        road.Unpaved_Road_Length = Convert.ToString(roads.Unpaved_Road_Length);
                        road.Original_Road_Agency = roads.Original_Road_Agency;
                        road.Road_Class_ID = roads.Road_Class_ID;
                        list.Add(road);
                    }

                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
        }

        public ActionResult ContractorProfile()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Vendors = GetVendors(vendorNo);
                model.BanksDetails = GetBanks(vendorNo);
                model.StakeholdersDetails = GetStakeholders(vendorNo);
                model.Litigations = GetVendorLitigationHistoryDetails(vendorNo);
                model.VendorPastExperience = GetVendorPastExeprience(vendorNo);
                model.Vendorbalancesheet = GetVendorBalanaceDetails(vendorNo);
                model.Vendorincomestatement = GetVendorIncomeStatementDetails(vendorNo);
                model.VendorProfessionalStaff = GetVendorProfessionalStaff(vendorNo);
                model.AttachedDocuments = PopulateSupplierRegistrationDocuments(vendorNo);
                return View(model);
            }
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

            return staffDetails;
        }
        private static List<Models.Contractor.SharePointTModel> PopulateSupplierRegistrationDocuments(string ittpnumber)
        {
            List<Models.Contractor.SharePointTModel> alldocuments = new List<Models.Contractor.SharePointTModel>();
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
                                    alldocuments.Add(new Models.Contractor.SharePointTModel { FileName = file.Name });

                                }
                            }
                        }

                    }
                }
                return alldocuments;
            }
        }
        public ActionResult MyAccount()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Vendors = GetVendors(vendorNo);
                model.BanksDetails = GetBanks(vendorNo);
                model.StakeholdersDetails = GetStakeholders(vendorNo);
                model.PrequalifcationHistory = GetPrequalificationHistory(vendorNo);
                model.PastExperience = GetVendorPastExeprience(vendorNo);
                model.litigationhistory = GetVendorLitigationHistoryDetails(vendorNo);
                model.balancesheet = GetVendorBalanaceDetails(vendorNo);
                model.incomestatement = GetVendorIncomeStatementDetails(vendorNo);
                return View(model);
            }
        }
        public  ActionResult ViewOngoingProject( string ProjectNumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Vendors = GetVendors(vendorNo);
                model.Project = GetProjectDetails(vendorNo,ProjectNumber);
                model.ProjectPackages = GetProjectPackages(vendorNo, ProjectNumber);
                model.ProjectBillsItems = GetProjectBillItems(vendorNo, ProjectNumber);
                return View(model);
            }
        }
        public ActionResult ViewCompletedProject(string ProjectNumber)
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.Vendors = GetVendors(vendorNo);
                model.Project = GetCompletedProjecttDetails(vendorNo, ProjectNumber);
                model.ProjectPackages = GetProjectPackages(vendorNo, ProjectNumber);
                model.ProjectBillsItems = GetProjectBillItems(vendorNo, ProjectNumber);
                return View(model);
            }
        }
        private static List<ProjectTasksLinesModel> GetProjectPackages(string vendorNo, string ProjectNumber)
        {

            List<ProjectTasksLinesModel> list = new List<ProjectTasksLinesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.JobTask.Where(x => x.Job_No == ProjectNumber).ToList();
                foreach (var projectslines in query)
                {
                    ProjectTasksLinesModel projectsline = new ProjectTasksLinesModel();
                    projectsline.Job_Task_No = projectslines.Job_Task_No;
                    projectsline.Job_No = projectslines.Job_No;
                    projectsline.Description = projectslines.Description;
                    projectsline.Start_Date =Convert.ToString( projectslines.Start_Date);
                    projectsline.End_Date = Convert.ToString(projectslines.End_Date);
                    projectsline.Start_Point_Km = Convert.ToString(projectslines.Start_Point_Km);
                    projectsline.End_Point_Km = Convert.ToString(projectslines.End_Point_Km);
                    projectsline.Surface_Types = Convert.ToString(projectslines.Surface_Types);
                    projectsline.Completed_Length_Km = Convert.ToString(projectslines.Completed_Length_Km);
                    projectsline.Road_Condition = projectslines.Road_Condition;
                    projectsline.Roads_Category = projectslines.Roads_Category;
                    list.Add(projectsline);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;

        }
        private static List<ProjectPlanningLinesModel> GetProjectBillItems(string vendorNo, string ProjectNumber)
        {

            List<ProjectPlanningLinesModel> list = new List<ProjectPlanningLinesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.JobPlaningLines.Where(x => x.Job_No == ProjectNumber).ToList();
                foreach (var projectslines in query)
                {
                    ProjectPlanningLinesModel projectsline = new ProjectPlanningLinesModel();
                    projectsline.Job_Task_No = projectslines.Job_Task_No;
                    projectsline.Job_No = projectslines.Job_No;
                    projectsline.No = projectslines.No;
                    projectsline.Description = projectslines.Description;
                    projectsline.Line_No = Convert.ToString(projectslines.Line_No);
                    projectsline.Quantity = Convert.ToString(projectslines.Quantity);
                    projectsline.Type = Convert.ToString(projectslines.Type);
                    projectsline.Road_Category = Convert.ToString(projectslines.Road_Activity_Categories);
                    projectsline.Unit_of_Measure = Convert.ToString(projectslines.Unit_of_Measure_Code);
                    list.Add(projectsline);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return list;

        }
        private static List<ContractorStatementModel> GetVendorsStatement(string vendorNo)
        {

            List<ContractorStatementModel> list = new List<ContractorStatementModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorStatetement.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var statetements in query)
                {
                    ContractorStatementModel statetement = new ContractorStatementModel();
                    statetement.Vendor_No = statetements.Vendor_No;
                    statetement.Document_Type = statetements.Document_Type;
                    statetement.Posting_Date = Convert.ToDateTime(statetements.Posting_Date).ToString("dd-MM-yy");
                    statetement.Description = statetements.Description;
                    statetement.Vendor_Name = statetements.Vendor_Name;
                    statetement.Amount = Convert.ToString(statetements.Amount);
                    statetement.Remaining_Amount = Convert.ToString(statetements.Remaining_Amount);
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
        private static List<ContractorIncomeStatementTModel> GetVendorIncomeStatementDetails(string vendorNo)
        {

            List<ContractorIncomeStatementTModel> incomestatementdetails = new List<ContractorIncomeStatementTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.vendorIncomestatement.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var incomestatement in query)
                {
                    ContractorIncomeStatementTModel income = new ContractorIncomeStatementTModel();
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
        private static List<ContractorsBalanceSheetTModel> GetVendorBalanaceDetails(string vendorNo)
        {

            List<ContractorsBalanceSheetTModel> balancesheetdetails = new List<ContractorsBalanceSheetTModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.vendorBalancesheet.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var balance in query)
                {
                    ContractorsBalanceSheetTModel balancesheet = new ContractorsBalanceSheetTModel();
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
        private static List<ContractorLitigationModel> GetVendorLitigationHistoryDetails(string vendorNo)
        {

            List<ContractorLitigationModel> litigationDetailsHistory = new List<ContractorLitigationModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorLitigationHistory.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var listigationhistory in query)
                {
                    ContractorLitigationModel litigations = new ContractorLitigationModel();
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
        private static List<ContractorPastExperienceModel> GetVendorPastExeprience(string vendorNo)
        {

            List<ContractorPastExperienceModel> pastexperienceDetails = new List<ContractorPastExperienceModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorPastExperience.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var past in query)
                {
                    ContractorPastExperienceModel pastexperience = new ContractorPastExperienceModel();
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
                    pastexperienceDetails.Add(pastexperience);
                }


            }
            catch (Exception e)
            {

                throw;
            }

            return pastexperienceDetails;
        }
        private static List<ContractorPrequalifiedCategoriesModel> GetPrequalificationHistory(string vendorNo)
        {

            List<ContractorPrequalifiedCategoriesModel> prequalificationDetails = new List<ContractorPrequalifiedCategoriesModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorPrequalificationEntries.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var preferences in query)
                {
                    ContractorPrequalifiedCategoriesModel preference = new ContractorPrequalifiedCategoriesModel();
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
        private static List<ContractorProfileModel> GetVendors(string vendorNo)
        {

            List<ContractorProfileModel> vendorsDetails = new List<ContractorProfileModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.eProVendorQT.Where(x => x.No == vendorNo).ToList();
                foreach (var vendors in query)
                {
                    ContractorProfileModel vendor = new ContractorProfileModel();
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
            catch (Exception e)
            {

                throw;
            }

            return vendorsDetails;
        }
        private static List<ContractorDirectorsModel> GetStakeholders(string vendorNo)
        {

            List<ContractorDirectorsModel> DirectorDetails = new List<ContractorDirectorsModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorShareholderDetails.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var shareholders in query)
                {
                    ContractorDirectorsModel shareholder = new ContractorDirectorsModel();
                    shareholder.Fullname = shareholders.Name;
                    shareholder.OwnershipPercentage = shareholders.Entity_Ownership;
                    shareholder.Phonenumber = shareholders.Phone_No;
                    shareholder.Address = shareholders.Address;
                    shareholder.Email = shareholders.E_Mail;
                    shareholder.IdNumber = shareholders.ID_Passport_No;
                    shareholder.CitizenshipType = shareholders.Citizenship_Type;
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
        private static List<ContractorBanksModel> GetBanks(string vendorNo)
        {

            List<ContractorBanksModel> BankDetails = new List<ContractorBanksModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var query = nav.VendorBankAccounts.Where(x => x.Vendor_No == vendorNo).ToList();
                foreach (var banks in query)
                {
                    ContractorBanksModel bank = new ContractorBanksModel();
                    bank.BankCode = banks.Code;
                    bank.BankName = banks.Name;
                    bank.BankAccountNo = banks.Bank_Account_No;
                    bank.CurrencyCode = banks.Currency_Code;
                    bank.Contact = banks.Contact;
                    bank.Phone_No = banks.Phone_No;
                    bank.Bank_Branch_No = banks.Bank_Branch_No;
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
        public ActionResult ActiveTenderNotices()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ActiveContracts> list = new List<ActiveContracts>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Tender_Name != "").ToList();
                    foreach (var tenders in query)
                    {
                        ActiveContracts tender = new ActiveContracts();
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
        public ActionResult ProjectsViewLists()
        {
            List<ProjectListModel> ProjectDetails = new List<ProjectListModel>();
            try
            {
                var nav = NavConnection.ReturnNav();
                var vendorNo = Session["vendorNo"].ToString();
                var query = nav.jobs.Where(x=>x.Contractor_No == vendorNo&&x.Status=="Open").ToList();
                foreach (var projects in query)
                {
                    ProjectListModel project = new ProjectListModel();
                    project.No = projects.No;
                    project.Description = projects.Description;
                    ProjectDetails.Add(project);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return View(ProjectDetails);
        }
        public ActionResult DraftMeasurementsSheets()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<MeasurementSheetModel> list = new List<MeasurementSheetModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.MeasurementsandPayments.Where(x => x.Contractor_No == vendorNo && x.Status == "Open" && x.Portal_Status == "Draft").ToList();
                    foreach (var measurements in query)
                    {
                        MeasurementSheetModel measurement = new MeasurementSheetModel();
                        measurement.Document_Date =Convert.ToString(measurements.Document_Date);
                        measurement.Documents_No = measurements.Document_No;
                        measurement.Project_ID = measurements.Project_ID;
                        measurement.Description = measurements.Description;
                        measurement.Works_Start_Chainage =Convert.ToString(measurements.Works_Start_Chainage);
                        measurement.Contractor_No = measurements.Contractor_No;
                        measurement.Contractor_Name = measurements.Contractor_Name;
                        measurement.Works_End_Chainage = Convert.ToString(measurements.Works_End_Chainage);
                        measurement.Status = measurements.Status;
                        measurement.Project_Name = measurements.Project_Name;
                        measurement.Road_Section_No = measurements.Road_Section_No;
                        measurement.Project_Start_Date = Convert.ToString(measurements.Project_Start_Date);
                        measurement.Region_ID = measurements.Region_ID;
                        measurement.Directorate_ID = measurements.Directorate_ID;
                        measurement.Constituency_ID = measurements.Constituency_ID;
                        list.Add(measurement);

                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult SubmittedMeasurementsSheets()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<MeasurementSheetModel> list = new List<MeasurementSheetModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.MeasurementsandPayments.Where(x => x.Contractor_No == vendorNo && x.Status == "Open" &&x.Portal_Status== "Submitted").ToList();
                    foreach (var measurements in query)
                    {
                        MeasurementSheetModel measurement = new MeasurementSheetModel();
                        measurement.Document_Date = Convert.ToString(measurements.Document_Date);
                        measurement.Documents_No = measurements.Document_No;
                        measurement.Project_ID = measurements.Project_ID;
                        measurement.Project_ID = measurements.Project_ID;
                        measurement.Description = measurements.Description;
                        measurement.Works_Start_Chainage = Convert.ToString(measurements.Works_Start_Chainage);
                        measurement.Contractor_No = measurements.Contractor_No;
                        measurement.Contractor_Name = measurements.Contractor_Name;
                        measurement.Works_End_Chainage = Convert.ToString(measurements.Works_End_Chainage);
                        measurement.Status = measurements.Status;
                        measurement.Project_Name = measurements.Project_Name;
                        measurement.Road_Section_No = measurements.Road_Section_No;
                        measurement.Project_Start_Date = Convert.ToString(measurements.Project_Start_Date);
                        measurement.Region_ID = measurements.Region_ID;
                        measurement.Directorate_ID = measurements.Directorate_ID;
                        measurement.Constituency_ID = measurements.Constituency_ID;
                        list.Add(measurement);

                    }


                }
                catch (Exception ex)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult ApprovedMeasurementsSheets()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<MeasurementSheetModel> list = new List<MeasurementSheetModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.MeasurementsandPayments.Where(x => x.Contractor_No == vendorNo && x.Status == "Released" && x.Portal_Status == "Submitted").ToList();
                    foreach (var measurements in query)
                    {
                        MeasurementSheetModel measurement = new MeasurementSheetModel();
                        measurement.Document_Date = Convert.ToString(measurements.Document_Date);
                        measurement.Documents_No = measurements.Document_No;
                        measurement.Project_ID = measurements.Project_ID;
                        measurement.Project_ID = measurements.Project_ID;
                        measurement.Description = measurements.Description;
                        measurement.Works_Start_Chainage = Convert.ToString(measurements.Works_Start_Chainage);
                        measurement.Contractor_No = measurements.Contractor_No;
                        measurement.Contractor_Name = measurements.Contractor_Name;
                        measurement.Works_End_Chainage = Convert.ToString(measurements.Works_End_Chainage);
                        measurement.Status = measurements.Status;
                        measurement.Project_Name = measurements.Project_Name;
                        measurement.Road_Section_No = measurements.Road_Section_No;
                        measurement.Project_Start_Date = Convert.ToString(measurements.Project_Start_Date);
                        measurement.Region_ID = measurements.Region_ID;
                        measurement.Directorate_ID = measurements.Directorate_ID;
                        measurement.Constituency_ID = measurements.Constituency_ID;
                        list.Add(measurement);

                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult FiledOrders()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<OrderToCommenceModel> list = new List<OrderToCommenceModel>();
                try
                {
                    var vendorNo = Session["vendorNo"].ToString();
                    var nav = NavConnection.ReturnNav();
                    var query = nav.CommencementOrders.Where(x => x.Contractor_No == vendorNo).ToList();
                    foreach (var orders in query)
                    {
                        OrderToCommenceModel order = new OrderToCommenceModel();
                        order.Notice_No = orders.Notice_of_Award_No;
                        order.Document_Date = Convert.ToString(orders.Document_Date);
                        order.Purchase_Contract_ID = orders.Purchase_Contract_ID;
                        order.Project_ID = orders.Project_ID;
                        order.Description = orders.Description;
                        order.Staff_Appointment_Voucher_No = orders.Staff_Appointment_Voucher_No;
                        order.Contractor_No = orders.Contractor_No;
                        order.Contractor_Name = orders.Contractor_Name;
                        order.Vendor_Address = orders.Vendor_Address;
                        order.Vendor_Address_2 = orders.Vendor_Address_2;
                        order.Vendor_Post_Code = orders.Vendor_Post_Code;
                        order.Status = orders.Status;
                        order.Vendor_City = orders.Vendor_City;
                        order.Primary_E_mail = orders.Primary_E_mail;
                        order.IFS_Code = orders.IFS_Code;
                        order.Tender_Name = orders.Tender_Name;
                        order.Project_Name = orders.Project_Name;
                        order.Road_Code = orders.Road_Code;
                        order.Road_Section_No = orders.Road_Section_No;
                        order.Section_Name = orders.Section_Name;
                        order.Project_Start_Date = Convert.ToString(orders.Project_Start_Date);
                        order.Region_ID = orders.Region_ID;
                        order.Directorate_ID = orders.Directorate_ID;
                        order.Constituency_ID = orders.Constituency_ID;
                        list.Add(order);

                    }


                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);

            }
        }
        public ActionResult ContractorDashboard()
        {
            if (Session["vendorNo"] != null)
            {
                List<ProjectTasksModel> list = new List<ProjectTasksModel>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.JobTask.ToList();
                    foreach (var projecttaks in query)
                    {
                        ProjectTasksModel projecttask = new ProjectTasksModel();
                        projecttask.Job_No = projecttaks.Job_No;
                        projecttask.Description = projecttaks.Description;
                        projecttask.Department_Code = projecttaks.Department_Code;
                        projecttask.Directorate_Code = projecttaks.Directorate_Code;
                        projecttask.Division = projecttaks.Division;
                        projecttask.Commitments = Convert.ToString(projecttaks.Commitments);
                        projecttask.Start_Point_Km = Convert.ToString(projecttaks.Start_Point_Km);
                        projecttask.End_Point_Km = Convert.ToString(projecttaks.End_Point_Km);
                        projecttask.Start_Date = Convert.ToString(projecttaks.Start_Date);
                        projecttask.End_Date = Convert.ToString(projecttaks.End_Date);
                        projecttask.Funding_Source = projecttaks.Funding_Source;
                        projecttask.Procurement_Method = projecttaks.Procurement_Method;
                        projecttask.Surface_Types = projecttaks.Surface_Types;
                        projecttask.Road_Condition = projecttaks.Road_Condition;
                        projecttask.Completed_Length_Km = Convert.ToString(projecttaks.Completed_Length_Km);
                        projecttask.Roads_Category = projecttaks.Roads_Category;
                        projecttask.Fund_Type = projecttaks.Fund_Type;
                        projecttask.Execution_Method = projecttaks.Execution_Method;
                        projecttask.Region = projecttaks.Global_Dimension_1_Code;
                        projecttask.Constituency = projecttaks.Global_Dimension_2_Code;
                        projecttask.Execution_Method = projecttaks.Execution_Method;
                        list.Add(projecttask);
                    }

                }
                catch (Exception e)
                {

                    throw;
                }
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Contractor");
            }
        }
        public ActionResult PlannedProjects()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ActiveContracts> list = new List<ActiveContracts>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Tender_Name != "").ToList();
                    foreach (var tenders in query)
                    {
                        ActiveContracts tender = new ActiveContracts();
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
        public ActionResult CompletedProjects()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                var vendorNo = Session["vendorNo"].ToString();
                dynamic model = new ExpandoObject();
                model.OngoingWorks = GetOngoingProjectsWorks(vendorNo);
                model.CompletedWorks = GetCompletedProjectsWorks(vendorNo);
                return View(model);
            }
        }

        public ActionResult UpcomingProjects()
        {
            if (Session["vendorNo"] == null)
            {
                return RedirectToAction("Login", "Contractor");
            }
            else
            {
                List<ActiveContracts> list = new List<ActiveContracts>();
                try
                {
                    var nav = NavConnection.ReturnNav();
                    var query = nav.invitetoTenders.Where(x => x.Published == true && x.Tender_Name != "").ToList();
                    foreach (var tenders in query)
                    {
                        ActiveContracts tender = new ActiveContracts();
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
        // GET: Contractor/Create
        public ActionResult Create()
        {
            return View();
        }

   
        // GET: Contractor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

     
        // GET: Contractor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contractor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Microsoft.SharePoint.Client.FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ResetPassword()
        {

            return View();
        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
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
                    var user = nav.PortalUsers.Where(x => x.Authentication_Email == model.Email && x.Password_Value == model.Password).ToList();
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

                        }
                        //check if the contact is registered in the vendor table
                        var vendor = nav.eProVendorQT.ToList();
                        var vendorDetails = (from a in vendor where a.No == (string)Session["vendorNo"] select a).ToList();
                        if (result.State == "Enabled")
                        {
                            foreach (var vendordetail in vendorDetails)
                            {
                                Session["vendorNo"] = vendordetail.No;
                                Session["vendorName"] = vendordetail.Name;
                                Session["userNo"] = vendordetail.No;
                                Session["vatNumber"] = vendordetail.VAT_Registration_No;
                            }
                            return RedirectToAction("ContractorDashboard", "Contractor");
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
