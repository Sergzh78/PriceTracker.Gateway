@echo off
echo Building Vue...
call npm run build
if errorlevel 1 (
    echo Build failed!
    pause
    exit /b 1
)
echo Copying files...
xcopy dist . /E /Y /I
echo Cleaning up...
rmdir /s /q dist
echo Done!
pause