# Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0.2"
    }
  }

  required_version = ">= 1.1.0"
}
provider "azurerm" {
  features {}
}

# Resource group
data "azurerm_resource_group" "example" {
  name = "UdemyBulkyRG"
}

# Storage account
resource "azurerm_storage_account" "example" {
  name                     = "udemybulkysa"
  resource_group_name      = data.azurerm_resource_group.example.name
  location                 = data.azurerm_resource_group.example.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

# SQL Server
resource "azurerm_mssql_server" "example" {
  name                         = "udemybulkysqlserver"
  resource_group_name          = data.azurerm_resource_group.example.name
  location                     = data.azurerm_resource_group.example.location
  version                      = "12.0"
  administrator_login          = var.secrets.sql_login
  administrator_login_password = var.secrets.sql_password
}

# SQL Database
resource "azurerm_mssql_database" "test" {
  name         = "udemybulkydb"
  server_id    = azurerm_mssql_server.example.id
  collation    = "SQL_Latin1_General_CP1_CI_AS"
  license_type = "LicenseIncluded"
  sku_name     = "S0"
}

# Container group
resource "azurerm_container_group" "example" {
  name                = "udemybulkyCI"
  location            = data.azurerm_resource_group.example.location
  resource_group_name = data.azurerm_resource_group.example.name
  ip_address_type     = "Public"
  dns_name_label      = "aci-udemy-bulky"
  os_type             = "Linux"

  image_registry_credential {
    server   = "udemybulkycr.azurecr.io"
    username = var.secrets.registry_login
    password = var.secrets.registry_password
  }

  container {
    name   = "udemybulkycontainer"
    image  = "udemybulkycr.azurecr.io/bulkybook"
    cpu    = "0.5"
    memory = "1.5"
    environment_variables = {
      "ASPNETCORE_ENVIRONMENT" = "Production"
    }
    secure_environment_variables = {
      "ASPNETCORE_ConnectionStrings__DefaultConnection" = "Server=tcp:${azurerm_mssql_server.example.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.test.name};Persist Security Info=False;User ID=${azurerm_mssql_server.example.administrator_login};Password=${azurerm_mssql_server.example.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
    }

    ports {
      port     = 90
      protocol = "TCP"
    }
  }
}

# Database firewall rule
resource "azurerm_mssql_firewall_rule" "example" {
  name             = "FirewallRule1"
  server_id        = azurerm_mssql_server.example.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}
