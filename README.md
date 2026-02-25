# RPG_ESI07

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Vue.js](https://img.shields.io/badge/Vue.js-3.x-4FC08D)](https://vuejs.org/)
[![Unity](https://img.shields.io/badge/Unity-2022.3_LTS-000000)](https://unity.com/)

Projet RNCP 36286 - Expert en Informatique et Systèmes d'Information  
Spécialisation: **Cybersécurité**

## 📋 Description

Architecture distribuée sécurisée combinant:
- **Client Unity** (Windows) - Jeu RPG tour par tour 2D
- **API REST** ASP.NET Core 10 - Backend sécurisé
- **Portail Vue.js 3** + Vuetify - Interface web
- **PostgreSQL 16** - Base de données (Docker)

## 🛠️ Stack Technique

### Backend
- .NET 10
- PostgreSQL 16 (Docker)
- Entity Framework Core 10
- JWT Authentication + MFA TOTP
- Argon2id password hashing
- Azure (production)

### Frontend
- Vue.js 3
- Vuetify 3
- Vite
- Pinia (state management)
- Axios

### Client Jeu
- Unity 2022.3 LTS
- Universal Render Pipeline (URP)
- C# scripting

### Sécurité
- MFA TOTP obligatoire
- Chiffrement AES-256 (données repos)
- TLS 1.3 (transit)
- SAST/DAST CI/CD
- OWASP Top 10 compliance

## 🚀 Quick Start

### Prérequis
- Docker Desktop
- .NET 10 SDK
- Node.js 22 LTS
- Unity 2022.3 LTS

### 1. Base de données (Docker)
```bash
# Démarrer PostgreSQL + pgAdmin
docker-compose up -d

# Vérifier
docker-compose ps
```

### 2. Backend
```bash
cd backend
dotnet restore
dotnet build
cd RPG_ESI07.API
dotnet run

# API: https://localhost:5001
# Swagger: https://localhost:5001/swagger
```

### 3. Frontend
```bash
cd frontend
npm install
npm run dev

# App: http://localhost:5173
```

### 4. Unity Client
```bash
# Ouvrir Unity Hub
# Add project: ./unity-client
# Open project
# Play ▶️
```

## 📚 Documentation

- [Setup Guide](docs/SETUP.md)
- [Architecture](docs/architecture/)
- [API Specification](docs/api-spec/)
- [Security Policy](SECURITY.md)

## 🔒 Sécurité

Voir [SECURITY.md](SECURITY.md) pour:
- Signalement vulnérabilités
- Mesures de sécurité implémentées
- Conformité RGPD

## 🧪 Tests
```bash
# Tests backend
cd backend
dotnet test

# Tests frontend
cd frontend
npm run test
```

## 📊 Status

- ✅ Setup environnement
- ⏳ Développement en cours

## 📄 License

Ce projet est sous licence [MIT](LICENSE).

## 👤 Auteur

**[Votre Nom]**  
Projet académique RNCP 36286 - 2026

## 🙏 Remerciements

- [OWASP](https://owasp.org/) - Ressources sécurité
- [Microsoft](https://docs.microsoft.com/) - Documentation .NET
- Communauté open-source

---

⭐ Si ce projet vous aide, n'hésitez pas à lui donner une étoile !