using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using SocialNetwork.Data.Repositories;
using SocialNetwork.OAuth.Helpers;

namespace SocialNetwork.OAuth.Configuration
{
    public class ResourceOwnerClassValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerClassValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userRepository.GetAsync(context.UserName,
                HashHelper.Sha512(context.Password + context.UserName));

            if (user != null)
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), authenticationMethod: "custom");
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credentials");
            }
        }
    }
}
