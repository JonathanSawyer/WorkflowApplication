﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdemWokflow.Service
{
    public interface IEntityService<T> //add constraint
    {
        IList<T> List();
        void Save(T item);
        void Delete(int id);
        T Get(int id);
    }
}
