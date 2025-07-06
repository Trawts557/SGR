using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Users
{
    public record GetUserDTO
    {
        public int IdUser { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public GetUserDTO(int idUser, string name, string lastName, string email, DateTime createdAt)
        {
            IdUser = idUser;
            Name = name;
            LastName = lastName;
            Email = email;
            CreatedAt = createdAt;
        }
    }
}
