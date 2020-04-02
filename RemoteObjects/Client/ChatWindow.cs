using System;
namespace Client
{
    public partial class ChatWindow : Gtk.Window
    {
        public ChatWindow() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }
    }
}
