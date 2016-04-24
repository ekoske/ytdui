using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ytdui
{
    public class youtube_dl_wrapper
    {
        public List<dl_item> urls=new List<dl_item>();

        public void add(string url)
        {
            dl_item i = new dl_item(url);
            urls.Add(i);
        }
    }


    public class dl_item
    {
        public string url;
        public List<string> output;
        public int status;

        public dl_item(string uri)
        {
            url = uri;
            status = 0;
            output = new List<string>();
        }
    }
}
