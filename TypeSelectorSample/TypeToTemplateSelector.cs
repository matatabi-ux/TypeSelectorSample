using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace TypeSelectorSample
{
    /// <summary>
    /// Item template selector from data item type
    /// </summary>
    [ContentProperty(Name = "Templates")]
    public class TypeToTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Select target templates
        /// </summary>
        public TypeToTemplateCollection Templates { get; private set; } = new TypeToTemplateCollection();

        /// <summary>
        /// Content property path
        /// </summary>
        public string ContentPath { get; set; }

        /// <summary>
        /// Select template from item
        /// </summary>
        /// <param name="item">data item</param>
        /// <param name="container">item container view</param>
        /// <returns>selected item template</returns>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item == null)
            {
                return default(DataTemplate);
            }

            // Get content by route down view model tree
            var content = item;
            if (!string.IsNullOrEmpty(this.ContentPath))
            {
                var tokens = this.ContentPath.Trim().Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var property in tokens)
                {
                    content = content.GetType().GetProperty(property).GetValue(content);
                }
            }

            var template = (from t in this.Templates
                            where t.TargetType != null
                            && content.GetType().Equals(t.TargetType)
                            select t.Template).FirstOrDefault();

            if (template != null)
            {
                return template;
            }

            // Select a template without specified 'DataType' attribute for default
            template = (from t in this.Templates
                        where t.TargetType == null
                        select t.Template).FirstOrDefault();

            if (template != null)
            {
                return template;
            }

            return base.SelectTemplateCore(item, container);
        }
    }

    /// <summary>
    /// Type convert to template class
    /// </summary>
    [ContentProperty(Name = "Template")]
    public class TypeToTemplate : DependencyObject
    {
        /// <summary>
        /// Data template
        /// </summary>
        public DataTemplate Template { get; set; }

        /// <summary>
        /// Type to DataTemplate
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Type to DataTemplate(Converted)
        /// </summary>
        public Type TargetType
        {
            get { return this.DataType != null ? Type.GetType(this.DataType) : null; }
        }
    }

    /// <summary>
    /// DataTemplate collection for xaml markup
    /// </summary>
    public class TypeToTemplateCollection : List<TypeToTemplate>
    {
        /// <summary>
        /// Constroctor
        /// </summary>
        public TypeToTemplateCollection() : base()
        {
        }
    }
}
