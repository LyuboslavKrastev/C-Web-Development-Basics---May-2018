using MeTube.Models;
using System;

namespace MeTube.App.Models.ViewModels
{
    public class TubeDetailsViewModel
    {
        public string Title { get; set; }

        public string YouTubeId { get; set; }

        public string Author { get; set; }

        public int Views { get; set; }

        public string Description { get; set; }

        public static Func<Tube, TubeDetailsViewModel> FromTube =>
            tube => new TubeDetailsViewModel()
            {
                Title = tube.Title,
                YouTubeId = tube.YoutubeId,
                Author = tube.Author,
                Description = tube.Description,
                Views = tube.Views

            };
    }
}
