using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LedDigital.Helpers
{
    public class GraphicsPloter
    {
        /// <summary>
        /// 画直线段
        /// </summary>
        /// <param name="points"></param>
        /// <param name="clr"></param>
        /// <returns></returns>
        public static Path DrawLine(List<Point> points, Brush brush)
        {
            PathSegmentCollection segments = new PathSegmentCollection();
            for (int i = 1; i < points.Count; i++)
            {
                segments.Add(new LineSegment(points[i], true));
            }

            Path segment = new Path()
            {
                StrokeLineJoin = PenLineJoin.Round,
                Stroke = brush,
                Fill = brush,
                StrokeThickness = 0,
                Data = new PathGeometry()
                {
                    Figures = new PathFigureCollection()
                    {
                        new PathFigure(){IsClosed = true, IsFilled = true, StartPoint = points[0], Segments = segments}
                    }
                }
            };
            return segment;
        }

        /// <summary>
        /// 画圆点
        /// </summary>
        /// <param name="p"></param>
        /// <param name="radius"></param>
        /// <param name="clr"></param>
        /// <returns></returns>
        public static Path DrawEllipse(Point p, double radius, Brush brush)
        {
            Path segment = new Path()
            {
                StrokeLineJoin = PenLineJoin.Round,
                Stroke = brush,
                StrokeThickness = 0,

                Fill = brush,
                Data = new EllipseGeometry(p, radius, radius)
            };

            return segment;
        }

        public static Path DrawGeometry(string geometry)
        {
            Path path = new Path
            {
                Data = Geometry.Parse(geometry)
            };

            return path;
        }
    }
}
