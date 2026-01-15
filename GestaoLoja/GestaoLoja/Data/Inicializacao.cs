using GestaoLoja.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GestaoLoja.Data
{
    public static class Inicializacao
    {
        public static void SeedDatabase(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Aplica migrações pendentes
            context.Database.Migrate();

            // ========================================
            // CRIAR TODOS OS ROLES
            // ========================================
            string[] roles = { "Administrador", "Funcionário", "Cliente", "Fornecedor" };

            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    roleManager.CreateAsync(new IdentityRole(role)).Wait();
                }
            }

            // ========================================
            // CRIAR ADMINISTRADOR
            // ========================================
            const string adminEmail = "admin@gestao.pt";
            const string adminPass = "Admin123!";

            var admin = userManager.FindByEmailAsync(adminEmail).Result;
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    NomeCompleto = "Administrador do Sistema",
                    Estado = "Ativo",
                    Perfil = "Administrador",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(admin, adminPass).Result;

                if (!result.Succeeded)
                {
                    throw new Exception("Erro a criar admin: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                userManager.AddToRoleAsync(admin, "Administrador").Wait();
            }
            else
            {
                // Garantir que o admin tem a role
                var adminRoles = userManager.GetRolesAsync(admin).Result;
                if (!adminRoles.Contains("Administrador"))
                {
                    userManager.AddToRoleAsync(admin, "Administrador").Wait();
                }
            }

            // ========================================
            // CRIAR FUNCIONÁRIO 
            // ========================================
            const string funcEmail = "func@gestao.pt";
            const string funcPass = "Func123!";

            var funcionario = userManager.FindByEmailAsync(funcEmail).Result;
            if (funcionario == null)
            {
                funcionario = new ApplicationUser
                {
                    UserName = funcEmail,
                    Email = funcEmail,
                    NomeCompleto = "Funcionário Exemplo",
                    Estado = "Ativo",
                    Perfil = "Funcionário",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(funcionario, funcPass).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(funcionario, "Funcionário").Wait();
                }
            }
            else
            {
                // Restaurar o funcionário se foi removido/alterado
                bool alterado = false;
                
                if (funcionario.Estado != "Ativo")
                {
                    funcionario.Estado = "Ativo";
                    alterado = true;
                }
                
                if (funcionario.Perfil != "Funcionário")
                {
                    funcionario.Perfil = "Funcionário";
                    alterado = true;
                }
                
                if (alterado)
                {
                    userManager.UpdateAsync(funcionario).Wait();
                }

                // Garantir que tem a role
                var funcRoles = userManager.GetRolesAsync(funcionario).Result;
                if (!funcRoles.Contains("Funcionário"))
                {
                    userManager.AddToRoleAsync(funcionario, "Funcionário").Wait();
                }
            }

            // ========================================
            // CRIAR CLIENTE DE TESTE
            // ========================================
            const string clienteEmail = "cliente@teste.pt";
            const string clientePass = "Cliente123!";

            var clienteUser = userManager.FindByEmailAsync(clienteEmail).Result;
            if (clienteUser == null)
            {
                clienteUser = new ApplicationUser
                {
                    UserName = clienteEmail,
                    Email = clienteEmail,
                    NomeCompleto = "João Silva (Cliente Teste)",
                    Estado = "Ativo",
                    Perfil = "Cliente",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(clienteUser, clientePass).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(clienteUser, "Cliente").Wait();
                }
            }
            else
            {
               
                if (clienteUser.Estado != "Ativo")
                {
                    clienteUser.Estado = "Ativo";
                    userManager.UpdateAsync(clienteUser).Wait();
                }

                
                var clienteRoles = userManager.GetRolesAsync(clienteUser).Result;
                if (!clienteRoles.Contains("Cliente"))
                {
                    userManager.AddToRoleAsync(clienteUser, "Cliente").Wait();
                }
            }

            // Criar registo na tabela Clientes 
            var clienteTeste = context.Clientes.FirstOrDefault(c => c.ApplicationUserId == clienteUser.Id);
            if (clienteTeste == null)
            {
                clienteTeste = new Cliente
                {
                    ApplicationUserId = clienteUser.Id,
                    NIF = "123456789",
                    Estado = "Ativo"
                };
                context.Clientes.Add(clienteTeste);
                context.SaveChanges();
            }

            // ========================================
            // CRIAR FORNECEDOR DE TESTE
            // ========================================
            const string fornecedorEmail = "fornecedor@teste.pt";
            const string fornecedorPass = "Forn123!";

            var fornecedorUser = userManager.FindByEmailAsync(fornecedorEmail).Result;
            if (fornecedorUser == null)
            {
                fornecedorUser = new ApplicationUser
                {
                    UserName = fornecedorEmail,
                    Email = fornecedorEmail,
                    NomeCompleto = "Maria Santos (Fornecedor Teste)",
                    Estado = "Ativo",
                    Perfil = "Fornecedor",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(fornecedorUser, fornecedorPass).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(fornecedorUser, "Fornecedor").Wait();
                }
            }
            else
            {
               
                if (fornecedorUser.Estado != "Ativo")
                {
                    fornecedorUser.Estado = "Ativo";
                    userManager.UpdateAsync(fornecedorUser).Wait();
                }

               
                var fornRoles = userManager.GetRolesAsync(fornecedorUser).Result;
                if (!fornRoles.Contains("Fornecedor"))
                {
                    userManager.AddToRoleAsync(fornecedorUser, "Fornecedor").Wait();
                }
            }

            // Criar registo na tabela Fornecedores 
            var fornecedorTeste = context.Fornecedores.FirstOrDefault(f => f.ApplicationUserId == fornecedorUser.Id);
            if (fornecedorTeste == null)
            {
                fornecedorTeste = new Fornecedor
                {
                    ApplicationUserId = fornecedorUser.Id,
                    NomeEmpresa = "MediaTeste Lda.",
                    Estado = "Aprovado"
                };
                context.Fornecedores.Add(fornecedorTeste);
                context.SaveChanges();
            }
        }
    }
}
