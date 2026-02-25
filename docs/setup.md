# Setup Guide

## Prérequis
- .NET 10 SDK
- Node.js 20 LTS
- PostgreSQL 16
- Unity 2022.3 LTS

## Installation Backend
```bash
cd backend
dotnet restore
dotnet build

Installation Frontend
cd frontend
npm install
npm run dev

Base de données
psql -U postgres
CREATE DATABASE jrpg_dev;

Configuration
Copier appsettings.json → appsettings.Development.json
Configurer connection string PostgreSQL
