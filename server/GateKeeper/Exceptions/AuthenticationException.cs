using System;

namespace GateKeeper.Exceptions
{
    public class AuthenticationException : Exception
    {
        public static string REASON_USER_NOT_FOUND = "USER_NOT_FOUND";
        public static string REASON_WRONG_PASSWORD = "WRONG_PASSWORD";

        public string Reason { get; private set; }

        public AuthenticationException(string reason)
            : base (reason)
        {
            Reason = reason;
        }
    }
}