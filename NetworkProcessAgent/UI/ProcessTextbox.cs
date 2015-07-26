using ElyDeckers.NetworkProcessAgent.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkProcessAgent.UI
{
    class ProcessTextbox : TextBox
    {
        private ProcessNameAutoCompleteSource _autoCompleteSource;

        public ProcessTextbox()
        {
            new PlaceholderTextBox(this) {
                PlaceholderText = "Name of the process to be killed when the selected interface comes online"
            };

            _autoCompleteSource = new ProcessNameAutoCompleteSource();
            new AutoCompleteTextBox(this, _autoCompleteSource);

            this.TextChanged += ProcessTextbox_TextChanged;
        }

        private void ProcessTextbox_TextChanged(object sender, EventArgs e)
        {
            _autoCompleteSource.UpdateProcessList();
        }

        private class AutoCompleteTextBox : TextBox
        {
            public AutoCompleteTextBox(TextBox textBox, AutoCompleteStringCollection collection)
            {
                textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox.AutoCompleteCustomSource = collection;
            }
        }
    }
}
