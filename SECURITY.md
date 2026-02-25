# Security Policy

## Supported Versions

| Version | Supported          |
| ------- | ------------------ |
| 1.0.x   | :white_check_mark: |

## Reporting a Vulnerability

**IMPORTANT**: Ce projet est à but éducatif et académique (RNCP 36286).

Pour signaler une vulnérabilité:

1. **NE PAS** créer de GitHub Issue public
2. Envoyer un email à: [votre-email]@example.com
3. Inclure:
   - Description de la vulnérabilité
   - Étapes de reproduction
   - Impact potentiel
   - Suggestions de correction (si applicable)

## Délai de Réponse

- Accusé réception: 48h
- Analyse: 7 jours
- Correction: 30 jours (selon criticité)

## Mesures de Sécurité Implémentées

### Authentification
- MFA TOTP obligatoire (Google Authenticator)
- JWT tokens (expiration 15min)
- Rate limiting (5 tentatives/5min)
- Account lockout automatique

### Données
- Passwords: Argon2id hashing (OWASP 2024)
- Données sensibles: AES-256 encryption
- Transit: TLS 1.3 uniquement
- Base de données: TDE (Transparent Data Encryption)

### Application
- Input validation: FluentValidation
- Output encoding: Anti-XSS
- CSRF protection: Tokens + SameSite cookies
- SQL Injection: Parameterized queries (EF Core)

### Infrastructure
- Docker containers isolated
- Secrets management: Azure Key Vault (production)
- Network: Private endpoints
- Monitoring: SIEM Azure Monitor

### CI/CD
- SAST: SonarQube (chaque commit)
- DAST: OWASP ZAP (hebdomadaire)
- Dependency scanning: Dependabot
- Secret scanning: Gitleaks

## Conformité

- ✅ RGPD (endpoints export/suppression données)
- ✅ OWASP Top 10 2021
- ✅ ISO 27001 (contrôles sélectionnés)

## Divulgation Responsable

Ce projet pratique la divulgation responsable:
1. Signalement privé
2. Analyse et correction
3. Publication coordonnée (si applicable)

## Contact

Pour toute question sécurité: [votre-email]@example.com

---

Dernière mise à jour: [Date]