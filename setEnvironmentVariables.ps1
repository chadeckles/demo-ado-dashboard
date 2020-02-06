Param(
[string]$sauceUserName,
[string]$sauceAccessKey,
[string]$office365UserName,
[string]$office365Password
)
Write-Output "sauce.userName stored in Azure DevOps=>=>$sauceUserName"
Write-Output "sauce.accessKey stored in Azure DevOps=>=>$sauceAccessKey"
Write-Output "365.user stored in Azure DevOps=>$rdcVodQaNativeAppApiKey"
Write-Output "365.pass stored in Azure DevOps=>$rdcSauceDemoIosRdcApiKey"

[Environment]::SetEnvironmentVariable("SAUCE_USERNAME", "$sauceUserName", "User")
[Environment]::SetEnvironmentVariable("SAUCE_ACCESS_KEY", "$sauceAccessKey", "User")
[Environment]::SetEnvironmentVariable("365_USER", "$office365UserName", "User")
[Environment]::SetEnvironmentVariable("365_PASS", "$office365Password", "User")