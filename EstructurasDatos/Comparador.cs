﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructurasDatos
{
    public interface Comparador
    {
        bool igualQue(Object q);
        bool menorQue(Object q);
        bool mayorQue(Object q);
    }
}
