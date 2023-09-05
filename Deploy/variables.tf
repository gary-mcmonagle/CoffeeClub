variable "project" {
  type = string
  description = "Project name"
}

variable "environment" {
  type = string
  description = "Environment (dev / stage / prod)"
}

variable "location" {
  type = string
  description = "Azure region to deploy module to"
}

variable "passwordlessConnectionString" {
  type = string
  description = "Passwordless connection string"
}

variable "appId" {
  type = string
  description = "appId"
}
variable "password" {
  type = string
  description = "password"
}
variable "tenant" {
  type = string
  description = "tenant"
}

variable "subscription_id" {
  type = string
  description = "subscription_id"
}