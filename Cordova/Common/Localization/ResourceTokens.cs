using Cordova.Common.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cordova.Common.Localization
{
    public static class ResourceTokens
    {
        // 기본 작업 관련 토큰
        public static readonly ResourceToken Create1 = new ResourceToken("Create1");
        public static readonly ResourceToken Create2 = new ResourceToken("Create2");

        // 플랫폼 관련 토큰
        public static readonly ResourceToken AddAndroidPlatform1 = new ResourceToken("AddAndroidPlatform1");
        public static readonly ResourceToken AddAndroidPlatform2 = new ResourceToken("AddAndroidPlatform2");
        public static readonly ResourceToken Requirements1 = new ResourceToken("Requirements1");
        public static readonly ResourceToken Requirements2 = new ResourceToken("Requirements2");

        // 설정 파일 관련 토큰
        public static readonly ResourceToken WriteConfig = new ResourceToken("WriteConfig");

        // 키스토어 관련 토큰
        public static readonly ResourceToken ALREADY_EXISTED_KEYSTORE = new ResourceToken("ALREADY_EXISTED_KEYSTORE");
        public static readonly ResourceToken CreateKeyStore1 = new ResourceToken("CreateKeyStore1");
        public static readonly ResourceToken CreateKeyStore2 = new ResourceToken("CreateKeyStore2");
        public static readonly ResourceToken CreateKeyStore3 = new ResourceToken("CreateKeyStore3");
        public static readonly ResourceToken CreateKeyStore4 = new ResourceToken("CreateKeyStore4");
        public static readonly ResourceToken CreateKeyStore5 = new ResourceToken("CreateKeyStore5");

        // 파일 관련 토큰
        public static readonly ResourceToken ModifyHtmlFiles = new ResourceToken("ModifyHtmlFiles");
        public static readonly ResourceToken CopyProjectFiles1 = new ResourceToken("CopyProjectFiles1");
        public static readonly ResourceToken CopyProjectFiles2 = new ResourceToken("CopyProjectFiles2");
        public static readonly ResourceToken CopyProjectFiles3 = new ResourceToken("CopyProjectFiles3");
        public static readonly ResourceToken CopyProjectFiles4 = new ResourceToken("CopyProjectFiles4");

        // 플러그인 관련 토큰
        public static readonly ResourceToken AddPlugins1 = new ResourceToken("AddPlugins1");
        public static readonly ResourceToken AddPlugins2 = new ResourceToken("AddPlugins2");

        // 빌드 및 설치 관련 토큰
        public static readonly ResourceToken Flush1 = new ResourceToken("Flush1");
        public static readonly ResourceToken Flush2 = new ResourceToken("Flush2");
        public static readonly ResourceToken Flush3 = new ResourceToken("Flush3");
        public static readonly ResourceToken INSTALLING_CORDOVA = new ResourceToken("INSTALLING_CORDOVA");
        public static readonly ResourceToken SUCCESS_INSTALLED_CORDOVA = new ResourceToken("SUCCESS_INSTALLED_CORDOVA");
        public static readonly ResourceToken FAIL_INSTALLED_CORDOVA = new ResourceToken("FAIL_INSTALLED_CORDOVA");
        public static readonly ResourceToken SUCCESSED_SETUP_FILE = new ResourceToken("SUCCESSED_SETUP_FILE");

        // 버전 관련 토큰
        public static readonly ResourceToken CHECK_VERSION_ALPHA = new ResourceToken("CHECK_VERSION_ALPHA");
        public static readonly ResourceToken CHECK_VERSION_OLD = new ResourceToken("CHECK_VERSION_OLD");
        public static readonly ResourceToken CHECK_VERSION_OLD_ASK = new ResourceToken("CHECK_VERSION_OLD_ASK");
        public static readonly ResourceToken CHECK_VERSION_LATEST = new ResourceToken("CHECK_VERSION_LATEST");
        public static readonly ResourceToken NOT_LATEST_CORDOV_VER = new ResourceToken("NOT_LATEST_CORDOV_VER");
        public static readonly ResourceToken REQUEST_NPM_INSTALL = new ResourceToken("REQUEST_NPM_INSTALL");
        public static readonly ResourceToken REQUEST_NPM_INSTALL_ASK = new ResourceToken("REQUEST_NPM_INSTALL_ASK");
        public static readonly ResourceToken LATEST_CORDOVA_READY = new ResourceToken("LATEST_CORDOVA_READY");
    }
}