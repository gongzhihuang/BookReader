using AngleSharp.Html.Parser;
using BookReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReader.Services
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
        Task<List<string>> SearchBook(string bookName);

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


    public class BookService : IBookService
    {
        ICommonService _commonService = null;
        public BookService( ICommonService commonService)
        {
            _commonService = commonService;
        }

        /// <summary>
        /// 小说列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Book>> GetBooks()
        {
            var html = await _commonService.GetHtmlSourceCodeAsync("https://www.piaotian5.com/");

            List<Book> books = new List<Book>();

            if (!string.IsNullOrWhiteSpace(html))
            {
                var parser = new HtmlParser();
                var document = await parser.ParseDocumentAsync(html);

                var nameList = document.QuerySelectorAll("*")
                    .Where(x => x.TagName == "A" &&
                    x.GetAttribute("href").StartsWith("/book/") &&
                    !x.TextContent.StartsWith("第") &&
                    !string.IsNullOrWhiteSpace(x.TextContent) &&
                    x.GetAttribute("href").Length == 16).ToList();

                foreach (var item in nameList)
                {
                    Book book = new Book {
                        BookName = item.TextContent,
                        BookHref = item.GetAttribute("href")
                    };
                    books.Add(book);
                }

                return books;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 搜索小说
        /// </summary>
        /// <param name="bookName">小说名称</param>
        /// <returns></returns>
        public async Task<List<string>> SearchBook(string bookName)
        {
            string url = "https://www.piaotian5.com/s.php?ie=gbk&q=" + bookName;
            var html = await _commonService.GetHtmlSourceCodeAsync(url);

            List<string> books = new List<string>();

            if (!string.IsNullOrWhiteSpace(html))
            {
                var parser = new HtmlParser();
                var document = await parser.ParseDocumentAsync(html);

                var nameList = document.QuerySelectorAll("*")
                    .Where(x => x.TagName == "A" && x.ParentElement.ClassName == "bookname").ToList();

                foreach (var item in nameList)
                {
                    books.Add(item.TextContent + item.GetAttribute("href"));
                }

                return books;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取章节列表
        /// </summary>
        /// <param name="bookHref">小说链接</param>
        /// <returns></returns>
        public async Task<List<Chapter>> GetChapters(string bookHref)
        {
            var url = "https://www.piaotian5.com" + bookHref;
            var html = await _commonService.GetHtmlSourceCodeAsync(url);

            List<Chapter> chapters = new List<Chapter>();

            if (!string.IsNullOrWhiteSpace(html))
            {
                var parser = new HtmlParser();
                var document = await parser.ParseDocumentAsync(html);

                var nameList = document.QuerySelectorAll("*")
                    .Where(x => x.TagName == "A" && 
                    x.GetAttribute("href").StartsWith(bookHref.Substring(0,11)) && 
                    x.GetAttribute("href").Length > 18)
                    .ToList();

                foreach (var item in nameList.Skip(7).Take(nameList.Count))
                {
                    Chapter chapter = new Chapter
                    {
                        ChapterName = item.TextContent,
                        ChapterHref = item.GetAttribute("href")
                    };
                    chapters.Add(chapter);
                }

                return chapters;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取章节内容
        /// </summary>
        /// <param name="chapterHref">章节链接</param>
        /// <returns></returns>
        public async Task<string> GetChapter(string chapterHref)
        {
            var url = "https://www.piaotian5.com" + chapterHref;
            var html = await _commonService.GetHtmlSourceCodeAsync(url);

            StringBuilder stringBuilder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(html))
            {
                var parser = new HtmlParser();
                var document = await parser.ParseDocumentAsync(html);
                var nameList = document.QuerySelectorAll("div > .showtxt");

                foreach (var item in nameList)
                {
                    stringBuilder.Append(item.TextContent);
                }

                return stringBuilder.ToString();
            }
            else
            {
                return "No html source code returned.";
            }
        }
    }
}
