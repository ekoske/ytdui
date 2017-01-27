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


        #region Old_stuff
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
        #endregion
    }

}