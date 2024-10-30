Hi, this is my implementation of the backend in C#.

## Install dotnet

### Mac

#### 1.install Homebrew(if you haven't already), a package manager for macOS that makes it easy to install software

Grab a coffee , it will take some time

```
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```

#### 2.Install .NET SDK
```
brew install --cask dotnet-sdk
```

#### 3.Verify the installation
```
dotnet --version
```


### Windows

#### Install .NET:
- Download the Installer: Go to the .NET download page and download the latest version of the .NET SDK for Windows.
- Run the Installer
    - Open the downloaded installer file.
    - Follow the on-screen instructions to complete the installation.

#### Verify the installation
```
dotnet --version
```



## Install the dependencies

Don't forget to navigate to the right path!
```
cd c-sharp-server-cathy
dotnet restore
```

## Build the project
```
dotnet build
```

## Run the server
```
dotnet run
```

Note: we will be using port 5004 and this is also indicated in the frontend.


## Run the frontend

open a new terminal, navigate to the root of this whole repo
```
cd cathy-rest-tester-5500
npm install
npm run dev
```

## Troubleshooting
If you see other errors, follow the instructions :D