using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DentalScheduler.Web.RestService.Models
{
    public class ODataParametersInputModel
    {
        [FromQuery(Name = "$filter")]
        public string Filter { get; set; }

        [FromQuery(Name = "$select")]
        public string Select { get; set; }

        [FromQuery(Name = "$orderby")]
        public string OrderBy { get; set; }

        [FromQuery(Name = "$expand")]
        public string Expand { get; set; }

        [FromQuery(Name = "$apply")]
        public string Apply { get; set; }

        [FromQuery(Name = "$count")]
        public string Count { get; set; }
    }
}