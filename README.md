# **ImobiManager**

## Tecnologias Utilizadas

- **Backend**: .NET 9 (API)
- **Banco de Dados**: SQL Server Express
- **ORM**: Entity Framework Core
- **Autenticação**: JWT (JSON Web Token)
- **Docker**: Docker Compose para subir o ambiente

## Descrição do Projeto

Este projeto implementa uma API para gerenciar vendas e reservas de apartamentos. Ele permite a autenticação de usuários, cadastro de clientes, apartamentos, reservas e vendas.

## Como Rodar o Projeto Localmente com Docker

1. **Clonando o Repositório** Para clonar o repositório e configurar o ambiente, execute o seguinte comando:

   ```bash
   git clone https://github.com/joao-victor-dias/ImobiManager.git
   cd imobimanager
   ```

2. **Subindo os Containers com Docker Compose** Certifique-se de que o Docker e o Docker Compose estão instalados em sua máquina. Para subir os containers com o SQL Server Express e a API, execute:

   ```bash
   docker-compose up --build
   ```
   
   Este comando vai criar e iniciar os containers para o banco de dados e o serviço da API.
   
3. **Acessando a API** A API estará disponível em `http://localhost:8080`.

4. **Parando os Containers** Quando terminar de testar, você pode parar os containers com:

   ```bash
   docker compose down -v
   ```

## Endpoints da API

### 1. Autenticação (Login)

- **POST** `/api/auth/login`

  Exemplo de requisição:

  ```
  curl -X POST http://localhost:8080/api/auth/login -H "Content-Type: application/json" -d '{"username": "admin", "password": "admin"}'
  ```

  Resposta:

  ```json
  {
    "token": "seu-token-jwt"
  }
  ```

  O token gerado será usado para autenticar as requisições subsequentes.

### 2. Cadastrar Cliente

- **POST** `/api/clients`

  Exemplo de requisição:

  ```
  curl -X POST http://localhost:8080/api/clients -H "Authorization: Bearer jwt-token" -H "Content-Type: application/json" -d '{"Name": "João da Silva", "Email": "joao@email.com", "Phone": "123456789", "Address": "Rua X, 123", "CpfCnpj": "12345678901", "IsCompany": false, "DateOfBirth": "1980-01-01"}'
  ```

  Exemplo de resposta (registro bem-sucedido):

  ```json
  {
  	"id": 1,
  	"name": "João da Silva",
  	"cpfCnpj": "12345678901",
  	"isCompany": false,
  	"dateOfBirth": "1980-01-01T00:00:00",
  	"email": "joao@email.com",
  	"phone": "123456789",
  	"address": "Rua X, 123"
  }
  ```

------

### 3. Listar Clientes

- **GET** `/api/clients`

  Exemplo de requisição:

  ```
  curl -X GET http://localhost:8080/api/clients -H "Authorization: Bearer jwt-token"
  ```

  Exemplo de resposta (lista de clientes):

  ```json
  [
  	{
  		"id": 1,
  		"name": "João da Silva",
  		"cpfCnpj": "12345678901",
  		"isCompany": false,
  		"dateOfBirth": "1980-01-01T00:00:00",
  		"email": "joao@email.com",
  		"phone": "123456789",
  		"address": "Rua X, 123"
  	}
  ]
  ```

------

### 4. Atualizar Cliente

- **PUT** `/api/clients/{id}`

  Exemplo de requisição:

  ```
  curl -X PUT http://localhost:8080/api/clients/1 -H "Authorization: Bearer jwt-token" -H "Content-Type: application/json" -d '{"Name": "João Updated", "Email": "joao@newemail.com", "Phone": "987654321", "Address": "Rua Y, 456", "CpfCnpj": "12345678901", "IsCompany": false, "DateOfBirth": "1980-01-01"}'
  ```

  Resposta:  **Status 204 No Content:** A atualização foi bem-sucedida. Nenhum conteúdo será retornado no corpo da resposta.

------

### 5. Deletar Cliente

- **DELETE** `/api/clients/{id}`

  Exemplo de requisição:

  ```
  curl -X DELETE http://localhost:8080/api/clients/1 -H "Authorization: Bearer jwt-token"
  ```

  Exemplo de resposta (exclusão bem-sucedida):

  ```json
  {
    "message": "Cliente excluído com sucesso."
  }
  ```

------

### 6. Buscar Cliente por ID

- **GET** `/api/clients/{id}`

  Exemplo de requisição:

  ```
  curl -X GET http://localhost:8080/api/clients/1 -H "Authorization: Bearer jwt-token"
  ```

  Exemplo de resposta (se o cliente for encontrado):

  ```json
  {
  	"id": 1,
  	"name": "João Updated",
  	"cpfCnpj": "12345678901",
  	"isCompany": false,
  	"dateOfBirth": "1980-01-01T00:00:00",
  	"email": "joao@newemail.com",
  	"phone": "987654321",
  	"address": "Rua Y, 456"
  }
  ```

  ------

### 7. Cadastrar Apartamento

- **POST** `/api/apartments`

  Exemplo de requisição:

  ```
  curl -X POST http://localhost:8080/api/apartaments -H "Authorization: Bearer jwt-token" -H "Content-Type: application/json" -d '{
    "Number": 101, 
    "BlockOrTower": "A", 
    "Floor": 1, 
    "Area": 75.5, 
    "Bedrooms": 2, 
    "Bathrooms": 1, 
    "GarageSpaces": 1, 
    "Price": 250000.0, 
    "Address": "Rua X, 123", 
    "Status": 0, 
    "Description": "Apartamento de 2 quartos com excelente localização."
  }'
  ```

  Status pode ser:  

  - Disponível = 0
  
  - Reservado = 1
  
  - Vendido = 2
  
  Exemplo de resposta (registro bem-sucedido):
  
  ```json
  {
    "id": 1,
    "number": 101,
    "blockOrTower": "A",
    "floor": 1,
    "area": 75.5,
    "bedrooms": 2,
    "bathrooms": 1,
    "garageSpaces": 1,
    "price": 250000,
    "address": "Rua X, 123",
    "status": 0,
    "description": "Apartamento de 2 quartos com excelente localização.",
    "reservation": null,
    "sale": null
  }
  ```
  
  ------

### 8. Listar Apartamentos

- **GET** `/api/apartments`

  Exemplo de requisição:

  ```
  curl -X GET http://localhost:8080/api/apartaments -H "Authorization: Bearer jwt-token"
  ```
  
  Exemplo de resposta (lista de apartamentos):
  
  ```json
  [
    {
      "id": 1,
      "number": 101,
      "blockOrTower": "A",
      "floor": 1,
      "area": 75.5,
      "bedrooms": 2,
      "bathrooms": 1,
      "garageSpaces": 1,
      "price": 250000,
      "address": "Rua X, 123",
      "status": 0,
      "description": "Apartamento de 2 quartos com excelente localização.",
      "reservation": null,
      "sale": null
    }
  ]
  ```

------

### 9. Atualizar Apartamento

- **PUT** `/api/apartments/{id}`

  Exemplo de requisição:

  ```
  curl -X PUT http://localhost:8080/api/apartaments/1 -H "Authorization: Bearer jwt-token" -H "Content-Type: application/json" -d '{
    "Number": 101, 
    "BlockOrTower": "A", 
    "Floor": 1, 
    "Area": 75.5, 
    "Bedrooms": 2, 
    "Bathrooms": 1, 
    "GarageSpaces": 1, 
    "Price": 260000.0, 
    "Address": "Rua X, 123", 
    "Status": 2, 
    "Description": "Apartamento de 2 quartos com excelente localização e preço atualizado."
  }'
  ```

  Resposta: **Status 204 No Content:** A atualização foi bem-sucedida. Nenhum conteúdo será retornado no corpo da resposta.

------

### 10. Deletar Apartamento

- **DELETE** `/api/apartments/{id}`

  Exemplo de requisição:

  ```
  curl -X DELETE http://localhost:8080/api/apartaments/1 -H "Authorization: Bearer jwt-token"
  ```
  
  Exemplo de resposta (exclusão bem-sucedida):
  
  ```
  {
    "message": "Apartamento excluído com sucesso."
  }
  ```

------

### 11. Buscar Apartamento por ID

- **GET** `/api/apartments/{id}`

  Exemplo de requisição:

  ```
  curl -X GET http://localhost:8080/api/apartaments/1 -H "Authorization: Bearer jwt-token"
  ```
  
  Exemplo de resposta (se o apartamento for encontrado):
  
  ```json
  {
    "id": 1,
    "number": 101,
    "blockOrTower": "A",
    "floor": 1,
    "area": 75.5,
    "bedrooms": 2,
    "bathrooms": 1,
    "garageSpaces": 1,
    "price": 260000,
    "address": "Rua X, 123",
    "status": 2,
    "description": "Apartamento de 2 quartos com excelente localização e preço 	atualizado.",
    "reservation": null,
    "sale": null
  }
  ```

### 12. Cadastrar Reserva

- **POST** `/api/reservation`

  Exemplo de requisição:

  ```
  curl -X POST http://localhost:8080/api/reservation -H "Authorization: Bearer jwt-token" -H "Content-Type: application/json" -d '{
    "ClientId": 1,
    "ApartamentId": 1,
    "ReservationDate": "2025-03-10"
  }'
  ```

  Exemplo de resposta (registro de reserva bem-sucedido):

  ```json
  {
    "id": 1,
    "clientId": 1,
    "apartamentId": 1,
    "reservationDate": "2025-03-10T00:00:00",
    "client": {
      "id": 1,
      "name": "João da Silva",
      "cpfCnpj": "12345678901",
      "isCompany": false,
      "dateOfBirth": "1980-01-01T00:00:00",
      "email": "joao@email.com",
      "phone": "123456789",
      "address": "Rua X, 123"
    },
    "apartament": {
      "id": 1,
      "number": 101,
      "blockOrTower": "A",
      "floor": 1,
      "area": 75.5,
      "bedrooms": 2,
      "bathrooms": 1,
      "garageSpaces": 1,
      "price": 260000,
      "address": "Rua X, 123",
      "status": 1,
      "description": "Apartamento de 2 quartos com excelente localização e preço atualizado."
    }
  }
  ```

------

### 13. Listar Reservas

- **GET** `/api/reservation`

  Exemplo de requisição:

  ```
  curl -X GET http://localhost:8080/api/reservation -H "Authorization: Bearer jwt-token"
  ```
  
  Exemplo de resposta (lista de reservas):
  
  ```json
  [
    {
      "id": 6,
      "clientId": 1,
      "apartamentId": 1,
      "reservationDate": "2025-03-10T00:00:00",
      "client": {
        "id": 1,
        "name": "João da Silva",
        "cpfCnpj": "12345678901",
        "isCompany": false,
        "dateOfBirth": "1980-01-01T00:00:00",
        "email": "joao@email.com",
        "phone": "123456789",
        "address": "Rua X, 123"
      },
      "apartament": {
        "id": 1,
        "number": 101,
        "blockOrTower": "A",
        "floor": 1,
        "area": 75.5,
        "bedrooms": 2,
        "bathrooms": 1,
        "garageSpaces": 1,
        "price": 260000,
        "address": "Rua X, 123",
        "status": 1,
        "description": "Apartamento de 2 quartos com excelente localização e preço atualizado."
      }
    }
  ]
  ```

------

### 14. Atualizar Reserva

- **PUT** `/api/reservation/{id}`

  Exemplo de requisição:

  ```
  curl -X PUT http://localhost:8080/api/reservation/1 -H "Authorization: Bearer jwt-token" -H "Content-Type: application/json" -d '{
    "ClientId": 1,
    "ApartamentId": 1,
    "ReservationDate": "2025-03-15T15:30:00"
  }'
  ```

  Resposta: **Status 204 No Content:** A atualização foi bem-sucedida. Nenhum conteúdo será retornado no corpo da resposta.


------

### 15. Deletar Reserva

- **DELETE** `/api/reservation/{id}`

  Exemplo de requisição:

  ```
  curl -X DELETE http://localhost:8080/api/reservations/1 -H "Authorization: Bearer jwt-token"
  ```
  
  Exemplo de resposta (reserva excluída com sucesso):
  
  ```json
  {
    "message": "Reserva excluída com sucesso."
  }
  ```

------

### 16. Buscar Reserva por ID

- **GET** `/api/reservation/{id}`

  Exemplo de requisição:

  ```
  curl -X GET http://localhost:8080/api/reservation/1 -H "Authorization: Bearer jwt-token"
  ```
  
  Exemplo de resposta (se a reserva for encontrada):
  
  ```json
  {
    "id": 1,
    "clientId": 1,
    "apartamentId": 1,
    "reservationDate": "2025-03-15T15:30:00",
    "client": {
      "id": 1,
      "name": "João da Silva",
      "cpfCnpj": "12345678901",
      "isCompany": false,
      "dateOfBirth": "1980-01-01T00:00:00",
      "email": "joao@email.com",
      "phone": "123456789",
      "address": "Rua X, 123"
    },
    "apartament": {
      "id": 1,
      "number": 101,
      "blockOrTower": "A",
      "floor": 1,
      "area": 75.5,
      "bedrooms": 2,
      "bathrooms": 1,
      "garageSpaces": 1,
      "price": 260000,
      "address": "Rua X, 123",
      "status": 1,
      "description": "Apartamento de 2 quartos com excelente localização e preço atualizado."
    }
  }
  ```
------
### 17. Cadastrar Venda

- **POST** `/api/sales`

  **Exemplo de requisição:**

  ```
  curl -X POST http://localhost:8080/api/sales -H "Authorization: Bearer jwt-token" -H "Content-Type: application/json" -d '{
    "clientId": 1,
    "apartamentId": 1,
    "saleDate": "2025-03-09"
  }'
  ```
  
  **Exemplo de resposta :**

  ```json
  {
    "id": 1,
    "clientId": 1,
    "apartamentId": 1,
    "saleDate": "2025-03-09T00:00:00",
    "client": null,
    "apartament": {
      "id": 1,
      "number": 101,
      "blockOrTower": "A",
      "floor": 1,
      "area": 75.5,
      "bedrooms": 2,
      "bathrooms": 1,
      "garageSpaces": 1,
      "price": 260000,
      "address": "Rua X, 123",
      "status": 2,
      "description": "Apartamento de 2 quartos com excelente localização e preço atualizado."
    }
  }
  ```

------

### 18. Listar Vendas

- **GET** `/api/sales`

  **Exemplo de requisição:**

  ```
  curl -X GET http://localhost:8080/api/sales -H "Authorization: Bearer jwt-token"
  ```
  
  **Exemplo de resposta (lista de pagamentos):**
  
  ```json
  [
    {
      "id": 1,
      "clientId": 1,
      "apartamentId": 1,
      "saleDate": "2025-03-09T00:00:00",
      "client": {
        "id": 1,
        "name": "João da Silva",
        "cpfCnpj": "12345678901",
        "isCompany": false,
        "dateOfBirth": "1980-01-01T00:00:00",
        "email": "joao@email.com",
        "phone": "123456789",
        "address": "Rua X, 123"
      },
      "apartament": {
        "id": 1,
        "number": 101,
        "blockOrTower": "A",
        "floor": 1,
        "area": 75.5,
        "bedrooms": 2,
        "bathrooms": 1,
        "garageSpaces": 1,
        "price": 260000,
        "address": "Rua X, 123",
        "status": 2,
        "description": "Apartamento de 2 quartos com excelente localização e preço atualizado."
      }
    }
  ]
  ```

------

### 19. Atualizar Venda

- **PUT** `/api/sales/{id}`

  **Exemplo de requisição:**

  ```
  curl -X PUT http://localhost:8080/api/sales/1 -H "Authorization: Bearer jwt-token" -H "Content-Type: application/json" -d '{
    "clientId": 1,
    "apartamentId": 2,
    "saleDate": "2025-03-09"
  }'
  ```
  
  Resposta: **Status 204 No Content:** A atualização foi bem-sucedida. Nenhum conteúdo será retornado no corpo da resposta.


------

### 20. Deletar Venda

- **DELETE** `/api/sales/{id}`

  **Exemplo de requisição:**

  ```
  curl -X DELETE http://localhost:8080/api/sales/1 -H "Authorization: Bearer jwt-token"
  ```
  
  **Exemplo de resposta (pagamento excluído com sucesso):**
  
  ```json
  {
    "message": "Venda excluída com sucesso."
  }
  ```

------

### 21. Buscar Venda por ID

- **GET** `/api/sales/{id}`

  **Exemplo de requisição:**

  ```
  curl -X GET http://localhost:8080/api/sales/1 -H "Authorization: Bearer jwt-token"
  ```
  
  **Exemplo de resposta:**
  
  ```json
  {
    "id": 1,
    "clientId": 1,
    "apartamentId": 1,
    "saleDate": "2025-03-09T00:00:00",
    "client": {
      "id": 1,
      "name": "João da Silva",
      "cpfCnpj": "12345678901",
      "isCompany": false,
      "dateOfBirth": "1980-01-01T00:00:00",
      "email": "joao@email.com",
      "phone": "123456789",
      "address": "Rua X, 123"
    },
    "apartament": {
      "id": 1,
      "number": 101,
      "blockOrTower": "A",
      "floor": 1,
      "area": 75.5,
      "bedrooms": 2,
      "bathrooms": 1,
      "garageSpaces": 1,
      "price": 260000,
      "address": "Rua X, 123",
      "status": 2,
      "description": "Apartamento de 2 quartos com excelente localização e preço atualizado."
    }
  }
  ```

## Considerações:

- A autenticação foi implementada utilizando JWT, assegurando que apenas usuários autenticados possam acessar os endpoints da aplicação.
- O Entity Framework foi adotado para simplificar o acesso e manipulação dos dados no banco, proporcionando maior agilidade e facilidade de manutenção.
- A estrutura do banco de dados foi cuidadosamente projetada para suportar os módulos de clientes, apartamentos, reservas e vendas, garantindo a integridade e consistência das transações.
- O Docker Compose foi empregado para otimizar a configuração e execução do ambiente de desenvolvimento, tornando o processo mais ágil e reproduzível.





