using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLib;

namespace ServerTwo
{
    public class User
    {
        private ISend _send;
        public IReceive _receive;

        public bool Wait { get; set; } = false; //изначально ниче не ожидаем
        private string _simbol;
        public User(ISend send, IReceive receive)
        {
            this._send = send;
            this._receive = receive;
        }
        public void AddSimbol(string simbol)
        {
            _simbol = simbol;
            if (simbol.ToLower().Equals("x"))
            {
                _send.Send(CommandType.X);
            }
            else
            {
                _send.Send(CommandType.O);  
            }
            //TODO - Это важный коммент
            //TODO: тут отправка клиенту типа символа
        }
        //прописывание команд
        public void ClientWait()
        {
            Wait = true; //ожидай ход
            _send.Send(CommandType.Wait);
        }
        public void Error()
        {
            _send.Send(CommandType.Error);  
        }
        public void ClientYouStep()
        {
            Wait = false;
        }

        public void Step(PlayingField field)
        {
            if (Wait == false) // если мы не стоим на месте
            {
                bool result = true;
                //тогда user должен идти
                do
                {
                    _send.Send(CommandType.Step);
                    SendFieldToClient(field);
                     result = ReceiveCoordinationOnClient(field);
                    if (result==true)
                    {
                        Wait = true;
                        ClientWait();
                    }
                    else
                    {
                        Error();    
                    }
                } while (result==false);
            }
        }
        private bool ReceiveCoordinationOnClient(PlayingField playingField)
        {
            string message = _receive.GetMessageToString(); //для принятия координат(0 ряд, 1 ячейка, ну к примеру)
            string[] positions = message.Split(':'); // разбивает массив на цифры position[0] = 0 position[1] = 1
            int.TryParse(positions[0], out int x);
            int.TryParse(positions[1], out int y);
            return playingField.Check(x, y, _simbol);
        }
        private void SendFieldToClient(PlayingField field)
        {
            var array = field.array;
            string message = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    message += array[i, j] + ",";
                }
            }
            message = message.Substring(0, message.Length - 1);
            _send.Send(message);
        }
        public void ClientLoser()
        {
            _send.Send(CommandType.YouLoser);   
        }
        public void ClientWin()
        {
            _send.Send(CommandType.YouWin);
        }
    }
}
