using Carglass.DivisorPrime.Domain.Dtos;

namespace Carglass.DivisorPrime.Service.Interfaces;

public interface INumberValidator
{
    ResponseDto<DivisorsDto> Validate(string? number, out int parsedNumber);
}
