using Carglass.DivisorPrime.Service.Interfaces;

namespace Carglass.DivisorPrime.Service.Handlers;

public class PrimeDivisorHandler : IPrimeDivisorHandler
{
    public List<int> Handle(int number)
    {
        var primeDivisors = new List<int>();

        for (int i = 1; i <= number; i++)
        {
            if (number % i == 0 && IsPrime(i))
            {
                primeDivisors.Add(i);
            }
        }

        return primeDivisors;
    }

    // Método para verificar se o número é primo
    private bool IsPrime(int number)
    {
        if (number <= 1) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }
}
