using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Queries.GetPermission
{
    public class GetPermissionQueryValidator : AbstractValidator<GetPermissionQuery>
    {
        public GetPermissionQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
