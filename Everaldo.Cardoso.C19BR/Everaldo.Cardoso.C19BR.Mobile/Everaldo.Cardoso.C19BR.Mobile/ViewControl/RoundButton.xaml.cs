using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundButton : Grid
    {        
        public RoundButton()
        {
            InitializeComponent();
            ValueHeight = 60;
            ValueWidth = 60;
            ValueMargin = 30;
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: nameof(Command),
            returnType: typeof(ICommand),
            declaringType: typeof(RoundButton),
            defaultValue: default(ICommand)
            );
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(CommandParameter),
            returnType: typeof(object),
            declaringType: typeof(RoundButton)
            );
        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        public static readonly BindableProperty IconCodeProperty = BindableProperty.Create(nameof(IconCode), typeof(string), typeof(RoundButton));
        public string IconCode
        {
            get
            { return (string)GetValue(IconCodeProperty); }
            set
            { SetValue(IconCodeProperty, value); }
        }

        public static readonly BindableProperty IconCodeColorProperty = BindableProperty.Create(nameof(IconCodeColor), typeof(string), typeof(RoundButton));
        public string IconCodeColor
        {
            get
            { return (string)GetValue(IconCodeColorProperty); }
            set
            { SetValue(IconCodeColorProperty, value); }
        }

        public static readonly BindableProperty ButtonBackgroundColorProperty = BindableProperty.Create(nameof(ButtonBackgroundColor), typeof(string), typeof(RoundButton));
        public string ButtonBackgroundColor
        {
            get
            { return (string)GetValue(ButtonBackgroundColorProperty); }
            set
            { SetValue(ButtonBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty ButtonClickProperty = BindableProperty.Create(propertyName: nameof(ButtonClick), returnType: typeof(EventHandler), declaringType: typeof(RoundButton), defaultBindingMode: BindingMode.TwoWay);
        public EventHandler ButtonClick
        {
            get
            { return (EventHandler)GetValue(ButtonClickProperty); }
            set
            { SetValue(ButtonClickProperty, value); }
        }
        private void ButtonClicked(object sender, EventArgs e)
        {
           if (ButtonClick != null) ButtonClick(sender, e);
        }

        public static readonly BindableProperty ValueHeightProperty = BindableProperty.Create(nameof(ValueHeight), typeof(double), typeof(RoundButton));
        public double ValueHeight
        {
            get
            { return (double)GetValue(ValueHeightProperty); }
            set
            { SetValue(ValueHeightProperty, value); }
        }

        public static readonly BindableProperty ValueWidthProperty = BindableProperty.Create(nameof(ValueWidth), typeof(double), typeof(RoundButton));
        public double ValueWidth
        {
            get
            { return (double)GetValue(ValueWidthProperty); }
            set
            { SetValue(ValueWidthProperty, value); }
        }

        public static readonly BindableProperty ValueMarginProperty = BindableProperty.Create(nameof(ValueMargin), typeof(double), typeof(RoundButton));
        public double ValueMargin
        {
            get
            { return (double)GetValue(ValueMarginProperty); }
            set
            { SetValue(ValueMarginProperty, value); }
        }

        public static readonly BindableProperty ValueBorderColorProperty = BindableProperty.Create(nameof(ValueBorderColor), typeof(string), typeof(RoundButton));
        public string ValueBorderColor
        {
            get
            { return (string)GetValue(ValueBorderColorProperty); }
            set
            { SetValue(ValueBorderColorProperty, value); }
        }
    }    
}