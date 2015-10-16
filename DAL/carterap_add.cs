using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class carterap
    {
        public long VigenciaPeriodo {
            get {
                return long.Parse(this.vigencia.ToString() + this.periodo.ToString().PadLeft(2, '0'));
            }
        }
    }
}
