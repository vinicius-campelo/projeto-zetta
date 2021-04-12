# projeto-zetta


## Rodando a API.
### 1 - docker-compose up
### 2 - F5


### para vê o cadastro no banco postgres fazer os seguintes passos:
#### docker pull dpage/pgadmin4
#### docker network create --driver bridge postgres-network
#### docker run --name meetgroup-pgadmin --network=postgres-network -p 15432:80 -e "PGADMIN_DEFAULT_EMAIL=meetgroup@email.com" -e "PGADMIN_DEFAULT_PASSWORD=meetgroup" -d dpage/pgadmin4

#### Acesso: http://localhost:15432/browser/


# API acesso token
- {
-   "email": "autanbr@gmail.com",
-   "senha": "123456"
- }
