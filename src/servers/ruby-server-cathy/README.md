Hello! This is my implementation of a Ruby Server.


## Install Ruby

### Mac

#### 1.install Homebrew(if you haven't already), a package manager for macOS that makes it easy to install software

Grab a coffee , it will take some time

```
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```

#### 2.Install Ruby using Homebrew
```
brew install ruby
```

#### 3.Verify the installation
```
ruby -v
```


### Windows

#### Install Ruby using RubyInstaller:
- Download the RubyInstaller from RubyInstaller for Windows.
- Run the installer and follow the setup instructions. 
- Make sure to check the option to add Ruby to your PATH.

#### Verify the installation
```
ruby -v
```

### Additional Steps for Both macOS and Windows

#### Install Bundler
Bundler is a gem that helps manage your project’s dependencies.Just like npm in JavaScript.

```
gem install bundler
```
#### Verify Bundler installation
```
bundler -v
```

## Install the dependencies

Don't forget to navigate to the right path!
```
cd ruby-server-cathy
bundle install
```


## Run the server
```
ruby server.rb
```

Note: we will be using port 5003 and this is also indicated in the frontend.

## Run the tests

Make sure you have installed all the gems using 
```
 bundle install
```

when you are ready, run

```
ruby test_server.rb
```

## Troubleshooting

If you see "Sinatra could not start, the "rackup" gem was not found!",
Do not panic. Do this:

Add it to your bundle with:
```
    bundle add rackup
```
or install it with:
```
    gem install rackup
```

If you see other errors, follow the instructions :D