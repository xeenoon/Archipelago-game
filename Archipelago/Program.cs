using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Archipelago
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RuleFunctions.OnStart();
            if (args.Length != 0)
            {
                Application.Run(new MainGameForm(args[0]));
            }
            else
            {
                Application.Run(new StartForm());
            }

        }
    }
}
