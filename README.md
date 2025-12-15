# NTools API - Docker Guide

## Configuração de Variáveis de Ambiente

Antes de executar a aplicação, você precisa configurar as variáveis de ambiente:

1. Copie o arquivo `.env.example` para `.env`:
    ```bash
    cp .env.example .env
    ```

2. Edite o arquivo `.env` e preencha com suas credenciais reais:
    ```bash
    # MailerSend Configuration
    MAILERSEND__MAILSENDER=seu-email@exemplo.com
    MAILERSEND__APIURL=https://api.mailersend.com/v1/email
    MAILERSEND__APITOKEN=seu-token-mailersend

    # DigitalOcean Spaces (S3) Configuration
    S3__ACCESSKEY=sua-access-key-digitalocean
    S3__SECRETKEY=sua-secret-key-digitalocean
    S3__ENDPOINT=https://seu-space.nyc3.digitaloceanspaces.com
    ```

    ?? **IMPORTANTE**: Nunca commite o arquivo `.env` com credenciais reais. Apenas o `.env.example` deve ser versionado.

## Comando para subir o SQL Server no docker
```
docker compose -f .\sql_server_compose.yml up -d
```

## Comandos para subir a API NTools

### Build da imagem Docker
```
docker compose build
```

### Subir o container
```
docker compose up -d
```

### Ver logs do container
```
docker compose logs -f ntools-api
```

### Parar o container
```
docker compose down
```

### Rebuild completo (limpar cache)
```
docker compose build --no-cache
docker compose up -d
```

## Acessar a API
Após subir o container, a API estará disponível em:
- http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

## Deploy na Azure
```
az container create --resource-group GoblinWarsRecursos --file deployPodsAz.yml
```

## Check Deploy
```
az container show --resource-group GoblinWarsRecursos --name goblin-wars --output table
```

## Registry Log
```
az acr login --name registrygw