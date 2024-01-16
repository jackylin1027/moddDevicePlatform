using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FineTekDeivePlatform.ViewModel;
using FineTekDeivePlatform.Models;
namespace FineTekDeivePlatform
{
    public interface ISensor
    {
       void UpdateParameter(EphProduct eph);
    }
}
