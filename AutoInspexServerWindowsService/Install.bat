REM execute this script from the output directory using an elevated prompt

set InstallUtil="C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe"

%InstallUtil% /i "AutoInspexWindowsService.exe"

Net Start "AutoPix WebSocket Server Windows Service"
