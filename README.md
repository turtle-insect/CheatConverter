# CheatConverter
Switch Cheat File Converter

# Run Spec
* Windows 10
* [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

# Build Spec
* Windows 10
* [Visual Studio 2022](https://visualstudio.microsoft.com/ja/vs/)

# usage
input   
C:\0123456789ABCDEF.txt
```
[hoge]
04000000 03000000 10000000

[huga]
04000000 04000000 20000000
```

output   
C:\output

↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

C:\output\hoge\cheats\0123456789ABCDEF.txt
```
04000000 03000000 10000000
```

C:\output\huga\cheats\0123456789ABCDEF.txt
```
04000000 04000000 20000000
```
