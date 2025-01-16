using System.Diagnostics.CodeAnalysis;

namespace Carglass.DivisorPrime.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class DivisorDto
{
    public List<int>? Divisors { get; set; }
}
