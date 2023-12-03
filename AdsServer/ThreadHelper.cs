using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsServer
{
     class ThreadHelper
    {
        public static void CreateThread(Action a)
        {
            Thread t = new Thread(() => { 
            try { a(); } catch (Exception e)
                {
                    PrintException.PrintExceptionInfo(e);
                }
            });
        }
    }
}
