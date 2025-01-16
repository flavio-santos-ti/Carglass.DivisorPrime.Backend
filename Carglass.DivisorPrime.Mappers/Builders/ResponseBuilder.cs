using Carglass.DivisorPrime.Domain.Dtos;
using Carglass.DivisorPrime.Domain.Enums;
using Carglass.DivisorPrime.Mappers.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Carglass.DivisorPrime.Mappers.Builders;

[ExcludeFromCodeCoverage]
public class ResponseBuilder : IResponseBuilder
{
    private readonly ILogger<ResponseBuilder> _logger;

    public ResponseBuilder(ILogger<ResponseBuilder> logger)
    {
        _logger = logger;
    }

    public ResponseDto<DivisorsDto> BuildSuccess(int parsedNumber, List<int> divisors, List<int> primeDivisors)
    {
        _logger.LogInformation("Resposta de sucesso gerada para o número: {Number}", parsedNumber);

        var dto = new DivisorsDto
        {
            Number = parsedNumber,
            Divisors = divisors,
            PrimeDivisors = primeDivisors
        };

        return new ResponseDto<DivisorsDto>
        {
            IsSuccess = true,
            Message = "Operação realizada com sucesso",
            StatusCode = ApiHttpStatusCode.Success,
            Divisors = dto
        };
    }

    public ResponseDto<DivisorsDto> BuildBadRequest(string message)
    {
        _logger.LogWarning("Resposta de BadRequest gerada: {Message}", message);

        return new ResponseDto<DivisorsDto>
        {
            IsSuccess = false,
            Message = message,
            StatusCode = ApiHttpStatusCode.BadRequest,
            Divisors = null
        };
    }

    public ResponseDto<DivisorsDto> BuildValidationSuccess()
    {
        _logger.LogInformation("Validação bem-sucedida");

        return new ResponseDto<DivisorsDto>
        {
            IsSuccess = true,
            Message = "Validado com sucesso",
            StatusCode = ApiHttpStatusCode.Success
        };
    }

    public ResponseDto<DivisorsDto> BuildInternalServerError(string message)
    {
        _logger.LogError("Erro interno do servidor: {Message}", message);

        return new ResponseDto<DivisorsDto>
        {
            IsSuccess = false,
            Message = message,
            StatusCode = ApiHttpStatusCode.InternalServerError,
            Divisors = null
        };
    }
}
