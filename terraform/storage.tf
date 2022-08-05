# Storage account
resource "azurerm_storage_account" "bulky" {
  name                     = "udemybulkysa"
  resource_group_name      = azurerm_resource_group.bulky.name
  location                 = azurerm_resource_group.bulky.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}
