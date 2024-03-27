using MultigraphEditor.Src.graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
            bool hasBox = false;

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
                else if (property.PropertyType == typeof(Font))
                {
                    ComboBox fontComboBox = new ComboBox();
                    fontComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    fontComboBox.Name = property.Name;
                    fontComboBox.Width = 200;
                    hasBox = true;

                    InstalledFontCollection fontsCollection = new InstalledFontCollection();
                    foreach (FontFamily fontFamily in fontsCollection.Families)
                    {
                        fontComboBox.Items.Add(fontFamily.Name);
                    }

                    Font currentFont = (Font)property.GetValue(DataObject);
                    if (currentFont != null)
                    {
                        fontComboBox.SelectedItem = currentFont.FontFamily.Name;
                    }

                    fontComboBox.SelectedIndexChanged += (sender, e) =>
                    {
                        string selectedFontName = fontComboBox.SelectedItem.ToString();
                        Font selectedFont = new Font(selectedFontName, currentFont.Size);
                        property.SetValue(DataObject, selectedFont);
                    };

                    DataInput.Controls.Add(fontComboBox, 1, DataInput.RowCount - 1);
                }
                else if (property.PropertyType == typeof(Color))
                {
                    Button colorButton = new Button();
                    colorButton.Name = property.Name;
                    colorButton.BackColor = (Color)property.GetValue(DataObject);
                    colorButton.Click += ColorButton_Click;
                    if (hasBox)
                    {
                        colorButton.Width = 200;
                    }
                    DataInput.Controls.Add(colorButton, 1, DataInput.RowCount - 1);
                }
                else
                {
                    TextBox textBox = new TextBox();
                    textBox.Name = property.Name;
                    textBox.Text = property.GetValue(DataObject)?.ToString();
                    if (hasBox)
                    {
                        textBox.Width = 200;
                    }
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

        private void ColorButton_Click(object sender, EventArgs e)
        {
            Button colorButton = sender as Button;
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorButton.BackColor = colorDialog.Color;
            }
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            foreach (Control control in DataInput.Controls)
            {
                PropertyInfo propertyInfo = DataObject.GetType().GetProperty(control.Name);
                if (control is TextBox tbox)
                {
                    if (IsValidInput(tbox.Text, propertyInfo.PropertyType))
                    {
                        object value = Convert.ChangeType(tbox.Text, propertyInfo.PropertyType);
                        propertyInfo.SetValue(DataObject, value);
                    }
                    else
                    {
                        MessageBox.Show("Invalid input for " + propertyInfo.Name);
                        return;
                    }
                }
                else if (control is CheckBox cbox)
                {
                    propertyInfo.SetValue(DataObject, cbox.Checked);
                }
                else if (control is Button colorButton)
                {
                    propertyInfo.SetValue(DataObject, colorButton.BackColor);
                }
                else if (control is ComboBox comboBox)
                {
                    propertyInfo.SetValue(DataObject, new Font(comboBox.SelectedItem.ToString(), 9));
                }
            }
            OnOk?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsValidInput(string input, Type targetType)
        {
            try
            {
                Convert.ChangeType(input, targetType);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}