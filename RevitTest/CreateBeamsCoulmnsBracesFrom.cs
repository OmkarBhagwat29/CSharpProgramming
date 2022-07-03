using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevitTest
{
    public partial class CreateBeamsCoulmnsBracesFrom : Form
    {
        CreateBeamsColumnsBraces data;

        public CreateBeamsCoulmnsBracesFrom()
        {
            InitializeComponent();
        }

        public CreateBeamsCoulmnsBracesFrom(CreateBeamsColumnsBraces _data)
        {
            data = _data;

            InitializeComponent();

            this.Load += CreateBeamsCoulmnsBracesFrom_Load;

        }

        private void TextBoxRefresh()
        {
            this.XTextBox.Text = "2";
            this.YTextBox.Text = "2";
            this.DistanceTextBox.Text = 20.0.ToString("0.0");
            this.floornumberTextBox.Text = "1";
        }

        private void CreateBeamsCoulmnsBracesFrom_Load(object sender, EventArgs e)
        {
            this.TextBoxRefresh();
            bool notloadSymbol = false;

            if (data.ColumnMaps.Count == 0)
            {
                TaskDialog.Show("Revit", "No Structural Columns family is loaded in the project");
                notloadSymbol = true;
            }

            if (data.BeamMaps.Count == 1)
            {
                TaskDialog.Show("Revit", "No Structural Framing family is loaded in the project");
                notloadSymbol = true;
            }

            if (notloadSymbol)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            this.columnComboBox.DataSource = data.ColumnMaps;
            this.columnComboBox.DisplayMember = "SymbolName";
            this.columnComboBox.ValueMember = "ElementType";

            this.beamComboBox.DataSource = data.BeamMaps;
            this.beamComboBox.DisplayMember = "SymbolName";
            this.beamComboBox.ValueMember = "ElementType";

            this.braceComboBox.DataSource = data.BraceMaps;
            this.braceComboBox.DisplayMember = "SymbolName";
            this.braceComboBox.ValueMember = "ElementType";
        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int xNumber = int.Parse(this.XTextBox.Text);
                int yNumber = int.Parse(this.YTextBox.Text);
                double distance = double.Parse(this.DistanceTextBox.Text);
                object columnType = columnComboBox.SelectedValue;
                object beamType = beamComboBox.SelectedValue;
                object braceType = braceComboBox.SelectedValue;
                int floorNumber = int.Parse(floornumberTextBox.Text);
            }
            catch (Exception ex)
            {
                
            }

        }
    }
}
