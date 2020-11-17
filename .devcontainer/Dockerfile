FROM mcr.microsoft.com/dotnet/core/sdk:3.1.404-bionic

# Install Azure CLI
RUN curl -sL https://aka.ms/InstallAzureCLIDeb |  bash

# Install Terraform CLI
## Ready for using apt-add-repository
RUN  apt-get install --assume-yes software-properties-common
RUN  apt-get update

RUN curl -fsSL https://apt.releases.hashicorp.com/gpg |  apt-key add -
RUN  apt-add-repository "deb [arch=amd64] https://apt.releases.hashicorp.com $(lsb_release -cs) main"
RUN  apt-get update &&  apt-get install terraform