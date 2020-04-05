using System;
namespace Client.Windows
{
    public partial class Users : Gtk.Window
    {
        public Users() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }
    }
}
