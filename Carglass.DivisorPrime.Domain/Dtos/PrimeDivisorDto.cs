using System.Diagnostics.CodeAnalysis;

namespace Carglass.DivisorPrime.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class PrimeDivisorDto
{
    public List<int> PrimeDivisors { get; set; }
}
