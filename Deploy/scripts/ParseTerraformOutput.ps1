param (
    $terraformJsonOutput
)
$output = $terraformJsonOutput | ConvertFrom-Json
$output.PSObject.Properties | ForEach-Object {
    $key = $_.Name
    $value = $_.Value.value
    Write-Output "$key=$value"
    Set-Item -LiteralPath Env:$key -Value $value
}
