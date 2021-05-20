using MvcControlsToolkit.Blazor.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KernelApp.Client
{
    public class MenuService
    {
        public static MenuService Instance = new MenuService();
        private readonly Dictionary<string, List<PageRole>>
            segmentedItems = new Dictionary<string, List<PageRole>>();
        private readonly HashSet<string>
            onRequestOnlyMenus = new HashSet<string>();
        public void DeclareOnRequest(IEnumerable<string> onRequest)
        {
            onRequestOnlyMenus.UnionWith(onRequest);
        }
        public event Func<string, List<PageRole>, Task> MenuChanged;
        public List<PageRole> Get(string role)
        {
            if (segmentedItems.TryGetValue(role, out var res))
                return res;
            else
                return new List<PageRole>();
        }
        public async Task Add(string role, List<PageRole> newItems)
        {
            var allItems = newItems;
            if (segmentedItems.TryGetValue(role, out var res))
            {
                res.AddRange(newItems);
                allItems = res;
            }
            else segmentedItems[role] = newItems;
            if (MenuChanged != null && ! onRequestOnlyMenus.Contains(role))
                await MenuChanged(role, allItems);
        }
        public async Task Add(ModuleExports exports)
        {
            if (exports?.Pages == null) return;
             foreach(var role in exports.Pages
                 .GroupBy(m => m.Role))
            {
                await Add(role.Key, role.ToList());
            }
                
        }
        public async Task Require(string role)
        {
            var items = Get(role);
            if (items != null && MenuChanged != null)
                await MenuChanged(role, items);
        }
    }
}
