using System;
using System.Collections.Generic;

namespace IndependentWork22
{
    public interface IComponent
    {
        int GetWordCount();
        void Display(int indent = 0); 
    }
}