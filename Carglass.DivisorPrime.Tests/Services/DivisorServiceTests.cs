using Carglass.DivisorPrime.Domain.Dtos;
using Carglass.DivisorPrime.Mappers.Interfaces;
using Carglass.DivisorPrime.Service.Interfaces;
using Carglass.DivisorPrime.Service.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Carglass.DivisorPrime.Tests.Services;

public class DivisorServiceTests : IDisposable
{
    private readonly Mock<IDivisorHandler> _mockDivisorHandler;
    private readonly Mock<IPrimeDivisorHandler> _mockPrimeDivisorHandler;
    private readonly Mock<INumberValidator> _mockNumberValidator;
    private readonly Mock<IResponseBuilder> _mockResponseBuilder;
    private readonly Mock<ILogger<DivisorService>> _mockLogger;
    private readonly DivisorService _divisorService;

    public DivisorServiceTests()
    {
        _mockDivisorHandler = new Mock<IDivisorHandler>();
        _mockPrimeDivisorHandler = new Mock<IPrimeDivisorHandler>();
        _mockNumberValidator = new Mock<INumberValidator>();
        _mockResponseBuilder = new Mock<IResponseBuilder>();
        _mockLogger = new Mock<ILogger<DivisorService>>();

        _divisorService = new DivisorService(
            _mockDivisorHandler.Object,
            _mockPrimeDivisorHandler.Object,
            _mockLogger.Object,
            _mockResponseBuilder.Object,
            _mockNumberValidator.Object
        );
    }

    [Fact]
    public void Get_ShouldReturnSuccess_WhenNumberIsValid()
    {
        // Arrange
        var number = "10";
        var parsedNumber = 10;
        var divisors = new List<int> { 1, 2, 5, 10 };
        var primeDivisors = new List<int> { 2, 5 };

        _mockNumberValidator
            .Setup(v => v.Validate(number, out parsedNumber))
            .Returns(new ResponseDto<DivisorsDto>
            {
                IsSuccess = true,
                Message = "Validado com sucesso"
            });

        _mockDivisorHandler
            .Setup(h => h.Handle(parsedNumber))
            .Returns(divisors);

        _mockPrimeDivisorHandler
            .Setup(h => h.Handle(parsedNumber))
            .Returns(primeDivisors);

        _mockResponseBuilder
            .Setup(rb => rb.BuildSuccess(parsedNumber, divisors, primeDivisors))
            .Returns(new ResponseDto<DivisorsDto>
            {
                IsSuccess = true,
                Message = "Operação realizada com sucesso",
                Divisors = new DivisorsDto
                {
                    Number = parsedNumber,
                    Divisors = divisors,
                    PrimeDivisors = primeDivisors
                }
            });

        _mockLogger
            .Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception?>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()))
            .Verifiable();

        // Act
        var result = _divisorService.Get(number);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Operação realizada com sucesso", result.Message);
        Assert.NotNull(result.Divisors);
        Assert.Equal(parsedNumber, result.Divisors.Number);
        Assert.Equal(divisors, result.Divisors.Divisors);
        Assert.Equal(primeDivisors, result.Divisors.PrimeDivisors);

        // Verificação dos mocks
        _mockNumberValidator.Verify(v => v.Validate(number, out parsedNumber), Times.Once);
        _mockDivisorHandler.Verify(h => h.Handle(parsedNumber), Times.Once);
        _mockPrimeDivisorHandler.Verify(h => h.Handle(parsedNumber), Times.Once);
        _mockResponseBuilder.Verify(rb => rb.BuildSuccess(parsedNumber, divisors, primeDivisors), Times.Once);
        _mockLogger.Verify();
    }

    [Fact]
    public void Get_ShouldReturnError_WhenNumberIsInvalid()
    {
        // Arrange
        var invalidNumber = "B";
        var expectedErrorMessage = "[Erro]: API: O número informado é inválido. Certifique-se de inserir um número inteiro válido.";

        _mockNumberValidator
            .Setup(v => v.Validate(invalidNumber, out It.Ref<int>.IsAny))
            .Returns(new ResponseDto<DivisorsDto>
            {
                IsSuccess = false,
                Message = expectedErrorMessage
            });

        _mockLogger
            .Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception?>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()))
            .Verifiable();

        // Act
        var result = _divisorService.Get(invalidNumber);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrorMessage, result.Message);
        Assert.Null(result.Divisors);

        // Verificação dos mocks
        _mockNumberValidator.Verify(v => v.Validate(invalidNumber, out It.Ref<int>.IsAny), Times.Once);
        _mockLogger.Verify();
    }

    [Fact]
    public void Get_ShouldReturnError_WhenNumberIsEmpty()
    {
        // Arrange
        var emptyNumber = "";
        var expectedErrorMessage = "[Erro]: API: O número deve ser maior que zero.";

        _mockNumberValidator
            .Setup(v => v.Validate(emptyNumber, out It.Ref<int>.IsAny))
            .Returns(new ResponseDto<DivisorsDto>
            {
                IsSuccess = false,
                Message = expectedErrorMessage
            });

        _mockLogger
            .Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception?>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()))
            .Verifiable();

        // Act
        var result = _divisorService.Get(emptyNumber);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedErrorMessage, result.Message);
        Assert.Null(result.Divisors);

        // Verificação dos mocks
        _mockNumberValidator.Verify(v => v.Validate(emptyNumber, out It.Ref<int>.IsAny), Times.Once);
        _mockLogger.Verify();
    }

    public void Dispose()
    {
        _mockLogger.VerifyNoOtherCalls();
        _mockNumberValidator.VerifyNoOtherCalls();
        _mockDivisorHandler.VerifyNoOtherCalls();
        _mockPrimeDivisorHandler.VerifyNoOtherCalls();
        _mockResponseBuilder.VerifyNoOtherCalls();
    }
}
