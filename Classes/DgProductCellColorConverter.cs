using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SmallStore
{
    class DgProductCellColorConverter
    {
        public object Convert(object value, Type targetType,
           object parameter, Product p)
        {
            switch ((string)value)
            {
                case "$":

                    return new SolidColorBrush(Colors.Blue);

                    break;

                case "*":

                    return new SolidColorBrush(Colors.Green);

                    break;

                default:

                    return SystemColors.AppWorkspaceColor;

                    break;
            }

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, Product p)
        {
            // Do the conversion from color to value
            throw new System.NotImplementedException();

        }
    }
}
