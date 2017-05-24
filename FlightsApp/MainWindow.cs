using System;
using System.Linq;
using Gtk;
using FlightsApp;
using System.Collections.Generic;
using FlightsApp.Lib.Utils;
using FlightsApp.Lib.Services;
using FlightsApp.Lib.Models;

public partial class MainWindow : Gtk.Window
{
    private const int DEFAULT_DATE_DIFF = 7;

    private readonly ILogger logger;
    private readonly FlightsService flightsService;
    private readonly TripService tripService;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        calendarStartDate.Date = DateTime.Now.AddDays(1);
        SetDefaultEndDate();

        logger = new UILogger(txtInfo);
		flightsService = new FlightsService(logger);
		tripService = new TripService(logger);

        LoadAirports();
    }

    private void LoadAirports()
    {
        var airports = Airport.All.OrderBy(a => a.Name).ToList();
        airports.ForEach(a =>{
            cbFrom.AppendText($"{a.Name} - {a.Code}"); 
            cbTo.AppendText($"{a.Name} - {a.Code}");
        });

        cbFrom.Active = airports.IndexOf(Airport.Dublin);
        cbTo.Active = airports.IndexOf(Airport.Suceava);
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnButton1Clicked(object sender, EventArgs e)
    {
        try
        {
            var airlines = new List<Airline>();
            // todo: this class should have a list of airlines, dinamically add the checkboxes and get the active list
            if (chkWizz.Active)
            {
                airlines.Add(Airline.Wizzair);
            }
            if (chkRyanair.Active)
            {
                airlines.Add(Airline.Ryanair);
            }
            if (chkBlueair.Active)
            {
                airlines.Add(Airline.Blueair);
            }
            if (chkTarom.Active)
            {
                airlines.Add(Airline.Tarom);
            }

            var from = Airport.All.First(a => cbFrom.ActiveText.IndexOf(a.Code) == (cbFrom.ActiveText.Length - 3));
            var to = Airport.All.First(a => cbTo.ActiveText.IndexOf(a.Code) == (cbTo.ActiveText.Length - 3));

            var flights = flightsService.Search(from, to, airlines, calendarStartDate.Date, calendarEndDate.Date);
            logger.Info("____________________________________");

            tripService.FindFightMatches(from, to, flights);
            logger.Info("____________________________________");

            tripService.FindFightMatches(to, from, flights);
            logger.Info("____________________________________");
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    protected void OnCalendar2PrevMonth(object sender, EventArgs e)
    {
        SetDefaultEndDate();
    }

    protected void OnCalendar2DaySelected(object sender, EventArgs e)
    {
        SetDefaultEndDate();
    }

    protected void OnCalendar2NextMonth(object sender, EventArgs e)
    {
        SetDefaultEndDate();
    }

    protected void OnCalendar2MonthChanged(object sender, EventArgs e)
    {
        SetDefaultEndDate();
    }

    protected void OnCalendar2PrevYear(object sender, EventArgs e)
    {
        SetDefaultEndDate();
    }

    protected void OnCalendar2NextYear(object sender, EventArgs e)
    {
        SetDefaultEndDate();
    }

    private void SetDefaultEndDate()
    {
        calendarEndDate.Date = calendarStartDate.Date.AddDays(DEFAULT_DATE_DIFF);
    }
}
