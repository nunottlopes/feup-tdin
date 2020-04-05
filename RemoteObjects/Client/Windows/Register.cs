using System;
namespace Client.Windows
{
    public partial class Register : Gtk.Window
    {
        public Register() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }
    }
}
