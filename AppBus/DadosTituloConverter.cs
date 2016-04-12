using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AppBus
{
    public class DadosTituloConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Dados d = value as Dados;
            return d.Titulo;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return true;
        }
    }

    public class DadosDadosConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Dados d = value as Dados;
            return d.Horas;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DadosStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value as string;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return true;
        }
    }
}
