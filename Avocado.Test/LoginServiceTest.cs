using System;
using System.Linq;
using Avocado.Domain.Entities;
using Avocado.Infrastructure.Authorization;
using Avocado.Infrastructure.Enumerations;
using Microsoft.Extensions.Options;
using Xunit;

namespace Avocado.Test
{
    public class LoginServiceTest
    {
        private readonly LoginService _loginService;
        private readonly TestRepo<Account> _accountRepo;
        private readonly TestRepo<Login> _loginRepo;

        public LoginServiceTest()
        {
            var someOptions = Options.Create(new LoginOptions
            {
                Key = "fitfa9WI7twa29MHr8AGko2Gn3G00BYtYkaI8P5mAdrbJPPRDTKQx4HJxuZBj2DU",
                Issuer = "blah",
                MillisecondsUntilExpiration = 1
            });
            _accountRepo = new TestRepo<Account>();
            _loginRepo = new TestRepo<Login>();
            _loginService = new LoginService(someOptions, _loginRepo, _accountRepo);
        }

        [Fact]
        public void Register()
        {
            var registerModel = new RegisterModel
            {
                Provider = Providers.Google.ToString(),
                ProviderId = "12345",
                Name = "John Snow",
                Picture = "foobar"
            };

            Assert.False(_loginService.TryRegister(new RegisterModel{
                ProviderId = "12345",
                Name = "John Snow"
            }, out string token2));
            Assert.Equal(string.Empty, token2);
            Assert.False(_loginService.TryRegister(new RegisterModel{
                Provider = Providers.Google.ToString(),
                Name = "John Snow"
            }, out string token3));
            Assert.Equal(string.Empty, token3);
            Assert.False(_loginService.TryRegister(new RegisterModel{
                Provider = Providers.Google.ToString(),
                ProviderId = "12345",
            }, out string token4));
            Assert.Equal(string.Empty, token4);

            Assert.True(_loginService.TryRegister(registerModel, out string token));
            Assert.NotEqual(string.Empty, token);
            Assert.Single(_accountRepo.List);
            Assert.Single(_loginRepo.List);
            var login = _loginRepo.List.First();
            Assert.Equal(registerModel.Provider, login.Provider);
            Assert.Equal(registerModel.ProviderId, login.ProviderId);
        }

        [Fact]
        public void Login()
        {
            var registerModel = new RegisterModel
            {
                Provider = Providers.Google.ToString(),
                ProviderId = "12345",
                Name = "John Snow",
                Picture = "foobar"
            };

            Assert.False(_loginService.TryLogin(new RegisterModel{
                Provider = Providers.Google.ToString(),
                ProviderId = "12345",
            }, out string token2));
            Assert.Equal(string.Empty, token2);

            _loginService.TryRegister(registerModel, out string t);

            Assert.True(_loginService.TryLogin(registerModel, out string token));
            Assert.NotEqual(string.Empty, token);

            Assert.False(_loginService.TryLogin(new RegisterModel(), out string token3));
            Assert.Equal(string.Empty, token3);
        }
    }
}