using System.Globalization;
using CanDatabase.Common.Constants;
using CanDatabase.Domain.Models;
using FluentValidation;

namespace CanDatabase.Application.CanDbs.Commands.ParseDbcFile
{
    /// <summary>
    /// ParseDbcFileValidator
    /// </summary>
    public class ParseDbcFileValidator : AbstractValidator<ParseDbcFileCommand>
    {
        public ParseDbcFileValidator()
        {
            RuleFor(parseDbcFileCommand => parseDbcFileCommand.OriginalFileName)
                .MaximumLength(CanDb.OriginalFileNameMaxLength)
                .Must(originalFileName => originalFileName
                    .EndsWith(
                        value: FileExtensionConstants.Dbc,
                        ignoreCase: true,
                        culture: CultureInfo.InvariantCulture
                    )
                );
        }
    }
}
