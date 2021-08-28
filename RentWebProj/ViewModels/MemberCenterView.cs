using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    public class MemberPersonDataViewModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public DateTime MemBerBirthday { get; set; }
        public string MemberPhone { get; set; }
        public string MemberBranchName { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPasswordHash { get; set; }
        //public IEnumerable<MemberOrderDetailViewModel> OrderDetail {get;set;}
    }


    public class MemberOrderDetailViewModel
    {
        public string BranchName { get; set; }

        //ExpirationDate - RentDate = RentDate
        public DateTime RentDate { get; set; }

        public int TotalAmount { get; set; }
        public DateTime StartDate { get; set; }
        public string ProductName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int DailyRate { get; set; }

        //public int Available { get; set; }
        //public int TotalAmount { get; set; }
    }
    public class MemberLoginDetailViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "信箱不得為空白,至少6個字元最多15個字元")]
        [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的電子信箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "密碼不得為空白,至少6個字元最多15個字元")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

    public class MemberRegisterDetailViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "信箱不得為空白,至少6個字元最多15個字元")]
        [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的電子信箱")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "密碼不得為空白,至少6個字元最多15個字元")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "確認密碼")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "密碼不得為空白,至少6個字元最多15個字元")]
        [Compare("Password", ErrorMessage = "密碼不一致")]
        public string ConfirmPassword { get; set; }

    }
    public class MemberGoogleLoginDetailViewModel
    {
        public string UserID { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
    }
    public class MemberLineLoginTokenViewModel
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string id_token { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
    }
    public class MemberLineProfileTokenViewModel
    {
        public string userId { get; set; }
        public string displayName { get; set; }
        public string pictureUrl { get; set; }
        public string statusMessage { get; set; }
        public string iss { get; set; }
        public string sub{ get; set; }
        public string aud { get; set; }

        public string exp { get; set; }
        public string iat { get; set; }

        public string nonce { get; set; }

        public string[] amr { get; set; }
        public string name { get; set; }

        public string picture { get; set; }

        public string email { get; set; }
    }
}