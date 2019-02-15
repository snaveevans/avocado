using System.Collections.Generic;
using Avocado.Domain.Entities;
using Avocado.Infrastructure.Enumerations;
using Avocado.Infrastructure.Authentication;
using System.Linq;
using Avocado.Domain.Interfaces;
using System;
using System.Linq.Expressions;

namespace Avocado.Infrastructure.Specifications
{
    public class FindLogin : ISpecification<Login>
    {
        private readonly string _provider;
        private readonly string _providerId;

        public FindLogin(Providers provider, string providerId)
        {
            _provider = provider.ToString();
            _providerId = providerId;
        }

        public Expression<Func<Login, bool>> BuildExpression() => login => login.Provider == _provider && login.ProviderId == _providerId;
    }
}
