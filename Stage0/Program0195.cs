namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0195();
            Welcome8204();
            Console.ReadKey();
        }

        private static void Welcome0195()
        {
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", userName);
            Console.Beep();
        }
        static partial void Welcome8204();

    }
}