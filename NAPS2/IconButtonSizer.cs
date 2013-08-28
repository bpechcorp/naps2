﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NAPS2
{
    public class IconButtonSizer
    {
        private static int ButtonTextWidth(Button x)
        {
            return (int)Math.Ceiling(x.CreateGraphics().MeasureString(x.Text, x.Font).Width);
        }

        public void ResizeButtons(params Button[] buttons)
        {
            // Dynamically determine the size and padding of the add/edit/delete buttons to make localization simpler
            var maxTextWidth = buttons.Select(ButtonTextWidth).Max();
            var buttonWidth = maxTextWidth + WidthOffset; // Fixed offset based on icon width and ideal padding
            foreach (var btn in buttons)
            {
                if (MaxWidth != 0 && buttonWidth > MaxWidth)
                {
                    // Set the button to be at least its necessary size (for sure), and at most the specified MaxWidth (preferably)
                    btn.Width = Math.Max(MaxWidth, ButtonTextWidth(btn) + WidthOffset);
                }
                else
                {
                    // Set the button to be the same width as the largest button
                    btn.Width = buttonWidth;
                }
                // Update the padding so that the text center is in the same place on each button
                int rightPadding = PaddingRight + (btn.Width - WidthOffset - ButtonTextWidth(btn)) / 2;
                btn.Padding = new Padding(btn.Padding.Left, btn.Padding.Top, rightPadding, btn.Padding.Bottom);
            }
        }

        public int WidthOffset { get; set; }

        public int PaddingRight { get; set; }

        public int MaxWidth { get; set; }
    }
}