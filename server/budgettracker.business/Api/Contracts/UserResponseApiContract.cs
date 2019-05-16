namespace budgettracker.business.Api.Contracts
{
    public class UserResponseApiContract : IApiContract
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}