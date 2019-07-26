using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using CareebizExam.Common;
using CareebizExam.DTO;
using CareebizExam.Helpers;
using CareebizExam.Logic;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CareebizExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShapesController : Controller
    {
        private readonly IShapesLogic _shapesLogic;
        private IConverter _converter;

        public ShapesController(IShapesLogic shapesLogic, IConverter converter)
        {
            _shapesLogic = shapesLogic;
            _converter = converter;
        }
        [HttpGet(Name = "GetShape")]
        public IActionResult Get()
        {
            var response = new ListResponse<ShapesDTO>();
            try
            {
                var result = _shapesLogic.GetAllShapes();
                //throw new Exception();
               

                if (result == null)
                {
                    response.Messages = ResponseMessages.NotFound();
                    return NotFound(response);
                }
                response.Messages = ResponseMessages.Success();
                response.Model = result.ToList();
                return Ok(response);
                
            }
            catch (Exception exception)
            {
                response.Messages = ResponseMessages.InternalServerError(exception.ToString());
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id:int}", Name = "GetShapeByid")]
        public IActionResult Get(int id)
        {
            var response = new SingleResponse<ShapesDTO>();
            try
            {
                var result = _shapesLogic.GetShapeById(id);
                if (result == null)
                {
                    response.Messages = ResponseMessages.NotFound();
                    return NotFound(response);
                }
                response.Messages = ResponseMessages.Success();
                response.Model = result;
                return Ok(response);
            }
            catch (Exception exception)
            {
                response.Messages = ResponseMessages.InternalServerError(exception.ToString());
                return StatusCode(500, response);

            }
            
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] ShapesRequest request)
        {
            var response =  new SingleResponse<ShapesDTO>();
            try
            {
                if (request == null)
                {
                    response.Messages = ResponseMessages.BadRequest();
                    return NotFound(response);
                }
                if (!ModelState.IsValid)
                {

                    response.Messages = ResponseMessages.ModelValidate(ModelState);
                    return UnprocessableEntity(response);
                }

                var result = _shapesLogic.AddShape(request);

                response.Messages = ResponseMessages.Created();
                response.Model = result;
                return CreatedAtRoute("GetShapeByid",
                    new { id = result.ShapeId },
                    response);
            }
            catch (Exception exception)
            {
                response.Messages = ResponseMessages.InternalServerError(exception.ToString());
                return StatusCode(500, response);
            }



        }


        //[HttpGet(Name = "CreatePDF")]
        [Route("/api/Shapes/CreatePDF/{ids?}")]
        public IActionResult CreatePDF([FromQuery]int?[] ids)
        {
            var response = new ListResponse<ShapesDTO>();
            try
            {
                IEnumerable<ShapesDTO> result = null;
                result = ids.Length > 0 ? _shapesLogic.GetShapesByIds(ids.Cast<int>().ToArray()) : _shapesLogic.GetAllShapes();
                 
                var fileList = new List<string>();
                var zipFileName = Path.Combine(Directory.GetCurrentDirectory(),
                    @"PDF\" + DateTime.Now.ToFileTime() + ".zip");
                if (result.Count() == 0)
                {
                    response.Messages = ResponseMessages.NotFound();
                    return NotFound(response);
                }
                
                     foreach (var shapesDto in result)
                {
                    var fileName = Path.Combine(Directory.GetCurrentDirectory(),
                        @"PDF\" + DateTime.Now.ToFileTime() + ".pdf");
                    CreatePDFFileWithZip(shapesDto, fileName);
                    fileList.Add(fileName);
                }

                CreateZipFile(zipFileName, fileList);
                response.Messages = ResponseMessages.CreatedPDF("File has been created at this URL = " + zipFileName);
                response.Model = result.ToList();
                return Ok(response);
               
               
            }
            catch (Exception exception)
            {
                response.Messages = ResponseMessages.InternalServerError(exception.ToString());
                return StatusCode(500, response);
            }
            
            
            

        }

        private void CreatePDFFileWithZip( ShapesDTO shapesDto, string fileName)
        {
            try
            {
               
                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report",
                    Out = fileName   
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = TemplateGenerator.GetHTMLString(shapesDto),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

               
                var file = _converter.Convert(pdf);
                if (file != null)
                {
                    
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        public  void CreateZipFile(string fileName, IList<string> files)
        {
            try
            {
                // Create and open a new ZIP file
                var zip = ZipFile.Open(fileName, ZipArchiveMode.Create);
                foreach (var file in files)
                {
                    // Add the entry for each file
                    zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                }
                // Dispose of the object when we are done
                zip.Dispose();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            
        }

    }
}