using CanDatabase.Application.CanDbs.Queries.GetCanDbs;
using CanDatabase.Common.Constants;
using CanDatabase.Domain.Models;
using FluentValidation;

namespace CanDatabase.Application.CanDbs.Commands.ParseDbcFile
{
    /// <summary>
    /// ParseDbcFileValidator
    /// </summary>
    public class GetCanDbsValidator : AbstractValidator<GetCanDbsQuery>
    {
        public GetCanDbsValidator()
        {
            RuleFor(getCanDbsQuery => getCanDbsQuery.Take)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(getCanDbsQuery => getCanDbsQuery.Skip)
                .GreaterThanOrEqualTo(0);
        }
    }
}
