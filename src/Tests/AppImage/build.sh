#!/usr/bin/env bash 
cd ..
dotnet publish -o AppImage/GermanBread.AppImageUpdater/usr/bin -r linux-x64 -c RELEASE
cd AppImage

sh appimage-helper.sh GermanBread.AppImageUpdater