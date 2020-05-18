using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    public class GameProcess
    {
        Random rand = new Random();
        public string[,] starting_game_board_initialization()
        {
            string[,] gameboard = new string[10, 10];
            
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    /*Выбор случайного цвета 
                    0-b; 1-g; 2-p; 3-r; 4-y;*/
                    switch (rand.Next(0, 5))
                    {
                        case 0:
                            gameboard[x, y] = "B"; break;
                        case 1:
                            gameboard[x, y] = "G"; break;
                        case 2:
                            gameboard[x, y] = "P"; break;
                        case 3:
                            gameboard[x, y] = "R"; break;
                        case 4:
                            gameboard[x, y] = "Y"; break;
                    }
                }
            }
            return gameboard;
        }

        private string colour_gen(int difficulty)
        {
            int bottle;
            switch(difficulty)
            {
                case 1:
                  bottle = rand.Next(1, 101);
                    if (bottle >= 1 && bottle <= 18)
                        return "B";
                    else if (bottle > 18 && bottle <= 36)
                        return "G";
                    else if (bottle > 36 && bottle <= 54)
                        return "P";
                    else if (bottle > 54 && bottle <= 72)
                        return "R";
                    else if (bottle > 72 && bottle <= 90)
                        return "Y";
                    else if (bottle > 90 && bottle <= 97)
                        return "Bomb";
                    else if (bottle > 97 && bottle <= 100)
                        return "Rock";
                    break;
                case 2:
                    bottle = rand.Next(1, 101);
                    if (bottle >= 1 && bottle <= 18)
                        return "B";
                    else if (bottle > 18 && bottle <= 36)
                        return "G";
                    else if (bottle > 36 && bottle <= 54)
                        return "P";
                    else if (bottle > 54 && bottle <= 72)
                        return "R";
                    else if (bottle > 72 && bottle <= 90)
                        return "Y";
                    else if (bottle > 90 && bottle <= 93)
                        return "Bomb";
                    else if (bottle > 93 && bottle <= 100)
                        return "Rock";
                    break;
                case 3:
                    bottle = rand.Next(1, 101);
                    if (bottle >= 1 && bottle <= 18)
                        return "B";
                    else if (bottle > 18 && bottle <= 36)
                        return "G";
                    else if (bottle > 36 && bottle <= 54)
                        return "P";
                    else if (bottle > 54 && bottle <= 72)
                        return "R";
                    else if (bottle > 72 && bottle <= 90)
                        return "Y";
                    else if (bottle > 90 && bottle <= 91)
                        return "Bomb";
                    else if (bottle > 91 && bottle <= 100)
                        return "Rock";
                    break;
            }
            return "";
        }
        public List<int[]> search_matches(ref string[,] gameboard, List<int[]> indexes, int difficulty)
        {
            List<int[]> match_indexes = new List<int[]>();
            switch (difficulty)
            {
                case 1:
                case 2:
                    difficulty = 3;
                    //первая клетка
                    if (match_is_discovered(gameboard, indexes[0][0], indexes[0][1], difficulty))
                        match_indexes.AddRange(define_remove_indexes(ref gameboard, indexes[0][0], indexes[0][1]));
                    //вторая клетка
                    if (match_is_discovered(gameboard, indexes[1][0], indexes[1][1], difficulty))
                        match_indexes.AddRange(define_remove_indexes(ref gameboard, indexes[1][0], indexes[1][1]));
                    break;
                case 3:
                    difficulty = 4;
                    //первая клетка
                    if (match_is_discovered(gameboard, indexes[0][0], indexes[0][1], difficulty))
                        match_indexes.AddRange(define_remove_indexes(ref gameboard, indexes[0][0], indexes[0][1]));
                    //вторая клетка
                    if (match_is_discovered(gameboard, indexes[1][0], indexes[1][1], difficulty))
                        match_indexes.AddRange(define_remove_indexes(ref gameboard, indexes[1][0], indexes[1][1]));
                    break;
            }
            return match_indexes;
        }

        private bool match_is_discovered(string[,] gameboard, int X_i, int Y_i, int difficulty)
        {
            int num_of_matches = 0;
            //по горихонтали(X)
            for (int x = X_i - (difficulty-1); x <= X_i + (difficulty - 1); x++)
            {
                if (x >= 0 && x <= 9 && (gameboard[x, Y_i] == gameboard[X_i, Y_i] || gameboard[x, Y_i] == "M"))
                    num_of_matches++;
                else num_of_matches = 0;
                if (num_of_matches == difficulty)
                    return true;
            }
            //по вертикали(Y)
            for (int y = Y_i - (difficulty - 1); y <= Y_i + (difficulty - 1); y++)
            {
                if (y >= 0 && y <= 9 && (gameboard[X_i, y] == gameboard[X_i, Y_i] || gameboard[X_i, y] == "M"))
                    num_of_matches++;
                else num_of_matches = 0;
                if (num_of_matches == difficulty)
                    return true;
            }
            return false;
        }

        private List<int[]> define_remove_indexes(ref string[,] gameboard, int startX, int startY)
        {
            string step = "x";
            bool end = false;
            bool miss = false;
            List<int[]> indexes = new List<int[]>();
            string color = gameboard[startX, startY];
            indexes.Add(new int[2] { startX, startY });
            while(!end)
            {
                end = by_line(ref gameboard, ref indexes, ref step, color);
                if(end && !miss)
                {
                    miss = true;
                    end = false;
                }
                else if(!end)
                {
                    miss = false;
                }
            }
            return indexes;
        }

        private bool by_line(ref string[,] gameboard, ref List<int[]> indexes, ref string step, string color)
        {
            int[] bottle;
            int dots = indexes.Count();
            bool find = false;
            bool unique; 
            switch(step)
            {
                case "x":
                    for(int i = 0;i<dots;i++)
                    {
                        //слева
                        unique = true;
                        if (indexes[i][0] - 1 >= 0)//допустимый индекс
                        {
                            for (int j = 0; j < dots; j++)//уникальный индекс?
                                if (indexes[j][0] == indexes[i][0] - 1 && indexes[j][1] == indexes[i][1])
                                {
                                    unique = false;
                                }
                            if (unique && (gameboard[indexes[i][0] - 1, indexes[i][1]] == color || gameboard[indexes[i][0] - 1, indexes[i][1]] == "M"))
                            {
                                find = true;
                                indexes.Add(new int[2] { indexes[i][0] - 1, indexes[i][1] });
                            }
                        }
                        //справа
                        unique = true;
                        if (indexes[i][0] + 1 <= 9)//допустимый индекс
                        {
                            for (int j = 0; j < dots; j++)//уникальный индекс?
                                if (indexes[j][0] == indexes[i][0] + 1 && indexes[j][1] == indexes[i][1])
                                {
                                    unique = false;
                                }
                            if (unique && (gameboard[indexes[i][0] + 1, indexes[i][1]] == color || gameboard[indexes[i][0] + 1, indexes[i][1]] == "M"))
                            {
                                find = true;
                                indexes.Add(new int[2] { indexes[i][0] + 1, indexes[i][1] });
                            }
                        }
                    }
                    step = "y";
                    break;
                case "y":
                    for (int i = 0; i < dots; i++)
                    {
                        //сверху
                        unique = true;
                        if (indexes[i][1] - 1 >= 0)//допустимый индекс
                        {
                            for (int j = 0; j < dots; j++)//уникальный индекс?
                                if (indexes[j][0] == indexes[i][0] && indexes[j][1] == indexes[i][1] - 1)
                                {
                                    unique = false;
                                }
                            if (unique && (gameboard[indexes[i][0], indexes[i][1] - 1] == color || gameboard[indexes[i][0], indexes[i][1] - 1] == "M"))
                            {
                                find = true;
                                indexes.Add(new int[2] { indexes[i][0], indexes[i][1] - 1 });
                            }
                        }
                        //снизу
                        unique = true;
                        if (indexes[i][1] + 1 <= 9)//допустимый индекс
                        {
                            for (int j = 0; j < dots; j++)//уникальный индекс?
                                if (indexes[j][0] == indexes[i][0] && indexes[j][1] == indexes[i][1] + 1)
                                {
                                    unique = false;
                                }
                            if (unique && (gameboard[indexes[i][0], indexes[i][1] + 1] == color || gameboard[indexes[i][0], indexes[i][1] + 1] == "M"))
                            {
                                find = true;
                                indexes.Add(new int[2] { indexes[i][0], indexes[i][1] + 1 });
                            }
                        }
                    }
                    step = "x";
                    break;
            }
            return !find;
        }

        public int[,] define_falling_balloons(ref string[,] gameboard, int difficulty)
        {
            int[,] ballon_falling_span = new int[10, 10];//На сколько клеток нужно упасть шарику
            //(если "-1", то сгенерировать на пустое место шарик)
            for (int y = 9; y >= 0; y--)
            {
                for (int x = 0; x <= 9; x++)
                {
                    if (gameboard[x, y] == "")
                    {
                        for (int y_fall = y - 1; y_fall >= -1; y_fall--) 
                        {
                            if (y_fall == -1)//Падать нечему, сгенерировать
                            {
                                ballon_falling_span[x, y] = -1;
                                gameboard[x, y] = colour_gen(difficulty);
                            }
                            else if (gameboard[x, y_fall] != "")
                            {
                                //сколько клеток
                                ballon_falling_span[x, y] = y - y_fall;
                                //падение
                                gameboard[x, y] = gameboard[x, y_fall];
                                gameboard[x, y_fall] = "";
                                break;
                            }
                        }
                    }
                }
            }
            return ballon_falling_span;
        }
    }
}
