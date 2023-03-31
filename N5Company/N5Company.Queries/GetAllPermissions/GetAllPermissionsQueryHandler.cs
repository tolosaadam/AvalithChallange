using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using N5Company.Entities.Models;
using N5Company.Repositories.Interfaces;
using N5Company.Repositories;

namespace N5Company.Queries.GetPermissions
{
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, IEnumerable<Permission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPermissionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Permission>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetRepository<Permission>().ListAsync();
        }
    }
}
