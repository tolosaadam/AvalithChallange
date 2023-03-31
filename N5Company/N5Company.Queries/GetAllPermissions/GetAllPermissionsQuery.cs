using MediatR;
using N5Company.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Queries.GetPermissions
{
    public class GetAllPermissionsQuery : IRequest<IEnumerable<Permission>>
    {
    }
}
