using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Features.Queries.Providers.GetAllProviders
{
    public class GetProvidersQueryRequest : IRequest<GetProvidersQueryResponse>
    {
    }
}
