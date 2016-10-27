using System.ComponentModel.Composition;
using Gemini.Framework.Menus;
using Gemini.Modules.Shell.Commands;
using Gemini.Properties;

namespace Gemini.Modules.Shell
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition FileNewMenuItem = new TextMenuItemDefinition(
            MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 0, Resources.FileNewCommandText);

        [Export]
        public static MenuItemGroupDefinition FileNewFileMenuGroup = new MenuItemGroupDefinition(
            FileNewMenuItem, 0);

        [Export]
        public static MenuItemDefinition FileNewFileMenuItem = new TextMenuItemDefinition(
            FileNewFileMenuGroup, 100, Resources.FileNewFileCommandText, new System.Uri("pack://application:,,,/Gemini;component/Resources/Icons/NewFile.png"));

        [Export]
        public static MenuItemGroupDefinition FileNewFilesCascadeGroup = new MenuItemGroupDefinition(
            FileNewFileMenuItem, 0);

        [Export]
        public static MenuItemDefinition FileNewMenuItemList = new CommandMenuItemDefinition<NewFileCommandListDefinition>(
            FileNewFilesCascadeGroup, 0);

        [Export]
        public static MenuItemDefinition FileOpenMenuItem = new TextMenuItemDefinition(
            MainMenu.MenuDefinitions.FileNewOpenMenuGroup, 1, Resources.FileOpenCommandText);

        [Export]
        public static MenuItemGroupDefinition FileOpenFileMenuGroup = new MenuItemGroupDefinition(
            FileOpenMenuItem, 100);

        [Export]
        public static MenuItemDefinition FileOpenFileMenuItem = new CommandMenuItemDefinition<OpenFileCommandDefinition>(
            FileOpenFileMenuGroup, 0);

        [Export]
        public static MenuItemDefinition FileCloseMenuItem = new CommandMenuItemDefinition<CloseFileCommandDefinition>(
            MainMenu.MenuDefinitions.FileCloseMenuGroup, 0);

        [Export]
        public static MenuItemDefinition FileSaveMenuItem = new CommandMenuItemDefinition<SaveFileCommandDefinition>(
            MainMenu.MenuDefinitions.FileSaveMenuGroup, 0);

        [Export]
        public static MenuItemDefinition FileSaveAsMenuItem = new CommandMenuItemDefinition<SaveFileAsCommandDefinition>(
            MainMenu.MenuDefinitions.FileSaveMenuGroup, 1);

        [Export]
        public static MenuItemDefinition FileSaveAllMenuItem = new CommandMenuItemDefinition<SaveAllFilesCommandDefinition>(
            MainMenu.MenuDefinitions.FileSaveMenuGroup, 1);

        [Export]
        public static MenuItemDefinition FileExitMenuItem = new CommandMenuItemDefinition<ExitCommandDefinition>(
            MainMenu.MenuDefinitions.FileExitOpenMenuGroup, 0);

        [Export]
        public static MenuItemDefinition WindowDocumentList = new CommandMenuItemDefinition<SwitchToDocumentCommandListDefinition>(
            MainMenu.MenuDefinitions.WindowDocumentListMenuGroup, 0);

        [Export]
        public static MenuItemDefinition ViewFullscreenItem = new CommandMenuItemDefinition<ViewFullScreenCommandDefinition>(
            MainMenu.MenuDefinitions.ViewPropertiesMenuGroup, 0);
    }
}
