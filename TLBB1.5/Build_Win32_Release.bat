echo off

set UNITY_PATH="D:\Unity\Editor\Unity.exe"
set PROJECT_PATH=E:\TLCard\Client
set RELEASE_PATH=E:\TLCard\Release

set RELEASE_NAME_PREFIX=tlcard

set WIN_PATH="%RELEASE_PATH%\win32\%RELEASE_NAME_PREFIX%.exe"
set WIN_LOG_PATH="%RELEASE_PATH%\win32\BuildLog.txt"

rem not use yet
rem set EXECUTEMETHOD=-executeMethod
rem set FUNCTION="CommandBuild.BuildWindows"
echo do clean
del /q %RELEASE_PATH%\win32\*.*
rmdir /s /q %RELEASE_PATH%\win32\%RELEASE_NAME_PREFIX%_Data

REM Win32 build
echo Running Win Build for: %PROJECT_PATH%
echo %UNITY_PATH% -batchmode -quit -projectPath %PROJECT_PATH% -buildWindowsPlayer %WIN_PATH% -logFile %WIN_LOG_PATH%
%UNITY_PATH% -batchmode -quit -projectPath %PROJECT_PATH% -buildWindowsPlayer %WIN_PATH% -logFile %WIN_LOG_PATH%

pause