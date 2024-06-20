using System.ComponentModel.DataAnnotations;

namespace MiApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string username { get; set; }

        public string password { get; set; }

        public string fullName { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string address { get; set; }

        public string role { get; set; }

        public string isActive { get; set; }
    }
}
