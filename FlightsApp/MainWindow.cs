using System;
using Gtk;
using FlightsApp;
using System.Collections.Generic;

public partial class MainWindow : Gtk.Window
{
    private const int DEFAULT_DATE_DIFF = 7;
    private readonly FlightsService flightsService;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        calendarStartDate.Date = DateTime.Now.AddDays(1);
        SetDefaultEndDate();

        flightsService = new FlightsService(new UILogger(txtInfo));
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnButton1Clicked(object sender, EventArgs e)
    {
        var airlines = new List<string>();
        // todo: this class should have a list of airlines, dinamically add the checkboxes and get the active list
        if (chkWizz.Active)
        {
            airlines.Add(chkWizz.Label);
        }
        if (chkRyanair.Active)
        {
            airlines.Add(chkRyanair.Label);
        }
        if (chkBlueair.Active)
        {
            airlines.Add(chkBlueair.Label);
        }
        if (chkTarom.Active)
        {
            airlines.Add(chkTarom.Label);
        }

        flightsService.Search(airlines, calendarStartDate.Date, calendarEndDate.Date);
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
