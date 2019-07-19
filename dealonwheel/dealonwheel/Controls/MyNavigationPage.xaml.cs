using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dealonwheel.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyNavigationPage : NavigationPage
    {
        public MyNavigationPage() : base()
        {
            InitializeComponent();
        }

        public MyNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}