namespace MiApi.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description {  get; set; }   
        public string image {  get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public string category {  get; set; } 
    }
}
