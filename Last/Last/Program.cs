using System;
class Program
{
    static void Main()
    {
        var input = Console.ReadLine();
        float n=Convert.ToSingle(input);

        Console.WriteLine(n);
        if (n%1==0)
        {
            Console.WriteLine("入力数は整数");
        }
        else
        {
            Console.WriteLine("入力数は小数");
        }
    }
}