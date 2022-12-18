using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using BookBuddy.Models;

namespace BookBuddy.Controllers
{
    public class Library : Controller
    {
        public IActionResult MyBooks()
        {
            return View();
        }

        public IActionResult AddBook()
        {
            return View();
        }

        public IActionResult setBooks()
        {
            string server = "localhost";
            string database = "BookBuddy";
            string username = "root";
            string password = "GracePW";

            string connstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

            string query;

            MySqlConnection conn = new MySqlConnection(connstring);
            MySqlCommand cmd;
            MySqlDataReader reader;

            conn.Open();
            query = "select Book.ISBN, Book.title, Author.aLastName, Genre.genre, Own.location, Own.rating, Own.readStatus" +
                " from Book left join Wrote on Book.ISBN = Wrote.W_ISBN" +
                " left join Genre on Genre.G_ISBN = Book.ISBN" +
                " join Author on Author.authorID = Wrote.author_ID" +
                " join Own on Own.O_ISBN = Book.ISBN" +
                " where Own.userID = @User_UserID" +
                " group by Book.ISBN;";
            cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@User_UserID", LibraryModel.UserID);
            reader = cmd.ExecuteReader();

            List<string> ISBN = new List<string>();
            List<string> Title = new List<string>();
            List<string> Author = new List<string>();
            List<string> Genre = new List<string>();
            List<string> Location = new List<string>();
            List<string> Rating = new List<string>();
            List<bool> Read = new List<bool>();
            int count = 0;


            while (reader.Read())
            {
                ISBN.Add(Convert.ToString(reader[0]));
                Title.Add(Convert.ToString(reader[1]));
                Author.Add(Convert.ToString(reader[2]));
                Genre.Add(Convert.ToString(reader[3]));
                Location.Add(Convert.ToString(reader[4]));
                Rating.Add(Convert.ToString(reader[5]));
                Read.Add(Convert.ToBoolean(reader[6]));
                count++;
            }

            LibraryModel.BookISBN = ISBN;
            LibraryModel.BookTitle = Title;
            LibraryModel.BookAuthor = Author;
            LibraryModel.BookGenre = Genre;
            LibraryModel.BookLocation = Location;
            LibraryModel.BookRating = Rating;
            LibraryModel.ReadStatus = Read;
            LibraryModel.BookCount = count;

            conn.Close();

            SetLends();
            return RedirectToAction("MyBooks", "Library");
        }

        public ActionResult SetLends()
        {
            string server = "localhost";
            string database = "BookBuddy";
            string username = "root";
            string password = "GracePW";

            string connstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

            string query;

            MySqlConnection conn = new MySqlConnection(connstring);
            MySqlCommand cmd;
            MySqlDataReader reader;

            conn.Open();
            query = "select User.username, book.title, startDate, startCondition, returned, borrowerPhone" +
                " from Lend join Book on Book.ISBN = Lend.L_ISBN" +
                " join User on User.userID = lend.borrowerID" +
                " where lend.ownerID = @User_UserID;";

            cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@User_UserID", LibraryModel.UserID);
            reader = cmd.ExecuteReader();

            List<string> Names = new List<string>();
            List<string> Titles = new List<string>();
            List<string> Dates = new List<string>();
            List<string> Conditions = new List<string>();
            List<bool> Returned = new List<bool>();
            List<string> Phones = new List<string>();
            int count = 0;


            while (reader.Read())
            {
                Names.Add(Convert.ToString(reader[0]));
                Titles.Add(Convert.ToString(reader[1]));
                Dates.Add(Convert.ToString(reader[2]));
                Conditions.Add(Convert.ToString(reader[3]));
                Returned.Add(Convert.ToBoolean(reader[4]));
                Phones.Add(Convert.ToString(reader[5]));
                count++;
            }

            BorrowModel.lendName = Names;
            BorrowModel.lendTitle = Titles;
            BorrowModel.lendDate = Dates;
            BorrowModel.lendCondition = Conditions;
            BorrowModel.lendReturned = Returned;
            BorrowModel.lendPhone = Phones;
            BorrowModel.lendCount = count;

            conn.Close();


            return View();
        }
    }
}
