using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ytdui
{
    class ClipboardMonitor : Control
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private const int WM_DRAWCLIPBOARD = 0x0308;
        private const int WM_CHANGECBCHAIN = 0x030D;
        private IntPtr _clipboardViewerNext;
        private IntPtr _Handle;
        public bool enabled = true;

        public event EventHandler<ClipboardChangedEventArgs> ClipboardChangedEventHandler;

        public ClipboardMonitor()
        {
            _Handle = Handle;
            _clipboardViewerNext = SetClipboardViewer(Handle);
            //_clipboardViewerNext = (IntPtr)SetClipboardViewer((int)_Handle);
        }
        public ClipboardMonitor(IntPtr h)
        {
            _Handle = h;
            _clipboardViewerNext = SetClipboardViewer(h);
            //_clipboardViewerNext = (IntPtr)SetClipboardViewer((int)this.Handle);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    if (enabled) OnClipboardChanged();
                    SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    break;
                case WM_CHANGECBCHAIN:
                    if (m.WParam == _clipboardViewerNext)
                        _clipboardViewerNext = m.LParam;
                    else
                        SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_clipboardViewerNext != null) ChangeClipboardChain(_Handle, _clipboardViewerNext);
        }

        public void OnClipboardChanged()
        {
            try
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (ClipboardChangedEventHandler != null)
                {
                    if (iData.GetDataPresent(DataFormats.Text))
                    {
                        string text = (string)iData.GetData(DataFormats.Text);
                        Debug.WriteLine(text);
                    }
                    ClipboardChangedEventHandler(this, new ClipboardChangedEventArgs(iData));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }

    public class ClipboardChangedEventArgs : EventArgs
    {
        public readonly IDataObject DataObject;
        public ClipboardChangedEventArgs(IDataObject dataObject)
        {
            DataObject = dataObject;
        }
    }
}
