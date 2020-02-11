using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ChemNotation
{
    public partial class AboutForm : Form
    {
        private static readonly ErrorLogger Log = new ErrorLogger(typeof(AboutForm));

        private List<Category> _Categories = null;
        private List<Category> Categories
        {
            get
            {
                if (_Categories == null)
                {
                    _Categories = new List<Category>();
                    using (StreamReader reader = File.OpenText(@"Resource/Help.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        _Categories = (List<Category>)serializer.Deserialize(reader, typeof(List<Category>));
                    }
                }
                return _Categories;
            }
        }

        public AboutForm()
        {
            InitializeComponent();

            foreach (var c in Categories)
            {
                CategoryList.Items.Add(c.Header);
            }
        }

        private void CategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string header = CategoryList.SelectedItem.ToString();
            string help = "";

            foreach (var cat in Categories)
            {
                if (cat.Header == header)
                {
                    foreach (var item in cat.Info)
                    {
                        help += item;
                        help += "\r\n";
                    }

                    break;
                }
            }

            InfoTextBox.Text = help;
        }

        private struct Category
        {
            public string Header;
            public string[] Info;

            public Category(string header, string[] info)
            {
                Header = header;
                Info = info;
            }
        }
    }
}
