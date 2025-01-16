using Carglass.DivisorPrime.Domain.Dtos;

namespace Carglass.DivisorPrime.Mappers.Interfaces;

public interface IResponseBuilder
{
    ResponseDto<DivisorsDto> BuildSuccess(int parsedNumber, List<int> divisors, List<int> primeDivisors);
    ResponseDto<DivisorsDto> BuildBadRequest(string message);
    ResponseDto<DivisorsDto> BuildValidationSuccess();
    ResponseDto<DivisorsDto> BuildInternalServerError(string message);
}
