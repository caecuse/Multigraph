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
    public partial class EdgeForm : Form
    {
        internal event EventHandler OnOk;
        public object DataObject { get; }

        public EdgeForm(object obj)
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
                BtnLayoutPanel.RowCount++;

                Label label = new Label();
                label.Text = property.Name;
                if (property.PropertyType == typeof(bool))
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Name = property.Name;
                    BtnLayoutPanel.Controls.Add(checkBox, 1, BtnLayoutPanel.RowCount - 1);
                }
                else
                {
                    TextBox textBox = new TextBox();
                    textBox.Name = property.Name;

                    BtnLayoutPanel.Controls.Add(textBox, 1, BtnLayoutPanel.RowCount - 1);
                }

                BtnLayoutPanel.Controls.Add(label, 0, BtnLayoutPanel.RowCount - 1);
            }

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false; 
            MinimizeBox = false;
        }

        private void EdgeForm_Load(object sender, EventArgs e)
        {

        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            foreach (Control control in BtnLayoutPanel.Controls)
            {
                if (control is TextBox tbox)
                {
                    if (!string.IsNullOrEmpty(tbox.Text))
                    {
                        PropertyInfo propertyInfo = DataObject.GetType().GetProperty(tbox.Name);
                        object value = Convert.ChangeType(tbox.Text, propertyInfo.PropertyType);
                        propertyInfo.SetValue(DataObject, value);
                    }
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
