using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookReader.IServices;

namespace BookReader.Services
{
    public class CommonService : ICommonService
    {
        /// <summary>
        /// 获取html内容
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> GetHtmlSourceCodeAsync(string uri)
        {
            using var httpClient = new HttpClient();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                var htmlSource = await httpClient.GetStringAsync(uri);
                return htmlSource;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
