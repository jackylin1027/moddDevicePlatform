using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LedDigital.Controls
{
    public interface ILedDigitalSegmentCreator
    {
        List<Point> GetBottomSegment();
        List<Point> GetDotSegment();
        List<Point> GetDownColonSegment();
        List<Point> GetDownLeftSegment();
        List<Point> GetDownRightSegment();
        List<Point> GetMiddleSegment();
        List<Point> GetTopSegment();
        List<Point> GetUpColonSegment();
        List<Point> GetUpLeftSegment();
        List<Point> GetUpRightSegment();
    }
}
