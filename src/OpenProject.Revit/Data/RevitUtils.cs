using System;
using Autodesk.Revit.DB;
using OpenProject.Shared.Math3D;
using decMath = DecimalMath.DecimalEx;

namespace OpenProject.Revit.Data
{
  public static class RevitUtils
  {
    public static Position TransformCameraPosition(
      ProjectPositionWrapper projectBase,
      Position position,
      bool reverse = false)
    {
      var i = reverse ? -1 : 1;

      Vector3 translation = projectBase.GetTranslation() * i;
      var rotation = i * projectBase.Angle;

      Vector3 center = reverse
        ? new Vector3(
          position.Center.X + translation.X,
          position.Center.Y + translation.Y,
          position.Center.Z + translation.Z)
        : position.Center;

      var centerX = center.X * decMath.Cos(rotation) - center.Y * decMath.Sin(rotation);
      var centerY = center.X * decMath.Sin(rotation) + center.Y * decMath.Cos(rotation);

      Vector3 newCenter = reverse
        ? new Vector3(centerX, centerY, center.Z)
        : new Vector3(centerX + translation.X, centerY + translation.Y, center.Z + translation.Z);

      var forwardX = position.Forward.X * decMath.Cos(rotation) -
                     position.Forward.Y * decMath.Sin(rotation);
      var forwardY = position.Forward.X * decMath.Sin(rotation) +
                     position.Forward.Y * decMath.Cos(rotation);
      var newForward = new Vector3(forwardX, forwardY, position.Forward.Z);

      var upX = position.Up.X * decMath.Cos(rotation) - position.Up.Y * decMath.Sin(rotation);
      var upY = position.Up.X * decMath.Sin(rotation) + position.Up.Y * decMath.Cos(rotation);
      var newUp = new Vector3(upX, upY, position.Up.Z);

      return new Position(newCenter, newForward, newUp);
    }

    public static ViewOrientation3D ToViewOrientation3D(this Position position) =>
      new(position.Center.ToRevitXyz(),
        position.Up.ToRevitXyz(),
        position.Forward.ToRevitXyz());

    public static XYZ ToRevitXyz(this Vector3 vec) =>
      new(Convert.ToDouble(vec.X),
        Convert.ToDouble(vec.Y),
        Convert.ToDouble(vec.Z));

    public static Vector3 ToVector3(this XYZ vec) => new(vec.X.ToDecimal(), vec.Y.ToDecimal(), vec.Z.ToDecimal());

    public static (double viewBoxHeight, double viewBoxWidth) ConvertToViewBoxValues(
      XYZ topRight, XYZ bottomLeft, XYZ right)
    {
      XYZ diagonal = topRight.Subtract(bottomLeft);
      var distance = topRight.DistanceTo(bottomLeft);
      var angleBetweenBottomAndDiagonal = diagonal.AngleTo(right);

      var height = distance * Math.Sin(angleBetweenBottomAndDiagonal);
      var width = distance * Math.Cos(angleBetweenBottomAndDiagonal);

      return (height, width);
    }

    public static double ToMeters(this double internalUnits)
    {
      return UnitUtils.ConvertFromInternalUnits(internalUnits, UnitTypeId.Meters);
    }

    public static double ToInternalRevitUnit(this double meters)
    {
      return UnitUtils.ConvertToInternalUnits(meters, UnitTypeId.Meters);
    }

    public static Vector3 ToMeters(this Vector3 vec)
    {
      var x = vec.X.IsFinite() ? Convert.ToDouble(vec.X).ToMeters().ToDecimal() : vec.X;
      var y = vec.Y.IsFinite() ? Convert.ToDouble(vec.Y).ToMeters().ToDecimal() : vec.Y;
      var z = vec.Z.IsFinite() ? Convert.ToDouble(vec.Z).ToMeters().ToDecimal() : vec.Z;

      return new Vector3(x, y, z);
    }

    public static Vector3 ToInternalUnits(this Vector3 vec)
    {
      var x = vec.X.IsFinite() ? Convert.ToDouble(vec.X).ToInternalRevitUnit().ToDecimal() : vec.X;
      var y = vec.Y.IsFinite() ? Convert.ToDouble(vec.Y).ToInternalRevitUnit().ToDecimal() : vec.Y;
      var z = vec.Z.IsFinite() ? Convert.ToDouble(vec.Z).ToInternalRevitUnit().ToDecimal() : vec.Z;

      return new Vector3(x, y, z);
    }

    public static Position ToMeters(this Position pos) =>
      new(pos.Center.ToMeters(), pos.Forward.ToMeters(), pos.Up.ToMeters());

    public static Position ToInternalUnits(this Position pos) =>
      new(pos.Center.ToInternalUnits(), pos.Forward.ToInternalUnits(), pos.Up.ToInternalUnits());
  }
}
