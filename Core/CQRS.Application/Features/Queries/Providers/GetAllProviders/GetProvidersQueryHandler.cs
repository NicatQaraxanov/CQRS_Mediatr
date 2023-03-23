using CQRS.Application.Repositories;
using CQRS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.Providers.GetAllProviders
{
    public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQueryRequest, GetProvidersQueryResponse>
    {

        private readonly IReadRepository<Provider> _providerReadRepository;

        public GetProvidersQueryHandler(IReadRepository<Provider> providerReadRepository)
        {
            _providerReadRepository = providerReadRepository;
        }

        public async Task<GetProvidersQueryResponse> Handle(GetProvidersQueryRequest request, CancellationToken cancellationToken)
        {
            var providers = _providerReadRepository.GetAll().ToList();

            return new()
            {
                Providers = providers
            };
        }
    }
}
