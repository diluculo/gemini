using Caliburn.Micro;
using Gemini.Framework.Services;
using Gemini.Modules.DialogManager.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace Gemini.Modules.DialogManager
{
    /// <summary>
    /// Displays a message box.
    /// </summary>
    public static class MessageBox
    {
        public static MessageBoxResult Show(string message)
        {
            return Show(message, "", MessageBoxButton.OK, MessageBoxImage.None);
        }

        public static MessageBoxResult Show(string message, string caption, MessageBoxButton button)
        {
            return Show(message, caption, button, MessageBoxImage.None);
        }

        public static MessageBoxResult Show(string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return new DialogManager().Show<MessageBoxResult>(new MessageBoxViewModel(caption, message, button, icon));
        }

        public static MessageBoxResult Show(string message, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return new DialogManager().Show<MessageBoxResult>(new MessageBoxViewModel(caption, message, button, icon, defaultResult));
        }
    }
}
