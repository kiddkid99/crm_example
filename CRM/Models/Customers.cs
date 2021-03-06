//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Customers
    {
        public Customers()
        {
            this.Contacts = new HashSet<Contacts>();
            this.Supply = new HashSet<Supply>();
        }

        [Display(Name = "客戶代號")]
        public int CustomerID { get; set; }

        [Display(Name = "公司名稱/姓名")]
        [Required(ErrorMessage = "請輸入公司名稱/姓名。")]
        public string CompanyName { get; set; }

        [Display(Name = "統一編號/身份證字號")]
        [Required(ErrorMessage = "請輸入統一編號/身份證字號。")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "負責人姓名")]
        [Required(ErrorMessage = "請輸入負責人姓名。")]
        public string ManagerName { get; set; }

        [Display(Name = "公司電話")]
        public string Phone { get; set; }

        [Display(Name = "公司電子信箱")]
        public string Email { get; set; }

        [Display(Name = "公司傳真")]
        public string Fax { get; set; }

        [Display(Name = "公司地址/戶籍地")]
        public string Address { get; set; }

        [Display(Name = "匯款戶名")]
        public string AccountName { get; set; }

        [Display(Name = "匯款帳號")]
        public string AccountNumber { get; set; }

        [Display(Name = "銀行")]
        public string Bank { get; set; }

        [Display(Name = "銀行代碼")]
        public string BankCode { get; set; }

        [Display(Name = "分行")]
        public string Branch { get; set; }

        [Display(Name = "分行代碼")]
        public string BranchCode { get; set; }

        public virtual ICollection<Contacts> Contacts { get; set; }
        public virtual ICollection<Supply> Supply { get; set; }
    }
}
