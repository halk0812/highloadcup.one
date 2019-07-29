using System;
using System.Collections.Generic;
using System.Text;

namespace OptionsService
{
    public interface IOptionProvider
    {
        void SetGeneratedTime(Int32 generatedTime);
        Int32 GetGeneratedTime();
        void SetTypeLoad(int typeLoad);
        int GetTypeLoad();
    }
}
