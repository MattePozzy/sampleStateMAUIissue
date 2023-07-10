using System;

namespace Project3
{
    public class BaseState
    {
        //public TranslateExtension TS { get; set; } = TranslateExtension.GetTS();
        public bool IsBusy { get; set; } = false;
        public bool PaginaCaricata { get; set; } = false;

        public bool IsLoggedIn { get; set; } = false;

        public string TestoFiltro { get; set; }

        #region non usare direttamente
        public BaseAlertState _AlertState { get; set; } = new();
        public BaseAlertState _ConfirmState { get; set; } = new();
        #endregion
    }

    public class BaseAlertState
    {
        public string MsgAlert { get; set; } = "";
        public string HeaderAlert { get; set; } = "";
        public Action CallbackAlertOk { get; set; } = null;
        public Action CallbackAlertKo { get; set; } = null;
        public bool ApriAlertOk { get; set; } = false;
    }
}
