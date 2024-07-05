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
	
	
	
SetOutPath "$INSTDIR\" 
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\DotNetChecked\Publish\DotNetChecker.application"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\DotNetChecked\Publish\setup.exe"
SetOutPath "$INSTDIR\Application Files\DotNetChecker_1_0_0_2"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\DotNetChecked\Publish\Application Files\DotNetChecker_1_0_0_2\DotNetChecker.application"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\DotNetChecked\Publish\Application Files\DotNetChecker_1_0_0_2\DotNetChecker.exe.deploy"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\DotNetChecked\Publish\Application Files\DotNetChecker_1_0_0_2\DotNetChecker.exe.manifest"
SetOutPath "$INSTDIR\Application Files\DotNetChecker_1_0_0_8"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\DotNetChecked\Publish\Application Files\DotNetChecker_1_0_0_8\DotNetChecker.exe.deploy"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\DotNetChecked\Publish\Application Files\DotNetChecker_1_0_0_8\DotNetChecker.exe.manifest"

	
SetOutPath "$INSTDIR\" 
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\GroupCurves.exe"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\MathNet.Iridium.dll"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\MathNet.Iridium.xml"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\ZedGraph.dll"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\ZedGraph.xml"
SetOutPath "$INSTDIR\de"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\de\MathNet.Iridium.resources.dll"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\de\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\es"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\es\MathNet.Iridium.resources.dll"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\es\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\fr"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\fr\MathNet.Iridium.resources.dll"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\fr\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\hu"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\hu\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\it"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\it\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\ja"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\ja\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\pt"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\pt\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\ru"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\ru\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\sk"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\sk\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\sv"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\sv\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\tr"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\tr\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\zh-cn"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\zh-cn\ZedGraph.resources.dll"
SetOutPath "$INSTDIR\zh-tw"
 File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\..\bin\Release\zh-tw\ZedGraph.resources.dll"

	
	SetOutPath "$INSTDIR\" 
	File ".\Quickstart.htm"
SetOutPath "$INSTDIR\QuickStart_files\" 
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\filelist.xml"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image001.png"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image002.jpg"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image003.png"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image004.jpg"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image005.png"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image006.jpg"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image007.png"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image008.jpg"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image009.png"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image010.jpg"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image011.png"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image012.jpg"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image013.png"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image014.jpg"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image015.png"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\image016.jpg"
File "C:\Users\basch\Desktop\Old Desktop\CategorizeCurves\GroupCurves\Installers\.\QuickStart_files\Thumbs.db"

	
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
	
Delete "$INSTDIR\DotNetChecker.application"
Delete "$INSTDIR\setup.exe"
Delete "$INSTDIR\Application Files\DotNetChecker_1_0_0_2\DotNetChecker.application"
Delete "$INSTDIR\Application Files\DotNetChecker_1_0_0_2\DotNetChecker.exe.deploy"
Delete "$INSTDIR\Application Files\DotNetChecker_1_0_0_2\DotNetChecker.exe.manifest"
Delete "$INSTDIR\Application Files\DotNetChecker_1_0_0_8\DotNetChecker.exe.deploy"
Delete "$INSTDIR\Application Files\DotNetChecker_1_0_0_8\DotNetChecker.exe.manifest"
Delete "$INSTDIR\GroupCurves.exe"
Delete "$INSTDIR\MathNet.Iridium.dll"
Delete "$INSTDIR\MathNet.Iridium.xml"
Delete "$INSTDIR\ZedGraph.dll"
Delete "$INSTDIR\ZedGraph.xml"
Delete "$INSTDIR\de\MathNet.Iridium.resources.dll"
Delete "$INSTDIR\de\ZedGraph.resources.dll"
Delete "$INSTDIR\es\MathNet.Iridium.resources.dll"
Delete "$INSTDIR\es\ZedGraph.resources.dll"
Delete "$INSTDIR\fr\MathNet.Iridium.resources.dll"
Delete "$INSTDIR\fr\ZedGraph.resources.dll"
Delete "$INSTDIR\hu\ZedGraph.resources.dll"
Delete "$INSTDIR\it\ZedGraph.resources.dll"
Delete "$INSTDIR\ja\ZedGraph.resources.dll"
Delete "$INSTDIR\pt\ZedGraph.resources.dll"
Delete "$INSTDIR\ru\ZedGraph.resources.dll"
Delete "$INSTDIR\sk\ZedGraph.resources.dll"
Delete "$INSTDIR\sv\ZedGraph.resources.dll"
Delete "$INSTDIR\tr\ZedGraph.resources.dll"
Delete "$INSTDIR\zh-cn\ZedGraph.resources.dll"
Delete "$INSTDIR\zh-tw\ZedGraph.resources.dll"
Delete "$INSTDIR\QuickStart_files\filelist.xml"
Delete "$INSTDIR\QuickStart_files\image001.png"
Delete "$INSTDIR\QuickStart_files\image002.jpg"
Delete "$INSTDIR\QuickStart_files\image003.png"
Delete "$INSTDIR\QuickStart_files\image004.jpg"
Delete "$INSTDIR\QuickStart_files\image005.png"
Delete "$INSTDIR\QuickStart_files\image006.jpg"
Delete "$INSTDIR\QuickStart_files\image007.png"
Delete "$INSTDIR\QuickStart_files\image008.jpg"
Delete "$INSTDIR\QuickStart_files\image009.png"
Delete "$INSTDIR\QuickStart_files\image010.jpg"
Delete "$INSTDIR\QuickStart_files\image011.png"
Delete "$INSTDIR\QuickStart_files\image012.jpg"
Delete "$INSTDIR\QuickStart_files\image013.png"
Delete "$INSTDIR\QuickStart_files\image014.jpg"
Delete "$INSTDIR\QuickStart_files\image015.png"
Delete "$INSTDIR\QuickStart_files\image016.jpg"
Delete "$INSTDIR\QuickStart_files\Thumbs.db"


	
RMDir "$INSTDIR\"
RMDir "$INSTDIR\Application Files\DotNetChecker_1_0_0_2\"
RMDir "$INSTDIR\Application Files\DotNetChecker_1_0_0_8\"
RMDir "$INSTDIR\de\"
RMDir "$INSTDIR\es\"
RMDir "$INSTDIR\fr\"
RMDir "$INSTDIR\hu\"
RMDir "$INSTDIR\it\"
RMDir "$INSTDIR\ja\"
RMDir "$INSTDIR\pt\"
RMDir "$INSTDIR\ru\"
RMDir "$INSTDIR\sk\"
RMDir "$INSTDIR\sv\"
RMDir "$INSTDIR\tr\"
RMDir "$INSTDIR\zh-cn\"
RMDir "$INSTDIR\zh-tw\"
RMDir "$INSTDIR\QuickStart_files\"

  
	empty3:
	; Remove remaining directories

	RMDir "$SMPROGRAMS\AFM Curve Grouper"
	
	RMDir "$INSTDIR\"

SectionEnd

; eof
