param (
    $resourceGroupName
)
Write-Host "RG NAME" + $resourceGroupName;
$secrets = Get-AzStaticWebAppSecret -name coffeclub-dev-staticapp -ResourceGroupName $resourceGroupName
$dict = $secrets.ToJsonString() | ConvertFrom-Json
$apiKey = $dict.properties.apiKey
Write-Host "GARY1"
Write-Host $apiKey
Write-Host "GARY2"
Write-Output $apiKey