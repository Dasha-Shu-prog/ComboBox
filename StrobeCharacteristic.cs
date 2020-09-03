using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Table1
{
    public class StrobeCharacteristic
    {
        public int Id { set; get; }
        public int Time_start { set; get; }
        public int Time_stop { set; get; }
        public int Amplitude { set; get; }
        public string Color { set; get; }

        internal IEnumerable<StrobeCharacteristic> Distinct()
        {
            throw new NotImplementedException();
        }
    }
}
