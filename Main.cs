using System;
using System.Collections.Generic;
using System.Drawing;
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
        private readonly int[] ComputerAttack = new int[] { 0, 9, 54, 162, 1458, 13112, 118008 };
        private readonly int[] ComputerDefense = new int[] { 0, 3, 27, 99, 729, 6561, 59049 };

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
                lb.Image = Properties.Resources.O;
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
                    lb.Image = Properties.Resources.O;
                    _vtMap[x, y] = 1;
                    CheckStep(x, y);

                    _player = 2;
                    ptbPayer.Image = Properties.Resources.Xcopy;
                    txtNamePlayer.Text = "Player2";
                }
                else
                {
                    psbCooldownTime.Value = 0;
                    lb.Image = Properties.Resources.X;
                    _vtMap[x, y] = 2;
                    CheckStep(x, y);

                    _player = 1;
                    ptbPayer.Image = Properties.Resources.OMG;
                    txtNamePlayer.Text = "Vinh";
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
            _map[x + 1, y].Image = Properties.Resources.X;

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
            int column = 0, row = 0, mdiagonal = 0, ediagonal = 0;
            int sc_ = 0, sc = 0, sr_ = 0, sr = 0, sm_ = 0, sm = 0, se_ = 0, se = 0;
            while (_vtMap[i, j] == 2 && i >= 0)
            {
                column++;
                i--;
            }
            if (_vtMap[i, j] == 0) sc_ = 1;
            i = x + 1;
            while (_vtMap[i, j] == 2 && i <= _rows)
            {
                column++;
                i++;
            }
            if (_vtMap[i, j] == 0) sc = 1;
            i = x; j = y - 1;
            while (_vtMap[i, j] == 2 && j >= 0)
            {
                row++;
                j--;
            }
            if (_vtMap[i, j] == 0) sr_ = 1;
            j = y + 1;
            while (_vtMap[i, j] == 2 && j <= _columns)
            {
                row++;
                j++;
            }
            if (_vtMap[i, j] == 0) sr = 1;
            i = x - 1; j = y - 1;
            while (_vtMap[i, j] == 2 && i >= 0 && j >= 0)
            {
                mdiagonal++;
                i--;
                j--;
            }
            if (_vtMap[i, j] == 0) sm_ = 1;
            i = x + 1; j = y + 1;
            while (_vtMap[i, j] == 2 && i <= _rows && j <= _columns)
            {
                mdiagonal++;
                i++;
                j++;
            }
            if (_vtMap[i, j] == 0) sm = 1;
            i = x - 1; j = y + 1;
            while (_vtMap[i, j] == 2 && i >= 0 && j <= _columns)
            {
                ediagonal++;
                i--;
                j++;
            }
            if (_vtMap[i, j] == 0) se_ = 1;
            i = x + 1; j = y - 1;
            while (_vtMap[i, j] == 2 && i <= _rows && j >= 0)
            {
                ediagonal++;
                i++;
                j--;
            }
            if (_vtMap[i, j] == 0) se = 1;

            if (column == 4) column = 5;
            if (row == 4) row = 5;
            if (mdiagonal == 4) mdiagonal = 5;
            if (ediagonal == 4) ediagonal = 5;

            if (column == 3 && sc == 1 && sc_ == 1) column = 4;
            if (row == 3 && sr == 1 && sr_ == 1) row = 4;
            if (mdiagonal == 3 && sm == 1 && sm_ == 1) mdiagonal = 4;
            if (ediagonal == 3 && se == 1 && se_ == 1) ediagonal = 4;

            if (column == 2 && row == 2 && sc == 1 && sc_ == 1 && sr == 1 && sr_ == 1) column = 3;
            if (column == 2 && mdiagonal == 2 && sc == 1 && sc_ == 1 && sm == 1 && sm_ == 1) column = 3;
            if (column == 2 && ediagonal == 2 && sc == 1 && sc_ == 1 && se == 1 && se_ == 1) column = 3;
            if (row == 2 && mdiagonal == 2 && sm == 1 && sm_ == 1 && sr == 1 && sr_ == 1) column = 3;
            if (row == 2 && ediagonal == 2 && se == 1 && se_ == 1 && sr == 1 && sr_ == 1) column = 3;
            if (ediagonal == 2 && mdiagonal == 2 && sm == 1 && sm_ == 1 && se == 1 && se_ == 1) column = 3;

            long Sum = ComputerAttack[row] + ComputerAttack[column] + ComputerAttack[mdiagonal] + ComputerAttack[ediagonal];

            return Sum;
        }

        private long EnemyChesses(int x, int y)
        {
            int i = x - 1, j = y;
            int sc_ = 0, sc = 0, sr_ = 0, sr = 0, sm_ = 0, sm = 0, se_ = 0, se = 0;
            int column = 0, row = 0, mdiagonal = 0, ediagonal = 0;
            while (_vtMap[i, j] == 1 && i >= 0)
            {
                column++;
                i--;
            }
            if (_vtMap[i, j] == 0) sc_ = 1;
            i = x + 1;
            while (_vtMap[i, j] == 1 && i <= _rows)
            {
                column++;
                i++;
            }
            if (_vtMap[i, j] == 0) sc = 1;
            i = x; j = y - 1;
            while (_vtMap[i, j] == 1 && j >= 0)
            {
                row++;
                j--;
            }
            if (_vtMap[i, j] == 0) sr_ = 1;
            j = y + 1;
            while (_vtMap[i, j] == 1 && j <= _columns)
            {
                row++;
                j++;
            }
            if (_vtMap[i, j] == 0) sr = 1;
            i = x - 1; j = y - 1;
            while (_vtMap[i, j] == 1 && i >= 0 && j >= 0)
            {
                mdiagonal++;
                i--;
                j--;
            }
            if (_vtMap[i, j] == 0) sm_ = 1;
            i = x + 1; j = y + 1;
            while (_vtMap[i, j] == 1 && i <= _rows && j <= _columns)
            {
                mdiagonal++;
                i++;
                j++;
            }
            if (_vtMap[i, j] == 0) sm = 1;
            i = x - 1; j = y + 1;
            while (_vtMap[i, j] == 1 && i >= 0 && j <= _columns)
            {
                ediagonal++;
                i--;
                j++;
            }
            if (_vtMap[i, j] == 0) se_ = 1;
            i = x + 1; j = y - 1;
            while (_vtMap[i, j] == 1 && i <= _rows && j >= 0)
            {
                ediagonal++;
                i++;
                j--;
            }
            if (_vtMap[i, j] == 0) se = 1;

            if (column == 4) column = 5;
            if (row == 4) row = 5;
            if (mdiagonal == 4) mdiagonal = 5;
            if (ediagonal == 4) ediagonal = 5;

            if (column == 3 && sc == 1 && sc_ == 1) column = 4;
            if (row == 3 && sr == 1 && sr_ == 1) row = 4;
            if (mdiagonal == 3 && sm == 1 && sm_ == 1) mdiagonal = 4;
            if (ediagonal == 3 && se == 1 && se_ == 1) ediagonal = 4;

            if (column == 2 && row == 2 && sc == 1 && sc_ == 1 && sr == 1 && sr_ == 1) column = 3;
            if (column == 2 && mdiagonal == 2 && sc == 1 && sc_ == 1 && sm == 1 && sm_ == 1) column = 3;
            if (column == 2 && ediagonal == 2 && sc == 1 && sc_ == 1 && se == 1 && se_ == 1) column = 3;
            if (row == 2 && mdiagonal == 2 && sm == 1 && sm_ == 1 && sr == 1 && sr_ == 1) column = 3;
            if (row == 2 && ediagonal == 2 && se == 1 && se_ == 1 && sr == 1 && sr_ == 1) column = 3;
            if (ediagonal == 2 && mdiagonal == 2 && sm == 1 && sm_ == 1 && se == 1 && se_ == 1) column = 3;
            long Sum = ComputerDefense[row] + ComputerDefense[column] + ComputerDefense[mdiagonal] + ComputerDefense[ediagonal];

            return Sum;
        }

        private void CheckStep(int x, int y)
        {
            int i = x - 1, j = y;
            int column = 1, row = 1, mdiagonal = 1, ediagonal = 1;
            while (_vtMap[x, y] == _vtMap[i, j] && i >= 0)
            {
                column++;
                i--;
            }
            i = x + 1;
            while (_vtMap[x, y] == _vtMap[i, j] && i <= _rows)
            {
                column++;
                i++;
            }
            i = x; j = y - 1;
            while (_vtMap[x, y] == _vtMap[i, j] && j >= 0)
            {
                row++;
                j--;
            }
            j = y + 1;
            while (_vtMap[x, y] == _vtMap[i, j] && j <= _columns)
            {
                row++;
                j++;
            }
            i = x - 1; j = y - 1;
            while (_vtMap[x, y] == _vtMap[i, j] && i >= 0 && j >= 0)
            {
                mdiagonal++;
                i--;
                j--;
            }
            i = x + 1; j = y + 1;
            while (_vtMap[x, y] == _vtMap[i, j] && i <= _rows && j <= _columns)
            {
                mdiagonal++;
                i++;
                j++;
            }
            i = x - 1; j = y + 1;
            while (_vtMap[x, y] == _vtMap[i, j] && i >= 0 && j <= _columns)
            {
                ediagonal++;
                i--;
                j++;
            }
            i = x + 1; j = y - 1;
            while (_vtMap[x, y] == _vtMap[i, j] && i <= _rows && j >= 0)
            {
                ediagonal++;
                i++;
                j--;
            }
            if (row >= 5 || column >= 5 || mdiagonal >= 5 || ediagonal >= 5)
            {
                Gameover();
                if (_isComputer)
                {
                    if (_player == 1)
                        MessageBox.Show("You win!!");
                    else
                        MessageBox.Show("You lost!!");
                }
                else
                {
                    if (_player == 1)
                        MessageBox.Show("Vinh Win");
                    else
                        MessageBox.Show("Player2 Win");
                }
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
        }

        private void menuQuit_Click(object sender, EventArgs e)
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
