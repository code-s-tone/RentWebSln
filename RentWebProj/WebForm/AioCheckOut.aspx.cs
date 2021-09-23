using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECPay.Payment.Integration;

//訂單產生
namespace AioCheckOut
{
    public partial class AioCheckOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            List<string> enErrors = new List<string>();
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    /* 服務參數 */
                    oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
                    oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
                    oPayment.HashKey = "5294y06JbISpM5x9";//ECPay提供的Hash Key
                    oPayment.HashIV = "v77hoKGq4kWxNNIS";//ECPay提供的Hash IV
                    oPayment.MerchantID = "2000132";//ECPay提供的特店編號

                    /* 基本參數 */
                    oPayment.Send.ReturnURL = "https://localhost:44399/";//付款完成通知回傳的網址
                    oPayment.Send.ClientBackURL = "https://localhost:44399/";//瀏覽器端返回的廠商網址
                    oPayment.Send.OrderResultURL = "https://localhost:44399/WebForm/CheckOutFeedback.aspx";//瀏覽器端回傳付款結果網址
                    oPayment.Send.MerchantTradeNo = new Random().Next(0, 99999).ToString("00000")+Session["OrderID"];//廠商的交易編號
                    oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間
                    oPayment.Send.TotalAmount = Decimal.Parse(Session["TotalAmount"].ToString()); //交易總金額
                    oPayment.Send.TradeDesc = "交易描述";//交易描述
                    oPayment.Send.ChoosePayment = PaymentMethod.Credit;//使用的付款方式
                    oPayment.Send.Remark = "";//備註欄位
                    oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;//使用的付款子項目
                    oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.Yes;//是否需要額外的付款資訊
                    oPayment.Send.DeviceSource = DeviceType.PC;//來源裝置
                    oPayment.Send.IgnorePayment = ""; //不顯示的付款方式
                    oPayment.Send.PlatformID = "";//特約合作平台商代號
                    oPayment.Send.CustomField1 = "";
                    oPayment.Send.CustomField2 = "";
                    oPayment.Send.CustomField3 = "";
                    oPayment.Send.CustomField4 = "";
                    oPayment.Send.EncryptType = 1;

                    //訂單的商品資料
                    oPayment.Send.Items.Add(new Item()
                    {
                        Name = "知租網測試",//商品名稱
                        Price = Decimal.Parse(Session["TotalAmount"].ToString()),//商品單價
                        Currency = "新台幣",//幣別單位
                        Quantity = Int32.Parse("1")//購買數量
                    });
 
                    /* 產生訂單 */
                    enErrors.AddRange(oPayment.CheckOut());
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
            }
            finally
            {
                // 顯示錯誤訊息。
                if (enErrors.Count() > 0)
                {
                    //string szErrorMessage = String.Join("\\r\\n", enErrors);
                    this.Response.Write("請盡速通知客服人員");
                }
            }
        }
    }
}