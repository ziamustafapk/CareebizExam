using System;
using System.ComponentModel.DataAnnotations;

namespace CareebizExam.DTO
{
    public class ShapesDTO
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

    public class ShapesRequest
    {

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string ShapeType { get; set; }
        [StringLength(500, MinimumLength = 0)]
        public string Description { get; set; }
        [Range(1, 100)]
        public double Latitude { get; set; }
        [Range(1, 100)]
        public double Longitude { get; set; }
        [Range(1, 100)]
        public double Area { get; set; }
        

    }
}
