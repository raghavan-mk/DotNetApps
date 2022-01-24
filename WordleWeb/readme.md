
# Wordle Automation

FWIW, I too got nerd swiped by one and all who were automating [Wordle](https://www.powerlanguage.co.uk/wordle/).

And here is a version of mine.

## Table of contents
[Install](#install)\
[Running](#running)\
[Usage](#usage)\
[Known Issues](#known-issues)\
[Screenshot](#screenshot)\
[Algorithm](#algorithm)

## Install

Installation is just about downloading the binaries and running them. 

Download binaries for [Windows](https://github.com/raghavan-mk/DotNetApps/releases/download/v1.0.0.0/win-x64.zip) \
Download binaries for [OSX](https://github.com/raghavan-mk/DotNetApps/releases/download/v1.0.0.0/osx-x64.zip)

Note the above binaries are self contained, so you don't need to install anything else.

If you already have .NET6 installed, then you can download a much smaller sized [binary](https://github.com/raghavan-mk/DotNetApps/releases/download/v1.0.0.0/WordleWeb.zip) \
This runs both on Windows as well as OSX

## Running

If you have downloaded platform specific binaries, then you can run them as

#### Windows

1. Unzip the binaries
2. Go to the folder - win-x64/win-x64
3. Run the executable - WordleWeb.exe

#### OSX

1. Unzip the binaries
2. Go to the folder - osx-x64
3. Run the executable - ./WordleWeb

#### Portable

1. Unzip the binaries
2. Go to the folder - WordleWeb
3. Run the executable - 
   **dotnet WordleWeb.dll** 

**Note**: When the application is run for the first time, it will download chrome driver

## Usage

By default the start word it uses is **stare** To override this we can do, as shown below.
First options is for portable, second is for OSX and third is for Windows

**dotnet wordleweb.dll -w=split** or\
**./wordleweb -w=split** or\
**wordleweb.exe -w=split**\

Now the program starts the guesses with split as the first word

Also by default the program runs in headless mode. This can be overridden by setting 

**dotnet wordleweb.dll -h=false** or\
**./wordleweb -h=false** or\
**wordleweb.exe -h=false**\

Both the options can be overridden in single command

**dotnet wordleweb.dll -h=false -w=split**\
**./wordleweb -h=false -w=split**\
**wordleweb.exe -h=false -w=split**\

## Known Issues

1. We may see application crash with some kind of crazy stack overflow errors :(
2. Rerun the application and it will be fine
3. But ensure the Chrome process is killed from task manager/activity monitor
4. Terrible coding :D 
5. Will fix them all sometime later :) 

## Screenshot

Application when run fine looks like this

![img](https://github.com/raghavan-mk/DotNetApps/blob/main/WordleWeb/Assets/img.png?raw=true)

## Algorithm

Well, brute forced it :D

1. Pick a random word from corpus. This is same as Josh Wardle uses i guess. Default start word is **stare**
2. Enter the word in the wordle website
3. Read the color coding of the word
4. We get to know what letters are 
   1. present - letter is present in the word but at wrong position
   2. absent -  letter is absent from the word
   3. correct - letter is present in the word and at correct position
5. Using these hints apply it for the next guessed word
6. And so on until we hit the maximum number of guesses, which is 6
7. If application is not able to guess within 6 guesses, then it will show the solution

