using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mymdb.Core.Models
{
    public class MovieList
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "results")]
        public IEnumerable<Movie> Movies { get; set; }
    }
}
