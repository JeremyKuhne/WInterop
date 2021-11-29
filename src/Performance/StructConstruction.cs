using BenchmarkDotNet.Attributes;
using System.Numerics;
using System.Runtime.CompilerServices;

#pragma warning disable IDE0052 // Remove unread private members
#pragma warning disable IDE0051 // Remove unused private members

namespace Performance
{
    [DisassemblyDiagnoser]
    [MemoryDiagnoser]
    public class StructConstruction
    {
        private Vector2 _vector;
        private Vector4 _vector4;
        private PointF1 _point1;
        private PointF2 _point2;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _vector4 = new Vector4(1970, 1980, 1990, 2000);
            _vector = new Vector2(124, 587);
            _point1 = new PointF1(_vector);
            _point2 = new PointF2(_vector);
        }

        public struct PointF1
        {
            private readonly float _x;
            private readonly float _y;

            public PointF1(Vector2 vector)
            {
                _x = vector.X;
                _y = vector.Y;
            }

            public Vector2 ToVector2() => new(_x, _y);
        }

        public struct PointF2
        {
            private readonly float _x;
            private readonly float _y;

            public PointF2(Vector2 vector)
            {
                this = Unsafe.As<Vector2, PointF2>(ref vector);
            }

            public Vector2 ToVector2() => Unsafe.As<PointF2, Vector2>(ref this);
        }

        public struct RectangleF1
        {
            private readonly float _x;
            private readonly float _y;
            private readonly float _width;
            private readonly float _height;

            public RectangleF1(Vector4 vector)
            {
                _y = vector.Y;
                _x = vector.X;
                _width = vector.Z;
                _height = vector.W;
            }
        }

        public struct RectangleF2
        {
            private readonly float _x;
            private readonly float _y;
            private readonly float _width;
            private readonly float _height;

            public RectangleF2(Vector4 vector)
            {
                this = Unsafe.As<Vector4, RectangleF2>(ref vector);
            }
        }

        [Benchmark()]
        public RectangleF1 AssignEach()
        {
            return new RectangleF1(_vector4);
        }

        [Benchmark()]
        public RectangleF2 AssignUnsafe()
        {
            return new RectangleF2(_vector4);
        }

        //[Benchmark(Baseline = true)]
        //public Vector2 ToVector2()
        //{
        //    return _point1.ToVector2();
        //}

        //[Benchmark()]
        //public Vector2 ToVector2Unsafe()
        //{
        //    return _point2.ToVector2();
        //}
    }
}

#pragma warning restore IDE0052 // Remove unread private members
#pragma warning restore IDE0051 // Remove unused private members
