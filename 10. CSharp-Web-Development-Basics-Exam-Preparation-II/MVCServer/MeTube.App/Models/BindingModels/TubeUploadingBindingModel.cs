using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MeTube.App.Models.BindingModels
{
    public class TubeUploadingBindingModel
    {
        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string YouTubeLink { get; set; }

        public string Description { get; set; }
    }
}
