# MyMEDIA 🎬
**Plataforma Omnichannel de E-Commerce e Gestão**

![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=c-sharp&logoColor=white)
![.NET 8](https://img.shields.io/badge/.NET_8-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-512BD4?style=flat-square&logo=blazor&logoColor=white)
![MAUI](https://img.shields.io/badge/MAUI-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoft-sql-server&logoColor=white)

> Uma solução *end-to-end* desenvolvida para a listagem, venda e aluguer de suportes media (filmes, discos, CD) e acessórios[cite: 8]. O projeto atua como um *marketplace* onde a plataforma serve de intermediária, processando uma margem sobre o preço base estipulado pelos fornecedores[cite: 8].

### 🛠️ Arquitetura do Sistema
O ecossistema do projeto está dividido em três grandes blocos tecnológicos[cite: 8]:

*   **Frontend Cross-Platform:** Construído com **Blazor Web** e **Blazor Hybrid** (.NET MAUI)[cite: 8].
*   **Backend & Segurança:** **API RESTful** documentada em Swagger, com autenticação garantida pelo **.NET Core Identity Framework** e tokens **JWT**[cite: 8].
*   **Dados:** Base de dados **SQL Server (LocalDB)** manipulada exclusivamente via **Entity Framework Core 8** e **LINQ**[cite: 8].

### 👥 Controlo de Acessos (RBAC)
A plataforma suporta múltiplos perfis com permissões altamente restritas[cite: 8]:

*   🛒 **Cliente:** Consulta o catálogo, gere o carrinho e efetiva encomendas com simulação de pagamento[cite: 8].
*   📦 **Fornecedor:** Submete e edita os seus próprios produtos (que ficam no estado "Pendente" até aprovação)[cite: 8].
*   ⚙️ **Administrador / Funcionário:** Acedem a um portal administrativo fechado para aprovar produtos, gerir *stocks*, expedir encomendas e validar novos utilizadores[cite: 8].

### 📂 Estrutura do Repositório

A arquitetura reflete uma separação rigorosa de responsabilidades, utilizando **Razor Class Libraries (RCL)** para partilhar componentes e lógica entre a Web e o Mobile[cite: 7, 8].

```text
📦 MyMEDIA
 ┣ 📂 RESTfulAPIPWeb/   # Backend: API RESTful e acessos à Base de Dados
 ┣ 📂 GestaoLoja/       # Frontend: Aplicação Web Administrativa (Interna)
 ┣ 📂 MyMediaWeb/       # Frontend: Aplicação Pública (Browser)
 ┣ 📂 MyMediaMAUI/      # Frontend: Aplicação Pública (Mobile/Desktop)
 ┗ 📂 RCL*/             # Bibliotecas Partilhadas (Componentes, HTTP, Carrinho)
