@echo off
echo Starting Vue dev server...
echo.
echo Vue at http://localhost:5173
echo API proxied to http://localhost:5188
echo.
start chrome http://localhost:5173
npm run dev