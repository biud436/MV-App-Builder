using System;
using System.Drawing;
using System.Windows.Forms;
using Cordova_Builder.Cordova.Common.Theme;

namespace Cordova_Builder.Cordova.Common.UI
{
    /// <summary>
    /// TextBox를 Panel로 감싸서 패딩과 테두리를 제공하는 래퍼 컨트롤
    /// </summary>
    public class PaddedTextBox : Panel
    {
        private TextBox _innerTextBox;
        
        public PaddedTextBox()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            _innerTextBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = DarkTheme.InputBackgroundColor,
                ForeColor = DarkTheme.InputForegroundColor,
                Dock = DockStyle.Fill
            };
            
            this.BackColor = DarkTheme.InputBackgroundColor;
            this.Padding = DarkTheme.InputPadding;
            this.BorderStyle = BorderStyle.FixedSingle;
            
            this.Controls.Add(_innerTextBox);
        }
        
        public TextBox InnerTextBox
        {
            get { return _innerTextBox; }
        }
        
        public override string Text
        {
            get { return _innerTextBox.Text; }
            set { _innerTextBox.Text = value; }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // 커스텀 테두리 그리기
            using (Pen pen = new Pen(DarkTheme.BorderColor, DarkTheme.BorderWidth))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
    
    /// <summary>
    /// ComboBox를 Panel로 감싸서 패딩과 테두리를 제공하는 래퍼 컨트롤
    /// </summary>
    public class PaddedComboBox : Panel
    {
        private ComboBox _innerComboBox;
        
        public PaddedComboBox()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            _innerComboBox = new ComboBox
            {
                FlatStyle = FlatStyle.Flat,
                BackColor = DarkTheme.InputBackgroundColor,
                ForeColor = DarkTheme.InputForegroundColor,
                Dock = DockStyle.Fill
            };
            
            this.BackColor = DarkTheme.InputBackgroundColor;
            this.Padding = DarkTheme.InputPadding;
            this.BorderStyle = BorderStyle.FixedSingle;
            
            this.Controls.Add(_innerComboBox);
        }
        
        public ComboBox InnerComboBox
        {
            get { return _innerComboBox; }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // 커스텀 테두리 그리기
            using (Pen pen = new Pen(DarkTheme.BorderColor, DarkTheme.BorderWidth))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
}
