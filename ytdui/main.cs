using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security;

namespace ytdui
{
    public partial class main : Form
    {
        youtube_dl_wrapper dl = new youtube_dl_wrapper();

        #region Test Kram
        string[] test_links = new string[] {
            "http://www.vevo.com/watch/bebe-rexha/I-Got-You/USWBV1600722",
            "https://www.youtube.com/watch?v=IcoqJCJlHbQ" };
        #endregion

        public main()
        {
            InitializeComponent();
            init_app();
            init_app_test();
        }

        private void init_app()
        {
            // Load Settings
            dl.proxy = ytdui.Properties.Settings.Default.proxy;
            dl.max_threads = Properties.Settings.Default.max_threads;
            dl.download_folder = Properties.Settings.Default.directory;
            textBox1.Text = dl.proxy;
        }

        private void refresh()
        {
            listBox1.DataSource = null;
            listBox1.DataSource = dl.urls;
        }

        private void init_app_test()
        {
            foreach(string i in test_links) comboBox1.Items.Add(i);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                dl.add_async(comboBox1.Text);
            }
            refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dl_item t = (sender as ListBox).SelectedValue as dl_item;
                listBox2.DataSource = null;
                listBox2.DataSource = t.output;
            } catch { }
            Debug.WriteLine(sender.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            foreach (string i in test_links) dl.add(i);
            refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dl.proxy=textBox1.Text;
            ytdui.Properties.Settings.Default.proxy = textBox1.Text;
            ytdui.Properties.Settings.Default.Save();

        }
    }
}
