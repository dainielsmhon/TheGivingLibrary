@echo off
cd /d %~dp0
echo Starting ngrok on port 57418...
.\ngrok.exe http 57418 --host-header=localhost
pause
