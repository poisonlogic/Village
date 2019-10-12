using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Village.Core.Items
{
    public class BaseItemFilter : IItemFilter
    {
        private readonly string TaxonomySeparator = @"\";

        private ItemFilterConfig _config;

        public ItemFilterConfig FilterConfig => _config;
        public string FilterId { get; }

        public BaseItemFilter(ItemFilterConfig config)
        {
            FilterId = Guid.NewGuid().ToString();
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public bool CanAcceptItemOfDef(ItemDef itemDef)
        {
            if (_config.ItemDefNames?.Any() ?? false)
                if (_config.ItemDefNames.Contains(itemDef.DefName))
                    return _config.WhiteList;

            if (_config.Taxonomies?.Any() ?? false)
                if (DoTaxFiltersContainItemTax(_config.Taxonomies, itemDef.Taxonomy))
                    return _config.WhiteList;

            return !_config.WhiteList;
        }

        public bool CanAcceptItem(IItemInstance item)
        {
            return CanAcceptItemOfDef(item.ItemDef);
        }

        public string GetLable()
        {
            throw new NotImplementedException();
        }

        private bool DoTaxFiltersContainItemTax(List<string> filterTaxes, string itemTax)
        {
            foreach (var filterTax in filterTaxes)
                if (CompairTax(filterTax, itemTax))
                    return true;

            return false;
        }

        private bool CompairTax(string filterTax, string itemTax)
        {
            //var fTokens = filterTax.Split(TaxonomySeparator);
            //var iTokens = itemTax.Split(TaxonomySeparator);

            return itemTax.StartsWith(filterTax);

        }
    }
}
