# Basic app settings for provisioning
app_name = "load"

env_name = "test"

region = "westus2"

number_of_instance = 3

# Will set in Azure Container Instance environmental variables
ConnectionString = "{Your ingress Event Hub connection string}"

EventHubName = "{Your ingress Event Hub name}"

NumberOfMessages = "{Number of message per batch}"

IntervalMiliSeconds = "{Interval miliseconds app sends message batch}"