$packageName='Chocolatey-AutoUpdater'
$installerType ='msi'
$version='1.0.1'
$silentArgs = "/quiet /qn /norestart /l*v `"$env:TEMP\chocolatey\$($packageName)\$($packageName).MsiInstall.log`"" # ALLUSERS=1 DISABLEDESKTOPSHORTCUT=1 ADDDESKTOPICON=0 ADDSTARTMENU=0
$url = "https://github.com/andre-hub/$($packageName)/releases/download/$($version)/$($packageName)-Setup.$($version).msi"
$checksumType = 'sha1'
$validExitCodes = @(0)
Write-Output $url

try {
   sc.exe stop Chocolatey-AutoUpdater.exe
} catch {
  Write-ChocolateySuccess $packageName $($_.Exception.Message)
}
Install-ChocolateyPackage -PackageName "$packageName" `
                          -FileType "$installerType" `
                          -SilentArgs "$silentArgs" `
                          -Url "$url" `
                          -ValidExitCodes $validExitCodes `
                          -ChecksumType "$checksumType"

try {
   sc.exe start Chocolatey-AutoUpdater.exe
} catch {
  Write-ChocolateySuccess $packageName $($_.Exception.Message)
}