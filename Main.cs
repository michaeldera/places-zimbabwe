using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlacesZimbabwe
{
    public static class Main 
    {
        [FunctionName("surburbs")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("http trigger function processed a request.");


            var highDensitySurburbs = new string[] { "Makokoba", "Mzilikazi", "Nguboyenja", "Barbourfields", "Thorngrove", "Mpopoma", "Pelandaba", "Mabutweni", "Matshobana", "Iminyela", "Entumbane", "Emakhandeni", "Njube", "Magwegwe", "Magwegwe Central", "Magwegwe North", "Magwegwe West", "Pumula East", "Pumula South", "Hyde Park Estate", "Robert Sinyoka Village", "Methodist Village", "Mazwi Village (St Peters)", "Tshabalala", "Tshabalala Extension", "Sizinda", "Nketa", "Emganwini", "Luveve Old", "Luveve New", "Luveve 5", "Luveve 4", "Luveve", "Luveve 3", "Luveve Enqameni", "Luveve Enqotsheni 5", "Gwabalanda", "Cowdray Park", "Umganin Rural", "Thorngrove", "Thorngrove West", "Emakhandeni", "Lobengula 1", "Lobengula 2", "Lobengula 3", "Lobengula 4", "Old Lobengula", "Nkulumane 1", "Nkulumane 2", "Nkulumane 3", "Nkulumane 4", "Nkulumane 5", "Nkulumane 10", "Nkulumane 11", "Nkulumane 12", "Upper Rangemore", "Nketa 6", "Nketa 7", "Nketa 8", "Nketa 9", "Emganwini  1", "Emganwini  2" };

            var lowAndMediumDensitySurburbs = new string[] { "Harrisvale", "Jacaranda", "Kingsdale", "Lobenvale", "Newmansford", "Northgate", "Northlea", "Northlynne", "Orange Grove", "Queensdale", "Trenance", "Roslyn", "Waterlea", "Woodville", "Woodville Park", "Bellevue", "Eloana", "Hillside West", "Newton West", "West Somerton", "Burnside", "Fortunes Gate", "Four Winds", "Lakeside", "Lochview", "Matsheumhlophe", "Matsheumhlophe North", "Riverside", "Riverside South", "Southdale", "Waterford", "Manningdale", "Willsgrove Park", "Barham Green", "Bradfield", "Famona", "Greenhill", "Hillcrest", "Ilanda", "Malindela", "Montrose", "Southwold", "Glenville", "Norwood", "Richmond", "Dlamini Township", "Giffords Grant", "Highmount", "Kennilworth", "Northend", "Queens Park East", "Queens Park West", "Rowena", "Saursetown", "Saursetown West", "Parkview", "Clement", "Glencoe", "Glengarry", "Hume Park", "Killarney", "Kumalo", "KumaloNorth", "Mahatshula", "Marlands", "Paddonhurst", "Parklands", "Riverside North", "Romney Park", "Selbourne Park", "Suburbs", "Sunninghill", "Sunnyside", "Tegela", "Woodlands", "Selbourne Brooke", "Whitest One", "Mqabuko Heights", "Helenvale", "Hillside South", "Munda", "Intini", "Ntabamoya", "Southdale", "Granite Park", "Fagadolas", "Northgate", "Kilalo", "North Trenance", "Upper Glenville", "Windor Park", "The Jungle", "Emhlangeni" };


            string city = req.Query["city"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            city = city ?? data?.city;


            //handle no-city-provided
            if (string.IsNullOrEmpty(city))
            {
                return new ObjectResult(new { message = "Enter the city you want to get a list of surburbs" });
            }

            if ("Bulawayo".Equals(city, StringComparison.OrdinalIgnoreCase)){
                var highDensity = new List<string>(highDensitySurburbs);
                var lowDensity = new List<string>(lowAndMediumDensitySurburbs);

                // Just sort for better experience.
                highDensity.Sort();
                lowDensity.Sort();

                object payload = new
                {
                    highDensity = highDensity.ToArray(),
                    lowDensity = lowDensity.ToArray()
                };

                return new OkObjectResult(payload);
            } else 
            {
                //handle a city not listed requested
                log.LogInformation(string.Format("{0} - city requested from the API", city));
                return new ObjectResult(new { message = string.Format("{0} is not yet listed as a city here", city) });
            }
        }
    }
}
