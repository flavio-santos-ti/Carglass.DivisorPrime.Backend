using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Carglass.DivisorPrime.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class DivisorsDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Number { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? Divisors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<int>? PrimeDivisors { get; set; }
}
