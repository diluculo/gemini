using Caliburn.Micro;
using Gemini.Demo.Properties;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Modules.Shell.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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
            var _shell = this as IShell;
            var notYetSavedDocuments = _shell.Documents.OfType<PersistedDocument>().Where(x => !x.IsNew && x.IsDirty).ToList();
            yield return new DialogResult() { DirtyDocuments = notYetSavedDocuments };
        }

        private class DialogResult : IResult
        {
            public List<PersistedDocument> DirtyDocuments { get; set; }

            public event EventHandler<ResultCompletionEventArgs> Completed;

            public async void Execute(CoroutineExecutionContext context)
            {
                MessageBoxResult result;

                if (DirtyDocuments != null && DirtyDocuments.Count > 0)
                {
                    var files = DirtyDocuments.Select(x => x.FilePath).ToList();
                    result = Gemini.Modules.DialogManager.SaveFilesPrompt.Show(files);
                    if (result == MessageBoxResult.Yes)
                    {
                        foreach (var document in DirtyDocuments)
                        {
                            await document.Save(document.FilePath);
                        }
                        Completed(this, new ResultCompletionEventArgs { WasCancelled = false });
                        return;
                    }
                    else if (result == MessageBoxResult.Cancel)
                    {
                        Completed(this, new ResultCompletionEventArgs { WasCancelled = true });
                        return;
                    }
                }

                result = MessageBoxResult.Yes;
                if (Settings.Default.ConfirmExit)
                {
                    result = Gemini.Modules.DialogManager.MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButton.YesNo);                    
                }

                Completed(this, new ResultCompletionEventArgs { WasCancelled = result != MessageBoxResult.Yes });
            }
        }
    }
}
