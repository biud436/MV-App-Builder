; Script generated by the HM NIS Edit Script Wizard.
Unicode true

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "MV App Builder"
!define PRODUCT_VERSION "0.1.32"
!define PRODUCT_PUBLISHER "biud436"
!define PRODUCT_WEB_SITE "https://github.com/biud436/MV-App-Builder/"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\Cordova Builder.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; MUI 1.67 compatible ------
!include "MUI.nsh"
!include "LogicLib.nsh"
!include "nsProcess.nsh"

!define	EUL_RUL				"를"					; 을/를 문제 해결을 위한 define. $PRODUCT 에 따라 바뀐다.
!define I_KA				"이"					; 이/가 문제 해결을 위한 define. $PRODUCT 에 따라 바뀐다.
!define KOREAN_RUL								; mui 외의 부분에서 을/를 문제가 발생하는것을 해결하기 위한부분- NSIS2.0 패치가 적용되어야 작동한다.

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Language Selection Dialog Settings
!define MUI_LANGDLL_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_LANGDLL_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_LANGDLL_REGISTRY_VALUENAME "NSIS:Language"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "LICENSE"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\Cordova Builder.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_LANGUAGE "Japanese"
!insertmacro MUI_LANGUAGE "Korean"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "MVAppBuilder.exe"
InstallDir "$PROGRAMFILES\MV App Builder"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

!define MY_APP_NAME "Cordova Builder.exe"

# 다국어 설정
LangString FORCE_ASK_EXIT_MESSAGE ${LANG_ENGLISH} "You must shut down the RPG Maker MV Cordova Builder before proceeding with the installation. $\nAre you sure you want to exit the program?"
LangString FORCE_ASK_EXIT_MESSAGE ${LANG_JAPANESE} "You must shut down the RPG Maker MV Cordova Builder before proceeding with the installation. $\nAre you sure you want to exit the program?"
LangString FORCE_ASK_EXIT_MESSAGE ${LANG_KOREAN} "설치를 진행하려면 RPG Maker MV Cordova Builder를 먼저 종료하여야 합니다. $\n프로그램을 강제로 종료할까요?"

!macro IsRunning 

  ${nsProcess::FindProcess} "Cordova Builder.exe" $R0
  ${If} $R0 == 0
    MessageBox MB_YESNO|MB_ICONINFORMATION|MB_DEFBUTTON2 "$(FORCE_ASK_EXIT_MESSAGE)" IDYES true IDNO false
    true:
      ${nsProcess::KillProcess} "Cordova Builder.exe" $R0
    goto notRunning
  ${Else}
    goto notRunning
  ${EndIf}
  
  false: 
    Abort

  notRunning:

!macroEnd

Function .onInit
  !insertmacro IsRunning
  !insertmacro MUI_LANGDLL_DISPLAY
FunctionEnd

Section "필수설치" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite try
  File ".\bin\Release\Cordova Builder.exe"
  CreateDirectory "$SMPROGRAMS\MV App Builder"
  # 내 문서에 폴더 생성
  CreateDirectory "$DOCUMENTS\RPG Maker MV Cordova Builder"
  CreateShortCut "$SMPROGRAMS\MV App Builder\MV App Builder.lnk" "$INSTDIR\Cordova Builder.exe"
  CreateShortCut "$DESKTOP\MV App Builder.lnk" "$INSTDIR\Cordova Builder.exe"
  File ".\bin\Release\Cordova Builder.exe.config"
  File ".\bin\Release\Cordova Builder.vshost.exe.config"
  SetOutPath "$INSTDIR\en"
  File ".\bin\Release\\en\Cordova Builder.resources.dll"
  SetOutPath "$INSTDIR\ja"
  File ".\bin\Release\\ja\Cordova Builder.resources.dll"
  SetOutPath "$INSTDIR\ko"
  File ".\bin\Release\\ko\Cordova Builder.resources.dll"
  SetOutPath "$INSTDIR"
  File ".\bin\Release\\Newtonsoft.Json.dll"
  File ".\bin\Release\\Newtonsoft.Json.xml"
SectionEnd

Section -AdditionalIcons
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\MV App Builder\Website.lnk" "$INSTDIR\${PRODUCT_NAME}.url"
  CreateShortCut "$SMPROGRAMS\MV App Builder\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\Cordova Builder.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\Cordova Builder.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name)는(은) 완전히 제거되었습니다."
FunctionEnd

Function un.onInit
!insertmacro MUI_UNGETLANGUAGE
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "$(^Name)을(를) 제거하시겠습니까?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\${PRODUCT_NAME}.url"
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\Newtonsoft.Json.xml"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$INSTDIR\ko\Cordova Builder.resources.dll"
  Delete "$INSTDIR\ja\Cordova Builder.resources.dll"
  Delete "$INSTDIR\en\Cordova Builder.resources.dll"
  Delete "$INSTDIR\Cordova Builder.vshost.exe.config"
  Delete "$INSTDIR\Cordova Builder.exe.config"
  Delete "$INSTDIR\Cordova Builder.exe"

  Delete "$SMPROGRAMS\MV App Builder\Uninstall.lnk"
  Delete "$SMPROGRAMS\MV App Builder\Website.lnk"
  Delete "$DESKTOP\MV App Builder.lnk"
  Delete "$SMPROGRAMS\MV App Builder\MV App Builder.lnk"

  RMDir "$SMPROGRAMS\MV App Builder"
  RMDir "$INSTDIR\ko"
  RMDir "$INSTDIR\ja"
  RMDir "$INSTDIR\en"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd