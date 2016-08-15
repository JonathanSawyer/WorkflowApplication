using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserData
    {
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public UserData()
        { }
        public void Set(User user)
        {
            Surname = user.Surname;
            Name = user.Name;
        }

        public void SetOwner(User owner)
        {
            owner.Surname = Surname;
            owner.Name = Name;
        }
    }
}
