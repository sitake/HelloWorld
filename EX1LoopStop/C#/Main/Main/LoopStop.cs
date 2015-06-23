using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Main
{
    class LoopStop
    {
        private bool isRunning = true;

        internal void Start()
        {
            var cts = new CancellationTokenSource();
            KeyboardEventLoop kel = new KeyboardEventLoop(code => Stop(code,cts));

            Task.WhenAll(
                kel.Start(cts.Token),
                Loop(cts.Token)

            );

        }

        void Stop(char ch,CancellationTokenSource cts)
        {
            cts.Cancel();
        }

        async Task Loop(CancellationToken ct)
        {
            int i = 0;
            while (!ct.IsCancellationRequested)
            {
                Console.WriteLine(i);
                i++;
                Thread.Sleep(10);
            }
        }
    }
}
