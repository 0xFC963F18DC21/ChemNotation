using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChemNotation
{
    public partial class WelcomeForm : Form
    {
        // Information strings that the program uses to describe the actions that buttons do.
        private static readonly string[] MessageStrings =
        {
            "Create a new diagram.",
            "About the program and used libraries, as well as the help pages.",
            "Exit the program."
        };

        // Keeps track of which button is being hovered over.
        private bool SelectedNewFile, SelectedAbout, SelectedExit;

        public WelcomeForm()
        {
            SelectedNewFile = false;
            SelectedAbout = false;
            SelectedExit = false;

            InitializeComponent();

            RefreshLabel();
        }

        /*
         * For each button, the MouseMove event describes what occurs when the mouse moves over the button,
         * the MouseLeave event desribes what occurs when the mouse leaves the button,
         * and Click describes what occurs when the button is clicked.
         */

        #region About Button
        private void AboutButton_MouseMove(object sender, MouseEventArgs e)
        {
            SelectedAbout = true;
            RefreshLabel();
        }

        private void AboutButton_MouseLeave(object sender, EventArgs e)
        {
            SelectedAbout = false;
            RefreshLabel();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            Program.AForm.ShowDialog();
        }
        #endregion

        #region Exit Button
        private void ExitButton_MouseMove(object sender, MouseEventArgs e)
        {
            SelectedExit = true;
            RefreshLabel();
        }

        private void ExitButton_MouseLeave(object sender, EventArgs e)
        {
            SelectedExit = false;
            RefreshLabel();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region New File Button
        private void NewFileButton_MouseMove(object sender, MouseEventArgs e)
        {
            SelectedNewFile = true;
            RefreshLabel();
        }

        private void NewFileButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.DForm.ShowDialog();
            this.Close();
        }

        private void NewFileButton_MouseLeave(object sender, EventArgs e)
        {
            SelectedNewFile = false;
            RefreshLabel();
        }
        #endregion

        /// <summary>
        /// Refreshes the text shown in the help label under the buttons.
        /// The text shown depends on the button which the mouse has moved over.
        /// </summary>
        private void RefreshLabel()
        {
            // This method changes the message as the mouse moves over / exits the areas of the buttons.
            if (SelectedNewFile)
            {
                AboutLabel.Text = MessageStrings[0];
            }
            else if (SelectedAbout)
            {
                AboutLabel.Text = MessageStrings[1];
            }
            else if (SelectedExit)
            {
                AboutLabel.Text = MessageStrings[2];
            }
            else
            {
                // Default text.
                AboutLabel.Text = "Hover over a button for more information.";
            }
        }
    }
}
