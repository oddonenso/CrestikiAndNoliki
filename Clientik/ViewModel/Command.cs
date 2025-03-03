﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Clientik.ViewModel
{
    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action _action;

        public Command(Action action)
        {
            ArgumentNullException.ThrowIfNull(action, nameof(action));
            _action = action;
        }

        public bool CanExecute(object? parameter) => true;


        public void Execute(object? parameter) => _action.Invoke();
    }
        
}
