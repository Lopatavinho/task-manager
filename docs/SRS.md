# Especificação de Requisitos de Software (SRS)

## 1. Introdução
Sistema para gerenciamento de tarefas com autenticação e CRUD completo.

## 2. Requisitos Funcionais
1. RF01 – O sistema deve permitir cadastro de usuário.
2. RF02 – O sistema deve permitir login seguro.
3. RF03 – O usuário deve poder criar uma nova tarefa.
4. RF04 – O usuário deve poder listar suas tarefas.
5. RF05 – O usuário deve poder editar uma tarefa.
6. RF06 – O usuário deve poder excluir uma tarefa.
7. RF07 – O sistema deve registrar data de criação e atualização.

## 3. Requisitos Não Funcionais
- RNF01 – Usabilidade simples e intuitiva.
- RNF02 – Segurança das credenciais com hash de senha.
- RNF03 – Persistência em banco PostgreSQL (via Docker).
- RNF04 – Backend desenvolvido em C# com .NET 8.
- RNF05 – Testes automatizados com xUnit.
- RNF06 – API documentada com Swagger.
