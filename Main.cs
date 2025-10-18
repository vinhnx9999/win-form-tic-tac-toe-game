using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;
using WinTicTacToe.Models;

namespace WinTicTacToe
{
    public partial class Main : Form
    {
        private readonly Label[,] _map;
        private readonly int _columns;
        private readonly int _rows;
        private int _player;
        private bool _gameover;
        private readonly bool _isComputer;
        private readonly int[,] _vtMap;
        private readonly Stack<Chess> _chesses;
        private Chess _chess;        

        public Main()
        {
            InitializeComponent();
            _columns = 20;
            _rows = 17;

            _isComputer = false;
            _gameover = false;
            _player = 1;
            _map = new Label[_rows + 2, _columns + 2];
            _vtMap = new int[_rows + 2, _columns + 2];
            _chesses = new Stack<Chess>();
            BuildTable();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Text = "Caro Game";            
        }

        public void BuildTable()
        {
            for (int i = 2; i <= _rows; i++)
                for (int j = 1; j <= _columns; j++)
                {
                    _map[i, j] = new Label
                    {
                        Parent = pnMain,
                        Top = i * AppConstant.BorderChess,
                        Left = j * AppConstant.BorderChess,
                        Size = new Size(AppConstant.BorderChess - 1, AppConstant.BorderChess - 1),
                        BackColor = Color.Snow
                    };

                    _map[i, j].MouseLeave += Main_MouseLeave;
                    _map[i, j].MouseEnter += Main_MouseEnter;
                    _map[i, j].Click += Main_Click;
                }
        }
        private void Main_MouseLeave(object sender, EventArgs e)
        {
            if (_gameover)
                return;
            Label lb = (Label)sender;
            lb.BackColor = Color.Snow;
        }

        private void Main_MouseEnter(object sender, EventArgs e)
        {
            if (_gameover)
                return;
            Label lb = (Label)sender;
            lb.BackColor = Color.AliceBlue;
        }

        private void Main_Click(object sender, EventArgs e)
        {
            if (_gameover)
                return;
            Label lb = (Label)sender;
            int x = lb.Top / AppConstant.BorderChess - 1, y = lb.Left / AppConstant.BorderChess;
            if (_vtMap[x, y] != 0)
                return;

            if (_isComputer)
            {
                _player = 1;
                psbCooldownTime.Value = 0;
                tmCountDown.Start();
                lb.Image = Properties.Resources.o;
                picUser.Image = Properties.Resources.computer1;
                _vtMap[x, y] = 1;
                CheckStep(x, y);
                CptFindChess();
            }
            else
            {
                if (_player == 1)
                {
                    psbCooldownTime.Value = 0;
                    tmCountDown.Start();
                    lb.Image = Properties.Resources.o;
                    _vtMap[x, y] = 1;
                    CheckStep(x, y);

                    _player = 2;
                    ptbPayer.Image = Properties.Resources.xcopy;
                    txtNamePlayer.Text = "Player2";
                    picUser.Image = Properties.Resources.player21;
                }
                else
                {
                    psbCooldownTime.Value = 0;
                    lb.Image = Properties.Resources.x;
                    _vtMap[x, y] = 2;
                    CheckStep(x, y);

                    _player = 1;
                    ptbPayer.Image = Properties.Resources.omg;
                    txtNamePlayer.Text = "Vinh";
                    picUser.Image = Properties.Resources.player11;
                }
            }
            _chess = new Chess(lb, x, y);
            _chesses.Push(_chess);
        }

        private void CptFindChess()
        {
            if (_gameover) return;
            long max = 0;
            int imax = 1, jmax = 1;
            for (int i = 1; i < _rows; i++)
            {
                for (int j = 1; j < _columns; j++)
                    if (_vtMap[i, j] == 0)
                    {
                        long temp = Caculate(i, j);
                        if (temp > max)
                        {
                            max = temp;
                            imax = i; jmax = j;
                        }
                    }
            }

            UpdateChess(imax, jmax);
        }

        private void UpdateChess(int x, int y)
        {
            _player = 0;
            psbCooldownTime.Value = 0;
            _map[x + 1, y].Image = Properties.Resources.x;

            _vtMap[x, y] = 2;
            CheckStep(x, y);

            _chess = new Chess(_map[x + 1, y], x, y);
            _chesses.Push(_chess);
        }

        private long Caculate(int x, int y)
        {
            return EnemyChesses(x, y) + ComputerChesses(x, y);
        }

        private long ComputerChesses(int x, int y)
        {
            int i = x - 1, j = y;
            int curColumn = 0, curRow = 0, mLine = 0, across = 0;
            int sc_ = 0, sc = 0, sr_ = 0, sr = 0, sm_ = 0, sm = 0, se_ = 0, se = 0;

            while (_vtMap[i, j] == 2 && i >= 0)
            {
                curColumn++;
                i--;
            }
            if (_vtMap[i, j] == 0) sc_ = 1;
            i = x + 1;

            while (_vtMap[i, j] == 2 && i <= _rows)
            {
                curColumn++;
                i++;
            }
            if (_vtMap[i, j] == 0) sc = 1;
            i = x; j = y - 1;

            while (_vtMap[i, j] == 2 && j >= 0)
            {
                curRow++;
                j--;
            }
            if (_vtMap[i, j] == 0) sr_ = 1;
            j = y + 1;

            while (_vtMap[i, j] == 2 && j <= _columns)
            {
                curRow++;
                j++;
            }
            if (_vtMap[i, j] == 0) sr = 1;
            i = x - 1; j = y - 1;

            while (_vtMap[i, j] == 2 && i >= 0 && j >= 0)
            {
                mLine++;
                i--;
                j--;
            }
            if (_vtMap[i, j] == 0) sm_ = 1;
            i = x + 1; j = y + 1;

            while (_vtMap[i, j] == 2 && i <= _rows && j <= _columns)
            {
                mLine++;
                i++;
                j++;
            }
            if (_vtMap[i, j] == 0) sm = 1;
            i = x - 1; j = y + 1;

            while (_vtMap[i, j] == 2 && i >= 0 && j <= _columns)
            {
                across++;
                i--;
                j++;
            }
            if (_vtMap[i, j] == 0) se_ = 1;
            i = x + 1; j = y - 1;

            while (_vtMap[i, j] == 2 && i <= _rows && j >= 0)
            {
                across++;
                i++;
                j--;
            }

            if (_vtMap[i, j] == 0) se = 1;

            if (curColumn == AppConstant.LineToWin - 1) curColumn = AppConstant.LineToWin;
            if (curRow == AppConstant.LineToWin - 1) curRow = AppConstant.LineToWin;
            if (mLine == AppConstant.LineToWin - 1) mLine = AppConstant.LineToWin;
            if (across == AppConstant.LineToWin - 1) across = AppConstant.LineToWin;

            if (curColumn == 3 && sc == 1 && sc_ == 1) curColumn = 4;
            if (curRow == 3 && sr == 1 && sr_ == 1) curRow = 4;
            if (mLine == 3 && sm == 1 && sm_ == 1) mLine = 4;
            if (across == 3 && se == 1 && se_ == 1) across = 4;

            if (curColumn == 2 && curRow == 2 && sc == 1 && sc_ == 1 && sr == 1 && sr_ == 1) curColumn = 3;
            if (curColumn == 2 && mLine == 2 && sc == 1 && sc_ == 1 && sm == 1 && sm_ == 1) curColumn = 3;
            if (curColumn == 2 && across == 2 && sc == 1 && sc_ == 1 && se == 1 && se_ == 1) curColumn = 3;
            if (curRow == 2 && mLine == 2 && sm == 1 && sm_ == 1 && sr == 1 && sr_ == 1) curColumn = 3;
            if (curRow == 2 && across == 2 && se == 1 && se_ == 1 && sr == 1 && sr_ == 1) curColumn = 3;
            if (across == 2 && mLine == 2 && sm == 1 && sm_ == 1 && se == 1 && se_ == 1) curColumn = 3;

            long Sum = AppConstant.ComputerAttack[curRow] + 
                AppConstant.ComputerAttack[curColumn] + 
                AppConstant.ComputerAttack[mLine] + 
                AppConstant.ComputerAttack[across];

            return Sum;
        }

        private long EnemyChesses(int x, int y)
        {
            int i = x - 1, j = y;
            int sc_ = 0, sc = 0, sr_ = 0, sr = 0, sm_ = 0, sm = 0, se_ = 0, se = 0;
            int curColumn = 0, curRow = 0, mLine = 0, across = 0;

            while (_vtMap[i, j] == 1 && i >= 0)
            {
                curColumn++;
                i--;
            }
            if (_vtMap[i, j] == 0) sc_ = 1;
            i = x + 1;

            while (_vtMap[i, j] == 1 && i <= _rows)
            {
                curColumn++;
                i++;
            }
            if (_vtMap[i, j] == 0) sc = 1;
            i = x; j = y - 1;

            while (_vtMap[i, j] == 1 && j >= 0)
            {
                curRow++;
                j--;
            }
            if (_vtMap[i, j] == 0) sr_ = 1;
            j = y + 1;

            while (_vtMap[i, j] == 1 && j <= _columns)
            {
                curRow++;
                j++;
            }
            if (_vtMap[i, j] == 0) sr = 1;
            i = x - 1; j = y - 1;

            while (_vtMap[i, j] == 1 && i >= 0 && j >= 0)
            {
                mLine++;
                i--;
                j--;
            }
            if (_vtMap[i, j] == 0) sm_ = 1;
            i = x + 1; j = y + 1;

            while (_vtMap[i, j] == 1 && i <= _rows && j <= _columns)
            {
                mLine++;
                i++;
                j++;
            }
            if (_vtMap[i, j] == 0) sm = 1;
            i = x - 1; j = y + 1;

            while (_vtMap[i, j] == 1 && i >= 0 && j <= _columns)
            {
                across++;
                i--;
                j++;
            }
            if (_vtMap[i, j] == 0) se_ = 1;
            i = x + 1; j = y - 1;

            while (_vtMap[i, j] == 1 && i <= _rows && j >= 0)
            {
                across++;
                i++;
                j--;
            }
            if (_vtMap[i, j] == 0) se = 1;

            if (curColumn == AppConstant.LineToWin - 1) curColumn = AppConstant.LineToWin;
            if (curRow == AppConstant.LineToWin - 1) curRow = AppConstant.LineToWin;
            if (mLine == AppConstant.LineToWin - 1) mLine = AppConstant.LineToWin;
            if (across == AppConstant.LineToWin -1) across = AppConstant.LineToWin;

            if (curColumn == 3 && sc == 1 && sc_ == 1) curColumn = 4;
            if (curRow == 3 && sr == 1 && sr_ == 1) curRow = 4;
            if (mLine == 3 && sm == 1 && sm_ == 1) mLine = 4;
            if (across == 3 && se == 1 && se_ == 1) across = 4;

            if (curColumn == 2 && curRow == 2 && sc == 1 && sc_ == 1 && sr == 1 && sr_ == 1) curColumn = 3;
            if (curColumn == 2 && mLine == 2 && sc == 1 && sc_ == 1 && sm == 1 && sm_ == 1) curColumn = 3;
            if (curColumn == 2 && across == 2 && sc == 1 && sc_ == 1 && se == 1 && se_ == 1) curColumn = 3;
            if (curRow == 2 && mLine == 2 && sm == 1 && sm_ == 1 && sr == 1 && sr_ == 1) curColumn = 3;
            if (curRow == 2 && across == 2 && se == 1 && se_ == 1 && sr == 1 && sr_ == 1) curColumn = 3;
            if (across == 2 && mLine == 2 && sm == 1 && sm_ == 1 && se == 1 && se_ == 1) curColumn = 3;

            long Sum = AppConstant.ComputerDefense[curRow] + 
                AppConstant.ComputerDefense[curColumn] + 
                AppConstant.ComputerDefense[mLine] + 
                AppConstant.ComputerDefense[across];

            return Sum;
        }

        private void CheckStep(int x, int y)
        {
            int i = x - 1, j = y;
            int curColumn = 1, curRow = 1, mLine = 1, across = 1;

            while (_vtMap[x, y] == _vtMap[i, j] && i >= 0)
            {
                curColumn++;
                i--;
            }
            i = x + 1;

            while (_vtMap[x, y] == _vtMap[i, j] && i <= _rows)
            {
                curColumn++;
                i++;
            }
            i = x; j = y - 1;

            while (_vtMap[x, y] == _vtMap[i, j] && j >= 0)
            {
                curRow++;
                j--;
            }
            j = y + 1;

            while (_vtMap[x, y] == _vtMap[i, j] && j <= _columns)
            {
                curRow++;
                j++;
            }
            i = x - 1; j = y - 1;

            while (_vtMap[x, y] == _vtMap[i, j] && i >= 0 && j >= 0)
            {
                mLine++;
                i--;
                j--;
            }
            i = x + 1; j = y + 1;

            while (_vtMap[x, y] == _vtMap[i, j] && i <= _rows && j <= _columns)
            {
                mLine++;
                i++;
                j++;
            }
            i = x - 1; j = y + 1;

            while (_vtMap[x, y] == _vtMap[i, j] && i >= 0 && j <= _columns)
            {
                across++;
                i--;
                j++;
            }
            i = x + 1; j = y - 1;

            while (_vtMap[x, y] == _vtMap[i, j] && i <= _rows && j >= 0)
            {
                across++;
                i++;
                j--;
            }

            if (curRow >= AppConstant.LineToWin || curColumn >= AppConstant.LineToWin || mLine >= AppConstant.LineToWin || across >= AppConstant.LineToWin)
            {
                Gameover();
                ShowWinner(false);                
            }
        }

        private void Gameover()
        {
            tmCountDown.Stop();
            _gameover = true;
            SetBackgroundGameover();
        }

        private void SetBackgroundGameover()
        {
            for (int i = 2; i <= _rows; i++)
                for (int j = 1; j <= _columns; j++)
                {
                    _map[i, j].BackColor = Color.Gray;
                }
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            if (_gameover)
                return;
            Label lb = (Label)sender;
            lb.BackColor = Color.AliceBlue;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            if (_gameover)
                return;
            Label lb = (Label)sender;
            lb.BackColor = Color.Snow;
        }

        private void TmCountDown_Tick(object sender, EventArgs e)
        {
            //.:: Tic tac toe ::.
            if (Text.Contains("Caro Game") && _player == 1)
            {
                Text = "Caro Game - Player 1's turn";
            }
            else if (Text.Contains("Caro Game") && _player == 2)
            {
                Text = "Caro Game - Player 2's turn";
            }
            else if (Text.Contains("Player 1") && _player == 2)
            {
                Text = "Caro Game - Player 2's turn";
            }
            else if (Text.Contains("Player 2") && _player == 1)
            {
                Text = "Caro Game - Player 1's turn";
            }
            else if (Text.Contains("wins"))
            {
                tmCountDown.Stop();
            }

            psbCooldownTime.PerformStep();
            if (psbCooldownTime.Value >= psbCooldownTime.Maximum)
            {
                Gameover();
                ShowWinner(true);                
            }
        }

        private void ShowWinner(bool isDownTime)
        {
            string winner;
            if (isDownTime)
            {
                if (_isComputer)
                {
                    winner = _player == 2 ? "You win!!" : "You lost!!";                    
                }
                else
                {
                    winner = _player == 2 ? "Vinh wins!!" : "Player2 wins!!";                   
                }
            }
            else if (_isComputer)
            {
                winner = _player == 1 ? "You win!!" : "You lost!!";                
            }
            else
            {
                winner = _player == 1 ? "Vinh wins!!" : "Player2 wins!!";
            }

            MessageBox.Show(winner);
            lblWinner.Text = $"[{BeatyStringTitleWinner(winner)}] {RandomMsgWin()}";
        }

        private string BeatyStringTitleWinner(string name)
        {
            if (string.IsNullOrEmpty(name)) return "Winner";
            return name.Replace("!!", "").Replace("wins", "").Replace("win", "");
        }

        private string RandomMsgWin()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 5);
            switch (randomNumber)
            {
                case 1:
                    return "Congratulations!";
                case 2:
                    return "Well played!";
                case 3:
                    return "You're a champion!";
                case 4:
                    return "Fantastic job!";
                default:
                    return "Victory is yours!";
            }
        }
         
        private void MenuQuit_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Are you sure stop the game?", "Close App", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                this.Dispose();
                this.Close();
            }
        }
    }
}
