using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTwo
{
    public class Game
    {
        private User _one=null!;
        private User _two=null!;
        public bool Join(User user)
        {
            if (_one==null)
            {
                _one = user;
                _one.AddSimbol("x");
                return true;
            }
            else if (_two==null &&_one!=user)
            {
                _two = user;
                _two.AddSimbol("o");
                Next();
                return true;
            }
            else
            {
                return false;   
            }
        }
        private bool odd = true;
        public void Next()
        {
            if (odd)
            {
                _one.ClientYouStep();
            }
            else
            {
                _two.ClientYouStep();
            }
            odd=!odd;

        }
    }
}
