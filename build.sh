#!/bin/bash

# Usage: ./build.sh ProjectName

# Get the ProjectName parameter from command line
ProjectName="$1"

# Validate if ProjectName is provided
if [ -z "$ProjectName" ]; then
    echo "Usage: ./build.sh ProjectName"
    exit 1
fi

# Install zip on Debian OS, since microsoft/dotnet container doesn't have zip by default
if [ -f /etc/debian_version ]; then
    apt -qq update
    apt -qq -y install zip
fi

ProjLocation="src/$ProjectName"

# Build and package the specified project
echo ".csproj location is: "
echo $ProjLocation
dotnet lambda package --project-location src/MoviesApi.SimpleExample --configuration release --framework net6.0 --output-package bin/release/net6.0/hello.zip

#dotnet lambda package --project-location $ProjLocation --configuration release --framework net6.0 --output-package "bin/release/net6.0/hello.zip"
