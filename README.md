Env vars:
PASSWORD_ENCRYPT: contraseña para algoritmo de encriptado
CONNECTION_STRING: connection string encriptado
ENVIRONMENT: DEVELOPMENT|PRODUCTION

Scaffold:
Scaffold-DbContext "Server=.\SQLExpress;Database=Worter;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f