﻿using System.Drawing;

namespace DarkUI
{
    public class DarkListItem
    {
        #region Property Region

        public string Text { get; set; }
        public Rectangle Area { get; set; }
        public Color TextColor { get; set; }
        public FontStyle FontStyle { get; set; }

        #endregion

        #region Constructor Region

        public DarkListItem()
        {
            TextColor = Colors.LightText;
            FontStyle = FontStyle.Regular;
        }

        public DarkListItem(string text)
            : this()
        {
            Text = text;
        }

        #endregion
    }
}