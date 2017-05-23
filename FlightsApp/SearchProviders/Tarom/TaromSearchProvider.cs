using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace FlightsApp
{
	public class TaromSearchProvider : SearchProviderBase, ISearchProvider
	{
		public TaromSearchProvider(ILogger logger, IApiHttpClient apiHttpClient)
            : base(Airline.Tarom, logger, apiHttpClient)
        {
		}

        protected override async Task<List<Flight>> SearchFlights(SearchCriteria searchCriteria)
        {
            var csfr = await this.GetCsfr();

            var encryptedRequest = await this.GetEncryptedRequest(csfr, searchCriteria);

            var flights = await this.GetFlights(encryptedRequest);
            var parsedFlights = string.Concat(flights.matrix.SelectMany(f => f.Select(ff => $"{ff.outboundDate.ToShortDateString()}-{ff.outboundPrice.totalAmount}{ff.outboundPrice.currency.code}  ")));
            logger.Info($"Tarom flights: {parsedFlights}");

            logger.Error("NOT IMPLEMENTED!");
            return await Task.Run(() => new List<Flight>());
        }

        private async Task<string> GetCsfr()
        {
            var homePage = await apiHttpClient.GetAsync("http://www.tarom.ro/");

            return this.GetHtmlInputValue(homePage, "csfr_token");
        }

        private async Task<string> GetEncryptedRequest(string csfr, SearchCriteria searchCriteria)
        {
            var date1 = searchCriteria.FromDate.AddDays(3);

            var requestBody = $"csfr_token={csfr}&__CM__LANGUAGE=en&ARRANGE_BY=&DIRECT_NON_STOP=FALSE&CABIN=E&B_LOCATION_1={searchCriteria.Route.Airport1.Code}&E_LOCATION_1={searchCriteria.Route.Airport2.Code}&" +
                $"B_DATE_1c={date1.ToString("dd+MMMM+yyyy")}&B_TIME=0001&B_DAY={date1.Day}&B_MONTH={date1.ToString("yyyyMM")}&E_DATE={date1.ToString("dd+MMMM+yyyy")}&" +
                $"E_TIME=0001&E_DAY={DateTime.Now.Day}&E_MONTH={DateTime.Now.ToString("yyyyMM")}&" +
                "selector_nr0=1&selector_nr1=0&selector_nr2=0&selector_nr3=0&selector_nr4=0&selector_nr5=0&pSelector_nr0=1&PASSENGER_TYPE0=ADTPAX&pSelector_nr1=1&PASSENGER_TYPE1=InfantPAX&pSelector_nr2=1&" +
                "PASSENGER_TYPE2=CHDPAX&pSelector_nr3=1&PASSENGER_TYPE3=TMP_ADTPAX&flex_pricer_option=TRUE&payment=card&TRIP_TYPE=O&reservations-flight=1&SO_SITE_MOD_E_TICKET=TRUE&BROWSER=&SERVER=&SERVLET_NAME_KEY=&" +
                "EMBEDDED_TRANSACTION=FlexPricerAvailability&SITE=BAXVBNEW&TRIPFLOW=YES&SEVEN_DAY_SEARCH=TRUE&LANGUAGE=GB&C_LANG=en&SEARCH_BY=1&AIRLINE_1_1=&AIRLINE_1_2=&AIRLINE_1_3=&AIRLINE_2_1=&AIRLINE_2_2=&AIRLINE_2_3=&" +
                $"B_DATE_1={date1.ToString("yyyyMMdd")}0001&B_DATE_2={date1.ToString("yyyyMMdd")}0001&" +
                "B_DATE_3=&B_DATE_4=&B_ANY_TIME_1=&B_ANY_TIME_2=&RBD_1_1=&RBD_1_2=&RBD_1_3=&FLIGHT_NUMBER_1_1=&FLIGHT_NUMBER_1_2=&FLIGHT_NUMBER_1_3=&FLIGHT_NUMBER_2_1=&FLIGHT_NUMBER_2_2=&FLIGHT_NUMBER_2_3=&RBD_2_1=&RBD_2_2=&" +
                "RBD_2_3=&TRAVELLER_TYPE_1=ADT&TRAVELLER_TYPE_2=&TRAVELLER_TYPE_3=&TRAVELLER_TYPE_4=&TRAVELLER_TYPE_5=&TRAVELLER_TYPE_6=&TRAVELLER_TYPE_7=&TRAVELLER_TYPE_8=&TRAVELLER_TYPE_9=&HAS_INFANT_1=FALSE&HAS_INFANT_2=FALSE&" +
                "HAS_INFANT_3=FALSE&HAS_INFANT_4=FALSE&HAS_INFANT_5=FALSE&HAS_INFANT_6=FALSE&HAS_INFANT_7=FALSE&HAS_INFANT_8=FALSE&HAS_INFANT_9=FALSE&SO_SITE_MOP_CALL_ME=FALSE&SO_SITE_MOP_EXT=TRUE&TRIP_FLOW=YES&" +
                "SO_GL=%3C%3Fxml+version%3D%271.0%27+encoding%3D%27iso-8859-1%27%3F%3E%3CSO_GL%3E%3CGLOBAL_LIST+mode%3D%27partial%27%3E%3CNAME%3ESITE_SITE_FARE_COMMANDS_AND_OPTIONS%3C%2FNAME%3E%3CLIST_ELEMENT%3E%3CCODE%3E103%3C%2" +
                "FCODE%3E%3CLIST_VALUE%3E0%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E2%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E4%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E0%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2" +
                "FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3EEUR%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2" +
                "FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2FLIST_VALUE%3E%3CLIST_VALUE%3E%3C%2FLIST_VALUE%3E%3C%2FLIST_ELEMENT%3E%3C%2FGLOBAL_LIST%3E%3C" +
                "GLOBAL_LIST+mode%3D%27complete%27%3E%3CNAME%3ESO_SINGLE_MULTIPLE_COMMAND_BUILDER%3C%2FNAME%3E%3CLIST_ELEMENT%3E%3CCODE%3E1%3C%2FCODE%3E%3CLIST_VALUE%3E%3C%21%5BCDATA%5BAPE-TECHNICAL.ASSISTANCE%40TAROM.RO%5D%5D%3E%3C%2" +
                "FLIST_VALUE%3E%3CLIST_VALUE%3ES%3C%2FLIST_VALUE%3E%3C%2FLIST_ELEMENT%3E%3C%2FGLOBAL_LIST%3E%3C%2FSO_GL%3E&SO_SITE_TK_ARRANGEMENT=XL&SO_SITE_TK_TIME_PERIOD=H48&SO_SITE_TK_OFFICE_ID=BUHRO08R0&SO_SITE_MINIMAL_TIME=H2&" +
                "SO_SITE_MAXIMAL_TIME=M12&SO_SITE_MIN_AVAIL_DATE_SPAN=H2&SO_SITE_MAX_AVAIL_DATE_SPAN=M12&SO_SITE_MIN_AIR_DATE_SPAN=H12&SO_SITE_MAX_AIR_DATE_SPAN=M12&SO_SITE_DISPL_ETKT_NUMBERS=FALSE&SO_SITE_NEW_UI_ENABLED=TRUE&" +
                "SO_SITE_NEW_UI_LIST_TPLS_ON=ALL&SO_SITE_RUI_ENABLE_IMPL_DATA=TRUE&SO_SITE_RUI_TIMEOUT_ENABLED=TRUE&SO_SITE_RUI_TIMEOUT_WARNING=180&SO_SITE_RUI_HIDE_MDF_SRC=TRUE&SO_SITE_RUI_MDF_SRCH_OW_SEL=SO_SITE_RUI_MDF_SRCH_OW_SEL&" +
                "SO_SITE_RUI_MDF_SRCH_OW_SEL=TRUE&SO_SITE_SPECIAL_REQ_NUMBER=3&SO_SITE_FP_ELEM_LEV_FF_DISP=TRUE&SO_SITE_FP_SEG_FLIGHT_TIME=TRUE&SO_SITE_PNR_SHARING=TRUE&COMMERCIAL_FARE_FAMILY_1=EUROPE&COMMERCIAL_FARE_FAMILY_TYPE_1=C&" +
                "PRICING_TYPE=O&DATE_RANGE_VALUE_1=3&DATE_RANGE_VALUE_2=3&DATE_RANGE_QUALIFIER_1=C&DATE_RANGE_QUALIFIER_2=C&DISPLAY_TYPE=2&MATRIX_CALENDAR=FALSE&REFRESH=0&SO_SITE_FP_PRICING_TYPE=O&SO_SITE_IS_INSURANCE_ENABLED=TRUE&" +
                "SO_SITE_ETKT_VIEW_ENABLED=FALSE&SO_SITE_EXT_PSPURL=https%3A%2F%2Fconverter-tarom.ro";

            var headers = new Dictionary<string, string>
            {
                {"Cache-Control", "max-age=0"},
                {"Upgrade-Insecure-Requests", "1"},
				{"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"},
				{"Accept-Language", "en-US,en;q=0.8,ro;q=0.6"},
            };

            var response = await apiHttpClient.SendAsync("http://www.tarom.ro/crypt.php?form=reservation", HttpMethod.Post, requestBody, "application/x-www-form-urlencoded", headers);

            return this.GetHtmlInputValue(response, "ENC");
        }

        private async Task<TaromFlights> GetFlights(string encodedSearch)
        {
            var requestBody = $"SITE=BAXVBNEW&LANGUAGE=RO&EMBEDDED_TRANSACTION=FlexPricerAvailability&ENC={encodedSearch}&ENCT=1";
            var headers = new Dictionary<string, string>
            {
                {"Cache-Control", "max-age=0"},
                {"Upgrade-Insecure-Requests", "1"},
                {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"},
                {"Accept-Language", "en-US,en;q=0.8,ro;q=0.6"},
            };

            var response = await apiHttpClient.SendAsync("https://book.tarom.ro/plnext/taromDX/Override.action", HttpMethod.Post, requestBody, "application/x-www-form-urlencoded", headers);

            var flightPrices = this.GetJsonSection(response, "owdCalendar", false);
            var flights = Deserialize<TaromFlights>(flightPrices);

            var flightsSchedule = GetFlightsSchedule();
            // todo: merge schedule into flights data

            return flights;
        }

        private List<string> GetFlightsSchedule()
        {
            // todo: call https://api.skyteam.com/v3/schedules?origin=SCV&origintype=APT&destination=OTP&destinationtype=APT&departuredate=2017-05-31&returndate=2017-06-09
            //       with header api_key:yfunvneadwz62wywjvtgqc8a
            // todo: parse response and return the flights schedule

            return null;
        }

        private string GetHtmlInputValue(string html, string inputName)
        {
            var delimiter = "(\"|')";
            var regex = new Regex($"name={delimiter}{inputName}{delimiter}(\\s+)value={delimiter}[0-9a-z]+{delimiter}", RegexOptions.IgnoreCase);
            var input = regex.Match(html).Value.Replace("'", "\"");
            // todo: can I use groups to identify the value?
            var start = input.IndexOf("\"", input.IndexOf("value")) + 1;
            var end = input.IndexOf("\"", start);

            return input.Substring(start, end - start);
        }

        private string GetJsonSection(string text, string sectionIdentifier, bool isArray)
        {
            var startParenthesis = isArray ? '[' : '{';
            var endParenthesis = isArray ? ']' : '}';

            var startIndex = text.IndexOf(sectionIdentifier);
            startIndex = text.IndexOf(startParenthesis, startIndex);

            var index = startIndex + 1;

            var openParentheses = 1;

            while (index < text.Length && openParentheses > 0)
            {
                var currentChar = text[index++];

                if (currentChar == startParenthesis)
                {
                    openParentheses++;
                }
                else if (currentChar == endParenthesis)
                {
                    openParentheses--;
                }
            }

            var json = text.Substring(startIndex, index - startIndex);
            return json;
        }
    }
}
