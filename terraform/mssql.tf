# SQL Server
resource "azurerm_mssql_server" "bulky" {
  name                         = "udemybulkysrv"
  resource_group_name          = azurerm_resource_group.bulky.name
  location                     = azurerm_resource_group.bulky.location
  version                      = "12.0"
  administrator_login          = "udemybulkyadmlogin"
  administrator_login_password = "Udemy@admin123"
}

# SQL Database
resource "azurerm_mssql_database" "bulky" {
  name      = "udemybulkydb"
  server_id = azurerm_mssql_server.bulky.id
  collation = "SQL_Latin1_General_CP1_CI_AS"

  auto_pause_delay_in_minutes = 60
  max_size_gb                 = 32
  min_capacity                = 0.5
  read_replica_count          = 0
  read_scale                  = false
  sku_name                    = "GP_S_Gen5_1"
  zone_redundant              = false

  threat_detection_policy {
    disabled_alerts      = []
    email_account_admins = "Disabled"
    email_addresses      = []
    retention_days       = 0
    state                = "Disabled"
  }
}

# Firewall Rule
resource "azurerm_mssql_firewall_rule" "bulky" {
  name             = "azureaccess"
  server_id        = azurerm_mssql_server.bulky.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}
