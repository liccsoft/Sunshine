FOR %%i IN (%~dp0\*.sql) do echo sqlcmd -i %%i & sqlcmd -i %%i -V DBName=%1

call %~dp0removedrecord\install.bat %1