using MeTube.Models;
using System;

namespace MeTube.App.Models.ViewModels
{
    public class TubeIndexViewModel
    {
        public int Id { get; set; }

        public string YouTubeId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public static Func<Tube, TubeIndexViewModel> FromTube
            => tube => new TubeIndexViewModel()
            {
                Id = tube.Id,
                YouTubeId = tube.YoutubeId,
                Title = tube.Title,
                Author = tube.Author
            };
    }
}
