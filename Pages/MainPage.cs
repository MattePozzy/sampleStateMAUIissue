using MauiReactor;
using Project3.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Pages
{
    internal class MainPageState : BaseState
    {
        public int Counter { get; set; }
    }

    internal class MainPage : VolosComponent<MainPageState>
    {
        public override VisualNode Render()
        {
            //return new VolosPage(State.TS.Translate("OFL_menu.iTuoiInterventi"))
            //{
                
            //}.OnAppearing(OnAppearing);

            return new ContentPage
            {
                BaseRender(new List<VisualNode> { 
                    new ScrollView
                    {
                        new VerticalStackLayout
                        {
                            new Image("dotnet_bot.png")
                                .HeightRequest(200)
                                .HCenter()
                                .Set(Microsoft.Maui.Controls.SemanticProperties.DescriptionProperty, "Cute dot net bot waving hi to you!"),

                            new Label(() => State._AlertState.MsgAlert)
                                .FontSize(32)
                                .HCenter(),

                            new Label("Welcome to MauiReactor: MAUI with superpowers!")
                                .FontSize(18)
                                .HCenter(),

                            new Button(State.Counter == 0 ? "Click me" : $"Clicked {State.Counter} times!")
                                .OnClicked(onClick)
                                .HCenter()
                        }
                        .VCenter()
                        .Spacing(25)
                        .Padding(30, 0)
                    }
                })
            };
        }

        private void onClick()
        {
            ApriAlert("MSG_" + new Random().Next(10, 100));
        }
    }
}