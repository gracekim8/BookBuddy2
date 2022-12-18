using BookBuddy.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBuddy.Controllers
{
    public class Edit : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditBook(string editISBN)
        {
            EditModel.editISBN = editISBN;
            setBook();
            return View();
        }

        public IActionResult EditSubmit()
        {
            return RedirectToAction("MyBooks", "Library");
        }

        public IActionResult setBook()
        {
            for(int i = 0; i < LibraryModel.BookCount; i++)
            {
                if (LibraryModel.BookISBN[i] == EditModel.editISBN)
                {
                    EditModel.editTitle = LibraryModel.BookTitle[i];
                    EditModel.editAuthor = LibraryModel.BookAuthor[i];
                    EditModel.editLocation = LibraryModel.BookLocation[i];
                    EditModel.editRating = LibraryModel.BookRating[i];
                    EditModel.editRead = LibraryModel.ReadStatus[i];
                }
            }
            return View();
        }


    }
}
