using NetworkTransportProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLib;

namespace SuperKrestikiNoliki
{
    public class User
    {
        public string Simbol {  get; set; }

        private IReceive _receiver;
        private ISend _sender;  
        public User(ISend sender, IReceive receiver)
        {
            _receiver = receiver;
            _sender = sender;
        }
        public bool MyStepNow { get; set; } = false;//изначально нет хода

        private bool wait = false;
        public void Wait()// метод для ожидания противника
        {
            _sender.Send(CommandType.Wait);
            wait = true;
            while (wait)
            {

            }
        }
        public void Step(PlayingField playingField)
        {
            wait = false;
            _sender.Send(CommandType.Step); //для разрешения шага
            bool result = false;
            do
            {
                SendFieldToClient(playingField.array);
                result = ReceiveCoordinationOnClient(playingField);
                if (result==false)
                {
                    _sender.Send(CommandType.Error);
                }
            } 
            while (!result);
            _sender.Send(CommandType.Wait);

           
        }
        private bool ReceiveCoordinationOnClient(PlayingField playingField)
        {
            string message = _receiver.GetMessageToString(); //для принятия координат(0 ряд, 1 ячейка, ну к примеру)
            string[] positions = message.Split(':'); // разбивает массив на цифры position[0] = 0 position[1] = 1
            int.TryParse(positions[0], out int x);
            int.TryParse(positions[1], out int y);
            return playingField.Check(x, y, Simbol);
        }
        private void SendFieldToClient(string[,] array)
        {
            string message = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    message += array[i, j] + ",";
                }
            }
            message = message.Substring(0,message.Length - 1);
            _sender.Send(message);
        }
        public void Error()
        {
            _sender.Send(CommandType.Error);   
        }
        public void Loser()
        {
            _sender.Send(CommandType.YouLoser);
        }
        public void Win()
        {
            _sender.Send(CommandType.YouWin);
        }
      
        
    }
}
