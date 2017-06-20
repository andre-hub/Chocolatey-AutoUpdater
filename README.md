# Chocolatey-AutoUpdater
chocolatey automatic updater run as nt-service

## NT-Service registration
`sc.exe create Chocolatey-AutoUpdater binpath= "C:\Program Files\Chocolatey.AutoUpdater\Chocolatey.AutoUpdater.exe" start= auto DisplayName= Chocolatey-AutoUpdater`