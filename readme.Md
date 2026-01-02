dotnet publish -c Release -o ./publish




name: Build and Deploy Trinkle Buddies Web API Server on Monster ASP

on:
  push:
    branches:
      - master

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest

    steps:
      - name: Checkout code
        uses: actions/checkout@v3

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '9.0.x'

      - name: Restore dependencies
        working-directory: ./P10_WebApi
        run: dotnet restore

      - name: Build 
        working-directory: ./P10_WebApi
        run: dotnet build --configuration Release

      - name: Publish project
        working-directory: ./P10_WebApi
        run: dotnet publish ./P10_WebApi.csproj --configuration Release --output ../publish

      - name: Ensure logs folder exists
        run: mkdir -p ./publish/logs

      - name: Create appsettings.Production.json
        env:
          DB_CONNECTION_STRING: ${{ secrets.DB_CONNECTION_STRING }}
          JWT_SECRET_KEY: ${{ secrets.JWT_SECRET_KEY }}
          CLOUDINARY_URL: ${{ secrets.CLOUDINARY_URL }}
          DB_NAME: ${{ secrets.DB_NAME }}
          SMTP_SERVER: ${{ secrets.SMTP_SERVER }}
          SMTP_PORT: ${{ secrets.SMTP_PORT }}
          SMTP_USERNAME: ${{ secrets.SMTP_USERNAME }}
          SMTP_PASSWORD: ${{ secrets.SMTP_PASSWORD }}
        run: |
          jq -n \
            --arg conn "$DB_CONNECTION_STRING" \
            --arg dbName "$DB_NAME" \
            --arg jwt "$JWT_SECRET_KEY" \
            --arg cloud "$CLOUDINARY_URL" \
            --arg smtpUsername "$SMTP_USERNAME" \
            --arg smtpPassword "$SMTP_PASSWORD" \
            --arg smtpServer "$SMTP_SERVER" \
            --arg smtpPort "$SMTP_PORT" \
            '{
              MongoDB: {
                ConnectionString: $conn,
                DatabaseName: $dbName
              },
              Jwt: {
                SecretKey: $jwt
              },
              Cloudinary: {
                CLOUDINARY_URL: $cloud
              },
              EmailSettings: {
                SmtpHost: $smtpServer,
                SmtpPort: $smtpPort,
                SmtpUsername: $smtpUsername,
                SmtpPassword: $smtpPassword
              },
              Logging: {
                LogLevel: {
                  Default: "Warning",
                  Microsoft: "Warning",
                  "Microsoft.AspNetCore": "Warning",
                  "Microsoft.Hosting.Lifetime": "Warning"
                }
              },
              AllowedHosts: "*"
            }' > ./publish/appsettings.Production.json

      - name: Deploy via FTP
        uses: SamKirkland/FTP-Deploy-Action@v4.3.4
        with:
          server: ${{ secrets.FTP_SERVER }}
          username: ${{ secrets.FTP_USERNAME }}
          password: ${{ secrets.FTP_PASSWORD }}
          local-dir: ./publish/
          server-dir: ${{ secrets.FTP_TARGET_DIR }}


 // "P10_WebApi": {
    //   "commandName": "Project",
    //   "dotnetRunMessages": true,
    //   "launchBrowser": false,
    //   "applicationUrl": "https://localhost:7209;http://localhost:5046",
    //   "environmentVariables": {
    //     "ASPNETCORE_ENVIRONMENT": "Development"
    //   }