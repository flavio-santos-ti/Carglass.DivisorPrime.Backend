using Carglass.DivisorPrime.Domain.Dtos;

namespace Carglass.DivisorPrime.Service.Interfaces;

public interface IDivisorService
{
    ResponseDto<DivisorsDto> Get(string? number);
}
