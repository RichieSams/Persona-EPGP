namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;

    public class SettingsSerializer
    {
        private List<string> boolItems = new List<string>();
        private Dictionary<string, ComboBoxStyle> cboxStyles = new Dictionary<string, ComboBoxStyle>();
        private Dictionary<string, Control> controlItems = new Dictionary<string, Control>();
        private List<string> dirinfoItems = new List<string>();
        private List<string> intItems = new List<string>();
        private object parent;
        private List<string> strItems = new List<string>();

        public SettingsSerializer(object ParentSettingsClass)
        {
            this.parent = ParentSettingsClass;
        }

        public void AddBooleanSetting(string SettingName)
        {
            if (!this.boolItems.Contains(SettingName))
            {
                this.boolItems.Add(SettingName);
            }
        }

        public void AddControlSetting(string ControlName, Control ControlToSerialize)
        {
            System.Type type = ControlToSerialize.GetType();
            switch (type.Name)
            {
                case "CheckBox":
                case "RadioButton":
                case "NumericUpDown":
                case "ListBox":
                case "Panel":
                case "TextBox":
                case "ComboBox":
                case "Form":
                case "TrackBar":
                case "CheckedListBox":
                case "Button":
                case "PictureBox":
                case "ColorControl":
                case "FontColorControl":
                    break;

                default:
                    if (type.BaseType.Name != "Form")
                    {
                        throw new ArgumentException("Cannot serialize control of type " + type.FullName);
                    }
                    break;
            }
            if (!this.controlItems.ContainsKey(ControlName))
            {
                this.controlItems.Add(ControlName, ControlToSerialize);
            }
        }

        public void AddDirectoryInfoSetting(string SettingName)
        {
            if (!this.dirinfoItems.Contains(SettingName))
            {
                this.dirinfoItems.Add(SettingName);
            }
        }

        public void AddIntSetting(string SettingName)
        {
            if (!this.intItems.Contains(SettingName))
            {
                this.intItems.Add(SettingName);
            }
        }

        public void AddStringSetting(string SettingName)
        {
            if (!this.strItems.Contains(SettingName))
            {
                this.strItems.Add(SettingName);
            }
        }

        public void ExportToXml(XmlTextWriter xWriter)
        {
            foreach (KeyValuePair<string, Control> pair in this.controlItems)
            {
                try
                {
                    ListBox box2;
                    int num;
                    Form form;
                    CheckedListBox box5;
                    int num2;
                    System.Type type = pair.Value.GetType();
                    switch (type.Name)
                    {
                        case "CheckBox":
                        {
                            CheckBox box = (CheckBox) pair.Value;
                            xWriter.WriteStartElement("CheckBox");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            xWriter.WriteAttributeString("Value", box.Checked.ToString());
                            xWriter.WriteEndElement();
                            continue;
                        }
                        case "RadioButton":
                        {
                            RadioButton button = (RadioButton) pair.Value;
                            xWriter.WriteStartElement("RadioButton");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            xWriter.WriteAttributeString("Value", button.Checked.ToString());
                            xWriter.WriteEndElement();
                            continue;
                        }
                        case "NumericUpDown":
                        {
                            NumericUpDown down = (NumericUpDown) pair.Value;
                            xWriter.WriteStartElement("NumericUpDown");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            xWriter.WriteAttributeString("Value", down.Value.ToString());
                            xWriter.WriteEndElement();
                            continue;
                        }
                        case "ListBox":
                            box2 = (ListBox) pair.Value;
                            xWriter.WriteStartElement("ListBox");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            num = 0;
                            goto Label_02A4;

                        case "Panel":
                        {
                            Panel panel = (Panel) pair.Value;
                            xWriter.WriteStartElement("Panel");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            xWriter.WriteAttributeString("W", panel.Size.Width.ToString());
                            xWriter.WriteAttributeString("H", panel.Size.Height.ToString());
                            xWriter.WriteEndElement();
                            continue;
                        }
                        case "TextBox":
                        {
                            TextBox box3 = (TextBox) pair.Value;
                            xWriter.WriteStartElement("TextBox");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            xWriter.WriteAttributeString("Value", box3.Text);
                            xWriter.WriteEndElement();
                            continue;
                        }
                        case "ComboBox":
                        {
                            ComboBox box4 = (ComboBox) pair.Value;
                            xWriter.WriteStartElement("ComboBox");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            xWriter.WriteAttributeString("Value", box4.Text);
                            xWriter.WriteEndElement();
                            continue;
                        }
                        case "Form":
                            goto Label_03CD;

                        case "TrackBar":
                        {
                            TrackBar bar = (TrackBar) pair.Value;
                            xWriter.WriteStartElement("TrackBar");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            xWriter.WriteAttributeString("Value", bar.Value.ToString());
                            xWriter.WriteEndElement();
                            continue;
                        }
                        case "CheckedListBox":
                            box5 = (CheckedListBox) pair.Value;
                            xWriter.WriteStartElement("CheckedListBox");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            num2 = 0;
                            goto Label_0626;

                        case "Button":
                        {
                            Button button2 = (Button) pair.Value;
                            xWriter.WriteStartElement("Button");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            xWriter.WriteAttributeString("Text", button2.Text);
                            xWriter.WriteAttributeString("Font", button2.Font.Name);
                            xWriter.WriteAttributeString("Style", button2.Font.Style.ToString());
                            xWriter.WriteAttributeString("Size", button2.Font.Size.ToString());
                            xWriter.WriteEndElement();
                            continue;
                        }
                        case "PictureBox":
                        {
                            PictureBox box6 = (PictureBox) pair.Value;
                            xWriter.WriteStartElement("PictureBox");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            xWriter.WriteAttributeString("Color", box6.BackColor.ToArgb().ToString());
                            xWriter.WriteEndElement();
                            continue;
                        }
                        case "ColorControl":
                        {
                            ColorControl control = (ColorControl) pair.Value;
                            xWriter.WriteStartElement("ColorControl");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            xWriter.WriteAttributeString("Color", control.ForeColorSetting.ToArgb().ToString());
                            xWriter.WriteEndElement();
                            continue;
                        }
                        case "FontColorControl":
                        {
                            FontColorControl control2 = (FontColorControl) pair.Value;
                            xWriter.WriteStartElement("FontColorControl");
                            xWriter.WriteAttributeString("Name", pair.Key);
                            int num3 = control2.ForeColorSetting.ToArgb();
                            xWriter.WriteAttributeString("ForeColor", num3.ToString());
                            num3 = control2.BackColorSetting.ToArgb();
                            xWriter.WriteAttributeString("BackColor", num3.ToString());
                            xWriter.WriteAttributeString("Font", control2.FontSetting.Name);
                            xWriter.WriteAttributeString("Style", control2.FontSetting.Style.ToString());
                            xWriter.WriteAttributeString("Size", control2.FontSetting.Size.ToString());
                            xWriter.WriteEndElement();
                            continue;
                        }
                        default:
                            goto Label_0868;
                    }
                Label_0274:
                    xWriter.WriteStartElement("Item");
                    xWriter.WriteString((string) box2.Items[num]);
                    xWriter.WriteEndElement();
                    num++;
                Label_02A4:
                    if (num < box2.Items.Count)
                    {
                        goto Label_0274;
                    }
                    xWriter.WriteEndElement();
                    continue;
                Label_03CD:
                    form = (Form) pair.Value;
                    xWriter.WriteStartElement("Form");
                    xWriter.WriteAttributeString("Name", pair.Key);
                    if (form.WindowState == FormWindowState.Normal)
                    {
                        xWriter.WriteAttributeString("X", form.Location.X.ToString());
                        xWriter.WriteAttributeString("Y", form.Location.Y.ToString());
                        xWriter.WriteAttributeString("W", form.Size.Width.ToString());
                        xWriter.WriteAttributeString("H", form.Size.Height.ToString());
                    }
                    else
                    {
                        xWriter.WriteAttributeString("X", form.RestoreBounds.Location.X.ToString());
                        xWriter.WriteAttributeString("Y", form.RestoreBounds.Location.Y.ToString());
                        xWriter.WriteAttributeString("W", form.RestoreBounds.Size.Width.ToString());
                        xWriter.WriteAttributeString("H", form.RestoreBounds.Size.Height.ToString());
                    }
                    xWriter.WriteEndElement();
                    continue;
                Label_05D9:
                    xWriter.WriteStartElement("Item");
                    xWriter.WriteAttributeString("Checked", box5.GetItemChecked(num2).ToString());
                    xWriter.WriteString((string) box5.Items[num2]);
                    xWriter.WriteEndElement();
                    num2++;
                Label_0626:
                    if (num2 < box5.Items.Count)
                    {
                        goto Label_05D9;
                    }
                    xWriter.WriteEndElement();
                    continue;
                Label_0868:
                    if (type.BaseType.Name == "Form")
                    {
                        goto Label_03CD;
                    }
                    continue;
                }
                catch
                {
                    continue;
                }
            }
            foreach (string str in this.strItems)
            {
                try
                {
                    xWriter.WriteStartElement("String");
                    xWriter.WriteAttributeString("Name", str);
                    xWriter.WriteAttributeString("Value", (string) this.GetFieldFromParent(str));
                    xWriter.WriteEndElement();
                    continue;
                }
                catch
                {
                    continue;
                }
            }
            foreach (string str2 in this.intItems)
            {
                try
                {
                    xWriter.WriteStartElement("Int32");
                    xWriter.WriteAttributeString("Name", str2);
                    xWriter.WriteAttributeString("Value", ((int) this.GetFieldFromParent(str2)).ToString());
                    xWriter.WriteEndElement();
                    continue;
                }
                catch
                {
                    continue;
                }
            }
            foreach (string str3 in this.boolItems)
            {
                try
                {
                    xWriter.WriteStartElement("Boolean");
                    xWriter.WriteAttributeString("Name", str3);
                    xWriter.WriteAttributeString("Value", ((bool) this.GetFieldFromParent(str3)).ToString());
                    xWriter.WriteEndElement();
                    continue;
                }
                catch
                {
                    continue;
                }
            }
            foreach (string str4 in this.dirinfoItems)
            {
                DirectoryInfo fieldFromParent = (DirectoryInfo) this.GetFieldFromParent(str4);
                if (fieldFromParent == null)
                {
                    fieldFromParent = new DirectoryInfo(@"C:\");
                }
                xWriter.WriteStartElement("DirectoryInfo");
                xWriter.WriteAttributeString("Name", str4);
                xWriter.WriteAttributeString("Value", fieldFromParent.FullName);
                xWriter.WriteEndElement();
            }
        }

        public void FinializeComboBoxes()
        {
            foreach (KeyValuePair<string, ComboBoxStyle> pair in this.cboxStyles)
            {
                try
                {
                    ComboBox box = (ComboBox) this.controlItems[pair.Key];
                    string text = box.Text;
                    box.DropDownStyle = pair.Value;
                    for (int i = 0; i < box.Items.Count; i++)
                    {
                        if (box.Items[i].ToString() == text)
                        {
                            box.SelectedIndex = i;
                        }
                    }
                    continue;
                }
                catch
                {
                    continue;
                }
            }
        }

        private string GetAttributeString(XmlNode Node, string AttributeName)
        {
            string str;
            try
            {
                str = Node.Attributes[AttributeName].Value;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("The attribute, " + AttributeName + " does not exist.");
            }
            return str;
        }

        private object GetFieldFromParent(string Name)
        {
            System.Type type = this.parent.GetType();
            FieldInfo field = type.GetField(Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
            {
                return field.GetValue(this.parent);
            }
            return type.GetProperty(Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(this.parent, null);
        }

        public int ImportFromXml(XmlTextReader xReader)
        {
            this.cboxStyles.Clear();
            int num = 0;
            XmlDocument document = new XmlDocument();
            string xml = xReader.ReadOuterXml();
            document.LoadXml(xml);
            foreach (XmlNode node in document.ChildNodes[0].ChildNodes)
            {
                try
                {
                    string attributeString;
                    string innerText;
                    string str4;
                    string str5;
                    int num2;
                    int num3;
                    int num4;
                    int num5;
                    Screen screen;
                    CheckedListBox box3;
                    List<string> list;
                    List<bool> list2;
                    int num8;
                    FontStyle style;
                    Font font;
                    FontColorControl control;
                    FontStyle style2;
                    Font font2;
                    Screen[] allScreens;
                    int num11;
                    switch (node.Name)
                    {
                        case "CheckBox":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Value");
                            ((CheckBox) this.controlItems[attributeString]).Checked = bool.Parse(innerText);
                            continue;
                        }
                        case "RadioButton":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Value");
                            ((RadioButton) this.controlItems[attributeString]).Checked = bool.Parse(innerText);
                            continue;
                        }
                        case "NumericUpDown":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Value");
                            ((NumericUpDown) this.controlItems[attributeString]).Value = decimal.Parse(innerText);
                            continue;
                        }
                        case "ListBox":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            ListBox box = (ListBox) this.controlItems[attributeString];
                            foreach (XmlNode node2 in node.ChildNodes)
                            {
                                innerText = node2.InnerText;
                                if (!box.Items.Contains(innerText))
                                {
                                    box.Items.Add(innerText);
                                }
                            }
                            continue;
                        }
                        case "Panel":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            str4 = this.GetAttributeString(node, "W");
                            str5 = this.GetAttributeString(node, "H");
                            ((Panel) this.controlItems[attributeString]).Size = new Size(int.Parse(str4), int.Parse(str5));
                            continue;
                        }
                        case "TextBox":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Value");
                            ((TextBox) this.controlItems[attributeString]).Text = innerText;
                            continue;
                        }
                        case "ComboBox":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Value");
                            ComboBox box2 = (ComboBox) this.controlItems[attributeString];
                            this.cboxStyles.Add(this.controlItems[attributeString].Name, box2.DropDownStyle);
                            box2.DropDownStyle = ComboBoxStyle.DropDown;
                            box2.Text = innerText;
                            continue;
                        }
                        case "Form":
                            num2 = 0x7fffffff;
                            num3 = 0x7fffffff;
                            num4 = 0;
                            num5 = 0;
                            allScreens = Screen.AllScreens;
                            num11 = 0;
                            goto Label_04A7;

                        case "TrackBar":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Value");
                            ((TrackBar) this.controlItems[attributeString]).Value = int.Parse(innerText);
                            continue;
                        }
                        case "CheckedListBox":
                            attributeString = this.GetAttributeString(node, "Name");
                            box3 = (CheckedListBox) this.controlItems[attributeString];
                            list = new List<string>();
                            list2 = new List<bool>();
                            num8 = 0;
                            goto Label_0659;

                        case "PictureBox":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Color");
                            ((PictureBox) this.controlItems[attributeString]).BackColor = Color.FromArgb(int.Parse(innerText));
                            continue;
                        }
                        case "Button":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            string str6 = this.GetAttributeString(node, "Text");
                            ((Button) this.controlItems[attributeString]).Text = str6;
                            switch (this.GetAttributeString(node, "Style"))
                            {
                                case "Bold":
                                    goto Label_07F6;

                                case "Italic":
                                    goto Label_07FB;

                                case "Strikeout":
                                    goto Label_0800;

                                case "Underline":
                                    goto Label_0805;
                            }
                            goto Label_080A;
                        }
                        case "ColorControl":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Color");
                            ((ColorControl) this.controlItems[attributeString]).ForeColorSetting = Color.FromArgb(int.Parse(innerText));
                            continue;
                        }
                        case "FontColorControl":
                            attributeString = this.GetAttributeString(node, "Name");
                            control = (FontColorControl) this.controlItems[attributeString];
                            control.ForeColorSetting = Color.FromArgb(int.Parse(this.GetAttributeString(node, "ForeColor")));
                            control.BackColorSetting = Color.FromArgb(int.Parse(this.GetAttributeString(node, "BackColor")));
                            switch (this.GetAttributeString(node, "Style"))
                            {
                                case "Bold":
                                    goto Label_093C;

                                case "Italic":
                                    goto Label_0941;

                                case "Strikeout":
                                    goto Label_0946;

                                case "Underline":
                                    goto Label_094B;
                            }
                            goto Label_0950;

                        case "String":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Value");
                            this.SetFieldOfParent(attributeString, innerText);
                            continue;
                        }
                        case "Int32":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Value");
                            this.SetFieldOfParent(attributeString, int.Parse(innerText));
                            continue;
                        }
                        case "Boolean":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            innerText = this.GetAttributeString(node, "Value");
                            this.SetFieldOfParent(attributeString, bool.Parse(innerText));
                            continue;
                        }
                        case "DirectoryInfo":
                        {
                            attributeString = this.GetAttributeString(node, "Name");
                            DirectoryInfo info = new DirectoryInfo(this.GetAttributeString(node, "Value"));
                            this.SetFieldOfParent(attributeString, info);
                            continue;
                        }
                        default:
                        {
                            continue;
                        }
                    }
                Label_0424:
                    screen = allScreens[num11];
                    if (screen.Bounds.X < num2)
                    {
                        num2 = screen.Bounds.X;
                    }
                    if (screen.Bounds.Y < num3)
                    {
                        num3 = screen.Bounds.Y;
                    }
                    num4 += screen.Bounds.Width;
                    num5 += screen.Bounds.Height;
                    num11++;
                Label_04A7:
                    if (num11 < allScreens.Length)
                    {
                        goto Label_0424;
                    }
                    int x = int.Parse(this.GetAttributeString(node, "X"));
                    int y = int.Parse(this.GetAttributeString(node, "Y"));
                    Rectangle workingArea = Screen.GetWorkingArea(new Point(x, y));
                    if ((x < num2) || (x > (num2 + num4)))
                    {
                        x = workingArea.Left;
                    }
                    if ((y < num3) || (y > (num3 + num5)))
                    {
                        y = workingArea.Top;
                    }
                    attributeString = this.GetAttributeString(node, "Name");
                    str4 = this.GetAttributeString(node, "W");
                    str5 = this.GetAttributeString(node, "H");
                    ((Form) this.controlItems[attributeString]).Left = x;
                    ((Form) this.controlItems[attributeString]).Top = y;
                    ((Form) this.controlItems[attributeString]).Width = int.Parse(str4);
                    ((Form) this.controlItems[attributeString]).Height = int.Parse(str5);
                    continue;
                Label_0629:
                    list.Add(box3.Items[num8].ToString());
                    list2.Add(box3.GetItemChecked(num8));
                    num8++;
                Label_0659:
                    if (num8 < box3.Items.Count)
                    {
                        goto Label_0629;
                    }
                    box3.Items.Clear();
                    foreach (XmlNode node3 in node.ChildNodes)
                    {
                        box3.Items.Add(node3.InnerText, bool.Parse(node3.Attributes["Checked"].Value));
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (!box3.Items.Contains(list[i]))
                        {
                            box3.Items.Add(list[i], list2[i]);
                        }
                    }
                    continue;
                Label_07F6:
                    style = FontStyle.Bold;
                    goto Label_080D;
                Label_07FB:
                    style = FontStyle.Italic;
                    goto Label_080D;
                Label_0800:
                    style = FontStyle.Strikeout;
                    goto Label_080D;
                Label_0805:
                    style = FontStyle.Underline;
                    goto Label_080D;
                Label_080A:
                    style = FontStyle.Regular;
                Label_080D:
                    font = new Font(this.GetAttributeString(node, "Font"), float.Parse(this.GetAttributeString(node, "Size")), style);
                    ((Button) this.controlItems[attributeString]).Font = font;
                    continue;
                Label_093C:
                    style2 = FontStyle.Bold;
                    goto Label_0953;
                Label_0941:
                    style2 = FontStyle.Italic;
                    goto Label_0953;
                Label_0946:
                    style2 = FontStyle.Strikeout;
                    goto Label_0953;
                Label_094B:
                    style2 = FontStyle.Underline;
                    goto Label_0953;
                Label_0950:
                    style2 = FontStyle.Regular;
                Label_0953:
                    font2 = new Font(this.GetAttributeString(node, "Font"), float.Parse(this.GetAttributeString(node, "Size")), style2);
                    control.FontSetting = font2;
                    continue;
                }
                catch (Exception exception)
                {
                    num++;
                    string moreInfo = string.Format("{0}\n\n{1}", node.OuterXml, exception.Message);
                    ActGlobals.oFormActMain.WriteExceptionLog(exception, moreInfo);
                    continue;
                }
            }
            return num;
        }

        public bool RemoveControlSetting(string ControlName)
        {
            if (this.controlItems.ContainsKey(ControlName))
            {
                this.controlItems.Remove(ControlName);
                return true;
            }
            return false;
        }

        private void SetFieldOfParent(string Name, object Value)
        {
            System.Type type = this.parent.GetType();
            FieldInfo field = type.GetField(Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(this.parent, Value);
            }
            else
            {
                type.GetProperty(Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(this.parent, Value, null);
            }
        }
    }
}

