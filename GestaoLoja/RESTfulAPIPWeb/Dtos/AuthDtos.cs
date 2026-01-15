using System.ComponentModel.DataAnnotations;

namespace RESTfulAPIPWeb.Dtos
{

    /// DTO para resposta de login com token JWT

    public class LoginResponseDto
    {
        public string Token { get; set; } = "";
        public DateTime Expiration { get; set; }
        public UserInfoDto User { get; set; } = new();
    }


    /// Informações básicas do utilizador

    public class UserInfoDto
    {
        public string Id { get; set; } = "";
        public string Email { get; set; } = "";
        public string Nome { get; set; } = "";
        public string Perfil { get; set; } = "";
        public IList<string> Roles { get; set; } = new List<string>();
    }

  
    /// DTO para login

    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password é obrigatória")]
        public string Password { get; set; } = "";
    }


    /// DTO para registo de Cliente

    public class RegisterClienteRequestDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = "";

        [StringLength(9, MinimumLength = 9, ErrorMessage = "NIF deve ter 9 dígitos")]
        public string ?NIF { get; set; } 

        [Required(ErrorMessage = "Password é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password deve ter pelo menos 6 caracteres")]
        public string Password { get; set; } = "";
    }


    /// DTO para registo de Fornecedor

    public class RegisterFornecedorRequestDto
    {
        [Required(ErrorMessage = "Nome do responsável é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Nome da empresa é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome da empresa deve ter entre 2 e 100 caracteres")]
        public string NomeEmpresa { get; set; } = "";

        [Required(ErrorMessage = "NIF é obrigatório")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "NIF deve ter 9 dígitos")]
        public string NIF { get; set; } = "";

        [Required(ErrorMessage = "Password é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password deve ter pelo menos 6 caracteres")]
        public string Password { get; set; } = "";
    }


    /// Resposta genérica da API

    public class ApiResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public object? Data { get; set; }
    }

 
    /// Resposta de erro da API

    public class ApiErrorDto
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public IEnumerable<string>? Errors { get; set; }
    }
}
