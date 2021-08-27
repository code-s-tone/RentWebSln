﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [StringLength(30, MinimumLength = 10 , ErrorMessage = "信箱至少10個字元,最多30字元")]
        [DataType(DataType.EmailAddress , ErrorMessage ="請輸入正確的電子信箱")]
        public string MemberEmail { get; set; }
        [Required]
        [Display(Name = "確認電子信箱")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "信箱至少10個字元,最多30字元")]
        [Compare("MemberEmail", ErrorMessage = "電子信箱不一致")]
        public string ComfirMemberEmail { get; set; }
        public string MemberPasswordHash { get; set; }
        public IEnumerable<MemberOrderDetailViewModel> MemberOrderDetail { get; set; }
    }


    public class MemberOrderDetailViewModel
    {
        public string BranchName { get; set; }

        //ExpirationDate - RentDate = RentDate
        public int RentDate { get; set; }
        public int TotalAmount { get; set; }

        public DateTime StartDate { get; set; }
        //public string StartDate { get; set; }

        public string ProductName { get; set; }

        public DateTime ExpirationDate { get; set; }
        //public string ExpirationDate { get; set; }
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
}