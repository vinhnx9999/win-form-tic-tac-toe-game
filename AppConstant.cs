namespace WinTicTacToe
{
    public class AppConstant
    {
        public static int DownTime = 100;
        public static int DownStep = 10000;
        public static int DownInterval = 100;
        public static string NamePlayer;
        public static int BorderChess = 28;
        public static int LineToWin = 5;
        public static int[] ComputerAttack = new int[] { 0, 9, 54, 162, 1458, 13112, 118008 };
        public static int[] ComputerDefense = new int[] { 0, 3, 27, 99, 729, 6561, 59049 };
    }
}
