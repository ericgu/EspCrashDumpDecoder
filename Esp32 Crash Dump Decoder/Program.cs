using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esp32_Crash_Dump_Decoder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //MapFile mapfile = new MapFile(@"D:\data\Electronics\SequenceController\Code\SequenceController\output.map");

            //Symbol symbol = mapfile.FindSymbol(0x400d3091);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
