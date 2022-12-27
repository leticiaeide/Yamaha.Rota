using FluentValidation;
using Yamaha.Rota.Domain.Dominio.Rota.Arguments;

namespace Yamaha.Rota.Domain.Dominio.Rota
{
    public class RotaValidacao : AbstractValidator<RotaRequest>
    {
        public RotaValidacao()
        {
            RuleFor(x => x.Destino)
                .NotNull().WithMessage("Destino da rota deve ser informado")
                .NotEmpty().WithMessage("Destino da rota deve ser informado");

            RuleFor(x => x.Origem)
                .NotNull().WithMessage("Origem da rota deve ser informado")
                .NotEmpty().WithMessage("Origem da rota deve ser informado");
        
            RuleFor(x => decimal.Parse(x.Valor))
              .GreaterThan(0).WithMessage("Valor da rota deve ser maior que zero");
        }
    }
}
