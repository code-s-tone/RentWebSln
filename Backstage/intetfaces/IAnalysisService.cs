using Backstage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.intetfaces
{
    interface IAnalysisService
    {
        public IEnumerable<SalesAnalysis> GetSalesData();

    }
}
