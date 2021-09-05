using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    public class CommentViewModel
    {
        // 練習抓取 <留言資料> 顯示到-首頁 <名駿>

        public int MemberID { get; set; }

        // 為了顯示人名 join到 資料庫<Member>資料表
        public string MemberName { get; set; }

        public int Score { get; set; }

        public DateTime Time { get; set; }

        public string Message { get; set; }

        public string PhotoUrl { get; set; }

    }
}