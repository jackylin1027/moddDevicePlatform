using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LedDigital.Controls
{
    public class BaseLedDigitalSegmentCreator : ILedDigitalSegmentCreator
    {
        private DigitalParameter _parameter = null;

        public BaseLedDigitalSegmentCreator(DigitalParameter parameter)
        {
            _parameter = parameter;
        }

        public List<Point> GetBottomSegment()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(_parameter.SegmentThickness + _parameter.SegmentInterval, _parameter.DigitalHeight - _parameter.SegmentThickness));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentThickness - _parameter.SegmentInterval, _parameter.DigitalHeight - _parameter.SegmentThickness));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.BevelWidth - _parameter.SegmentInterval, _parameter.DigitalHeight - _parameter.BevelWidth));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.BevelWidth * 2 - _parameter.SegmentInterval, _parameter.DigitalHeight));
            points.Add(new Point(_parameter.BevelWidth * 2 + _parameter.SegmentInterval, _parameter.DigitalHeight));
            points.Add(new Point(_parameter.BevelWidth + _parameter.SegmentInterval, _parameter.DigitalHeight - _parameter.BevelWidth));

            return points;
        }

        public List<Point> GetDotSegment()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(_parameter.DigitalWidth + _parameter.SegmentThickness / 2, _parameter.DigitalHeight - _parameter.SegmentThickness / 2));
            return points;
        }

        public List<Point> GetDownColonSegment()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(_parameter.DigitalWidth / 2, 3 * _parameter.DigitalHeight / 4 - _parameter.SegmentThickness / 4));
            return points;
        }

        public List<Point> GetDownLeftSegment()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(0, _parameter.DigitalHeight - _parameter.BevelWidth * 2 - _parameter.SegmentInterval));
            points.Add(new Point(0, _parameter.DigitalHeight / 2 + _parameter.SegmentInterval + _parameter.SegmentThickness / 2));
            points.Add(new Point(_parameter.SegmentThickness / 2, _parameter.DigitalHeight / 2 + _parameter.SegmentInterval));
            points.Add(new Point(_parameter.SegmentThickness, _parameter.DigitalHeight / 2 + _parameter.SegmentThickness / 2 + _parameter.SegmentInterval));
            points.Add(new Point(_parameter.SegmentThickness, _parameter.DigitalHeight - _parameter.SegmentThickness - _parameter.SegmentInterval));
            points.Add(new Point(_parameter.BevelWidth, _parameter.DigitalHeight - _parameter.BevelWidth - _parameter.SegmentInterval));
            return points;
        }

        public List<Point> GetDownRightSegment()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(_parameter.DigitalWidth, _parameter.DigitalHeight - _parameter.BevelWidth * 2 - _parameter.SegmentInterval));
            points.Add(new Point(_parameter.DigitalWidth, _parameter.DigitalHeight / 2 + _parameter.SegmentInterval + _parameter.SegmentThickness / 2));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentThickness / 2, _parameter.DigitalHeight / 2 + _parameter.SegmentInterval));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentThickness, _parameter.DigitalHeight / 2 + _parameter.SegmentThickness / 2 + _parameter.SegmentInterval));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentThickness, _parameter.DigitalHeight - _parameter.SegmentThickness - _parameter.SegmentInterval));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.BevelWidth, _parameter.DigitalHeight - _parameter.BevelWidth - _parameter.SegmentInterval));
            return points;
        }

        public List<Point> GetMiddleSegment()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(_parameter.SegmentThickness + _parameter.SegmentInterval, _parameter.DigitalHeight / 2 - _parameter.SegmentThickness / 2));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentThickness - _parameter.SegmentInterval, _parameter.DigitalHeight / 2 - _parameter.SegmentThickness / 2));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentInterval - _parameter.SegmentThickness / 2, _parameter.DigitalHeight / 2));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentThickness - _parameter.SegmentInterval, _parameter.DigitalHeight / 2 + _parameter.SegmentThickness / 2));
            points.Add(new Point(_parameter.SegmentThickness + _parameter.SegmentInterval, _parameter.DigitalHeight / 2 + _parameter.SegmentThickness / 2));
            points.Add(new Point(_parameter.SegmentThickness / 2 + _parameter.SegmentInterval, _parameter.DigitalHeight / 2));
            return points;
        }

        public List<Point> GetTopSegment()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(_parameter.BevelWidth * 2 + _parameter.SegmentInterval, 0));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.BevelWidth * 2 - _parameter.SegmentInterval, 0));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.BevelWidth - _parameter.SegmentInterval, _parameter.BevelWidth));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentInterval - _parameter.SegmentThickness, _parameter.SegmentThickness));
            points.Add(new Point(_parameter.SegmentThickness + _parameter.SegmentInterval, _parameter.SegmentThickness));
            points.Add(new Point(_parameter.BevelWidth + _parameter.SegmentInterval, _parameter.BevelWidth));
            return points;
        }

        public List<Point> GetUpColonSegment()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(_parameter.DigitalWidth / 2, _parameter.DigitalHeight / 4 + _parameter.SegmentThickness / 4));
            return points;
        }

        public List<Point> GetUpLeftSegment()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(0, _parameter.BevelWidth * 2 + _parameter.SegmentInterval));
            points.Add(new Point(0, _parameter.DigitalHeight / 2 - _parameter.SegmentInterval - _parameter.SegmentThickness / 2));
            points.Add(new Point(_parameter.SegmentThickness / 2, _parameter.DigitalHeight / 2 - _parameter.SegmentInterval));
            points.Add(new Point(_parameter.SegmentThickness, _parameter.DigitalHeight / 2 - _parameter.SegmentThickness / 2 - _parameter.SegmentInterval));
            points.Add(new Point(_parameter.SegmentThickness, _parameter.SegmentThickness + _parameter.SegmentInterval));
            points.Add(new Point(_parameter.BevelWidth, _parameter.BevelWidth + _parameter.SegmentInterval));
            return points;
        }

        public List<Point> GetUpRightSegment()
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(_parameter.DigitalWidth, _parameter.BevelWidth * 2 + _parameter.SegmentInterval));
            points.Add(new Point(_parameter.DigitalWidth, _parameter.DigitalHeight / 2 - _parameter.SegmentThickness / 2 - _parameter.SegmentInterval));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentThickness / 2, _parameter.DigitalHeight / 2 - _parameter.SegmentInterval));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentThickness, _parameter.DigitalHeight / 2 - _parameter.SegmentThickness / 2 - _parameter.SegmentInterval));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.SegmentThickness, _parameter.SegmentThickness + _parameter.SegmentInterval));
            points.Add(new Point(_parameter.DigitalWidth - _parameter.BevelWidth, _parameter.BevelWidth + _parameter.SegmentInterval));
            return points;
        }
    }
}
