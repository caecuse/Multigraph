using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultigraphEditor.Forms
{
    public partial class EditForm : Form
    {
        internal event EventHandler OnOk;
        public object DataObject { get; }

        public EditForm(object obj)
        {
            InitializeComponent();

            DataObject = obj;
            PropertyInfo[] properties = DataObject.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetCustomAttribute<ExcludeFromFormAttribute>() != null)
                {
                    continue;
                }
                DataInput.RowCount++;

                Label label = new Label();
                label.Text = property.Name;
                if (property.PropertyType == typeof(bool))
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Name = property.Name;
                    checkBox.Checked = (bool)property.GetValue(DataObject);
                    DataInput.Controls.Add(checkBox, 1, DataInput.RowCount - 1);
                }
                else
                {
                    TextBox textBox = new TextBox();
                    textBox.Name = property.Name;
                    textBox.Text = property.GetValue(DataObject)?.ToString();

                    DataInput.Controls.Add(textBox, 1, DataInput.RowCount - 1);
                }

                DataInput.Controls.Add(label, 0, DataInput.RowCount - 1);
            }

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {

        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            foreach (Control control in DataInput.Controls)
            {
                if (control is TextBox tbox)
                {
                    PropertyInfo propertyInfo = DataObject.GetType().GetProperty(tbox.Name);
                    object value = Convert.ChangeType(tbox.Text, propertyInfo.PropertyType);
                    propertyInfo.SetValue(DataObject, value);
                }
                else if (control is CheckBox cbox)
                {
                    PropertyInfo propertyInfo = DataObject.GetType().GetProperty(cbox.Name);
                    propertyInfo.SetValue(DataObject, cbox.Checked);
                }
            }
            OnOk?.Invoke(this, EventArgs.Empty);
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
