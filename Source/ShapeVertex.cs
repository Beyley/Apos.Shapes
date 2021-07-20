using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apos.Shapes {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VertexShape : IVertexType {
        public VertexShape(Vector3 position, Vector2 textureCoordinate, float shape, Color c1, Color c2, float thickness = 0f, float pixelSize = 1f, float width = 1.0f) {
            if (thickness <= 0f) {
                c2 = c1;
                thickness = 0f;
            }

            Position = position;
            TextureCoordinate = textureCoordinate;
            Color1 = c1;
            Color2 = c2;

            Meta = new Vector4(thickness, shape, pixelSize, width);
        }

        public Vector3 Position;
        public Vector2 TextureCoordinate;
        public Color Color1;
        public Color Color2;
        public Vector4 Meta;
        public static readonly VertexDeclaration VertexDeclaration;

        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;

        public override int GetHashCode() {
            unchecked {
                var hashCode = Position.GetHashCode();
                hashCode = (hashCode * 397) ^ TextureCoordinate.GetHashCode();
                hashCode = (hashCode * 397) ^ Color1.GetHashCode();
                hashCode = (hashCode * 397) ^ Color2.GetHashCode();
                hashCode = (hashCode * 397) ^ Meta.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() {
            return
                "{{Position:" + this.Position +
                " Color1:" + this.Color1 +
                " Color2:" + this.Color1 +
                " TextureCoordinate:" + this.TextureCoordinate +
                " Thickness:" + this.Meta.X +
                " Shape:" + this.Meta.Y +
                " PixelSize:" + this.Meta.Z +
                " Width:" + this.Meta.W +
                "}}";
        }

        public static bool operator ==(VertexShape left, VertexShape right) {
            return
                left.Position == right.Position &&
                left.TextureCoordinate == right.TextureCoordinate &&
                left.Color1 == right.Color1 &&
                left.Color2 == right.Color2 &&
                left.Meta == right.Meta;
        }

        public static bool operator !=(VertexShape left, VertexShape right) {
            return !(left == right);
        }

        public override bool Equals(object obj) {
            if (obj == null)
                return false;

            if (obj.GetType() != base.GetType())
                return false;

            return (this == ((VertexShape)obj));
        }

        static VertexShape() {
            var elements = new VertexElement[] {
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
                new VertexElement(sizeof(float) * 5, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(sizeof(float) * 5 + sizeof(int), VertexElementFormat.Color, VertexElementUsage.Color, 1),
                new VertexElement(sizeof(float) * 5 + sizeof(int) * 2, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 1)
            };
            VertexDeclaration = new VertexDeclaration(elements);
        }
    }
}