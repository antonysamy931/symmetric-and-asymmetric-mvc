//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileWithEncryption.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FileUploadDB
    {
        public int asymfileid { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public byte[] filedata { get; set; }
        public string symmentryKey { get; set; }
        public string asymprivatepublicKey { get; set; }
    }
}