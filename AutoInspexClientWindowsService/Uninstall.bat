REM execute this script from the output directory using an elevated prompt

set InstallUtil="C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe"

Net Stop "AutoPix WebSocket Server Windows Service"

%InstallUtil% /u "AutoInspexWindowsService.exe"
