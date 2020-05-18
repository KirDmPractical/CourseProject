using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace CourseProject
{
    public partial class Balloons : Form
    {
        int remaining_moves;//счётчик оставшихся ходов
        bool mistake = false;//совершил ли игрок ошибку
        bool was_mistake = false;
        List<int[]> match_indexes = new List<int[]>();//индексы при уничтожении шариков
        List<int[]> indexes = new List<int[]>();//индексы при перемещении шариков
        List<int[]> explosion_indexes = new List<int[]>();//индексы центра взрыва бомбы
        Timer tDestroy = new Timer();
        Timer tFall = new Timer();
        List <Point> points = new List<Point>();//индексы первого и второго выбранного шарика
        string[,] gameboard;//игровое поле
        string cursor;//курсор - показывает какой предмет из магазина активирован
        int[,] ballon_falling_span;//на сколько клеток нужно упасть шарику
        int difficulty = 2;//сложность

        public Balloons()
        {
            InitializeComponent();          
        }
        
        public void GameBoardInitialization()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(128, 128, 255));
            using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\Nametag.png"))
            {
                g.DrawImage(image, new Point(0, 0));
            }          
            DrawingLib drawing = new DrawingLib();
            g.DrawImage(drawing.draw_grid(pictureBox1.Width,pictureBox1.Height), new Point(0, 60));
            pictureBox1.Image = bmp;            
        }                
     
        private void Form1_Load(object sender, EventArgs e)
        {
            //улучшение графики
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            UpdateStyles(); 
        }

        //Обработчик нажатия игрока на игровое поле
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            GameProcess game = new GameProcess();
            //объявить битмап
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Graphics g = Graphics.FromImage(bmp);
            for (int y = 0; y < 10; y++)
                for (int x = 0; x < 10; x++)
                {
                    //считывание координат
                    if (e.X > x * 42 && e.X < (x + 1) * 42 &&
                        e.Y > y * 42 + 60 && e.Y < (y + 1) * 42 + 60)
                    {
                        int[] XY = { x, y };
                        //Если выбран инструмент
                        //кирка
                        if (cursor == "pickaxe")
                        {
                            match_indexes.Clear();
                            match_indexes.Add(XY);
                            DrawDestroyingBalloons();
                            tDestroy.Start();
                            cursor = "";
                        }
                        //краска
                        else if (cursor == "bucket")
                        {
                            gameboard[x, y] = "M";
                            cursor = "";
                            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 255)), new Rectangle(new Point(x * 42 + 1, y * 42 + 61), new Size(41, 41)));
                            using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\M.png"))
                            {
                                g.DrawImage(image, new Point(x * 42 + 1, y * 42 + 61));
                            }
                        }
                        //бомба
                        else if(cursor == "bomb")
                        {
                            gameboard[x, y] = "Bomb";
                            cursor = "";
                            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 255)), new Rectangle(new Point(x * 42 + 1, y * 42 + 61), new Size(41, 41)));
                            using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\Bomb.png"))
                            {
                                g.DrawImage(image, new Point(x * 42 + 1, y * 42 + 61));
                            }
                        }
                        //если это камень
                        else if (gameboard[x, y] == "Rock") ;
                        //если нет выбранного шарика
                        else if (indexes.Count == 0)
                        {
                            indexes.Add(XY);
                            g.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 64)), new Rectangle(new Point(x * 42 + 1, y * 42 + 61), new Size(41, 41)));
                            using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\" + gameboard[x, y] + ".png"))
                            {
                                g.DrawImage(image, new Point(x * 42 + 1, y * 42 + 61));
                            }
                        }
                        //если второй выбранный шарик находится рядом с первым
                        else if ((indexes[0][0] + 1 == XY[0] && indexes[0][1] == XY[1])
                              || (indexes[0][0] - 1 == XY[0] && indexes[0][1] == XY[1])
                              || (indexes[0][1] + 1 == XY[1] && indexes[0][0] == XY[0])
                              || (indexes[0][1] - 1 == XY[1] && indexes[0][0] == XY[0]))
                        {
                            indexes.Add(XY);
                            //поменять местами
                            DrawSwapingBalloons();
                            string bottle = gameboard[indexes[0][0], indexes[0][1]];
                            gameboard[indexes[0][0], indexes[0][1]] = gameboard[indexes[1][0], indexes[1][1]];
                            gameboard[indexes[1][0], indexes[1][1]] = bottle;
                            //поиск совпадений
                            match_indexes = game.search_matches(ref gameboard, indexes, difficulty);
                            bottle = gameboard[indexes[0][0], indexes[0][1]];
                            gameboard[indexes[0][0], indexes[0][1]] = gameboard[indexes[1][0], indexes[1][1]];
                            gameboard[indexes[1][0], indexes[1][1]] = bottle;
                            if (match_indexes.Count == 0)
                            {
                                mistake = true;
                                remaining_moves -= 1;
                                moves.Text = remaining_moves.ToString();
                                //если ходы кончились
                                if (remaining_moves == 0)
                                {
                                    label1.Visible = false;
                                    label3.Visible = false;
                                    score.Visible = false;
                                    moves.Visible = false;
                                    Shop.Visible = false;
                                    pictureBox1.Visible = false;
                                    label4.Visible = true;
                                    Finalscore.Visible = true;
                                    Finalscore.Text += score.Text;
                                }
                            }
                            else
                            {
                                //проверить бомбы
                                explosion_indexes = game.define_explode_indexes(gameboard, ref match_indexes);
                                //начислить очки
                                score.Text = ((int)(Convert.ToInt32(score.Text) + 2 * match_indexes.Count * Math.Pow(1.1, match_indexes.Count) * difficulty)).ToString();
                                //нарисовать
                                DrawDestroyingBalloons();
                            }
                            return;
                        }
                        //если второй выбранный шарик НЕ находится рядом с первым
                        else
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 255)), new Rectangle(new Point(indexes[0][0] * 42 + 1, indexes[0][1] * 42 + 61), new Size(41, 41)));
                            using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\" + gameboard[indexes[0][0], indexes[0][1]] + ".png"))
                            {
                                g.DrawImage(image, new Point(indexes[0][0] * 42, indexes[0][1] * 42 + 60));
                            }
                            g.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 64)), new Rectangle(new Point(x * 42 + 1, y * 42 + 61), new Size(41, 41)));
                            using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\" + gameboard[x, y] + ".png"))
                            {
                                g.DrawImage(image, new Point(x * 42 + 1, y * 42 + 61));
                            }
                            indexes[0][0] = x;
                            indexes[0][1] = y;
                        }
                        break;
                    }
                }
          
            pictureBox1.Image = bmp;//вывод на picturebox
        }

        private void DrawFallingBalloons()
        {   //объявить битмап
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Graphics g = Graphics.FromImage(bmp);
            //закрасить столбцы для падения
            for (int y = 9; y >= 0; y--)
                for (int x = 0; x <= 9; x++)
                    if (ballon_falling_span[x, y] > 0 || ballon_falling_span[x, y] == -1)
                        for (int i = y; i >= 0; i--)
                            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 255)), new Rectangle(new Point(x * 42 + 1, i * 42 + 61), new Size(41, 41)));
            float timer_ticks = 1;
            tFall = new Timer();
            tFall.Interval = 15;
            Bitmap generatedbmp = new Bitmap(bmp);//Сохнаняет текущую картинку
            tFall.Tick += new EventHandler((s, e) =>
            {
                //объявить битмап
                bmp = new Bitmap(generatedbmp);
                g = Graphics.FromImage(bmp);
                //просмотреть gameboard: падающие - вниз
                for (int x = 0; x <= 9; x++)
                {
                    for (int y = 9; y >= 0; y--)
                    {
                        if (ballon_falling_span[x, y] == -1)//новый шарик
                        {
                            using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\" + gameboard[x, y] + ".png"))
                            {
                                g.DrawImage(image, new Point(x * 42 + 1, (int)(timer_ticks / 25 * y * 42 + timer_ticks / 25 * 61)));
                            }
                        }
                        else if (ballon_falling_span[x, y] > 0)//падающий шарик
                        {
                            using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\" + gameboard[x, y] + ".png"))
                            {
                                g.DrawImage(image, new Point(x * 42 + 1, (int)((y - ballon_falling_span[x, y]) * 42 + 61 + (timer_ticks / 25.0f * ballon_falling_span[x, y] * 42))));
                            }
                        }                        
                    }
                }
                //нарисовать плашку
                using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\Nametag.png"))
                {
                    g.DrawImage(image, new Point(0, 0));
                }
                if (timer_ticks == 25)
                {
                    Timer t = s as Timer;
                    t.Dispose();
                }
                else
                    timer_ticks++;
                pictureBox1.Image = bmp;//вывод на picturebox
            }
            );
            return;
        }

        private void DrawDestroyingBalloons()
        {
            DrawingLib drawing = new DrawingLib();
            int timer_ticks = 0;
            tDestroy = new Timer();
            tDestroy.Interval = 30;
            tDestroy.Tick += new EventHandler((s, e) =>
            {
                //объявить битмап
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Graphics g = Graphics.FromImage(bmp);
                for (int i = 0;i<match_indexes.Count();i++)
                {
                    g.FillPie(new SolidBrush(Color.FromArgb(128, 128, 255)), new Rectangle(new Point(match_indexes[i][0] * 42 + 1, match_indexes[i][1] * 42 + 61), new Size(41, 41)), 18 * timer_ticks, 18 * timer_ticks + 1);
                }
                //если бомбы есть - нарисовать
                if (explosion_indexes.Count >= 1)
                {
                    for (int i = 0; i < explosion_indexes.Count; i++)
                        g.DrawImage(drawing.draw_explosion(timer_ticks, Application.StartupPath.ToString() + "\\Resouses\\Explosion\\explosion" + timer_ticks + ".png"), new Point((explosion_indexes[i][0] - 1) * 42, (explosion_indexes[i][1] - 1) * 42 + 61));
                }
                pictureBox1.Image = bmp;//вывод на picturebox   
                if (timer_ticks == 19)
                {
                    //нарисовать сетку
                    g.DrawImage(drawing.draw_grid(pictureBox1.Width, pictureBox1.Height), new Point(0, 60));
                    //очистить gameboard
                    for (int i = 0; i < match_indexes.Count(); i++)
                    {
                        gameboard[match_indexes[i][0], match_indexes[i][1]] = "";
                    }
                    explosion_indexes.Clear();
                    Timer t = s as Timer;
                    t.Dispose();
                    GameProcess game = new GameProcess();
                    ballon_falling_span = game.define_falling_balloons(ref gameboard, difficulty);
                    DrawFallingBalloons();
                    tFall.Start();
                }
                else
                    timer_ticks++;           
            }
            );         
            return;
        }

        private void DrawSwapingBalloons()
        {
            //объявить битмап
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 255)), new Rectangle(new Point(indexes[0][0] * 42 + 1, indexes[0][1] * 42 + 61), new Size(41, 41)));
            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 255)), new Rectangle(new Point(indexes[1][0] * 42 + 1, indexes[1][1] * 42 + 61), new Size(41, 41)));
            pictureBox1.Image = bmp;//вывод на picturebox
            Bitmap generatedbmp = new Bitmap(bmp);//Сохнаняет текущую картинку
            int timer_ticks = 1;
            Timer tSwap = new Timer();
            tSwap.Interval = 15;            
            tSwap.Tick += new EventHandler((s, e) =>
            {
                bmp = new Bitmap(generatedbmp);
                g = Graphics.FromImage(bmp);
                using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\" + gameboard[indexes[0][0], indexes[0][1]] + ".png"))
                {
                    g.DrawImage(image, new Point(indexes[0][0] * 42 + 6 * timer_ticks * (indexes[1][0] - indexes[0][0]) + 1, indexes[0][1] * 42 + 61 + 6 * timer_ticks * (indexes[1][1] - indexes[0][1])));
                }
                using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\" + gameboard[indexes[1][0], indexes[1][1]] + ".png"))
                {
                    g.DrawImage(image, new Point(indexes[1][0] * 42 + 6 * timer_ticks * (indexes[0][0] - indexes[1][0]) + 1, indexes[1][1] * 42 + 61 + 6 * timer_ticks * (indexes[0][1] - indexes[1][1])));
                }
                DrawingLib drawing = new DrawingLib();
                g.DrawImage(drawing.draw_grid(pictureBox1.Width, pictureBox1.Height), new Point(0, 60));
                pictureBox1.Image = bmp;//вывод на picturebox
                if (timer_ticks == 7)
                {                    
                    timer_ticks = 0;
                    if (mistake)
                    {
                        was_mistake = true;
                        mistake = false;
                        DrawSwapingBalloons();
                        string bottle = gameboard[indexes[0][0], indexes[0][1]];
                        gameboard[indexes[0][0], indexes[0][1]] = gameboard[indexes[1][0], indexes[1][1]];
                        gameboard[indexes[1][0], indexes[1][1]] = bottle;                        
                    }
                    else
                    {    
                        string bottle = gameboard[indexes[0][0], indexes[0][1]];
                        gameboard[indexes[0][0], indexes[0][1]] = gameboard[indexes[1][0], indexes[1][1]];
                        gameboard[indexes[1][0], indexes[1][1]] = bottle;
                        if (!was_mistake)
                        {
                            tDestroy.Start();
                        }
                        else
                            was_mistake = false;
                        indexes.Clear();
                    }   
                    Timer t = s as Timer;
                    t.Dispose();
                }
                timer_ticks++;
            }
            );
            tSwap.Start();
        }
        private void DrawFallingBalloons_start()
        {
            //объявить битмап
            Bitmap generatedbmp = new Bitmap(pictureBox1.Image);//Сохнаняет текущую картинку
            Bitmap bmp = new Bitmap(generatedbmp);//переменная ка которой будет происходить рисование
            Graphics g = Graphics.FromImage(bmp);
            int rowindex = 9;
            int timer_ticks = 0;
            Timer tStart = new Timer();
            tStart.Interval = 5;
            tStart.Tick += new EventHandler((o, e) =>
                {
                    bmp = new Bitmap(generatedbmp);
                    g = Graphics.FromImage(bmp);
                    for (int x = 0; x < 10; x++)
                    {
                        using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\" + gameboard[x, rowindex] + ".png"))
                        {
                            g.DrawImage(image, new Point(x * 42 + 1 , timer_ticks * 14 - 9));
                        }
                    }
                    //нарисовать плашку
                    using (var image = new Bitmap(Application.StartupPath.ToString() + "\\Resouses\\Nametag.png"))
                    {
                        g.DrawImage(image, new Point(0, 0));
                    }
                    if (timer_ticks * 14 >= rowindex * 42 + 61)
                    {
                        timer_ticks = -1;
                        generatedbmp = bmp;//сохранение после обрисовки строки
                        if (rowindex == 0)
                        {
                            Timer t = o as Timer;
                            t.Stop();
                        }
                        else
                            rowindex--;
                    }
                    pictureBox1.Image = bmp;//вывод на picturebox
                    timer_ticks++;
                }
                );
            tStart.Start();
        }

        //Выбор уровня сложности
        private void easy_Click(object sender, EventArgs e)
        {
            easy.BackColor = Color.Green;
            normal.BackColor = Color.FromArgb(0, 64, 0);
            hard.BackColor = Color.FromArgb(0, 64, 0);
            difficulty = 1;
        }

        private void normal_Click(object sender, EventArgs e)
        {
            normal.BackColor = Color.Green;
            easy.BackColor = Color.FromArgb(0, 64, 0);
            hard.BackColor = Color.FromArgb(0, 64, 0);
            difficulty = 2;
        }

        private void hard_Click(object sender, EventArgs e)
        {
            hard.BackColor = Color.Green;
            easy.BackColor = Color.FromArgb(0, 64, 0);
            normal.BackColor = Color.FromArgb(0, 64, 0);
            difficulty = 3;
        }
        //Начало игры
        private void startgame_Click(object sender, EventArgs e)
        {
            label2.Visible = false; 
            easy.Visible = false; 
            normal.Visible = false; 
            hard.Visible = false;
            startgame.Visible = false;
            pictureBox1.Visible = true;
            label1.Visible = true;
            label3.Visible = true;
            score.Visible = true;
            moves.Visible = true;
            Shop.Visible = true;
            switch(difficulty)
            {
                case 1: remaining_moves = 5; break;
                case 2: remaining_moves = 2; break;
                case 3: remaining_moves = 1; break;
            }
            toolTip1.SetToolTip(Pickaxe, (1000 * difficulty * 0.5).ToString());
            toolTip1.SetToolTip(Bomb, (1600 * difficulty * 0.5).ToString());
            toolTip1.SetToolTip(Bucket, (3000 * difficulty * 0.5).ToString());
            moves.Text = remaining_moves.ToString();
            GameBoardInitialization();//нарисовать сетку
            GameProcess gameProcess = new GameProcess();
            gameboard = gameProcess.starting_game_board_initialization();
            DrawFallingBalloons_start();                 
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Pickaxe_Click(object sender, EventArgs e)
        {
            score.Text = (Convert.ToInt32(score.Text) - 1000 * difficulty * 0.5).ToString();
            if (Convert.ToInt32(score.Text) > 0)
                cursor = "pickaxe";
            else
                score.Text = (Convert.ToInt32(score.Text) + 1000 * difficulty * 0.5).ToString();
        }

        private void Bucket_Click(object sender, EventArgs e)
        {
            score.Text = (Convert.ToInt32(score.Text) - 1600 * difficulty * 0.5).ToString();
            if (Convert.ToInt32(score.Text) > 0)
                cursor = "bucket";
            else
                score.Text = (Convert.ToInt32(score.Text) + 1600 * difficulty * 0.5).ToString();
        }

        private void Bomb_Click(object sender, EventArgs e)
        {
            score.Text = (Convert.ToInt32(score.Text) - 3000 * difficulty * 0.5).ToString();
            if (Convert.ToInt32(score.Text) > 0)
                cursor = "bomb";
            else
                score.Text = (Convert.ToInt32(score.Text) + 3000 * difficulty * 0.5).ToString();
        }
    }
}
