using Gemini.Modules.DialogManager.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace Gemini.Modules.DialogManager
{
    public class SaveFilesPrompt
    {
        public static MessageBoxResult Show(string message, string caption, List<string> targetFiles)
        {
            if (targetFiles == null || targetFiles.Count == 0)
                return MessageBoxResult.Cancel;

            var files = new List<DialogTreeViewModel>();
            foreach (var item in targetFiles)
            {
                var file = new DialogTreeViewModel() { Name = item };
                files.Add(file);
            }

            var viewModel = new SaveFilesPromptViewModel(caption, message, files);
            var result = new DialogManager().Show<MessageBoxResult>(viewModel);
            return result;
        }

        public static MessageBoxResult Show(List<string> targetFiles)
        {
            if (targetFiles == null || targetFiles.Count == 0)
                return MessageBoxResult.Cancel;

            var files = new List<DialogTreeViewModel>();
            foreach (var item in targetFiles)
            {
                var file = new DialogTreeViewModel() { Name = item };
                files.Add(file);
            }

            var viewModel = new SaveFilesPromptViewModel(files);
            var result = new DialogManager().Show<MessageBoxResult>(viewModel);
            return result;
        }
    }
}
