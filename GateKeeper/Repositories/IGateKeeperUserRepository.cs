using GateKeeper.Models;
using System.Threading.Tasks;

namespace GateKeeper.Repositories
{
    public interface IGateKeeperUserRepository<U> where U : IUser
    {
        /// <summary>
        /// Returns the user that has the given username or null if
        /// it doesn't exist. The password on the user returned in this
        /// will be encrypted.
        /// </summary>
        Task<U> GetByUsername(string username);
    }
}
