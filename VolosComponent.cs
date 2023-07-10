using MauiReactor;
using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Project3.Components
{
    public class VolosComponent<T, S> : Component<T, S> where T : BaseState, new() where S : class, new()
    {
        public void HideLoading()
        {
            SetState(s => s.IsBusy = false, false);
        }

        public void ShowLoading()
        {
            SetState(s => s.IsBusy = true, false);
        }

        public void ApriAlert(string messaggio, string headerText = "", Action callbackOk = null)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SetState(s =>
                {
                    s._AlertState = new BaseAlertState
                    {
                        HeaderAlert = headerText,
                        MsgAlert = messaggio,
                        CallbackAlertOk = callbackOk,
                        ApriAlertOk = true
                    };
                }, false);
                //}, false); // TODO capire se si riesce a passare a FALSE per evirare il re-render di tutta la pagina.
            });
        }

        public void ApriConfirm(string messaggio, string headerText = "", Action callbackOk = null, Action callbackKo = null)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SetState(s =>
                {
                    s._ConfirmState = new BaseAlertState
                    {
                        HeaderAlert = headerText,
                        MsgAlert = messaggio,
                        CallbackAlertOk = callbackOk,
                        CallbackAlertKo = callbackKo,
                        ApriAlertOk = true
                    };
                });
                //}, false); // TODO capire se si riesce a passare a FALSE per evirare il re-render di tutta la pagina.
            });
        }

        public override VisualNode Render()
        {
            throw new NotImplementedException();
        }

        public Grid BaseRender(IList<VisualNode> visualNodes)
        {
            Grid container = new();

            foreach (VisualNode node in visualNodes)
            {
                container.Add(node);
            }

            // Creo il popup del loading
            VolosLoading loading = new();
            loading.IsOpen(() => State.IsBusy);
            loading.OnClosed(() => State.IsBusy = false);
            container.Add(loading);

            // Creo il popup dell'alert
            container.Add(new VolosPopUp()
                .ShowCloseButton(false)
                .SetHeaderText(() => string.IsNullOrEmpty(State._AlertState.HeaderAlert) ? "Warning" : State._AlertState.HeaderAlert)
                .SetFooterTemplate(() =>
                    new VStack {
                        new BoxView()
                            .WidthRequest(999).Margin(0)
                            .HeightRequest(1).Color(Colors.Gray),
                        new Grid("*", "0.3*,*,0.3*") {
                            new Button("OK")
                                .GridRow(0).GridColumn(1)
                                .HeightRequest(40).Margin(new Thickness(0,4,0,0))
                                .OnClicked(async () => {
                                    if (State._AlertState.CallbackAlertOk != null) {
                                        var _cb = State._AlertState.CallbackAlertOk; // salvo il cb perchè devo chiudere prima il popup e nel closing si resetta tutto.
                                        SetState(s => s._AlertState.ApriAlertOk = false, false);
                                        await Task.Delay(350); // --> https://support.syncfusion.com/support/tickets/474868
                                        _cb?.Invoke();
                                    } else {
                                        SetState(s => s._AlertState.ApriAlertOk = false, false);
                                    }
                                })
                        }
                    }.HCenter().VCenter().Spacing(0).Padding(0))
                .SetText(() => State._AlertState.MsgAlert)
                .IsOpen(() => State._AlertState.ApriAlertOk)
                .OnClosing(() =>
                {
                    SetState(s =>
                    {
                        s._AlertState = new BaseAlertState
                        {
                            HeaderAlert = "",
                            MsgAlert = "",
                            CallbackAlertOk = null,
                            ApriAlertOk = false
                        };
                    }, false);
                })
            );

            SetState(s => s.PaginaCaricata = true);
            return container;
        }
    }

    public class VolosComponent<T> : VolosComponent<T, EmptyProps> where T : BaseState, new()
    {
    }
}
