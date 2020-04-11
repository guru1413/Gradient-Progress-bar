using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomProgressBar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradientProgressBar : Frame
    {
        public float Percent
        {
            get { return Percentage * 100; }
        }

        public GradientProgressBar()
        {
            InitializeComponent();
            Content.BindingContext = this;
        }
        public static BindableProperty PercentageProperty = BindableProperty.Create(nameof(Percentage), typeof(float),
                                                             typeof(GradientProgressBar), 0f, BindingMode.OneWay,
                                                             validateValue: (_, value) => value != null,
                                                             propertyChanged: OnPercentageChanged);


        public float Percentage
        {
            get => (float)GetValue(PercentageProperty);
            set => SetValue(PercentageProperty, value);
        }

        public static BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float),
                                                           typeof(GradientProgressBar), 12f, BindingMode.OneWay,
                                                           validateValue: (_, value) => value != null && (float)value >= 0,
                                                           propertyChanged: (b, o, n) => { ((GradientProgressBar)b).FontSize = (float)n; });

        public float FontSize
        {
            get => (float)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static BindableProperty GradientStartColorProperty = BindableProperty.Create(nameof(GradientStartColor), typeof(Color),
                                                                    typeof(GradientProgressBar), Color.Purple, BindingMode.OneWay,
                                                                    validateValue: (_, value) => value != null,
                                                                    propertyChanged: (b, o, n) => { ((GradientProgressBar)b).GradientStartColor = (Color)n; });

        public Color GradientStartColor
        {
            get => (Color)GetValue(GradientStartColorProperty);
            set => SetValue(GradientStartColorProperty, value);
        }

        public static BindableProperty GradientEndColorProperty = BindableProperty.Create(nameof(GradientEndColor), typeof(Color),
                                                                    typeof(GradientProgressBar), Color.Blue, BindingMode.OneWay,
                                                                    validateValue: (_, value) => value != null,
                                                                    propertyChanged: (b, o, n) => { ((GradientProgressBar)b).GradientEndColor = (Color)n; });

        public Color GradientEndColor
        {
            get => (Color)GetValue(GradientEndColorProperty);
            set => SetValue(GradientEndColorProperty, value);
        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color),
                                                            typeof(GradientProgressBar), Color.Blue, BindingMode.OneWay,
                                                            validateValue: (_, value) => value != null,
                                                            propertyChanged: (b, o, n) => { ((GradientProgressBar)b).TextColor = (Color)n; });

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static BindableProperty AlternativeTextColorProperty = BindableProperty.Create(nameof(AlternativeTextColor), typeof(Color),
                                                                        typeof(GradientProgressBar), Color.White, BindingMode.OneWay,
                                                                        validateValue: (_, value) => value != null,
                                                                        propertyChanged: (b, o, n) => { ((GradientProgressBar)b).AlternativeTextColor = (Color)n; });

        public Color AlternativeTextColor
        {
            get => (Color)GetValue(AlternativeTextColorProperty);
            set => SetValue(AlternativeTextColorProperty, value);
        }


        private static void OnPercentageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            GradientProgressBar progressControl = bindable as GradientProgressBar;
            progressControl.OnPropertyChanged(nameof(Percent));

            progressControl.UpdateValue(progressControl);
        }

        private void UpdateValue(GradientProgressBar progressControl)
        {
            var percentageWidth = progressControl.progressView.WidthRequest = Math.Floor(progressControl.unprogressView.Width * progressControl.Percentage);

            var xText = percentageWidth / 2 - progressControl.pLbl.X / 2;
            if (xText <= 0)
            {
                xText = progressControl.unprogressView.Width / 2 - progressControl.pLbl.X;
                progressControl.pLbl.TextColor = TextColor;
            }
            else
            {
                xText = percentageWidth / 2 - progressControl.pLbl.X;
                progressControl.pLbl.TextColor = AlternativeTextColor;
            }
            progressControl.pLbl.TranslationX = xText;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            UpdateValue(this);
        }
    }
}