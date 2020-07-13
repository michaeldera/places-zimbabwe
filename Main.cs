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
        [FunctionName("suburbs")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("http trigger function processed a request.");


            var bulawayoSuburbs = new string[] { "Makokoba", "Mzilikazi", "Nguboyenja", "Barbourfields", "Thorngrove", "Mpopoma", "Pelandaba", "Mabutweni", "Matshobana", "Iminyela", "Entumbane", "Emakhandeni", "Njube", "Magwegwe", "Magwegwe Central", "Magwegwe North", "Magwegwe West", "Pumula East", "Pumula South", "Hyde Park Estate", "Robert Sinyoka Village", "Methodist Village", "Mazwi Village (St Peters)", "Tshabalala", "Tshabalala Extension", "Sizinda", "Nketa", "Emganwini", "Luveve Old", "Luveve New", "Luveve 5", "Luveve 4", "Luveve", "Luveve 3", "Luveve Enqameni", "Luveve Enqotsheni 5", "Gwabalanda", "Cowdray Park", "Umganin Rural", "Thorngrove", "Thorngrove West", "Emakhandeni", "Lobengula 1", "Lobengula 2", "Lobengula 3", "Lobengula 4", "Old Lobengula", "Nkulumane 1", "Nkulumane 2", "Nkulumane 3", "Nkulumane 4", "Nkulumane 5", "Nkulumane 10", "Nkulumane 11", "Nkulumane 12", "Upper Rangemore", "Nketa 6", "Nketa 7", "Nketa 8", "Nketa 9", "Emganwini  1", "Emganwini  2", "Harrisvale", "Jacaranda", "Kingsdale", "Lobenvale", "Newmansford", "Northgate", "Northlea", "Northlynne", "Orange Grove", "Queensdale", "Trenance", "Roslyn", "Waterlea", "Woodville", "Woodville Park", "Bellevue", "Eloana", "Hillside West", "Newton West", "West Somerton", "Burnside", "Fortunes Gate", "Four Winds", "Lakeside", "Lochview", "Matsheumhlophe", "Matsheumhlophe North", "Riverside", "Riverside South", "Southdale", "Waterford", "Manningdale", "Willsgrove Park", "Barham Green", "Bradfield", "Famona", "Greenhill", "Hillcrest", "Ilanda", "Malindela", "Montrose", "Southwold", "Glenville", "Norwood", "Richmond", "Dlamini Township", "Giffords Grant", "Highmount", "Kennilworth", "Northend", "Queens Park East", "Queens Park West", "Rowena", "Saursetown", "Saursetown West", "Parkview", "Clement", "Glencoe", "Glengarry", "Hume Park", "Killarney", "Kumalo", "Kumalo North", "Mahatshula", "Marlands", "Paddonhurst", "Parklands", "Riverside North", "Romney Park", "Selbourne Park", "Suburbs", "Sunninghill", "Sunnyside", "Tegela", "Woodlands", "Selbourne Brooke", "Whitest One", "Mqabuko Heights", "Helenvale", "Hillside South", "Munda", "Intini", "Ntabamoya", "Southdale", "Granite Park", "Fagadolas", "Northgate", "Kilalo", "North Trenance", "Upper Glenville", "Windor Park", "The Jungle", "Emhlangeni" };

            var harareSuburbs = new string[] { "Budiriro 1", "Budiriro 2", "Budiriro 3", "Budiriro 4", "Budiriro 5", "Cold Comfort", "Dzivaresekwa", "Gevstein Park", "Glaudina", "Kuwadzana Phase 3", "Tynwald South", "Warren Park D", "Warren Park North", "New Ardbennie", "Glen Norah", "Glen View", "Alexandra Park", "New Alexandra Park", "Avenues", "Avondale", "Avondale West", "Belgravia", "Belvedere North", "Belvedere South", "Civic Centre", "Coronation Park", "Eastlea North", "Eastlea South", "Gun Hill", "Kensington", "Lincoln Green", "Little Norfolk", "Milton Park", "Monovale", "Ridgeview", "Strathaven", "Workington", "New Ridgeview", "Donnybrook", "Amby", "Athlone", "Ballantyne Park", "Beverley", "Beverley West", "Borrowdale", "Borrowdale Brooke", "Brooke Ridge", "Carrick Creagh", "Chikurubi", "Chisipitie", "Colne Valley", "Colray", "Dawn Hill", "Glen Lorne", "Glenwood", "Greendale", "Green Grove", "Greystone Park", "Helensvale", "Highlands", "Hogerty Hill", "Kambanje", "Letombo Park", "Lewisam", "Lichendale", "Lorelei", "Luna", "Mandara", "Manresa", "Msasa", "Mukuvisi Park", "Newlands", "Philadelphia", "Quinnington", "Rhodesville", "Ringley", "Rietfontein", "Rolf Valley", "Runniville", "Ryelands", "The Grange", "Umwinsidale", "Shawasha Hills", "Adylinn", "Ashbrittle", "Ashdown Park", "Avonlea", "Bloomingdale", "Bluff Hill", "Bluf Hill (New)", "Bluff Hill Park", "Borrowdale West", "Cotswold Hills", "Emerald Hill", "Greencroft", "Groombridge", "Haig Park", "Hatcliffe", "Mabelreign", "Marlborough", "Marlborough (New)", "Manidoda Park", "Mayfield Park", "Meyrick Park", "Mount Hampden", "Mount Pleasant", "Nkwisi Park", "Northwood", "Pomona", "Sanganai Park", "Science Park", "Sentosa", "Sherwood Park", "St. Andrews Park", "Sunridge", "Sunrise", "Tynwald", "Tynwald North", "Vainona", "Valencedene", "Warren Park North", "Old Forest", "Arcadia", "Arlingon", "Braeside", "City Centre", "Civic Centre", "Cranbourne Park", "Graniteside", "Hillside", "Kopje", "Kutsaga", "Logan Park", "Prospect Park", "Queensdale", "St. Martins", "Sunningdale", "Chadcombe", "Chiremba Park", "Coronation Park", "Epworth", "Hatfield", "Msasa Park", "Mukuvisi", "Park Meadowlands", "Queensdale", "Wilmington Park", "Highfield", "Willowvale", "Aspindale Park", "Kambuzuma", "Lochinvar", "Rugare", "Southerton", "Warren Park", "Westwood", "Crowborough North", "Kuwadzana", "Chizhanje", "Mabvuku", "Tafara", "Ventersburg", "Hopley", "Mainway Meadows", "Mbare", "Prospect", "Shortson", "Ardbennie", "Grobbie Park", "Houghton Park", "Induna", "Malvern", "Mbare", "Midlands", "Parktown", "Uplands", "Waterfalls", "Crowborough", "Marimba Park", "Mufakose"
            };


            string city = req.Query["city"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            city = city ?? data?.city;

             var harare = new List<string>(harareSuburbs);
             var bulawayo = new List<string>(bulawayoSuburbs);

            harare.Sort();
            bulawayo.Sort();

            //handle no-city-provided
            if (string.IsNullOrEmpty(city))
            {
                return new ObjectResult(new { bulawayo, harare});
            }

            if ("Bulawayo".Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                object payload = new
                {
                    surburbs = bulawayoSuburbs
                };

                return new OkObjectResult(payload);
            } 
            else if ("Harare".Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                object payload = new
                {
                    surburbs = harareSuburbs
                };

                return new OkObjectResult(payload);
            }
            else
            {
                //handle a city not listed requested
                log.LogInformation(string.Format("{0} - city requested from the API", city));
                return new ObjectResult(new { message = string.Format("{0} is not yet listed as a city here", city) });
            }
        }
    }
}
