; Script generated with the Venis Install Wizard
!include "WordFunc.nsh"

!insertmacro WordReplace
!insertmacro WordFind


!define SHCNE_ASSOCCHANGED 0x8000000
!define SHCNF_IDLIST 0

Var STR_HAYSTACK
Var STR_NEEDLE
Var STR_CONTAINS_VAR_1
Var STR_CONTAINS_VAR_2
Var STR_CONTAINS_VAR_3
Var STR_CONTAINS_VAR_4
Var STR_RETURN_VAR
 
Function StrContains
  Exch $STR_NEEDLE
  Exch 1
  Exch $STR_HAYSTACK
  ; Uncomment to debug
  ;MessageBox MB_OK 'STR_NEEDLE = $STR_NEEDLE STR_HAYSTACK = $STR_HAYSTACK '
    StrCpy $STR_RETURN_VAR ""
    StrCpy $STR_CONTAINS_VAR_1 -1
    StrLen $STR_CONTAINS_VAR_2 $STR_NEEDLE
    StrLen $STR_CONTAINS_VAR_4 $STR_HAYSTACK
    loop:
      IntOp $STR_CONTAINS_VAR_1 $STR_CONTAINS_VAR_1 + 1
      StrCpy $STR_CONTAINS_VAR_3 $STR_HAYSTACK $STR_CONTAINS_VAR_2 $STR_CONTAINS_VAR_1
      StrCmp $STR_CONTAINS_VAR_3 $STR_NEEDLE found
      StrCmp $STR_CONTAINS_VAR_1 $STR_CONTAINS_VAR_4 done
      Goto loop
    found:
      StrCpy $STR_RETURN_VAR $STR_NEEDLE
      Goto done
    done:
   Pop $STR_NEEDLE ;Prevent "invalid opcode" errors and keep the
   Exch $STR_RETURN_VAR  
FunctionEnd
 
!macro _StrContainsConstructor OUT NEEDLE HAYSTACK
  Push "${HAYSTACK}"
  Push "${NEEDLE}"
  Call StrContains
  Pop "${OUT}"
!macroend
 
!define StrContains '!insertmacro "_StrContainsConstructor"'

; Define your application name
!define APPNAME "AFM_Curve_Grouper"
!define APPNAMEANDVERSION "AFM Curve Grouper 0.1"

; Main Install settings
Name "${APPNAMEANDVERSION}"
InstallDir "$PROGRAMFILES\AFMCURVEGROUPER"
InstallDirRegKey HKLM "Software\${APPNAME}" ""
OutFile "SetupAFMCurveGrouper.exe"

; Modern interface settings
!include "MUI.nsh"

!define MUI_ABORTWARNING
!define MUI_FINISHPAGE_RUN "$INSTDIR\GroupCurves.exe"
    !define MUI_FINISHPAGE_NOAUTOCLOSE
    ;!define MUI_FINISHPAGE_RUN
    ;!define MUI_FINISHPAGE_RUN_CHECKED
    ;!define MUI_FINISHPAGE_RUN_TEXT "Quick Start"
    ;!define MUI_FINISHPAGE_RUN_FUNCTION "LaunchLink"
    ;!define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED
    !define MUI_FINISHPAGE_SHOWREADME "$INSTDIR\Quickstart.htm"


!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "License.txt"
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_INSTFILES

!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

; Set languages (first is default language)
!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_RESERVEFILE_LANGDLL

Function .onInit
         InitPluginsDir
FunctionEnd
 
Function .onInstSuccess
    ExecShell "" " " ""
FunctionEnd


Function LaunchLink
  
  ExecShell "" "iexplore.exe" "$INSTDIR\QuickStart.htm"
FunctionEnd


Section "AFMCurveGrouper" Section1

	; Set Section properties
	SetOverwrite on

	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\" 
	File ".\Redist\vcredist_x86.exe"
	
	SetOutPath "$INSTDIR\" 
	File ".\Redist\dotnetfx35setup.exe"
	
	
	
	<%InsertFiles "$INSTDIR\" "DotNetChecked\Publish\"%>
	
	<%InsertFiles "$INSTDIR\" "..\bin\Release\"%>
	
	SetOutPath "$INSTDIR\" 
	File ".\Quickstart.htm"
	<%InsertFiles "$INSTDIR\QuickStart_files\" ".\QuickStart_files\"%>
	
  ExecWait '"$INSTDIR\vcredist_x86.exe" /q:a'
	ExecWait '"$INSTDIR\setup.exe" /q:a'
	
	CreateShortCut "$DESKTOPAFM Curve Grouper\AFM Curve Grouper.lnk" "$INSTDIR\GroupCurves.exe"
	
	CreateDirectory "$SMPROGRAMS\AFM Curve Grouper\"
	CreateShortCut "$SMPROGRAMS\AFM Curve Grouper\AFM Curve Grouper.lnk" "$INSTDIR\GroupCurves.exe"
	CreateShortCut "$SMPROGRAMS\AFM Curve Grouper\AFM Curve Grouper Quick Start.lnk" "$INSTDIR\QuickStart.htm"
	
	CreateShortCut "$SMPROGRAMS\AFM Curve Grouper\Uninstall.lnk" "$INSTDIR\uninstall.exe"
	
SectionEnd


Section -FinishSection

  
	WriteRegStr HKLM "Software\${APPNAME}" "" "$INSTDIR"
	WriteRegStr HKLM "Software\${APPNAME}" "Path" "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "DisplayName" "${APPNAME}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "UninstallString" "$INSTDIR\uninstall.exe"
	WriteUninstaller "$INSTDIR\uninstall.exe"
  
SectionEnd

; Modern install component descriptions
!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
	!insertmacro MUI_DESCRIPTION_TEXT ${Section1} ""
	!insertmacro MUI_DESCRIPTION_TEXT ${Section2} ""
	!insertmacro MUI_DESCRIPTION_TEXT ${Section3} ""
!insertmacro MUI_FUNCTION_DESCRIPTION_END

;Uninstall section
Section Uninstall

	;Remove from registry...
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}"
	DeleteRegKey HKLM "SOFTWARE\${APPNAME}"

	; Delete self
	Delete "$INSTDIR\uninstall.exe"

	; Delete Shortcuts
	Delete "$DESKTOP\AFM Curve Grouper.lnk"
	Delete "$SMPROGRAMS\AFM Curve Grouper\AFM Curve Grouper.lnk"
	Delete "$SMPROGRAMS\AFM Curve Grouper\Uninstall.lnk"
	
	
	; Clean up Micromanager.NET
	
	<%DeleteAllFiles %>
	<%DeleteAllShortcuts %>
	
	<% DeleteAllDirectories %>
  
	empty3:
	; Remove remaining directories

	RMDir "$SMPROGRAMS\AFM Curve Grouper"
	
	RMDir "$INSTDIR\"

SectionEnd

; eof