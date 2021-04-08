*You know what would be a great idea? If there were a README...*

# AppImage updater

## About

This library makes updating your Linux-AppImages dead simple

* Extremely easy to use
* It handles downloading for you
* No uncaught exceptions

## How to use

**Install from nuget using dotnet CLI**

```bash
dotnet add package GermanBread.AppImageUpdater
```

### Include namespace in your project

```cs
using GermanBread.AppImageUpdater
```

### Download an update

```cs
UpdateConfig _config = new UpdateConfig("http://example.com", "http://example.org");
Updater.Download(_config);
```

*when dealing with local files*

```cs
Updater.Download("/path/to/file");
```

### Updating

```cs
Updater.Update();
```

## Compiling from source

1. Navigate into src/GermanBread.AppImageUpdater
2. Open terminal and run `dotnet build -o build`