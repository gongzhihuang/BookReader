using System;
using System.Threading.Tasks;

namespace BookReader.IServices
{
    public interface ICommonService
    {
        /// <summary>
        /// 获取html内容
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<string> GetHtmlSourceCodeAsync(string uri);
    }
}
