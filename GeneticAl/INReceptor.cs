using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thinker
{
    public interface INeuronReceptor
    {
        Dictionary<INeuronSignal, NeuralFactor> Input { get; }
    }
}
