using System;
using Newtonsoft.Json;

namespace Mymdb.Core.Models
{
    public class Movie : BusinessLayer.BusinessEntityBase
    {
        [JsonProperty(PropertyName = "imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "runtime")]
        public int Runtime { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public DateTime ReleaseDate { get; set; }

        public bool IsFavorite { get; set; }

        public byte[] Image { get; set; }

        public string ImagePath { get; set; }
    }
}
