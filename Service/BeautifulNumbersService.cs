namespace BeautifulNumbers.Service;

public class BeautifulNumbersService
{
    private const int NumeralSystem = 13;
    
    //Колличесто суммируемых разрядов
    const int DigitsCount = 6;

    /// <summary>
    /// Количество 13-ти значных красивых чисел с ведущими нулями в тринадцатиричной системе исчисления
    /// </summary>
    /// <remarks>
    /// Назовем число красивым, если сумма его первых шести цифр равна сумме шести последних цифр.
    /// Пример:
    /// Число 0055237050A00 - красивое, так как 0+0+5+5+2+3 = 0+5+0+A+0+0
    /// Число 1234AB988BABA - некрасивое, так как 1+2+3+4+A+B != 8+8+B+A+B+A
    /// </remarks>
    public long GetBeautifulNumbersCount()
    { 
       //домножаем т.к. есть свободный разряд
       return GetLuckyTicketCount(DigitsCount, NumeralSystem) * NumeralSystem;
    }

    /// <summary>
    /// Обобщенный подход для расчета колличества счасливых билетов в любой с.с.,
    /// </summary>
    /// <param name="digitsCount">Длина сравниваемой части</param>
    /// <param name="numeralSystem">Система счисления</param>
    internal long GetLuckyTicketCount(int digitsCount, int numeralSystem)
    {
        int maxDigitValue = numeralSystem - 1;
        
        // инициализируем матрицу для частичных решений
        var partialSolutions = new long[digitsCount][];
        partialSolutions[0] = new long[numeralSystem];
        for (int k = 0; k <= maxDigitValue; ++k)
        {
            partialSolutions[0][k] = 1;
        }
        
        for (int n = 1; n < digitsCount; ++n)
        {
            int maxSum = (n + 1) * maxDigitValue;
            partialSolutions[n] = new long[maxSum + 1];
            for (int k = maxSum; k >= 0; --k)
            {
                int statIndex = Math.Max(0, k - n * maxDigitValue);
                for (int i = statIndex; i < numeralSystem && i <= k; ++i)
                {
                    partialSolutions[n][k] += partialSolutions[n - 1][k - i];
                }
            }
        }

        long result = 0;
        for (int k = 0; k <= maxDigitValue * digitsCount; ++k)
        {
            result += partialSolutions[digitsCount - 1][k] * partialSolutions[digitsCount - 1][k];
        }
        return result;
    }
}