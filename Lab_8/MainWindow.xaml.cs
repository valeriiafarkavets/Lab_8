using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Lab_8.Models;
using Lab_8.Services;
namespace Lab_8{
    public partial class MainWindow : Window{
        private FontService font = new FontService();
        private Glyph8x10 currentGlyph = new Glyph8x10();
        private DispatcherTimer timer = new DispatcherTimer();
        private Button[,] editorButtons = new Button[10, 8];
        private bool[,] displayPixels = new bool[10, 64];
        private bool[,] messagePixels = new bool[10, 64];
        private int position = 0;
        private int messageWidth = 0;
        public MainWindow(){
            InitializeComponent();
            CreateEditor();
            LoadSymbol();
            timer.Tick += Timer_Tick;
            ChangeSpeed();
        }
        private void CreateEditor() {
            EditorGrid.Children.Clear();
            for (int y = 0; y < 10; y++){
                for (int x = 0; x < 8; x++){
                    Button button = new Button();
                    button.Margin = new Thickness(2);
                    button.Tag = new Point(x, y);
                    button.Click += PixelButton_Click;
                    editorButtons[y, x] = button;
                    EditorGrid.Children.Add(button);
                }
            }
        }
        private void UpdateEditor(){
            for (int y = 0; y < 10; y++){
                for (int x = 0; x < 8; x++){
                    editorButtons[y, x].Background =
                        currentGlyph.GetPixel(x, y)
                        ? Brushes.Turquoise
                        : new SolidColorBrush(Color.FromRgb(36, 64, 73));
                }
            }
            HexTextBox.Text = currentGlyph.GetHex();
        }
        private void PixelButton_Click(object sender, RoutedEventArgs e){
            Button button = (Button)sender;
            Point point = (Point)button.Tag;
            currentGlyph.TogglePixel((int)point.X, (int)point.Y);
            UpdateEditor();
            StatusText.Text = "Піксель змінено";
        }
        private void LoadSymbol(){
            if (SymbolTextBox.Text.Length == 0)
                return;
            char symbol = SymbolTextBox.Text[0];
            currentGlyph = new Glyph8x10();
            currentGlyph.SetBytes(font.GetGlyph(symbol).GetBytes());
            UpdateEditor();
            StatusText.Text = "Символ завантажено";
        }
        private void LoadSymbolButton_Click(object sender, RoutedEventArgs e){
            LoadSymbol();
        }
        private void SaveSymbolButton_Click(object sender, RoutedEventArgs e){
            if (SymbolTextBox.Text.Length == 0)
                return;
            char symbol = SymbolTextBox.Text[0];
            Glyph8x10 saved = new Glyph8x10();
            saved.SetBytes(currentGlyph.GetBytes());
            font.SaveGlyph(symbol, saved);
            StatusText.Text = "Символ збережено";
        }
        private void ApplyHexButton_Click(object sender, RoutedEventArgs e){
            string[] parts = HexTextBox.Text.Split( new[] { ' ', '\n', '\r', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 10) {
                StatusText.Text = "Потрібно ввести рівно 10 HEX-байтів";
                return;
            }
            byte[] data = new byte[10];
            for (int i = 0; i < 10; i++){
                if (!byte.TryParse(parts[i],
                    System.Globalization.NumberStyles.HexNumber,
                    null, out data[i])){
                    StatusText.Text = "Некоректний HEX-код";
                    return;
                }
            }
            currentGlyph.SetBytes(data);
            UpdateEditor();
            StatusText.Text = "HEX-код застосовано";
        }
        private void StartButton_Click(object sender, RoutedEventArgs e){
            PrepareMessage();
            position = 0;
            timer.Start();
            StatusText.Text = "Рядок рухається";
        }
        private void StopButton_Click(object sender, RoutedEventArgs e){
            timer.Stop();
            StatusText.Text = "Рух зупинено";
        }
        private void SpeedSlider_ValueChanged(object sender,
            RoutedPropertyChangedEventArgs<double> e){
            if (timer != null)
                ChangeSpeed();
        }
private void ChangeSpeed() {
    int speed = (int)SpeedSlider.Value;
    timer.Interval = TimeSpan.FromMilliseconds(550 - speed * 45);

    if (SpeedText != null)
        SpeedText.Text = speed.ToString();
}
        private void PrepareMessage() {
            string text = MessageTextBox.Text.ToUpperInvariant();
            messageWidth = text.Length * 8 + 64;
            messagePixels = new bool[10, messageWidth];
            for (int i = 0; i < text.Length; i++) {
                Glyph8x10 glyph = font.GetGlyph(text[i]);
                for (int y = 0; y < 10; y++){
                    for (int x = 0; x < 8; x++){
                        messagePixels[y, 64 + i * 8 + x] =
                            glyph.GetPixel(x, y);
                    }
                }
            }
        }
        private void Timer_Tick(object? sender, EventArgs e){
            for (int y = 0; y < 10; y++){
                for (int x = 0; x < 64; x++){
                    int sourceX = position + x;
                    displayPixels[y, x] =
                        sourceX < messageWidth &&
                        messagePixels[y, sourceX];
                }
            }
            LedDisplay.SetPixels(displayPixels);
            position++;
            if (position >= messageWidth)
                position = 0;
        }
    }
}
