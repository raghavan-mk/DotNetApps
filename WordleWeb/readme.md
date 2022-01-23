
## Wordle Automation

FWIW, I too got nerd swiped by one and all who were automating [Wordle](https://www.powerlanguage.co.uk/wordle/).

And here is a version of mine.

### Table of contents
[Install](#install)

#### Install


Run it as 

**dotnet wordleweb.dll**

And we should see output similar to this -

![img.png](https://github.com/raghavan-mk/DotNetApps/blob/main/WordleWeb/Assets/img.png?raw=true)

WordleWeb program tries to mimic user actions by automating the guesses and interacting with the website.

By default the start word it uses is **stare** To override this we can do

**dotnet wordleweb.dll -w=split**

Now the program starts the guesses with split as the first word

Also by default the program runs in headless mode. This can be overridden by setting 

**dotnet wordleweb.dll -h=false**
