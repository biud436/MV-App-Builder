using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cordova_Builder.Cordova.Common.Theme
{
    /// <summary>
    /// 다크 테마 관련 색상 및 레이아웃 상수 정의
    /// </summary>
    public static class DarkTheme
    {
        #region Background and Foreground Colors
        
        /// <summary>
        /// 입력 폼의 배경 색상 (RGB: 26, 41, 62)
        /// </summary>
        public static readonly Color InputBackgroundColor = Color.FromArgb(255, 26, 41, 62);
        
        /// <summary>
        /// 입력 폼의 텍스트 색상 (White)
        /// </summary>
        public static readonly Color InputForegroundColor = Color.White;
        
        /// <summary>
        /// 플러그인 목록의 배경 색상 (RGB: 57, 60, 62)
        /// </summary>
        public static readonly Color PluginListBackgroundColor = Color.FromArgb(255, 57, 60, 62);
        
        /// <summary>
        /// 플러그인 목록의 텍스트 색상 (White)
        /// </summary>
        public static readonly Color PluginListForegroundColor = Color.White;
        
        /// <summary>
        /// 로그 창의 배경 색상 (RGB: 57, 60, 62)
        /// </summary>
        public static readonly Color LogBackgroundColor = Color.FromArgb(255, 57, 60, 62);
        
        /// <summary>
        /// 로그 창의 텍스트 색상 (White)
        /// </summary>
        public static readonly Color LogForegroundColor = Color.White;
        
        #endregion
        
        #region Text Colors
        
        /// <summary>
        /// 에러 메시지 색상 (Red)
        /// </summary>
        public static readonly Color ErrorTextColor = Color.Red;
        
        /// <summary>
        /// 성공 메시지 색상 (LightSteelBlue)
        /// </summary>
        public static readonly Color SuccessTextColor = Color.LightSteelBlue;
        
        /// <summary>
        /// 경고 메시지 색상 (Yellow)
        /// </summary>
        public static readonly Color WarningTextColor = Color.Yellow;
        
        /// <summary>
        /// 정보 메시지 색상 (YellowGreen)
        /// </summary>
        public static readonly Color InfoTextColor = Color.YellowGreen;
        
        #endregion
        
        #region Border Colors
        
        /// <summary>
        /// 기본 테두리 색상 (RGB: 45, 45, 48)
        /// </summary>
        public static readonly Color BorderColor = Color.FromArgb(255, 45, 45, 48);
        
        /// <summary>
        /// 밝은 테두리 색상 (RGB: 63, 63, 70)
        /// </summary>
        public static readonly Color BorderLightColor = Color.FromArgb(255, 63, 63, 70);
        
        #endregion
        
        #region Layout Constants
        
        /// <summary>
        /// 입력 컨트롤의 내부 패딩 (8dp)
        /// </summary>
        public static readonly Padding InputPadding = new Padding(8);
        
        /// <summary>
        /// 행(Row)의 외부 여백 (좌우 8dp, 상하 4dp)
        /// </summary>
        public static readonly Padding RowPadding = new Padding(8, 4, 8, 4);
        
        /// <summary>
        /// 테두리 두께 (1px)
        /// </summary>
        public const int BorderWidth = 1;
        
        /// <summary>
        /// 입력 컨트롤의 기본 높이 (32dp)
        /// </summary>
        public const int RowHeight = 32;
        
        #endregion
    }
}
