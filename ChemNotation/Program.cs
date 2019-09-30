using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChemNotation
{
    static class Program
    {
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
                if (_DForm == null) _DForm = new DrawingForm();
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
                if (_AForm == null) _AForm = new AboutForm();
                return _AForm;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WForm = new WelcomeForm();

            Application.Run(WForm);
        }
    }
}
