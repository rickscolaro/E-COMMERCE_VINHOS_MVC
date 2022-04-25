

1 - Cria pasta Models e classe Produto e Categoria e suas propriedades

2 - Cria a Pasta Context e a classe AppDbContext criando suas propriedades e As DataAnnotations

3 - Registrar o serviço em Startup services.AddDbContext<AppDbContext>()

4 - Gerar a migrations

5 - Popular as Tabelas

6 - Criar os Controladores

7 - Criar a Pasta Services os arquivos Interfaces, Classes e registrar os serviços no Startup

8 - Implementar O Repositório

9 - implementar padrão UnitOfWork

10 - Implementar Padrão DTO (Padrão que mostra só informações necessárias aos clientes e escondem outras)
    dotnet add package AutoMapper 
    dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

-- Aplicar Segurança --

11 - Alterar o DbContext para IdentityDbContext e registrar no services

12 - Fazer uma noma migração depois que fez as alterações para poder gerar as novas tabelas de autenticação
    Roles Users ...

13 - Criar uma classe Usuário e seu controlador

14 - Criar os métodos de registro e login

15 - Criar o token de validação  sua classe e chave secreta

16 - registrar o serviço do Token




'2022-01-27 12:00:00.000'