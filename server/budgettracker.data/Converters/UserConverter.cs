using budgettracker.data.Models;
using budgettracker.common.Models;

namespace budgettracker.data.Converters
{
    public class UserConverter : IConverter<User, UserModel>
    {
        public UserModel ToDataModel(User businessModel)
        {
            UserModel model = new UserModel() {
                FirstName = businessModel.FirstName,
                LastName = businessModel.LastName,
                UserName = businessModel.Username,
                Email = businessModel.Email,
                PassWord = businessModel.Password
            };
            return model;
        }

        public User ToBusinessModel(UserModel dataModel)
        {
            User model = new User() {
                FirstName = dataModel.FirstName,
                LastName = dataModel.LastName,
                Username = dataModel.UserName,
                Email = dataModel.Email,
                Password = dataModel.PassWord
            };
            return model;
        }
    }
}