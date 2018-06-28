using KittenApp.Models;
using System;
using System.Linq.Expressions;

namespace KittenApp.App.Models.ViewModels
{
    public class KittenViewModel
    {

        public string Name { get; set; }

        public int Age { get; set; }

        public string Breed { get; set; }

        public string Picture { get; set; }

        public static Expression<Func<Kitten, KittenViewModel>> FromKitten =>
         k => new KittenViewModel()
         {
             Name = k.Name,
             Age = k.Age,
             Breed = k.Breed.Name
         };
    }
}
