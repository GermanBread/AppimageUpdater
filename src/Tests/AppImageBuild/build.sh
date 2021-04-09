#!/usr/bin/env bash 

# Remove logfile
rm log

# Remove template
rm -rf Tests

# Create template
sh appimage-helper.sh Tests

# Compile
dotnet publish ../Tests.csproj -o Tests/usr/bin -r linux-x64 -c RELEASE

# Package
sh appimage-helper.sh Tests