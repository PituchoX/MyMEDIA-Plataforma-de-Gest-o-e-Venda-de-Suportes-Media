using Microsoft.AspNetCore.Identity;
using GestaoLoja.Entities;

namespace GestaoLoja.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string NomeCompleto { get; set; } = string.Empty;


        public string Estado { get; set; } = "Ativo";

        
        public string Perfil { get; set; } = "Cliente";


        public Cliente? Cliente { get; set; }
        public Fornecedor? Fornecedor { get; set; }
    }
}
