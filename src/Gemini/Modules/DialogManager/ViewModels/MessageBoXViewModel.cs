using Gemini.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Gemini.Modules.DialogManager.ViewModels
{
    /// <summary>
    /// View model for the message box view.
    /// </summary>    
    [Export(typeof(MessageBoxViewModel))]
    public class MessageBoxViewModel : DialogViewModel<MessageBoxResult>
    {
        #region Initialisation.

        public MessageBoxViewModel() { }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="title">The title of the message box dialog.</param>
        /// <param name="message">The message to display in the message box.</param>
        public MessageBoxViewModel(string title, string message)
        {
            Title = title;
            MessageBoxText = message;
            SetDialogVisuals();
            SetDefaultResult(DefaultResult);
        }

        /// <summary>
        /// Overloaded.
        /// </summary>
        /// <param name="title">The title of the message box dialog.</param>
        /// <param name="message">The message to display in the message box.</param>
        /// <param name="settings">MessageBoxSettings for the dialog.</param>
        public MessageBoxViewModel(string title, string message, MessageBoxButton button)
        {
            Title = title;
            MessageBoxText = message;
            Button = button;
            SetDialogVisuals();

            SetDefaultResult(DefaultResult);
        }

        public MessageBoxViewModel(string title, string message, MessageBoxButton button, MessageBoxImage icon)
        {
            Title = title;
            MessageBoxText = message;
            Button = button;
            Icon = icon;
            SetDialogVisuals();

            SetDefaultResult(DefaultResult);
        }

        public MessageBoxViewModel(string title, string message, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            Title = title;
            MessageBoxText = message;
            Button = button;
            Icon = icon;
            
            SetDialogVisuals();            

            DefaultResult = defaultResult;
            SetDefaultResult(DefaultResult);
        }

        #endregion // Initialisation.

        #region Class Methods.
        /// <summary>
        /// Set the dialog visuals based on the MessageBoxSettings.
        /// </summary>
        private void SetDialogVisuals()
        {
            // Set dialog button visibility.
            switch (Button)
            {
                case MessageBoxButton.OK:
                    YesButtonVisibility = Visibility.Collapsed;
                    NoButtonVisibility = Visibility.Collapsed;
                    OKButtonVisibility = Visibility.Visible;
                    CancelButtonVisibility = Visibility.Collapsed;
                    OKButtonAsDefault = true;
                    DefaultResult = MessageBoxResult.OK;
                    break;
                case MessageBoxButton.OKCancel:
                    YesButtonVisibility = Visibility.Collapsed;
                    NoButtonVisibility = Visibility.Collapsed;
                    OKButtonVisibility = Visibility.Visible;
                    CancelButtonVisibility = Visibility.Visible;
                    OKButtonAsDefault = true;
                    CancelButtonAsCancel = true;
                    DefaultResult = MessageBoxResult.Cancel;
                    break;
                case MessageBoxButton.YesNo:
                    YesButtonVisibility = Visibility.Visible;
                    NoButtonVisibility = Visibility.Visible;
                    OKButtonVisibility = Visibility.Collapsed;
                    CancelButtonVisibility = Visibility.Collapsed;
                    YesButtonAsDefault = true;
                    NoButtonAsCancel = false;
                    DefaultResult = MessageBoxResult.No;
                    break;
                case MessageBoxButton.YesNoCancel:
                    YesButtonVisibility = Visibility.Visible;
                    NoButtonVisibility = Visibility.Visible;
                    OKButtonVisibility = Visibility.Collapsed;
                    CancelButtonVisibility = Visibility.Visible;
                    YesButtonAsDefault = true;
                    CancelButtonAsCancel = true;
                    DefaultResult = MessageBoxResult.Cancel;
                    break;
                default:
                    break;
            }

            switch (Icon)
            {
                case MessageBoxImage.Error:
                    {
                        this.ImageSource = new BitmapImage(new
                            Uri("pack://application:,,,/Gemini;component/Resources/Icons/StatusCriticalError_48x.png"));
                        break;
                    }
                case MessageBoxImage.Information:
                    {
                        this.ImageSource = new BitmapImage(new
                            Uri("pack://application:,,,/Gemini;component/Resources/Icons/StatusInformation_48x.png"));
                        break;
                    }
                case MessageBoxImage.Question:
                    {
                        this.ImageSource = new BitmapImage(new
                            Uri("pack://application:,,,/Gemini;component/Resources/Icons/StatusHelp_48x.png"));
                        break;
                    }
                case MessageBoxImage.Warning:
                    {
                        this.ImageSource = new BitmapImage(new
                            Uri("pack://application:,,,/Gemini;component/Resources/Icons/StatusWarning_48x.png"));
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the button click events for the YES button.
        /// </summary>
        public void YesButtonClick()
        {
            Close(MessageBoxResult.Yes);
        }

        /// <summary>
        /// Handles the button click events for the NO button.
        /// </summary>
        public void NoButtonClick()
        {
            Close(MessageBoxResult.No);
        }

        /// <summary>
        /// Handles the button click events for the OK button.
        /// </summary>
        public void OKButtonClick()
        {
            Close(MessageBoxResult.OK);
        }

        /// <summary>
        /// Handles the button click events for the CANCEL button.
        /// </summary>
        public void CancelButtonClick()
        {
            Close(MessageBoxResult.Cancel);
        }

        #endregion // Class Methods.

        #region Settings

        /// <summary>
        /// Sets the button styles to use.
        /// Default is 'OK' and 'Cancel'.
        /// </summary>
        private MessageBoxButton button = MessageBoxButton.OK;
        public MessageBoxButton Button
        {
            get { return button; }
            set { button = value; }
        }

        /// <summary>
        /// Icon that is displayed by a message box.
        /// </summary>
        private MessageBoxImage icon = MessageBoxImage.None;
        public MessageBoxImage Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        /// <summary>
        /// A MessageBoxResult value that specifies the default result of the message box.
        /// </summary>
        private MessageBoxResult defaultResult = MessageBoxResult.OK;
        public MessageBoxResult DefaultResult
        {
            get { return defaultResult; }
            set { defaultResult = value; }
        }

        #endregion Settings

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
        /// ImageSource
        /// </summary>
        private BitmapImage imageSource = null;
        public BitmapImage ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                NotifyOfPropertyChange(() => ImageSource);
            }
        }
                
        /// <summary>
        /// Hold the text for the Message.
        /// </summary>
        private string messageBoxText;
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

        /// <summary>
        /// Yes button text.
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
        /// Visibility for the yes button.
        /// </summary>
        private Visibility yesButtonVisibility = Visibility.Visible;
        public Visibility YesButtonVisibility
        {
            get { return yesButtonVisibility; }
            set
            {
                if (yesButtonVisibility == value)
                    return;
                yesButtonVisibility = value;
                NotifyOfPropertyChange(() => YesButtonVisibility);
            }
        }

        private bool yesButtonAsDefault;
        public bool YesButtonAsDefault
        {
            get { return yesButtonAsDefault; }
            set
            {
                if (yesButtonAsDefault == value)
                    return;
                yesButtonAsDefault = value;
                NotifyOfPropertyChange(() => YesButtonAsDefault);
            }
        }

        /// <summary>
        /// The No button text to use.
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
        /// Visibility for the No button.
        /// </summary>
        private Visibility noButtonVisibility = Visibility.Visible;
        public Visibility NoButtonVisibility
        {
            get { return noButtonVisibility; }
            set
            {
                if (noButtonVisibility == value)
                    return;
                noButtonVisibility = value;
                NotifyOfPropertyChange(() => NoButtonVisibility);
            }
        }

        /// <summary>
        /// IsCancel for the No button
        /// </summary>
        private bool noButtonAsCancel;
        public bool NoButtonAsCancel
        {
            get { return noButtonAsCancel; }
            set
            {
                if (noButtonAsCancel == value)
                    return;
                noButtonAsCancel = value;
                NotifyOfPropertyChange(() => NoButtonAsCancel);
            }
        }

        /// <summary>
        /// OK button text.
        /// </summary>
        private string okButtonText = Resources.OKButtonText;
        public string OKButtonText
        {
            get { return okButtonText; }
            set
            {
                if (okButtonText == value)
                    return;
                okButtonText = value;
                NotifyOfPropertyChange(() => OKButtonText);
            }
        }

        /// <summary>
        /// OK button visibility.
        /// </summary>
        private Visibility okButtonVisibility = Visibility.Collapsed;
        public Visibility OKButtonVisibility
        {
            get { return okButtonVisibility; }
            set
            {
                if (okButtonVisibility == value)
                    return;
                okButtonVisibility = value;
                NotifyOfPropertyChange(() => OKButtonVisibility);
            }
        }

        /// <summary>
        /// IsDefault for the OK button.
        /// </summary>
        private bool okButtonAsDefault = true;
        public bool OKButtonAsDefault
        {
            get { return okButtonAsDefault; }
            set
            {
                if (okButtonAsDefault == value)
                    return;
                okButtonAsDefault = value;
                NotifyOfPropertyChange(() => OKButtonAsDefault);
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

        /// <summary>
        /// Cancel button visibility.
        /// </summary>
        private Visibility cancelButtonVisibility = Visibility.Collapsed;
        public Visibility CancelButtonVisibility
        {
            get { return cancelButtonVisibility; }
            set
            {
                if (cancelButtonVisibility == value)
                    return;
                cancelButtonVisibility = value;
                NotifyOfPropertyChange(() => CancelButtonVisibility);
            }
        }

        /// <summary>
        /// IsCancel for the Cancel button.
        /// </summary>
        private bool cancelButtonAsCancel = true;
        public bool CancelButtonAsCancel
        {
            get { return cancelButtonAsCancel; }
            set
            {
                if (cancelButtonAsCancel == value)
                    return;
                cancelButtonAsCancel = value;
                NotifyOfPropertyChange(() => CancelButtonAsCancel);
            }
        }

        #endregion Properties
    }
}
