using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ErrorLog : Entity<int>
    {
        public virtual string Stack { get; set; }
        public virtual string ControllerContext { get; set; }
        public virtual DateTime ErrorDateTime { get; set; }
        public ErrorLog()
        {
            ErrorDateTime = DateTime.Now;
        }   
    }
}
