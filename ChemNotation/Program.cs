using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChemNotation
{
    static class Program
    {
        // Debug logger
        private static readonly ErrorLogger Log = new ErrorLogger(typeof(Program));

        private static DrawingForm _DForm;
        private static AboutForm _AForm;
        
        public static WelcomeForm WForm { get; private set; }
        
        /// <summary>
        /// The Form with the main drawing interface.
        /// This field is set to initialise upon trying to be read for the first time.
        /// </summary>
        public static DrawingForm DForm
        {
            get
            {
                // This pattern esssentially tells the field to initialise if it is null
                // but otherwise it will be returned.
                // The private field is used as a non-modifiable container.
                if (_DForm == null)
                {
                    Log.LogMessageInfo("DrawingForm created.");
                    _DForm = new DrawingForm();
                }
                return _DForm;
            }
        }

        /// <summary>
        /// The Form with help pages and information about the program.
        /// This field is set to initialise upon trying to be read for the first time.
        /// </summary>
        public static AboutForm AForm
        {
            get
            {
                if (_AForm == null)
                {
                    Log.LogMessageInfo("AboutForm created.");
                    _AForm = new AboutForm();
                }
                return _AForm;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.LogMessageGeneral("ChemNotation main process start.");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log.LogMessageGeneral("Style and text rendering set.");

            WForm = new WelcomeForm();

            Log.LogMessageGeneral("Welcome form created, launching.");

            Application.Run(WForm);

            Log.LogMessageGeneral("ChemNotation main process exiting.");
        }
    }
}
