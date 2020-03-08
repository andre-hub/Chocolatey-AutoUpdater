# Chocolatey-AutoUpdater
Chocolatey-AutoUpdater runs as NT service in the background. It calls the normal chocolatey application at intervals, for example every 4 hours.
It serves the user to keep the applications up to date without the user having to worry about it.
However, you should only install and run this application if you trust the respective installed applications and sources.

## NT-Service registration
`sc.exe create Chocolatey-AutoUpdater binpath= "C:\Program Files\Chocolatey.AutoUpdater\Chocolatey.AutoUpdater.exe" start= auto DisplayName= Chocolatey-AutoUpdater`
