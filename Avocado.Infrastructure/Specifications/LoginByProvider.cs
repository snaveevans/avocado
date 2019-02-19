using System.Collections.Generic;
using Avocado.Domain.Entities;
using Avocado.Infrastructure.Authentication;
using System.Linq;
using Avocado.Domain.Interfaces;
using System;
using System.Linq.Expressions;

namespace Avocado.Infrastructure.Specifications
{
    public class LoginByProvider : ISpecification<Login>
    {
        private readonly string _provider;
        private readonly string _providerId;

        public LoginByProvider(string provider, string providerId)
        {
            _provider = provider;
            _providerId = providerId;
        }

        public Expression<Func<Login, bool>> BuildExpression() => login => login.Provider == _provider && login.ProviderKey == _providerId;
    }
}
