using Krankenkassen.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krankenkassen.DataTemplates
{
    internal class Selector : DataTemplateSelector
    {
        /// <summary>
        /// Rückgabe einer Ansicht an die CollectionView
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            CsvLineModel model = null;
            try
            {
                model = item as CsvLineModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new Template(model);
        }
        
    }
}
