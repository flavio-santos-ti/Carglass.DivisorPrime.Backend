using Carglass.DivisorPrime.Domain.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Carglass.DivisorPrime.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class ResponseDto<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = "Operação relizada com sucesso";  // Mensagem padrão
    public ApiHttpStatusCode StatusCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Divisors { get; set; }
}
