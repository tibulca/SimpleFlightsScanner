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

    private readonly CurrencyConverter currencyConverter;
    private readonly ILogger logger;
    private readonly FlightsService flightsService;
    private readonly TripService tripService;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        calendarStartDate.Date = DateTime.Now.AddDays(1);
        SetDefaultEndDate();

        currencyConverter = new CurrencyConverter(new Dictionary<string, double>
        {
            { "RON", double.Parse(txtRon.Text) },
            { "GBP", double.Parse(txtGbp.Text) },
        });
        logger = new UILogger(txtInfo);
		flightsService = new FlightsService(currencyConverter, logger);
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

            var tripsFrom = tripService.FindFightMatches(from, to, flights);
            LogTrips(from, to, tripsFrom);

            var tripsBack = tripService.FindFightMatches(to, from, flights);
            LogTrips(to, from, tripsBack);
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    private void LogTrips(Airport from, Airport to, List<Trip> trips)
    {
        logger.Info($"TRIP from {from.Name} to {to.Name}");
        if (!trips.Any())
        {
            logger.Info($"\tNO RESULTS!");
            return;
        }

        var minPrice = trips.Select(t => t.TotalPrice)
                            .Min();
        logger.Info($"\tmin price: {(int)minPrice}");

        trips.GroupBy(t => t.DateFrom.Date)
             .OrderBy(g => g.Key)
             .ToList()
             .ForEach(g =>
             {
                 logger.Info(string.Empty);
                 logger.Info($"{g.Key.ToString("ddd dd/MMM/yyyy")}:");
                 g.OrderBy(f => f.DateFrom).ToList().ForEach(Log);
             });
        logger.Info("____________________________________");
    }

    private void Log(Trip trip)
    {
        var flights = string.Join(" --> ", trip.Flights.Select(f => $"{f.DateFrom.ToString("HH:mm")} {f.From.Name} - {f.To.Name} {f.DateTo.ToString("HH:mm")}"));
        var airlines = string.Join(", ", trip.Flights.Select(f => f.Airline.Name));
        var prices = string.Join(", ", trip.Flights.Select(f => $"{(int)f.Price} {f.CurrencyCode}"));

        logger.Info($"\t{flights}, ({airlines}),\t{(int)trip.TotalPrice} EUR\t({prices})");
    }

    protected void OnStartDateChanged(object sender, EventArgs e)
    {
        SetDefaultEndDate();
        UpdateSelectedDates();
    }

    protected void OnEndDayChanged(object sender, EventArgs e)
    {
        UpdateSelectedDates();
    }

    private void SetDefaultEndDate()
    {
        var endDate = calendarStartDate.Date.AddDays(DEFAULT_DATE_DIFF);
        if (calendarEndDate.Date == endDate)
        {
            return;
        }

        calendarEndDate.Date = endDate;
    }

    private void UpdateSelectedDates()
    {
        lblDates.Text = $"{calendarStartDate.Date:dd.MMM.yyyy ddd}\n{calendarEndDate.Date:dd.MMM.yyyy ddd}";
    }
}
