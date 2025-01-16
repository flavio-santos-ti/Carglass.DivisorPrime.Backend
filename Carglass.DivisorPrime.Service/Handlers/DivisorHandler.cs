using Carglass.DivisorPrime.Service.Interfaces;

namespace Carglass.DivisorPrime.Service.Handlers;

public class DivisorHandler : IDivisorHandler
{
    public List<int> Handle(int number)
    {
        var divisors = new List<int>();

        for (int i = 1; i <= number; i++)
        {
            if (number % i == 0)
            {
                divisors.Add(i);
            }
        }

        return divisors;
    }
}
