namespace Advanced_Combat_Tracker
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public static class ThreadInvokes
    {
        private static int stackLevel;

        public static void CheckboxSetChecked(Form parent, CheckBox target, bool isChecked)
        {
            if (target.InvokeRequired)
            {
                CheckboxSetCheckedCallback method = new CheckboxSetCheckedCallback(ThreadInvokes.CheckboxSetChecked);
                parent.Invoke(method, new object[] { parent, target, isChecked });
            }
            else
            {
                target.Checked = isChecked;
            }
        }

        public static void CheckedListBoxSetChecked(Form parent, CheckedListBox target, int itemIndex, bool isChecked)
        {
            if (target.InvokeRequired)
            {
                CheckedListBoxSetCheckedCallback method = new CheckedListBoxSetCheckedCallback(ThreadInvokes.CheckedListBoxSetChecked);
                parent.Invoke(method, new object[] { parent, target, itemIndex, isChecked });
            }
            else
            {
                target.SetItemChecked(itemIndex, isChecked);
            }
        }

        public static void ComboBoxSetSelectedIndex(Form parent, ComboBox target, int selectedIndex)
        {
            if (target.InvokeRequired)
            {
                ComboBoxSetSelectedIndexCallback method = new ComboBoxSetSelectedIndexCallback(ThreadInvokes.ComboBoxSetSelectedIndex);
                parent.Invoke(method, new object[] { parent, target, selectedIndex });
            }
            else
            {
                target.SelectedIndex = selectedIndex;
            }
        }

        public static string ControlGetText(Form parent, Control target)
        {
            if (target.InvokeRequired)
            {
                ControlGetTextCallback method = new ControlGetTextCallback(ThreadInvokes.ControlGetText);
                return (string) parent.Invoke(method, new object[] { parent, target });
            }
            return target.Text;
        }

        public static void ControlSetEnabled(Form parent, Control target, bool enabled)
        {
            if (target.InvokeRequired)
            {
                ControlSetEnabledCallback method = new ControlSetEnabledCallback(ThreadInvokes.ControlSetEnabled);
                parent.Invoke(method, new object[] { parent, target, enabled });
            }
            else
            {
                target.Enabled = enabled;
            }
        }

        public static void ControlSetText(Form parent, Control target, string text)
        {
            if (target.InvokeRequired)
            {
                ControlSetTextCallback method = new ControlSetTextCallback(ThreadInvokes.ControlSetText);
                parent.Invoke(method, new object[] { parent, target, text });
            }
            else
            {
                target.Text = text;
            }
        }

        public static void ControlSetVisible(Form parent, Control target, bool visible)
        {
            if (target.InvokeRequired)
            {
                ControlSetVisibleCallback method = new ControlSetVisibleCallback(ThreadInvokes.ControlSetVisible);
                parent.Invoke(method, new object[] { parent, target, visible });
            }
            else
            {
                target.Visible = visible;
            }
        }

        public static void ListboxAdd(Form parent, ListBox target, object item)
        {
            if (target.InvokeRequired)
            {
                ListboxAddCallback method = new ListboxAddCallback(ThreadInvokes.ListboxAdd);
                parent.Invoke(method, new object[] { parent, target, item });
            }
            else
            {
                target.Items.Add(item);
            }
        }

        public static void ListboxClear(Form parent, ListBox target)
        {
            if (target.InvokeRequired)
            {
                ListboxClearCallback method = new ListboxClearCallback(ThreadInvokes.ListboxClear);
                parent.Invoke(method, new object[] { parent, target });
            }
            else
            {
                target.Items.Clear();
            }
        }

        public static bool ListboxGetSelected(Form parent, ListBox target, int itemIndex)
        {
            if (target.InvokeRequired)
            {
                ListBoxGetSelectedCallback method = new ListBoxGetSelectedCallback(ThreadInvokes.ListboxGetSelected);
                return (bool) parent.Invoke(method, new object[] { parent, target, itemIndex });
            }
            return target.GetSelected(itemIndex);
        }

        public static void ListboxSetSelected(Form parent, ListBox target, int itemIndex, bool value)
        {
            if (target.InvokeRequired)
            {
                ListboxSetSelectedCallback method = new ListboxSetSelectedCallback(ThreadInvokes.ListboxSetSelected);
                parent.Invoke(method, new object[] { parent, target, itemIndex, value });
            }
            else
            {
                target.SetSelected(itemIndex, value);
            }
        }

        public static ListViewItem ListViewGetItem(Form parent, ListView target, int index)
        {
            if (target.InvokeRequired)
            {
                ListViewGetItemCallback method = new ListViewGetItemCallback(ThreadInvokes.ListViewGetItem);
                return (ListViewItem) parent.Invoke(method, new object[] { parent, target, index });
            }
            return target.Items[index];
        }

        public static bool ListViewGetItemChecked(Form parent, ListView target, int index)
        {
            if (target.InvokeRequired)
            {
                ListViewGetItemCheckedCallback method = new ListViewGetItemCheckedCallback(ThreadInvokes.ListViewGetItemChecked);
                return (bool) parent.Invoke(method, new object[] { parent, target, index });
            }
            return target.Items[index].Checked;
        }

        public static void ListViewInsert(Form parent, ListView target, int insertIndex, ListViewItem lvi)
        {
            if (target.InvokeRequired)
            {
                ListViewInsertCallback method = new ListViewInsertCallback(ThreadInvokes.ListViewInsert);
                parent.Invoke(method, new object[] { parent, target, insertIndex, lvi });
            }
            else
            {
                target.Items.Insert(insertIndex, lvi);
            }
        }

        public static void ProgressBarSetValue(Form parent, ProgressBar target, int value)
        {
            if (target.InvokeRequired)
            {
                ProgressBarSetValueCallback method = new ProgressBarSetValueCallback(ThreadInvokes.ProgressBarSetValue);
                parent.Invoke(method, new object[] { parent, target, value });
            }
            else if (value >= 0)
            {
                target.Style = ProgressBarStyle.Blocks;
                target.Value = value;
            }
            else
            {
                target.Style = ProgressBarStyle.Marquee;
            }
        }

        public static void RichTextBoxAppendDateTimeLine(Form parent, RichTextBox target, string text)
        {
            if (target.InvokeRequired && (stackLevel < 10))
            {
                stackLevel++;
                RichTextBoxAppendDateTimeLineCallback method = new RichTextBoxAppendDateTimeLineCallback(ThreadInvokes.RichTextBoxAppendDateTimeLine);
                parent.Invoke(method, new object[] { parent, target, text });
            }
            else
            {
                target.AppendText(string.Format("\n[{0}] {1}", DateTime.Now.ToLongTimeString(), text));
                target.ScrollToCaret();
                stackLevel = 0;
            }
        }

        public static void RichTextBoxAppendText(Form parent, RichTextBox target, string text)
        {
            if (target.InvokeRequired)
            {
                RichTextBoxAppendTextCallback method = new RichTextBoxAppendTextCallback(ThreadInvokes.RichTextBoxAppendText);
                parent.Invoke(method, new object[] { parent, target, text });
            }
            else
            {
                target.AppendText(text);
                target.ScrollToCaret();
            }
        }

        public static void RichTextBoxSetRtf(Form parent, RichTextBox target, string rtf)
        {
            if (target.InvokeRequired)
            {
                RichTextBoxSetRtfCallback method = new RichTextBoxSetRtfCallback(ThreadInvokes.RichTextBoxSetRtf);
                parent.Invoke(method, new object[] { parent, target, rtf });
            }
            else
            {
                target.Rtf = rtf;
            }
        }

        public static void TreeViewClear(Form parent, TreeView target)
        {
            if (target.InvokeRequired)
            {
                TreeViewClearCallback method = new TreeViewClearCallback(ThreadInvokes.TreeViewClear);
                parent.Invoke(method, new object[] { parent, target });
            }
            else
            {
                target.Nodes.Clear();
            }
        }

        private delegate bool CheckboxGetCheckedCallback(Form parent, CheckBox target, bool isChecked);

        private delegate void CheckboxSetCheckedCallback(Form parent, CheckBox target, bool isChecked);

        private delegate void CheckedListBoxSetCheckedCallback(Form parent, CheckedListBox target, int itemIndex, bool isChecked);

        private delegate void ComboBoxSetSelectedIndexCallback(Form parent, ComboBox target, int selectedIndex);

        private delegate string ControlGetTextCallback(Form parent, Control target);

        private delegate void ControlSetEnabledCallback(Form parent, Control target, bool enabled);

        private delegate void ControlSetTextCallback(Form parent, Control target, string text);

        private delegate void ControlSetVisibleCallback(Form parent, Control target, bool visible);

        private delegate void ListboxAddCallback(Form parent, ListBox target, object item);

        private delegate void ListboxClearCallback(Form parent, ListBox target);

        private delegate bool ListBoxGetSelectedCallback(Form parent, ListBox target, int itemIndex);

        private delegate void ListboxSetSelectedCallback(Form parent, ListBox target, int itemIndex, bool value);

        private delegate ListViewItem ListViewGetItemCallback(Form parent, ListView target, int index);

        private delegate bool ListViewGetItemCheckedCallback(Form parent, ListView target, int index);

        private delegate void ListViewInsertCallback(Form parent, ListView target, int insertIndex, ListViewItem lvi);

        private delegate void ProgressBarSetValueCallback(Form parent, ProgressBar target, int Value);

        private delegate void RichTextBoxAppendDateTimeLineCallback(Form parent, RichTextBox target, string text);

        private delegate void RichTextBoxAppendTextCallback(Form parent, RichTextBox target, string text);

        private delegate void RichTextBoxSetRtfCallback(Form parent, RichTextBox target, string rtf);

        private delegate void TreeViewClearCallback(Form parent, TreeView target);
    }
}

