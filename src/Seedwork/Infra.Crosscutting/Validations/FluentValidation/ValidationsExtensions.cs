using Goal.Seedwork.Infra.Crosscutting.Validations.Validators;

namespace FluentValidation
{
    public static class ValidationsExtensions
    {
        public static IRuleBuilderOptions<T, string> Cnpj<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new CnpjValidator<T>());
        public static IRuleBuilderOptions<T, string> Cpf<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new CpfValidator<T>());
    }
}
