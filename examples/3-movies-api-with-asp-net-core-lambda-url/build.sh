#!/bin/bash

#install zip on debian OS, since microsoft/dotnet container doesn't have zip by default
if [ -f /etc/debian_version ]
then
  apt -qq update
  apt -qq -y install zip
fi

dotnet lambda package --project-location src/MoviesApi.AspNetCoreServer --configuration release --framework net6.0 --output-package bin/release/net6.0/hello.zip