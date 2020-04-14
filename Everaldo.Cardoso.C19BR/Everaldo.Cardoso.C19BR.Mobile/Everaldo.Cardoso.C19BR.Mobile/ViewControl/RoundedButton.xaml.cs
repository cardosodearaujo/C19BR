using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundedButton : Grid
    {
        public RoundedButton()
        {
            InitializeComponent();
        }


        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            propertyName: nameof(Command),
            returnType: typeof(ICommand),
            declaringType: typeof(RoundedButton),
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
            declaringType: typeof(RoundedButton)
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

        public static readonly BindableProperty ValueWidthProperty = BindableProperty.Create(propertyName: nameof(ValueWidth), returnType: typeof(double), declaringType: typeof(RoundedButton), defaultBindingMode: BindingMode.TwoWay);
        public double ValueWidth
        {
            get
            { 
                return (double)GetValue(ValueWidthProperty); 
            }
            set
            { 
                SetValue(ValueWidthProperty, value); 
            }
        }


        public static readonly BindableProperty ValueHeightProperty = BindableProperty.Create(propertyName: nameof(ValueHeight), returnType: typeof(double), declaringType: typeof(RoundedButton), defaultBindingMode: BindingMode.TwoWay);
        public double ValueHeight
        {
            get
            {
                return (double)GetValue(ValueHeightProperty);
            }
            set
            {
                SetValue(ValueHeightProperty, value);
            }
        }


        public static readonly BindableProperty ButtonBackgroundColorProperty = BindableProperty.Create(propertyName: nameof(ButtonBackgroundColor), returnType: typeof(string), declaringType: typeof(RoundedButton), defaultBindingMode: BindingMode.TwoWay);
        public string ButtonBackgroundColor
        {
            get
            {
                return (string)GetValue(ButtonBackgroundColorProperty);
            }
            set
            {
                SetValue(ButtonBackgroundColorProperty, value);
            }
        }


        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(propertyName: nameof(TextColor), returnType: typeof(string), declaringType: typeof(RoundedButton), defaultBindingMode: BindingMode.TwoWay);
        public string TextColor
        {
            get
            {
                return (string)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }


        public static readonly BindableProperty TextProperty = BindableProperty.Create(propertyName: nameof(Text), returnType: typeof(string), declaringType: typeof(RoundedButton), defaultBindingMode: BindingMode.TwoWay);
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }


        public static readonly BindableProperty ButtonClickProperty = BindableProperty.Create(propertyName: nameof(ButtonClick), returnType: typeof(EventHandler), declaringType: typeof(RoundedButton), defaultBindingMode: BindingMode.TwoWay);
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

        public static readonly BindableProperty ValueBorderColorProperty = BindableProperty.Create(nameof(ValueBorderColor), typeof(string), typeof(RoundedButton));
        public string ValueBorderColor
        {
            get
            { return (string)GetValue(ValueBorderColorProperty); }
            set
            { SetValue(ValueBorderColorProperty, value); }
        }
    }
}