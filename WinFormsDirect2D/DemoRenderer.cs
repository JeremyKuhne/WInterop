using System.Collections.Generic;
using System.Collections.Immutable;
using WInterop.Winforms;

namespace WinFormsDirect2D
{
    internal partial class DemoRenderer
    {
        private readonly ImmutableDictionary<string, IRenderItem> _demos;

        public DemoRenderer(ID2DFactoryProvider factoryProvider)
        {
            var temp = new Dictionary<string, IRenderItem>()
            {
                { 
                  nameof(SimpleTestPicture), 
                  new SimpleTestPicture() { D2DFactoryProvider = factoryProvider }
                },
                {
                  nameof(HelloWorld),
                  new HelloWorld() { D2DFactoryProvider = factoryProvider }
                },

            };

            _demos = temp.ToImmutableDictionary();
        }

        public ImmutableDictionary<string, IRenderItem> Demos
            => _demos;
    }
}
