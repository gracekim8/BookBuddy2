using BookBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace BookBuddy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        public IActionResult LoginSubmit(LoginModel loginUser)
        {

            string un = loginUser.Username;
            string pw = loginUser.Password;
            bool hasAccess = false;

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
            query = "select password, userID from User where password = @User_Password and username = @User_Username";
            cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@User_Username", loginUser.Username);
            cmd.Parameters.AddWithValue("@User_Password", loginUser.Password);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (pw == reader.GetString(0))
                {
                    loginUser.Username = un;
                    loginUser.Password = pw;
                    LibraryModel.Username = un;
                    LibraryModel.UserID = Convert.ToInt32(reader[1]);
                    hasAccess = true;
                }
            }

            conn.Close();

            if (hasAccess == true)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }
        }

        /*
        public ActionResult setName(LoginModel loginUser)
        {
            string un = loginUser.Username;

            string server = "localhost";
            string database = "ProgramEval";
            string username = "root";
            string password = "GracePW";

            string connstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

            string query;

            MySqlConnection conn = new MySqlConnection(connstring);
            MySqlCommand cmd;
            MySqlDataReader reader;

            conn.Open();
            query = "select Stu_Name from student where Stu_Username = @Stu_Username";
            cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Stu_Username", un);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                UserModel.Name = reader.GetString(0);
            }

            return View("Index");
        }

        */


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
