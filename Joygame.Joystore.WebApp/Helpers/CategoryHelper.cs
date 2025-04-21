using Joygame.Joystore.WebApp.Models.Category;

namespace Joygame.Joystore.WebApp.Helpers
{
    public static class CategoryHelper
    {
        public static List<CategoryModel> BuildCategoryTree(List<CategoryModel> flatList)
        {
            var lookup = flatList.ToDictionary(c => c.Id);
            var rootCategories = new List<CategoryModel>();

            foreach (var category in flatList)
            {
                if (category.ParentId == null)
                {
                    rootCategories.Add(category);
                }
                else if (lookup.TryGetValue(category.ParentId.Value, out var parent))
                {
                    parent.Children.Add(category);
                }
            }

            return rootCategories;
        }
    }

}
