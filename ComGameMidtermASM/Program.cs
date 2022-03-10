using System;

namespace ComGameMidtermASM
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            //using (var game = new main()) game.Run();
            
            using (var gametest = new maintest()) gametest.Run();
        }
    }
}
