﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thinker
{
    public interface INeuralLayer : IList<INeuron>
    {
        void Pulse(INeuralNet net);
        void ApplyLearning(INeuralNet net);
        void InitializeLearning(INeuralNet net); 
    }
}
