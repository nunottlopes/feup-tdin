
// This file has been generated by the GUI designer. Do not modify.
namespace Client.Windows
{
	public partial class Register
	{
		private global::Gtk.Alignment alignment1;

		private global::Gtk.VBox vbox15;

		private global::Gtk.Label label11;

		private global::Gtk.HSeparator hseparator5;

		private global::Gtk.HBox hbox5;

		private global::Gtk.Label label5;

		private global::Gtk.Entry username;

		private global::Gtk.HBox hbox7;

		private global::Gtk.Label label7;

		private global::Gtk.Entry name;

		private global::Gtk.HBox hbox9;

		private global::Gtk.Label label9;

		private global::Gtk.Entry password;

		private global::Gtk.Label status;

		private global::Gtk.HSeparator hseparator3;

		private global::Gtk.HBox hbox11;

		private global::Gtk.Button exit;

		private global::Gtk.Alignment alignment2;

		private global::Gtk.Alignment alignment3;

		private global::Gtk.Button register;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Client.Windows.Register
			this.Name = "Client.Windows.Register";
			this.Title = global::Mono.Unix.Catalog.GetString("TDIN");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Resizable = false;
			this.AllowGrow = false;
			// Container child Client.Windows.Register.Gtk.Container+ContainerChild
			this.alignment1 = new global::Gtk.Alignment(0.5F, 0.5F, 1F, 1F);
			this.alignment1.Name = "alignment1";
			this.alignment1.LeftPadding = ((uint)(1));
			this.alignment1.TopPadding = ((uint)(1));
			this.alignment1.RightPadding = ((uint)(1));
			this.alignment1.BottomPadding = ((uint)(1));
			this.alignment1.BorderWidth = ((uint)(1));
			// Container child alignment1.Gtk.Container+ContainerChild
			this.vbox15 = new global::Gtk.VBox();
			this.vbox15.Name = "vbox15";
			this.vbox15.Spacing = 6;
			this.vbox15.BorderWidth = ((uint)(9));
			// Container child vbox15.Gtk.Box+BoxChild
			this.label11 = new global::Gtk.Label();
			this.label11.Name = "label11";
			this.label11.LabelProp = global::Mono.Unix.Catalog.GetString("Register");
			this.vbox15.Add(this.label11);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox15[this.label11]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox15.Gtk.Box+BoxChild
			this.hseparator5 = new global::Gtk.HSeparator();
			this.hseparator5.Name = "hseparator5";
			this.vbox15.Add(this.hseparator5);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox15[this.hseparator5]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox15.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("username:");
			this.hbox5.Add(this.label5);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.label5]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.username = new global::Gtk.Entry();
			this.username.CanFocus = true;
			this.username.Name = "username";
			this.username.IsEditable = true;
			this.username.InvisibleChar = '●';
			this.hbox5.Add(this.username);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.username]));
			w4.Position = 1;
			this.vbox15.Add(this.hbox5);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox15[this.hbox5]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox15.Gtk.Box+BoxChild
			this.hbox7 = new global::Gtk.HBox();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.label7 = new global::Gtk.Label();
			this.label7.Name = "label7";
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString("real name:");
			this.hbox7.Add(this.label7);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.label7]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.name = new global::Gtk.Entry();
			this.name.CanFocus = true;
			this.name.Name = "name";
			this.name.IsEditable = true;
			this.name.InvisibleChar = '●';
			this.hbox7.Add(this.name);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.name]));
			w7.Position = 1;
			this.vbox15.Add(this.hbox7);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox15[this.hbox7]));
			w8.Position = 3;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox15.Gtk.Box+BoxChild
			this.hbox9 = new global::Gtk.HBox();
			this.hbox9.Name = "hbox9";
			this.hbox9.Spacing = 6;
			// Container child hbox9.Gtk.Box+BoxChild
			this.label9 = new global::Gtk.Label();
			this.label9.Name = "label9";
			this.label9.LabelProp = global::Mono.Unix.Catalog.GetString("password: ");
			this.hbox9.Add(this.label9);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox9[this.label9]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox9.Gtk.Box+BoxChild
			this.password = new global::Gtk.Entry();
			this.password.CanFocus = true;
			this.password.Name = "password";
			this.password.IsEditable = true;
			this.password.Visibility = false;
			this.password.InvisibleChar = '●';
			this.hbox9.Add(this.password);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox9[this.password]));
			w10.Position = 1;
			this.vbox15.Add(this.hbox9);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox15[this.hbox9]));
			w11.Position = 4;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox15.Gtk.Box+BoxChild
			this.status = new global::Gtk.Label();
			this.status.Name = "status";
			this.vbox15.Add(this.status);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox15[this.status]));
			w12.Position = 5;
			w12.Expand = false;
			w12.Fill = false;
			// Container child vbox15.Gtk.Box+BoxChild
			this.hseparator3 = new global::Gtk.HSeparator();
			this.hseparator3.Name = "hseparator3";
			this.vbox15.Add(this.hseparator3);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox15[this.hseparator3]));
			w13.Position = 6;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vbox15.Gtk.Box+BoxChild
			this.hbox11 = new global::Gtk.HBox();
			this.hbox11.Name = "hbox11";
			this.hbox11.Homogeneous = true;
			this.hbox11.Spacing = 6;
			// Container child hbox11.Gtk.Box+BoxChild
			this.exit = new global::Gtk.Button();
			this.exit.CanFocus = true;
			this.exit.Name = "exit";
			this.exit.UseUnderline = true;
			this.exit.Label = global::Mono.Unix.Catalog.GetString("Exit");
			this.hbox11.Add(this.exit);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.exit]));
			w14.Position = 0;
			// Container child hbox11.Gtk.Box+BoxChild
			this.alignment2 = new global::Gtk.Alignment(0.5F, 0.5F, 1F, 1F);
			this.alignment2.Name = "alignment2";
			this.hbox11.Add(this.alignment2);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.alignment2]));
			w15.Position = 1;
			// Container child hbox11.Gtk.Box+BoxChild
			this.alignment3 = new global::Gtk.Alignment(0.5F, 0.5F, 1F, 1F);
			this.alignment3.Name = "alignment3";
			this.hbox11.Add(this.alignment3);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.alignment3]));
			w16.Position = 2;
			// Container child hbox11.Gtk.Box+BoxChild
			this.register = new global::Gtk.Button();
			this.register.CanFocus = true;
			this.register.Name = "register";
			this.register.UseUnderline = true;
			this.register.Label = global::Mono.Unix.Catalog.GetString("Register");
			this.hbox11.Add(this.register);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.register]));
			w17.Position = 3;
			this.vbox15.Add(this.hbox11);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox15[this.hbox11]));
			w18.Position = 7;
			w18.Expand = false;
			w18.Fill = false;
			this.alignment1.Add(this.vbox15);
			this.Add(this.alignment1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 392;
			this.DefaultHeight = 208;
			this.Show();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
			this.exit.Clicked += new global::System.EventHandler(this.OnExitClicked);
			this.register.Clicked += new global::System.EventHandler(this.OnRegisterClicked);
		}
	}
}