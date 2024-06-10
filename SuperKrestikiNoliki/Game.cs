using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperKrestikiNoliki
{
    public class Game
    {
        private User _one;
        private User _two;

        public PlayingField _field = new PlayingField();

        public void Join(User _user)
        {
            if (_one==null)
            {
                _one = _user;
                _one.Simbol = "x";
                _one.Wait();
            }
            else if (_two==null&&_user!=_one)
            {
                _two = _user;
                _two.Simbol = "o";
                _two.Wait();
                StartGame();
            }
            else
            {
                _user.Error();
            }
        }
        private void StartGame()
        {
            bool value = true;
            while (!_field.Win())
            {
                if (value)
                {
                    _one.MyStepNow = true;
                    _two.MyStepNow = false;
                }
                else
                {
                    _two.MyStepNow = true;
                    _one.MyStepNow = false;
                }
                value = !value;
            }
            if (value)
            {
                _two.Win();
                _one.Win();
            }
        }
    }
}
