using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Cordova.Controls
{
    /// <summary>
    /// Modern styled GroupBox with dark theme support
    /// </summary>
    public class ModernGroupBox : GroupBox
    {
        private Color _borderColor = Color.FromArgb(70, 130, 180);
        private Color _headerColor = Color.FromArgb(45, 45, 48);
        private Color _backgroundColor = Color.FromArgb(37, 37, 38);
        private int _borderWidth = 1;
        private int _cornerRadius = 0;

        public ModernGroupBox()
        {
            SetStyle(ControlStyles.UserPaint | 
                     ControlStyles.ResizeRedraw | 
                     ControlStyles.DoubleBuffer | 
                     ControlStyles.AllPaintingInWmPaint, true);
            
            BackColor = _backgroundColor;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Padding = new Padding(10, 30, 10, 10);
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        public Color HeaderColor
        {
            get { return _headerColor; }
            set { _headerColor = value; Invalidate(); }
        }

        public int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; Invalidate(); }
        }

        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Background
            using (SolidBrush bgBrush = new SolidBrush(BackColor))
            {
                g.FillRectangle(bgBrush, ClientRectangle);
            }

            // Header background
            Rectangle headerRect = new Rectangle(0, 0, Width, 28);
            using (SolidBrush headerBrush = new SolidBrush(_headerColor))
            {
                g.FillRectangle(headerBrush, headerRect);
            }

            // Border
            using (Pen borderPen = new Pen(_borderColor, _borderWidth))
            {
                Rectangle borderRect = new Rectangle(
                    _borderWidth / 2,
                    _borderWidth / 2,
                    Width - _borderWidth,
                    Height - _borderWidth
                );
                g.DrawRectangle(borderPen, borderRect);
                
                // Header bottom line
                g.DrawLine(borderPen, 0, 28, Width, 28);
            }

            // Title text
            if (!string.IsNullOrEmpty(Text))
            {
                using (SolidBrush textBrush = new SolidBrush(ForeColor))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };
                    
                    Rectangle textRect = new Rectangle(10, 0, Width - 20, 28);
                    g.DrawString(Text, Font, textBrush, textRect, sf);
                }
            }
        }
    }
}
