terraform {
  backend "azurerm" {
      resource_group_name  = "tfstate"
      storage_account_name = "tfstate446022584"
      container_name       = "tfstate"
      key                  = "terraform.tfstate"
  }
}