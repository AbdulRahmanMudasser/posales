using System;
using System.Drawing;
using System.Windows.Forms;

public class CustomPanel : Panel
{
    public Color BorderColor { get; set; } = Color.Black; // Default border color

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using (Pen pen = new Pen(BorderColor, 1))
        {
            // Draw border around the panel
            e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}