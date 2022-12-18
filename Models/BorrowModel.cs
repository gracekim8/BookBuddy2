namespace BookBuddy.Models
{
    public class BorrowModel
    {
        public static List<string> lendName { get; set; }

        public static List<string> lendTitle { get; set; }
        public static List<string> lendDate { get; set; }
        public static List<string> lendCondition { get; set; }
        public static List<bool> lendReturned { get; set; }
        public static List<string> lendPhone { get; set;  }

        public static int lendCount { get; set; }


        public static List<string> borrowName { get; set; }

        public static List<string> borrowTitle { get; set; }
        public static List<string> borrowDate { get; set; }
        public static List<string> borrowCondition { get; set; }
        public static List<bool> borrowReturned { get; set; }
        public static int borrowCount { get; set; }
    }
}
