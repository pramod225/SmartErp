using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YuktiSolutions.SmartERP.Areas.Sales.Models.Database;

namespace YuktiSolutions.SmartERP.Areas.Sales.Models.UI
{
    public class MasterEntriesListItem
    {

        public Guid ID { get; set; }

        public string Name { get; set; }

        public string ApiKey { get; set; }

        public MasterEntryType MasterEntryType { get; set; }

    }
}