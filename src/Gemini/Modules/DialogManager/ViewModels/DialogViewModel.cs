using Caliburn.Micro;

namespace Gemini.Modules.DialogManager.ViewModels
{
    /// <summary>
    /// DialogViewModel class which should be inherited for all view 
    /// model that want to be displayed as metro dialogs.
    /// </summary>
    public abstract class DialogViewModel : Screen, IDialogViewModel
    {
        /// <summary>
        /// Deafult constructor.
        /// </summary>
        protected DialogViewModel()
        { }

        /// <summary>
        /// Close the dialog.
        /// </summary>
        public void Close()
        {
            TryClose(true);
        }
    }

    public abstract class DialogViewModel<IResult> : Screen, IDialogViewModel
    {
        public IResult DialogResult { get; private set; }

        /// <summary>
        /// Deafult constructor.
        /// </summary>
        public DialogViewModel()
        { }

        public void Close()
        {
            TryClose(true);
        }

        /// <summary>
        /// Close the dialog.
        /// </summary>
        public void Close(IResult result)
        {
            DialogResult = result;
            TryClose(true);
        }

        public void SetDefaultResult(IResult defaultResult)
        {
            DialogResult = defaultResult;
        }
    }
}
