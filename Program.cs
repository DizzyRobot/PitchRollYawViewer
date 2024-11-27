using System;
using System.Windows.Forms;

[assembly: System.Runtime.Versioning.SupportedOSPlatformAttribute("windows")]

namespace PitchRollYawViewer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AppMainForm());
        }
    }
}
