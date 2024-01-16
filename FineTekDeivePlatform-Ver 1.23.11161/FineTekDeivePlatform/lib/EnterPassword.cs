using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fk_lib
{
    public partial class EnterPassword : Form
    {
        bool m_eng_mode = false;

        public EnterPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            check_password();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool get_open_eng_mode()
        {
            return m_eng_mode;
        }

        private void check_password()
        {
            if (textBox1.Text == "80132123")
                m_eng_mode = true;
            else
                m_eng_mode = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                button1.Focus();
                button1_Click(sender, e);
            }
        }

        
    }
}
