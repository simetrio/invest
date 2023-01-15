namespace Invest.TaxCalculator.BusinessLogic
{
    public static class Instruction
    {
        public static void WriteInstruction(this IApplicationBuilder _)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Open https://localhost:5001 in browser");
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}