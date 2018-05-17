using Caliburn.Micro;
using Gemini.Demo.Properties;
using Gemini.Framework.Services;
using Gemini.Modules.Shell.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;

namespace Gemini.Demo.Modules.Shell.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Gemini.Modules.Shell.ViewModels.ShellViewModel
    {
        static ShellViewModel()
        {
            ViewLocator.AddNamespaceMapping(typeof(ShellViewModel).Namespace, typeof(ShellView).Namespace);
        }

        public override void CanClose(Action<bool> callback)
        {
            Coroutine.BeginExecute(CanClose().GetEnumerator(), null, (s, e) => callback(!e.WasCancelled));
        }

        private IEnumerable<IResult> CanClose()
        {
            yield return new MessageBoxResult();
        }

        private class MessageBoxResult : IResult
        {
            public event EventHandler<ResultCompletionEventArgs> Completed;

            public void Execute(CoroutineExecutionContext context)
            {
                var result = System.Windows.MessageBoxResult.Yes;

                List<string> files = new List<string>() { "aaa", "bbb", "ccc" };
                result = Gemini.Modules.DialogManager.SaveFilesPrompt.Show(files);

                if (Settings.Default.ConfirmExit)
                {
                    result = Gemini.Modules.DialogManager.MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButton.YesNo);
                    //result = Gemini.Modules.DialogManager.MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Error, System.Windows.MessageBoxResult.Cancel);
                }

                Completed(this, new ResultCompletionEventArgs { WasCancelled = (result != System.Windows.MessageBoxResult.Yes) });
            }
        }
    }
}
