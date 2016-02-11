using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTC
{
    public partial class TaskForm : Form
    {
        public TaskForm()
        {
            InitializeComponent();
        }

        public Action OnCancel = null;
        public bool CanClose = true;

        public void Reset()
        {
            progressBar1.Value = 0;
            textBox1.Clear();
        }

        public void SetProgress(int percent)
        {
            progressBar1.Value = percent;
        }

        public void AddMessage(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            textBox1.AppendText(msg + "\r\n");
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            if (OnCancel != null) OnCancel();
        }

        private void TaskForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CanClose;
        }
    }
}
