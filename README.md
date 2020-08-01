# Introduction
[![Build status](https://ci.appveyor.com/api/projects/status/w296e5145wwpn5dc/branch/master?svg=true)](https://ci.appveyor.com/project/biud436/mv-app-builder/branch/master)

This tool allows you to build your own Android APK File easily.

# Features

- Includes keystore generation features.
- Includes Self-Build.
- Includes automatic update system.
- Cordoba plugins support.
- Includes unused resource removal.
- Support for Korean, Japanese, and English.

# Screenshots

![IMG](./screenshot.png)

# Usage

1. ```RPG Maker MV```를 켜고, 미사용 리소스 제거를 한 후 게임을 바탕 화면 등으로 배포합니다.

2. 빌더를 켜고, ```index.html``` 파일이 있는 ```<Game 폴더>/www```폴더를 선택합니다.

3. 프로그램 내에 미리 작성된 패키지 명, 게임명, 폴더명, 조직 단위 명 등을 수정합니다.

4. 설정에서 미사용 리소스 제거 기능을 OFF로 변경합니다.

5. Build 버튼을 누르고 빌드 완료 메시지가 뜰 때까지 기다리면 APK 파일 생성이 완료됩니다.

6. 휴대폰이 USB 잭으로 컴퓨터와 연결되어있고, ```adb```가 역시 설치되어 있다면, ```adb install -r app-release.apk``` 등으로 휴대폰에 앱을 자동으로 설치할 수 있습니다.

7. Debug Mode로 Build를 하면, 크롬 확장에 adb를 설치, ```chrome://inspect```의 크롬 개발자 도구에서 원격 디버깅(remote-debugging)이 가능합니다.

# Requirements
This tool is based on C# Winform and Node.js and it has used a many of third-party tools. Some of them must be installed manually due to complexity of implementation. List that you must download is belows.

## 닷넷 프레임워크 4.5.4 버전 이상

A higher version of the .Net Framework is required for a program developed with C# Winform to run.

▶ 다운로드 : https://dotnet.microsoft.com/download/dotnet-framework

## Android Studio & Android SDK

APK 파일을 실제로 만들어내는 프로그램이므로 Android SDK가 필요합니다. 안드로이드 SDK API 수준은 ```19, 28, 29``` 버전이 설치되어야 합니다.

▶ 다운로드 : https://developer.android.com/studio/index.html?hl=ko

설치를 끝냈으면 시작 버튼 옆에 검색 버튼에서 시스템 환경 변수 편집을 검색합니다.

시스템 변수 란에서 Path를 찾아서 다음과 같이 새로 추가합니다.

```cmd
%ANDROID_SDK_ROOT%
%ANDROID_SDK_ROOT%tools\
%ANDROID_SDK_ROOT%tools\bin\
%ANDROID_SDK_ROOT%platform-tools\
```

## Node.js

코르도바와 미사용 리소스 제거기는 Node.js로 개발된 프로그램이고, 노드 패키지 관리자를 이용하여 설치 및 업데이트를 하게 됩니다. 따라서 Node.js가 필요합니다.

▶ Download : https://nodejs.org/ko/download/

## Cordova v9.0.0

Node.js 설치 이후에 수행하셔야 합니다. 사실 본 프로그램 내에는 코르도바 설치를 직접 수행하는 기능이 따로 있지만, 아직까진 특정 상황에서만 수행됩니다. 따라서 직접 설치를 해주셔야 합니다. 과정은 ctrl + r를 누른 후, 실행에서 cmd(명령 프롬프트)를 여신 후, ```npm install -g cordova```를 입력하면 설치됩니다.

▶ 설치 방법 : https://www.npmjs.com/package/cordova#installation

## JDK 8
JDK 8에서만 안드로이드 API 가져오기 명령이 정상적으로 동작하기 때문에 JDK 8 설치가 필요합니다.

▶ Download : https://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html

JDK가 정상적으로 인식되려면 윈도우즈 환경 변수를 설정해야 합니다. 환경 변수는 Windows 10이라면 시작 버튼 옆에 돋보기 모양의 버튼을 눌러 환경 변수라고 치면 설정이 가능합니다.

▶ 환경 변수 JAVA_HOME 설정 : https://zetawiki.com/wiki/%EC%9C%88%EB%8F%84%EC%9A%B0_JAVA_HOME_%ED%99%98%EA%B2%BD%EB%B3%80%EC%88%98_%EC%84%A4%EC%A0%95

## Windows 10 이상 (Windows 7은 구동 확인되지 않았음)

본 프로그램이 구동 되려면 ```robocopy``` 명령이 제대로 실행되는 시스템을 사용해야 합니다. ```robocopy``` 명령은 ```xcopy```와는 달리 최소 8 쓰레드로 동시에 파일을 고속 복사할 수 있는 ```Windows``` 파일 복사 프로그램입니다. 본 프로그램은 파일을 목적 폴더에 고속으로 파일을 복사 하게 됩니다. 이 기능은 미사용 리소스 제거 기능을 사용하지 않을 때에만 동작합니다. 미사용 리소스 제거 기능을 사용할 땐 파일을 선별적으로 복사 합니다.

# 호환 플러그인

앱 빌드 시 추가 기능을 원하시는 분들이 있으시면 다음 파일들을 내려 받은 후, 압축을 해제하고 ```js/plugins``` 폴더에 넣어주시기 바랍니다.

종료 대화 상자는 기본적으로 포함되어있지 않습니다. 강제로 포함할 시 빌드 시간이 좀 추가되기 때문입니다.

백스페이스 버튼 터치 시 종료 대화 상자 띄우기 - <a href="https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/biud436/MV/blob/master/Android/RS_ExitDialog.js">다운로드[ZIP]</a>

애드몹의 경우, 플러그인 추가 과정에서 특정 인자를 넘겨야 합니다. 해당 세팅은 프로그램에서 자동으로 지원하는데요.

애드몹 광고 모듈 (배너, 동영상 광고, 보상형 광고) - <a href="https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/biud436/MV/blob/master/Android/RS_Admob.js">다운로드[ZIP]</a>

자동으로 세팅을 하려면 다음 플러그인을 추가로 넣어야 합니다.

​AdmobSettings.js - <a href="https://minhaskamal.github.io/DownGit/#/home?url=https://github.com/biud436/MV/blob/master/Android/AdmobSettings.js">다운로드[ZIP]</a>

​플러그인의 주석 부분(/*: ~ */ 사이 부분)에 ```@cordova_plugin cordova-plugin-admob-free```로 시작하는 라인이 있습니다. 

```js
/*:
 * @cordova_plugin cordova-plugin-admob-free --save --variable ADMOB_APP_ID="ca-app-pub~생략
 */
```

텍스트 에디터를 이용하여 ```ADMOB_APP_ID``` 부분의 테스트 기기 용 ```APP ID```를 애드몹 사이트에 있는 앱 ID로 수정을 해주시기 바랍니다.

화면 꺼짐 방지 플러그인은 필수 기능이므로 강제로 추가 됩니다.

# 플러그인 개발자 팁

게임 폴더를 설정할 때 플러그인이 자동 인식되는 원리는 플러그인 관리자의 원리와 동일하게 코멘트를 파싱하는 방식입니다.

따라서 다음과 같은 양식을 주석에 삽입해야 합니다.

이렇게 하면 프로젝트를 불러올 때 ```cordova-plugin-dialogs``` 라는 플러그인이 자동으로 빌드 과정에 추가됩니다.

최적화를 위해 라인을 최소한으로 읽으니 최상단 부분에 넣어주세요.

```js
/*:
* @cordova_plugin cordova-plugin-dialogs
*/
```

# Bug Report

빌드 중에 오류가 발생했을 때, ```Log``` 창에서 오른쪽 버튼을 누르면 빌드 로그 저장 버튼을 사용하면 로그 저장이 가능합니다.

아래 사이트에 버그를 제보할 때 빌드 로그도 같이 올려주시기 바랍니다.

정상적인 빌드 로그 :

```powershell
--- 준비 ---
java version "1.8.0_121"
Java(TM) SE Runtime Environment (build 1.8.0_121-b13)
Java HotSpot(TM) 64-Bit Server VM (build 25.121-b13, mixed mode)
-- keytool을 찾았습니다.
9.0.0 (cordova-lib@9.0.1)

안드로이드 SDK / ANDROID_SDK_ROOT를 찾았습니다 : E:\Android\sdk\
[SDK]==========================================
android-19
android-23
android-26
android-27
android-28
android-29
==============================================
--- 필요한 모든 프로그램이 설치되어있습니다.
프로그램이 최신 버전인지 확인 중입니다... (1초 미만)
최신 버전을 사용하고 있습니다.
코르도바가 최신 버전인지 확인 중입니다... (5초 미만)
현재 최신 버전의 코르도바를 사용하고 있습니다.(00:00:04.7386189)
```

한글로 된 파일명은 아직 구글 안드로이드 앱 빌드에서 지원하지 않으니 파일명은 반드시 영어여야 합니다. 

윈도우 사용자 계정 명과 그 부속 폴더들도 영어로 되어있어야 합니다.

https://github.com/biud436/MV-App-Builder/issues

# Download
다음 링크에서 최신 버전을 내려 받으시기 바랍니다 (자동 업데이트 기능 있음)

링크 : https://github.com/biud436/MV-App-Builder/releases