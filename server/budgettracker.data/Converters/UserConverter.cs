using budgettracker.data.Models;
using budgettracker.common.Models;
using System.Collections.Generic;

namespace budgettracker.data.Converters
{
    public class UserConverter : IConverter<User, UserModel>
    {
        public UserModel ToDataModel(User businessModel)
        {
            UserModel model = new UserModel() {
                Id = businessModel.Id,
                FirstName = businessModel.FirstName,
                LastName = businessModel.LastName,
                UserName = businessModel.Username,
                Email = businessModel.Email,
                PassWord = businessModel.Password
            };
            return model;
        }

        public List<UserModel> ToDataModels(List<User> businessModels)
        {
            List<UserModel> users = new List<UserModel>();
            foreach (User model in businessModels)
            {
                users.Add(ToDataModel(model));
            }
            return users;
        }

        public User ToBusinessModel(UserModel dataModel)
        {
            User model = new User() {
                Id = dataModel.Id,
                FirstName = dataModel.FirstName,
                LastName = dataModel.LastName,
                Username = dataModel.UserName,
                Email = dataModel.Email,
                Password = dataModel.PassWord
            };
            return model;
        }

        public List<User> ToBusinessModels(List<UserModel> dataModels)
        {
            List<User> users = new List<User>();
            foreach (UserModel model in dataModels)
            {
                users.Add(ToBusinessModel(model));
            }
            return users;
        }
    }
}
