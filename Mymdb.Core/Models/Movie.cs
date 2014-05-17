using System;
using Newtonsoft.Json;
using SQLite.Net.Attributes;

namespace Mymdb.Core.Models
{
    public class Movie : BusinessLayer.BusinessEntityBase
    {
        [JsonProperty(PropertyName = "imdb_id"), Column("ImdbId"), MaxLength(20)]
        public string ImdbId { get; set; }

        [JsonProperty(PropertyName = "title"), MaxLength(20)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "poster_path"), MaxLength(100)]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "runtime")]
        public int Runtime { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public DateTime ReleaseDate { get; set; }

        public bool IsFavorite { get; set; }

        public byte[] Image { get; set; }

        [MaxLength(500)]
        public string ImagePath { get; set; }
    }
}
