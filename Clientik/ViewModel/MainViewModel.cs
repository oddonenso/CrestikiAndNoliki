using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TransportLib;

namespace Clientik.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        //на стрелочку надо нажать


        #region "это цвета кнопки" 
        public string Color1
        {
            get => Colors[0, 0];
            set
            {
                Colors[0, 0] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color1)));    
            }
        }
           
        public string Color2
        {
            get => Colors[0, 1];
            set
            {
                Colors[0, 1] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color2)));
            }
        }
        public string Color3
        {
            get => Colors[0, 2];
            set
            {
                Colors[0, 2] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color3)));
            }
        }
        public string Color4
        {
            get => Colors[1, 0];
            set
            {
                Colors[1, 0] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color4)));
            }
        }
        public string Color5
        {
            get => Colors[1, 1];
            set
            {
                Colors[1, 1] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color5)));
            }
        }
        public string Color6
        {
            get => Colors[1, 2];
            set
            {
                Colors[1, 2] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color5)));
            }
        }
        public string Color7
        {
            get => Colors[2, 0];
            set
            {
                Colors[2, 0] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color6)));
            }
        }
        public string Color8
        {
            get => Colors[2, 1];
            set
            {
                Colors[2, 1] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color7)));
            }
        }
        public string Color9
        {
            get => Colors[2, 2];
            set
            {
                Colors[2, 2] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color8)));
            }
        }
        #endregion //для сокращения кода


        public string[,] Colors { get; set; } = new string[3, 3];
        #region "кнопки"
        public ICommand Click1 { get; set; } = null!;
        public ICommand Click2 { get; set; } = null!;
        public ICommand Click3 { get; set; } = null!;
        public ICommand Click4 { get; set; } = null!;
        public ICommand Click5 { get; set; } = null!;
        public ICommand Click6 { get; set; } = null!;
        public ICommand Click7 { get; set; } = null!;
        public ICommand Click8 { get; set; } = null!;
        public ICommand Click9 { get; set; } = null!;
        public ICommand Join { get; set; } = null!;

        #endregion

        private readonly ISend _sender;
        private readonly IReceive _receiver;
        public MainViewModel(ISend send, IReceive receive)
        {
            ArgumentNullException.ThrowIfNull(send, nameof(send));
            ArgumentNullException.ThrowIfNull(receive, nameof(receive));
            _sender = send;
            _receiver = receive;
            Join = new Command(StartGame);

            Click1 = new Command(() => Step(0, 0));
            Click2 = new Command(() => Step(0, 1));
            Click3 = new Command(() => Step(0, 2));

            Click4 = new Command(() => Step(1, 0));
            Click5 = new Command(() => Step(1, 1));
            Click6 = new Command(() => Step(1, 2));

            Click7 = new Command(() => Step(2, 0));
            Click8 = new Command(() => Step(2, 1));
            Click9 = new Command(() => Step(2, 2));
        }

    
        #region "метод для кнопки"

        private bool wait = false; //для ожидания
        public string _simbol;
        private void StartGame()
        {
            _sender.Send(CommandType.StartGame);
            CommandType simbol = _receiver.GetMessage();
            if (simbol==CommandType.Error)
            {
                MessageBox.Show("Нет мест");
                return;
            }
            if (simbol==CommandType.X)
            {
                _simbol = "x";
            }
            else
            {
                _simbol = "o";
            }
            CommandType result = _receiver.GetMessage();
            if (result==CommandType.Wait)
            {
                wait = true;
                MessageBox.Show("Поиск противника");
                Task.Run(Wait);
            }
            else if(result==CommandType.Error)
            {
                MessageBox.Show("Нет мест");
            }
        }
        #endregion

        //метод ожидания хода

        private void Wait()
        {
            CommandType type = _receiver.GetMessage();
            if (type==CommandType.Step)
            {
                wait = false;
                string fieldToString = _receiver.GetMessageToString();
                MessageBox.Show(fieldToString);
                string[] field = fieldToString.Split(',');
                if (field.Length >= 9)
                {
                    Color1 = GetColor(field[0]);
                    Color2 = GetColor(field[1]);
                    Color3 = GetColor(field[2]);
                    Color4 = GetColor(field[3]);
                    Color5 = GetColor(field[4]);
                    Color6 = GetColor(field[5]);
                    Color7 = GetColor(field[6]);
                    Color8 = GetColor(field[7]);
                    Color9 = GetColor(field[8]);
                }
            }
            else if (type.ToString().ToLower()==_simbol)
            {
                Color1 = GetColor(_simbol);
                Color2 = GetColor(_simbol);
                Color3 = GetColor(_simbol);
                Color4 = GetColor(_simbol);
                Color5 = GetColor(_simbol);
                Color6 = GetColor(_simbol);
                Color7 = GetColor(_simbol);
                Color8 = GetColor(_simbol);
                Color9 = GetColor(_simbol);
            }
            else
            {
                Color1 = "Black";
                Color2 = "Black";
                Color3 = "Black";
                Color4 = "Black";
                Color5 = "Black";
                Color6 = "Black";
                Color7 = "Black";
                Color8 = "Black";
                Color9 = "Black";
            }
        }
      
        private string GetColor(string simbol )
        {
            if (simbol=="x")
            {
                return "Red";
            }
            else if (simbol=="o")
            {
                return "Blue";
            }
            else
            {
                return "White";
            }
        }
        private void Step(int x, int y)
        {
            if (wait==false)
            {
                string answer = $"{x}:{y}";
                _sender.Send(answer);
                CommandType result = _receiver.GetMessage();

                if (result==CommandType.Error)
                {
                    Wait();
                }
                else
                {
                    wait = true;
                    Colors[x, y] = GetColor(_simbol);
                    ChangeField();
                    Task.Run(Wait);
                }
            }
        }
        private void ChangeField()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color1)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color2)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color3)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color4)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color5)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color6)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color7)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color8)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color9)));

        }
    }
}
