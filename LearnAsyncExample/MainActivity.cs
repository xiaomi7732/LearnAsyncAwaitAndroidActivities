using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Lifecycle;
using System;
using System.Threading.Tasks;

namespace LearnAsyncExample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView _textViewTest;
        private Button _buttonClickMe;
        
        private TextViewModel _textViewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _textViewTest = FindViewById<TextView>(Resource.Id.textview_test);
            _buttonClickMe = FindViewById<Button>(Resource.Id.button_click_me);

            _buttonClickMe.Click += ButtonClickMe_Clicked;

            _textViewModel = (TextViewModel)new ViewModelProvider(this).Get(Java.Lang.Class.FromType(typeof(TextViewModel)));
        }

        protected override void OnResume()
        {
            base.OnResume();
            _textViewTest.Text = _textViewModel.TextValue;

            _textViewModel.PropertyChanged += TextViewModel_PropertyChanged;
        }

        protected override void OnPause()
        {
            _textViewModel.PropertyChanged -= TextViewModel_PropertyChanged;

            base.OnPause();
        }

        private void TextViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_textViewModel.TextValue))
            {
                _textViewTest.Text = _textViewModel.TextValue;
            }
        }

        private async void ButtonClickMe_Clicked(object sender, System.EventArgs e)
        {
            await _textViewModel.LoadDataAsync();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}