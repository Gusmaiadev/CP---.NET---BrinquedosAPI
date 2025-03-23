Que ótimo que tudo funcionou! Vou criar um **README completo** para o seu projeto, explicando a estrutura, os endpoints disponíveis, como executar o projeto e exemplos de uso. Aqui está o texto do README:

---

# BrinquedosAPI

A **BrinquedosAPI** é uma aplicação RESTful desenvolvida em .NET para gerenciar brinquedos, categorias e estoques. Ela permite realizar operações básicas de CRUD (Create, Read, Update, Delete) para cada uma dessas entidades, utilizando um banco de dados Oracle.

## 🚀 Tecnologias Utilizadas

- **.NET 6**: Framework para desenvolvimento da API.
- **Entity Framework Core**: ORM para mapeamento e manipulação do banco de dados.
- **Oracle Database**: Banco de dados relacional utilizado para armazenar os dados.
- **Swagger/OpenAPI**: Documentação interativa da API.
- **DTOs (Data Transfer Objects)**: Padrão para transferência de dados entre a API e o cliente.

---

## 📋 Estrutura do Projeto

O projeto está organizado da seguinte forma:

```
BrinquedosAPI/
├── Controllers/
│   ├── BrinquedosController.cs
│   ├── CategoriasController.cs
│   └── EstoquesController.cs
├── Data/
│   ├── AppDbContext.cs
│   ├── Brinquedo.cs
│   ├── Categoria.cs
│   └── Estoque.cs
├── DTOs/
│   ├── BrinquedoDTO.cs
│   ├── CategoriaDTO.cs
│   ├── EstoqueDTO.cs
│   ├── BrinquedoResponseDTO.cs
│   ├── CategoriaResponseDTO.cs
│   └── EstoqueResponseDTO.cs
├── Migrations/
│   └── (Arquivos de migração do Entity Framework)
├── appsettings.json
└── Program.cs
```

### Principais Classes

- **Controllers**: Contêm os endpoints da API.
- **Data**: Contém as classes de modelo e o contexto do banco de dados (`AppDbContext`).
- **DTOs**: Contém os objetos de transferência de dados (DTOs) para entrada e saída da API.
- **Migrations**: Contém as migrações do Entity Framework para criar e atualizar o banco de dados.

---

## 🛠️ Como Executar o Projeto

### Pré-requisitos

1. **.NET 6 SDK**: Instale o .NET 6 SDK a partir do [site oficial](https://dotnet.microsoft.com/download/dotnet/6.0).
2. **Oracle Database**: Certifique-se de ter um banco de dados Oracle configurado e acessível.
3. **Visual Studio ou Visual Studio Code**: Para editar e executar o projeto.

### Passos para Execução

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/seu-usuario/BrinquedosAPI.git
   cd BrinquedosAPI
   ```

2. **Configure a conexão com o banco de dados**:
   - No arquivo `appsettings.json`, atualize a string de conexão com as credenciais do seu banco de dados Oracle:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "User Id=seu_usuario;Password=sua_senha;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=seu_host)(PORT=1521))(CONNECT_DATA=(SID=seu_sid)))"
     }
     ```

3. **Execute as migrações**:
   - No terminal, execute o seguinte comando para aplicar as migrações e criar as tabelas no banco de dados:
     ```bash
     dotnet ef database update
     ```

4. **Execute o projeto**:
   - No terminal, execute:
     ```bash
     dotnet run
     ```
   - A API estará disponível em `https://localhost:5001` (ou outra porta, dependendo da configuração).

5. **Acesse o Swagger**:
   - Abra o navegador e acesse `https://localhost:5001/swagger` para visualizar e testar os endpoints da API.

---

## 📚 Endpoints da API

A API possui três recursos principais: **Brinquedos**, **Categorias** e **Estoques**. Abaixo estão os endpoints disponíveis para cada recurso.

### 1. Brinquedos

#### GET /api/brinquedos
- **Descrição**: Retorna uma lista de todos os brinquedos.
- **Exemplo de Resposta**:
  ```json
  [
    {
      "id_brinquedo": 1,
      "nome_brinquedo": "Barbie",
      "tipo_brinquedo": "Boneca",
      "classificacao": "4+",
      "tamanho": "12cm",
      "preco": 50.00,
      "id_categoria": 1,
      "id_estoque": 1
    }
  ]
  ```

#### GET /api/brinquedos/{id}
- **Descrição**: Retorna um brinquedo específico pelo ID.
- **Exemplo de Resposta**:
  ```json
  {
    "id_brinquedo": 1,
    "nome_brinquedo": "Barbie",
    "tipo_brinquedo": "Boneca",
    "classificacao": "4+",
    "tamanho": "12cm",
    "preco": 50.00,
    "id_categoria": 1,
    "id_estoque": 1
  }
  ```

#### POST /api/brinquedos
- **Descrição**: Cria um novo brinquedo.
- **Exemplo de Requisição**:
  ```json
  {
    "nome_brinquedo": "Carrinho",
    "tipo_brinquedo": "Brinquedo de montar",
    "classificacao": "Livre",
    "tamanho": "Médio",
    "preco": 30.00,
    "id_categoria": 2,
    "id_estoque": 2
  }
  ```

#### PUT /api/brinquedos/{id}
- **Descrição**: Atualiza um brinquedo existente.
- **Exemplo de Requisição**:
  ```json
  {
    "nome_brinquedo": "Carrinho Atualizado",
    "tipo_brinquedo": "Brinquedo de montar",
    "classificacao": "Livre",
    "tamanho": "Médio",
    "preco": 35.00,
    "id_categoria": 2,
    "id_estoque": 2
  }
  ```

#### DELETE /api/brinquedos/{id}
- **Descrição**: Remove um brinquedo pelo ID.
- **Resposta**: `204 No Content`.

---

### 2. Categorias

#### GET /api/categorias
- **Descrição**: Retorna uma lista de todas as categorias.
- **Exemplo de Resposta**:
  ```json
  [
    {
      "id_categoria": 1,
      "nome_categoria": "Bonecas"
    }
  ]
  ```

#### GET /api/categorias/{id}
- **Descrição**: Retorna uma categoria específica pelo ID.
- **Exemplo de Resposta**:
  ```json
  {
    "id_categoria": 1,
    "nome_categoria": "Bonecas"
  }
  ```

#### POST /api/categorias
- **Descrição**: Cria uma nova categoria.
- **Exemplo de Requisição**:
  ```json
  {
    "nome_categoria": "Carrinhos"
  }
  ```

#### PUT /api/categorias/{id}
- **Descrição**: Atualiza uma categoria existente.
- **Exemplo de Requisição**:
  ```json
  {
    "nome_categoria": "Carrinhos Atualizados"
  }
  ```

#### DELETE /api/categorias/{id}
- **Descrição**: Remove uma categoria pelo ID.
- **Resposta**: `204 No Content`.

---

### 3. Estoques

#### GET /api/estoques
- **Descrição**: Retorna uma lista de todos os estoques.
- **Exemplo de Resposta**:
  ```json
  [
    {
      "id_estoque": 1,
      "quantidade": 100,
      "faixa": "1 a 500"
    }
  ]
  ```

#### GET /api/estoques/{id}
- **Descrição**: Retorna um estoque específico pelo ID.
- **Exemplo de Resposta**:
  ```json
  {
    "id_estoque": 1,
    "quantidade": 100,
    "faixa": "1 a 500"
  }
  ```

#### POST /api/estoques
- **Descrição**: Cria um novo estoque.
- **Exemplo de Requisição**:
  ```json
  {
    "quantidade": 200,
    "faixa": "500 a 1000"
  }
  ```

#### PUT /api/estoques/{id}
- **Descrição**: Atualiza um estoque existente.
- **Exemplo de Requisição**:
  ```json
  {
    "quantidade": 250,
    "faixa": "500 a 1000"
  }
  ```

#### DELETE /api/estoques/{id}
- **Descrição**: Remove um estoque pelo ID.
- **Resposta**: `204 No Content`.

---

## 📝 Exemplos de Uso

### Criar um Brinquedo
1. **Requisição**:
   ```bash
   POST /api/brinquedos
   ```
   ```json
   {
     "nome_brinquedo": "Lego",
     "tipo_brinquedo": "Brinquedo de montar",
     "classificacao": "6+",
     "tamanho": "Grande",
     "preco": 100.00,
     "id_categoria": 2,
     "id_estoque": 3
   }
   ```

2. **Resposta**:
   ```json
   {
     "id_brinquedo": 3,
     "nome_brinquedo": "Lego",
     "tipo_brinquedo": "Brinquedo de montar",
     "classificacao": "6+",
     "tamanho": "Grande",
     "preco": 100.00,
     "id_categoria": 2,
     "id_estoque": 3
   }
   ```

