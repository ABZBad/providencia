using System;

namespace SIP
{
    public class PublishedVersion
    {
        public DateTime ReleaseDate { get; set; }
        public string UrlNewPackage { get; set; }
        public string UrlOldPackage { get; set; }
        public string NewVersionNumber { get; set; }
        public string Installpath { get; set; }
        public string App { set; get; }
    }
}