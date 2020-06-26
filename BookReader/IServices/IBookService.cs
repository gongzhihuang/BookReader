using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookReader.Models;

namespace BookReader.IServices
{
    public interface IBookService
    {
        /// <summary>
        /// 小说列表
        /// </summary>
        /// <returns></returns>
        Task<List<Book>> GetBooks();

        /// <summary>
        /// 搜索小说
        /// </summary>
        /// <param name="bookName">小说名称</param>
        /// <returns></returns>
        Task<List<Book>> SearchBook(string bookName);

        /// <summary>
        /// 获取章节列表
        /// </summary>
        /// <param name="bookHref">小说链接</param>
        /// <returns></returns>
        Task<List<Chapter>> GetChapters(string bookHref);

        /// <summary>
        /// 获取章节内容
        /// </summary>
        /// <param name="chapterHref">章节链接</param>
        /// <returns></returns>
        Task<string> GetChapter(string chapterHref);
    }
}
