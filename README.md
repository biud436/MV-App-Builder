# RPG Maker MV App Builder

![Build Status](https://github.com/biud436/MV-App-Builder/actions/workflows/dotnet.yml/badge.svg)
[![Release](https://img.shields.io/github/v/release/biud436/MV-App-Builder)](https://github.com/biud436/MV-App-Builder/releases)
[![License](https://img.shields.io/github/license/biud436/MV-App-Builder)](LICENSE)

A comprehensive tool that allows you to build Android APK files from RPG Maker MV projects easily.

## ✨ Features

- **Keystore Generation**: Built-in keystore generation for Android app signing
- **Self-Build System**: Complete build automation from project to APK
- **Automatic Updates**: Self-updating system to keep the tool current
- **Cordova Plugin Support**: Easy integration of Cordova plugins
- **Resource Optimization**: Removes unused resources to reduce APK size
- **Multi-language Support**: Korean, Japanese, and English interfaces
- **Dark Theme**: Modern UI with dark theme support
- **Version Management**: Integrated version checking and management

## 📸 Screenshots

<img width="722" alt="Main Interface" src="https://github.com/biud436/MV-App-Builder/assets/13586185/66fc905f-7cac-4794-a9e2-a56b6f18d892">

## 🔧 Requirements

This tool requires several third-party programs to be installed manually. Below is the complete list:

### System Requirements

- **Operating System**: Windows 10 or higher (Windows 11 recommended)
- **RAM**: 4GB minimum (8GB recommended)
- **Storage**: At least 10GB free space for Android SDK

### Required Software

#### 1. .NET Framework v4.8 or Higher

The application is built with C# WinForms and requires .NET Framework 4.8 or higher.

📥 Download: [https://dotnet.microsoft.com/download/dotnet-framework](https://dotnet.microsoft.com/download/dotnet-framework)

#### 2. Node.js (Latest LTS Version)

Required for Cordova and npm package management.

📥 Download: [https://nodejs.org/](https://nodejs.org/)

**Installation Note**: After installing Node.js, you can install Cordova using npm:
```bash
npm install -g cordova@13.0.0
```

#### 3. Cordova v13.0.0 or Higher

The tool uses Cordova CLI for building Android applications.

📥 Installation:
```bash
npm install -g cordova
```

📖 Documentation: [https://cordova.apache.org/docs/en/latest/guide/cli/](https://cordova.apache.org/docs/en/latest/guide/cli/)

#### 4. JDK 17 or Higher

Cordova v13.0.0 requires JDK 17 or higher to build Android applications.

📥 Download: [https://learn.microsoft.com/en-us/java/openjdk/download](https://learn.microsoft.com/en-us/java/openjdk/download)

**Environment Variable Setup**:
- Set `JAVA_HOME` to your JDK installation directory
- Add `%JAVA_HOME%\bin` to your `PATH`

#### 5. Android Studio & Android SDK

Required for building Android applications. You need API Level 35 (latest).

📥 Download: [https://developer.android.com/studio](https://developer.android.com/studio)

**Required Components**:
- Android SDK Platform 35
- Android SDK Build-Tools (latest)
- Android SDK Platform-Tools
- Android SDK Command-line Tools

**Environment Variable Setup**:
```cmd
ANDROID_SDK_ROOT=<Your Android SDK Path>
PATH=%ANDROID_SDK_ROOT%\platform-tools\
PATH=%ANDROID_SDK_ROOT%\cmdline-tools\latest\bin\
```

### Important Notes

⚠️ **Project Path Requirements**: 
- Your project path must not contain non-ASCII characters (e.g., Korean, Japanese, Chinese)
- Due to Gradle limitations, all paths must use English characters only
- The tool automatically creates projects in `%LOCALAPPDATA%\RPG Maker MV Cordova Builder` to avoid this issue

⚠️ **Windows User Account**: 
- Your Windows user account name should be in English
- If your account name contains non-ASCII characters, you may encounter build errors

## 📦 Installation

### Method 1: Download Release (Recommended)

1. Visit the [Releases page](https://github.com/biud436/MV-App-Builder/releases)
2. Download the latest version (v0.8.0 or higher)
3. Extract the ZIP file to your desired location
4. Run `MVAppBuilder.exe`

## 🚀 Usage

### Basic Build Process

1. **Export Your Game**
   - Open RPG Maker MV
   - Export your game project to a folder (e.g., Desktop)
   - Ensure the `www` folder contains `index.html`

2. **Launch the Builder**
   - Run `MVAppBuilder.exe`
   - The tool will automatically check for required programs

3. **Configure Your Project**
   - Click "Select Game Folder" and choose your `<YOUR_GAME_FOLDER>/www` folder
   - Fill in the required fields:
     - **Package Name**: Reverse domain format (e.g., `com.example.mygame`)
     - **Game Name**: Your game's display name
     - **Keystore Information**: For app signing (or use auto-generate)

4. **Configure Build Options**
   - Open the Options dialog
   - Configure:
     - **Unused Resource Removal**: Enable to reduce APK size
     - **Audio Format**: Choose between OGG/M4A
     - **SDK Versions**: Minimum, Target, and Compile SDK versions
     - **Orientation**: Portrait, Landscape, or Auto
     - **Plugins**: Select required Cordova plugins

5. **Start Building**
   - Click the "Build" button
   - Monitor progress in the Log window
   - Wait for "Build completed successfully" message

6. **Install on Device (Optional)**
   - Connect your Android device via USB
   - Enable USB Debugging on your device
   - Run the following command:
   ```bash
   adb install -r app-release.apk
   ```

### Advanced Features

#### Remote Debug (Debug Mode)

If you build in debug mode, you can debug your app remotely:

1. Build your app in debug mode
2. Install the APK on your device
3. Open Chrome browser on your PC
4. Navigate to `chrome://inspect`
5. Your device will appear in the list
6. Click "Inspect" to start debugging

#### Unused Resource Removal

The tool includes a feature to remove unused resources from your game:

1. Open the Options dialog
2. Enable "Remove Unused Resources"
3. The builder will analyze your project and remove:
   - Unused images
   - Unused audio files
   - Unused data files

This significantly reduces the APK size.

## 🔌 Compatible Plugins

### Exit Dialog Plugin

Shows a confirmation dialog when the back button is pressed.

📥 Download: [RS_ExitDialog.js](https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/biud436/MV/blob/master/Android/RS_ExitDialog.js)

**Installation**: Place in `js/plugins/` folder

### Insomnia Plugin

Prevents screen from dimming during gameplay (automatically included).

## 🛠️ Plugin Developer Tips

### Auto-Detection Format

Plugins are automatically detected during project loading. Use the following comment format at the top of your plugin file:

```javascript
/*:
 * @cordova_plugin cordova-plugin-dialogs
 */
```

This will automatically add `cordova-plugin-dialogs` to the build process.

## 🐛 Bug Report

### Reporting Issues

If you encounter errors during the build process:

1. Right-click in the **Log Window**
2. Select **Save Build Log**
3. Visit [Issues Page](https://github.com/biud436/MV-App-Builder/issues)
4. Create a new issue with:
   - Build log file
   - Your system information
   - Steps to reproduce the error

### Successful Build Log Example

```powershell
--- Preparation ---
java version "17.0.1"
Java(TM) SE Runtime Environment (build 17.0.1+12-LTS-39)
Java HotSpot(TM) 64-Bit Server VM (build 17.0.1+12-LTS-39, mixed mode, sharing)
-- Found keytool
13.0.0 (cordova-lib@13.0.0)

Found Android SDK / ANDROID_SDK_ROOT: E:\Android\sdk\
[SDK]==========================================
android-33
android-34
android-35
==============================================
--- All required programs are installed.
Checking for program updates... (less than 1 second)
You are using the latest version.
Checking for latest Cordova version... (less than 5 seconds)
You are using the latest version of Cordova. (00:00:04.7386189)
```

### Common Issues

#### Issue 1: Non-ASCII Characters in Path

**Error Message**:
```
Failed to apply plugin 'com.android.internal.application'.
Your project path contains non-ASCII characters.
```

**Solution**: 
- The tool now automatically uses `%LOCALAPPDATA%\RPG Maker MV Cordova Builder`
- Avoid Korean/Japanese/Chinese characters in file paths
- Change Windows user account name to English if needed

#### Issue 2: SDK Not Found

**Error Message**:
```
Failed to find 'ANDROID_SDK_ROOT' environment variable.
```

**Solution**:
- Install Android Studio
- Set `ANDROID_SDK_ROOT` environment variable
- Add SDK paths to `PATH` variable

## 📥 Download

Download the latest version from the [Releases page](https://github.com/biud436/MV-App-Builder/releases).

The tool includes an automatic update feature that will notify you when new versions are available.

**Latest Version**: v0.8.0  
**Release Date**: February 18, 2026

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 👨‍💻 Author

- **biud436** - [GitHub Profile](https://github.com/biud436)


## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/biud436/MV-App-Builder/issues)
- **Discussions**: [GitHub Discussions](https://github.com/biud436/MV-App-Builder/discussions)
