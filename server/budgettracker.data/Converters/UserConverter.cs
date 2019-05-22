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
                UserName = businessModel.UserName,
                Email = businessModel.Email
            };
            return model;
        }

        public User ToBusinessModel(UserModel dataModel)
        {
            User model = new User() {
                FirstName = dataModel.FirstName,
                LastName = dataModel.LastName,
                UserName = dataModel.UserName,
                Email = dataModel.Email
            };
            return model;
        }
    }
}