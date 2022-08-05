using FluentValidation;
using FluentValidation.Validators;

namespace Goal.Seedwork.Infra.Crosscutting.Validations.Fluent.Validators
{
    internal class CnpjValidator<T> : PropertyValidator<T, string>, IPropertyValidator
    {
        public override bool IsValid(ValidationContext<T> context, string cnpj)
            => Validations.Validators.Documents.IsValidCnpj(cnpj);

        public override string Name => "CnpjValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "O Cnpj informado não é válido";
    }
}
