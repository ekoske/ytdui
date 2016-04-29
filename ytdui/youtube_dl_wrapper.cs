using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ytdui
{
    public class youtube_dl_wrapper
    {
        public List<dl_item> urls { get; private set; } = new List<dl_item>();
        public string proxy { get; set; } = "";
        public int max_threads { get; set; } = 2;
        public int running_threads { get; private set; } = 0;
        public string download_folder { get; set; } = "";

        public void add(string url)
        {
            Debug.WriteLine(string.Format("Adding '{0}' to downloadlist.", url));
            dl_item d= new dl_item(url);
            urls.Add(d);
            start_download(d);
            Debug.WriteLine(proxy);
        }

        public void start_download(dl_item d)
        {
            if(running_threads<max_threads)
            {
                running_threads++;
                d.status &= ~dl_state.notstarted;
                d.status |= dl_state.running;
                if (download(d) != 0)
                {
                    d.status |= dl_state.error;
                }
                d.status &= ~dl_state.running;
                running_threads--;
            }
        }

        #region Download not async
        public int download(dl_item d)
        {
            int ret=0;
            Process cmd = new Process();
            cmd.StartInfo.FileName = "youtube-dl.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            //cmd.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            cmd.StartInfo.UseShellExecute = false;
            if (proxy=="")
            {
                cmd.StartInfo.Arguments = d.url;
            } else {
                cmd.StartInfo.Arguments = String.Format("--proxy {0} {1}",proxy,d.url);
            }
            cmd.Start();
            StreamReader sr = cmd.StandardOutput;
            while (!sr.EndOfStream)
            {
                String s = sr.ReadLine();
                if (s != "")
                {
                    Debug.WriteLine(s);
                    d.output.Add(s);
                }
            }
            sr.ReadToEnd();
            cmd.WaitForExit();
            if (cmd.ExitCode != 0) ret = cmd.ExitCode;
            return ret;
        }
        #endregion
    }

    #region dl_item defintion
    public class dl_item
    {
        public string url;
        public string filename;
        public List<string> output;
        public dl_state status;

        public dl_item(string uri)
        {
            url = uri;
            filename = "";
            status = dl_state.notstarted;
            output = new List<string>();
#if(DEBUG)
            output.Add(String.Format("Starte youtube-dl für '{0}'...", uri));
#endif
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}",status.ToString(),url);
        }
    }

    [Flags]
    public enum dl_state
    {
        notstarted = 0x1,
        running = 0x2,
        ready = 0x4,
        error = 0x8
    }
    #endregion
}
