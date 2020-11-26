;JEAN-YVES DENONCOURT 9977949

;--------------------------------
;Include Modern UI

!include "MUI2.nsh"

;--------------------------------
;General

;Name and file
!define NAME "WeatherApp" 
Name "WeatherApp" 
OutFile "WeatherAppInstall.exe"
!define MUI_ICON "weatherApp.ico"   ;"D:\Informatique\DEC shawi\Session_5\Dev-Speciaux\Laboratoires\TP4b-WeatherStation\weatherApp.ico"
Unicode True

;Default installation folder
InstallDir "$Profile\9977949"   ;"$Profile\9977949"

;Get installation folder from registry if available
InstallDirRegKey HKCU "Software\WeatherApp" ""

;Request application privileges for Windows Vista
RequestExecutionLevel user

;--------------------------------
;Interface Settings

!define MUI_ABORTWARNING

;--------------------------------
;Pages

!insertmacro MUI_PAGE_LICENSE "License.txt"  ;"D:\Informatique\DEC shawi\Session_5\Dev-Speciaux\Laboratoires\TP4b-WeatherStation\License.txt"
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
  
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
  
;--------------------------------
;Languages
 
!insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "WeatherApp" SecWeatherApp

SetOutPath "$INSTDIR"

File /r "installFile\*.*"  ;"D:\Informatique\DEC shawi\Session_5\Dev-Speciaux\Laboratoires\TP4b-WeatherStation\installFile\*.*"

WriteRegStr HKCU "Software\WeatherApp" "" $INSTDIR

WriteUninstaller "$INSTDIR\Uninstall.exe"

CreateDirectory $SMPROGRAMS\9977949
CreateShortcut $SMPROGRAMS\9977949\WeatherApp.lnk $INSTDIR\${NAME}.exe "" "\weatherApp.ico" 0
CreateShortcut $SMPROGRAMS\9977949\WeatherApp-uninstall.lnk $INSTDIR\Uninstall.exe
CreateShortcut "$desktop\WeatherApp(9977949).lnk" "$INSTDIR\WeatherApp.exe" "" "$INSTDIR\weatherApp.ico" 0


SectionEnd

;--------------------------------
;Descriptions

;Aucune description de sous section

;--------------------------------
;Uninstaller Section

Section "Uninstall"

;ADD YOUR OWN FILES HERE...

Delete "$INSTDIR\Uninstall.exe"

RMDir /r "$INSTDIR"

Delete "$desktop\WeatherApp(9977949).lnk"

DeleteRegKey /ifempty HKCU "Software\WeatherApp"

RMDir /r "$SMPROGRAMS\9977949"

SectionEnd