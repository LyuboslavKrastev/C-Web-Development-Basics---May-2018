using System.Collections.Generic;

namespace KittenApp.Models
{
    public class Breed
    {
        public Breed()
        {
            this.Kittens = new List<Kitten>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Kitten> Kittens { get; set; }
    }
}
