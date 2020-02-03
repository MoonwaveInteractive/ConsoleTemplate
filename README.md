# ConsoleTemplate
An advanced template for getting a complex and robust console application targetting .NET Core up and running as fast as possible.

This template includes some built in commands to get you started and begin learning right away:
* ECHO [message] - Prints the rest of the input after the command without tokenization.
* HELP [command] - Displays a list of available commands and their descriptions.
* CLS - Clears the screen.
* EXIT [exitCode] - Exits the application.

You can also display help information on commands by using:
```
[command] /?
```

This template makes light usage of Moonwave.Text for displaying command help information. Moonwave.Text has been integrated into the project instead of being a dependancy in order to increase portability. It is recommended that you implement your own text formatter as Moonwave.Text is currently incapable of text wrapping.
