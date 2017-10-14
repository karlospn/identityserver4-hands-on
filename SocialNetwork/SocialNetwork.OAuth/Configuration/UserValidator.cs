using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SocialNetwork.Data.Models;
using SocialNetwork.Data.Repositories;
using SocialNetwork.OAuth.Helpers;

namespace SocialNetwork.OAuth.Configuration
{
    public class UserValidator: IUserValidator
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        public bool ValidateCredentials(string username, string password)
        {
            var user  = _userRepository.GetAsync(username, HashHelper.Sha512(password + username));
            return user != null;
        }

        public User FindByUsername(string username)
        {
            var user = _userRepository.GetAsync(username);
            return user.Result;
        }

        public User FindByExternalProvider(string provider, string userId)
        {
            throw new NotImplementedException();
        }

        public User AutoProvisionUser(string provider, string userId, List<Claim> claims)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserValidator
    {
        bool ValidateCredentials(string username, string password);

        User FindByUsername(string username);

        User FindByExternalProvider(string provider, string userId);

        User AutoProvisionUser(string provider, string userId, List<Claim> claims);

    }
}
