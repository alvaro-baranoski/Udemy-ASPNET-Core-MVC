# Container Instances
resource "azurerm_container_group" "bootcamp" {
  name                = "bootcampcg"
  location            = azurerm_resource_group.bulky.location
  resource_group_name = azurerm_resource_group.bulky.name
  ip_address_type     = "Public"
  dns_name_label      = "aciudemybulky"
  os_type             = "Linux"

  image_registry_credential {
    server   = azurerm_container_registry.bulky.login_server
    username = azurerm_container_registry.bulky.admin_username
    password = azurerm_container_registry.bulky.admin_password
  }

  # Bulky Container
  container {
    name   = "udemybulkybook"
    image  = "udemybulkycr.azurecr.io/udemybulkybook:latest"
    cpu    = "0.5"
    memory = "1.5"

    ports {
      port     = 80
      protocol = "TCP"
    }

    environment_variables = {
      "ASPNETCORE_ENVIRONMENT"                          = "Production"
      "ASPNETCORE_URLS"                                 = "http://+:80"
      "ASPNETCORE_ConnectionStrings__DefaultConnection" = "Server=tcp:${azurerm_mssql_server.bulky.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.bulky.name};Persist Security Info=False;User ID=${azurerm_mssql_server.bulky.administrator_login};Password=${azurerm_mssql_server.bulky.administrator_login_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
    }
  }

  depends_on = [
    azurerm_container_registry.bulky,
    azurerm_mssql_server.bulky,
    azurerm_mssql_database.bulky,
    null_resource.build_images,
    null_resource.upload_images
  ]
}
