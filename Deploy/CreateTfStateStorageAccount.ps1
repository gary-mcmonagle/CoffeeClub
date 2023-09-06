# Param (
#     $appId,
#     $password,
#     $tenantId,
#     $subscriptionId
# )
# $SecurePassword = ConvertTo-SecureString -String $password -AsPlainText -Force
# $TenantId = $tenantId
# $ApplicationId = $appId
# $Credential = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $ApplicationId, $SecurePassword
# Connect-AzAccount -ServicePrincipal -TenantId $TenantId -Credential $Credential -subscriptionId $subscriptionId

$RESOURCE_GROUP_NAME='tfstate'
$STORAGE_ACCOUNT_NAME="tfstate446022584"
# $CONTAINER_NAME='tfstate'

# # Create resource group
# New-AzResourceGroup -Name $RESOURCE_GROUP_NAME -Location eastus

# # Create storage account
# $storageAccount = New-AzStorageAccount -ResourceGroupName $RESOURCE_GROUP_NAME -Name $STORAGE_ACCOUNT_NAME -SkuName Standard_LRS -Location eastus -AllowBlobPublicAccess $false

# # Create blob container
# New-AzStorageContainer -Name $CONTAINER_NAME -Context $storageAccount.context
$ACCOUNT_KEY=(Get-AzStorageAccountKey -ResourceGroupName $RESOURCE_GROUP_NAME -Name $STORAGE_ACCOUNT_NAME)[0].value
$env:ARM_ACCESS_KEY=$ACCOUNT_KEY