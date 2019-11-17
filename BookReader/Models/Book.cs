using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReader.Models
{
    /// <summary>
    /// 小说
    /// </summary>
    public class Book
    {
        /// <summary>
        /// 小说名称
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// 小说链接
        /// </summary>
        public string BookHref { get; set; }

        /// <summary>
        /// 章节列表
        /// </summary>
        public List<Chapter> Chapters { get; set; }
    }
}
