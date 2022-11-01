using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdobeSignDemo.Models
{
    public class AgreementModel
    {
        public fileInfos fileInfos { get; set; }

        public participantSetsInfo participantSetsInfo { get; set; }

        public string name { get; set; }

        public string signatureType { get; set; }

        public string state { get; set; }
    }
    public class fileInfos 
    {
        public string transientDocumentId { get; set; }
    }
    public class participantSetsInfo 
    {
        public memberInfos memberInfos { get; set; }

        public int order { get; set; }

        public string role { get; set; }
    }
    public class memberInfos 
    {
        public string email { get; set; }
    }
}