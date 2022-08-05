resource "null_resource" "build_images" {
  provisioner "local-exec" {
    working_dir = "${path.module}/../"
    command     = "docker build -t udemybulkycr.azurecr.io/udemybulkybook ./"
  }
}

resource "null_resource" "upload_images" {
  triggers = {
    order = azurerm_container_registry.bulky.id
  }

  provisioner "local-exec" {
    working_dir = "${path.module}/../"
    command     = "az acr login --name udemybulkycr && docker push udemybulkycr.azurecr.io/udemybulkybook:latest"
  }

  depends_on = [
    azurerm_container_registry.bulky,
    null_resource.build_images
  ]
}
