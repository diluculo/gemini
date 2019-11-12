namespace Gemini.Modules.DialogManager.ViewModels
{
    /// <summary>
    /// Interface that governs whether a ViewModel.View can 
    /// be shown/displayed as a metro modal dialog screen.
    /// </summary>
    internal interface IDialogViewModel
    {
        void Close();
    }
}
