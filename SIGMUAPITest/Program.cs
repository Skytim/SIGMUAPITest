using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SIGMUAPITest
{
    class Program
    {
        /// <summary>
        /// 科研會員ResponseModel
        /// </summary>
        public class QueryMemberRewardResponse
        {
            /// <summary>
            /// 回傳代碼。
            /// </summary>
            public string returnCode { get; set; }

            /// <summary>
            /// 回傳訊息。
            /// </summary>
            public string returnMsg { get; set; }

            /// <summary>
            /// 會員編號。
            /// </summary>
            public string memberID { get; set; }

            /// <summary>
            /// 會員名稱
            /// </summary>
            public string userName { get; set; }

            /// <summary>
            /// 可享折扣。
            /// </summary>
            public decimal discount { get; set; }

            /// <summary>
            /// 目前可用紅利點數
            /// </summary>
            public int rewardPoints { get; set; }

            /// <summary>
            /// 即將到期紅利點數(依據紅利預計到期日來計算)
            /// </summary>
            public int expirePoints { get; set; }

            /// <summary>
            /// 促銷活動清單
            /// </summary>
            public RewardActivities[] promotionList { get; set; }
        }

        public class RewardActivities
        {
            /// <summary>
            /// 促銷活動名稱
            /// </summary>
            public string promotionName { get; set; }

            /// <summary>
            /// 兌換比例
            /// </summary>
            public int exchangeRate { get; set; }

            /// <summary>
            /// 紅利兌點上限
            /// </summary>
            public int redeemMax { get; set; }

            /// <summary>
            /// 交易金額下限
            /// </summary>
            public int tradeMin { get; set; }
        }

        public class QueryMemberRewardInfo
        {
            public string memberID { get; set; }

            public string expireDate { get; set; }

            public string checkCode { get; set; }
        }


        static void Main(string[] args)
        {
            var responseData = string.Empty;
            var httpWebRequest = HttpWebRequest.Create("http://sigmuecapi4dev.azurewebsites.net/api/QueryMemberReward");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Proxy = null;
            var oqueryMemberRewardResponse= new QueryMemberRewardResponse();
            var oqueryMemberRewardInfo = new QueryMemberRewardInfo();
            oqueryMemberRewardInfo.checkCode = "F53B1CEEDCD951694689CB2D83EE911647F8EE0F";
            oqueryMemberRewardInfo.expireDate = "2016/09/30";
            oqueryMemberRewardInfo.memberID = "22345-67891";

            var postData = JsonConvert.SerializeObject(oqueryMemberRewardInfo);
            var finalData = string.Empty;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(postData);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseData = streamReader.ReadToEnd();
                oqueryMemberRewardResponse = JsonConvert.DeserializeObject<QueryMemberRewardResponse>(responseData);
         
            }
        }
    }
}
