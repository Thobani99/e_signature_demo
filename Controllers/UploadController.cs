using System.IO;
using System.Web;
using System.Web.Mvc;
using AdobeSignDemo.Models;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace FileUpload.Controllers
{
    public class UploadController : Controller
    {
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
        [HttpGet]
        public ActionResult UploadedFiles()
        {
            return View();
        }

        public FileResult Download(string filePath)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = Path.GetFileName(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [System.Obsolete]
        public ActionResult click() 
        {
            #region Getting Token 
            //var client = new RestClient("https://secure.na3.echosign.com/public/oauth/v2");

            //var request = new RestRequest("?redirect_uri=https://www.google.co.in&response_type=code&client_id=CBJCHBCAABAAgcv1a9TpTNYQpM9TtUwkb97VBK8PFXCI&scope=user_login:account+agreement_send:account", Method.POST);

            //request.AddParameter("redirect_uri", "https://www.google.co.in", ParameterType.UrlSegment);
            //request.AddParameter("code", "code", ParameterType.UrlSegment);
            //request.AddParameter("client_id", "CBNCKBAAHBCAABAASj3OO-3o6h7bBXXw438vEiAS3Od0d8h1", ParameterType.UrlSegment);
            //request.AddParameter("scope", "user_login:account+agreement_send:account", ParameterType.UrlSegment);


            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            //var queryResult = client.Execute(request);
            #endregion

            #region GET File
            var files = Directory.GetFiles(Server.MapPath("~/UploadedFiles"));
            var filePath = files[0];
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = Path.GetFileName(filePath);
            var file = File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            #endregion


            var client = new RestClient("https://api.na3.echosign.com/api/rest/v2");

            var postDocumentRequest = uploadTransientDocuments();

            var request = createAgreement();

            IRestResponse response = client.Execute(request);
            var content = response.Content;

             return Content(content);
        }

        private RestRequest uploadTransientDocuments() 
        {
            #region GET File
            var files = Directory.GetFiles(Server.MapPath("~/UploadedFiles"));
            var filePath = files[0];
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = Path.GetFileName(filePath);
            var file = File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            #endregion

            var request = new RestRequest("transientDocuments", Method.POST);

            request.AddHeader("Authorization", "Bearer 3AAABLblqZhDVCuJlOhMZn6dX5FUkT8ZfQI8KQgu65LN0RHKL59UBxsJ-uGQGOex6f9pjJ9rspm4g4YLcLv8zgjLXpp-5lvur");

            request.RequestFormat = DataFormat.Xml;

            request.AddFileBytes("File", fileBytes, fileName);

            return request;
        }

        [System.Obsolete]
        private RestRequest createAgreement()
        {
            var request = new RestRequest("agreements", Method.POST);

            request.AddHeader("Authorization", "Bearer 3AAABLblqZhDVCuJlOhMZn6dX5FUkT8ZfQI8KQgu65LN0RHKL59UBxsJ-uGQGOex6f9pjJ9rspm4g4YLcLv8zgjLXpp-5lvur");

            request.RequestFormat = DataFormat.Json;

            var agreementModel = new AgreementModel 
            {
                name = "Agree_1",
                state = "ACTIVE",
                signatureType = "ESIGN",
                participantSetsInfo = new participantSetsInfo 
                {
                    role = "SIGNER",
                    order = 1,
                    memberInfos = new memberInfos { email = "thobani.ndlela@dutappfactory.tech" }
                },
                fileInfos = new fileInfos
                {
                    transientDocumentId = "3AAABLblqZhDOGYPW4rSGiwDrHTgk9GmqqJQtyyaaIeEG7evxH16AqtJLffv7HOdnMykHBoRYL3vappUW_CA8KfpNYzl8vbRaL-aHUn88eQvDe8tCwy4rUQXkh99toujrmGMnKZoNAQgikGGA320OhpXc_IyIa_DyrM4iCssXiR8taxNQbuvJKiN3Bb8G_oEqXbUEnxBhQzOHeUz8vtXobAIwiXfDnoakWlXfVDptAVW4AUkQ2MqO128ym47YBoHrcvizWANPojwlIx3UQZx7GLNc_-Nj-cebhMmBkTrrTJpIihhSFS-4r9FFcv2n1Vzq0UYuP1-uCSQicr4bb945WuqrCIOL-s7b9S78qXA_1l5UaZUj2_oqRTCODsUEP9xNzTtbb5wyXF4*"
                }
            };

            var jsonAgreementModel = Newtonsoft.Json.JsonConvert.SerializeObject(agreementModel);
            

            //request.AddJsonBody(jsonAgreementModel);

            return request;
        }

    }
}