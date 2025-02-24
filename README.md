# Password Hook

Password Hook is a simple console application that evaluates the strength of a password based on a predefined word list. The application reads a word list from a file and calculates a score for a given password. If the score meets a certain threshold, the password is considered strong.

Password Hook is written to be used with [passwdhk.dll](https://sourceforge.net/projects/passwdhk/), to make it harder for the user to use easily guessable passwords.

## Disclaimer

I have little to no experience with C# and .NET and most of this code is generated by ChatGPT. 

## Features

- Reads word lists from `.txt` or `.csv` files.
- Calculates password strength based on the presence of words from the word list and repeated characters.
- Outputs the score of the password.
- Exits with a status code indicating whether the password is strong enough.

## Usage

```sh
PasswordHook.exe <path to wordList> <username> <password>
```

Example
```sh
PasswordHook.exe wordlist.txt myusername mypassword
```

## Word list format
The word list file should be a `.txt` or `.csv` file with each line containing a word and its associated value, separated by a comma. For example:
```markdown
password,1
123456,1
qwerty,1
```

## Build and run
**Prerequsites**
- .NET 9.0 SDK

**Building the project**
To run the project, use the following command
```sh
dotnet run --project PasswordHook <path to wordList> <username> <password>
```

## Project Structure
```markdown
PasswordHook/
├── bin/
├── obj/
├── PasswordHook.csproj
├── Program.cs
├── utilities/
│   └── Utility.cs
├── WordList.cs
├── README.md
├── wordlist.csv
```

## Contributing
Contributions are welcome! Please open an issue or submit a pull request.

## TODO
- Tests
- Configuration
- ...

## License
This project is licensed under the MIT License.