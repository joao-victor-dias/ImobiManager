# **ImobiManager**

## **1 - Pré-requisitos**

Antes de rodar o projeto, certifique-se de ter instalado em sua máquina:

- [Docker](https://www.docker.com/)

------

## **2 - Como rodar o projeto?**

### **2.1 Clonar o repositório**

```bash
git clone https://github.com/joao-victor-dias/ImobiManager.git
cd imobimanager
```

### **2.2 - Subir os containers com Docker Compose**

Agora que o banco de dados foi criado corretamente, suba a aplicação e o banco via Docker:

```bash
cd ..
docker-compose up -d --build
```

------

## **3 - Como acessar a API?**

Após rodar o `docker-compose`, a API estará disponível em:

```bash
http://localhost:8080
```

Use ferramentas como **Postman** ou **Insomnia** para testar os endpoints.

------

## **6 - Diagrama das tabelas criadas no banco de dados**

![image-20250307232021893](C:\Users\joaov\AppData\Roaming\Typora\typora-user-images\image-20250307232021893.png)
