using System.ComponentModel.Composition;
using Gemini.Demo.Modules.FilterDesigner.Commands;
using Gemini.Framework.Menus;

namespace Gemini.Demo.Modules.FilterDesigner
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemGroupDefinition FileOpenGraphMenuGroup = new MenuItemGroupDefinition(
            Gemini.Modules.Shell.MenuDefinitions.FileOpenMenuItem, 0);

        [Export]
        public static MenuItemDefinition OpenGraphMenuItem = new CommandMenuItemDefinition<OpenGraphCommandDefinition>(
            FileOpenGraphMenuGroup, 0);
    }
}
