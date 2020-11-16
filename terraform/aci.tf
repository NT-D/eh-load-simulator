resource "azurerm_container_group" "simulator" {
  count               = var.number_of_instance
  name                = "aci-${local.namespace}-${count.index}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  os_type             = "Linux"
  restart_policy      = "Always"
  ip_address_type     = "Public"

  container {
    name   = "eh-simulator"
    image  = "masota/eh-simulator"
    cpu    = "1"
    memory = "2.0"

    secure_environment_variables = {
      "ConnectionString"    = var.ConnectionString
      "EventHubName"        = var.EventHubName
      "NumberOfMessages"    = var.NumberOfMessages
      "IntervalMiliSeconds" = var.IntervalMiliSeconds
    }

    ports {
      port     = 5672
      protocol = "TCP"
    }
  }
}
