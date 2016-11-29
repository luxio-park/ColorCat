using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Luxio.ColorCat
{
    /// <summary>
    /// Spoide control that get color from dragging mouse to screen.
    /// </summary>
    public partial class SpoidControl : UserControl
    {
        public SpoidControl()
        {
            InitializeComponent();
        }

        #region SelectedColor Property
        public static readonly DependencyProperty SelectedColorProperty
            = DependencyProperty.Register(nameof(SelectedColor), typeof(Color), typeof(SpoidControl),
                new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.None));

        /// <summary>
        /// Gets color from dragging mouse to screen.
        /// </summary>
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { throw new Exception("An attempt of modify read-only property."); }
        }
        #endregion

        #region Functions
        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CursorImg.Visibility = Visibility.Hidden;
            Mouse.OverrideCursor = new Cursor(
                Application.GetResourceStream(
                    new Uri(@"pack://application:,,,/Dropper.cur")
                ).Stream
            );
            Mouse.Capture(this);
            e.Handled = true;
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CursorImg.Visibility = Visibility.Visible;
            Mouse.OverrideCursor = Cursors.Arrow;
            GetScreenColor();
            ReleaseMouseCapture();
            e.Handled = true;
        }

        private void GetScreenColor()
        {
            var clientPoint = Mouse.GetPosition(this);
            var screenPoint = PointToScreen(clientPoint);

            var hDC = NativeMethods.GetWindowDC(IntPtr.Zero);
            var pixel = NativeMethods.GetPixel(hDC, (int)screenPoint.X, (int)screenPoint.Y);
            NativeMethods.ReleaseDC(IntPtr.Zero, hDC);

            var color = Color.FromRgb(
                (byte)pixel,
                (byte)(pixel >> 8),
                (byte)(pixel >> 16)
            );
            SetValue(SelectedColorProperty, color);
        }
        #endregion
    }
}
