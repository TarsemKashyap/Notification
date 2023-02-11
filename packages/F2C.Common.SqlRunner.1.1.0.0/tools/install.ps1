# Runs every time a package is installed in a project

param($installPath, $toolsPath, $package, $project)

# Copy SQL Runner executable into output directory

$projectFullPath = $project.Properties.Item("FullPath").Value;
$projectActiveConfiguration = $project.ConfigurationManager.ActiveConfiguration.ConfigurationName
$dst = "$projectFullPath" + "bin\" + $projectActiveConfiguration;
#Write-Host $dst

$src = "$installPath\lib\net45\F2CSqlRunner.exe"
#Write-Host $src

Write-Host "copying " $src " to " $dst
copy $src $dst

$src = "$installPath\lib\net45\Microsoft.Data.ConnectionUI.dll"
#Write-Host $src
Write-Host "copying " $src " to " $dst
copy $src $dst

$src = "$installPath\lib\net45\Microsoft.Data.ConnectionUI.Dialog.dll"
#Write-Host $src
Write-Host "copying " $src " to " $dst
copy $src $dst


