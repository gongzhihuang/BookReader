using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReader.Models
{
    /// <summary>
    /// 章节
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// 章节名称
        /// </summary>
        public string ChapterName { get; set; }

        /// <summary>
        /// 章节链接
        /// </summary>
        public string ChapterHref { get; set; }

        /// <summary>
        /// 章节内容
        /// </summary>
        public string ChapterContent { get; set; }
    }
}
