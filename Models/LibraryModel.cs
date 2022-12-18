namespace BookBuddy.Models
{
    public class LibraryModel
    {
        public static int UserID { get; set; }
        public static string Username { get; set; }
        public static List<string> BookISBN { get; set; }
        public static List<string> BookTitle { get; set; }

        public static List<string> BookAuthor { get; set; }

        public static List<string> BookGenre { get; set; }

        public static List<string> BookLocation { get; set; }

        public static List<string> BookRating { get; set; }

        public static List<bool> ReadStatus { get; set; }

        public static int BookCount;

    }
}
