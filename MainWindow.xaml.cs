using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Main Frame Window.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }

    /// <summary>
    /// MainWindow's ViewModel
    /// </summary>
    class MainWndModel : INotifyPropertyChanged
    {
        #region Properties
        private byte[] ColorBytes = new byte[3];

        public byte Red
        {
            get { return ColorBytes[0]; }
            set
            {
                if (value != ColorBytes[0])
                {
                    ColorBytes[0] = value;
                    RaisePropertyChanged(nameof(Red));

                    var color = Color.FromRgb(Red, Green, Blue);
                    UpdateHtmlColor(color);
                    UpdateWin32MacroColor(ScreenColor);
                    UpdateBrushColor(color);
                }
            }
        }

        public byte Green
        {
            get { return ColorBytes[1]; }
            set
            {
                if (value != ColorBytes[1])
                {
                    ColorBytes[1] = value;
                    RaisePropertyChanged(nameof(Green));

                    var color = Color.FromRgb(Red, Green, Blue);
                    UpdateHtmlColor(color);
                    UpdateWin32MacroColor(ScreenColor);
                    UpdateBrushColor(color);
                }
            }
        }

        public byte Blue
        {
            get { return ColorBytes[2]; }
            set
            {
                if (value != ColorBytes[2])
                {
                    ColorBytes[2] = value;
                    RaisePropertyChanged(nameof(Blue));

                    var color = Color.FromRgb(Red, Green, Blue);
                    UpdateHtmlColor(color);
                    UpdateWin32MacroColor(ScreenColor);
                    UpdateBrushColor(color);
                }
            }
        }

        private Color _ScreenColor = Colors.Black;
        public Color ScreenColor
        {
            get { return _ScreenColor; }
            set
            {
                if (value != _ScreenColor)
                {
                    _ScreenColor = value;

                    UpdateRgbColor(ScreenColor);
                    UpdateHtmlColor(ScreenColor);
                    UpdateWin32MacroColor(ScreenColor);
                    UpdateBrushColor(ScreenColor);
                }
            }
        }

        public string HtmlColor
        {
            get;
            private set;
        } = "#000000";

        public string Win32MacroColor
        {
            get;
            private set;
        } = "RGB(0, 0, 0)";

        public Brush BrushColor
        {
            get;
            private set;
        } = new SolidColorBrush(Colors.Black);
        #endregion

        #region Functions
        protected void UpdateRgbColor(Color color)
        {
            ColorBytes[0] = color.R;
            ColorBytes[1] = color.G;
            ColorBytes[2] = color.B;
            RaisePropertyChanged(nameof(Red));
            RaisePropertyChanged(nameof(Green));
            RaisePropertyChanged(nameof(Blue));
        }

        protected void UpdateHtmlColor(Color color)
        {
            HtmlColor = string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
            RaisePropertyChanged(nameof(HtmlColor));
        }

        protected void UpdateWin32MacroColor(Color color)
        {
            Win32MacroColor = string.Format("RGB({0}, {1}, {2})", color.R, color.G, color.B);
            RaisePropertyChanged(nameof(Win32MacroColor));
        }

        protected void UpdateBrushColor(Color color)
        {
            BrushColor = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
            RaisePropertyChanged(nameof(BrushColor));
        }
        #endregion

        #region Commands
        private RelayCommand _CopyHtmlCommand;
        public ICommand CopyHtmlCommand
        {
            get
            {
                return _CopyHtmlCommand ?? (_CopyHtmlCommand = new RelayCommand(p =>
                {
                    Clipboard.SetText(HtmlColor);
                }));
            }
        }

        private RelayCommand _CopyWin32MacroCommand;
        public ICommand CopyWin32MacroCommand
        {
            get
            {
                return _CopyWin32MacroCommand ?? (_CopyWin32MacroCommand = new RelayCommand(p =>
                {
                    Clipboard.SetText(Win32MacroColor);
                }));
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
