# 📬 dotnet-cep-api

API REST desenvolvida em **.NET 8** para consulta de CEP com sistema de cache, autenticação JWT e rate limiting.

---

## 🚀 Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- PostgreSQL
- Dapper
- JWT Authentication
- BCrypt
- Rate Limiting (nativo .NET 8)
- FluentValidation
- Serilog

---

## 📌 Funcionalidades

- ✅ Consulta de CEP com integração à API ViaCEP
- ✅ Sistema de cache para consultas repetidas (reduz chamadas externas)
- ✅ Autenticação JWT (login e registro)
- ✅ Rate limiting por endpoint (proteção contra DDoS)
  - Global: 60 requisições/minuto
  - Login: 5 tentativas/minuto
  - Registro: 3 tentativas/minuto
- ✅ CRUD de usuários
- ✅ Validação de senha com complexidade
- ✅ Proteção IDOR (uso de ID do token)

---

## 🏗️ Estrutura do Projeto

O projeto segue separação em camadas:

| Camada | Responsabilidade |
|--------|------------------|
| **API** | Controllers, endpoints, configuração |
| **Application** | Services, DTOs, Validators, Interfaces |
| **Domain** | Entities, Enums |
| **Infrastructure** | Repositories, Unit of Work, External Services |

---

## 🔐 Segurança

A aplicação implementa múltiplas camadas de segurança:

- **JWT** - Autenticação stateless
- **BCrypt** - Hashing seguro de senhas
- **Rate Limiting** - Proteção contra força bruta e DDoS
- **IDOR Prevention** - Operações usam ID do token
- **FluentValidation** - Validação de entrada

---

## 📊 Endpoints Principais

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|------|
| POST | `/api/auth/login` | Autenticação | ❌ |
| POST | `/api/user` | Cadastro | ❌ |
| GET | `/api/user/me` | Perfil | ✅ |
| PUT | `/api/user` | Atualizar perfil | ✅ |
| DELETE | `/api/user` | Deletar conta | ✅ |
| GET | `/api/address/cep/{cep}` | Consultar CEP | ✅ |

---

## 🎯 Objetivo do Projeto

Este projeto foi desenvolvido com foco em:

- Consumo de APIs externas (ViaCEP)
- Implementação de cache para otimização
- Autenticação JWT do zero
- Rate limiting para proteção
- Arquitetura em camadas
- Validações robustas
- Segurança contra vulnerabilidades comuns (IDOR, força bruta)

---

## 👨‍💻 Autor

**Caue Alves**  
Desenvolvedor Backend .NET  
[GitHub](https://github.com/caue010101)

---

> Projeto desenvolvido para fins de estudo e portfólio.
