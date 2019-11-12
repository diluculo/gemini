using Caliburn.Micro;
using Gemini.Framework.Services;
using Gemini.Modules.MainWindow.ViewModels;
using Gemini.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gemini.Modules.DialogManager.ViewModels
{
    [Export(typeof(SaveFilesPromptViewModel))]
    public class SaveFilesPromptViewModel : DialogViewModel<MessageBoxResult>
    {
        #region Initialisation.

        public SaveFilesPromptViewModel()
        {
            this.SetDefaultResult(MessageBoxResult.Cancel);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="title">The title of the message box dialog.</param>
        /// <param name="targetFiles">Target FileNames shown in the message box.</param>
        public SaveFilesPromptViewModel(string message, IEnumerable<DialogTreeViewModel> targetFiles)
        {
            this.Title = IoC.Get<IMainWindow>().Title;
            this.MessageBoxText = message;
            this.TargetFiles = targetFiles;
            this.SetDefaultResult(MessageBoxResult.Cancel);
        }

        /// <summary>
        /// Overloaded.
        /// </summary>
        /// <param name="title">The title of the message box dialog.</param>
        /// <param name="message">The message to display in the message box.</param>
        /// <param name="targetFiles">Target FileNames shown in the message box.</param>
        public SaveFilesPromptViewModel(string title, string message, IEnumerable<DialogTreeViewModel> targetFiles)
        {
            this.Title = title;
            this.MessageBoxText = message;
            this.TargetFiles = targetFiles;
            this.SetDefaultResult(MessageBoxResult.Cancel);
        }

        /// <summary>
        /// Overloaded.
        /// </summary>
        /// <param name="targetFiles">Target FileNames shown in the message box.</param>
        public SaveFilesPromptViewModel(IEnumerable<DialogTreeViewModel> targetFiles)
        {
            this.Title = IoC.Get<IMainWindow>().Title;
            this.TargetFiles = targetFiles;
            this.SetDefaultResult(MessageBoxResult.Cancel);
        }

        #endregion // Initialisation.

        #region Class Methods.

        /// <summary>
        /// Handles the button click events for the yes button.
        /// </summary>
        public void YesButtonClick()
        {
            Close(MessageBoxResult.Yes);
        }

        /// <summary>
        /// Handles the button click events for the no button.
        /// </summary>
        public void NoButtonClick()
        {
            Close(MessageBoxResult.No);
        }

        /// <summary>
        /// Handles the button click events for the cancel button.
        /// </summary>
        public void CancelButtonClick()
        {
            Close(MessageBoxResult.Cancel);
        }

        #endregion // Class Methods.

        #region Properties.

        /// <summary>
        /// Hold the dialog title.
        /// </summary>
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title == value)
                    return;
                title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        /// <summary>
        /// Hold the text for the dialog body.
        /// </summary>
        private string messageBoxText = Resources.SaveChangesMessage;
        public string MessageBoxText
        {
            get { return messageBoxText; }
            set
            {
                if (messageBoxText == value)
                    return;
                messageBoxText = value;
                NotifyOfPropertyChange(() => MessageBoxText);
            }
        }

        private IEnumerable<DialogTreeViewModel> targetFiles;
        public IEnumerable<DialogTreeViewModel> TargetFiles
        {
            get { return targetFiles; }
            set
            {
                if (targetFiles == value)
                    return;
                targetFiles = value;
                NotifyOfPropertyChange(() => TargetFiles);
            }
        }

        /// <summary>
        /// Yes button text. Default Yes.
        /// </summary>
        private string yesButtonText = Resources.YesButtonText;
        public string YesButtonText
        {
            get { return yesButtonText; }
            set
            {
                if (yesButtonText == value)
                    return;
                yesButtonText = value;
                NotifyOfPropertyChange(() => YesButtonText);
            }
        }

        /// <summary>
        /// No button text to use.
        /// </summary>
        private string noButtonText = Resources.NoButtonText;
        public string NoButtonText
        {
            get { return noButtonText; }
            set
            {
                if (noButtonText == value)
                    return;
                noButtonText = value;
                NotifyOfPropertyChange(() => NoButtonText);
            }
        }
        
        /// <summary>
        /// Cancel button text.
        /// </summary>
        private string cancelButtonText = Resources.CancelButtonText;
        public string CancelButtonText
        {
            get { return cancelButtonText; }
            set
            {
                if (cancelButtonText == value)
                    return;
                cancelButtonText = value;
                NotifyOfPropertyChange(() => CancelButtonText);
            }
        }
        
        #endregion // Properties.
    }
}
