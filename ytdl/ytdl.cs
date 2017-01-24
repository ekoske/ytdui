using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ytdl_sharp
{
    public class ytdl
    {
        public List<ytdl_Item> urls { get; private set; } = new List<ytdl_Item>();
        public string proxy { get; set; } = "";
        public int max_threads { get; set; } = 2;
        public int running_threads { get; private set; } = 0;
        public string download_folder { get; set; } = "";

        public void add(string url)
        {
            Debug.WriteLine($"Adding '{url}' to downloadlist.");
            ytdl_Item d = new ytdl_Item(url);
            if(proxy != "") d.param = $"--proxy {proxy}";
            urls.Add(d);
            d.download();
            Debug.WriteLine(proxy);
        }

        //public async void add_async(string url)
        //{
        //    Debug.WriteLine($"Adding '{url}' to downloadlist.");
        //    ytdl_Item d = new ytdl_Item(url);
        //    urls.Add(d);
        //    await start_download_async(d);
        //    Debug.WriteLine(proxy);
        //}

        //public void start_download(ytdl_Item d)
        //{
        //    if (running_threads < max_threads)
        //    {
        //        running_threads++;
        //        d.status &= ~ytdl_State.notstarted;
        //        d.status |= ytdl_State.running;
        //        if (download(d) != 0)
        //        {
        //            d.status |= ytdl_State.error;
        //        }
        //        d.status &= ~ytdl_State.running;
        //        running_threads--;
        //    }
        //}

        //public async Task start_download_async(ytdl_Item d)
        //{
        //    if (running_threads < max_threads)
        //    {
        //        running_threads++;
        //        d.status &= ~ytdl_State.notstarted;
        //        d.status |= ytdl_State.running;
        //        int r = await download_async(d);
        //        if (r != 0)
        //        {
        //            d.status |= ytdl_State.error;
        //        }
        //        d.status &= ~ytdl_State.running;
        //        running_threads--;
        //    }
        //}

        #region Old_stuff

        //#region Download async
        //private async Task<int> download_async(ytdl_Item d)
        //{
        //    TaskCompletionSource<int> ret = new TaskCompletionSource<int>();
        //    Process cmd = new Process();
        //    cmd.StartInfo.FileName = "youtube-dl.exe";
        //    cmd.StartInfo.RedirectStandardInput = true;
        //    cmd.StartInfo.RedirectStandardOutput = true;
        //    cmd.StartInfo.CreateNoWindow = true;
        //    cmd.StartInfo.UseShellExecute = false;
        //    cmd.Exited += (s, ea) => ret.SetResult(cmd.ExitCode);
        //    cmd.OutputDataReceived += (s, ea) => Debug.WriteLine(ea.Data);
        //    if (proxy == "")
        //    {
        //        cmd.StartInfo.Arguments = d.url;
        //    }
        //    else
        //    {
        //        cmd.StartInfo.Arguments = $"--proxy {proxy} {d.url}";
        //    }
        //    cmd.Start();
        //    cmd.BeginOutputReadLine();
        //    await ret.Task;
        //    return ret.Task.Result;
        //}
        //#endregion

        //#region Download not async
        //public int download(ytdl_Item d)
        //{
        //    int ret = 0;
        //    Process cmd = new Process();
        //    cmd.StartInfo.FileName = "youtube-dl.exe";
        //    cmd.StartInfo.RedirectStandardInput = true;
        //    cmd.StartInfo.RedirectStandardOutput = true;
        //    cmd.StartInfo.CreateNoWindow = true;
        //    //cmd.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //    cmd.StartInfo.UseShellExecute = false;
        //    if (proxy == "")
        //    {
        //        cmd.StartInfo.Arguments = d.url;
        //    }
        //    else
        //    {
        //        cmd.StartInfo.Arguments = $"--proxy {proxy} {d.url}";
        //    }
        //    cmd.Start();
        //    StreamReader sr = cmd.StandardOutput;
        //    while (!sr.EndOfStream)
        //    {
        //        String s = sr.ReadLine();
        //        if (s != "")
        //        {
        //            Debug.WriteLine(s);
        //            d.output.Add(s);
        //        }
        //    }
        //    sr.ReadToEnd();
        //    cmd.WaitForExit();
        //    if (cmd.ExitCode != 0) ret = cmd.ExitCode;
        //    return ret;
        //}
        //#endregion

        //public void add(string url)
        //{
        //    Debug.WriteLine($"Adding '{url}' to downloadlist.");
        //    ytdl_Item d = new ytdl_Item(url);
        //    urls.Add(d);
        //    start_download(d);
        //    Debug.WriteLine(proxy);
        //}
        #endregion
    }

}