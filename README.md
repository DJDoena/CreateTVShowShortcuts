# CreateTVShowShortcuts

A .NET console application that automatically creates Windows shortcuts for TV show season folders, providing centralized access to video files scattered across multiple drives or locations.

## Overview

CreateTVShowShortcuts scans your video file locations for TV show folders organized by seasons and creates shortcuts in a central location. This makes it easier to browse and access your TV show collection without navigating through multiple drives or complex folder structures.

## Features

- **Automatic Shortcut Generation**: Scans multiple video folders and creates shortcuts for TV show season folders
- **Multi-Drive Support**: Works with TV shows stored across multiple drives or network locations
- **Flexible Folder Patterns**: Supports both "Season X" and "Staffel X" (German) folder naming conventions
- **Article Handling**: Intelligently processes series names with articles (e.g., "The", "A", "Der", "Die", "Das")
- **Cleanup**: Automatically removes existing shortcuts before creating new ones
- **Logging**: Optional file logging with dual console/file output support
- **Configurable**: Customizable default values for paths and patterns

## Requirements

- .NET Framework 4.8.1 or higher
- Windows OS (uses Windows shortcut/LNK file format)

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/DJDoena/CreateTVShowShortcuts.git
   ```

2. Open the solution in Visual Studio 2022 or later

3. Build the solution:
   ```bash
   dotnet build
   ```

4. The executable will be available in the output directory

## Configuration

Before running the application, you need to configure the default paths in `CreateShortcuts\Interfaces\Defaults.cs`:

```csharp
public const string RootFolderForShortcutFiles = @"D:\Videos\Links";
public const string SeasonFolderPattern = "Season *";
public const string StaffelFolderPattern = "Staffel *";
public const string SeriesNamePattern = "*.*";

public static IEnumerable<string> VideoFileFolders
{
    get
    {
        yield return @"N:\Drive1\TVShows\";
        yield return @"N:\Drive2\TVShows\";
        yield return @"N:\Drive3\TVShows\";
        yield return @"N:\Drive4\TVShows\";
    }
}
```

### Configuration Parameters

- **RootFolderForShortcutFiles**: The destination folder where shortcuts will be created
- **SeasonFolderPattern**: Pattern to match season folders (English naming)
- **StaffelFolderPattern**: Pattern to match season folders (German naming)
- **SeriesNamePattern**: Pattern to match series folders
- **VideoFileFolders**: List of source folders containing your TV show files

## Usage

### Basic Usage

Run the executable without arguments to process all configured video folders:

```bash
CreateShortcuts.exe
```

### Command-Line Arguments

The application supports the following command-line arguments:

- `/AppendArticles[=false]` - Control whether to append articles to series names (default: true)
- `/LogFile={FileName}` - Specify a log file to write output
- `/DualLog[=false]` - Write output to both console and log file (requires /LogFile)
- `/Help` or `/?` - Display help information

### Examples

Display help:
```bash
CreateShortcuts.exe /Help
```

Run with logging:
```bash
CreateShortcuts.exe /LogFile=output.log
```

Run with both console and file logging:
```bash
CreateShortcuts.exe /LogFile=output.log /DualLog=true
```

Disable article appending:
```bash
CreateShortcuts.exe /AppendArticles=false
```

## How It Works

1. **Initialization**: The program loads configuration and processes command-line arguments
2. **Folder Discovery**: Scans all configured video folders for TV show series
3. **Shortcut Creation**: For each series with season folders:
   - Creates a series folder in the shortcuts location
   - Creates a shortcut for each season folder
   - Handles series name articles intelligently
4. **Shortcut Copying**: Copies created shortcuts back to the original video folder locations
5. **Cleanup**: Removes any pre-existing shortcuts before creating new ones

## Project Structure

The solution consists of four projects:

- **CreateShortcuts.Program**: Main console application entry point
- **CreateShortcuts.Implementation**: Core business logic and processors
- **CreateShortcuts.Interfaces**: Interfaces and default configurations
- **CreateShortcuts.Tests**: Unit tests

### Key Components

- **ShortcutFolderProcessor**: Creates shortcuts for season folders
- **VideoFolderProcessor**: Copies shortcuts to video folder locations
- **ArgumentsProcessor**: Handles command-line argument parsing
- **ArticleProcessor**: Processes series names with articles
- **ShortcutCreator**: Creates Windows LNK shortcut files

## Architecture

The application follows a modular architecture with clear separation of concerns:

- Dependency injection via ObjectStorage pattern
- Interface-based design for testability
- Abstraction layer for I/O operations
- Processor pattern for discrete operations

## Example Folder Structure

**Before:**
```
N:\Drive1\TVShows\
  └── Breaking Bad
      ├── Season 1\
      ├── Season 2\
      └── Season 3\
N:\Drive2\TVShows\
  └── The Office
      ├── Season 1\
      └── Season 2\
```

**After (Shortcuts at D:\Videos\Links):**
```
D:\Videos\Links\
  ├── Breaking Bad\
  │   ├── Season 1.lnk → N:\Drive1\TVShows\Breaking Bad\Season 1\
  │   ├── Season 2.lnk → N:\Drive1\TVShows\Breaking Bad\Season 2\
  │   └── Season 3.lnk → N:\Drive1\TVShows\Breaking Bad\Season 3\
  └── Office, The\
      ├── Season 1.lnk → N:\Drive2\TVShows\The Office\Season 1\
      └── Season 2.lnk → N:\Drive2\TVShows\The Office\Season 2\
```

## Building from Source

### Prerequisites

- Visual Studio 2022 or later (Professional/Enterprise/Community)
- .NET Framework 4.8.1 SDK

### Build Steps

1. Open `CreateShortcuts.slnx` in Visual Studio
2. Restore NuGet packages (if any)
3. Build the solution (Ctrl+Shift+B)
4. The executable will be in `CreateShortcuts\Program\bin\Debug\net481\CreateShortcuts.exe`

Alternatively, use the .NET CLI:
```bash
dotnet build CreateShortcuts.slnx
```

## Testing

The solution includes a test project. Run tests using:

Visual Studio Test Explorer or:
```bash
dotnet test
```

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is open source. Please check the repository for license details.

## Author

**DJ Doena**
- GitHub: [@DJDoena](https://github.com/DJDoena)

## Version

The application uses automatic versioning based on build date/time (yyyy.MM.dd.HHmm format).

## Troubleshooting

### Common Issues

**Shortcuts not being created:**
- Verify that the source folders exist and contain TV show folders
- Check that the destination folder is writable
- Ensure the folder structure matches the expected patterns (Season X / Staffel X)

**Permission errors:**
- Run the application with appropriate permissions
- Verify write access to the destination folder
- Check network drive access if using UNC paths

**Invalid arguments:**
- Use `/Help` to see valid command-line options
- Ensure boolean arguments use proper format: `/ArgName=true` or `/ArgName=false`

## Acknowledgments

Special thanks to the .NET community and all contributors.

---

**Note**: Remember to customize the `Defaults.cs` file with your actual folder paths before running the application.
