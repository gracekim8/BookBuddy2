using BookBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace BookBuddy.Controllers
{
    public class Borrow : Controller
    {
        public IActionResult Borrowed()
        {
            return View();
        }

        public IActionResult SetAll()
        {
            SetLends();
            SetBorrows();
            return RedirectToAction("Borrowed");
        }

        public IActionResult BorrowHistory()
        {
            return View();
        }

        public IActionResult BorrowNew()
        {
            return View();
        }

        public IActionResult LendNew()
        {
            return View();
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

        public ActionResult SetBorrows()
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
            query = "select User.username, book.title, startDate, startCondition, returned" +
                " from Lend join Book on Book.ISBN = Lend.L_ISBN" +
                " join User on User.userID = lend.ownerID" +
                " where lend.borrowerID = 1;";

            cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@User_UserID", LibraryModel.UserID);
            reader = cmd.ExecuteReader();

            List<string> Names = new List<string>();
            List<string> Titles = new List<string>();
            List<string> Dates = new List<string>();
            List<string> Conditions = new List<string>();
            List<bool> Returned = new List<bool>();
            int count = 0;


            while (reader.Read())
            {
                Names.Add(Convert.ToString(reader[0]));
                Titles.Add(Convert.ToString(reader[1]));
                Dates.Add(Convert.ToString(reader[2]));
                Conditions.Add(Convert.ToString(reader[3]));
                Returned.Add(Convert.ToBoolean(reader[4]));
                count++;
            }

            BorrowModel.borrowName = Names;
            BorrowModel.borrowTitle = Titles;
            BorrowModel.borrowDate = Dates;
            BorrowModel.borrowCondition = Conditions;
            BorrowModel.borrowReturned = Returned;
            BorrowModel.borrowCount = count;

            conn.Close();


            return View();
        }
    }
}
