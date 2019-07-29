using System;

namespace OptionsService
{
    public class OptionProvider : IOptionProvider
    {
        private Int32 _generatedTime;
        private int _typeLoad;
        public int GetGeneratedTime()
        {
            return _generatedTime;
        }

        public int GetTypeLoad()
        {
            return _typeLoad;
        }

        public void SetGeneratedTime(int generatedTime)
        {
            _generatedTime = generatedTime;
            Console.WriteLine($"{generatedTime}");
        }

        public void SetTypeLoad(int typeLoad)
        {
            _typeLoad = typeLoad;
            Console.WriteLine($"{typeLoad}");

        }
    }
}
