using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ytdl_sharp
{
    public class ytdl_Item
    {
        private string exec = "youtube-dl.exe";
        public string url;
        public string filename { get; private set; }
        public string param;
        public List<string> output { get; private set; }

        public ytdl_State status { get; private set; }

        public event EventHandler<ytdl_Item_EventArgs> OutputChangedEventHandler;

        #region Constructors
        public ytdl_Item(string uri)
        {
            url = uri;
            filename = "";
            param = "";
            status = ytdl_State.notstarted;
            output = new List<string>();
#if (DEBUG)
            output.Add($"Starte youtube-dl für '{uri}'...");
            #endif
        }
        #endregion

        #region Download async
        public async void download()
        {
            status &= ~ytdl_State.notstarted;
            status |= ytdl_State.running;
            int ret = await download_async();
            if (ret != 0) status |= ytdl_State.error;
            status &= ~ytdl_State.running;
        }

        private async Task<int> download_async()
        {
            TaskCompletionSource<int> ret = new TaskCompletionSource<int>();
            Process cmd = new Process();
            cmd.StartInfo.FileName = exec;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.EnableRaisingEvents = true;
            cmd.Exited += (s, ea) => {
                ret.SetResult(cmd.ExitCode);
                cmd.Dispose();
            };
            #region DEBUG
            #if DEBUG
                cmd.OutputDataReceived += (s, ea) => Debug.WriteLine(ea.Data);
                cmd.ErrorDataReceived += (s, ea) => Debug.WriteLine(ea.Data);
#endif
            #endregion
            cmd.OutputDataReceived += (s, ea) => output_add(ea.Data);
            //cmd.OutputDataReceived += (s, ea) =>
            //{
                //output.Add(ea.Data);
                //OnOutputChanged(new ytdl_Item_EventArgs(this));
            //};
            //cmd.ErrorDataReceived += (s, ea) => output.Add(ea.Data);
            cmd.ErrorDataReceived += (s, ea) => output_add(ea.Data);
            if (param == "")
            {
                cmd.StartInfo.Arguments = url;
            }
            else
            {
                cmd.StartInfo.Arguments = $"{param} \"{url}\"";
            }
            cmd.Start();
            cmd.BeginErrorReadLine();
            cmd.BeginOutputReadLine();
            await ret.Task;
            //cmd.WaitForExit();
            return ret.Task.Result;
        }
        #endregion

        protected void output_add(string s)
        {
            output.Add(s);
            //OnOutputChanged(new ytdl_Item_EventArgs(this));
            if (OutputChangedEventHandler != null)
            {
                OutputChangedEventHandler(this, new ytdl_Item_EventArgs(this));
            }

        }

        protected void OnOutputChanged(ytdl_Item_EventArgs e)
        {
            if (OutputChangedEventHandler != null)
            {
                OutputChangedEventHandler(this, e);
            }
        }


        public override string ToString()
        {
            return $"{status.ToString()}: {url}";
        }
    }

    #region ytdl_EventArgs
    public class ytdl_Item_EventArgs : EventArgs
    {
        public ytdl_Item ytdl_item { get; private set; }
        public ytdl_Item_EventArgs(ytdl_Item i)
        {
            ytdl_item = i;
        }
    }
    #endregion

    #region ytdl_State
    [Flags]
    public enum ytdl_State
    {
        notstarted = 0x1,
        running = 0x2,
        ready = 0x4,
        error = 0x8
    }
    #endregion
}
