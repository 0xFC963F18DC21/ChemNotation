using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkiaSharp;
using ChemNotation.DiagramObjects;

namespace ChemNotation
{
    public partial class EditingForm : Form
    {
        private DiagramObject Editable { get; set; }
        private Dictionary<string, object> Properties { get; set; }
        private int ID { get; set; }
        private DiagramObject.ObjectTypeID ObjectType { get; set; }
        private DiagramObject ObjectLocal { get; set; }

        private EditingForm()
        {
            // This argumentless constructor should not be used.
            InitializeComponent();
        }

        public EditingForm(DiagramObject obj) : this()
        {
            // Grab the properties of the object.
            Properties = new Dictionary<string, object>();
            var p = obj.GetEditableParameters();
            Editable = obj;

            foreach (string key in p.Keys) Properties.Add(key, p[key]);

            foreach (string key in Properties.Keys)
            {
                PropertyList.Items.Add(key);
            }

            ObjectLocal = obj;
            ID = obj.DiagramID;
            ObjectType = obj.ObjectID;
            Header.Text = $"Editing object with ID {ID} ({ObjectType.ToString()})";
        }

        private void PropertyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string prop = PropertyList.SelectedItem.ToString();

            try
            {
                PropertyValue.Text = Properties[prop].ToString();
            }
            catch (Exception exc)
            {
                ErrorLogger.ShowErrorMessageBox(exc);
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            string prop = PropertyList.SelectedItem.ToString();

            try
            {
                if (new string[] { "Red", "Green", "Blue", "Alpha" }.Contains(prop))
                {
                    Properties[prop] = byte.Parse(PropertyValue.Text);
                    SKColor c = new SKColor((byte)Properties["Red"], (byte)Properties["Green"], (byte)Properties["Blue"], (byte)Properties["Alpha"]);
                }
                else if (Properties[prop].GetType() == typeof(int))
                {
                    Properties[prop] = int.Parse(PropertyValue.Text);
                }
                else if (Properties[prop].GetType() == typeof(float))
                {
                    Properties[prop] = float.Parse(PropertyValue.Text);
                }
                else
                {
                    Properties[prop] = PropertyValue.Text;
                }


                Editable.ReplaceInternalParameters(Properties);
            }
            catch (Exception exc)
            {
                ErrorLogger.ShowErrorMessageBox(exc);
            }

            Program.DForm.UpdateScreen(true);
            Program.DForm.MessageBoxText = $"Edited {prop} object ID {ID} ({ObjectType.ToString()}).";

            Properties = ObjectLocal.GetEditableParameters();
        }

        private void EditingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.DForm.MessageBoxText = $"Finished editing object with ID {ID} ({ObjectType.ToString()}).";
            if (Program.DForm.SelectedObject == ObjectLocal) Program.DForm.SelectedObject = null;
        }
    }
}
