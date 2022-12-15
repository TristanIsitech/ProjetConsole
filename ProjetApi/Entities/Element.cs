using System.ComponentModel.DataAnnotations;

namespace ProjetApi.Entities
{
    public class Element
    {
        [Key]
        public string name { get; set; }
        public List<Pokemon>? pokemon { get; set; }
    }
}