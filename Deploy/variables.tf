variable "project" {
  type = string
  default = "coffeclub"
  description = "Project name"
}

variable "environment" {
  type = string
  default = "dev"
  description = "Environment (dev / stage / prod)"
}

variable "location" {
  type = string
  default = "West Europe"
  description = "Azure region to deploy module to"
}

variable "passwordlessConnectionString" {
  type = string
  default = "TODO"
  description = "Passwordless connection string"
}

variable "appId" {
  type = string
  description = "appId"
}
variable "password" {
  type = string
  description = "password"
    sensitive = true
}
variable "tenant" {
  type = string
  description = "tenant"
}

variable "subscription_id" {
  type = string
  description = "subscription_id"
    sensitive = true

}