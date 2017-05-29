
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Fixed fixed1;

	private global::Gtk.Calendar calendarStartDate;

	private global::Gtk.Calendar calendarEndDate;

	private global::Gtk.Button btnSearch;

	private global::Gtk.Alignment alignment1;

	private global::Gtk.ScrolledWindow GtkScrolledWindow;

	private global::Gtk.TextView txtInfo;

	private global::Gtk.CheckButton chkWizz;

	private global::Gtk.CheckButton chkRyanair;

	private global::Gtk.CheckButton chkBlueair;

	private global::Gtk.CheckButton chkTarom;

	private global::Gtk.ComboBox cbFrom;

	private global::Gtk.ComboBox cbTo;

	private global::Gtk.Label lblDates;

	private global::Gtk.Label label1;

	private global::Gtk.Label label2;

	private global::Gtk.Entry txtRon;

	private global::Gtk.Entry txtGbp;

	private global::Gtk.Label label3;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("Flights");
		this.Icon = global::Stetic.IconLoader.LoadIcon(this, "gtk-find", global::Gtk.IconSize.Menu);
		this.WindowPosition = ((global::Gtk.WindowPosition)(3));
		this.DefaultWidth = 1200;
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.fixed1 = new global::Gtk.Fixed();
		this.fixed1.Name = "fixed1";
		this.fixed1.HasWindow = false;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.calendarStartDate = new global::Gtk.Calendar();
		this.calendarStartDate.CanFocus = true;
		this.calendarStartDate.Name = "calendarStartDate";
		this.calendarStartDate.DisplayOptions = ((global::Gtk.CalendarDisplayOptions)(35));
		this.fixed1.Add(this.calendarStartDate);
		global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.calendarStartDate]));
		w1.X = 15;
		w1.Y = 10;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.calendarEndDate = new global::Gtk.Calendar();
		this.calendarEndDate.CanFocus = true;
		this.calendarEndDate.Name = "calendarEndDate";
		this.calendarEndDate.DisplayOptions = ((global::Gtk.CalendarDisplayOptions)(35));
		this.fixed1.Add(this.calendarEndDate);
		global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.calendarEndDate]));
		w2.X = 250;
		w2.Y = 10;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.btnSearch = new global::Gtk.Button();
		this.btnSearch.WidthRequest = 120;
		this.btnSearch.HeightRequest = 50;
		this.btnSearch.CanFocus = true;
		this.btnSearch.Name = "btnSearch";
		this.btnSearch.UseUnderline = true;
		this.btnSearch.Label = global::Mono.Unix.Catalog.GetString("Search");
		this.fixed1.Add(this.btnSearch);
		global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.btnSearch]));
		w3.X = 1090;
		w3.Y = 10;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.alignment1 = new global::Gtk.Alignment(0.5F, 0.5F, 1F, 1F);
		this.alignment1.Name = "alignment1";
		this.fixed1.Add(this.alignment1);
		global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.alignment1]));
		w4.X = 370;
		w4.Y = 268;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow.WidthRequest = 1200;
		this.GtkScrolledWindow.HeightRequest = 500;
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.txtInfo = new global::Gtk.TextView();
		this.txtInfo.CanFocus = true;
		this.txtInfo.Name = "txtInfo";
		this.GtkScrolledWindow.Add(this.txtInfo);
		this.fixed1.Add(this.GtkScrolledWindow);
		global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.GtkScrolledWindow]));
		w6.X = 15;
		w6.Y = 200;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.chkWizz = new global::Gtk.CheckButton();
		this.chkWizz.CanFocus = true;
		this.chkWizz.Name = "chkWizz";
		this.chkWizz.Label = global::Mono.Unix.Catalog.GetString("Wizzair");
		this.chkWizz.Active = true;
		this.chkWizz.DrawIndicator = true;
		this.chkWizz.UseUnderline = true;
		this.fixed1.Add(this.chkWizz);
		global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.chkWizz]));
		w7.X = 670;
		w7.Y = 10;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.chkRyanair = new global::Gtk.CheckButton();
		this.chkRyanair.CanFocus = true;
		this.chkRyanair.Name = "chkRyanair";
		this.chkRyanair.Label = global::Mono.Unix.Catalog.GetString("Ryanair");
		this.chkRyanair.Active = true;
		this.chkRyanair.DrawIndicator = true;
		this.chkRyanair.UseUnderline = true;
		this.fixed1.Add(this.chkRyanair);
		global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.chkRyanair]));
		w8.X = 670;
		w8.Y = 40;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.chkBlueair = new global::Gtk.CheckButton();
		this.chkBlueair.CanFocus = true;
		this.chkBlueair.Name = "chkBlueair";
		this.chkBlueair.Label = global::Mono.Unix.Catalog.GetString("Blueair");
		this.chkBlueair.DrawIndicator = true;
		this.chkBlueair.UseUnderline = true;
		this.fixed1.Add(this.chkBlueair);
		global::Gtk.Fixed.FixedChild w9 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.chkBlueair]));
		w9.X = 670;
		w9.Y = 70;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.chkTarom = new global::Gtk.CheckButton();
		this.chkTarom.CanFocus = true;
		this.chkTarom.Name = "chkTarom";
		this.chkTarom.Label = global::Mono.Unix.Catalog.GetString("Tarom");
		this.chkTarom.Active = true;
		this.chkTarom.DrawIndicator = true;
		this.chkTarom.UseUnderline = true;
		this.fixed1.Add(this.chkTarom);
		global::Gtk.Fixed.FixedChild w10 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.chkTarom]));
		w10.X = 670;
		w10.Y = 100;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.cbFrom = global::Gtk.ComboBox.NewText();
		this.cbFrom.WidthRequest = 150;
		this.cbFrom.Name = "cbFrom";
		this.fixed1.Add(this.cbFrom);
		global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.cbFrom]));
		w11.X = 500;
		w11.Y = 10;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.cbTo = global::Gtk.ComboBox.NewText();
		this.cbTo.WidthRequest = 150;
		this.cbTo.Name = "cbTo";
		this.fixed1.Add(this.cbTo);
		global::Gtk.Fixed.FixedChild w12 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.cbTo]));
		w12.X = 500;
		w12.Y = 40;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.lblDates = new global::Gtk.Label();
		this.lblDates.WidthRequest = 120;
		this.lblDates.Name = "lblDates";
		this.lblDates.LabelProp = global::Mono.Unix.Catalog.GetString("Selected dates");
		this.fixed1.Add(this.lblDates);
		global::Gtk.Fixed.FixedChild w13 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.lblDates]));
		w13.X = 500;
		w13.Y = 100;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.label1 = new global::Gtk.Label();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("1 EUR = ");
		this.fixed1.Add(this.label1);
		global::Gtk.Fixed.FixedChild w14 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.label1]));
		w14.X = 780;
		w14.Y = 17;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.label2 = new global::Gtk.Label();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("GBP");
		this.fixed1.Add(this.label2);
		global::Gtk.Fixed.FixedChild w15 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.label2]));
		w15.X = 885;
		w15.Y = 47;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.txtRon = new global::Gtk.Entry();
		this.txtRon.WidthRequest = 50;
		this.txtRon.CanFocus = true;
		this.txtRon.Name = "txtRon";
		this.txtRon.Text = global::Mono.Unix.Catalog.GetString("4.566");
		this.txtRon.IsEditable = true;
		this.txtRon.InvisibleChar = '●';
		this.fixed1.Add(this.txtRon);
		global::Gtk.Fixed.FixedChild w16 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.txtRon]));
		w16.X = 830;
		w16.Y = 12;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.txtGbp = new global::Gtk.Entry();
		this.txtGbp.WidthRequest = 50;
		this.txtGbp.CanFocus = true;
		this.txtGbp.Name = "txtGbp";
		this.txtGbp.Text = global::Mono.Unix.Catalog.GetString("0.869");
		this.txtGbp.IsEditable = true;
		this.txtGbp.InvisibleChar = '●';
		this.fixed1.Add(this.txtGbp);
		global::Gtk.Fixed.FixedChild w17 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.txtGbp]));
		w17.X = 830;
		w17.Y = 42;
		// Container child fixed1.Gtk.Fixed+FixedChild
		this.label3 = new global::Gtk.Label();
		this.label3.Name = "label3";
		this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("RON");
		this.fixed1.Add(this.label3);
		global::Gtk.Fixed.FixedChild w18 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.label3]));
		w18.X = 885;
		w18.Y = 17;
		this.Add(this.fixed1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultHeight = 733;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.calendarStartDate.PrevMonth += new global::System.EventHandler(this.OnStartDateChanged);
		this.calendarStartDate.DaySelected += new global::System.EventHandler(this.OnStartDateChanged);
		this.calendarStartDate.NextMonth += new global::System.EventHandler(this.OnStartDateChanged);
		this.calendarStartDate.MonthChanged += new global::System.EventHandler(this.OnStartDateChanged);
		this.calendarStartDate.PrevYear += new global::System.EventHandler(this.OnStartDateChanged);
		this.calendarStartDate.DaySelectedDoubleClick += new global::System.EventHandler(this.OnStartDateChanged);
		this.calendarStartDate.NextYear += new global::System.EventHandler(this.OnStartDateChanged);
		this.calendarEndDate.PrevMonth += new global::System.EventHandler(this.OnEndDayChanged);
		this.calendarEndDate.DaySelected += new global::System.EventHandler(this.OnEndDayChanged);
		this.calendarEndDate.NextMonth += new global::System.EventHandler(this.OnEndDayChanged);
		this.calendarEndDate.MonthChanged += new global::System.EventHandler(this.OnEndDayChanged);
		this.calendarEndDate.PrevYear += new global::System.EventHandler(this.OnEndDayChanged);
		this.calendarEndDate.DaySelectedDoubleClick += new global::System.EventHandler(this.OnEndDayChanged);
		this.calendarEndDate.NextYear += new global::System.EventHandler(this.OnEndDayChanged);
		this.btnSearch.Clicked += new global::System.EventHandler(this.OnButton1Clicked);
	}
}
