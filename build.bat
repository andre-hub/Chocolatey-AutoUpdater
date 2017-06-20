@echo off
SET projectName=Chocolatey.AutoUpdater
SET setupName=Chocolatey-AutoUpdater-Setup.msi
SET nuspecName=Chocolatey-AutoUpdater.nuspec

SET WixToolSetPath="C:\Program Files (x86)\WiX Toolset v4.0\bin"
SET WixSetupPath=source\%projectName%.Setup
SET WixSetupProject="Setup.wixproj"
SET source=source\%projectName%\%projectName%.csproj
SET outputPath="..\..\setup"

echo %source%
dotnet build %source%

cd %WixSetupPath%
%WixToolSetPath%\candle.exe -out obj\Release\ -arch x64 Product.wxs
%WixToolSetPath%\Light.exe -out %outputPath%\\%setupName% -cultures:null -contentsfile obj\Release\Setup.wixproj.BindContentsFileList.txt -outputsfile obj\Release\Setup.wixproj.BindOutputsFileList.txt -builtoutputsfile obj\Release\Setup.wixproj.BindBuiltOutputsFileList.txt -wixprojectfile %WixSetupProject% obj\Release\Product.wixobj
cd ..\..\

choco pack %nuspecName%