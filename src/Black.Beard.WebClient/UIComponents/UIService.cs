using Bb.ComponentModel.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bb.WebClient.UIComponents
{

    public class UIService
    {


        public UIService()
        {
            _menus = new Dictionary<string, List<UIComponent>>();
        }


        public Task<List<UIComponent>> GetUI(string name)
        {

            if (_menus.TryGetValue(name, out var block))
                return Task.FromResult(block);

            return Task.FromResult(new List<UIComponent>());

        }


        public UIComponent? Resolve(Guid id, UIComponent? value = null)
        {

            if (value != null)
            {

                if (value.Uuid == id)
                    return value;

                foreach (var item in value.Children)
                {
                    var block = Resolve(id, item);
                    if (block != null)
                        return block;
                }
            }

            else
            {

                foreach (var item in _menus)
                    foreach (var item2 in item.Value)
                    {
                        var block = Resolve(id, item2);
                        if (block != null)
                            return block;
                    }

            }

            return null;

        }


        public UIComponentMenu GetMenuOrCreate(string name, Guid? guid, TranslatedKeyLabel? label = null)
        {

            if (!guid.HasValue)
                guid = Guid.Empty;

            if (!_menus.TryGetValue(name, out var listBlock))
                _menus.Add(name, listBlock = new List<UIComponent>());

            UIComponent? block = listBlock.FirstOrDefault(c => c.Uuid == guid.Value);

            if (block == null)
            {
                listBlock.Add(block = new UIComponentMenu(guid)
                {
                    Display = label,
                    Type = UIComponent.Types.Menu,
                });
            }

            return (UIComponentMenu)block;

        }

        public UIComponentMenu? GetMenu(string name, Guid? guid)
        {

            if (!guid.HasValue)
                guid = Guid.Empty;

            if (!_menus.TryGetValue(name, out var listBlock))
                _menus.Add(name, listBlock = new List<UIComponent>());

            UIComponent? block = listBlock.FirstOrDefault(c => c.Uuid == guid.Value);

            if (block is UIComponentMenu menu)
                return menu;

            return null;

        }

        public static string LeftMenu { get; } = "LeftMenu";
        public static string TopMenu { get; } = "TopMenu";


        private readonly Dictionary<string, List<UIComponent>> _menus;

        public static class Guids
        {

            public static Guid Configurations { get; } = new Guid("{0F368852-6097-4FD7-B060-26367A4852B1}");
            public static Guid Translations { get; } = new Guid("{46839164-66C6-4C04-BC63-387132DD961C}");

            public static Guid Home { get; } = new Guid("{2E1273B0-35F1-43AD-9E24-572B73BAAB90}");
            public static Guid Languages { get; } = new Guid("{F1349EB1-BCF7-40AD-B2DF-59025F3D0493}");

        }

    }


}
