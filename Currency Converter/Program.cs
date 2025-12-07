using System;
using System.Windows.Forms;

namespace Currency_Converter
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запускаємо головне меню
            Application.Run(new Welcome());
        }
    }
}