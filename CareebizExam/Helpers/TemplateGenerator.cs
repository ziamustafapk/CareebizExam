using System.Text;
using CareebizExam.DTO;

namespace CareebizExam.Helpers
{
    public static class TemplateGenerator 
    {
        public static string GetHTMLString( ShapesDTO shapesDto)
        {
            

            var sb = new StringBuilder();
            sb.Append(@"<html><head>
                            </head>
                            <body>
                                <div class='header'><h1>  Title : " + shapesDto.Title + @"</h1></div>
<div class='header'><h1>Description : " + shapesDto.Description + @"</h1></div>
<div class='header'><h1>Longitude : " + shapesDto.Longitude + @"</h1></div>
<div class='header'><h1>Latitude : " + shapesDto.Latitude + @"</h1></div>
<div class='header'><h1>Area : " + shapesDto.Area + @"</h1></div>
                               
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
