using MauiReactor;
using Syncfusion.Maui.Popup;

namespace Project3.Components
{
    public class VolosLoading : VolosPopUp
    {
        public VolosLoading()
        {
            //Color background = Utility.IsDarkTheme() ? Colors.Transparent : Colors.Black;
            //Color loading = Utility.GetResources<Color>(Utility.IsDarkTheme() ? "PrimaryDark" : "Primary");
            Color background = Colors.Transparent;
            Color loading = Colors.Orange;

            this.WidthRequest(50);
            this.HeightRequest(50);
            this.ShowFooter(false);
            this.ShowHeader(false);
            this.BackgroundColor(background);
            this.PopupStyle(new PopupStyle() { MessageBackground = background });
            this.Content(() =>
                new VStack() {
                    new ActivityIndicator().IsRunning(true).Color(loading)//.Scale(5)
                }).VCenter().HCenter().BackgroundColor(background);
        }
    }
}