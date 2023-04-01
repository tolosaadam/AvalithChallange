using N5Company.Entities.Models;
using N5Company.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Repositories.Repositories
{
    public class PermissionTypeRepository : Repository<PermissionType>, IPermissionTypeRepository
    {
        public PermissionTypeRepository(AppDbContext context) : base(context) { }
    }
}
