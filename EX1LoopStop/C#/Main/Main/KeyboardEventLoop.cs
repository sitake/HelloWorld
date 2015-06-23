using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Main
{

    delegate void KeyboardEventHandler(char ch);

    class KeyboardEventLoop
    {

        KeyboardEventHandler _onkeyDown;
        private KeyboardEventHandler keyboardEventHandler;

        public KeyboardEventLoop(KeyboardEventHandler OnKeyDown)
        {
            _onkeyDown += OnKeyDown;
        }

        public Task Start(CancellationToken ct){
            return Task.Run(() => EventLoop(ct));
        }

        private void EventLoop(CancellationToken ct)            
        {
            while(!ct.IsCancellationRequested){
                char ch = Console.ReadLine().ToCharArray()[0];
                _onkeyDown(ch);
            }

        }

    }
}
