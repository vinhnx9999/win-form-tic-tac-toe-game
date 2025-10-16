using System.Windows.Forms;

namespace WinTicTacToe.Models
{
    public class Chess
    {
        public Label _lb;
        public int X;
        public int Y;

        public Chess() => _lb = new Label();

        public Chess(Label lb, int x, int y)
        {
            _lb = lb ?? new Label();
            X = x;
            Y = y;
        }
    }
}
