using System;

namespace TreeLoc.Transform
{
  public class CoordsConvertor
  {
    private const double _EPS = 1e-4; 

    public double[] JTSKtoWGS84(double x, double y)
    {
      double delta = 5;
      double latitude = 49;
      double longitude = 14;
      double st_EPS = 0;
      do
      {
        double[] jtsk = WGS84toJTSK(latitude - delta, longitude - delta);
        double v1;
        if (jtsk[0] != 0 && jtsk[1] != 0)
        {
          v1 = distPoints(jtsk[0], jtsk[1], x, y);
        }
        else
        {
          v1 = 1e32;
        }

        jtsk = WGS84toJTSK(latitude - delta, longitude + delta);
        double v2;
        if (jtsk[0] != 0 && jtsk[1] != 0)
        {
          v2 = distPoints(jtsk[0], jtsk[1], x, y);
        }
        else
        {
          v2 = 1e32;
        }

        jtsk = WGS84toJTSK(latitude + delta, longitude - delta);
        double v3;
        if (jtsk[0] != 0 && jtsk[1] != 0)
        {
          v3 = distPoints(jtsk[0], jtsk[1], x, y);
        }
        else
        {
          v3 = 1e32;
        }

        jtsk = WGS84toJTSK(latitude + delta, longitude + delta);
        double v4;
        if (jtsk[0] != 0 && jtsk[1] != 0)
        {
          v4 = distPoints(jtsk[0], jtsk[1], x, y);
        }
        else
        {
          v4 = 1e32;
        }

        if ((v1 <= v2) && (v1 <= v3) && (v1 <= v4))
        {
          latitude -= delta / 2;
          longitude -= delta / 2;
        }

        if ((v2 <= v1) && (v2 <= v3) && (v2 <= v4))
        {
          latitude -= delta / 2;
          longitude += delta / 2;
        }

        if ((v3 <= v1) && (v3 <= v2) && (v3 <= v4))
        {
          latitude += delta / 2;
          longitude -= delta / 2;
        }

        if ((v4 <= v1) && (v4 <= v2) && (v4 <= v3))
        {
          latitude += delta / 2;
          longitude += delta / 2;
        }

        delta *= 0.55;
        st_EPS += 4;
      } while (!((delta < 0.00001) || (st_EPS > 1000)));

      double[] r = { latitude, longitude };
      return r;
    }

    public double[] WGS84toJTSK(double latitude, double longitude)
    {
      if ((latitude < 40) || (latitude > 60) || (longitude < 5) || (longitude > 25))
      {
        double[] r = { 0, 0 };
        return r;
      }
      else
      {
        double[] r = WGS84toBessel(latitude, longitude);

        return BesseltoJTSK(r[0], r[1]);
      }
    }

    public double[] WGS84toBessel(double latitude, double longitude, double altitude = 0)
    {
      double B = deg2rad(latitude);
      double L = deg2rad(longitude);
      double H = altitude;

      double[] xyz1 = BLHToGeoCoords(B, L, H);
      double[] xyz2 = transformCoords(xyz1[0], xyz1[1], xyz1[2]);

      _ = geoCoordsToBLH(xyz2[0], xyz2[1], xyz2[2]);

      latitude = rad2deg(B);
      longitude = rad2deg(L);
      //Altitude = H;
      double[] r = { latitude, longitude };
      return r;
    }

    public double[] BesseltoJTSK(double latitude, double longitude)
    {
      double e = 0.081696831215303;
      double n = 0.97992470462083;
      double rho_0 = 12310230.12797036;
      double sinUQ = 0.863499969506341;
      double cosUQ = 0.504348889819882;
      double sinVQ = 0.420215144586493;
      double cosVQ = 0.907424504992097;
      double alfa = 1.000597498371542;
      double k_2 = 1.00685001861538;

      double B = deg2rad(latitude);
      double L = deg2rad(longitude);

      double sinB = Math.Sin(B);
      double t = (1 - e * sinB) / (1 + e * sinB);
      t = Math.Pow(1 + sinB, 2) / (1 - Math.Pow(sinB, 2)) * Math.Exp(e * Math.Log(t));
      t = k_2 * Math.Exp(alfa * Math.Log(t));

      double sinU = (t - 1) / (t + 1);
      double cosU = Math.Sqrt(1 - sinU * sinU);
      double V = alfa * L;
      double sinV = Math.Sin(V);
      double cosV = Math.Cos(V);
      double cosDV = cosVQ * cosV + sinVQ * sinV;
      double sinDV = sinVQ * cosV - cosVQ * sinV;
      double sinS = sinUQ * sinU + cosUQ * cosU * cosDV;
      double cosS = Math.Sqrt(1 - sinS * sinS);
      double sinD = sinDV * cosU / cosS;
      double cosD = Math.Sqrt(1 - sinD * sinD);

      double _EPS = n * Math.Atan(sinD / cosD);
      double rho = rho_0 * Math.Exp(-n * Math.Log((1 + sinS) / cosS));

      double[] r = { rho * Math.Cos(_EPS), rho * Math.Sin(_EPS) };
      return r;
    }

    public double[] BLHToGeoCoords(double B, double L, double H)
    {
      // WGS-84 ellipsoid parameters
      double a = 6378137.0;
      double f_1 = 298.257223563;
      double e2 = 1 - Math.Pow(1 - 1 / f_1, 2);
      double rho = a / Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(B), 2));
      double x = (rho + H) * Math.Cos(B) * Math.Cos(L);
      double y = (rho + H) * Math.Cos(B) * Math.Sin(L);
      double z = ((1 - e2) * rho + H) * Math.Sin(B);

      double[] r = { x, y, z };
      return r;
    }

    public double[] geoCoordsToBLH(double x, double y, double z)
    {
      // Bessel's ellipsoid parameters
      double a = 6377397.15508;
      double f_1 = 299.152812853;
      double a_b = f_1 / (f_1 - 1);
      double p = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
      double e2 = 1 - Math.Pow(1 - 1 / f_1, 2);
      double th = Math.Atan(z * a_b / p);
      double st = Math.Sin(th);
      double ct = Math.Cos(th);
      double t = (z + e2 * a_b * a * Math.Pow(st, 3)) / (p - e2 * a * Math.Pow(ct, 3));

      double B = Math.Atan(t);
      double H = Math.Sqrt(1 + t * t) * (p - a / Math.Sqrt(1 + (1 - e2) * t * t));
      double L = 2 * Math.Atan(y / (p + x));

      double[] r = { B, L, H };
      return r;
    }

    private double distPoints(double x1, double y1, double x2, double y2)
    {
      double dist = hypot(x1 - x2, y1 - y2);
      if (dist < _EPS)
      {
        return 0;
      }
      return dist;
    }

    private double[] transformCoords(double xs, double ys, double zs)
    {
      double dx = -570.69;
      double dy = -85.69;
      double dz = -462.84; // shift
      double wx = 4.99821 / 3600 * Math.PI / 180;
      double wy = 1.58676 / 3600 * Math.PI / 180;
      double wz = 5.2611 / 3600 * Math.PI / 180; // rotation
      double m = -3.543e-6; // scale

      double xn = dx + (1 + m) * (+xs + wz * ys - wy * zs);
      double yn = dy + (1 + m) * (-wz * xs + ys + wx * zs);
      double zn = dz + (1 + m) * (+wy * xs - wx * ys + zs);

      double[] r = { xn, yn, zn };
      return r;
    }

    private double deg2rad(double deg)
    {
      return (deg * Math.PI / 180.0);
    }

    private double rad2deg(double rad)
    {
      return (rad / Math.PI * 180.0);
    }


    public static double hypot(double a, double b)
    {

      a = Math.Abs(a);

      b = Math.Abs(b);

      if (a < b)
      {

        double temp = a;

        a = b;

        b = temp;

      }

      if (a == 0.0)

        return 0.0;

      else
      {

        double ba = b / a;

        return a * Math.Sqrt(1.0 + ba * ba);

      }
    }
  }
}
