using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookReader.Services;

namespace BookReader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        public IBookService _bookService = null;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// 小说列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/books")]
        public async Task<IActionResult> GetBooks()
        {
            var res = await _bookService.GetBooks();
            return Ok(res);
        }

        /// <summary>
        /// 章节列表
        /// </summary>
        /// <param name="bookHref"></param>
        /// <returns></returns>
        [HttpGet("/chapters")]
        public async Task<IActionResult> GetChapters(string bookHref)
        {
            var res = await _bookService.GetChapters(bookHref);
            return Ok(res);
        }

        /// <summary>
        /// 章节内容
        /// </summary>
        /// <param name="chapterHref"></param>
        /// <returns></returns>
        [HttpGet("/chapter")]
        public async Task<IActionResult> GetChapter( string chapterHref)
        {
            var res = await _bookService.GetChapter(chapterHref);
            return Ok(res);
        }

        [HttpGet("/book")]
        public async Task<IActionResult> SearchBook(string bookName)
        {
            var res = await _bookService.SearchBook(bookName);
            return Ok(res);
        }
    }
}