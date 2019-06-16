namespace budgettracker.common
{
    public class Constants
    {
        public class Authentication
        {
            public static class ApiResponseErrorCodes
            {
                public static string PASSWORD_CONFIRM_INCORRECT = "PASSWORD_CONFIRM_INCORRECT";
                public static string DUPLICATE_USERNAME = "DUPLICATE_USERNAME";
                public static string UNKNOWN = "UNKNOWN";
                public static string USER_NOT_FOUND = "USER_NOT_FOUND";
            }
        }

        public class Budget
        {
            public static class ApiResponseErrorCodes
            {
                public static string INVALID_ARGUMENTS = "INVALID_ARGUMENTS";
                public static string UNKNOWN = "UNKNOWN";
            }
        }

    }
}
