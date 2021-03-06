﻿using MeTube.Models;
using System;

namespace MeTube.App.Models.ViewModels
{
    public class TubeProfileViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public static Func<Tube, TubeProfileViewModel> FromTube => tube => new TubeProfileViewModel()
        {
            Id = tube.Id,
            Author = tube.Author,
            Title = tube.Title        
        };
    }
}
