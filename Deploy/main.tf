resource "azurerm_resource_group" "resource_group" {
  name     = "${var.project}-${var.environment}-resource-group"
  location = var.location
}

resource "azurerm_storage_account" "storage_account" {
  name                     = "${var.project}${var.environment}storage"
  resource_group_name      = azurerm_resource_group.resource_group.name
  location                 = var.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_application_insights" "application_insights" {
  name                = "${var.project}-${var.environment}-application-insights"
  location            = var.location
  resource_group_name = azurerm_resource_group.resource_group.name
  application_type    = "web"
}

resource "azurerm_app_service_plan" "app_service_plan" {
  name                = "${var.project}-${var.environment}-app-service-plan"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = var.location
  kind                = "FunctionApp"
  reserved            = true # this has to be set to true for Linux. Not related to the Premium Plan
  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_windows_function_app" "function_app" {
  name                = "${var.project}-${var.environment}-function-app"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = var.location
  service_plan_id     = azurerm_app_service_plan.app_service_plan.id
  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY" = azurerm_application_insights.application_insights.instrumentation_key,
    # "SIGNALR_SERVICE_CONNECTION_STRING" = azurerm_signalr_service.signalr_service.primary_connection_string,
  }
  site_config {
    application_stack {
      dotnet_version              = "v7.0"
      use_dotnet_isolated_runtime = true
    }
  }
  storage_account_name       = azurerm_storage_account.storage_account.name
  storage_account_access_key = azurerm_storage_account.storage_account.primary_access_key
  identity {
    type = "SystemAssigned"
  }
}

# resource "azurerm_signalr_service" "signalr_service" {
#   name                = "${var.project}-${var.environment}-signalr"
#   resource_group_name = azurerm_resource_group.resource_group.name
#   location            = var.location
#   sku {
#     name     = "Free_F1"
#     capacity = 1
#   }
#   service_mode = "Serverless"
# }

# resource "azurerm_mssql_server" "sqlserver" {
#   name                = "${var.project}-${var.environment}-sqlserver"
#   resource_group_name = azurerm_resource_group.resource_group.name
#   location            = var.location
#   version             = "12.0"

#   azuread_administrator {
#     azuread_authentication_only = true
#     login_username              = "AzureAD Admin"
#     object_id                   = "caf87c93-9030-44e8-a25d-fed63261ea67"
#   }
# }

# resource "azurerm_mssql_database" "database" {
#   name                        = "${var.project}-${var.environment}-db"
#   server_id                   = azurerm_mssql_server.sqlserver.id
#   collation                   = "SQL_Latin1_General_CP1_CI_AS"
#   max_size_gb                 = 1
#   auto_pause_delay_in_minutes = 60
#   min_capacity                = 0.5
#   read_replica_count          = 0
#   read_scale                  = false
#   sku_name                    = "GP_S_Gen5_1"
#   zone_redundant              = false
# }

# # Create SQL Server firewall rule for Azure resouces access
# resource "azurerm_sql_firewall_rule" "azureservicefirewall" {
#   name                = "allow-azure-service"
#   resource_group_name = azurerm_resource_group.resource_group.name
#   server_name         = azurerm_mssql_server.sqlserver.name
#   start_ip_address    = "0.0.0.0"
#   end_ip_address      = "0.0.0.0"
# }

resource "azurerm_static_site" "staticapp" {
  name                = "${var.project}-${var.environment}-db"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = var.location
}

