using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    //個人資料
    public class MemberPersonDataViewModel
    {
        public int MemberId { get; set; }
        [Required]
        [Display(Name = "確認姓名")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "姓名至少2個字元,最多20字元")]
        public string MemberName { get; set; }
        public DateTime MemBerBirthday { get; set; }

        public string MemberYear { get; set; }
        public string MemberMonth { get; set; }
        public string MemberDay { get; set; }

        public string MemberPhone { get; set; }
        public string MemberBranchName { get; set; }
        [Required]
        [Display(Name = "電子信箱")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "信箱至少10個字元,最多30字元")]
        [DataType(DataType.EmailAddress, ErrorMessage = "請輸入正確的電子信箱")]
        public string MemberEmail { get; set; }

        [Required]
        [Display(Name = "確認電子信箱")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "信箱至少10個字元,最多30字元")]
        [Compare("MemberEmail", ErrorMessage = "輸入電子信箱不一致")]
        public string ComfirMemberEmail { get; set; }

        [Required]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "密碼至少6個字元,最多30字元")]
        //[DataType(DataType.Password, ErrorMessage = "請輸入正確的密碼")]
        public string MemberPasswordHash { get; set; }

        [Required]
        [Display(Name = "確認密碼")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "密碼至少6個字元,最多30字元")]
        [Compare("MemberPasswordHash", ErrorMessage = "輸入密碼不一致")]
        public string ComfigMemberPasswordHash { get; set; }

        //測試中
        public List<MemberOrderViewModel> MemberOrders { get; set; }
    }

    //訂單資訊
    public class MemberOrderViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int DeliverID { get; set; }
        public OrderStatusName OrderStatus { get; set; }
        public string BranchName { get; set; }
        public List<MemberOrderDetailViewModel> OrderDetails { get; set; }
    }

    public enum OrderStatusName
    {
        已取消,待付款,付款中,已付款
    }

    public class MemberOrderDetailViewModel
    {
        public string ProductName { get; set; }
        //public string ProductImgSrc { get; set; }
        public int DailyRate { get; set; }
        public int TotalAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        //ExpirationDate - StartDate = RentDate
        public int RentDate { get; set; }
        public GoodsStatusName GoodsStatus { get; set; }

        public string ProductID { get; set; }
    }
    public enum GoodsStatusName
    {
        已歸還, 待出貨, 已出貨, 已到貨, 已取貨
    }

    //修改密碼驗證使用
    public class CheckPassword
    {
        public string Password { get; set; }
    }

    public class CheckFullName
    {
        public string Name { get; set; }
    }

    public class CheckPhone
    {
        public string Phone { get; set; }
    }

    public class CheckBirthDay
    {
        public DateTime? BirthDay { get; set; }
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
        public string sub { get; set; }
        public string aud { get; set; }

        public string exp { get; set; }
        public string iat { get; set; }

        public string nonce { get; set; }

        public string[] amr { get; set; }
        public string name { get; set; }

        public string picture { get; set; }

        public string email { get; set; }
    }

    public class MemberFbProfileTokenViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class MemberProfileViewModel
    {

        public string Fullname { get; set; }

        public string ProfilePhotoUrl { get; set; }
        public string email { get; set; }
    }
    public class NOtify
    {
        public int MemberID { get; set; }
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public string ProductID { get; set; }

        public int GoodsStatus { get; set; }
        //軒：暫時加nullable 已通過編譯
        public int? Notify { get; set; }
    }
}