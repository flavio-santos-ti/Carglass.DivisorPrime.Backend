using Carglass.DivisorPrime.Domain.Dtos;
using Carglass.DivisorPrime.Mappers.Builders;
using Carglass.DivisorPrime.Mappers.Interfaces;
using Carglass.DivisorPrime.Service.Interfaces;
using Carglass.DivisorPrime.Service.Validators;
using Microsoft.Extensions.Logging;

namespace Carglass.DivisorPrime.Service.Services;

public class DivisorService : IDivisorService
{
    private readonly IDivisorHandler _divisorHandler;
    private readonly IPrimeDivisorHandler _primeDivisorHandler;
    private readonly ILogger<DivisorService> _logger;
    private readonly IResponseBuilder _responseBuilder;
    private readonly INumberValidator _numberValidator;

    public DivisorService(IDivisorHandler divisorHandler, IPrimeDivisorHandler primeDivisorHandler, ILogger<DivisorService> logger, IResponseBuilder responseBuilder, INumberValidator numberValidator)
    {
        _divisorHandler = divisorHandler;
        _primeDivisorHandler = primeDivisorHandler;
        _logger = logger;
        _responseBuilder = responseBuilder;
        _numberValidator = numberValidator;
    }

    public ResponseDto<DivisorsDto> Get(string? number)
    {
        _logger.LogInformation("Iniciando processamento para o número: {Number}", number);

        try
        {
            var validationResponse = _numberValidator.Validate(number, out int parsedNumber);

            if (!validationResponse.IsSuccess)
            {
                _logger.LogWarning("Validação falhou para o número: {Number}", number);
                return validationResponse;
            }

            var divisors = _divisorHandler.Handle(parsedNumber);
            var primeDivisors = _primeDivisorHandler.Handle(parsedNumber);

            _logger.LogInformation("Processamento concluído com sucesso para o número: {Number}", number);
            return _responseBuilder.BuildSuccess(parsedNumber, divisors, primeDivisors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao processar o número: {Number}", number);
            return _responseBuilder.BuildInternalServerError($"Ocorreu um erro inesperado. Por favor, tente novamente mais tarde: {ex.Message}");
        }
    }
}
