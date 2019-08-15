using DarkUI.Config;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DarkUI.Controls
{
    public class DarkSectionPanel : Panel
    {
        #region Field Region

        private string _sectionHeader;
        private int _sectionHeaderHeight = 25;
        private Font _sectionHeaderFont = DefaultFont;

        #endregion

        #region Property Region

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get => new Padding(base.Padding.Left, base.Padding.Top - SectionHeaderHeight, base.Padding.Right, base.Padding.Bottom);
            set
            {
                base.Padding = new Padding(value.Left, value.Top + SectionHeaderHeight, value.Right, value.Bottom);
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The section header text associated with this control.")]
        public string SectionHeader
        {
            get { return _sectionHeader; }
            set
            {
                _sectionHeader = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The height of the section header.")]
        [DefaultValue(25)]
        public int SectionHeaderHeight
        {
            get => _sectionHeaderHeight;
            set
            {
                _sectionHeaderHeight = value;
                base.Padding = new Padding(base.Padding.Left, value, base.Padding.Right, base.Padding.Bottom);
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The font used to render the section header text.")]
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8.25pt")]
        public Font SectionHeaderFont
        {
            get => _sectionHeaderFont;
            set
            {
                _sectionHeaderFont = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor Region

        public DarkSectionPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            base.Padding = new Padding(1, 25, 1, 1);
        }

        #endregion

        #region Event Handler Region

        protected override void OnEnter(System.EventArgs e)
        {
            base.OnEnter(e);

            Invalidate();
        }

        protected override void OnLeave(System.EventArgs e)
        {
            base.OnLeave(e);

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (Controls.Count > 0)
                Controls[0].Focus();
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = ClientRectangle;

            // Fill body
            using (var b = new SolidBrush(Colors.GreyBackground))
            {
                g.FillRectangle(b, rect);
            }

            // Draw header
            var bgColor = ContainsFocus ? Colors.BlueBackground : Colors.HeaderBackground;
            var darkColor = ContainsFocus ? Colors.DarkBlueBorder : Colors.DarkBorder;
            var lightColor = ContainsFocus ? Colors.LightBlueBorder : Colors.LightBorder;

            using (var b = new SolidBrush(bgColor))
            {
                var bgRect = new Rectangle(0, 0, rect.Width, SectionHeaderHeight);
                g.FillRectangle(b, bgRect);
            }

            using (var p = new Pen(darkColor))
            {
                g.DrawLine(p, rect.Left, 0, rect.Right, 0);
                g.DrawLine(p, rect.Left, SectionHeaderHeight - 1, rect.Right, SectionHeaderHeight - 1);
            }

            using (var p = new Pen(lightColor))
            {
                g.DrawLine(p, rect.Left, 1, rect.Right, 1);
            }

            var xOffset = 3;

            using (var b = new SolidBrush(Colors.LightText))
            {
                var textRect = new Rectangle(xOffset, 0, rect.Width - 4 - xOffset, SectionHeaderHeight);

                var format = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                g.DrawString(SectionHeader, SectionHeaderFont, b, textRect, format);
            }

            // Draw border
            using (var p = new Pen(Colors.DarkBorder, 1))
            {
                var modRect = new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);

                g.DrawRectangle(p, modRect);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Absorb event
        }

        #endregion
    }
}
