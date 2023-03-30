using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Entities.Validator
{
    public static class CustomValidator
    {
        public async static Task<ValidatorResponse> ValidateModelAsync<TModel, TValidator>(this TModel model, TValidator validator)
            where TModel : class
            where TValidator : AbstractValidator<TModel>
        {
            var result = await validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return new ValidatorResponse { Errors = errors, IsError = true };
            }
            return new ValidatorResponse();
        }

        public static ValidatorResponse ValidateModel<TModel, TValidator>(this TModel model, TValidator validator)
        where TModel : class
        where TValidator : AbstractValidator<TModel>
        {
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                return new ValidatorResponse { Errors = errors, IsError = true };
            }
            return new ValidatorResponse();
        }
    }
}
