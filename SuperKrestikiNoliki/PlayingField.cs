using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperKrestikiNoliki
{
    public class PlayingField
    {
        public string[,] array {  get; set; }   

        public PlayingField()
        {
            array = new string[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    array[i, j] = "";
                }
            }
        }
        public bool Check(int x, int y, string simbol)
        {
            if (x<0&&x>=3 &&y<=0 && y>=3)
            {
                return false;
            }
            if (array[x,y]!="x" && array[x,y]!="o")
            {
                array[x, y] = simbol;
                return true;
            }
                return false;
        }
        public bool Win()
        {
            if (array[0, 0]!="" && array[0, 0] == array[0, 1] && array[0, 0] == array[0, 2])
                return true;
            else if (array[1, 0] != "" && array[1, 0] == array[1, 1] && array[1, 0] == array[1, 2])
                return true;
            else if (array[2, 0] != "" && array[2, 0] == array[2, 1] && array[2, 0] == array[2, 2])
                return true;
            else if (array[0, 0] != "" && array[0, 0] == array[1, 0] && array[0, 0] == array[2, 0])
                return true;
            else if (array[0, 1] != "" && array[0, 1] == array[1, 1] && array[0, 1] == array[2, 1])
                return true;
            else if (array[0, 2] != "" && array[0, 2] == array[1, 2] && array[0, 2] == array[2, 2])
                return true;
            else if (array[0, 0] != "" && array[0, 0] == array[1, 1] && array[0, 0] == array[2, 2])
                return true;
            else if (array[0, 2] != "" && array[0, 2] == array[1, 1] && array[0, 2] == array[2, 0])
                return true;
            else
                return false;


        }
    }
}
