using System;
namespace Client.Windows
{
    public partial class Auth : Gtk.Window
    {
        public Auth() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }
   }
}
