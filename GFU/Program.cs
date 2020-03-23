using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GFU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);



            Application.Run(new GFUForm());
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            try
            {
                if (ex != null)
                {
                    if (ex.Message=="Stopped")
                    {
                        MessageBox.Show("Interrupted!");
                GFUForm f = Application.OpenForms[0] as GFUForm;
                if (f != null) f.ResetButton();
                    }
                }
                if (e.IsTerminating)
                    MessageBox.Show("GFU has encountered an error\r\n\r\n" + ex.ToString(), "GFU", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.Exception as Exception;
                if (ex != null)
                {
                    if (ex.Message == "Stopped")
                    {
                        GFUForm f = Application.OpenForms[0] as GFUForm;
                        if (f != null)
                        {
                            if (f.InvokeRequired) f.BeginInvoke(new Action(() => f.Stop()));
                            else f.Stop();
                        }
                    } else
                       MessageBox.Show("GFU has encountered an error\r\n\r\n" + ex.ToString(), "GFU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
            }
        }


    }
}
