using System;
using Gtk;
using FlightsApp;
using System.Collections.Generic;
using System.Linq;

public partial class MainWindow : Gtk.Window
{
	private readonly Wizzair wizzair;
	private readonly Logger logger;
	private readonly IApiHttpClient apiHttpClient;

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();

		calendar2.Date = DateTime.Now.AddDays(1);
		calendar3.Date = DateTime.Now.AddDays(7);

		this.logger = new Logger(this.textview1);
		this.apiHttpClient = new ApiHttpClient();//new ApiHttpClientMock();

		this.wizzair = new Wizzair(this.logger, this.apiHttpClient);
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void OnButton1Clicked(object sender, EventArgs e)
	{
		logger.Info(calendar2.Date.ToString() + " - " + calendar3.Date.ToString());

		var wizzDestinations = new List<Tuple<string, string>>
		{
			Tuple.Create("SCV", "LTN"),
			Tuple.Create("SCV", "CIA"),
			Tuple.Create("SCV", "BLQ"),
			Tuple.Create("SCV", "BGY"),
			Tuple.Create("SCV", "TSF")
		};

		wizzDestinations.ForEach(cities =>
		{
			var searchCriteria = new SearchCriteria
			{
				From = cities.Item1,
				To = cities.Item2,
				FromDate = calendar2.Date,
				ToDate = calendar3.Date
			};
			var task = wizzair.Search(searchCriteria);
			task.Wait();
			logger.Info("--------------------------");
		});
	}

	protected void OnCalendar2PrevMonth(object sender, EventArgs e)
	{
		calendar3.Date = calendar2.Date.AddDays(5);
	}

	protected void OnCalendar2DaySelected(object sender, EventArgs e)
	{
		calendar3.Date = calendar2.Date.AddDays(5);
	}

	protected void OnCalendar2NextMonth(object sender, EventArgs e)
	{
		calendar3.Date = calendar2.Date.AddDays(5);
	}

	protected void OnCalendar2MonthChanged(object sender, EventArgs e)
	{
		calendar3.Date = calendar2.Date.AddDays(5);
	}

	protected void OnCalendar2PrevYear(object sender, EventArgs e)
	{
		calendar3.Date = calendar2.Date.AddDays(5);
	}

	protected void OnCalendar2NextYear(object sender, EventArgs e)
	{
		calendar3.Date = calendar2.Date.AddDays(5);
	}
}
