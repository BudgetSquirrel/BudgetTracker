using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using GateKeeper.Exceptions;
using GateKeeper.Models;
using GateKeeper.Repositories;

namespace GateKeeper
{
    public class Authentication
    {
        /// <summary>
        /// Authenticates the user login. This gets the user from the repository, decrypts
        /// the stored password and determines if the un-encrypted password in passwordGuess
        /// is the same as the un-encrypted password from the user retrieved in the
        /// repository.
        /// </summary>
        public static U Authenticate<U>(string username, string passwordGuess,
            IUserRepository<U> userRepository, ICryptor cryptor,
            GateKeeperConfig gateKeeperConfig) where U : IUser
        {
            U user = userRepository.GetByUsername(username);
            if (user == null)
            {
                throw new AuthenticationException(AuthenticationException.REASON_USER_NOT_FOUND);
            }
            string realPassword = cryptor.Decrypt(user.Password, gateKeeperConfig.EncryptionKey, gateKeeperConfig.Salt);
            if (passwordGuess == realPassword)
            {
                return user;
            }
            else
            {
                throw new AuthenticationException(AuthenticationException.REASON_WRONG_PASSWORD);
            }
        }
    }
}