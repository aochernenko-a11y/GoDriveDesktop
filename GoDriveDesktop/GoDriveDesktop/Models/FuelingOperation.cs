using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoDriveDesktop.Models
{
    public enum ColumnState
    {
        Free,
        Fueling,
        WaitingNozzleBack,
        WaitingPayment
    }
    public class FuelingOperation
    {
        public int ColumnId { get; set; }
        public string FuelName { get; set; } = "";
        public decimal CurrentValue { get; set; }
        public decimal TargetValue { get; set; }
        public decimal FinalLiters { get; set; }
        public bool IsCountdownMode { get; set; }
        public ColumnState State { get; set; } = ColumnState.Free;
        public RoundedButton Button { get; set; }
    }

}
