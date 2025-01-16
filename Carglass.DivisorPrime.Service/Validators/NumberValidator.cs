using Carglass.DivisorPrime.Domain.Dtos;
using Carglass.DivisorPrime.Mappers.Interfaces;
using Carglass.DivisorPrime.Service.Interfaces;

namespace Carglass.DivisorPrime.Service.Validators;

public class NumberValidator : INumberValidator
{
    private readonly IResponseBuilder _responseBuilder;

    public NumberValidator(IResponseBuilder responseBuilder)
    {
        _responseBuilder = responseBuilder;
    }

    public ResponseDto<DivisorsDto> Validate(string? number, out int parsedNumber)
    {
        parsedNumber = 0;

        if (!string.IsNullOrWhiteSpace(number) && !int.TryParse(number, out parsedNumber))
            return _responseBuilder.BuildBadRequest("O número informado é inválido. Certifique-se de inserir um número inteiro válido.");

        if (string.IsNullOrWhiteSpace(number) || parsedNumber <= 0)
            return _responseBuilder.BuildBadRequest("O número deve ser maior que zero.");

        return _responseBuilder.BuildValidationSuccess();
    }
}
