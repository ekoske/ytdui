using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ytdui
{
    public partial class main : Form
    {
        youtube_dl_wrapper dl = new youtube_dl_wrapper();

        public main()
        {
            InitializeComponent();
            //init_app();
            init_app_test();
        }

        private void init_app()
        {
            throw new NotImplementedException();
        }

        private void init_app_test()
        {
            comboBox1.Items.Add("http://www.vevo.com/watch/taylor-swift/Shake-It-Off/USCJY1431460");
            comboBox1.Items.Add("http://www.vevo.com/watch/taylor-swift/Bad-Blood/USCJY1531563");
            comboBox1.Items.Add("http://www.vevo.com/watch/grimes/Kill-V-Maim/GB2871500078");
            comboBox1.Items.Add("http://www.vevo.com/watch/grimes/Flesh-without-Blood-Life-in-the-Vivid-Dream/GB2871500053");        
            comboBox1.Items.Add("http://www.vevo.com/watch/mark-forster/Au-Revoir/DEQ321400099");
            comboBox1.Items.Add("https://www.youtube.com/watch?v=IcoqJCJlHbQ");
            //comboBox1.Items.Add("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dl.add(comboBox1.SelectedItem.ToString());
        }
    }
}
