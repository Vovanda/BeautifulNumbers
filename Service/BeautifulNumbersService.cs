namespace BeautifulNumbers.Service;

public class BeautifulNumbersService
{
    private const int NumeralSystem = 13;
    
    //Колличесто суммируемых разрядов
    const int DigitsCount = 6;

    private long[][] partialSolutions;
    
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
       return GetBeautifulNumbersCount(DigitsCount, NumeralSystem);
    }

    /// <summary>
    /// Обобщенный подход для поиска крассивых числел вида dd..dDdd..d в любой с.с.,
    /// где dd..d - части фиксированной лины суммы разрядов которых сравниваются, D - свободный разряд
    /// </summary>
    /// <param name="digitsCount">Длина сравниваемой части числа</param>
    /// <param name="numeralSystem">Система счисления</param>
    /// <remarks>Требуется для возможности протестировать</remarks>
    internal long GetBeautifulNumbersCount(int digitsCount, int numeralSystem)
    {
        int maxDigitValue = numeralSystem - 1;

        long result = 0;
        //В случае 13-й с.с. проходим от 0 до 72 т.к. 72 максимальная сумма из 6 цифр 
        for (var n = 0; n <= maxDigitValue * digitsCount; ++n)
        {
            // вычисляем колличество композиций задонной длины
            var compositionCount = GetCompositionCount(n, digitsCount, numeralSystem);
            // возводим в квадрат т.к. любое число с суммой n c одной и другой стороны нас устраивает
            result += compositionCount * compositionCount;
        }
        // домножаем на значение с.c. т.к. 1 разряд свободный и при его изменении числа не перестанут быть красивыми
        return result * numeralSystem;
    }
    
    internal long GetCompositionCount(int sumValue, int digitsCount, int numeralSystem)
    {
        if (partialSolutions == null) PartialSolutionsMatrixInit(digitsCount, numeralSystem);
        return CalculateCompositionsCount(0, sumValue, digitsCount, numeralSystem);
    }
    
    private long CalculateCompositionsCount(int index, int sumValue, int digitsCount, int numeralSystem)
    {
        if (index == digitsCount) return sumValue == 0 ? 1 : 0;
        if (sumValue == 0)  return 1;

        if (partialSolutions[index][sumValue - 1] != -1)
        {
            return partialSolutions[index][sumValue - 1];
        }
        
        long answer = 0;
 
        for(int i = 0; i < numeralSystem; i++)
        {
            if (i > sumValue) break;
            answer += CalculateCompositionsCount(index + 1,sumValue - i, digitsCount, numeralSystem);
        }
        
        partialSolutions[index][sumValue - 1] = answer;
        
        return answer;
    }

    private void PartialSolutionsMatrixInit(int digitsCount, int numeralSystem)
    {
        int maxValueOfSumDigits = (numeralSystem - 1) * digitsCount;
        // Инициализируем матрицу с количеством  для частичных решений
        partialSolutions = new long[digitsCount][];
        for (int i = 0; i < digitsCount; ++i)
        {
            partialSolutions[i] = new long[maxValueOfSumDigits];
            partialSolutions[i].Populate(-1);
        }
    }
}