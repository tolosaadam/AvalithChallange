using MediatR;
using N5Company.Entities.Models;
using N5Company.Entities.Validator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Queries.GetPermission
{
    public class GetPermissionQuery : IRequest<Permission>
    {
        public int Id { get; private set; }

        public GetPermissionQuery(int id)
        {
            Id = id;
        }

        public async Task<ValidatorResponse> Validate() => await CustomValidator.ValidateModelAsync(this, new GetPermissionQueryValidator());
    }
}
