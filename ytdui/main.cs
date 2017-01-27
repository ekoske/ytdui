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
using ytdl_sharp;

namespace ytdui
{
    public partial class main : Form
    {
        ytdl dl = new ytdl();

        #region Test Kram
        string[] test_links = new string[] {
            "http://www.vevo.com/watch/bebe-rexha/I-Got-You/USWBV1600722",
            "https://www.youtube.com/watch?v=6Mgqbai3fKo",
            "https://www.youtube.com/watch?v=IcoqJCJlHbQ",
            "https://www.youtube.com/watch?v=gBAfejjUQoA"};
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
                dl.add(comboBox1.Text);
            }
            refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ytdl_Item t = (sender as ListBox).SelectedValue as ytdl_Item;
                listBox2.DataSource = null;
                listBox2.DataSource = t.output;
            } catch {
                Debug.WriteLine("EXEPTION: listBox1_SelectedIndexChanged");
            }
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
