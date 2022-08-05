# Container Registry
resource "azurerm_container_registry" "bulky" {
  name                = "udemybulkycr"
  resource_group_name = azurerm_resource_group.bulky.name
  location            = azurerm_resource_group.bulky.location
  sku                 = "Basic"
  admin_enabled       = true

  depends_on = [
    azurerm_resource_group.bulky
  ]
}
