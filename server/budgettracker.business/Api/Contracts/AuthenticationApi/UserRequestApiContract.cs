namespace budgettracker.business.Api.Contracts.AuthenticationApi
{
    public class UserRequestApiContract : IApiContract
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}