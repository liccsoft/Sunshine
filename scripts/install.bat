
if "%1"=="" GOTO END

FOR %%i IN (%~dp0\*.sql) do echo sqlcmd -i %%i & sqlcmd -i %%i -V DBName=%1

call %~dp0schema\install.bat %1
GOTO OK

:END
echo usage: install.bat [dbname]

:OK
