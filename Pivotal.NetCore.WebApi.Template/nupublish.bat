echo Start Time: %time%
@SET INFO=@echo

%INFO% **************************************************************
%INFO% * SCRIPT: nugetpublish.bat *
%INFO% * CREATED: 03/25/2019
%INFO% * AUTHOR: Pivotal
%INFO% **************************************************************

IF "%1"=="" @GOTO NO_ARGUMENT_PROVIDED
%INFO% Argument is the version of the nuget package

@echo publishing to nuget 

nuget pack Pivotal.NetCore.WebApi.Template.nuspec -Version 1.0.%1 -OutputDirectory _publish

IF %ERRORLEVEL% NEQ 0 GOTO ERROR

nuget add _publish/Pivotal.NetCore.WebApi.Template.1.0.%1.nupkg -Source C:\MyLocalNugetRepo

IF %ERRORLEVEL% NEQ 0 GOTO ERROR
IF %ERRORLEVEL% EQU 0 GOTO SUCCESS

:NO_ARGUMENT_PROVIDED
@echo \\\\\\\WARNING/////////
@echo "***No  argument (version) provided***"
cd %~dp0

:ERROR
@echo \\\\\\\WARNING/////////
@echo "***Error occurred while running the command on %computername%***"
cd %~dp0
@GOTO END

:END
@echo End time: %time%
@echo "=============================================================="
exit /B 9

:SUCCESS
cd %~dp0
@echo ***************************************************************
@echo ***************** Process Completed **************************
@echo ***************************************************************
exit /B 9
