using MauiReactor;
using Microsoft.Maui.Devices;
using Syncfusion.Maui.Popup;
using System;

namespace Project3.Components
{
    [Scaffold(typeof(Syncfusion.Maui.Popup.SfPopup))]
    public partial class VolosPopUp
    {
        bool _showCloseButton = false;

        private void Init()
        {
            base.OnMount();
            this.AutoSizeMode(PopupAutoSizeMode.Height);
        }

        public VolosPopUp AutoSizeToMiddle(bool attiva)
        {
            if (attiva)
            {
                this.AutoSizeMode(PopupAutoSizeMode.Both);
                this.WidthRequest(DeviceDisplay.Current.MainDisplayInfo.Width / 2);
                this.HeightRequest(DeviceDisplay.Current.MainDisplayInfo.Height / 2);
            }
            else
            {
                this.AutoSizeMode(PopupAutoSizeMode.Height);
            }

            return this;
        }

        public VolosPopUp SetText(string testo)
        {
            Init();

            this.Set(Syncfusion.Maui.Popup.SfPopup.ContentTemplateProperty,
                new MauiControls.DataTemplate(() => TemplateHost.Create(DefaultLabelText(testo)).NativeElement));

            return this;
        }

        public VolosPopUp SetText(Func<string> textFunc)
        {
            Init();

            this.Set(Syncfusion.Maui.Popup.SfPopup.ContentTemplateProperty,
                new MauiControls.DataTemplate(() => TemplateHost.Create(DefaultLabelText(textFunc)).NativeElement));

            return this;
        }

        public VolosPopUp Content(Func<VisualNode> render)
        {
            Init();

            this.Set(Syncfusion.Maui.Popup.SfPopup.ContentTemplateProperty,
                new MauiControls.DataTemplate(() => TemplateHost.Create(DefaultMsgContainer(render)).NativeElement));
            return this;
        }

        public VolosPopUp KoCallback(Action func)
        {
            this.Set(Syncfusion.Maui.Popup.SfPopup.DeclineCommandProperty, new MauiControls.Command(func ?? new(() => { })));
            return this;
        }

        public VolosPopUp SetHeaderText(string headerText)
        {
            this.Set(Syncfusion.Maui.Popup.SfPopup.HeaderTemplateProperty,
                new MauiControls.DataTemplate(() => TemplateHost.Create(DefaultHeaderTemplate(headerText, this)).NativeElement));
            return this;
        }

        public VolosPopUp SetHeaderText(Func<string> headerTextFunc)
        {
            this.Set(Syncfusion.Maui.Popup.SfPopup.HeaderTemplateProperty,
                new MauiControls.DataTemplate(() => TemplateHost.Create(DefaultHeaderTemplate(headerTextFunc(), this)).NativeElement));

            return this;
        }

        public VolosPopUp SetHeaderTemplate(Func<VisualNode> render)
        {
            this.Set(Syncfusion.Maui.Popup.SfPopup.HeaderTemplateProperty,
              new MauiControls.DataTemplate(() => TemplateHost.Create(render()).NativeElement));
            return this;
        }

        public VolosPopUp SetFooterTemplate(Func<VisualNode> render)
        {
            this.Set(Syncfusion.Maui.Popup.SfPopup.FooterTemplateProperty,
              new MauiControls.DataTemplate(() => TemplateHost.Create(render()).NativeElement));
            return this;
        }

        // Override
        public VolosPopUp ShowCloseButton(bool showCloseButton)
        {
            _showCloseButton = showCloseButton;
            //NativeControl.ShowCloseButton = false;
            return this;
        }

        private static VisualNode DefaultLabelText(string testo)
        {
            VisualNode render() =>
                new Label(testo?.Replace(@"[NEWLINE]", Environment.NewLine))
                    .HorizontalTextAlignment(TextAlignment.Start)
                    .VerticalTextAlignment(TextAlignment.Start)
                    .LineBreakMode(LineBreakMode.WordWrap);
            return DefaultMsgContainer(render);
        }

        private static VisualNode DefaultLabelText(Func<string> textFunc)
        {
            VisualNode render() =>
                new Label(textFunc?.Invoke()?.Replace(@"[NEWLINE]", Environment.NewLine))
                    .HorizontalTextAlignment(TextAlignment.Start)
                    .VerticalTextAlignment(TextAlignment.Start)
                    .LineBreakMode(LineBreakMode.WordWrap);
            return DefaultMsgContainer(render);
        }

        private static VisualNode DefaultMsgContainer(Func<VisualNode> render)
        {
            VisualNode vn =
               new ScrollView {
                    new VStack {
                        render()
                    }.Padding(10)
               };
            return vn;
            //ScrollView sv = new();

            //MauiControls.ScrollView _sv = TemplateHost.Create(sv).NativeElement as MauiControls.ScrollView;
            //MauiControls.VerticalStackLayout _v = new();
            //var cont = TemplateHost.Create(render()).NativeElement;
            //_v.Add(cont as Microsoft.Maui.IView);
            //_sv.Content = _v;

            //VisualNode vn = sv;
            //return vn;

            //ScrollView sv = new();
            //var native = TemplateHost.Create(sv).NativeElement;
            //(native as Microsoft.Maui.IView).InvalidateMeasure();

            //var vs = new VStack { render() }.Padding(10);
            //(native as MauiControls.ScrollView).Content = TemplateHost.Create(vs).NativeElement as MauiControls.View;

            //VisualNode vn = sv;
            //return vn;
        }

        private static VisualNode DefaultHeaderTemplate(string headerText, VolosPopUp instance)
        {
            Label lblTitolo = new Label(headerText)
                .FontAttributes(MauiControls.FontAttributes.Bold)
                .FontSize(21)
                .HorizontalTextAlignment(TextAlignment.Center)
                .VerticalTextAlignment(TextAlignment.Center)
                .LineBreakMode(LineBreakMode.NoWrap)
                .TextColor(Colors.White);

            Button btnCloseIcon = new Button()
                .OnClicked(() => { instance.NativeControl.IsOpen = false; })
                .BackgroundColor(Colors.Transparent)
                .BorderWidth(0)
                .Text("X")
                .FontSize(21).IsVisible(instance._showCloseButton);

            VisualNode vn = new VStack
            {
                new Grid("*", "0.3*,*,0.3*") {
                    lblTitolo.GridRow(0).GridColumn(1),
                    btnCloseIcon.GridRow(0).GridColumn(2)
                },
            }.VCenter().Spacing(0).Padding(0);

            return vn;
        }
    }

    [Scaffold(typeof(Syncfusion.Maui.Core.SfView))]
    public abstract class SfView { }
}
