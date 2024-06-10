using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportLib
{
    public enum CommandType
    {
        None = 0,   
        StartGame=1,
        Step=2,
        Exit=3,
        Wait=4,
        YouWin=5,
        YouLoser=6,
        Error = 7,
        X=8,
        O=9,
    }
}
