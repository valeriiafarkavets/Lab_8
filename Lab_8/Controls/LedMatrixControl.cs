using System;
using System.Windows;
using System.Windows.Media;
namespace Lab_8.Controls{
    public class LedMatrixControl : FrameworkElement {
        private bool[,] pixels = new bool[10, 64];
        public void SetPixels(bool[,] newPixels){
            if (newPixels == null ||
                newPixels.GetLength(0) != 10 ||
                newPixels.GetLength(1) != 64)
                return;
            for (int y = 0; y < 10; y++){
                for (int x = 0; x < 64; x++){
                    pixels[y, x] = newPixels[y, x];
                }
            }
            InvalidateVisual();
        }
        protected override void OnRender(DrawingContext dc){
            base.OnRender(dc);
            double cellWidth = ActualWidth / 64;
            double cellHeight = ActualHeight / 10;
            double size = Math.Min(cellWidth, cellHeight) * 0.75;
            Brush onBrush = new SolidColorBrush(Color.FromRgb(45, 212, 191));
            Brush offBrush = new SolidColorBrush(Color.FromRgb(36, 64, 73));
            dc.DrawRectangle(new SolidColorBrush(Color.FromRgb(16, 39, 49)), null, new Rect(0, 0, ActualWidth, ActualHeight));
            for (int y = 0; y < 10; y++){
                for (int x = 0; x < 64; x++){
                    double centerX = x * cellWidth + cellWidth / 2;
                    double centerY = y * cellHeight + cellHeight / 2;
                    dc.DrawEllipse(
                        pixels[y, x] ? onBrush : offBrush,
                        null,
                        new Point(centerX, centerY),
                        size / 2,
                        size / 2);
                }
            }
        }
    }
}
