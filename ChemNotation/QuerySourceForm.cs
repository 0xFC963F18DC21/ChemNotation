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
    public partial class QuerySourceForm : Form
    {
        private string Query { get; set; } = "";

        public QuerySourceForm()
        {
            InitializeComponent();
        }

        private void ButtonPubChem_Click(object sender, EventArgs e)
        {
            string finalQuery = $"https://pubchem.ncbi.nlm.nih.gov/#query={Query.ToString()}";
            System.Diagnostics.Process.Start(finalQuery);

            Close();
        }

        private void ButtonChemSpider_Click(object sender, EventArgs e)
        {
            string finalQuery = $"http://www.chemspider.com/Search.aspx?q={Query.ToString()}";
            System.Diagnostics.Process.Start(finalQuery);

            Close();
        }

        public void ChooseSource(string query)
        {
            Query = query;
            label1.Text = $"Please choose a chemical database website to query for:\n{query}";

            ShowDialog();
        }
    }
}
