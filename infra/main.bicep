targetScope = 'resourceGroup'

@description('Primary location for deployed resources')
param location string = resourceGroup().location

output AZURE_LOCATION string = location
