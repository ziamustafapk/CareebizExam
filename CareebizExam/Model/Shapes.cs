using System;

namespace CareebizExam.Model
{
    public class Shapes
    {
      
        public int ShapeId { get; set; }
        public string Title { get; set; }
        public string ShapeType { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Area { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


    }
}
