using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.ViewModels
{
    public class ApiResponse
    {
        // 狀態 成功 or 失敗 (True / False)
        public bool IsSuccessful { get; set; }

        // 文字訊息 (成功 / 失敗)
        public string Result { get; set; }
    }
}
