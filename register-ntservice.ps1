$serviceRegistrationName="Chocolatey.AutoUpdater"
$installExe="C:\Program Files\$($serviceRegistrationName)\$($serviceRegistrationName).exe"
$accountName="AdminUser"
$password="AdminUserPassword"
$logfile="c:\windows\temp\service-register.log"
$serviceDisplayName=$serviceRegistrationName

#sc.exe stop $($serviceRegistrationName)
#sc.exe delete $($serviceRegistrationName)
sc.exe create $serviceRegistrationName binpath= $installExe start= auto DisplayName= $serviceDisplayName obj= $accountName password= $password 2>&1 | Out-File -FilePath $logfile -Append
sc.exe start $($serviceRegistrationName)